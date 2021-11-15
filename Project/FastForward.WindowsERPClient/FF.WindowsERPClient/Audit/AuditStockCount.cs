using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;
using FF.BusinessObjects.InventoryNew;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.Threading.Tasks;
using FF.WindowsERPClient.Reports.Service;

namespace FF.WindowsERPClient.Audit
{
    public partial class AuditStockCount : FF.WindowsERPClient.Base
    {
        DataTable _jobDetails = new DataTable();
        List<AuditJobItem> _processedAuditJobItems;
        List<AuditJobSerial> _processedAuditJobSerials;
        List<AuditReportStatus> _auditReasons;
        List<AuditRemarkValue> _AuditRmkList = new List<AuditRemarkValue>();
        List<AuditJobSerial> _remarkedSerials;
        List<RequestApprovalSerials> _ExchnageItems = new List<RequestApprovalSerials>();
        List<AuditReportStatus> _stusList = new List<AuditReportStatus>();
        List<AuditMemebers> AuditMemberList = new List<AuditMemebers>();
        List<AuditJobSerial> _AuditJobSerialcharges = new List<AuditJobSerial>();
        //PriceDetailRef _priceBookLevelRef = new PriceDetailRef();
        private PriceBookLevelRef _priceBookLevelRef = null;

        int _indexVariance = 30;
        int _processedItemsStartIndex = 1;
        int _processedItemsEndIndex = 30;
        int _processedSerialStartIndex = 1;
        int _processedSerialEndIndex = 30;
        string _supervisorCode = null;
        int _currentSerialId = 0;
        private bool IsSaleFigureRoundUp = false;
        private bool _isStrucBaseTax = false;
        private decimal _tmpVat = 0;
        private decimal UnitAmt = 0;
        private Int32 _SeqNo = 0;
        private enum SerialUploadOption
        {
            UploadFromFile = 0,
            UploadSerial = 1
        };

        private enum AuditJobStatus
        {
            Pending = 1,
            Finished = 2,
            Canceled = 3,
            Approved = 4
        };


        //identify wether grid data edited
        private bool _isSerialGridDataEdited = false;
        private ListSortDirection _itemListSortDirecction = ListSortDirection.Ascending;
        private ListSortDirection _serailListSortDirection = ListSortDirection.Ascending;
        //   public static readonly ChannelOperator channelService = new ChannelOperator();
        private bool IsUserHasPermiossion = true;
        public AuditStockCount()
        {
            InitializeComponent();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearUiData();
            txtLocation.Text = BaseCls.GlbUserDefLoca;
            txtLocation.Focus();
        }

        private void ClearUiData()
        {
            txtJobNo.Enabled = true;
            txtJobNo.Text = null;
            lblJobStatus.Text = null;
            txtSubJobTotal.Text = "0";

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16072)) { txtSubJobTotal.ReadOnly = false; } else { txtSubJobTotal.ReadOnly = true; }

            txtRemarks.Text = null;
            txtSupervisor.Text = null;

            dtpFromDate.Value = DateTime.Today;
            dtpToDate.Value = DateTime.Today;
            btnSave.Visible = false;

            chkNewJob.Checked = false;
            chkNewJob.CheckState = CheckState.Unchecked;

            // dgvMemberList.DataSource = GetAuditMembers().DefaultView;
            lblJobStatus.Text = null;
            txtCommonSerialField.Text = null;
            _jobDetails = new DataTable();

            dgvSubJobList.Rows.Clear();
            dgvProcessedAudItems.Rows.Clear();
            dgvProcessedSerials.Rows.Clear();

            dgvProcessedAudItems.Refresh();
            dgvProcessedSerials.Refresh();
            dgvSubJobList.Refresh();
            pnlSerialInfo.Visible = false;
            btnUpdateRemark.Visible = false;

            _supervisorCode = null;
            EnableUserControls();

            cmbSerialPageCount.SelectedIndex = 0;
            cmbItemPageCount.SelectedIndex = 0;
            lblSupervisorName.Text = string.Empty;

            lblTotalDbQty.Text = "0.00";
            lblTotalLedgerQty.Text = "0.00";
            lblTotalPhysicalQty.Text = "0.00";
            mainLayout.Enabled = true;

            rmkPanel1.Visible = false;
            rmkPanel2.Visible = false;
            rmkPanel3.Visible = false;
            rmkPanel4.Visible = false;
            rmkPanel5.Visible = false;
            rmkPanel6.Visible = false;
            rmkPanel7.Visible = false;
            rmkPanel8.Visible = false;
            rmkPanel9.Visible = false;
            rmkPanel10.Visible = false;

            pnlMainSearchControl.Enabled = true;
            btnLayout.Enabled = true;
            grbAuditDetailPnl.Enabled = true;

            grbSubJobSelection.Visible = true;
            pnlAuditRmk.Visible = false;
            cmbItemPageCount.SelectedIndex = 0;
            cmbSerialPageCount.SelectedIndex = 0;
            chkIsExcessItem.Checked = false;
            lblUploadItem.Visible = false;
            txtUploadItem.Visible = false;

            txtUploadItem.Text = null;
            txtEmp.Text = null;
            chkViewComment.Checked = false;
            chkViewNonSerials.Checked = false;
            txtSearchSerial.Text = "";

            lblSuccessMsg.Visible = false;
            pbCorrect.Visible = false;
            pbIncorrect.Visible = false;
            pnlReports.Visible = false;
            pnlReports.SendToBack();
            AuditMemberList = new List<AuditMemebers>();

            txtEpfNo.Text = null;
            txtNicNo.Text = null;
            txtMemberName.Text = null;
            chkTeamLead.Checked = false;
            dgvMemberList.Rows.Clear();
            grbSupervisorSelection.Enabled = false;
            pnlReports.Enabled = true;
            grbSubJobSelection.Enabled = true;
            grbSubJobSelection.Visible = true;
            lblLocationName.Text = string.Empty;
            IsUserHasPermiossion = true;
        }

        public DataTable GetAuditMembers(string _jobNo = null)
        {
            DataTable _auditMembers = new DataTable();
            try
            {
                _auditMembers = CHNLSVC.Inventory.GetAuditMembers(BaseCls.GlbUserComCode, _jobNo);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
                MessageBox.Show("An error occurred while loading audit member details." + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _auditMembers;
        }

        private void AuditStockCount_Load(object sender, EventArgs e)
        {
            grbSupervisorSelection.Enabled = false;
            pnlReports.Visible = false;
            cmbSerialPageCount.SelectedIndex = 0;
            cmbItemPageCount.SelectedIndex = 0;

            //dgvMemberList.DataSource = GetAuditMembers().DefaultView;
            txtLocation.Text = BaseCls.GlbUserDefLoca;
            btnSave.Visible = false;
            txtLocation.Focus();
            txtLocation.Select();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtLocation.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Auditors:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ExchangeRequest:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtLocation.Text.ToUpper().Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ForwardInvoice:
                    {
                        DataTable _profitCenterDetails = new DataTable();
                        _profitCenterDetails = CHNLSVC.Sales.GetLocationCode(BaseCls.GlbUserComCode, txtLocation.Text.ToUpper().Trim());
                        string _tmpProfitCenter = txtLocation.Text;

                        if (_profitCenterDetails.Rows.Count > 0)
                        {
                            _tmpProfitCenter = _profitCenterDetails.Rows[0]["ml_def_pc"] == DBNull.Value ? txtLocation.Text : _profitCenterDetails.Rows[0]["ml_def_pc"].ToString().ToUpper();

                        }
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + _tmpProfitCenter + seperator + lblItem.Text + seperator);
                        // paramsText.Append(BaseCls.GlbUserComCode + seperator + txtLocation.Text + seperator + lblItem.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.FixAssetRequest:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtLocation.Text + seperator + lblItem.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Department:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType:
                    {
                        paramsText.Append("AUDT" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GetItmByTypeNew:
                    {
                        paramsText.Append("V" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void btnSearchLocation_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.IsReturnFullRow = true;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLocation;
                _CommonSearch.ShowDialog();
                txtLocation.Select();

                if (!string.IsNullOrEmpty(txtLocation.Text))
                {
                    List<string> _tmlLocationDetails = new List<string>();
                    _tmlLocationDetails = _CommonSearch.UserSelectedRow.Split(new string[] { "|" }, StringSplitOptions.None).ToList();

                    if ((_tmlLocationDetails != null) && (_tmlLocationDetails.Count > 0)) { lblLocationName.Text = _tmlLocationDetails[1]; }

                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
                MessageBox.Show("An error occurred while loading location details." + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchLocation_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtJobNo.Focus();
            }
        }

        private void btnSearchJob_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtLocation.Text))
                {
                    MessageBox.Show("Please select the location", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLocation.Focus();
                    return;
                }

                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                _CommonSearch.dtpTo.Value = DateTime.Now.Date;
                _CommonSearch.dtpFrom.Value = _CommonSearch.dtpTo.Value.Date.AddMonths(-1);
                DataTable _result = CHNLSVC.Inventory.SearchAuditJobs(_CommonSearch.SearchParams, null, null, _CommonSearch.dtpFrom.Value.Date, _CommonSearch.dtpTo.Value.Date);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtJobNo;
                _CommonSearch.ShowDialog();
                txtJobNo.Select();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
                MessageBox.Show("An error occurred while loading job details." + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchJob_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                chkNewJob.Focus();
            }
        }

        private void chkNewJob_CheckedChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtJobNo.Text))
            //{
            //    if (chkNewJob.Checked == true)
            //    {
            //        chkNewJob.Checked = false;
            //        txtJobNo.Text = string.Empty;
            //        txtJobNo.Enabled = false;
            //        // btnEdit.Visible = false;
            //        btnSave.Visible = true;
            //        btnFinish.Visible = false;
            //        lblJobStatus.Text = "NEW JOB";
            //        btnReset.Visible = false;
            //        grbSupervisorSelection.Enabled = true;
            //    }
            //    else
            //    {
            //        chkNewJob.Checked = false;
            //        txtJobNo.Text = string.Empty;
            //        txtJobNo.Enabled = true;
            //        btnFinish.Visible = true;
            //        //btnEdit.Visible = true;
            //        btnSave.Visible = false;
            //        lblJobStatus.Text = null;
            //        btnReset.Visible = true;
            //        grbSupervisorSelection.Enabled = false;
            //    }
            //}
            //else
            //{
            //    ClearUiData();
            //    chkNewJob.Checked = true;
            //}

        }

        private void chkNewJob_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (chkNewJob.Checked == true)
                {
                    chkNewJob.Checked = false;
                }
                else
                {
                    chkNewJob.Checked = true;
                }
                txtEpfNo.Focus();
            }
        }

        private void txtSubJobTotal_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar.Equals((char)Keys.Back))
            {
                return;
            }
            else if (e.KeyChar.Equals((char)Keys.Enter))
            {
                return;
            }
            else
            {
                if (!IsNumeric(e.KeyChar.ToString()))
                {
                    e.Handled = true;
                    return;
                }
            }

        }

        private void txtSubJobTotal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRemarks.Focus();
            }
        }

        //private void txtSupervisor_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.F2)
        //    {
        //        btnSearchSupervisor_Click(null, null);
        //    }
        //    else if (e.KeyCode == Keys.Enter)
        //    {
        //        btnSave.Focus();
        //    }
        //}

        //private void btnSearchSupervisor_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (chkNewJob.Checked)
        //        {
        //            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
        //            _CommonSearch.ReturnIndex = 0;
        //            _CommonSearch.IsSearchEnter = true;
        //            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Auditors);
        //            DataTable _result = CHNLSVC.CommonSearch.SearchAllEmployee(_CommonSearch.SearchParams, null, null);
        //            _CommonSearch.dvResult.DataSource = _result;
        //            _CommonSearch.BindUCtrlDDLData(_result);
        //            _CommonSearch.obj_TragetTextBox = txtSupervisor;
        //            _CommonSearch.ShowDialog();
        //            txtSupervisor.Select();
        //            _supervisorCode = txtSupervisor.Text;

        //            if (!string.IsNullOrEmpty(_supervisorCode))
        //            {
        //                _result = CHNLSVC.CommonSearch.SearchAllEmployee(BaseCls.GlbUserComCode + "|", "EPF NO", _supervisorCode);
        //                if (_result.Rows.Count > 0)
        //                {
        //                    lblSupervisorName.Text = _result.AsEnumerable().Where(x => x.Field<string>("EPF NO").ToString() == _supervisorCode).Select(x => x.Field<string>("FIRST NAME")).First().ToString();
        //                }
        //            }
        //            else { lblSupervisorName.Text = string.Empty; }
        //        }

        //       // txtSupervisor.Text = _result.AsEnumerable().Where(x => x.Field<string>("Id").ToString() == _supervisorCode).Select(x => x.Field<string>("NAME")).First().ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Cursor = Cursors.Default;
        //        CHNLSVC.CloseAllChannels();
        //        MessageBox.Show("An error occurred while loading auditor's details." + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void txtLocation_DoubleClick(object sender, EventArgs e)
        {
            btnSearchLocation_Click(null, null);
        }

        private void txtJobNo_DoubleClick(object sender, EventArgs e)
        {
            btnSearchJob_Click(null, null);
        }

        //private void txtSupervisor_DoubleClick(object sender, EventArgs e)
        //{
        //    if (chkNewJob.Checked)
        //    {
        //        btnSearchSupervisor_Click(null, null);
        //    }

        //}

        private void txtLocation_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtLocation.Text))
                {
                    DataTable _locations = new DataTable();
                    _locations = CHNLSVC.CommonSearch.GetLocationSearchData(BaseCls.GlbUserComCode + "|", "CODE", txtLocation.Text);

                    if (_locations.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid location code -" + txtLocation.Text, "Stock Verification-Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtLocation.Focus();
                        return;
                    }
                    else
                    {
                        ClearUiData();
                        lblLocationName.Text = _locations.Rows[0]["Description"] == DBNull.Value ? string.Empty : _locations.Rows[0]["Description"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
                MessageBox.Show("An error occurred while loading location's details." + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtJobNo_Leave(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                LoadJobDetails();
            }

        }

        private bool IsValidToProceed()
        {
            try
            {
                if (string.IsNullOrEmpty(txtLocation.Text))
                {
                    MessageBox.Show("Please select the location", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLocation.Focus();
                    return false;
                }
                else if ((string.IsNullOrEmpty(txtLocation.Text)) && (!chkNewJob.Checked))
                {
                    MessageBox.Show("Please enter the job no", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtJobNo.Focus();
                    return false;
                }
                else if (dtpFromDate.Value.Date > dtpToDate.Value.Date)
                {
                    MessageBox.Show("Invalid date range. 'From Date' cannot be greater than 'To Date'", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpFromDate.Focus();
                    return false;
                }
                else if (dtpToDate.Value.Date < dtpFromDate.Value.Date)
                {
                    MessageBox.Show("Invalid date range. 'To Date' cannot be less than 'From Date'", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpToDate.Focus();
                    return false;
                }
                else if (!IsNumeric(txtSubJobTotal.Text))
                {
                    MessageBox.Show("Invalid number format. No of Sub Job must be a numeric value", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSubJobTotal.Focus();
                    return false;
                }
                else if (int.Parse(txtSubJobTotal.Text) < 0)
                {
                    MessageBox.Show("No of Sub Job cannot be a negative or zero value", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSubJobTotal.Focus();
                    return false;
                }
                //else if (string.IsNullOrEmpty(txtSupervisor.Text))
                //{
                //    MessageBox.Show("Please select the tech lead", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    txtSupervisor.Focus();
                //    _isValid = false;
                //}
                else if (dgvMemberList.Rows.Count == 0)
                {
                    MessageBox.Show("Please enter the verification member details", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEpfNo.Focus();
                    return false;

                    //int _checkedCount = 0;
                    //foreach (DataGridViewRow _grvRow in dgvMemberList.Rows)
                    //{
                    //    if (Convert.ToBoolean((_grvRow.Cells["colMemberChecked"] as DataGridViewCheckBoxCell).Value))
                    //    {
                    //        _checkedCount += 1;
                    //    }
                    //}

                    //if (_checkedCount <= 0)
                    //{
                    //    MessageBox.Show("Please select audit members", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    dgvMemberList.Focus();
                    //    return false;
                    //}
                }

                if ((AuditMemberList != null) && (AuditMemberList.Count > 0))
                {
                    int _teamLeadCount = 0;
                    _teamLeadCount = AuditMemberList.Where(x => x.Ajm_Mem_type == "TEAM LEAD").Select(x => x.Ajm_Mem_Id).Count();
                    if (_teamLeadCount == 0)
                    {
                        MessageBox.Show("Please select at least one TEAM LEAD!", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEpfNo.Focus();
                        return false;
                    }
                }

                DataTable _result = new DataTable();
                _result = CHNLSVC.Inventory.SearchAuditJobs(BaseCls.GlbUserComCode + "|" + txtLocation.Text.Trim() + "|", null, null, null, null);
                if (_result.Rows.Count > 0)
                {
                    List<DataRow> _tmpRowList = _result.Rows.Cast<DataRow>().Where(x => x.Field<string>("Status") == "PENDING").ToList();
                    if (_tmpRowList.Count > 0)
                    {
                        MessageBox.Show("New job cannot be created, when there are pending jobs are available", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnSave.Focus();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("An error occurred while validing entered details." + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult _dialog = MessageBox.Show("Do you want to save the current job ?", "Stock Verification - Save Jobs", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (_dialog == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            if (IsValidToProceed())
            {
                SaveAuditProjectPlane();
            }
        }

        private void SaveAuditProjectPlane()
        {
            int rowsAffected = 0;
            string _docNo = string.Empty;
            string _message = null;

            PhysicalStockVerificationHdr _PhysicalStockVerificationHdr = new PhysicalStockVerificationHdr();

            try
            {
                _PhysicalStockVerificationHdr.AUSH_COM = BaseCls.GlbUserComCode;
                _PhysicalStockVerificationHdr.AUSH_CRE_BY = BaseCls.GlbUserID;
                _PhysicalStockVerificationHdr.AUSH_DT = DateTime.Today;
                _PhysicalStockVerificationHdr.AUSH_FRM_DT = dtpFromDate.Value.Date;
                _PhysicalStockVerificationHdr.AUSH_TO_DT = dtpToDate.Value.Date;
                _PhysicalStockVerificationHdr.AUSH_JOB = txtJobNo.Text.Trim();
                _PhysicalStockVerificationHdr.AUSH_LOC = txtLocation.Text.Trim();
                _PhysicalStockVerificationHdr.AUSH_MOD_BY = BaseCls.GlbUserID;
                _PhysicalStockVerificationHdr.AUSH_REM = txtRemarks.Text;
                _PhysicalStockVerificationHdr.AUSH_STUS = "P";
                _PhysicalStockVerificationHdr.AUSH_NO_JOB = int.Parse(txtSubJobTotal.Text);
                _PhysicalStockVerificationHdr.AUSH_SUPVIS_BY = _supervisorCode.Trim();
                _PhysicalStockVerificationHdr.AUSH_SESSION_ID = BaseCls.GlbUserSessionID;

                if (chkNewJob.Checked) { _PhysicalStockVerificationHdr.IsNewJob = true; }
                else { _PhysicalStockVerificationHdr.IsNewJob = false; }

                _PhysicalStockVerificationHdr.AUSH_DIPARTMENT = txtDept.Text.ToString();
                _PhysicalStockVerificationHdr.AUSH_REASON = txtSubType.Text.ToString();

                MasterAutoNumber _autoNumber = GenerateMasterAutoNumber();

                ////List<AuditMemebers> _auditMembers = new List<AuditMemebers>();
                //if (dgvMemberList.Rows.Count > 0)
                //{
                //    foreach (DataGridViewRow _grvRow in dgvMemberList.Rows)
                //    {
                //        if (!Convert.ToBoolean((_grvRow.Cells["colMemberChecked"] as DataGridViewCheckBoxCell).Value))
                //        {
                //            AuditMemberList.RemoveAll(x => x.Ajm_Mem_Id == _grvRow.Cells["colEpfNo"].Value.ToString());
                //        }

                //        //if (Convert.ToBoolean((_grvRow.Cells["dgvcMemberChecked"] as DataGridViewCheckBoxCell).Value))
                //        //{
                //        //    AuditMemebers _auditMemeber = new AuditMemebers();
                //        //    _auditMemeber.Ajm_Job_no = _docNo;
                //        //    _auditMemeber.Ajm_Mem_Id = _grvRow.Cells["dgvcMemberId"].Value.ToString();
                //        //    _auditMemeber.Ajm_Cre_by = BaseCls.GlbUserID;
                //        //    _auditMemeber.Ajm_Cre_Dt = DateTime.Today.Date;
                //        //    _auditMembers.Add(_auditMemeber);
                //        //}
                //    }
                //}

                rowsAffected = CHNLSVC.Inventory.SaveStockVerification(_PhysicalStockVerificationHdr, _autoNumber, out _docNo, out _message, AuditMemberList);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Job # " + _docNo + " saved successfully", "Stock Verification-Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearUiData();
                    txtJobNo.Text = _docNo;


                    txtJobNo_Leave(null, null);
                    //if (dgvSubJobList.Rows.Count == 1)
                    //{
                    //    dgvSubJobList.Rows[0].Cells["chkColSelectJob"].Value = true;
                    //    dgvSubJobList_CellContentClick(null, new DataGridViewCellEventArgs(0, 0));
                    //    btnProcess.Focus();
                    //}

                }
                else
                {
                    MessageBox.Show("Couldn't save the job details " + Environment.NewLine + _message, "Stock Verification-Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
                MessageBox.Show("An error occurred while saving audit job " + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private MasterAutoNumber GenerateMasterAutoNumber()
        {
            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_cate_cd = txtLocation.Text.ToUpper().Trim();  // string.IsNullOrEmpty(Session["UserDefLoca"].ToString()) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "MTJO";
            masterAuto.Aut_number = 0;
            masterAuto.Aut_start_char = "MTJO";
            masterAuto.Aut_year = DateTime.Today.Year;
            return masterAuto;
        }

        private void dgvSubJobList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvSubJobList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            if (dgvSubJobList.Rows.Count > 0)
            {
                if (e.ColumnIndex == 0)
                {
                    if (Convert.ToBoolean(dgvSubJobList.Rows[e.RowIndex].Cells["chkColSelectJob"].Value))
                    {
                        dgvSubJobList.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Aquamarine;
                        dgvSubJobList.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Aquamarine;
                    }
                    else
                    {
                        dgvSubJobList.Rows[e.RowIndex].DefaultCellStyle.BackColor = SystemColors.Window;
                        dgvSubJobList.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = SystemColors.Window;
                    }
                    dgvSubJobList.Refresh();
                }
            }
        }

        private void cmbSubJobSelection_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == Convert.ToChar(Keys.Up)) || (e.KeyChar == Convert.ToChar(Keys.Down)))
            {
                return;
            }
            else
            {
                e.Handled = true;
                return;
            }
        }

        private void LoadJobDetails()
        {
            try
            {
                btnProcess.Enabled = true;
                btnFinish.Enabled = true;
                btnViewSerial.Enabled = true;
                IsUserHasPermiossion = true;

                _indexVariance = 30;
                _processedItemsStartIndex = 1;
                _processedItemsEndIndex = 30;
                _processedSerialStartIndex = 1;
                _processedSerialEndIndex = 30;
                cmbItemPageCount.SelectedIndex = 0;
                cmbSerialPageCount.SelectedIndex = 0;

                if (string.IsNullOrEmpty(txtLocation.Text))
                {
                    MessageBox.Show("Please select the location", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLocation.Focus();
                    return;
                }

                _jobDetails = new DataTable();
                _jobDetails = CHNLSVC.Inventory.GetAuditJobDetails(BaseCls.GlbUserComCode, txtLocation.Text.Trim(), txtJobNo.Text.Trim());

                if (_jobDetails.Rows.Count > 0)
                {
                    DataRow _details = _jobDetails.Rows[0];
                    dtpFromDate.Text = Convert.ToDateTime(_details["Job Start Date"].ToString()).ToString("dd-MMM-yyyy");
                    dtpToDate.Text = Convert.ToDateTime(_details["Job End Date"].ToString()).ToString("dd-MMM-yyyy");
                    lblJobStatus.Text = string.IsNullOrEmpty(_details["Status"].ToString()) ? null : _details["Status"].ToString();
                    txtSubJobTotal.Text = (_jobDetails.Rows.Count - 1).ToString(); //When audit job create by deafault a job will be created for main job. therefore when consider the sub job count we need to ignore it.
                    txtRemarks.Text = string.IsNullOrEmpty(_details["Remarks"].ToString()) ? string.Empty : _details["Remarks"].ToString();

                    _supervisorCode = string.IsNullOrEmpty(_details["Supervised By"].ToString()) ? null : _details["Supervised By"].ToString();

                    string _tmpUser = _details["User"] == DBNull.Value ? string.Empty : _details["User"].ToString();

                    txtDept.Text = string.IsNullOrEmpty(_details["aush_dipartment"].ToString()) ? string.Empty : _details["aush_dipartment"].ToString();
                    txtSubType.Text = string.IsNullOrEmpty(_details["aush_reason"].ToString()) ? string.Empty : _details["aush_reason"].ToString();
                    if (_tmpUser.ToUpper().Trim() != BaseCls.GlbUserID)
                    {
                        IsUserHasPermiossion = false;
                    }

                    //if (!string.IsNullOrEmpty(_supervisorCode))
                    //{
                    //    DataTable _result = CHNLSVC.Inventory.SearchAuditMembers(BaseCls.GlbUserComCode + "|", "ID", _supervisorCode);
                    //    if (_result.Rows.Count > 0)
                    //    {
                    //        txtSupervisor.Text = _supervisorCode;
                    //        List<DataRow>  _tmpResult= _result.AsEnumerable().Where(x => x.Field<string>("Id") == _supervisorCode).ToList();
                    //        if (_tmpResult.Count > 0)
                    //        {
                    //            lblSupervisorName.Text = _tmpResult.Select(x => x.Field<string>("NAME")).First();
                    //        }                            
                    //    }
                    //}


                    //Bind sub job details to gridview
                    BindingSource _bindSource = new BindingSource();
                    _bindSource.DataSource = _jobDetails.AsEnumerable().Select(x => new
                    {
                        _jobNo = x.Field<string>("Sub Job No"),
                        _date = x.Field<string>("Date"),
                        _status = x.Field<string>("Sub Job Status"),
                        _startDate = x.Field<string>("Start Date"),
                        _endDate = x.Field<string>("End Date")
                    }).ToList();
                    dgvSubJobList.DataSource = _bindSource;
                    dgvSubJobList.Refresh();

                    //Get existing members in current job
                    DataTable _existingMembers = new DataTable();
                    _existingMembers = GetAuditMembers(txtJobNo.Text.Trim());

                    if (_existingMembers.Rows.Count > 0)
                    {
                        AuditMemberList = new List<AuditMemebers>();
                        foreach (DataRow row in _existingMembers.Rows)
                        {
                            AuditMemebers _member = new AuditMemebers();
                            _member.Ajm_Mem_Id = row["ajm_mem_id"] == DBNull.Value ? string.Empty : row["ajm_mem_id"].ToString();
                            _member.Ajm_Mem_Nic = row["ajm_mem_nic"] == DBNull.Value ? string.Empty : row["ajm_mem_nic"].ToString();
                            _member.Ajm_Mem_Name = row["ajm_mem_name"] == DBNull.Value ? string.Empty : row["ajm_mem_name"].ToString();
                            _member.Ajm_Mem_type = row["ajm_mem_type"] == DBNull.Value ? string.Empty : row["ajm_mem_type"].ToString();
                            _member.Ajm_Job_no = row["ajm_job_no"] == DBNull.Value ? string.Empty : row["ajm_job_no"].ToString();
                            _member.Ajm_Cre_by = row["ajm_cre_by"] == DBNull.Value ? string.Empty : row["ajm_cre_by"].ToString();
                            AuditMemberList.Add(_member);
                        }

                        if ((AuditMemberList != null) && (AuditMemberList.Count > 0))
                        {
                            BindingSource _memberSource = new BindingSource();
                            _memberSource.DataSource = AuditMemberList.Select(x => new { x.Ajm_Mem_Name, x.Ajm_Mem_Id, x.Ajm_Mem_Nic, x.Ajm_Mem_type }).ToList();
                            dgvMemberList.Rows.Clear();
                            dgvMemberList.DataSource = _memberSource;
                        }

                        //if (dgvMemberList.Rows.Count > 0)
                        //{
                        //    dgvMemberList.Rows.Cast<DataGridViewRow>().ToList().ForEach(x => x.Cells["colMemberChecked"].Value = true);
                        //}
                        ////Select existing members from member list
                        //foreach (DataRow _member in _existingMembers.Rows)
                        //{
                        //    List<DataGridViewRow> _rowList = dgvMemberList.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["dgvcMemberId"].Value.Equals(_member["Id"])).ToList();
                        //    if (_rowList.Count > 0)
                        //    {
                        //        int _rowIndex = _rowList.Select(x => x.Index).First();
                        //        dgvMemberList.Rows[_rowIndex].Cells["dgvcMemberChecked"].Value = true;
                        //    }                            
                        //}
                    }

                    DisableUserControls();

                    if (dgvSubJobList.Rows.Count == 1)
                    {
                        dgvSubJobList.Rows[0].Cells["chkColSelectJob"].Value = true;
                        dgvSubJobList_CellContentClick(null, new DataGridViewCellEventArgs(0, 0));
                        btnProcess.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid job no -" + txtJobNo.Text, "Stock Verification-Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtLocation.Focus();
                    return;
                }

            }
            catch (Exception ex)
            {
                EnableUserControls();
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
                MessageBox.Show("An error occurred while loading audit job details." + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UploadSerialFiles(string _uploadedText, SerialUploadOption _serialUploadOption)
        {
            try
            {
                if (string.IsNullOrEmpty(txtLocation.Text))
                {
                    MessageBox.Show("Please select the location", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLocation.Focus();
                    return;
                }

                if (cmbSubJobSelection.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a sub job #", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbSubJobSelection.Focus();
                    return;
                }

                if ((chkIsExcessItem.Checked) && (string.IsNullOrEmpty(txtCommonSerialField.Text)))
                {
                    MessageBox.Show("Please enter the excess serial no", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUploadItem.Focus();
                    return;
                }

                if ((chkIsExcessItem.Checked) && (string.IsNullOrEmpty(txtUploadItem.Text)))
                {
                    MessageBox.Show("Please enter the excess item code", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUploadItem.Focus();
                    return;
                }

                List<string> _serialList = GetUploadedSerialList(_uploadedText, _serialUploadOption);
                if (_serialList.Count > 0)
                {
                    if (_jobDetails.Rows.Count > 0)
                    {
                        string _tmpSeqNo = _jobDetails.AsEnumerable().Where(x => x.Field<string>("Sub Job No") == cmbSubJobSelection.SelectedItem.ToString()).Distinct().Select(x => x.Field<string>("Seq #")).First().ToString();
                        string _resultMessage = null;

                        CHNLSVC.Inventory.UploadPhysicallyAvailableSerials(BaseCls.GlbUserComCode, txtLocation.Text.Trim(), _serialList, cmbSubJobSelection.SelectedItem.ToString(), Convert.ToInt32(_tmpSeqNo), BaseCls.GlbUserID, chkIsExcessItem.Checked, txtUploadItem.Text.Trim(), BaseCls.GlbUserSessionID, out _resultMessage);

                        if (!string.IsNullOrEmpty(_resultMessage))
                        {
                            MessageBox.Show("Couldn't update serial details" + Environment.NewLine + _resultMessage, "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Serial details uploaded successfully", "Stock Verification-Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCommonSerialField.Text = string.Empty;
                            txtUploadItem.Text = null;
                            chkIsExcessItem.Checked = false;
                            _processedAuditJobSerials = new List<AuditJobSerial>();
                            _processedAuditJobSerials = LoadSerialDetails(1, _indexVariance);
                            BindSerialDetails(_processedAuditJobSerials);
                            txtUploadItem.Text = string.Empty; // add by akila 2017/05/25
                        }
                    }
                    else
                    {
                        MessageBox.Show("Job details not found. Please check the correct sub job #", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtJobNo.Focus();
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
                MessageBox.Show("An error occurred while uploading serials." + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            chkIsExcessItem.Checked = false;
        }

        private List<string> GetUploadedSerialList(string _uploadedText, SerialUploadOption _uploadOption)
        {
            List<string> _uploadedSerials = new List<string>();

            try
            {
                if (_uploadOption == SerialUploadOption.UploadFromFile)
                {
                    if (File.Exists(_uploadedText))
                    {
                        string _fileExtention = Path.GetExtension(_uploadedText).ToLower();
                        if (_fileExtention == ".txt")
                        {
                            //Read from text file
                            _uploadedSerials = File.ReadAllLines(_uploadedText).Select(x => x.Trim()).Distinct().ToList<string>();
                        }
                        else if ((_fileExtention == ".xls") || (_fileExtention == ".xlsx"))
                        {
                            //Read from excel
                            DataTable _dataTbale = new DataTable();
                            string _excelConnectionString = ConfigurationManager.ConnectionStrings[_fileExtention == ".xls" ? "ConStringExcel03" : "ConStringExcel07"].ConnectionString;
                            _excelConnectionString = String.Format(_excelConnectionString, _uploadedText, "0");

                            OleDbConnection connExcel = new OleDbConnection(_excelConnectionString);
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                cmdExcel.Connection = connExcel;
                                //Get the name of First Sheet
                                connExcel.Open();
                                DataTable dtExcelSchema;
                                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                cmdExcel.CommandText = "SELECT distinct * From [" + SheetName + "]";

                                using (OleDbDataAdapter oda = new OleDbDataAdapter())
                                {
                                    oda.SelectCommand = cmdExcel;
                                    oda.Fill(_dataTbale);
                                }
                                connExcel.Close();
                            }
                            _uploadedSerials = _dataTbale.AsEnumerable().Select(x => x.Field<string>(0)).ToList().Select(x => x.Trim()).ToList<string>();

                        }
                        else
                        {
                            MessageBox.Show("File extention does not support. File extention shoule be (.txt, .xls or .xlsx)", "Stock Verification - File Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("File does not exists", "Stock Verification - File Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (_uploadOption == SerialUploadOption.UploadSerial)
                {
                    _uploadedSerials.Add(_uploadedText);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _uploadedSerials;
        }

        private void btnUploadSerials_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSubJobSelection.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the sub job #", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbSubJobSelection.Focus();
                    return;
                }

                SerialUploadOption _uploadOption = SerialUploadOption.UploadFromFile;
                if ((string.IsNullOrEmpty(txtCommonSerialField.Text)) || (string.IsNullOrWhiteSpace(txtCommonSerialField.Text)))
                {
                    //if text field is empty, search for serial file
                    OpenFileDialog _fileDialog = new OpenFileDialog();
                    _fileDialog.DefaultExt = ".txt";
                    _fileDialog.Filter = "Text (*.txt)|*.txt|Excel 97-2003 (*.xls)|*.xls| Excel workbook (*.xlsx)|*.xlsx";
                    _fileDialog.Multiselect = false;

                    DialogResult _dialogResult = _fileDialog.ShowDialog();
                    if (_dialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        txtCommonSerialField.Text = _fileDialog.FileName;
                        if (IsFileOpen(new FileInfo(txtCommonSerialField.Text)))
                        {
                            MessageBox.Show("This file already open by another program. Please close it and continue", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            _uploadOption = SerialUploadOption.UploadFromFile;
                            UploadSerialFiles(txtCommonSerialField.Text, _uploadOption);
                        }
                    }
                }
                else
                {
                    //if text field value is not null, then identify whether entered value is file path or serial #
                    if ((txtCommonSerialField.Text.ToLower().Contains(".")) || (txtCommonSerialField.Text.ToLower().Contains("'\'")) || (txtCommonSerialField.Text.ToLower().Contains("':\'")))
                    {
                        _uploadOption = SerialUploadOption.UploadFromFile;
                        if (IsFileOpen(new FileInfo(txtCommonSerialField.Text)))
                        {
                            MessageBox.Show("This file already open by another program. Please close it and continue", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        _uploadOption = SerialUploadOption.UploadSerial;
                    }
                    UploadSerialFiles(txtCommonSerialField.Text, _uploadOption);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("An error occurred while uploading serials." + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLocation.Text))
            {
                MessageBox.Show("Please select the location", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLocation.Focus();
                return;
            }

            if ((chkNewJob.Checked) && (string.IsNullOrEmpty(txtJobNo.Text)))
            {
                MessageBox.Show("Please save the job befoer process", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJobNo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please enter the job no", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJobNo.Focus();
                return;
            }

            if (lblJobStatus.Text == "FINISHED")
            {
                MessageBox.Show("Invalid operation, cannot process finished job", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJobNo.Focus();
                return;
            }

            this.pnlMainSearchControl.Enabled = false;
            this.grbAuditDetailPnl.Enabled = false;
            lblUserWait.Text = "Job details are processing. Please wait";
            userWaitControl.Visible = true;
            appBackGroundWorker.RunWorkerAsync();
        }

        private void dgvSubJobList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void cmbSubJobSelection_Leave(object sender, EventArgs e)
        {
            txtCommonSerialField.Focus();
        }

        private void txtCommonSerialField_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCommonSerialField.Text))
            {
                string _tmpSerial = txtCommonSerialField.Text.Trim().ToUpper();
                if (_tmpSerial == "NA")
                {
                    txtCommonSerialField.Text = "N/A";
                }

                if (chkIsExcessItem.Checked)
                {
                    if ((!string.IsNullOrEmpty(txtCommonSerialField.Text)) && (!string.IsNullOrEmpty(txtUploadItem.Text)))
                    {
                        _tmpSerial = txtCommonSerialField.Text.Trim().ToUpper();
                        if (_tmpSerial != "N/A")
                        {
                            DataTable tmpSerialDetails = new DataTable();
                            string _tmpItemCode = string.IsNullOrEmpty(txtUploadItem.Text) ? null : txtUploadItem.Text.ToUpper().Trim(); // add by akila 2017/05/25
                            tmpSerialDetails = CHNLSVC.Inventory.GetSerialDetailsBySerial(BaseCls.GlbUserComCode, txtLocation.Text.Trim().ToUpper(), _tmpItemCode, _tmpSerial);
                            if (tmpSerialDetails.Rows.Count > 0)
                            {
                                MessageBox.Show("Invalid Serial #" + Environment.NewLine + "Serial # - " + _tmpSerial + " all ready exist in current location", "Stock Verification-Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtCommonSerialField.Text = string.Empty;
                                txtCommonSerialField.Focus();
                                return;
                            }
                        }
                    }
                }
            }


            btnUploadSerials.Focus();
        }

        private void btnSerialDetailsCloseBtn_Click(object sender, EventArgs e)
        {
            if (_isSerialGridDataEdited)
            {
                if (GetUserEnteredGridData().Count > 0)
                {
                    this.Cursor = Cursors.Default;
                    DialogResult _dialogResult = MessageBox.Show("Entered remarks will lost. Do you want to proceed ?", "Stock Verification - Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (_dialogResult == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                }
            }

            _indexVariance = 30;
            _processedItemsStartIndex = 1;
            _processedItemsEndIndex = 30;
            _processedSerialStartIndex = 1;
            _processedSerialEndIndex = 30;
            cmbItemPageCount.SelectedIndex = 0;
            cmbSerialPageCount.SelectedIndex = 0;
            pnlSerialInfo.Visible = false;
            txtSearchSerial.Text = "";
        }

        private void btnViewSerial_Click(object sender, EventArgs e)
        {
            try
            {
                //pnlSerialInfo.Visible = true;
                //pnlSerialInfo.Size = new Size(820, 520);

                List<string> _selectedSubJobs = new List<string>();
                if (dgvSubJobList.Rows.Count > 0)
                {
                    foreach (DataGridViewRow _dataRow in dgvSubJobList.Rows)
                    {
                        bool _isRowSelected = _dataRow.Cells["chkColSelectJob"].Value == DBNull.Value ? false : Convert.ToBoolean(_dataRow.Cells["chkColSelectJob"].Value);
                        if (_isRowSelected)
                        {
                            _selectedSubJobs.Add(_dataRow.Cells["txtColSubJobNo"].Value.ToString());
                        }
                    }

                    if (_selectedSubJobs.Count > 0)
                    {
                        pnlSerialInfo.Visible = true;
                        pnlSerialInfo.Size = new Size(820, 520);
                        this.pnlSerialInfo.Location = new System.Drawing.Point(3, 0);
                        //Bind selected job numbers to drop down
                        BindingSource _cmbDataBind = new BindingSource();
                        _cmbDataBind.DataSource = _selectedSubJobs;
                        cmbSubJobSelection.DataSource = _cmbDataBind;
                        cmbSubJobSelection.SelectedIndex = 0;

                        //Get processed audit item details
                        _processedAuditJobItems = new List<AuditJobItem>();
                        _processedAuditJobItems = CHNLSVC.Inventory.GetProcessedJobItems(cmbSubJobSelection.SelectedItem.ToString(), _processedItemsStartIndex, _processedItemsEndIndex);
                        if (_processedAuditJobItems.Count > 0)
                        {
                            BindItemDetails(_processedAuditJobItems);

                            //Get processed audit serial details
                            _processedAuditJobSerials = new List<AuditJobSerial>();
                            _processedAuditJobSerials = CHNLSVC.Inventory.GetProcessedJobSerials(cmbSubJobSelection.SelectedItem.ToString(), _processedSerialStartIndex, _processedSerialEndIndex, 0); //0 - All, 1- Mismatch, 2- Match
                            BindSerialDetails(_processedAuditJobSerials);

                            //Get audit reasons
                            _auditReasons = new List<AuditReportStatus>();
                            _auditReasons = GetAuditReason();
                            BindAuditReasons();


                            radioBtnMatch.Checked = false;
                            radioBtnAll.Checked = false;
                            radioBtnMismatch.Checked = true;

                            SetAuditItemSummery(cmbSubJobSelection.SelectedItem.ToString());
                        }
                        else
                        {
                            MessageBox.Show("Selected job hasn't been processed", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            pnlSerialInfo.Visible = false;
                            dgvSubJobList.Focus();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a sub job number", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvSubJobList.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Sub job details not found. Please selecte correct job#", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtJobNo.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseAllChannels();
                this.Cursor = Cursors.Default;
                MessageBox.Show("An error occurred while searching processed job items." + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSubJobSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _processedItemsStartIndex = 1;
                _processedItemsEndIndex = _indexVariance;
                _processedSerialStartIndex = 1;
                _processedSerialEndIndex = _indexVariance;

                SetAuditItemSummery(cmbSubJobSelection.SelectedItem.ToString());

                _processedAuditJobItems = new List<AuditJobItem>();
                _processedAuditJobItems = CHNLSVC.Inventory.GetProcessedJobItems(cmbSubJobSelection.SelectedItem.ToString(), _processedItemsStartIndex, _processedItemsEndIndex);
                BindItemDetails(_processedAuditJobItems);

                _processedAuditJobSerials = new List<AuditJobSerial>();
                _processedAuditJobSerials = LoadSerialDetails(1, _indexVariance);
                if (_processedAuditJobSerials.Count > 0)
                {
                    if ((radioBtnAll.Checked) || (radioBtnMatch.Checked))
                    {
                        BindSerialDetails(_processedAuditJobSerials);

                        btnUpdateRemark.Visible = false;
                        this.colReason.Visible = false;
                        this.colReasonStatus.Visible = false;
                        this.colRemark.Visible = false;
                    }
                    else if (radioBtnMismatch.Checked)
                    {
                        BindSerialDetails(_processedAuditJobSerials);
                        if ((_auditReasons.Count > 0) && (_processedAuditJobSerials.Count > 0))
                        {
                            BindAuditReasons();
                            this.colReason.Visible = true;
                            this.colReasonStatus.Visible = true;
                        }

                        this.colRemark.Visible = true;
                        btnUpdateRemark.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseAllChannels();
                this.Cursor = Cursors.Default;
                MessageBox.Show("An error occurred while searching processed job items." + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioBtnAll_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnAll.Checked)
            {
                _processedAuditJobSerials = new List<AuditJobSerial>();
                _processedAuditJobSerials = LoadSerialDetails(1, _indexVariance);

                BindSerialDetails(_processedAuditJobSerials);

                btnUpdateRemark.Visible = false;
                this.colReason.Visible = false;
                this.colReasonStatus.Visible = false;
                this.colRemark.Visible = false;
            }
        }

        private void radioBtnMatch_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnMatch.Checked)
            {
                _processedAuditJobSerials = new List<AuditJobSerial>();
                _processedAuditJobSerials = LoadSerialDetails(1, _indexVariance);

                BindSerialDetails(_processedAuditJobSerials);

                btnUpdateRemark.Visible = false;
                this.colReason.Visible = false;
                this.colReasonStatus.Visible = false;
                this.colRemark.Visible = false;
            }
        }

        private void radioBtnMismatch_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnMismatch.Checked)
            {
                _processedAuditJobSerials = new List<AuditJobSerial>();
                _processedAuditJobSerials = LoadSerialDetails(1, _indexVariance);

                BindSerialDetails(_processedAuditJobSerials);
                if ((_auditReasons.Count > 0) && (_processedAuditJobSerials.Count > 0))
                {
                    BindAuditReasons();
                    this.colReason.Visible = true;
                    this.colReasonStatus.Visible = true;
                }

                this.colRemark.Visible = true;
                btnUpdateRemark.Visible = true;

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult _dialog = MessageBox.Show("Do you want to cancel the current job ?", "Stock Verification - Cancel Jobs", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dialog == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                if (string.IsNullOrEmpty(txtLocation.Text))
                {
                    MessageBox.Show("Please select the location", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLocation.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtJobNo.Text))
                {
                    MessageBox.Show("Please enter the job no", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtJobNo.Focus();
                    return;
                }

                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16068))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("You don't have permission to perform this action " + Environment.NewLine + "  Advice: Required permission code :16068", "Cancel Audit Job - Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                string _message = null;
                int _status = CHNLSVC.Inventory.UpdateAuditHeaderDetails(BaseCls.GlbUserComCode, txtLocation.Text.Trim(), txtJobNo.Text.Trim(), BaseCls.GlbUserID, Convert.ToInt32(AuditJobStatus.Canceled), BaseCls.GlbUserSessionID, out _message);

                if (_status > 0)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Job # - " + txtJobNo.Text.Trim() + " has been cancelled", "Stock Verification-Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearUiData();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_message))
                    {
                        throw new Exception(_message);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
                MessageBox.Show("Job # - " + txtJobNo.Text + " couldn't cancel due to an error" + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProcessedAudItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private List<AuditReportStatus> GetAuditReason()
        {
            List<AuditReportStatus> _reasons = new List<AuditReportStatus>();
            try
            {
                _reasons = CHNLSVC.Inventory.GetAllAuditReportStstus(BaseCls.GlbUserComCode, "%");
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
                MessageBox.Show("An error occurred while uploading audit reasons." + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _reasons;
        }

        private void btnItemPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                if ((_processedItemsStartIndex - _indexVariance) > 0)
                {
                    _processedItemsStartIndex -= _indexVariance;
                    _processedItemsEndIndex -= _indexVariance;

                    _processedAuditJobItems = new List<AuditJobItem>();
                    _processedAuditJobItems = CHNLSVC.Inventory.GetProcessedJobItems(cmbSubJobSelection.SelectedItem.ToString(), _processedItemsStartIndex, _processedItemsEndIndex);
                    if (_processedAuditJobItems.Count > 0)
                    {
                        BindItemDetails(_processedAuditJobItems);
                    }
                    else
                    {
                        _processedItemsStartIndex += _indexVariance;
                        _processedItemsEndIndex += _indexVariance;
                    }
                }
            }
            catch (Exception ex)
            {
                _processedItemsStartIndex += _indexVariance;
                _processedItemsEndIndex += _indexVariance;

                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
                MessageBox.Show("An error occurred while loading item details " + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindItemDetails(List<AuditJobItem> _itemList)
        {
            if (_itemList.Count > 0)
            {
                BindingSource _bindSource = new BindingSource();
                _bindSource.DataSource = _itemList.Select(x => new { x.Audji_itm, x.Audji_stus, x.Audji_db_qty, x.Audji_ledger_qty, x.Audji_physical_qty }).ToList();
                dgvProcessedAudItems.DataSource = _bindSource;
            }
            else { dgvProcessedAudItems.Rows.Clear(); }
        }

        private void BindSerialDetails(List<AuditJobSerial> _serialList)
        {
            if (_serialList.Count > 0)
            {
                BindingSource _bindSource = new BindingSource();
                _bindSource.DataSource = _serialList.Select(x => new { x.Audjs_ItemCode, x.Audjs_ItemStatus, x.Audjs_SerialNo, x.Audjs_PhysicallyAvailableSerial, x.Audjs_InDocNo, x.Audjs_SerialId }).ToList();
                dgvProcessedSerials.DataSource = _bindSource;
            }
            else { dgvProcessedSerials.Rows.Clear(); }
        }

        private void btnItemNext_Click(object sender, EventArgs e)
        {
            try
            {
                _processedItemsStartIndex += _indexVariance;
                _processedItemsEndIndex += _indexVariance;

                _processedAuditJobItems = new List<AuditJobItem>();
                _processedAuditJobItems = CHNLSVC.Inventory.GetProcessedJobItems(cmbSubJobSelection.SelectedItem.ToString(), _processedItemsStartIndex, _processedItemsEndIndex);
                if (_processedAuditJobItems.Count > 0)
                {
                    BindItemDetails(_processedAuditJobItems);
                }
                else
                {
                    _processedItemsStartIndex -= _indexVariance;
                    _processedItemsEndIndex -= _indexVariance;
                }
            }
            catch (Exception ex)
            {
                _processedItemsStartIndex -= _indexVariance;
                _processedItemsEndIndex -= _indexVariance;

                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
                MessageBox.Show("An error occurred while loading item details " + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSerialPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isSerialGridDataEdited)
                {
                    if (GetUserEnteredGridData().Count > 0)
                    {
                        this.Cursor = Cursors.Default;
                        DialogResult _dialogResult = MessageBox.Show("Entered remarks will lost. Do you want to proceed ?", "Stock Verification - Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (_dialogResult == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                        else { _isSerialGridDataEdited = true; }
                    }
                }

                if ((_processedSerialStartIndex - _indexVariance) > 0)
                {
                    _processedSerialStartIndex -= _indexVariance;
                    _processedSerialEndIndex -= _indexVariance;
                    _processedAuditJobSerials = new List<AuditJobSerial>();
                    _processedAuditJobSerials = LoadSerialDetails(_processedSerialStartIndex, _processedSerialEndIndex);

                    //Bind details

                    if (_processedAuditJobSerials.Count > 0)
                    {
                        if ((radioBtnAll.Checked) || (radioBtnMatch.Checked))
                        {
                            BindSerialDetails(_processedAuditJobSerials);

                            btnUpdateRemark.Visible = false;
                            this.colReason.Visible = false;
                            this.colReasonStatus.Visible = false;
                            this.colRemark.Visible = false;
                        }
                        else if (radioBtnMismatch.Checked)
                        {
                            BindSerialDetails(_processedAuditJobSerials);
                            if ((_auditReasons.Count > 0) && (_processedAuditJobSerials.Count > 0))
                            {
                                BindAuditReasons();
                                this.colReason.Visible = true;
                                this.colReasonStatus.Visible = true;
                            }

                            this.colRemark.Visible = true;
                            btnUpdateRemark.Visible = true;
                        }
                    }
                    else
                    {
                        _processedSerialStartIndex += _indexVariance;
                        _processedSerialEndIndex += _indexVariance;
                    }
                }
            }
            catch (Exception ex)
            {
                _processedSerialStartIndex += _indexVariance;
                _processedSerialEndIndex += _indexVariance;
                this.Cursor = Cursors.Default;
                MessageBox.Show("An error occurred while loading serial details " + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSerialNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isSerialGridDataEdited)
                {
                    if (GetUserEnteredGridData().Count > 0)
                    {
                        this.Cursor = Cursors.Default;
                        DialogResult _dialogResult = MessageBox.Show("Entered remarks will lost. Do you want to proceed ?", "Stock Verification - Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (_dialogResult == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                        else { _isSerialGridDataEdited = false; }
                    }
                }

                _processedSerialStartIndex += _indexVariance;
                _processedSerialEndIndex += _indexVariance;
                _processedAuditJobSerials = new List<AuditJobSerial>();
                _processedAuditJobSerials = LoadSerialDetails(_processedSerialStartIndex, _processedSerialEndIndex);

                //Bind details

                if (_processedAuditJobSerials.Count > 0)
                {
                    if ((radioBtnAll.Checked) || (radioBtnMatch.Checked))
                    {
                        BindSerialDetails(_processedAuditJobSerials);

                        btnUpdateRemark.Visible = false;
                        this.colReason.Visible = false;
                        this.colReasonStatus.Visible = false;
                        this.colRemark.Visible = false;
                    }
                    else if (radioBtnMismatch.Checked)
                    {
                        BindSerialDetails(_processedAuditJobSerials);
                        if ((_auditReasons.Count > 0) && (_processedAuditJobSerials.Count > 0))
                        {
                            BindAuditReasons();
                            this.colReason.Visible = true;
                            this.colReasonStatus.Visible = true;
                        }

                        this.colRemark.Visible = true;
                        btnUpdateRemark.Visible = true;
                    }
                }
                else
                {
                    _processedSerialStartIndex -= _indexVariance;
                    _processedSerialEndIndex -= _indexVariance;
                }
            }
            catch (Exception ex)
            {
                _processedSerialStartIndex -= _indexVariance;
                _processedSerialEndIndex -= _indexVariance;
                this.Cursor = Cursors.Default;
                MessageBox.Show("An error occurred while loading serial details " + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<AuditJobSerial> GetUserEnteredGridData()
        {
            List<AuditJobSerial> _userEnteredDataList = new List<AuditJobSerial>();

            try
            {
                if (dgvProcessedSerials.Rows.Count > 0)
                {
                    foreach (DataGridViewRow _dataRow in dgvProcessedSerials.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["colRemark"].Value != null || x.Cells["colReason"].Value != null).Select(x => x).ToList())
                    {
                        if ((_dataRow.Cells["colRemark"].Value != null) || (_dataRow.Cells["colReason"].Value != null))
                        {
                            AuditJobSerial _userEnteredData = new AuditJobSerial();
                            _userEnteredData.Audjs_JobNo = cmbSubJobSelection.SelectedItem.ToString();
                            _userEnteredData.Audjs_ItemCode = _dataRow.Cells["colSerialItemCode"].Value == null ? string.Empty : _dataRow.Cells["colSerialItemCode"].Value.ToString();
                            _userEnteredData.Audjs_SerialNo = _dataRow.Cells["colSystemSerialId"].Value == null ? string.Empty : _dataRow.Cells["colSystemSerialId"].Value.ToString();
                            _userEnteredData.Audjs_InDocNo = _dataRow.Cells["colDocNo"].Value == null ? string.Empty : _dataRow.Cells["colDocNo"].Value.ToString();
                            _userEnteredData.Audjs_RefStatus = _dataRow.Cells["colReasonStatus"].Value == null ? string.Empty : _dataRow.Cells["colReasonStatus"].Value.ToString();
                            _userEnteredData.Audjs_SerialId = _dataRow.Cells["colSerialId"].Value == null ? 0 : Convert.ToInt32(_dataRow.Cells["colSerialId"].Value);

                            if (!string.IsNullOrEmpty(_userEnteredData.Audjs_RefStatus))
                            {
                                _userEnteredData.Audjs_RptType = _auditReasons.Where(x => x.Aurs_main_cd == _userEnteredData.Audjs_RefStatus).Select(x => x.Aurs_code).First().ToString();
                            }

                            _userEnteredData.Audjs_Remark = _dataRow.Cells["colRemark"].Value == null ? string.Empty : _dataRow.Cells["colRemark"].Value.ToString();
                            _userEnteredData.Audjs_ModBy = BaseCls.GlbUserID;
                            _userEnteredData.Audjs_SessionId = BaseCls.GlbUserSessionID;
                            _userEnteredDataList.Add(_userEnteredData);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _userEnteredDataList;
        }

        private void SaveAuditNotes()
        {
            try
            {
                List<AuditJobSerial> _auditNotes = GetUserEnteredGridData();
                if (_auditNotes.Count > 0)
                {
                    string _returnMessage = null;
                    CHNLSVC.Inventory.SaveAuditNotes(_auditNotes, out _returnMessage);
                    if (string.IsNullOrEmpty(_returnMessage))
                    {
                        this.Cursor = Cursors.Default;
                        _isSerialGridDataEdited = false;
                        //MessageBox.Show("Audit notes saved successfully", "Stock Verification - Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        throw new Exception(_returnMessage);
                    }
                }
                else
                {
                    _isSerialGridDataEdited = false;
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("No changes to save", "Stock Verification - Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
                MessageBox.Show("An error occurred while saving audit notes" + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindPreviousAuditNotes()
        {
            try
            {
                if (dgvProcessedSerials.Rows.Count > 0)
                {
                    if (_processedAuditJobSerials.Count > 0)
                    {
                        List<AuditJobSerial> _tmpSerialList = _processedAuditJobSerials.Where(x => x.Audjs_RptType.ToString() != string.Empty || x.Audjs_Remark.ToString() != string.Empty).Select(x => x).ToList();
                        if (_tmpSerialList.Count > 0)
                        {
                            foreach (AuditJobSerial _serial in _tmpSerialList)
                            {
                                var queryList = dgvProcessedSerials.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["colSystemSerialId"].Value == _serial.Audjs_SerialNo).ToList();
                                queryList.ForEach(x => x.Cells["colReasonStatus"].Value = string.IsNullOrEmpty(_serial.Audjs_RefStatus) ? string.Empty : _serial.Audjs_RefStatus);
                                queryList.ForEach(x => x.Cells["colReason"].Value = string.IsNullOrEmpty(_serial.Audjs_RefStatus) ? string.Empty : _serial.Audjs_RefStatus);
                                queryList.ForEach(x => x.Cells["colRemark"].Value = string.IsNullOrEmpty(_serial.Audjs_Remark) ? string.Empty : _serial.Audjs_Remark);
                                dgvProcessedSerials.RefreshEdit();
                                //dgvProcessedSerials.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["colSystemSerialId"].Value == _serial.Audjs_SerialNo).ToList().ForEach(x => x.Cells["colReasonStatus"].Value = string.IsNullOrEmpty(_serial.Audjs_RefStatus) ? string.Empty : _serial.Audjs_RefStatus);
                                //dgvProcessedSerials.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["colSystemSerialId"].Value == _serial.Audjs_SerialNo).ToList().ForEach(x => x.Cells["colReason"].Value = string.IsNullOrEmpty(_serial.Audjs_RptType) ? string.Empty : _serial.Audjs_RptType);
                                //dgvProcessedSerials.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["colSystemSerialId"].Value == _serial.Audjs_SerialNo).ToList().ForEach(x => x.Cells["colRemark"].Value = string.IsNullOrEmpty(_serial.Audjs_Remark) ? string.Empty : _serial.Audjs_Remark);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void BindAuditReasons()
        {
            try
            {
                BindingSource _bind = new BindingSource();
                _bind.DataSource = _auditReasons.Select(x => new { x.Aurs_main_cd, x.Aurs_desc }).ToList();
                this.colReason.DataSource = _bind;
                this.colReason.DisplayMember = "Aurs_desc";
                this.colReason.ValueMember = "Aurs_main_cd";
                BindPreviousAuditNotes();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<AuditJobSerial> LoadSerialDetails(int _startIndex, int _endIndex)
        {
            List<AuditJobSerial> _auditSerialList = new List<AuditJobSerial>();
            try
            {
                _processedSerialStartIndex = _startIndex;
                _processedSerialEndIndex = _endIndex;

                _processedAuditJobSerials = new List<AuditJobSerial>();

                if (radioBtnAll.Checked)
                {
                    _auditSerialList = CHNLSVC.Inventory.GetProcessedJobSerials(cmbSubJobSelection.SelectedItem.ToString(), _processedSerialStartIndex, _processedSerialEndIndex, 0); //0 - All, 1- Mismatch, 2- Match
                }
                else if (radioBtnMatch.Checked)
                {
                    _auditSerialList = CHNLSVC.Inventory.GetProcessedJobSerials(cmbSubJobSelection.SelectedItem.ToString(), _processedSerialStartIndex, _processedSerialEndIndex, 2); //0 - All, 1- Mismatch, 2- Match
                }
                else if (radioBtnMismatch.Checked)
                {
                    _auditSerialList = CHNLSVC.Inventory.GetProcessedJobSerials(cmbSubJobSelection.SelectedItem.ToString(), _processedSerialStartIndex, _processedSerialEndIndex, 1); //0 - All, 1- Mismatch, 2- Match
                }

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
                MessageBox.Show("An error occurred while loading serial details " + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _auditSerialList;
        }

        private void dgvProcessedAudItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dgvProcessedSerials_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dgvProcessedSerials_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 1) || (e.ColumnIndex == 2))
            {
                if ((dgvProcessedSerials.IsCurrentCellDirty) || (dgvProcessedSerials.IsCurrentRowDirty))
                {
                    _isSerialGridDataEdited = true;
                    _currentSerialId = dgvProcessedSerials.Rows[e.RowIndex].Cells["colSerialId"].Value == null ? 0 : Convert.ToInt32(dgvProcessedSerials.Rows[e.RowIndex].Cells["colSerialId"].Value);
                }
                else if ((dgvProcessedSerials.Rows.Count > 0) && (dgvProcessedSerials.CurrentCell != null))
                {
                    if (dgvProcessedSerials.CurrentCell.IsInEditMode)
                    {
                        _isSerialGridDataEdited = true;
                        _currentSerialId = dgvProcessedSerials.Rows[e.RowIndex].Cells["colSerialId"].Value == null ? 0 : Convert.ToInt32(dgvProcessedSerials.Rows[e.RowIndex].Cells["colSerialId"].Value);
                    }
                }
                string value = "";

                if (dgvProcessedSerials.Rows.Count > 0)
                {
                    value = dgvProcessedSerials.Rows[e.RowIndex].Cells["colReasonStatus"].Value == null ? string.Empty : dgvProcessedSerials.Rows[e.RowIndex].Cells["colReasonStatus"].Value.ToString();
                    if (value.ToString() != "ISM")
                    {
                        dgvProcessedSerials.Rows[e.RowIndex].Cells["colRemark"].Value = "";
                    }
                }


            }




        }

        private void radioBtnAll_Click(object sender, EventArgs e)
        {
            if ((_isSerialGridDataEdited) && (radioBtnMismatch.Checked))
            {
                if (GetUserEnteredGridData().Count > 0)
                {
                    this.Cursor = Cursors.Default;
                    DialogResult _dialogResult = MessageBox.Show("Entered remarks will lost. Do you want to proceed ?", "Stock Verification - Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (_dialogResult == System.Windows.Forms.DialogResult.No)
                    {
                        btnUpdateRemark.Focus();
                        return;
                    }
                }
            }

            radioBtnAll.Focus();
            radioBtnAll.Checked = true;
            radioBtnMatch.Checked = false;
            radioBtnMismatch.Checked = false;
        }

        private void radioBtnMatch_Click(object sender, EventArgs e)
        {
            if ((_isSerialGridDataEdited) && (radioBtnMismatch.Checked))
            {
                if (GetUserEnteredGridData().Count > 0)
                {
                    this.Cursor = Cursors.Default;
                    DialogResult _dialogResult = MessageBox.Show("Entered remarks will lost. Do you want to proceed ?", "Stock Verification - Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (_dialogResult == System.Windows.Forms.DialogResult.No)
                    {
                        btnUpdateRemark.Focus();
                        return;
                    }
                }
            }

            radioBtnMatch.Focus();
            radioBtnAll.Checked = false;
            radioBtnMatch.Checked = true;
            radioBtnMismatch.Checked = false;
        }

        private void radioBtnMismatch_Click(object sender, EventArgs e)
        {
            radioBtnMismatch.Focus();
            radioBtnAll.Checked = false;
            radioBtnMatch.Checked = false;
            radioBtnMismatch.Checked = true;
        }

        private void btnUpdateRemark_Click(object sender, EventArgs e)
        {
            DialogResult _dialog = MessageBox.Show("Do you want to save the changes you have made?", "Stock Verification - Update Remarks", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (_dialog == System.Windows.Forms.DialogResult.No)
            {
                _isSerialGridDataEdited = true;
                return;
            }
            else
            {
                if (dgvProcessedSerials.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dataRow in dgvProcessedSerials.Rows)
                    {
                        string _tmpString = dataRow.Cells["colReasonStatus"].Value == null ? string.Empty : dataRow.Cells["colReasonStatus"].Value.ToString();
                        if (_tmpString == "ISM")
                        {
                            string _tmpItem = dataRow.Cells["colSerialItemCode"].Value == null ? string.Empty : dataRow.Cells["colSerialItemCode"].Value.ToString();
                            string _tmpserial = dataRow.Cells["colRemark"].Value == null ? string.Empty : dataRow.Cells["colRemark"].Value == "" ? string.Empty : dataRow.Cells["colRemark"].Value.ToString();
                            if (string.IsNullOrEmpty(_tmpserial))
                            {
                                MessageBox.Show("Please enter mismatch serial", "Stock Verification - Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            DataTable _tmpTable = new DataTable();
                            _tmpTable = CHNLSVC.Inventory.CheckSerialBySerial(BaseCls.GlbUserComCode, txtLocation.Text, _tmpItem, _tmpserial);
                            if (_tmpTable.Rows.Count > 0)
                            {
                                MessageBox.Show("This serial already available in current location", "Stock Verification - Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                }

                if (_isSerialGridDataEdited)
                {
                    SaveAuditNotes();
                    // LoadAuditRemarkScreen(cmbSubJobSelection.SelectedItem.ToString(), _currentSerial);
                }
                //else { MessageBox.Show("No changes to save", "Stock Verification - Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                LoadAuditRemarkScreen(cmbSubJobSelection.SelectedItem.ToString(), _currentSerialId.ToString());
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {

            try
            {

                DialogResult _dialog = MessageBox.Show("Do you want to finish the current job ?", "Stock Verification - Finish Jobs", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dialog == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                if (string.IsNullOrEmpty(txtLocation.Text))
                {
                    MessageBox.Show("Please select the location", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLocation.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtJobNo.Text))
                {
                    MessageBox.Show("Please enter the job no", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtJobNo.Focus();
                    return;
                }

                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16069))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("You don't have permission to perform this action " + Environment.NewLine + "  Advice: Required permission code :16069", "Finalize Audit Job - Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //#region remark updeted item are procees by tharanga 2018/02/21
                List<string> _selectedSubJobs = new List<string>();
                if (dgvSubJobList.Rows.Count > 0)
                {
                    foreach (DataGridViewRow _dataRow in dgvSubJobList.Rows)
                    {
                        bool _isRowSelected = _dataRow.Cells["chkColSelectJob"].Value == DBNull.Value ? false : Convert.ToBoolean(_dataRow.Cells["chkColSelectJob"].Value);
                        if (_isRowSelected)
                        {
                            _selectedSubJobs.Add(_dataRow.Cells["txtColSubJobNo"].Value.ToString());
                        }
                    }
                }
                foreach (var item in _selectedSubJobs)
                {
                    _AuditJobSerialcharges = new List<AuditJobSerial>();
                    _AuditJobSerialcharges = CHNLSVC.Inventory.GetProcessedJobSerials_all(item, _processedSerialStartIndex, _processedSerialEndIndex, 0,BaseCls.GlbUserComCode); //0 - All, 1- Mismatch, 2- Match
                    if (_AuditJobSerialcharges.Count > 0)
                    {
                        _AuditJobSerialcharges.RemoveAll(r => r.Audjs_RefStatus == "" || r.Audjs_RefStatus == null);
                        Int32 count = _AuditJobSerialcharges.Where(r => r.audjs_charges_processed == 1).Count();
                        if (_AuditJobSerialcharges.Count != count)
                        {
                            MessageBox.Show("Please process item charges all updated seriales", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    
                }
                //foreach (var item in _selectedSubJobs)
                //{
                //    PhysicalStockVerificationHdr _PhysicalStockVerificationHdr = new PhysicalStockVerificationHdr();
                //    _PhysicalStockVerificationHdr = CHNLSVC.Inventory.GET_STOCKVERF_HDR(item);
                //    if (_PhysicalStockVerificationHdr.AUSH_CHARGES_APP == 0)
                //    {
                //        MessageBox.Show("Please approve item charges ", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return;
                //    }


                //}


                //btnreqadj_Click(null, null);

                //#endregion


                DataTable _jobs = new DataTable();
                _jobs = CHNLSVC.Inventory.GetAuditJobDetails(BaseCls.GlbUserComCode, txtLocation.Text.Trim(), txtJobNo.Text.Trim());
                if (_jobs.Rows.Count > 0)
                {
                    bool _hasPendingJobs = (_jobDetails.AsEnumerable().Where(x => x.Field<string>("Sub Job Status").ToString() == "PENDING").Select(x => x).ToList().Count > 0) ? true : false;
                    if (_hasPendingJobs)
                    {
                        MessageBox.Show("Couldn't finalize job - " + txtJobNo.Text + Environment.NewLine + "All Job need be processed", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnProcess.Focus();
                        return;
                    }
                    else
                    {
                        string _jobStatus = _jobDetails.AsEnumerable().Select(x => x.Field<string>("Status")).Distinct().First().ToString();
                        if (_jobStatus == "PENDING")
                        {
                            //Finalize the job
                            string _message = null;
                            int _status = CHNLSVC.Inventory.UpdateAuditHeaderDetails(BaseCls.GlbUserComCode, txtLocation.Text.Trim(), txtJobNo.Text.Trim(), BaseCls.GlbUserID, Convert.ToInt32(AuditJobStatus.Finished), BaseCls.GlbUserSessionID, out _message);
                            if (_status > 0)
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("Job # - " + txtJobNo.Text.Trim() + " has been finalized", "Stock Verification-Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                GenarateStockSignatureReport();
                                ClearUiData();
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(_message))
                                {
                                    throw new Exception(_message);
                                }
                            }
                        }
                        else if (_jobStatus == "CANCELED")
                        {
                            MessageBox.Show("This job has been canceled", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            btnProcess.Focus();
                            return;
                        }
                        else if (_jobStatus == "FINISHED")
                        {
                            MessageBox.Show("This job already has been finalized", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            btnProcess.Focus();
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Couldn't find job details - Invalid job no", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtJobNo.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
                MessageBox.Show("Job # - " + txtJobNo.Text + " couldn't finalize due to an error" + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void appBackGroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            userWaitControl.Visible = false;
            this.pnlMainSearchControl.Enabled = true;
            this.grbAuditDetailPnl.Enabled = true;

            List<string> _resultS = new List<string>();
            _resultS = e.Result.ToString().Split(',').ToList();

            if (_resultS.Count > 0)
            {
                if (_resultS[0] == "0") { MessageBox.Show(_resultS[1], "Stock Verification - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else if (_resultS[0] == "1") { MessageBox.Show(_resultS[1], "Stock Verification - Information", MessageBoxButtons.OK, MessageBoxIcon.Information); GenerateCurrentInvBalceReport(); LoadJobDetails(); }
                else if (_resultS[0] == "2") { MessageBox.Show(_resultS[1], "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            this.Cursor = Cursors.Default;
        }

        private void appBackGroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string _returnMessage = string.Empty;
            try
            {

                List<string> _selectedSubJobs = new List<string>();
                if (dgvSubJobList.Rows.Count > 0)
                {
                    foreach (DataGridViewRow _dataRow in dgvSubJobList.Rows)
                    {
                        bool _isRowSelected = _dataRow.Cells["chkColSelectJob"].Value == DBNull.Value ? false : Convert.ToBoolean(_dataRow.Cells["chkColSelectJob"].Value);
                        if (_isRowSelected)
                        {
                            if (_dataRow.Cells["txtColSubJobStatus"].Value.ToString() == "CANCELED")
                            {
                                _returnMessage = "0,Couldn't process Job details" + Environment.NewLine + "Job# - " + _dataRow.Cells["txtColSubJobNo"].Value.ToString() + " has been canceled";
                                goto ReturnStage;
                            }
                            else if (_dataRow.Cells["txtColSubJobStatus"].Value.ToString() == "FINISHED")
                            {
                                _returnMessage = "0,Couldn't process Job details" + Environment.NewLine + "Job# - " + _dataRow.Cells["txtColSubJobNo"].Value.ToString() + " has been finalized";
                                goto ReturnStage;
                            }
                            else if (_dataRow.Cells["txtColSubJobStatus"].Value.ToString() == "START")
                            {
                                _returnMessage = "0,Couldn't process Job details" + Environment.NewLine + "Job# - " + _dataRow.Cells["txtColSubJobNo"].Value.ToString() + " has been processed";
                                goto ReturnStage;
                            }
                            else
                            {
                                _selectedSubJobs.Add(_dataRow.Cells["txtColSubJobNo"].Value.ToString());
                            }

                        }
                    }

                    if (_selectedSubJobs.Count > 0)
                    {
                        List<PhsicalStockVerificationMain> _mainList = new List<PhsicalStockVerificationMain>();
                        foreach (string _subJobNo in _selectedSubJobs)
                        {
                            //Process selected sub jobs
                            PhsicalStockVerificationMain _main = new PhsicalStockVerificationMain();
                            _main.Ausm_com = BaseCls.GlbUserComCode;
                            _main.Ausm_cre_by = BaseCls.GlbUserID;
                            _main.Ausm_cre_dt = DateTime.Today;
                            _main.Ausm_dt = DateTime.Today;
                            _main.Ausm_job = _subJobNo;
                            _main.Ausm_loc = txtLocation.Text.Trim();
                            _main.Ausm_main_job = txtJobNo.Text.Trim();

                            if (_jobDetails.Rows.Count > 0)
                            {
                                _main.Ausm_seq = Convert.ToInt32(_jobDetails.AsEnumerable().Where(x => x.Field<string>("Sub Job No") == _subJobNo).Select(x => x.Field<string>("Seq #")).First());
                            }

                            _main.Ausm_stus = true;
                            _main.Ausm_Subjob_Enddt = null;
                            _main.Ausm_Subjob_Status = "S";
                            _main.Ausm_Subjob_Strdt = DateTime.Today;
                            _main.Ausm_Mod_By = BaseCls.GlbUserID;
                            _main.Ausm_Session_Id = BaseCls.GlbUserSessionID;
                            _mainList.Add(_main);
                        }

                        int _result = CHNLSVC.Inventory.ProcessStockCountJob(BaseCls.GlbUserComCode, txtLocation.Text.Trim(), BaseCls.GlbUserID, _mainList, out _returnMessage);


                        if (_result >= 1)
                        {
                            _returnMessage = "1,Job details processed successfully";
                        }
                        else
                        {
                            _returnMessage = "0,Couldn't processed Job details" + Environment.NewLine + _returnMessage;
                        }
                    }
                    else
                    {
                        _returnMessage = "0,Please select a sub job to proceed";
                    }
                }
                else
                {
                    _returnMessage = "0,Sub job details not found. Please selecte correct job#";
                }

            }
            catch (Exception ex)
            {
                _returnMessage = "2,An error occurred while processing job details." + Environment.NewLine + ex.Message;
            }

        ReturnStage:
            e.Result = _returnMessage;
        }

        private void DisableUserControls()
        {
            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;
            txtSubJobTotal.Enabled = false;
            txtRemarks.Enabled = false;
            txtSupervisor.ReadOnly = true;
            dgvMemberList.ReadOnly = true;
            btnSearchSupervisor.Enabled = false;
            btnSearchEmp.Enabled = false;

            if (lblJobStatus.Text == "FINISHED")
            {
                dgvProcessedAudItems.ReadOnly = true;
                dgvProcessedSerials.ReadOnly = true;
                gvRmk.ReadOnly = true;
                btnUpdateRemark.Enabled = false;
                btnAddRmk.Enabled = false;
                btnUploadSerials.Enabled = false;
                btn_Exreq.Visible = false;
                txtRmk1.ReadOnly = true;
                txtRmk2.ReadOnly = true;
                txtRmk3.ReadOnly = true;
                txtRmk4.ReadOnly = true;
                txtRmk5.ReadOnly = true;
                txtRmk6.ReadOnly = true;
                txtRmk7.ReadOnly = true;
                txtRmk8.ReadOnly = true;
                txtRmk9.ReadOnly = true;
                txtRmk10.ReadOnly = true;
                cmbRmk11.Enabled = false;
            }
            else
            {
                dgvProcessedAudItems.ReadOnly = false;
                dgvProcessedSerials.ReadOnly = false;
                gvRmk.ReadOnly = false;
                btnUpdateRemark.Enabled = true;
                btnUploadSerials.Enabled = true;
                btn_Exreq.Visible = true;
                btnAddRmk.Enabled = true;
                txtRmk1.ReadOnly = false;
                txtRmk2.ReadOnly = false;
                txtRmk3.ReadOnly = false;
                txtRmk4.ReadOnly = false;
                txtRmk5.ReadOnly = false;
                txtRmk6.ReadOnly = false;
                txtRmk7.ReadOnly = false;
                txtRmk8.ReadOnly = false;
                txtRmk9.ReadOnly = false;
                txtRmk10.ReadOnly = false;
                cmbRmk11.Enabled = true;

                if (!IsUserHasPermiossion)
                {
                    btnProcess.Enabled = false;
                    btnFinish.Enabled = false;
                    btnViewSerial.Enabled = false;
                }
            }
        }

        private void EnableUserControls()
        {
            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;
            txtSubJobTotal.Enabled = true;
            txtRemarks.Enabled = true;
            dgvMemberList.Enabled = true;
            dgvMemberList.ReadOnly = false;
            btnSearchSupervisor.Enabled = true;
            btnSearchEmp.Enabled = true;
            txtSupervisor.ReadOnly = false;

            dgvProcessedAudItems.ReadOnly = false;
            dgvProcessedSerials.ReadOnly = false;
            gvRmk.ReadOnly = false;
            btnUpdateRemark.Enabled = true;
            btnUploadSerials.Enabled = true;
            btn_Exreq.Visible = true;
            btnAddRmk.Enabled = true;
            txtRmk1.ReadOnly = false;
            txtRmk2.ReadOnly = false;
            txtRmk3.ReadOnly = false;
            txtRmk4.ReadOnly = false;
            txtRmk5.ReadOnly = false;
            txtRmk6.ReadOnly = false;
            txtRmk7.ReadOnly = false;
            txtRmk8.ReadOnly = false;
            txtRmk9.ReadOnly = false;
            txtRmk10.ReadOnly = false;
            cmbRmk11.Enabled = true;

            btnProcess.Enabled = true;
            btnFinish.Enabled = true;
            btnViewSerial.Enabled = true;
        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
            txtJobNo.Text = string.Empty;
        }

        private void chkSelectAllSubJobs_CheckedChanged(object sender, EventArgs e)
        {

            if (dgvSubJobList.Rows.Count > 0)
            {
                foreach (DataGridViewRow subJob in dgvSubJobList.Rows)
                {
                    dgvSubJobList.Rows[subJob.Index].Cells["chkColSelectJob"].Value = chkSelectAllSubJobs.Checked;
                    dgvSubJobList_CellContentClick(null, new DataGridViewCellEventArgs(0, subJob.Index));
                }
            }
        }

        private void cmbItemPageCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            _indexVariance = Convert.ToInt32(cmbItemPageCount.Text);
            _processedItemsEndIndex = _indexVariance;
            _processedAuditJobItems = new List<AuditJobItem>();
            _processedAuditJobItems = CHNLSVC.Inventory.GetProcessedJobItems(cmbSubJobSelection.SelectedItem.ToString(), _processedItemsStartIndex, _processedItemsEndIndex);
            if (_processedAuditJobItems.Count > 0)
            {
                BindItemDetails(_processedAuditJobItems);
            }
        }

        private void cmbSerialPageCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkViewComment.Checked = false;
            _indexVariance = Convert.ToInt32(cmbSerialPageCount.SelectedItem == null ? "30" : cmbSerialPageCount.SelectedItem.ToString());
            _processedSerialEndIndex = _indexVariance;
            _processedAuditJobSerials = new List<AuditJobSerial>();
            _processedAuditJobSerials = LoadSerialDetails(1, _processedSerialEndIndex);
            BindSerialDetails(_processedAuditJobSerials);

            if (radioBtnMismatch.Checked)
            {
                if ((_auditReasons.Count > 0) && (_processedAuditJobSerials.Count > 0))
                {
                    BindAuditReasons();
                    this.colReason.Visible = true;
                    this.colReasonStatus.Visible = true;
                }
                this.colRemark.Visible = true;
                btnUpdateRemark.Visible = true;
            }
        }

        private void cmbItemPageCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _indexVariance = Convert.ToInt32(cmbItemPageCount.Text);
                _processedItemsEndIndex = _indexVariance;
                _processedAuditJobItems = new List<AuditJobItem>();
                _processedAuditJobItems = CHNLSVC.Inventory.GetProcessedJobItems(cmbSubJobSelection.SelectedItem.ToString(), _processedItemsStartIndex, _processedItemsEndIndex);
                if (_processedAuditJobItems.Count > 0)
                {
                    BindItemDetails(_processedAuditJobItems);
                }
            }
        }

        private void cmbSerialPageCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    chkViewComment.Checked = false;
                    _indexVariance = Convert.ToInt32(cmbSerialPageCount.Text);
                    _processedSerialEndIndex = _indexVariance;
                    _processedAuditJobSerials = new List<AuditJobSerial>();
                    _processedAuditJobSerials = LoadSerialDetails(1, _processedSerialEndIndex);
                    BindSerialDetails(_processedAuditJobSerials);

                    if (radioBtnMismatch.Checked)
                    {
                        if ((_auditReasons.Count > 0) && (_processedAuditJobSerials.Count > 0))
                        {
                            BindAuditReasons();
                            this.colReason.Visible = true;
                            this.colReasonStatus.Visible = true;
                        }
                        this.colRemark.Visible = true;
                        btnUpdateRemark.Visible = true;
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        private bool IsFileOpen(FileInfo _file)
        {
            FileStream stream = null;
            bool _isfileOpen = false;

            try
            {
                stream = _file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                _isfileOpen = true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return _isfileOpen;
        }

        //private void txtSupervisor_Leave(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(txtSupervisor.Text))
        //        {
        //            DataTable _result = CHNLSVC.CommonSearch.SearchAllEmployee(BaseCls.GlbUserComCode +"|", "EPF NO", txtSupervisor.Text.Trim());
        //            if (_result.Rows.Count > 0)
        //            {
        //                lblSupervisorName.Text = _result.Rows[0]["FIRST NAME"].ToString();
        //            }
        //            else
        //            {
        //                lblSupervisorName.Text = string.Empty;
        //                MessageBox.Show("Team lead information not found. Please enter the correct EPF No", "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                return;
        //            }
        //        }               
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Cursor = Cursors.Default;
        //        CHNLSVC.CloseAllChannels();
        //        MessageBox.Show("An error occurred while loading auditor's details." + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void dgvProcessedAudItems_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (_processedAuditJobItems.Count > 0)
                {
                    BindingSource _source = new BindingSource();
                    if (_itemListSortDirecction == ListSortDirection.Ascending)
                    {
                        _source.DataSource = _processedAuditJobItems.Select(x => new { x.Audji_itm, x.Audji_stus, x.Audji_db_qty, x.Audji_ledger_qty, x.Audji_physical_qty }).ToList().OrderByDescending(x => x.Audji_itm).ToList();
                        _itemListSortDirecction = ListSortDirection.Descending;
                    }
                    else if (_itemListSortDirecction == ListSortDirection.Descending)
                    {
                        _source.DataSource = _processedAuditJobItems.Select(x => new { x.Audji_itm, x.Audji_stus, x.Audji_db_qty, x.Audji_ledger_qty, x.Audji_physical_qty }).ToList().OrderBy(x => x.Audji_itm).ToList();
                        _itemListSortDirecction = ListSortDirection.Ascending;
                    }

                    dgvProcessedAudItems.DataSource = _source;
                }
            }
        }

        private void dgvProcessedAudItems_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn column in dgvProcessedAudItems.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        private void dgvProcessedSerials_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn column in dgvProcessedSerials.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
                if (chkViewComment.Checked)
                {
                    chkViewComment.Checked = false;
                }
            }
        }

        private void dgvProcessedSerials_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.ColumnIndex == 4) && (_processedAuditJobSerials.Count > 0))
            {
                //Sort by item status
                BindingSource _source = new BindingSource();
                if (_serailListSortDirection == ListSortDirection.Ascending)
                {
                    _source.DataSource = _processedAuditJobSerials.Select(x => new { x.Audjs_ItemCode, x.Audjs_ItemStatus, x.Audjs_SerialNo, x.Audjs_PhysicallyAvailableSerial, x.Audjs_InDocNo, x.Audjs_SerialId }).ToList().OrderByDescending(x => x.Audjs_ItemStatus).ToList();
                    _serailListSortDirection = ListSortDirection.Descending;
                }
                else if (_serailListSortDirection == ListSortDirection.Descending)
                {
                    _source.DataSource = _processedAuditJobSerials.Select(x => new { x.Audjs_ItemCode, x.Audjs_ItemStatus, x.Audjs_SerialNo, x.Audjs_PhysicallyAvailableSerial, x.Audjs_InDocNo, x.Audjs_SerialId }).ToList().OrderBy(x => x.Audjs_ItemStatus).ToList();
                    _serailListSortDirection = ListSortDirection.Ascending;
                }
                dgvProcessedSerials.DataSource = _source;
            }
            else if ((e.ColumnIndex == 3) && (_processedAuditJobSerials.Count > 0))
            {
                //sory by item code
                BindingSource _source = new BindingSource();
                if (_serailListSortDirection == ListSortDirection.Ascending)
                {
                    _source.DataSource = _processedAuditJobSerials.Select(x => new { x.Audjs_ItemCode, x.Audjs_ItemStatus, x.Audjs_SerialNo, x.Audjs_PhysicallyAvailableSerial, x.Audjs_InDocNo, x.Audjs_SerialId }).ToList().OrderByDescending(x => x.Audjs_ItemCode).ToList();
                    _serailListSortDirection = ListSortDirection.Descending;
                }
                else if (_serailListSortDirection == ListSortDirection.Descending)
                {
                    _source.DataSource = _processedAuditJobSerials.Select(x => new { x.Audjs_ItemCode, x.Audjs_ItemStatus, x.Audjs_SerialNo, x.Audjs_PhysicallyAvailableSerial, x.Audjs_InDocNo, x.Audjs_SerialId }).ToList().OrderBy(x => x.Audjs_ItemCode).ToList();
                    _serailListSortDirection = ListSortDirection.Ascending;
                }
                dgvProcessedSerials.DataSource = _source;
            }
            else if ((e.ColumnIndex == 5) && (_processedAuditJobSerials.Count > 0))
            {
                //sory by system serial
                BindingSource _source = new BindingSource();
                if (_serailListSortDirection == ListSortDirection.Ascending)
                {
                    _source.DataSource = _processedAuditJobSerials.Select(x => new { x.Audjs_ItemCode, x.Audjs_ItemStatus, x.Audjs_SerialNo, x.Audjs_PhysicallyAvailableSerial, x.Audjs_InDocNo, x.Audjs_SerialId }).ToList().OrderByDescending(x => x.Audjs_PhysicallyAvailableSerial).ToList();
                    _serailListSortDirection = ListSortDirection.Descending;
                }
                else if (_serailListSortDirection == ListSortDirection.Descending)
                {
                    _source.DataSource = _processedAuditJobSerials.Select(x => new { x.Audjs_ItemCode, x.Audjs_ItemStatus, x.Audjs_SerialNo, x.Audjs_PhysicallyAvailableSerial, x.Audjs_InDocNo, x.Audjs_SerialId }).ToList().OrderBy(x => x.Audjs_PhysicallyAvailableSerial).ToList();
                    _serailListSortDirection = ListSortDirection.Ascending;
                }
                dgvProcessedSerials.DataSource = _source;
            }

            if (radioBtnMismatch.Checked)
            {
                if ((_auditReasons.Count > 0) && (_processedAuditJobSerials.Count > 0))
                {
                    BindAuditReasons();
                    this.colReason.Visible = true;
                    this.colReasonStatus.Visible = true;
                }
                this.colRemark.Visible = true;
                btnUpdateRemark.Visible = true;
            }

            if (chkViewNonSerials.Checked)
            {
                //hide un-commented rows
                if (dgvProcessedSerials.Rows.Count > 0)
                {
                    dgvProcessedSerials.CurrentCell = null;
                    foreach (DataGridViewRow _row in dgvProcessedSerials.Rows)
                    {
                        string _tmpSerial = _row.Cells["colSystemSerialId"].Value == null ? "N/A" : _row.Cells["colSystemSerialId"].Value.ToString().Trim().ToUpper();
                        if (_tmpSerial != "N/A")
                        {
                            dgvProcessedSerials.Rows[_row.Index].Visible = false;
                        }
                    }
                }
            }
        }

        private void SetAuditItemSummery(string _subJobNo)
        {
            lblTotalDbQty.Text = "0.00";
            lblTotalLedgerQty.Text = "0.00";
            lblTotalPhysicalQty.Text = "0.00";

            try
            {
                if (!string.IsNullOrEmpty(_subJobNo))
                {
                    DataTable _summery = new DataTable();
                    _summery = CHNLSVC.Inventory.GetAuditItemSummery(_subJobNo);
                    if (_summery.Rows.Count > 0)
                    {
                        foreach (DataRow _row in _summery.Rows)
                        {
                            lblTotalDbQty.Text = _row["Total_DbQty"] == DBNull.Value ? "0.00" : _row["Total_DbQty"].ToString();
                            lblTotalLedgerQty.Text = _row["Total_LedgerQty"] == DBNull.Value ? "0.00" : _row["Total_LedgerQty"].ToString();
                            lblTotalPhysicalQty.Text = _row["Total_PhysicalQty"] == DBNull.Value ? "0.00" : _row["Total_PhysicalQty"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseAllChannels();
                this.Cursor = Cursors.Default;
                MessageBox.Show("An error occurred while loading audit item summery." + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetSearchSerialDetails(string _subJobNo, string _serialNo)
        {
            List<AuditJobSerial> _searchResult = new List<AuditJobSerial>();

            try
            {
                //Search by system serial
                _searchResult = CHNLSVC.Inventory.GetAuditSerialDetails(_subJobNo, 0, _serialNo);
                if (_searchResult.Count < 1)
                {
                    //Search by physical serial
                    _searchResult = CHNLSVC.Inventory.GetAuditSerialDetails(_subJobNo, 1, _serialNo);
                    if (_searchResult.Count > 0)
                    {
                        BindSearchSerialDetails(_searchResult);
                    }
                    else
                    {
                        MessageBox.Show("Couldn't find serial details", "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else { BindSearchSerialDetails(_searchResult); }
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseAllChannels();
                this.Cursor = Cursors.Default;
                MessageBox.Show("An error occurred while search serial details" + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtSearchSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtSearchSerial.Text))
                {
                    GetSearchSerialDetails(cmbSubJobSelection.SelectedItem.ToString(), txtSearchSerial.Text.Trim());
                }
            }
        }

        private void btnSearchSerial_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearchSerial.Text))
            {
                MessageBox.Show("Please enter the serail number", "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                GetSearchSerialDetails(cmbSubJobSelection.SelectedItem.ToString(), txtSearchSerial.Text.Trim());
            }
        }

        private void BindSearchSerialDetails(List<AuditJobSerial> _serialList)
        {
            if (_serialList.Count > 0)
            {
                BindingSource _bindSource = new BindingSource();
                _bindSource.DataSource = _serialList.Select(x => new { x.Audjs_ItemCode, x.Audjs_ItemStatus, x.Audjs_SerialNo, x.Audjs_PhysicallyAvailableSerial, x.Audjs_InDocNo }).ToList();
                dgvSearchSerial.DataSource = _bindSource;
            }
            else { dgvSearchSerial.Rows.Clear(); }
        }

        //Datagrid Reasom combobox drop down custome event 
        private void cmbReasonValueChanged(object sender, EventArgs e)
        {
            try
            {
                var currentcell = dgvProcessedSerials.CurrentCellAddress;
                var sendingCB = sender as DataGridViewComboBoxEditingControl;
                //DataGridViewComboBoxCell cel = (DataGridViewComboBoxCell)dgvProcessedSerials.Rows[currentcell.Y].Cells[currentcell.X];
                //cel.Value = sendingCB.EditingControlFormattedValue.ToString();

                if (sendingCB.Focused)
                {
                    string _selectedValue = sendingCB.SelectedValue == null ? string.Empty : sendingCB.SelectedValue.ToString();

                    if (!string.IsNullOrEmpty(_selectedValue))
                    {
                        string _reasonStatus = _auditReasons.Where(x => x.Aurs_main_cd == _selectedValue).Select(x => x.Aurs_main_cd).First().ToString();
                        if (dgvProcessedSerials.Rows.Count > 0)
                        {
                            if ((_reasonStatus == "CUST") || (_reasonStatus == "SPE") || (_reasonStatus == "FLUE") || (_reasonStatus == "EXME") || (_reasonStatus == "AEX") || (_reasonStatus == "CUSTR") || (_reasonStatus == "ITE"))
                            {
                                string _currentSerialDoc = dgvProcessedSerials.CurrentRow.Cells["colDocNo"].Value == null ? string.Empty : dgvProcessedSerials.CurrentRow.Cells["colDocNo"].Value.ToString();
                                if (!string.IsNullOrEmpty(_currentSerialDoc))
                                {
                                    dgvProcessedSerials.Rows[currentcell.Y].Cells["colReasonStatus"].Value = string.Empty;
                                    sendingCB.SelectedIndex = -1;
                                    MessageBox.Show("Invalid audit note", "Stock Verification - Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            //else if (_reasonStatus == "ISM")
                            //{
                            //    string _tmpString = dgvProcessedSerials.CurrentRow.Cells["colRemark"].Value == null ? string.Empty : dgvProcessedSerials.CurrentRow.Cells["colRemark"].Value.ToString();
                            //    if (string.IsNullOrEmpty(_tmpString))
                            //    {
                            //        MessageBox.Show("Please enter mismatch serial", "Stock Verification - Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //        return;
                            //    }
                            //}

                            dgvProcessedSerials.Rows[currentcell.Y].Cells["colReasonStatus"].Value = _reasonStatus;
                            _isSerialGridDataEdited = true;



                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dgvProcessedSerials_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvProcessedSerials.CurrentCell.ColumnIndex == 0 && e.Control is ComboBox)
            {
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged += cmbReasonValueChanged;
                _currentSerialId = dgvProcessedSerials.CurrentRow.Cells["colSerialId"].Value == null ? 0 : Convert.ToInt32(dgvProcessedSerials.CurrentRow.Cells["colSerialId"].Value);

            }

        }

        private void BtnAuditRemarks_Click(object sender, EventArgs e)
        {
            lblSuccessMsg.Visible = false;
            pbCorrect.Visible = false;
            pbIncorrect.Visible = false;

            try
            {

                List<string> _selectedSubJobs = new List<string>();
                if (dgvSubJobList.Rows.Count > 0)
                {
                    foreach (DataGridViewRow _dataRow in dgvSubJobList.Rows)
                    {
                        bool _isRowSelected = _dataRow.Cells["chkColSelectJob"].Value == DBNull.Value ? false : Convert.ToBoolean(_dataRow.Cells["chkColSelectJob"].Value);
                        if (_isRowSelected)
                        {
                            _selectedSubJobs.Add(_dataRow.Cells["txtColSubJobNo"].Value.ToString());
                        }
                    }

                    if (_selectedSubJobs.Count > 0)
                    {
                        //Bind selected job numbers to drop down
                        BindingSource _cmbDataBind = new BindingSource();
                        _cmbDataBind.DataSource = _selectedSubJobs;
                        cmbAudSubJobNo.DataSource = _cmbDataBind;
                        cmbAudSubJobNo.SelectedIndex = 0;

                        _stusList = new List<AuditReportStatus>();
                        _stusList = GetAuditReason();

                        LoadRemarkedSerials(); //load serail details which have been entered the remarks previously                      
                    }
                    else
                    {
                        MessageBox.Show("Please select a sub job number", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvSubJobList.Focus();
                        return;
                    }

                    //_stusList = new List<AuditReportStatus>(); 
                    //_stusList = GetAuditReason();
                    if (_stusList.Count > 0)
                    {
                        BindingSource _bind = new BindingSource();
                        _bind.DataSource = _stusList.Select(x => new { x.Aurs_code, x.Aurs_desc }).ToList();
                        cmbRmk.DataSource = _bind;
                        cmbRmk.DisplayMember = "Aurs_desc";
                        cmbRmk.ValueMember = "Aurs_code";
                        cmbRmk.SelectedIndex = 0;
                    }

                    gvRmk_CellContentClick(null, new DataGridViewCellEventArgs(0, 0));

                    grbSubJobSelection.Visible = false;
                    pnlAuditRmk.Size = new Size(820, 550);
                    pnlAuditRmk.Visible = true;
                    //pnlAuditRmk.Width = 740;
                }
                else
                {
                    MessageBox.Show("Sub job details not found", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseAllChannels();
                this.Cursor = Cursors.Default;
                MessageBox.Show("An error occurred while loading audit remarks" + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbRmk_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbRmk.SelectedValue == null)
                    return;
                List<AuditRemark> _rmkList = CHNLSVC.Inventory.GeatAuditRemarks(BaseCls.GlbUserComCode, cmbRmk.SelectedValue.ToString());

                rmkPanel1.Visible = false;
                rmkPanel2.Visible = false;
                rmkPanel3.Visible = false;
                rmkPanel4.Visible = false;
                rmkPanel5.Visible = false;
                rmkPanel6.Visible = false;
                rmkPanel7.Visible = false;
                rmkPanel8.Visible = false;
                rmkPanel9.Visible = false;
                rmkPanel10.Visible = false;
                rmkPanel11.Visible = false;

                if (_rmkList != null && _rmkList.Count > 0)
                {
                    foreach (AuditRemark _rmk in _rmkList)
                    {
                        if (_rmk.Ausr_line == 1)
                        {
                            rmkPanel1.Visible = true;
                            lblRmk1.Text = _rmk.Ausr_rmk;
                        }

                        if (_rmk.Ausr_line == 2)
                        {
                            rmkPanel2.Visible = true;
                            lblRmk2.Text = _rmk.Ausr_rmk;
                        }

                        if (_rmk.Ausr_line == 3)
                        {
                            rmkPanel3.Visible = true;
                            lblRmk3.Text = _rmk.Ausr_rmk;
                        }

                        if (_rmk.Ausr_line == 4)
                        {
                            rmkPanel4.Visible = true;
                            lblRmk4.Text = _rmk.Ausr_rmk;
                        }

                        if (_rmk.Ausr_line == 5)
                        {
                            rmkPanel5.Visible = true;
                            lblRmk5.Text = _rmk.Ausr_rmk;
                        }

                        if (_rmk.Ausr_line == 6)
                        {
                            rmkPanel6.Visible = true;
                            lblRmk6.Text = _rmk.Ausr_rmk;
                        }

                        if (_rmk.Ausr_line == 7)
                        {
                            rmkPanel7.Visible = true;
                            lblRmk7.Text = _rmk.Ausr_rmk;
                        }

                        if (_rmk.Ausr_line == 8)
                        {
                            rmkPanel8.Visible = true;
                            lblRmk8.Text = _rmk.Ausr_rmk;
                        }

                        if (_rmk.Ausr_line == 9)
                        {
                            rmkPanel9.Visible = true;
                            lblRmk9.Text = _rmk.Ausr_rmk;
                        }

                        if (_rmk.Ausr_line == 10)
                        {
                            rmkPanel10.Visible = true;
                            lblRmk10.Text = _rmk.Ausr_rmk;
                        }

                        if (_rmk.Ausr_line == 11)
                        {
                            rmkPanel11.Visible = true;
                            lblRmk11.Text = _rmk.Ausr_rmk;
                        }
                    }

                    txtRmk1.Text = "";
                    txtRmk2.Text = "";
                    txtRmk3.Text = "";
                    txtRmk4.Text = "";
                    txtRmk5.Text = "";
                    txtRmk6.Text = "";
                    txtRmk7.Text = "";
                    txtRmk8.Text = "";
                    txtRmk9.Text = "";
                    txtRmk10.Text = "";
                    cmbRmk11.Text = "";
                    cmbRmk11.DataSource = null;

                }
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseAllChannels();
                this.Cursor = Cursors.Default;
                MessageBox.Show("An error occurred while loading audit remarks" + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnAddRmk_Click(object sender, EventArgs e)
        {
            try
            {
                string _returnControl = string.Empty;
                if (!IsAllRemarksEntered(out _returnControl))
                {
                    MessageBox.Show("Remark field '" + _returnControl.Trim() + "' is mandatory !", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int _seqNo = 0;
                string _rmkStatus = string.Empty;

                if (_remarkedSerials.Count > 0)
                {
                    _seqNo = Convert.ToInt32(_remarkedSerials.Where(x => x.Audjs_SerialNo == lblSerial.Text.Trim() && x.Audjs_ItemCode == lblItem.Text.Trim() && x.Audjs_SerialId == Convert.ToInt32(lblSerId.Text) && x.Audjs_RptType == cmbRmk.SelectedValue.ToString()).Select(x => x.Audjs_Seq).First());
                    _rmkStatus = _remarkedSerials.Where(x => x.Audjs_SerialNo == lblSerial.Text.Trim() && x.Audjs_ItemCode == lblItem.Text.Trim() && x.Audjs_SerialId == Convert.ToInt32(lblSerId.Text)).Select(x => x.Audjs_RefStatus).First().ToString();

                    //_seqNo = Convert.ToInt32(_remarkedSerials.Where(x => x.Audjs_SerialNo == lblSerial.Text.Trim() && x.Audjs_ItemCode == lblItem.Text.Trim() && x.Audjs_SerialId == Convert.ToInt32(lblSerId.Text)).Select(x => x.Audjs_Seq).First());
                    //_rmkStatus = _remarkedSerials.Where(x => x.Audjs_SerialNo == lblSerial.Text.Trim() && x.Audjs_ItemCode == lblItem.Text.Trim() && x.Audjs_SerialId == Convert.ToInt32(lblSerId.Text)).Select(x => x.Audjs_RefStatus).First().ToString();
                }

                List<AuditRemarkValue> _rmkVal = new List<AuditRemarkValue>();
                if (_AuditRmkList != null)
                {

                    _rmkVal = (from _res in _AuditRmkList
                               where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_itm == lblItem.Text.ToUpper() && _res.Ausv_rpt_cd == cmbRmk.SelectedValue.ToString()
                               select _res).ToList<AuditRemarkValue>();

                    //_rmkVal = (from _res in _AuditRmkList
                    //           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text)
                    //           select _res).ToList<AuditRemarkValue>();

                }
                else
                {
                    _AuditRmkList = new List<AuditRemarkValue>();
                }

                if (_rmkVal != null && _rmkVal.Count > 0)
                {
                    if (txtRmk1.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_itm == lblItem.Text.ToUpper() && _res.Ausv_rpt_cd == cmbRmk.SelectedValue.ToString() && _res.Ausv_line == 1
                                                           select _res).ToList<AuditRemarkValue>();

                        //List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                        //                                   where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_line == 1
                        //                                   select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_rpt_stus = _rmkStatus;
                            _rmkVal1[0].Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _rmkVal1[0].Ausv_val = txtRmk1.Text;
                            _rmkVal1[0].Ausv_line = 1;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;

                            //Add by akila 2017/04/28
                            _rmkVal1[0].Ausv_Mod_By = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal[0]);
                        }
                        else
                        {
                            AuditRemarkValue _tmpRmkVal = new AuditRemarkValue();
                            _tmpRmkVal.Ausv_itm = lblItem.Text;
                            _tmpRmkVal.Ausv_itm_stus = lblStus.Text;
                            _tmpRmkVal.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _tmpRmkVal.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _tmpRmkVal.Ausv_rpt_stus = _rmkStatus;
                            _tmpRmkVal.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _tmpRmkVal.Ausv_val = txtRmk1.Text;
                            _tmpRmkVal.Ausv_line = 1;
                            _tmpRmkVal.Ausv_cre_by = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_cre_dt = DateTime.Now;
                            _tmpRmkVal.Ausv_Mod_By = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_tmpRmkVal);
                        }
                    }
                    if (txtRmk2.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_itm == lblItem.Text.ToUpper() && _res.Ausv_rpt_cd == cmbRmk.SelectedValue.ToString() && _res.Ausv_line == 2
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_rpt_stus = _rmkStatus;
                            _rmkVal1[0].Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _rmkVal1[0].Ausv_val = txtRmk2.Text;
                            _rmkVal1[0].Ausv_line = 2;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;

                            //Add by akila 2017/04/28
                            _rmkVal1[0].Ausv_Mod_By = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal1[0]);
                        }
                        else
                        {
                            AuditRemarkValue _tmpRmkVal = new AuditRemarkValue();
                            _tmpRmkVal.Ausv_itm = lblItem.Text;
                            _tmpRmkVal.Ausv_itm_stus = lblStus.Text;
                            _tmpRmkVal.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _tmpRmkVal.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _tmpRmkVal.Ausv_rpt_stus = _rmkStatus;
                            _tmpRmkVal.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _tmpRmkVal.Ausv_val = txtRmk2.Text;
                            _tmpRmkVal.Ausv_line = 2;
                            _tmpRmkVal.Ausv_cre_by = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_cre_dt = DateTime.Now;
                            _tmpRmkVal.Ausv_Mod_By = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_tmpRmkVal);
                        }
                    }
                    if (txtRmk3.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_itm == lblItem.Text.ToUpper() && _res.Ausv_rpt_cd == cmbRmk.SelectedValue.ToString() && _res.Ausv_line == 3
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_rpt_stus = _rmkStatus;
                            _rmkVal1[0].Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _rmkVal1[0].Ausv_val = txtRmk3.Text;
                            _rmkVal1[0].Ausv_line = 3;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;

                            //Add by akila 2017/04/28
                            _rmkVal1[0].Ausv_Mod_By = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal1[0]);
                        }
                        else
                        {
                            AuditRemarkValue _tmpRmkVal = new AuditRemarkValue();
                            _tmpRmkVal.Ausv_itm = lblItem.Text;
                            _tmpRmkVal.Ausv_itm_stus = lblStus.Text;
                            _tmpRmkVal.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _tmpRmkVal.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _tmpRmkVal.Ausv_rpt_stus = _rmkStatus;
                            _tmpRmkVal.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _tmpRmkVal.Ausv_val = txtRmk3.Text;
                            _tmpRmkVal.Ausv_line = 3;
                            _tmpRmkVal.Ausv_cre_by = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_cre_dt = DateTime.Now;
                            _tmpRmkVal.Ausv_Mod_By = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_tmpRmkVal);
                        }
                        // _AuditRmkList.Add(_rmk);
                    }
                    if (txtRmk4.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_itm == lblItem.Text.ToUpper() && _res.Ausv_rpt_cd == cmbRmk.SelectedValue.ToString() && _res.Ausv_line == 4
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            //AuditRemarkValue _rmk = new AuditRemarkValue();
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_rpt_stus = _rmkStatus;
                            _rmkVal1[0].Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _rmkVal1[0].Ausv_val = txtRmk4.Text;
                            _rmkVal1[0].Ausv_line = 4;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;

                            //Add by akila 2017/04/28
                            _rmkVal1[0].Ausv_Mod_By = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal1[0]);
                            //_AuditRmkList.Add(_rmk);
                        }
                        else
                        {
                            AuditRemarkValue _tmpRmkVal = new AuditRemarkValue();
                            _tmpRmkVal.Ausv_itm = lblItem.Text;
                            _tmpRmkVal.Ausv_itm_stus = lblStus.Text;
                            _tmpRmkVal.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _tmpRmkVal.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _tmpRmkVal.Ausv_rpt_stus = _rmkStatus;
                            _tmpRmkVal.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _tmpRmkVal.Ausv_val = txtRmk4.Text;
                            _tmpRmkVal.Ausv_line = 4;
                            _tmpRmkVal.Ausv_cre_by = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_cre_dt = DateTime.Now;
                            _tmpRmkVal.Ausv_Mod_By = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_tmpRmkVal);
                        }
                    }
                    if (txtRmk5.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_itm == lblItem.Text.ToUpper() && _res.Ausv_rpt_cd == cmbRmk.SelectedValue.ToString() && _res.Ausv_line == 5
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            // AuditRemarkValue _rmk = new AuditRemarkValue();
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_rpt_stus = _rmkStatus;
                            _rmkVal1[0].Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _rmkVal1[0].Ausv_val = txtRmk5.Text;
                            _rmkVal1[0].Ausv_line = 5;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;

                            //Add by akila 2017/04/28
                            _rmkVal1[0].Ausv_Mod_By = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal1[0]);
                            //_AuditRmkList.Add(_rmk);
                        }
                        else
                        {
                            AuditRemarkValue _tmpRmkVal = new AuditRemarkValue();
                            _tmpRmkVal.Ausv_itm = lblItem.Text;
                            _tmpRmkVal.Ausv_itm_stus = lblStus.Text;
                            _tmpRmkVal.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _tmpRmkVal.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _tmpRmkVal.Ausv_rpt_stus = _rmkStatus;
                            _tmpRmkVal.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _tmpRmkVal.Ausv_val = txtRmk5.Text;
                            _tmpRmkVal.Ausv_line = 5;
                            _tmpRmkVal.Ausv_cre_by = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_cre_dt = DateTime.Now;
                            _tmpRmkVal.Ausv_Mod_By = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_tmpRmkVal);
                        }
                    }
                    if (txtRmk6.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_itm == lblItem.Text.ToUpper() && _res.Ausv_rpt_cd == cmbRmk.SelectedValue.ToString() && _res.Ausv_line == 6
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            // AuditRemarkValue _rmk = new AuditRemarkValue();
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_rpt_stus = _rmkStatus;
                            _rmkVal1[0].Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _rmkVal1[0].Ausv_val = txtRmk6.Text;
                            _rmkVal1[0].Ausv_line = 6;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;

                            //Add by akila 2017/04/28
                            _rmkVal1[0].Ausv_Mod_By = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal1[0]);
                            //_AuditRmkList.Add(_rmk);
                        }
                        else
                        {
                            AuditRemarkValue _tmpRmkVal = new AuditRemarkValue();
                            _tmpRmkVal.Ausv_itm = lblItem.Text;
                            _tmpRmkVal.Ausv_itm_stus = lblStus.Text;
                            _tmpRmkVal.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _tmpRmkVal.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _tmpRmkVal.Ausv_rpt_stus = _rmkStatus;
                            _tmpRmkVal.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _tmpRmkVal.Ausv_val = txtRmk6.Text;
                            _tmpRmkVal.Ausv_line = 6;
                            _tmpRmkVal.Ausv_cre_by = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_cre_dt = DateTime.Now;
                            _tmpRmkVal.Ausv_Mod_By = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_tmpRmkVal);
                        }
                    }
                    if (txtRmk7.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_itm == lblItem.Text.ToUpper() && _res.Ausv_rpt_cd == cmbRmk.SelectedValue.ToString() && _res.Ausv_line == 7
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            //AuditRemarkValue _rmk = new AuditRemarkValue();
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_rpt_stus = _rmkStatus;
                            _rmkVal1[0].Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_val = txtRmk7.Text;
                            _rmkVal1[0].Ausv_line = 7;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;

                            //Add by akila 2017/04/28
                            _rmkVal1[0].Ausv_Mod_By = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal1[0]);
                            //_AuditRmkList.Add(_rmk);
                        }
                        else
                        {
                            AuditRemarkValue _tmpRmkVal = new AuditRemarkValue();
                            _tmpRmkVal.Ausv_itm = lblItem.Text;
                            _tmpRmkVal.Ausv_itm_stus = lblStus.Text;
                            _tmpRmkVal.Ausv_rpt_stus = _rmkStatus;
                            _tmpRmkVal.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _tmpRmkVal.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _tmpRmkVal.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _tmpRmkVal.Ausv_val = txtRmk7.Text;
                            _tmpRmkVal.Ausv_line = 7;
                            _tmpRmkVal.Ausv_cre_by = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_cre_dt = DateTime.Now;
                            _tmpRmkVal.Ausv_Mod_By = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_tmpRmkVal);
                        }
                    }
                    if (txtRmk8.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_itm == lblItem.Text.ToUpper() && _res.Ausv_rpt_cd == cmbRmk.SelectedValue.ToString() && _res.Ausv_line == 8
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            //AuditRemarkValue _rmk = new AuditRemarkValue();
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_rpt_stus = _rmkStatus;
                            _rmkVal1[0].Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _rmkVal1[0].Ausv_val = txtRmk8.Text;
                            _rmkVal1[0].Ausv_line = 8;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;

                            //Add by akila 2017/04/28
                            _rmkVal1[0].Ausv_Mod_By = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal1[0]);
                            // _AuditRmkList.Add(_rmk);
                        }
                        else
                        {
                            AuditRemarkValue _tmpRmkVal = new AuditRemarkValue();
                            _tmpRmkVal.Ausv_itm = lblItem.Text;
                            _tmpRmkVal.Ausv_itm_stus = lblStus.Text;
                            _tmpRmkVal.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _tmpRmkVal.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _tmpRmkVal.Ausv_rpt_stus = _rmkStatus;
                            _tmpRmkVal.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _tmpRmkVal.Ausv_val = txtRmk8.Text;
                            _tmpRmkVal.Ausv_line = 8;
                            _tmpRmkVal.Ausv_cre_by = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_cre_dt = DateTime.Now;
                            _tmpRmkVal.Ausv_Mod_By = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_tmpRmkVal);
                        }
                    }
                    if (txtRmk9.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_itm == lblItem.Text.ToUpper() && _res.Ausv_rpt_cd == cmbRmk.SelectedValue.ToString() && _res.Ausv_line == 9
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            //AuditRemarkValue _rmk = new AuditRemarkValue();
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_rpt_stus = _rmkStatus;
                            _rmkVal1[0].Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _rmkVal1[0].Ausv_val = txtRmk9.Text;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_line = 9;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;

                            //Add by akila 2017/04/28
                            _rmkVal1[0].Ausv_Mod_By = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal1[0]);
                            //  _AuditRmkList.Add(_rmk);
                        }
                        else
                        {
                            AuditRemarkValue _tmpRmkVal = new AuditRemarkValue();
                            _tmpRmkVal.Ausv_itm = lblItem.Text;
                            _tmpRmkVal.Ausv_itm_stus = lblStus.Text;
                            _tmpRmkVal.Ausv_rpt_stus = _rmkStatus;
                            _tmpRmkVal.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _tmpRmkVal.Ausv_val = txtRmk9.Text;
                            _tmpRmkVal.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _tmpRmkVal.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _tmpRmkVal.Ausv_line = 9;
                            _tmpRmkVal.Ausv_cre_by = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_cre_dt = DateTime.Now;
                            _tmpRmkVal.Ausv_Mod_By = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_tmpRmkVal);
                        }
                    }
                    if (txtRmk10.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_itm == lblItem.Text.ToUpper() && _res.Ausv_rpt_cd == cmbRmk.SelectedValue.ToString() && _res.Ausv_line == 10
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            // AuditRemarkValue _rmk = new AuditRemarkValue();
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_rpt_stus = _rmkStatus;
                            _rmkVal1[0].Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _rmkVal1[0].Ausv_val = txtRmk10.Text;
                            _rmkVal1[0].Ausv_line = 10;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;

                            //Add by akila 2017/04/28
                            _rmkVal1[0].Ausv_Mod_By = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal1[0]);
                            //_AuditRmkList.Add(_rmk);
                        }
                        else
                        {
                            AuditRemarkValue _tmpRmkVal = new AuditRemarkValue();
                            _tmpRmkVal.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _tmpRmkVal.Ausv_itm = lblItem.Text;
                            _tmpRmkVal.Ausv_itm_stus = lblStus.Text;
                            _tmpRmkVal.Ausv_rpt_stus = _rmkStatus;
                            _tmpRmkVal.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _tmpRmkVal.Ausv_val = txtRmk10.Text;
                            _tmpRmkVal.Ausv_line = 10;
                            _tmpRmkVal.Ausv_cre_by = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _tmpRmkVal.Ausv_cre_dt = DateTime.Now;
                            _tmpRmkVal.Ausv_Mod_By = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_tmpRmkVal);
                        }
                    }
                    if (cmbRmk11.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_itm == lblItem.Text.ToUpper() && _res.Ausv_rpt_cd == cmbRmk.SelectedValue.ToString() && _res.Ausv_line == 10
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            // AuditRemarkValue _rmk = new AuditRemarkValue();
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_rpt_stus = _rmkStatus;
                            _rmkVal1[0].Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _rmkVal1[0].Ausv_val = cmbRmk11.Text;
                            _rmkVal1[0].Ausv_line = 11;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;

                            //Add by akila 2017/04/28
                            _rmkVal1[0].Ausv_Mod_By = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal1[0]);
                            //_AuditRmkList.Add(_rmk);
                        }
                        else
                        {
                            AuditRemarkValue _tmpRmkVal = new AuditRemarkValue();
                            _tmpRmkVal.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _tmpRmkVal.Ausv_itm = lblItem.Text;
                            _tmpRmkVal.Ausv_itm_stus = lblStus.Text;
                            _tmpRmkVal.Ausv_rpt_stus = _rmkStatus;
                            _tmpRmkVal.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _tmpRmkVal.Ausv_val = cmbRmk11.Text;
                            _tmpRmkVal.Ausv_line = 11;
                            _tmpRmkVal.Ausv_cre_by = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _tmpRmkVal.Ausv_cre_dt = DateTime.Now;
                            _tmpRmkVal.Ausv_Mod_By = BaseCls.GlbUserID;
                            _tmpRmkVal.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_tmpRmkVal);
                        }
                    }
                }
                else
                {
                    if (txtRmk1.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_job_seq = _seqNo;
                        _rmk.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                        _rmk.Ausv_rpt_stus = _rmkStatus;
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_val = txtRmk1.Text;
                        _rmk.Ausv_line = 1;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_cre_dt = DateTime.Now;

                        //Add by akila 2017/04/28
                        _rmk.Ausv_Mod_By = BaseCls.GlbUserID;
                        _rmk.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                        //_AuditRmkList.Add(_rmk);
                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                    if (txtRmk2.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_job_seq = _seqNo;
                        _rmk.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                        _rmk.Ausv_rpt_stus = _rmkStatus;
                        _rmk.Ausv_val = txtRmk2.Text;
                        _rmk.Ausv_line = 2;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_cre_dt = DateTime.Now;
                        //_AuditRmkList.Add(_rmk);

                        //Add by akila 2017/04/28
                        _rmk.Ausv_Mod_By = BaseCls.GlbUserID;
                        _rmk.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                    if (txtRmk3.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_job_seq = _seqNo;
                        _rmk.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                        _rmk.Ausv_rpt_stus = _rmkStatus;
                        _rmk.Ausv_val = txtRmk3.Text;
                        _rmk.Ausv_line = 3;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_cre_dt = DateTime.Now;
                        //_AuditRmkList.Add(_rmk);

                        //Add by akila 2017/04/28
                        _rmk.Ausv_Mod_By = BaseCls.GlbUserID;
                        _rmk.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                    if (txtRmk4.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_job_seq = _seqNo;
                        _rmk.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                        _rmk.Ausv_rpt_stus = _rmkStatus;
                        _rmk.Ausv_val = txtRmk4.Text;
                        _rmk.Ausv_line = 4;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_cre_dt = DateTime.Now;
                        //_AuditRmkList.Add(_rmk);

                        //Add by akila 2017/04/28
                        _rmk.Ausv_Mod_By = BaseCls.GlbUserID;
                        _rmk.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                    if (txtRmk5.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_job_seq = _seqNo;
                        _rmk.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                        _rmk.Ausv_rpt_stus = _rmkStatus;
                        _rmk.Ausv_val = txtRmk5.Text;
                        _rmk.Ausv_line = 5;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_cre_dt = DateTime.Now;
                        //_AuditRmkList.Add(_rmk);

                        //Add by akila 2017/04/28
                        _rmk.Ausv_Mod_By = BaseCls.GlbUserID;
                        _rmk.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                    if (txtRmk6.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_job_seq = _seqNo;
                        _rmk.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                        _rmk.Ausv_rpt_stus = _rmkStatus;
                        _rmk.Ausv_val = txtRmk6.Text;
                        _rmk.Ausv_line = 6;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_cre_dt = DateTime.Now;
                        //_AuditRmkList.Add(_rmk);

                        //Add by akila 2017/04/28
                        _rmk.Ausv_Mod_By = BaseCls.GlbUserID;
                        _rmk.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                    if (txtRmk7.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_rpt_stus = _rmkStatus;
                        _rmk.Ausv_job_seq = _seqNo;
                        _rmk.Ausv_val = txtRmk7.Text;
                        _rmk.Ausv_line = 7;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_cre_dt = DateTime.Now;
                        // _AuditRmkList.Add(_rmk);

                        //Add by akila 2017/04/28
                        _rmk.Ausv_Mod_By = BaseCls.GlbUserID;
                        _rmk.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                    if (txtRmk8.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_job_seq = _seqNo;
                        _rmk.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                        _rmk.Ausv_rpt_stus = _rmkStatus;
                        _rmk.Ausv_val = txtRmk8.Text;
                        _rmk.Ausv_line = 8;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_cre_dt = DateTime.Now;
                        //_AuditRmkList.Add(_rmk);

                        //Add by akila 2017/04/28
                        _rmk.Ausv_Mod_By = BaseCls.GlbUserID;
                        _rmk.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                    if (txtRmk9.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                        _rmk.Ausv_rpt_stus = _rmkStatus;
                        _rmk.Ausv_val = txtRmk9.Text;
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_job_seq = _seqNo;
                        _rmk.Ausv_line = 9;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_cre_dt = DateTime.Now;
                        // _AuditRmkList.Add(_rmk);

                        //Add by akila 2017/04/28
                        _rmk.Ausv_Mod_By = BaseCls.GlbUserID;
                        _rmk.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                    if (txtRmk10.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_job_seq = _seqNo;
                        _rmk.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                        _rmk.Ausv_rpt_stus = _rmkStatus;
                        _rmk.Ausv_val = txtRmk10.Text;
                        _rmk.Ausv_line = 10;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_cre_dt = DateTime.Now;
                        //_AuditRmkList.Add(_rmk);

                        //Add by akila 2017/04/28
                        _rmk.Ausv_Mod_By = BaseCls.GlbUserID;
                        _rmk.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                    if (cmbRmk11.Visible)
                    {
                        string _tmpString = cmbRmk11.SelectedValue == null ? cmbRmk11.Text : cmbRmk11.SelectedValue.ToString();
                        if (!string.IsNullOrEmpty(_tmpString))
                        {
                            AuditRemarkValue _rmk = new AuditRemarkValue();
                            _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmk.Ausv_itm = lblItem.Text;
                            _rmk.Ausv_itm_stus = lblStus.Text;
                            _rmk.Ausv_job_seq = _seqNo;
                            _rmk.Ausv_job = cmbAudSubJobNo.SelectedItem.ToString();
                            _rmk.Ausv_rpt_stus = _rmkStatus;
                            _rmk.Ausv_val = _tmpString;
                            _rmk.Ausv_line = 11;
                            _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                            _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmk.Ausv_cre_dt = DateTime.Now;
                            //_AuditRmkList.Add(_rmk);

                            //Add by akila 2017/04/28
                            _rmk.Ausv_Mod_By = BaseCls.GlbUserID;
                            _rmk.Ausv_Session_Id = BaseCls.GlbUserSessionID;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                        }
                    }
                }

                txtRmk1.Text = "";
                txtRmk2.Text = "";
                txtRmk3.Text = "";
                txtRmk4.Text = "";
                txtRmk5.Text = "";
                txtRmk6.Text = "";
                txtRmk7.Text = "";
                txtRmk8.Text = "";
                txtRmk9.Text = "";
                txtRmk10.Text = "";
                cmbRmk11.Text = "";
                cmbRmk11.DataSource = null;

                btn_Exreq.Visible = false;

                lblSuccessMsg.Text = "Remarks saved successfully";
                lblSuccessMsg.Visible = true;
                pbCorrect.Visible = true;
                pbIncorrect.Visible = false;
            }
            catch (Exception)
            {
                CHNLSVC.CloseChannel();
                lblSuccessMsg.Text = "Couldn't save remarks";
                lblSuccessMsg.Visible = true;
                pbCorrect.Visible = false;
                pbIncorrect.Visible = true;

            }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btn_Exreq_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbRmk.SelectedValue.ToString() == "NOTE 03")
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ExchangeRequest);
                    DataTable _result = CHNLSVC.CommonSearch.SearchExchangeRequest(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtRmk7;
                    _CommonSearch.ShowDialog();
                    //txtCrNAmount.Text = _CommonSearch.GetResult(_CommonSearch.GlbSelectData, 3);                
                    txtRmk7.Select();

                    if (!string.IsNullOrEmpty(txtRmk7.Text))
                    {
                        _ExchnageItems = new List<RequestApprovalSerials>();
                        CHNLSVC.General.Get_gen_reqapp_ser(BaseCls.GlbUserComCode, txtRmk7.Text.Trim(), out _ExchnageItems);
                        if (_ExchnageItems.Count > 0)
                        {
                            BindingSource _bind = new BindingSource();
                            _bind.DataSource = _ExchnageItems.Select(x => x.Gras_anal2).ToList();
                            cmbRmk11.DataSource = _bind;
                            //cmbRmk11.DisplayMember = "Gras_anal2";
                            //cmbRmk11.ValueMember = "Gras_anal2";
                            cmbRmk11.SelectedIndex = 0;
                        }
                    }
                }
                else if (cmbRmk.SelectedValue.ToString() == "NOTE 02")
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ForwardInvoice);
                    DataTable _result = CHNLSVC.CommonSearch.SearchForwardInvoice(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtRmk7;
                    _CommonSearch.ShowDialog();
                    txtRmk7.Select();
                    txtRmk7.Focus();
                }
                else if (cmbRmk.SelectedValue.ToString() == "X NOTE 02")
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.FixAssetRequest);
                    DataTable _result = CHNLSVC.CommonSearch.SearchFixAssetRequest(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtRmk7;
                    _CommonSearch.ShowDialog();
                    txtRmk7.Select();

                    if (!string.IsNullOrEmpty(txtRmk7.Text))
                    {
                        _result = new DataTable();
                        _result = CHNLSVC.CommonSearch.SearchFixAssetRequest(_CommonSearch.SearchParams, "REQUEST NO", txtRmk7.Text);
                        if (_result.Rows.Count > 0)
                        {
                            string _tmpRequestNo = string.Empty;
                            foreach (DataRow _row in _result.Rows)
                            {
                                _tmpRequestNo = _row["REQUEST DATE"] == DBNull.Value ? string.Empty : "(" + _row["REQUEST DATE"].ToString() + ")";
                            }
                            txtRmk7.Text += " " + _tmpRequestNo;
                        }
                    }
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

        private void gvRmk_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblSuccessMsg.Visible = false;
            pbCorrect.Visible = false;
            pbIncorrect.Visible = false;

            try
            {
                if (gvRmk.Rows.Count > 0)
                {
                    if (e.RowIndex != -1 && e.ColumnIndex == 0)
                    {
                        lblItem.Text = null;
                        lblStus.Text = null;
                        lblSerId.Text = null;
                        lblSerial.Text = null;

                        itemLayout.Visible = true;
                        lblItem.Text = gvRmk.Rows[e.RowIndex].Cells["AudItemCode"].Value.ToString();
                        lblStus.Text = gvRmk.Rows[e.RowIndex].Cells["AudItemStatus"].Value.ToString();
                        lblSerId.Text = gvRmk.Rows[e.RowIndex].Cells["AuditSerialId"].Value.ToString();
                        lblSerial.Text = gvRmk.Rows[e.RowIndex].Cells["AudSystemSerial"].Value.ToString();
                        cmbRmk.SelectedValue = gvRmk.Rows[e.RowIndex].Cells["AudRptType"].Value.ToString();

                        _AuditRmkList = new List<AuditRemarkValue>();
                        _AuditRmkList = CHNLSVC.Inventory.GetPhicalStockRemark(cmbAudSubJobNo.SelectedItem.ToString());

                        if (((gvRmk.Rows[e.RowIndex].Cells["AudRptType"].Value.ToString() == "NOTE 03") || (gvRmk.Rows[e.RowIndex].Cells["AudRptType"].Value.ToString() == "NOTE 02") || (gvRmk.Rows[e.RowIndex].Cells["AudRptType"].Value.ToString() == "X NOTE 02")) && (lblJobStatus.Text != "FINISHED"))
                        {
                            btn_Exreq.Visible = true;
                        }
                        else
                        {
                            btn_Exreq.Visible = false;
                        }

                        //load previous remarks
                        List<AuditRemarkValue> _rmk = new List<AuditRemarkValue>();
                        if (_AuditRmkList != null)
                        {
                            _rmk = (from _res in _AuditRmkList
                                    where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_itm == lblItem.Text.ToUpper() && _res.Ausv_rpt_cd == gvRmk.Rows[e.RowIndex].Cells["AudRptType"].Value.ToString()
                                    select _res).ToList<AuditRemarkValue>();

                            //_rmk = (from _res in _AuditRmkList
                            //        where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text)
                            //        select _res).ToList<AuditRemarkValue>();
                        }

                        // _rmk = _rmk.GroupBy(x => new { x.Ausv_ser_id, x.Ausv_line }).Select(x => x.Last()).ToList<AuditRemarkValue>();
                        txtRmk1.Text = "";
                        txtRmk2.Text = "";
                        txtRmk3.Text = "";
                        txtRmk4.Text = "";
                        txtRmk5.Text = "";
                        txtRmk6.Text = "";
                        txtRmk7.Text = "";
                        txtRmk8.Text = "";
                        txtRmk9.Text = "";
                        txtRmk10.Text = "";
                        cmbRmk11.Text = "";
                        _ExchnageItems = new List<RequestApprovalSerials>();

                        //Load remark details
                        if (_rmk.Count < 1)
                        {
                            LoadRemarkDetails(gvRmk.Rows[e.RowIndex].Cells["AudRptType"].Value.ToString());
                        }

                        gvRmkVal.AutoGenerateColumns = false;
                        BindingSource _sou = new BindingSource();
                        _sou.DataSource = _rmk;
                        gvRmkVal.DataSource = _sou;
                        if (_rmk != null && _rmk.Count > 0)
                        {
                            foreach (AuditRemarkValue _rmkVal in _rmk)
                            {

                                if (_rmkVal.Ausv_line == 1)
                                {
                                    txtRmk1.Text = _rmkVal.Ausv_val;
                                }
                                if (_rmkVal.Ausv_line == 2)
                                {
                                    txtRmk2.Text = _rmkVal.Ausv_val;
                                }
                                if (_rmkVal.Ausv_line == 3)
                                {
                                    txtRmk3.Text = _rmkVal.Ausv_val;
                                }
                                if (_rmkVal.Ausv_line == 4)
                                {
                                    txtRmk4.Text = _rmkVal.Ausv_val;
                                }
                                if (_rmkVal.Ausv_line == 5)
                                {
                                    txtRmk5.Text = _rmkVal.Ausv_val;
                                }
                                if (_rmkVal.Ausv_line == 6)
                                {
                                    txtRmk6.Text = _rmkVal.Ausv_val;
                                }
                                if (_rmkVal.Ausv_line == 7)
                                {
                                    txtRmk7.Text = _rmkVal.Ausv_val;
                                }
                                if (_rmkVal.Ausv_line == 8)
                                {
                                    txtRmk8.Text = _rmkVal.Ausv_val;
                                }
                                if (_rmkVal.Ausv_line == 9)
                                {
                                    txtRmk9.Text = _rmkVal.Ausv_val;
                                }
                                if (_rmkVal.Ausv_line == 10)
                                {
                                    txtRmk10.Text = _rmkVal.Ausv_val;
                                }
                                if (_rmkVal.Ausv_line == 11)
                                {
                                    cmbRmk11.Text = _rmkVal.Ausv_val;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            { CHNLSVC.CloseChannel(); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnpopupRmk_Click(object sender, EventArgs e)
        {
            itemLayout.Visible = false;
            grbSubJobSelection.Visible = true;
            pnlAuditRmk.Visible = false;
            pnlSerialInfo.Visible = true;
            pnlSerialInfo.Size = new Size(820, 520);
            this.pnlSerialInfo.Location = new System.Drawing.Point(3, 0);

        }

        private void LoadRemarkedSerials()
        {
            if (cmbAudSubJobNo.SelectedIndex >= 0)
            {
                _AuditRmkList = new List<AuditRemarkValue>();
                _AuditRmkList = CHNLSVC.Inventory.GetPhicalStockRemark(cmbAudSubJobNo.SelectedItem.ToString());

                _remarkedSerials = new List<AuditJobSerial>();
                _remarkedSerials = CHNLSVC.Inventory.GetRemarkedSerials(cmbAudSubJobNo.SelectedItem.ToString());


                if ((_remarkedSerials.Count > 0) && (_stusList.Count > 0))
                {

                    var _queryREsult = (from rmkSerials in _remarkedSerials
                                        join note in _stusList
                                            on rmkSerials.Audjs_RefStatus equals note.Aurs_main_cd
                                        select new { rmkSerials.Audjs_ItemCode, rmkSerials.Audjs_ItemStatus, rmkSerials.Audjs_SerialNo, rmkSerials.Audjs_PhysicallyAvailableSerial, rmkSerials.Audjs_WarrantyNo, Audjs_RefStatus = note.Aurs_desc, rmkSerials.Audjs_RptType, rmkSerials.Audjs_SerialId }).ToList();

                    BindingSource _serials = new BindingSource();
                    _serials.DataSource = _queryREsult.Select(x => new { x.Audjs_ItemCode, x.Audjs_ItemStatus, x.Audjs_SerialNo, x.Audjs_PhysicallyAvailableSerial, x.Audjs_WarrantyNo, x.Audjs_RefStatus, x.Audjs_RptType, x.Audjs_SerialId }).ToList();
                    gvRmk.DataSource = _serials;
                }

            }
            else
            {
                MessageBox.Show("Please select a sub job#", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAudSubJobNo.Focus();
                return;
            }

        }

        private void cmbAudSubJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadRemarkedSerials(); //load serail details which have been entered the remarks previously
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("An error occurred while loading audit remarks" + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAuditRemarkScreen(string _subJobNo, string _currentRowSerialId)
        {
            try
            {
                List<string> _selectedSubJobs = new List<string>();
                if (!string.IsNullOrEmpty(_subJobNo))
                {
                    _selectedSubJobs.Add(_subJobNo);

                    if (_selectedSubJobs.Count > 0)
                    {
                        //Load audit reasons
                        _stusList = new List<AuditReportStatus>();
                        _stusList = GetAuditReason();

                        //Bind selected job numbers to drop down
                        BindingSource _cmbDataBind = new BindingSource();
                        _cmbDataBind.DataSource = _selectedSubJobs;
                        cmbAudSubJobNo.DataSource = _cmbDataBind;
                        cmbAudSubJobNo.SelectedIndex = 0;


                        //Load audit reasons
                        _stusList = new List<AuditReportStatus>();
                        _stusList = GetAuditReason();
                        LoadRemarkedSerials(); //load serail details which have been entered the remarks previously

                        //load remarked serials which are only in current page                        
                        //if (_remarkedSerials.Count > 0)
                        //{
                        //    List<string> _updatedSerials = new List<string>();
                        //    _updatedSerials = GetRemarkedSerails();
                        //    if (_updatedSerials.Count > 0)
                        //    {
                        //        _remarkedSerials = (from rmkSerials in _remarkedSerials
                        //                            join serials in _updatedSerials
                        //                                on rmkSerials.Audjs_SerialNo equals serials
                        //                            select rmkSerials).ToList();

                        //        LoadRemarkedSerials(); //load serail details which have been entered the remarks previously 

                        //        //BindingSource _serials = new BindingSource();
                        //        //_serials.DataSource = _remarkedSerials.Select(x => new { x.Audjs_ItemCode, x.Audjs_ItemStatus, x.Audjs_SerialNo, x.Audjs_PhysicallyAvailableSerial, x.Audjs_WarrantyNo, x.Audjs_RefStatus, x.Audjs_RptType, x.Audjs_SerialId }).ToList();
                        //        //gvRmk.DataSource = _serials;                              
                        //    }
                        //}

                        BindingSource _bind = new BindingSource();
                        _bind.DataSource = _stusList.Select(x => new { x.Aurs_code, x.Aurs_desc }).ToList();
                        cmbRmk.DataSource = _bind;
                        cmbRmk.DisplayMember = "Aurs_desc";
                        cmbRmk.ValueMember = "Aurs_code";
                        cmbRmk.SelectedIndex = 0;

                        int _defaultIndex = 0;
                        if (!string.IsNullOrEmpty(_currentRowSerialId))
                        {
                            List<DataGridViewRow> _tmpRowList = gvRmk.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["AuditSerialId"].Value.ToString() == _currentRowSerialId).ToList();
                            if (_tmpRowList != null)
                            {
                                if (_tmpRowList.Count > 0)
                                {
                                    _defaultIndex = Convert.ToInt32(_tmpRowList.Select(x => x.Index).First());
                                }
                            }
                        }

                        lblSuccessMsg.Visible = false;
                        pbCorrect.Visible = false;
                        pbIncorrect.Visible = false;

                        grbSubJobSelection.Visible = false;
                        pnlSerialInfo.Visible = false;
                        gvRmk_CellContentClick(null, new DataGridViewCellEventArgs(0, _defaultIndex));
                        gvRmk.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                        if (gvRmk.CurrentRow != null) { gvRmk.CurrentRow.Selected = false; }
                        // gvRmk.CurrentRow.Selected = false;
                        gvRmk.FirstDisplayedScrollingRowIndex = _defaultIndex;
                        gvRmk.Rows[_defaultIndex].Selected = true;
                        pnlAuditRmk.Size = new Size(820, 530);
                        pnlAuditRmk.BringToFront();
                        pnlAuditRmk.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Please select a sub job number", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvSubJobList.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Sub job details not found", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseAllChannels();
                this.Cursor = Cursors.Default;
                MessageBox.Show("An error occurred while loading audit remarks" + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<string> GetRemarkedSerails()
        {
            List<string> _updatedSerials = new List<string>();
            try
            {
                if (dgvProcessedSerials.Rows.Count > 0)
                {
                    foreach (DataGridViewRow _dataRow in dgvProcessedSerials.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["colRemark"].Value != null || x.Cells["colReason"].Value != null).Select(x => x).ToList())
                    {
                        if ((_dataRow.Cells["colRemark"].Value != null) || (_dataRow.Cells["colReason"].Value != null))
                        {
                            _updatedSerials.Add(_dataRow.Cells["colSystemSerialId"].Value == null ? string.Empty : _dataRow.Cells["colSystemSerialId"].Value.ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _updatedSerials;
        }

        private void chkIsExcessItem_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsExcessItem.Checked)
            {
                if (!string.IsNullOrEmpty(txtCommonSerialField.Text))
                {
                    if ((chkIsExcessItem.Checked) && (txtCommonSerialField.Text != "N/A"))
                    {
                        string _tmpSerial = txtCommonSerialField.Text.Trim().ToUpper();
                        DataTable tmpSerialDetails = new DataTable();
                        tmpSerialDetails = CHNLSVC.Inventory.GetSerialDetailsBySerial(BaseCls.GlbUserComCode, txtLocation.Text.Trim().ToUpper(), null, _tmpSerial);
                        if (tmpSerialDetails.Rows.Count > 0)
                        {
                            MessageBox.Show("Invalid Serial #" + Environment.NewLine + "Serial # - " + _tmpSerial + " all ready exist in current location", "Stock Verification-Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCommonSerialField.Text = string.Empty;
                            chkIsExcessItem.Checked = false;
                            txtCommonSerialField.Focus();
                            return;
                        }
                    }
                }


                lblUploadItem.Visible = true;
                txtUploadItem.Visible = true;
                btnSearchExcessItem.Visible = true;
            }
            else
            {
                lblUploadItem.Visible = false;
                txtUploadItem.Visible = false;
                btnSearchExcessItem.Visible = false;
            }
        }

        private void btnSearchExcessItem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtUploadItem;
                _CommonSearch.ShowDialog();
                txtUploadItem.Focus();
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

        private void txtSerSearchContOnGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSerialSearchOnGrid_Click(null, null);
            }

        }

        private void btnSerialSearchOnGrid_Click(object sender, EventArgs e)
        {
            if (_isSerialGridDataEdited)
            {
                if (GetUserEnteredGridData().Count > 0)
                {
                    this.Cursor = Cursors.Default;
                    DialogResult _dialogResult = MessageBox.Show("Entered remarks will lost. Do you want to proceed ?", "Stock Verification - Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (_dialogResult == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    else { _isSerialGridDataEdited = false; }
                }
            }

            if (string.IsNullOrEmpty(txtSerSearchContOnGrid.Text))
            {
                MessageBox.Show("Please enter the serail no", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUploadItem.Focus();
                return;
            }
            else
            {
                radioBtnMismatch.Checked = true;
                radioBtnMismatch_CheckedChanged(null, null);

            BeginSearch:
                if ((dgvProcessedSerials.Rows.Count > 0) && (_processedAuditJobSerials.Count > 0))
                {
                    List<DataGridViewRow> _queryResult = dgvProcessedSerials.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["colSystemSerialId"].Value.ToString().Contains(txtSerSearchContOnGrid.Text)).ToList();
                    if (_queryResult.Count > 0)
                    {
                        int _rowIndex = _queryResult.Select(x => x.Index).First();
                        dgvProcessedSerials.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgvProcessedSerials.CurrentRow.Selected = false;
                        dgvProcessedSerials.Rows[_rowIndex].Selected = true;
                        dgvProcessedSerials.FirstDisplayedScrollingRowIndex = _rowIndex;
                    }
                    else { btnSerialNext_Click(null, null); goto BeginSearch; }
                }
                else { MessageBox.Show("Couldn't find serial details", "Stock Verfication - Information", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            }
        }

        //private void btnSearchEmp_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (chkNewJob.Checked)
        //        {
        //            txtEmp.Text = null;
        //            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
        //            _CommonSearch.ReturnIndex = 0;
        //            _CommonSearch.IsSearchEnter = true;
        //            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Auditors);
        //            DataTable _result = CHNLSVC.CommonSearch.SearchAllEmployee(_CommonSearch.SearchParams, null, null);
        //            _CommonSearch.dvResult.DataSource = _result;
        //            _CommonSearch.BindUCtrlDDLData(_result);
        //            _CommonSearch.obj_TragetTextBox = txtEmp;
        //            _CommonSearch.ShowDialog();
        //            txtEmp.Select();

        //            string _empId = txtEmp.Text;
        //            if (!string.IsNullOrEmpty(_empId))
        //            {
        //                _result = CHNLSVC.CommonSearch.SearchAllEmployee( BaseCls.GlbUserComCode+"|", "EPF NO", _empId);
        //                if (_result.Rows.Count > 0)
        //                {
        //                    txtEmp.Text = _result.AsEnumerable().Where(x => x.Field<string>("EPF NO").ToString() == _empId).Select(x => x.Field<string>("FIRST NAME")).First().ToString();
        //                    if (dgvMemberList.Rows.Count > 0)
        //                    {
        //                        //foreach (DataGridViewRow row in dgvMemberList.Rows)
        //                        //{
        //                        //    if (row.Cells["dgvcMemberId"].Value.ToString() == _empId)
        //                        //    {
        //                        //        row.Cells["dgvcMemberChecked"].Value = true;
        //                        //            dgvMemberList.FirstDisplayedScrollingRowIndex = row.Index;
        //                        //    }
        //                        //}

        //                        List<DataGridViewRow> _rowList = dgvMemberList.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["dgvcMemberId"].Value.Equals(_empId)).ToList();
        //                        if (_rowList.Count > 0)
        //                        {
        //                            int _rowIndex = _rowList.Select(x => x.Index).First();
        //                            dgvMemberList.Rows[_rowIndex].Cells["dgvcMemberChecked"].Value = true;
        //                            dgvMemberList.FirstDisplayedScrollingRowIndex = _rowIndex;
        //                        }
        //                    }
        //                }                        
        //            }
        //            else { txtEmp.Text = string.Empty; }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        this.Cursor = Cursors.Default;
        //        CHNLSVC.CloseAllChannels();
        //        MessageBox.Show("An error occurred while searching members" + Environment.NewLine + ex.Message, "Stock Verfication - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void txtEmp_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.F2)
        //    {
        //        btnSearchEmp_Click(null, null);
        //    }
        //    else if (e.KeyCode == Keys.Enter)
        //    {
        //        btnSearchSupervisor.Focus();
        //    }
        //}

        //private void txtEmp_DoubleClick(object sender, EventArgs e)
        //{
        //    btnSearchEmp_Click(null, null);
        //}

        private void chkViewComment_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkViewComment.Checked)
                {
                    //hide un-commented rows
                    if (dgvProcessedSerials.Rows.Count > 0)
                    {
                        dgvProcessedSerials.CurrentCell = null;
                        foreach (DataGridViewRow _row in dgvProcessedSerials.Rows)
                        {
                            string _tmpReason = _row.Cells["colReasonStatus"].Value == null ? string.Empty : _row.Cells["colReasonStatus"].Value.ToString();
                            string _tmpComment = _row.Cells["colRemark"].Value == null ? string.Empty : _row.Cells["colRemark"].Value.ToString();
                            if ((string.IsNullOrEmpty(_tmpReason)) && (string.IsNullOrEmpty(_tmpComment)))
                            {
                                dgvProcessedSerials.Rows[_row.Index].Visible = false;
                            }
                        }
                    }
                }
                else
                {
                    //un-hide all rows
                    if (dgvProcessedSerials.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow _row in dgvProcessedSerials.Rows)
                        {
                            dgvProcessedSerials.Rows[_row.Index].Visible = true;
                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cmbRmk11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cmbRmk11.SelectedValue != null) && (cmbRmk.SelectedValue.ToString() == "NOTE 03"))
            {
                if (_ExchnageItems.Count > 0)
                {
                    string _tmpSerial = _ExchnageItems.Where(x => x.Gras_anal2.Equals(cmbRmk11.SelectedValue)).Select(x => x.Gras_anal3).First().ToString();
                    txtRmk10.Text = _tmpSerial;
                }
            }
        }

        private void LoadRemarkDetails(string _remarkType)
        {
            try
            {
                if (_remarkType == "X NOTE 01")
                {
                    DataTable _damageItemDetails = new DataTable();
                    _damageItemDetails = CHNLSVC.Inventory.GetDamageAuditItemDetails(BaseCls.GlbUserComCode, txtLocation.Text.Trim(), lblSerial.Text, lblItem.Text);
                    if (_damageItemDetails.Rows.Count > 0)
                    {
                        foreach (DataRow _item in _damageItemDetails.Rows)
                        {
                            string _remark = _item["itr_note"] == DBNull.Value ? string.Empty : "(" + _item["itr_note"].ToString() + ")";
                            string _docDate = _item["itr_dt"] == DBNull.Value ? string.Empty : _item["itr_dt"].ToString();
                            string _docNo = _item["itr_req_no"] == DBNull.Value ? string.Empty : _item["itr_req_no"].ToString();
                            txtRmk2.Text = _docNo + " - " + _docDate + " " + _remark;
                        }
                    }
                }
                else if (_remarkType == "NOTE 08")// Load rcc details
                {
                    DataTable _rccDetails = new DataTable();
                    _rccDetails = CHNLSVC.Inventory.GetRccDetails(BaseCls.GlbUserComCode, txtLocation.Text.Trim(), lblSerial.Text, lblItem.Text);
                    if (_rccDetails.Rows.Count > 0)
                    {
                        foreach (DataRow _rcc in _rccDetails.Rows)
                        {
                            string _tmpRccNo = _rcc["Rcc No"] == DBNull.Value ? string.Empty : _rcc["Rcc No"].ToString();
                            string _tmpRccDate = _rcc["Rcc Date"] == DBNull.Value ? string.Empty : _rcc["Rcc Date"].ToString();
                            string _tmpAgentCode = _rcc["Agent Code"] == DBNull.Value ? string.Empty : _rcc["Agent Code"].ToString();
                            string _tmpAgentName = _rcc["Agent Name"] == DBNull.Value ? string.Empty : _rcc["Agent Name"].ToString();
                            string _tmpRemark = _rcc["Remark"] == DBNull.Value ? string.Empty : "(" + _rcc["Remark"].ToString() + ")";
                            string _tmpJobNo = _rcc["Job No"] == DBNull.Value ? string.Empty : _rcc["Job No"].ToString();
                            string _tmpJobDate = _rcc["Job Date"] == DBNull.Value ? string.Empty : _rcc["Job Date"].ToString();

                            txtRmk1.Text = _tmpRccNo + " - " + _tmpRccDate + " " + _tmpRemark;
                            txtRmk7.Text = _tmpJobNo + " - " + _tmpJobDate;
                            txtRmk3.Text = _tmpAgentCode + " - " + _tmpAgentName;
                        }
                    }
                }
                else if (_remarkType == "X NOTE 07")// Load rcc details
                {
                    DataTable _rccDetails = new DataTable();
                    _rccDetails = CHNLSVC.Inventory.GetRccDetails(BaseCls.GlbUserComCode, txtLocation.Text.Trim(), lblSerial.Text, lblItem.Text);
                    if (_rccDetails.Rows.Count > 0)
                    {
                        foreach (DataRow _rcc in _rccDetails.Rows)
                        {
                            string _tmpRccNo = _rcc["Rcc No"] == DBNull.Value ? string.Empty : _rcc["Rcc No"].ToString();
                            string _tmpRccDate = _rcc["Rcc Date"] == DBNull.Value ? string.Empty : _rcc["Rcc Date"].ToString();

                            txtRmk5.Text = _tmpRccNo;
                            txtRmk6.Text = _tmpRccDate;
                        }
                    }
                }
                else if (_remarkType == "X NOTE 06")// Load rcc details
                {
                    DataTable _invDetails = new DataTable();
                    _invDetails = CHNLSVC.Inventory.GetInvNotDeliveredDetails(BaseCls.GlbUserComCode, txtLocation.Text.Trim(), lblSerial.Text, lblItem.Text);
                    if (_invDetails.Rows.Count > 0)
                    {
                        foreach (DataRow _inv in _invDetails.Rows)
                        {
                            string _tmpInvNo = _inv["ith_doc_no"] == DBNull.Value ? string.Empty : _inv["ith_doc_no"].ToString();
                            string _tmpInvDate = _inv["ith_doc_date"] == DBNull.Value ? string.Empty : _inv["ith_doc_date"].ToString();

                            txtRmk6.Text = _tmpInvNo;
                            txtRmk4.Text = _tmpInvDate;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult _dialog = MessageBox.Show("Do you want to reser the current job ?", "Stock Verification - Finish Jobs", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dialog == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16071))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("You don't have permission to perform this action " + Environment.NewLine + "  Advice: Required permission code :16071", "Reset Audit Job - Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (string.IsNullOrEmpty(txtLocation.Text))
                {
                    MessageBox.Show("Please select the location", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLocation.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtJobNo.Text))
                {
                    MessageBox.Show("Please enter the job no", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtJobNo.Focus();
                    return;
                }

                string _returnMessage = null;
                int _effects = CHNLSVC.Inventory.UpdateAuditHeaderDetails(BaseCls.GlbUserComCode, txtLocation.Text.Trim(), txtJobNo.Text.Trim(), BaseCls.GlbUserID, 1, BaseCls.GlbUserSessionID, out _returnMessage);
                if (_effects > 0)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Job # - " + txtJobNo.Text.Trim() + " has been reset", "Stock Verification-Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadJobDetails();
                }
                else if (!string.IsNullOrEmpty(_returnMessage)) { MessageBox.Show("Job # - " + txtJobNo.Text + " couldn't reset due to an error" + Environment.NewLine + _returnMessage, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
                MessageBox.Show("Job # - " + txtJobNo.Text + " couldn't reset due to an error" + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<AuditJobSerial> UpdateRemarkedSerails(List<AuditJobSerial> _serials, List<AuditReportStatus> _noteList)
        {
            try
            {
                if ((_serials.Count > 0) && (_noteList.Count > 0))
                {
                    foreach (AuditReportStatus note in _noteList)
                    {
                        _serials.Where(x => x.Audjs_RefStatus == note.Aurs_main_cd).ToList().ForEach(x => x.Audjs_RefStatus = note.Aurs_desc);
                    }

                    //queryList.ForEach(x => x.Cells["colReasonStatus"].Value = string.IsNullOrEmpty(_serial.Audjs_RefStatus) ? string.Empty : _serial.Audjs_RefStatus);
                    //var _queryREsult = (from rmkSerials in _remarkedSerials
                    //                    join note in _stusList
                    //                        on rmkSerials.Audjs_RefStatus equals note.Aurs_code
                    //                    select new { rmkSerials.Audjs_ItemCode, rmkSerials.Audjs_ItemStatus, rmkSerials.Audjs_SerialNo, rmkSerials.Audjs_PhysicallyAvailableSerial, rmkSerials.Audjs_WarrantyNo, Audjs_RefStatus = note.Aurs_desc, rmkSerials.Audjs_RptType, rmkSerials.Audjs_SerialId }).ToList();

                    //BindingSource _serials = new BindingSource();
                    //_serials.DataSource = _queryREsult.Select(x => new { x.Audjs_ItemCode, x.Audjs_ItemStatus, x.Audjs_SerialNo, x.Audjs_PhysicallyAvailableSerial, x.Audjs_WarrantyNo, x.Audjs_RefStatus, x.Audjs_RptType, x.Audjs_SerialId }).ToList();
                    //gvRmk.DataSource = _serials;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _serials;
        }

        private void cmbSerialPageCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!IsNumeric(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }

        private void chkViewNonSerials_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkViewNonSerials.Checked)
                {
                    //hide un-commented rows
                    if (dgvProcessedSerials.Rows.Count > 0)
                    {
                        dgvProcessedSerials.CurrentCell = null;
                        foreach (DataGridViewRow _row in dgvProcessedSerials.Rows)
                        {
                            string _tmpSerial = _row.Cells["colSystemSerialId"].Value == null ? "N/A" : _row.Cells["colSystemSerialId"].Value.ToString().Trim().ToUpper();
                            if (_tmpSerial != "N/A")
                            {
                                dgvProcessedSerials.Rows[_row.Index].Visible = false;
                            }
                        }
                    }
                }
                else
                {
                    //un-hide all rows
                    if (dgvProcessedSerials.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow _row in dgvProcessedSerials.Rows)
                        {
                            dgvProcessedSerials.Rows[_row.Index].Visible = true;
                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool IsAllRemarksEntered(out string _controlNo)
        {
            if ((rmkPanel1.Visible) && (string.IsNullOrEmpty(txtRmk1.Text)))
            {
                _controlNo = lblRmk1.Text;
                return false;
            }

            if ((rmkPanel2.Visible) && (string.IsNullOrEmpty(txtRmk2.Text)))
            {
                _controlNo = lblRmk2.Text;
                return false;
            }

            if ((rmkPanel3.Visible) && (string.IsNullOrEmpty(txtRmk3.Text)))
            {
                _controlNo = lblRmk3.Text;
                return false;
            }

            if ((rmkPanel4.Visible) && (string.IsNullOrEmpty(txtRmk4.Text)))
            {
                _controlNo = lblRmk4.Text;
                return false;
            }

            if ((rmkPanel5.Visible) && (string.IsNullOrEmpty(txtRmk5.Text)))
            {
                _controlNo = lblRmk5.Text;
                return false;
            }

            if ((rmkPanel6.Visible) && (string.IsNullOrEmpty(txtRmk6.Text)))
            {
                _controlNo = lblRmk6.Text;
                return false;
            }

            if ((rmkPanel7.Visible) && (string.IsNullOrEmpty(txtRmk7.Text)))
            {
                _controlNo = lblRmk7.Text;
                return false;
            }

            if ((rmkPanel8.Visible) && (string.IsNullOrEmpty(txtRmk8.Text)))
            {
                _controlNo = lblRmk8.Text;
                return false;
            }

            if ((rmkPanel9.Visible) && (string.IsNullOrEmpty(txtRmk9.Text)))
            {
                _controlNo = lblRmk9.Text;
                return false;
            }

            if ((rmkPanel10.Visible) && (string.IsNullOrEmpty(txtRmk10.Text)))
            {
                _controlNo = lblRmk10.Text;
                return false;
            }

            if ((rmkPanel11.Visible) && ((cmbRmk11.SelectedValue == null) && (string.IsNullOrEmpty(cmbRmk11.Text))))
            {
                _controlNo = lblRmk1.Text;
                return false;
            }

            _controlNo = string.Empty;
            return true;
        }

        private void txtRmk9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((cmbRmk.SelectedValue.ToString() == "NOTE 04") || (cmbRmk.SelectedValue.ToString() == "NOTE 05") || (cmbRmk.SelectedValue.ToString() == "NOTE 06"))
            {
                if (!IsNumeric(e.KeyChar.ToString()))
                {
                    e.Handled = true;
                }

                if ((e.KeyChar.ToString() == ".") && (!txtRmk9.Text.Contains(".")))
                {
                    e.Handled = false;
                }

                if (e.KeyChar.ToString() == "\b")
                {
                    e.Handled = false;
                }
            }
        }

        private void txtRmk9_Leave(object sender, EventArgs e)
        {
            if ((cmbRmk.SelectedValue.ToString() == "NOTE 04") || (cmbRmk.SelectedValue.ToString() == "NOTE 05") || (cmbRmk.SelectedValue.ToString() == "NOTE 06"))
            {
                if (!string.IsNullOrEmpty(txtRmk9.Text))
                {
                    txtRmk9.Text = (Convert.ToDouble(txtRmk9.Text)).ToString("N2");
                }
            }
        }

        private void btnRptPrint_Click(object sender, EventArgs e)
        {
            if (rbtLastNoSeqRpt.Checked) // last number sequnce report by location
            {
                if (string.IsNullOrEmpty(txtLocation.Text))
                {
                    MessageBox.Show("Please select a location", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtLocation.Focus();
                    return;
                }

                Reports.Reconciliation.Reconcile_Rep _reconRep = new Reports.Reconciliation.Reconcile_Rep();
                _reconRep.CheckPermission = false;
                _reconRep.opt3.Checked = true;
                _reconRep.txtPC.Text = txtLocation.Text.ToUpper().Trim();
                _reconRep._isProcessed = false;
                if (chk.Checked == true)
                    _reconRep._isProcessed = true;
                _reconRep.btnDisplay_Click(null, null);
            }
            else if (rbtLastNoPcRpt.Checked)// last number sequnce report by profit center
            {
                if (string.IsNullOrEmpty(txtLocation.Text))
                {
                    MessageBox.Show("Please select a location", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtLocation.Focus();
                    return;
                }

                DataTable _profitCenterDetails = new DataTable();
                _profitCenterDetails = CHNLSVC.Sales.GetLocationCode(BaseCls.GlbUserComCode, txtLocation.Text.ToUpper().Trim());
                if (_profitCenterDetails.Rows.Count > 0)
                {
                    string _tmpProfitCenter = _profitCenterDetails.Rows[0]["ml_def_pc"] == DBNull.Value ? string.Empty : _profitCenterDetails.Rows[0]["ml_def_pc"].ToString().ToUpper();

                    if ((string.IsNullOrEmpty(_tmpProfitCenter)) || (_tmpProfitCenter == "N/A"))
                    {
                        MessageBox.Show("Profitcenter hasn't setuped for selected location", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //txtLocation.Focus();
                        return;
                    }

                    Reports.Reconciliation.Reconcile_Rep _reconRep = new Reports.Reconciliation.Reconcile_Rep();
                    _reconRep.CheckPermission = false;
                    _reconRep.opt2.Checked = true;
                    _reconRep.txtPC.Text = _tmpProfitCenter;
                    _reconRep._isProcessed = false;
                    if (chk.Checked == true)
                    {
                        _reconRep._isProcessed = true;
                        _reconRep.txtPC.Text = txtLocation.Text;    //kapila 25/5/2016
                    }
                    _reconRep.btnDisplay_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Profitcenter details not found for selected location", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtLocation.Focus();
                    return;
                }
            }
            else if (rbtCurrInvBalance.Checked)
            {
                Reports.Inventory.Inv_Rep _invRep = new Reports.Inventory.Inv_Rep();
                _invRep.CheckPermission = false;
                _invRep.opt9.Checked = true;
                _invRep.txtPC.Text = txtLocation.Text.ToUpper().Trim();
                _invRep.btnDisplay_Click(null, null);
            }
            else if (rbtFixedAssetsRpt.Checked)
            {
                Reports.Audit.Audit_Rep _auditRep = new Reports.Audit.Audit_Rep();
                _auditRep.CheckPermission = false;
                _auditRep.opt13.Checked = true;
                _auditRep.txtPC.Text = txtLocation.Text.ToUpper().Trim();
                _auditRep.btnDisplay_Click(null, null);
            }
            else if (rbtAodOutRpt.Checked)
            {
                Reports.Audit.Audit_Rep _auditRep = new Reports.Audit.Audit_Rep();
                _auditRep.CheckPermission = false;
                _auditRep.opt12.Checked = true;
                _auditRep.txtPC.Text = txtLocation.Text.ToUpper().Trim();
                _auditRep._isProcessed = false;
                if (chk.Checked == true)
                    _auditRep._isProcessed = true;
                _auditRep.btnDisplay_Click(null, null);
            }
            else if (rbtPendingDelivery.Checked)
            {
                DataTable _profitCenterDetails = new DataTable();
                _profitCenterDetails = CHNLSVC.Sales.GetLocationCode(BaseCls.GlbUserComCode, txtLocation.Text.ToUpper().Trim());
                if (_profitCenterDetails.Rows.Count > 0)
                {
                    string _tmpProfitCenter = _profitCenterDetails.Rows[0]["ml_def_pc"] == DBNull.Value ? string.Empty : _profitCenterDetails.Rows[0]["ml_def_pc"].ToString().ToUpper();

                    if ((string.IsNullOrEmpty(_tmpProfitCenter)) || (_tmpProfitCenter == "N/A"))
                    {
                        MessageBox.Show("Profitcenter hasn't setuped for selected location", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //txtLocation.Focus();
                        return;
                    }

                    Reports.Audit.Audit_Rep _auditRep = new Reports.Audit.Audit_Rep();
                    _auditRep.CheckPermission = false;
                    _auditRep.opt18.Checked = true;
                    _auditRep.txtPC.Text = _tmpProfitCenter; // pass profit center related to selected location
                    _auditRep._isProcessed = false;
                    if (chk.Checked == true)
                        _auditRep._isProcessed = true;
                    _auditRep.btnDisplay_Click(null, null);
                }

                else
                {
                    MessageBox.Show("Profitcenter details not found for selected location", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtLocation.Focus();
                    return;
                }
                //commented by akila 2017/05/24
                //Reports.Audit.Audit_Rep _auditRep = new Reports.Audit.Audit_Rep();
                //_auditRep.CheckPermission = false;
                //_auditRep.opt18.Checked = true;
                //_auditRep.txtPC.Text = txtLocation.Text.ToUpper().Trim();
                //_auditRep._isProcessed = false;
                //if (chk.Checked == true)
                //    _auditRep._isProcessed = true;
                //_auditRep.btnDisplay_Click(null, null);
            }
            else if (rbtStockSignature.Checked)
            {
                if (string.IsNullOrEmpty(txtJobNo.Text))
                {
                    MessageBox.Show("Please enter the job no", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtJobNo.Focus();
                    return;
                }

                if (lblJobStatus.Text != "FINISHED")
                {
                    MessageBox.Show("Selected job hasn't finalized ", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rbtStockSignature.Focus();
                    return;
                }

                Reports.Audit.Audit_Rep _auditRep = new Reports.Audit.Audit_Rep();
                _auditRep.CheckPermission = false;
                _auditRep.opt26.Checked = true;
                _auditRep.chkjobno.Checked = false;
                _auditRep.txtjobno.Text = txtJobNo.Text;
                _auditRep.txtPC.Text = txtLocation.Text.ToUpper().Trim();
                _auditRep.btnDisplay_Click(null, null);
            }
            else if (rdirccreport.Checked)//add  by tharanga 2018/05/15
            {
                DataTable _profitCenterDetails = new DataTable();
                _profitCenterDetails = CHNLSVC.Sales.GetLocationCode(BaseCls.GlbUserComCode, txtLocation.Text.ToUpper().Trim());
                if (_profitCenterDetails.Rows.Count > 0)
                {
                    string _tmpProfitCenter = _profitCenterDetails.Rows[0]["ml_def_pc"] == DBNull.Value ? string.Empty : _profitCenterDetails.Rows[0]["ml_def_pc"].ToString().ToUpper();

                    if ((string.IsNullOrEmpty(_tmpProfitCenter)) || (_tmpProfitCenter == "N/A"))
                    {
                        MessageBox.Show("Profitcenter hasn't setuped for selected location", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //txtLocation.Focus();
                        return;
                    }
                    BaseCls.GlbReportName = "RCCReport.rpt";
                    Reports.Service.Service_Rep _Service_Rep = new Reports.Service.Service_Rep();
                    _Service_Rep.txtPC.Text = _tmpProfitCenter;
                    _Service_Rep.txtToDate.Text = Convert.ToString(DateTime.Now.Date);
                    _Service_Rep.txtFromDate.Text = "2000/01/01";
                    _Service_Rep.opt5.Checked = true;
                    _Service_Rep.btnDisplay_Click_1(null, null);

                }
            }

        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            //rbtLastNoSeqRpt.Focus();            
            pnlReports.Size = new Size(263, 230);
            pnlReports.BringToFront();
            pnlReports.Enabled = true;
            pnlReports.Visible = true;
            // grbSubJobSelection.Enabled = false;
        }

        private void btnCloseRptPanel_Click(object sender, EventArgs e)
        {
            pnlReports.Visible = false;
            //grbSubJobSelection.Enabled = true;
        }

        private void GenarateStockSignatureReport()
        {
            string _returnMessage = string.Empty;
            try
            {
                Reports.Audit.Audit_Rep _auditRep = new Reports.Audit.Audit_Rep();
                _auditRep.CheckPermission = false;
                _auditRep.opt26.Checked = true;
                _auditRep.chkjobno.Checked = false;
                _auditRep.txtjobno.Text = txtJobNo.Text;
                _auditRep.txtPC.Text = txtLocation.Text.ToUpper().Trim();
                _auditRep.btnDisplay_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while generating Stock Signature Report" + Environment.NewLine + ex.Message, "Stock Verfication - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateCurrentInvBalceReport()
        {
            string _returnMessage = string.Empty;
            try
            {
                Reports.Inventory.Inv_Rep _invRep = new Reports.Inventory.Inv_Rep();
                _invRep.CheckPermission = false;
                _invRep.opt9.Checked = true;
                _invRep.txtPC.Text = txtLocation.Text.ToUpper().Trim();
                _invRep.btnDisplay_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while generating Current Inventory Balance Report" + Environment.NewLine + ex.Message, "Stock Verfication - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtEpfNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNicNo.Focus();
            }
        }

        private void txtNicNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMemberName.Focus();
            }
        }

        private void txtMemberName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddMember.Focus();
            }
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            try
            {
                if ((string.IsNullOrEmpty(txtEpfNo.Text)) || (string.IsNullOrWhiteSpace(txtEpfNo.Text)))
                {
                    MessageBox.Show("Please enter EPF No", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEpfNo.Focus();
                    return;
                }

                if ((string.IsNullOrEmpty(txtNicNo.Text)) || (string.IsNullOrWhiteSpace(txtNicNo.Text)))
                {
                    MessageBox.Show("Please enter NIC No", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNicNo.Focus();
                    return;
                }

                if ((string.IsNullOrEmpty(txtMemberName.Text)) || (string.IsNullOrWhiteSpace(txtMemberName.Text)))
                {
                    MessageBox.Show("Please enter member name", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMemberName.Focus();
                    return;
                }


                if ((AuditMemberList != null) && (AuditMemberList.Count > 0))
                {
                    int _tmpCount = 0;

                    //validate team lead
                    if (chkTeamLead.Checked)
                    {
                        _tmpCount = AuditMemberList.Where(x => x.Ajm_Mem_type == "TEAM LEAD").Select(x => x.Ajm_Mem_Id).Count();
                        if (_tmpCount >= 1)
                        {
                            MessageBox.Show("Invalid team lead. Can have only one team leader", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            chkTeamLead.Checked = false;
                            chkTeamLead.Focus();
                            return;
                        }
                    }

                    //validate epf
                    _tmpCount = AuditMemberList.Where(x => x.Ajm_Mem_Id == txtEpfNo.Text.Trim()).Select(x => x.Ajm_Mem_Id).Count();
                    if (_tmpCount >= 1)
                    {
                        MessageBox.Show("EPF No - " + txtEpfNo.Text.Trim() + " already exists!", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEpfNo.Focus();
                        return;
                    }

                    //validate nic
                    _tmpCount = AuditMemberList.Where(x => x.Ajm_Mem_Nic == txtNicNo.Text.Trim()).Select(x => x.Ajm_Mem_Id).Count();
                    if (_tmpCount >= 1)
                    {
                        MessageBox.Show("NIC No - " + txtNicNo.Text.Trim() + " already exists!", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtNicNo.Focus();
                        return;
                    }

                    //validate name
                    _tmpCount = AuditMemberList.Where(x => x.Ajm_Mem_Name == txtMemberName.Text.Trim()).Select(x => x.Ajm_Mem_Id).Count();
                    if (_tmpCount >= 1)
                    {
                        MessageBox.Show("Member name - " + txtMemberName.Text.Trim() + " already exists!", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMemberName.Focus();
                        return;
                    }
                }

                AuditMemebers _member = new AuditMemebers();
                _member.Ajm_Mem_Id = txtEpfNo.Text.Trim();
                _member.Ajm_Mem_Nic = txtNicNo.Text.Trim();
                _member.Ajm_Mem_Name = txtMemberName.Text.Trim();
                _member.Ajm_Mem_type = chkTeamLead.Checked == true ? "TEAM LEAD" : "MEMBER";
                _member.Ajm_Job_no = null;
                _member.Ajm_Cre_by = BaseCls.GlbUserID;

                if (chkTeamLead.Checked)
                {
                    _supervisorCode = txtEpfNo.Text.Trim();
                }

                AuditMemberList.Add(_member);

                if ((AuditMemberList != null) && (AuditMemberList.Count > 0))
                {
                    BindingSource _memberSource = new BindingSource();
                    _memberSource.DataSource = AuditMemberList.Select(x => new { x.Ajm_Mem_Name, x.Ajm_Mem_Id, x.Ajm_Mem_Nic, x.Ajm_Mem_type }).ToList();
                    dgvMemberList.Rows.Clear();
                    dgvMemberList.DataSource = _memberSource;
                }

                //if (dgvMemberList.Rows.Count > 0)
                //{
                //    dgvMemberList.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["colEpfNo"].Value.ToString() == txtEmp.Text.Trim()).ToList().ForEach(x => x.Cells["colMemberChecked"].Value = true);
                //}

                txtEpfNo.Text = null;
                txtNicNo.Text = null;
                txtMemberName.Text = null;
                chkTeamLead.Checked = false;
                txtEpfNo.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding member details" + Environment.NewLine + ex.Message, "Stock Verfication - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dgvMemberList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult _result = MessageBox.Show("Do you want to remove selected record ?", "Stock Verfication - Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (_result == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            else
            {
                AuditMemberList.RemoveAt(e.RowIndex);
                if ((AuditMemberList != null) && (AuditMemberList.Count > 0))
                {
                    BindingSource _memberSource = new BindingSource();
                    _memberSource.DataSource = AuditMemberList.Select(x => new { x.Ajm_Mem_Name, x.Ajm_Mem_Id, x.Ajm_Mem_Nic, x.Ajm_Mem_type }).ToList();
                    dgvMemberList.Rows.Clear();
                    dgvMemberList.DataSource = _memberSource;
                }
                else
                {
                    dgvMemberList.Rows.Clear();
                }

            }
        }

        private void rbtAodOutRpt_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtAodOutRpt.Checked == true)
            {
                if (CHNLSVC.Financial.IsPrvDayTxnsFound_DO(BaseCls.GlbUserComCode, txtLocation.Text, DateTime.Now.Date) == true)
                    chk.Checked = false;
                else
                    chk.Checked = true;
            }
        }

        private void rbtPendingDelivery_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtPendingDelivery.Checked == true)
            {
                DataTable _profitCenterDetails = new DataTable();
                string _tmpProfitCenter = "";
                _profitCenterDetails = CHNLSVC.Sales.GetLocationCode(BaseCls.GlbUserComCode, txtLocation.Text.ToUpper().Trim());
                if (_profitCenterDetails.Rows.Count > 0)
                {
                    _tmpProfitCenter = _profitCenterDetails.Rows[0]["ml_def_pc"] == DBNull.Value ? string.Empty : _profitCenterDetails.Rows[0]["ml_def_pc"].ToString().ToUpper();

                }
                else
                {
                    MessageBox.Show("Profitcenter details not found for selected location", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtLocation.Focus();
                    return;
                }
                if (CHNLSVC.Financial.IsPrvDayTxnsFound_Sale_DO(BaseCls.GlbUserComCode, _tmpProfitCenter, txtLocation.Text, DateTime.Now.Date) == true)
                    chk.Checked = false;
                else
                    chk.Checked = true;
            }
        }

        private void rbtLastNoSeqRpt_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtLastNoSeqRpt.Checked == true)
            {
                if (CHNLSVC.Financial.IsPrvDayTxnsFound_DO_Rcc(BaseCls.GlbUserComCode, txtLocation.Text, DateTime.Now.Date) == true)
                    chk.Checked = false;
                else
                    chk.Checked = true;
            }
        }

        private void rbtLastNoPcRpt_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtLastNoPcRpt.Checked == true)
            {
                DataTable _profitCenterDetails = new DataTable();
                string _tmpProfitCenter = "";
                _profitCenterDetails = CHNLSVC.Sales.GetLocationCode(BaseCls.GlbUserComCode, txtLocation.Text.ToUpper().Trim());
                if (_profitCenterDetails.Rows.Count > 0)
                {
                    _tmpProfitCenter = _profitCenterDetails.Rows[0]["ml_def_pc"] == DBNull.Value ? string.Empty : _profitCenterDetails.Rows[0]["ml_def_pc"].ToString().ToUpper();

                }
                else
                {
                    MessageBox.Show("Profitcenter details not found for selected location", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtLocation.Focus();
                    return;
                }
                if (CHNLSVC.Financial.IsPrvDayTxnsFound_Sale_Rec(BaseCls.GlbUserComCode, _tmpProfitCenter, DateTime.Now.Date) == true)
                    chk.Checked = false;
                else
                    chk.Checked = true;
            }
        }

        private void txtUploadItem_Leave(object sender, EventArgs e)
        {
            if (chkIsExcessItem.Checked)
            {
                if ((!string.IsNullOrEmpty(txtCommonSerialField.Text)) && (!string.IsNullOrEmpty(txtUploadItem.Text)))
                {
                    string _tmpSerial = txtCommonSerialField.Text.Trim().ToUpper();
                    if (_tmpSerial != "N/A")
                    {
                        DataTable tmpSerialDetails = new DataTable();
                        string _tmpItemCode = string.IsNullOrEmpty(txtUploadItem.Text) ? null : txtUploadItem.Text.ToUpper().Trim(); // add by akila 2017/05/25
                        tmpSerialDetails = CHNLSVC.Inventory.GetSerialDetailsBySerial(BaseCls.GlbUserComCode, txtLocation.Text.Trim().ToUpper(), _tmpItemCode, _tmpSerial);
                        if (tmpSerialDetails.Rows.Count > 0)
                        {
                            MessageBox.Show("Invalid Serial #" + Environment.NewLine + "Serial # - " + _tmpSerial + " all ready exist in current location", "Stock Verification-Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCommonSerialField.Text = string.Empty;
                            txtCommonSerialField.Focus();
                            return;
                        }
                    }
                }
            }
        }

        private void btnexlupload_Click(object sender, EventArgs e)
        {
            lblexle_parth.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            lblexle_parth.Text = _obj[_obj.Length - 1].ToString();
            lblexle_parth.Text = openFileDialog1.FileName;

            DialogResult _dialog = MessageBox.Show("Do you want to save the current job ?", "Stock Verification - Save Jobs", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (_dialog == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            excleupload();

        }
        private void excleupload() //Tharanga 2017/07/03
        {
            int rowsAffected = 0;
            string _docNo = string.Empty;
            string _message = null;
            Base _basePage;
            string _msg = string.Empty;
            if (string.IsNullOrEmpty(lblexle_parth.Text))
            {
                MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblexle_parth.Text = string.Empty;
                lblexle_parth.Focus();
                return;
            }




            System.IO.FileInfo _fileObj = new System.IO.FileInfo(lblexle_parth.Text);




            if (_fileObj.Exists == false)
            {
                MessageBox.Show("Selected file does not exist at the following path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblexle_parth.Focus();
            }

            string Extension = _fileObj.Extension;

            string conStr = "";

            if (Extension.ToUpper() == ".XLS")
            {

                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                         .ConnectionString;
            }
            else if (Extension.ToUpper() == ".XLSX")
            {
                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                          .ConnectionString;

            }
            else
            {
                MessageBox.Show("Invalid upload file Format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }


            string _excelConnectionString = ConfigurationManager.ConnectionStrings[Extension == ".XLS" ? "ConStringExcel03" : "ConStringExcel07"].ConnectionString;
            _excelConnectionString = String.Format(conStr, lblexle_parth.Text, "YES");
            OleDbConnection _connExcel = new OleDbConnection(_excelConnectionString);
            OleDbCommand _cmdExcel = new OleDbCommand();
            OleDbDataAdapter _oda = new OleDbDataAdapter();
            DataTable _dt = new DataTable();
            _cmdExcel.Connection = _connExcel;
            int count = 0;
            try
            {
                _connExcel.Open();
            }
            catch (Exception)
            {

                MessageBox.Show("Excel file cannot be open during upload ! Please close it and process !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //Get the name of First Sheet


            DataTable _dtExcelSchema;
            _dtExcelSchema = _connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string _sheetName = _dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            _connExcel.Close();

            _connExcel.Open();
            _cmdExcel.CommandText = "SELECT * From [" + _sheetName + "]";
            _oda.SelectCommand = _cmdExcel;
            _oda.Fill(_dt);
            _connExcel.Close();
            List<PhysicalStockVerificationHdr> _PhysicalStockVerificationHdr = new List<PhysicalStockVerificationHdr>();
            List<AuditMemebers> AuditMemberList = new List<AuditMemebers>();

            StringBuilder _errorLst = new StringBuilder();
            if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            if (_dt.Rows.Count > 0)
            {

                string com = string.Empty;
                string loc = string.Empty;
                DateTime fromdt = DateTime.Now.Date;
                DateTime todt = DateTime.Now.Date;
                _basePage = new Base();
                Int32 AUSH_RECORD_COUNT = 0;
                foreach (DataRow _dr in _dt.Rows)
                {
                    bool newhdr = false;
                    if ("" != _dr[0].ToString() && "" != _dr[1].ToString() && "" != _dr[2].ToString().ToUpper().Trim().ToString() && "" != _dr[3].ToString().ToUpper().Trim().ToString())
                    {
                        newhdr = true;
                        com = _dr[0].ToString().ToUpper().Trim().ToString();
                        loc = _dr[1].ToString().ToUpper().Trim().ToString();
                        fromdt = Convert.ToDateTime(_dr[2].ToString().ToUpper().Trim().ToString()).Date;
                        todt = Convert.ToDateTime(_dr[3].ToString().ToUpper().Trim().ToString()).Date;
                        DataTable odt = new DataTable();
                        if (com == "")
                        {
                            MessageBox.Show("Please check the company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (loc == "")
                        {
                            MessageBox.Show("Please check the Location", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        odt = CHNLSVC.Sales.GetLocationCode(BaseCls.GlbUserComCode, loc);
                        if (odt.Rows.Count <= 0)
                        {
                            MessageBox.Show("Please check the Location." + " " + loc, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (BaseCls.GlbUserComCode != com)
                        {
                            MessageBox.Show("Please check the company" + " " + com, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if ("" == _dr[2].ToString().ToUpper().Trim().ToString())
                        {
                            MessageBox.Show("Please check the FROM DATE", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if ("" == _dr[3].ToString().ToUpper().Trim().ToString())
                        {

                            MessageBox.Show("Please check the TO DATE", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if ("" == _dr[10].ToString().ToUpper().Trim().ToString())
                        {

                            MessageBox.Show("Please check the department", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        MasterDepartment _masterDept = null;
                        _masterDept = CHNLSVC.General.GetDeptByCode(_dr[10].ToString().ToUpper().Trim());

                        if (_masterDept == null)
                        {
                            MessageBox.Show("Please check the department " + " " + _dr[10].ToString().ToUpper().Trim(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        DataTable mst_movcatetp = new DataTable();
                        mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeMainTable(_dr[11].ToString().ToUpper().Trim(), "AUDT");
                        if (mst_movcatetp.Rows.Count <= 0)
                        {
                            MessageBox.Show("Please check the reason code." + " " + _dr[11].ToString().ToUpper().Trim(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                    }

                    if (newhdr == true)
                    {
                        AUSH_RECORD_COUNT++;
                        PhysicalStockVerificationHdr _PhysicalStockHdr = new PhysicalStockVerificationHdr();
                        _PhysicalStockHdr.AUSH_COM = _dr[0].ToString().ToUpper().Trim().ToString();
                        _PhysicalStockHdr.AUSH_CRE_BY = BaseCls.GlbUserID;
                        _PhysicalStockHdr.AUSH_DT = DateTime.Today;
                        _PhysicalStockHdr.AUSH_FRM_DT = Convert.ToDateTime(_dr[2].ToString().ToUpper().Trim().ToString());
                        _PhysicalStockHdr.AUSH_TO_DT = Convert.ToDateTime(_dr[3].ToString().ToUpper().Trim().ToString());
                        _PhysicalStockHdr.AUSH_JOB = string.Empty;
                        _PhysicalStockHdr.AUSH_LOC = _dr[1].ToString().ToUpper().Trim().ToString();
                        _PhysicalStockHdr.AUSH_MOD_BY = BaseCls.GlbUserID;
                        _PhysicalStockHdr.AUSH_REM = _dr[9].ToString().ToUpper().Trim().ToString();
                        _PhysicalStockHdr.AUSH_STUS = "P";
                        _PhysicalStockHdr.AUSH_NO_JOB = int.Parse(_dr[4].ToString().ToUpper().Trim());
                        _PhysicalStockHdr.AUSH_SUPVIS_BY = _dr[8].ToString().ToUpper().Trim().ToString();
                        _PhysicalStockHdr.AUSH_SESSION_ID = BaseCls.GlbUserSessionID;
                        _PhysicalStockHdr.IsNewJob = true;
                        _PhysicalStockHdr.AUSH_RECORD_COUNT = AUSH_RECORD_COUNT;
                        _PhysicalStockHdr.AUSH_DIPARTMENT = _dr[10].ToString().ToUpper().Trim().ToString();
                        _PhysicalStockHdr.AUSH_REASON = _dr[11].ToString().ToUpper().Trim().ToString();
                        _PhysicalStockVerificationHdr.Add(_PhysicalStockHdr);
                    }



                    AuditMemebers _member = new AuditMemebers();
                    _member.Ajm_Mem_Id = _dr[6].ToString().ToUpper().Trim().ToString();
                    _member.Ajm_Mem_Nic = _dr[7].ToString().ToUpper().Trim().ToString();
                    _member.Ajm_Mem_Name = _dr[8].ToString().ToUpper().Trim().ToString();
                    _member.Ajm_Mem_type = _dr[5].ToString().ToUpper().Trim().ToString();
                    _member.Ajm_Job_no = null;
                    _member.Ajm_Cre_by = BaseCls.GlbUserID;
                    _member.AJM_RECORD_COUNT = AUSH_RECORD_COUNT;
                    AuditMemberList.Add(_member);



                }

                rowsAffected = CHNLSVC.Inventory.SaveStockVerification_EXCLE(_PhysicalStockVerificationHdr, out _docNo, out _message, AuditMemberList);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Job # " + _docNo + " saved successfully", "Stock Verification-Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearUiData();
                    //txtJobNo.Text = _docNo;
                    //txtJobNo_Leave(null, null);

                }
                else
                {
                    MessageBox.Show("Couldn't save the job details " + Environment.NewLine + _message, "Stock Verification-Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

        }

        private void btnSearchSupervisor_Click(object sender, EventArgs e)
        {

        }

        private void btnSearchDept_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Department);
                DataTable _result = CHNLSVC.CommonSearch.Get_Departments(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDept;
                _CommonSearch.ShowDialog();

                txtDept.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        private void btnreason_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
                DataTable _result = CHNLSVC.CommonSearch.GetMovementDocSubTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSubType;
                _CommonSearch.ShowDialog();
                txtSubType.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkjobno_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkjobno.Checked == true)
            //{
            //    chkNewJob.Checked = false;
            //    btnSearchJob_Click(null, null);
            //    txtJobNo.Text = string.Empty;
            //    txtJobNo.Enabled = true;
            //    btnFinish.Visible = true;
            //    //btnEdit.Visible = true;
            //    btnSave.Visible = false;
            //    lblJobStatus.Text = null;
            //    btnReset.Visible = true;
            //    grbSupervisorSelection.Enabled = false;
            //}
            //else
            //{
            //    chkNewJob.Checked = false;
            //}
        }

        private void rdojob_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                if (rdojob.Checked == true)
                {

                    txtJobNo.Text = string.Empty;
                    txtJobNo.Enabled = false;
                    // btnEdit.Visible = false;
                    btnSave.Visible = true;
                    btnFinish.Visible = false;
                    lblJobStatus.Text = "NEW JOB";
                    btnReset.Visible = false;
                    grbSupervisorSelection.Enabled = true;
                }
                else
                {

                    txtJobNo.Text = string.Empty;
                    txtJobNo.Enabled = true;
                    btnFinish.Visible = true;
                    //btnEdit.Visible = true;
                    btnSave.Visible = false;
                    lblJobStatus.Text = null;
                    btnReset.Visible = true;
                    grbSupervisorSelection.Enabled = false;
                }
            }
            else
            {
                ClearUiData();
                chkNewJob.Checked = true;
            }
        }

        private void btn_proc_itm_charges_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_itm_charges.Visible = false;
                if (lblJobStatus.Text == "FINISHED")
                {
                    btn_updatecharges.Enabled = true;
                    btnapproved.Enabled = true;
                    btnreqadj.Enabled = true;
                    btnprocess_itm_charges.Enabled = false;
                }
                if (lblJobStatus.Text == "PENDING")
                {
                    btnprocess_itm_charges.Enabled = true;

                }
                if (lblJobStatus.Text == "CANCELED")
                {
                    MessageBox.Show("This job has been canceled", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btn_proc_itm_charges.Focus();
                    return;

                }

                //pnlSerialInfo.Visible = true;
                //pnlSerialInfo.Size = new Size(820, 520);

                List<string> _selectedSubJobs = new List<string>();
                if (dgvSubJobList.Rows.Count > 0)
                {
                    foreach (DataGridViewRow _dataRow in dgvSubJobList.Rows)
                    {
                        bool _isRowSelected = _dataRow.Cells["chkColSelectJob"].Value == DBNull.Value ? false : Convert.ToBoolean(_dataRow.Cells["chkColSelectJob"].Value);
                        if (_isRowSelected)
                        {
                            _selectedSubJobs.Add(_dataRow.Cells["txtColSubJobNo"].Value.ToString());
                        }
                    }

                    if (_selectedSubJobs.Count > 0)
                    {
                        pnl_itm_charges.Visible = true;
                        pnl_itm_charges.Size = new Size(741, 431);
                        this.pnl_itm_charges.Location = new System.Drawing.Point(3, 0);
                        //Bind selected job numbers to drop down
                        BindingSource _cmbDataBind = new BindingSource();
                        _cmbDataBind.DataSource = _selectedSubJobs;
                        cmbSubJobSelection1.DataSource = _cmbDataBind;
                        cmbSubJobSelection1.SelectedIndex = 0;

                        //Get processed audit item details
                        _processedAuditJobItems = new List<AuditJobItem>();
                        _processedAuditJobItems = CHNLSVC.Inventory.GetProcessedJobItems(cmbSubJobSelection1.SelectedItem.ToString(), _processedItemsStartIndex, _processedItemsEndIndex);
                        if (_processedAuditJobItems.Count > 0)
                        {
                            BindItemDetails(_processedAuditJobItems);

                            //Get processed audit serial details
                            _AuditJobSerialcharges = new List<AuditJobSerial>();
                            _AuditJobSerialcharges = CHNLSVC.Inventory.GetProcessedJobSerials_all(cmbSubJobSelection1.SelectedItem.ToString(), _processedSerialStartIndex, _processedSerialEndIndex, 0, BaseCls.GlbUserComCode); //0 - All, 1- Mismatch, 2- Match
                            //_processedAuditJobSerials.Where(r => r.Audjs_RefStatus != "").ToList();
                            _AuditJobSerialcharges.RemoveAll(r => r.Audjs_RefStatus == "" || r.Audjs_RefStatus == null);
                            if (_AuditJobSerialcharges.Count == 0)
                            {
                                MessageBox.Show("There is no any chargeable stock discrepancy", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                lbltotamount.Text = "0.00";
                                return;
                            }
                            dgvitm_charges.AutoGenerateColumns = false;
                            dgvitm_charges.DataSource = _AuditJobSerialcharges;
                            // BindSerialDetails_with_Charges(_AuditJobSerialcharges);
                           
                            //Get audit reasons
                            _auditReasons = new List<AuditReportStatus>();
                            _auditReasons = GetAuditReason();
                            //BindAuditReasons_itm_charges();
                            load_itm_charges_summery(cmbSubJobSelection1.SelectedItem.ToString());
                            PhysicalStockVerificationHdr _PhysicalStockVerificationHdr = new PhysicalStockVerificationHdr();
                            _PhysicalStockVerificationHdr = CHNLSVC.Inventory.GET_STOCKVERF_HDR(cmbSubJobSelection1.SelectedItem.ToString());
                            if (!string.IsNullOrEmpty(_PhysicalStockVerificationHdr.AUSH_JOB))
                            {
                                if (_PhysicalStockVerificationHdr.AUSH_CHARGES_APP == 1)
                                {
                                    btn_updatecharges.Enabled = false;
                                    btnapproved.Enabled = false;
                                    btnprocess_itm_charges.Enabled = false;

                                }
                                if (_PhysicalStockVerificationHdr.AUSH_ADJ_REQ_IS_SEND == 1)
                                {
                                    btnreqadj.Enabled = false;


                                }
                            }
                            // SetAuditItemSummery(cmbSubJobSelection1.SelectedItem.ToString());
                        }
                        else
                        {
                            MessageBox.Show("Selected job hasn't been processed", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            pnl_itm_charges.Visible = false;
                            dgvitm_charges.Focus();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a sub job number", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvSubJobList.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Sub job details not found. Please selecte correct job#", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtJobNo.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseAllChannels();
                this.Cursor = Cursors.Default;
                MessageBox.Show("An error occurred while searching processed job items." + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            pnl_itm_charges.Visible = false;
        }
        private void BindSerialDetails_with_Charges(List<AuditJobSerial> _serialList)
        {
            if (_serialList.Count > 0)
            {
                BindingSource _bindSource = new BindingSource();
                _bindSource.DataSource = _serialList.Select(x => new { x.Audjs_ItemCode, x.Audjs_ItemStatus, x.Audjs_SerialNo, x.Audjs_PhysicallyAvailableSerial, x.Audjs_InDocNo, x.Audjs_SerialId }).ToList();
                dgvitm_charges.DataSource = _bindSource;
            }
            else { dgvitm_charges.Rows.Clear(); }
        }

        private void BindPreviousAuditNotes_itm_charges()
        {
            try
            {
                if (dgvitm_charges.Rows.Count > 0)
                {
                    if (_AuditJobSerialcharges.Count > 0)
                    {
                        List<AuditJobSerial> _tmpSerialList = _AuditJobSerialcharges.Where(x => x.Audjs_RptType.ToString() != string.Empty || x.Audjs_Remark.ToString() != string.Empty).Select(x => x).ToList();
                        if (_tmpSerialList.Count > 0)
                        {
                            foreach (AuditJobSerial _serial in _tmpSerialList)
                            {
                                var queryList = dgvitm_charges.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["Audjs_SerialNo1"].Value == _serial.Audjs_SerialNo).ToList();
                                queryList.ForEach(x => x.Cells["colReason2"].Value = string.IsNullOrEmpty(_serial.Audjs_RefStatus) ? string.Empty : _serial.Audjs_RefStatus);
                                queryList.ForEach(x => x.Cells["colReasonStatus2"].Value = string.IsNullOrEmpty(_serial.Audjs_RefStatus) ? string.Empty : _serial.Audjs_RefStatus);
                                queryList.ForEach(x => x.Cells["colRemark2"].Value = string.IsNullOrEmpty(_serial.Audjs_Remark) ? string.Empty : _serial.Audjs_Remark);
                                queryList.ForEach(x => x.Cells["audjs_charges1"].Value = string.IsNullOrEmpty(Convert.ToString(_serial.Aud_charges)) ? "0.00" : Convert.ToString(_serial.Aud_charges));
                                queryList.ForEach(x => x.Cells["audjs_charges_user1"].Value = string.IsNullOrEmpty(Convert.ToString(_serial.AUDJS_CHARGES_USER)) ? "0.00" : Convert.ToString(_serial.AUDJS_CHARGES_USER));
                                dgvitm_charges.RefreshEdit();
                                //dgvProcessedSerials.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["colSystemSerialId"].Value == _serial.Audjs_SerialNo).ToList().ForEach(x => x.Cells["colReasonStatus"].Value = string.IsNullOrEmpty(_serial.Audjs_RefStatus) ? string.Empty : _serial.Audjs_RefStatus);
                                //dgvProcessedSerials.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["colSystemSerialId"].Value == _serial.Audjs_SerialNo).ToList().ForEach(x => x.Cells["colReason"].Value = string.IsNullOrEmpty(_serial.Audjs_RptType) ? string.Empty : _serial.Audjs_RptType);
                                //dgvProcessedSerials.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["colSystemSerialId"].Value == _serial.Audjs_SerialNo).ToList().ForEach(x => x.Cells["colRemark"].Value = string.IsNullOrEmpty(_serial.Audjs_Remark) ? string.Empty : _serial.Audjs_Remark);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btn_nxt_itmchrges_Click(object sender, EventArgs e)
        {
            try
            {

                _processedSerialStartIndex += _indexVariance;
                _processedSerialEndIndex += _indexVariance;
                _AuditJobSerialcharges = new List<AuditJobSerial>();
                _AuditJobSerialcharges = LoadSerialDetails(_processedSerialStartIndex, _processedSerialEndIndex);
                _AuditJobSerialcharges.RemoveAll(r => r.Audjs_RefStatus == "" || r.Audjs_RefStatus == null);
                //Bind details

                if (_AuditJobSerialcharges.Count > 0)
                {
                    if ((radioBtnAll.Checked) || (radioBtnMatch.Checked))
                    {
                        BindSerialDetails_with_Charges(_AuditJobSerialcharges);


                    }
                    else if (radioBtnMismatch.Checked)
                    {
                        BindSerialDetails_with_Charges(_AuditJobSerialcharges);
                        if ((_auditReasons.Count > 0) && (_AuditJobSerialcharges.Count > 0))
                        {
                            //  BindAuditReasons_itm_charges();
                        }
                    }
                }
                else
                {
                    _processedSerialStartIndex -= _indexVariance;
                    _processedSerialEndIndex -= _indexVariance;
                }
            }
            catch (Exception ex)
            {
                _processedSerialStartIndex -= _indexVariance;
                _processedSerialEndIndex -= _indexVariance;
                this.Cursor = Cursors.Default;
                MessageBox.Show("An error occurred while loading serial details " + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_prv_itm_charges_Click(object sender, EventArgs e)
        {
            try
            {


                if ((_processedSerialStartIndex - _indexVariance) > 0)
                {
                    _processedSerialStartIndex -= _indexVariance;
                    _processedSerialEndIndex -= _indexVariance;
                    _AuditJobSerialcharges = new List<AuditJobSerial>();
                    _AuditJobSerialcharges = LoadSerialDetails(_processedSerialStartIndex, _processedSerialEndIndex);
                    _AuditJobSerialcharges.RemoveAll(r => r.Audjs_RefStatus == "" || r.Audjs_RefStatus == null);
                    //Bind details

                    if (_AuditJobSerialcharges.Count > 0)
                    {
                        if ((radioBtnAll.Checked) || (radioBtnMatch.Checked))
                        {
                            BindSerialDetails_with_Charges(_AuditJobSerialcharges);

                        }
                        else if (radioBtnMismatch.Checked)
                        {
                            BindSerialDetails(_AuditJobSerialcharges);
                            if ((_auditReasons.Count > 0) && (_AuditJobSerialcharges.Count > 0))
                            {
                                //BindAuditReasons_itm_charges();

                            }


                        }
                    }
                    else
                    {
                        _processedSerialStartIndex += _indexVariance;
                        _processedSerialEndIndex += _indexVariance;
                    }
                }
            }
            catch (Exception ex)
            {
                _processedSerialStartIndex += _indexVariance;
                _processedSerialEndIndex += _indexVariance;
                this.Cursor = Cursors.Default;
                MessageBox.Show("An error occurred while loading serial details " + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnprocess_itm__charges_Click(object sender, EventArgs e)
        {

            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);

            if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;

            if (_AuditJobSerialcharges.Count > 0)
            {
                foreach (AuditJobSerial item in _AuditJobSerialcharges)
                {
                    decimal netamount = 0;
                    MasterItem _itmMasterItem = new MasterItem();
                    _itmMasterItem = CHNLSVC.General.GetItemMaster(item.Audjs_ItemCode);

                    List<AUD_CHARGES> _AUD_CHARGES = new List<AUD_CHARGES>();
                    _AUD_CHARGES = CHNLSVC.Inventory.get_AUD_CHARGES(BaseCls.GlbUserComCode, item.Audjs_RefStatus, null, item.Audjs_ItemStatus, null, null, null, 0, null, null, DateTime.Now.Date, DateTime.Now.Date, null, 1, null, item.Audjs_ItemCode, _itmMasterItem.Mi_cate_1, _itmMasterItem.Mi_brand);
                    if (_AUD_CHARGES.Count > 0 && _AUD_CHARGES != null)
                    {
                        foreach (AUD_CHARGES _AUD_CHARGES_ in _AUD_CHARGES)
                        {
                            #region AUD_CHARGES_.aud_charge_base_on =="Selling"
                            if (_AUD_CHARGES_.aud_charge_base_on == "Selling")
                            {
                                Decimal itm_charges = 0;
                                if (!string.IsNullOrEmpty(_AUD_CHARGES_.aud_price_book) && !string.IsNullOrEmpty(_AUD_CHARGES_.aud_p_level))
                                {

                                    List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
                                    _priceDetailRef = CHNLSVC.Sales.GetPrice_01(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null, _AUD_CHARGES_.aud_price_book, _AUD_CHARGES_.aud_p_level, null, item.Audjs_ItemCode, 1, DateTime.Now.Date);
                                    if (_priceDetailRef.Count > 0)
                                    {
                                        _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, _AUD_CHARGES_.aud_price_book, _AUD_CHARGES_.aud_p_level);
                                        if (_AUD_CHARGES_.aud_value == "V")
                                        {

                                            var _one = from _itm in _priceDetailRef
                                                       select _itm;
                                            int _priceType = 0;
                                            foreach (var _single in _one)
                                            {
                                                _priceType = _single.Sapd_price_type;
                                                PriceTypeRef _promotion = TakePromotion(_priceType);
                                                decimal UnitPrice = FigureRoundUp(TaxCalculation(item.Audjs_ItemCode, _AUD_CHARGES_.aud_itm_stus, Convert.ToDecimal(1), _priceBookLevelRef, _priceDetailRef.FirstOrDefault().Sapd_itm_price, Convert.ToDecimal(0), Convert.ToDecimal(0), false), true);
                                                netamount = CalculateItem(UnitPrice, 1, item.Audjs_ItemCode, _AUD_CHARGES_.aud_itm_stus);
                                            }
                                            _AuditJobSerialcharges.Where(r => r.Audjs_ItemCode == item.Audjs_ItemCode && r.Audjs_ItemStatus == _AUD_CHARGES_.aud_itm_stus && r.Audjs_RefStatus == _AUD_CHARGES_.aud_aud_tp).ToList().ForEach(y => y.Aud_charges = netamount); break;//_priceDetailRef.FirstOrDefault().Sapd_itm_price
                                        }
                                        if (_AUD_CHARGES_.aud_value == "P")
                                        {
                                            itm_charges = _priceDetailRef.FirstOrDefault().Sapd_itm_price * _AUD_CHARGES_.aud_chargers / 100;
                                            _AuditJobSerialcharges.Where(r => r.Audjs_ItemCode == item.Audjs_ItemCode && r.Audjs_ItemStatus == _AUD_CHARGES_.aud_itm_stus && r.Audjs_RefStatus == _AUD_CHARGES_.aud_aud_tp).ToList().ForEach(y => y.Aud_charges = itm_charges);
                                            break;
                                        }

                                    }
                                    else
                                    {
                                        this.Cursor = Cursors.Default;
                                        MessageBox.Show("Tax rates not setup for selected item code ( " + _AUD_CHARGES_.aud_item_cd + " ). Please contact Inventory Department.", "Item Tax", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }


                                }
                            }
                            #endregion

                            #region AUD_CHARGES_.aud_charge_base_on =="Revt_bal"
                            if (_AUD_CHARGES_.aud_charge_base_on == "Revt_bal")
                            {
                                DataTable results = CHNLSVC.Sales.GetRever_balance(item.Audjs_InDocNo);
                                if (results.Rows.Count > 0)
                                {
                                    netamount = results.Rows[0]["rvet BAmt"] == DBNull.Value ? 0 : Convert.ToDecimal(results.Rows[0]["rvet BAmt"].ToString());
                                    _AuditJobSerialcharges.Where(r => r.Audjs_ItemCode == item.Audjs_ItemCode && r.Audjs_ItemStatus == _AUD_CHARGES_.aud_itm_stus && r.Audjs_RefStatus == _AUD_CHARGES_.aud_aud_tp).ToList().ForEach(y => y.Aud_charges = netamount);

                                    break;
                                }

                            }
                            #endregion
                            #region _AUD_CHARGES_.aud_charge_base_on == "Sel+Pre"

                            if (_AUD_CHARGES_.aud_charge_base_on == "Sel+Pre")
                            {
                                Decimal itm_charges = 0;
                                Decimal fixed_val = 0;
                                if (!string.IsNullOrEmpty(_AUD_CHARGES_.aud_price_book) && !string.IsNullOrEmpty(_AUD_CHARGES_.aud_p_level))
                                {

                                    List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
                                    _priceDetailRef = CHNLSVC.Sales.GetPrice_01(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "CS", _AUD_CHARGES_.aud_price_book, _AUD_CHARGES_.aud_p_level, null, item.Audjs_ItemCode, 1, DateTime.Now.Date);
                                    if (_priceDetailRef.Count > 0)
                                    {
                                        _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, _AUD_CHARGES_.aud_price_book, _AUD_CHARGES_.aud_p_level);
                                        if (_AUD_CHARGES_.aud_value == "V")
                                        {
                                            fixed_val = _AUD_CHARGES_.aud_chargers;
                                            //var _one = from _itm in _priceDetailRef
                                            //           select _itm;
                                            //int _priceType = 0;
                                            //foreach (var _single in _one)
                                            //{
                                            //    _priceType = _single.Sapd_price_type;
                                            //    PriceTypeRef _promotion = TakePromotion(_priceType);
                                            //    //decimal UnitPrice = FigureRoundUp(TaxCalculation(item.Audjs_ItemCode, _AUD_CHARGES_.aud_itm_stus, Convert.ToDecimal(1), _priceBookLevelRef, _priceDetailRef.FirstOrDefault().Sapd_itm_price, Convert.ToDecimal(0), Convert.ToDecimal(0), false), true);
                                            //    //netamount = CalculateItem(UnitPrice, 1, item.Audjs_ItemCode, _AUD_CHARGES_.aud_itm_stus);
                                            //    fixed_val = _AUD_CHARGES_.aud_chargers;
                                            //}
                                            //AuditJobSerialcharges.Where(r => r.Audjs_ItemCode == item.Audjs_ItemCode && r.Audjs_ItemStatus == _AUD_CHARGES_.aud_itm_stus && r.Audjs_RefStatus == _AUD_CHARGES_.aud_aud_tp).ToList().ForEach(y => y.Aud_charges = netamount); break;//_priceDetailRef.FirstOrDefault().Sapd_itm_price
                                        }
                                        if (_AUD_CHARGES_.aud_value == "P")
                                        {
                                            var _one = from _itm in _priceDetailRef
                                                       select _itm;
                                            int _priceType = 0;
                                            foreach (var _single in _one)
                                            {
                                                _priceType = _single.Sapd_price_type;
                                                PriceTypeRef _promotion = TakePromotion(_priceType);
                                                decimal UnitPrice = FigureRoundUp(TaxCalculation(item.Audjs_ItemCode, _AUD_CHARGES_.aud_itm_stus, Convert.ToDecimal(1), _priceBookLevelRef, _priceDetailRef.FirstOrDefault().Sapd_itm_price, Convert.ToDecimal(0), Convert.ToDecimal(0), false), true);
                                                netamount = CalculateItem(UnitPrice, 1, item.Audjs_ItemCode, _AUD_CHARGES_.aud_itm_stus);
                                                netamount = netamount * _AUD_CHARGES_.aud_chargers / 100;
                                            }
                                        }
                                        itm_charges = _priceDetailRef.FirstOrDefault().Sapd_itm_price * _AUD_CHARGES_.aud_chargers / 100;
                                        _AuditJobSerialcharges.Where(r => r.Audjs_ItemCode == item.Audjs_ItemCode && r.Audjs_ItemStatus == _AUD_CHARGES_.aud_itm_stus && r.Audjs_RefStatus == _AUD_CHARGES_.aud_aud_tp).ToList().ForEach(y => y.Aud_charges = netamount + fixed_val);
                                        break;

                                    }
                                    else
                                    {
                                        this.Cursor = Cursors.Default;
                                        MessageBox.Show("Tax rates not setup for selected item code ( " + _AUD_CHARGES_.aud_item_cd + " ). Please contact Inventory Department.", "Item Tax", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }


                                }
                            }

                            #endregion
                            #region AUD_CHARGES_.aud_charge_base_on =="Pre-define"
                            if (_AUD_CHARGES_.aud_charge_base_on == "Pre-define")
                            {

                                _AuditJobSerialcharges.Where(r => r.Audjs_ItemCode == item.Audjs_ItemCode && r.Audjs_ItemStatus == _AUD_CHARGES_.aud_itm_stus && r.Audjs_RefStatus == _AUD_CHARGES_.aud_aud_tp).ToList().ForEach(y => y.Aud_charges = _AUD_CHARGES_.aud_chargers);
                                break;
                            }
                            #endregion
                        }
                    }
                }
            }

            #region update charges
            updateitmCharges(_AuditJobSerialcharges);
            //LoadAuditRemarkScreen(cmbSubJobSelection.SelectedItem.ToString(), _currentSerialId.ToString());
            btn_proc_itm_charges_Click(null, null);
            #endregion
        }

        private decimal FigureRoundUp(decimal value, bool _isFinal)
        {
            if (IsSaleFigureRoundUp && _isFinal) return RoundUpForPlace(Math.Round(value), 2);
            else return Math.Round(value, 2);
        }
        protected PriceTypeRef TakePromotion(Int32 _priceType)
        {
            List<PriceTypeRef> _type = CHNLSVC.Sales.GetAllPriceType(string.Empty);
            var _ptype = from _types in _type
                         where _types.Sarpt_indi == _priceType
                         select _types;
            PriceTypeRef _list = new PriceTypeRef();
            foreach (PriceTypeRef _ones in _ptype)
            {
                _list = _ones;
            }
            return _list;
        }
        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction)
        {
            decimal _returnValValue = 0;

            if (_priceBookLevelRef != null)
                if (_priceBookLevelRef.Sapl_vat_calc)
                {
                    bool _isVATInvoice = false;
                    List<MasterItemTax> _taxs = new List<MasterItemTax>();
                    if (_isTaxfaction == false)
                    {
                        if (_isStrucBaseTax == true)
                        {
                            MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                            _taxs = CHNLSVC.Sales.GetTax_strucbase(BaseCls.GlbUserComCode, _item, _status, null, null, _mstItem.Mi_anal1);
                        }
                        else
                            _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _status);
                    }
                    else
                    {
                        if (_isStrucBaseTax == true)
                        {
                            MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                            _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", _mstItem.Mi_anal1);
                        }
                        else
                            _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT");
                    }
                    var _Tax = from _itm in _taxs
                               select _itm;
                    foreach (MasterItemTax _one in _Tax)
                    {
                        if (_isTaxfaction == false)
                            if (_isStrucBaseTax == true)
                                _returnValValue = _pbUnitPrice;
                            else
                                _returnValValue += _pbUnitPrice * _one.Mict_tax_rate;
                        else
                            if (_isVATInvoice)
                            {
                                _discount = (_pbUnitPrice * _qty) * _disRate / 100;
                                _returnValValue += (((_pbUnitPrice - _discount / _qty) + _returnValValue) * _one.Mict_tax_rate / 100) * _qty;
                            }
                            else
                                _returnValValue += ((_pbUnitPrice + _returnValValue) * _one.Mict_tax_rate / 100) * _qty;
                    }

                }
                else
                {
                    _returnValValue = _pbUnitPrice;
                    if (_isTaxfaction) _returnValValue = 0;
                }


            return _returnValValue;
        }

        private decimal CalculateItem(Decimal _unitAamt, decimal _qty, string _itm, string _Status)
        {
            string txtUnitAmt = "";
            string txtTaxAmt = "";
            string txtLineTotAmt = "";
            txtUnitAmt = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(_unitAamt) * Convert.ToDecimal(_qty), true)));

            decimal _vatPortion = FigureRoundUp(TaxCalculation(_itm, _Status, _qty, _priceBookLevelRef, _unitAamt, 0, 0, true), true);
            txtTaxAmt = FormatToCurrency(Convert.ToString(_vatPortion));
            decimal _totalAmount = Convert.ToDecimal(_unitAamt) + Convert.ToDecimal(txtUnitAmt);
            decimal _disAmt = 0;
            _totalAmount = FigureRoundUp(_unitAamt + Convert.ToDecimal(txtTaxAmt) - _disAmt, true);
            txtLineTotAmt = FormatToCurrency(Convert.ToString(_totalAmount));
            return _totalAmount;
        }

        private void updateitmCharges(List<AuditJobSerial> _AuditJobSerialcharges)
        {
            try
            {
                List<AuditJobSerial> _auditNotes = _AuditJobSerialcharges;
                if (_auditNotes.Count > 0)
                {
                    string _returnMessage = null;
                    CHNLSVC.Inventory.updateAuditJobSerails_charhes(_auditNotes, out _returnMessage);
                    if (string.IsNullOrEmpty(_returnMessage))
                    {
                        this.Cursor = Cursors.Default;
                        _isSerialGridDataEdited = false;
                        MessageBox.Show("Audit Charges saved successfully", "Stock Verification - Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        throw new Exception(_returnMessage);
                    }
                }
                else
                {
                    _isSerialGridDataEdited = false;
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("No changes to save", "Stock Verification - Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_proc_itm_charges_Click(null, null);
                }
            }

            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
                MessageBox.Show("An error occurred while saving audit notes" + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvitm_charges_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                string Audjs_RefStatus = "";
                string Audjs_ItemStatus = "";
                string Audjs_ItemCode = "";

                //Audjs_RefStatus = dgvitm_charges.Rows[e.RowIndex].Cells["colReasonStatus1"].Value == null ? string.Empty : Convert.ToString(dgvitm_charges.Rows[e.RowIndex].Cells["colReasonStatus1"].Value);
                //Audjs_ItemStatus = dgvitm_charges.Rows[e.RowIndex].Cells["Audjs_ItemStatus"].Value == null ? string.Empty : Convert.ToString(dgvitm_charges.Rows[e.RowIndex].Cells["Audjs_ItemStatus"].Value);
                //Audjs_ItemCode = dgvitm_charges.Rows[e.RowIndex].Cells["Audjs_ItemCode"].Value == null ? string.Empty : Convert.ToString(dgvitm_charges.Rows[e.RowIndex].Cells["Audjs_ItemCode"].Value);
                //List<AUD_CHARGES> _AUD_CHARGES = new List<AUD_CHARGES>();
                //_AUD_CHARGES = CHNLSVC.Inventory.get_AUD_CHARGES(BaseCls.GlbUserComCode,Audjs_RefStatus, null,Audjs_ItemStatus, null, null, null, 0, null, null, DateTime.Now.Date, DateTime.Now.Date, null, 1, null, Audjs_ItemCode);



            }
        }

        private void dgvitm_charges_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnupdatecharges_Click(object sender, EventArgs e)
        {
            string Audjs_JobNo = "";
            List<AuditJobSerial> _AuditSercharges = new List<AuditJobSerial>();
            if (dgvSubJobList.Rows.Count > 0)
            {
                foreach (DataGridViewRow _dataRow in dgvSubJobList.Rows)
                {
                    bool _isRowSelected = _dataRow.Cells["chkColSelectJob"].Value == DBNull.Value ? false : Convert.ToBoolean(_dataRow.Cells["chkColSelectJob"].Value);
                    if (_isRowSelected)
                    {
                        Audjs_JobNo = _dataRow.Cells["txtColSubJobNo"].Value.ToString();
                    }
                }
            }
            foreach (DataGridViewRow _row in dgvitm_charges.Rows)
            {

                AuditJobSerial _AuditJobSerial = new AuditJobSerial();
                _AuditJobSerial.AUDJS_CHARGES_USER = Convert.ToDecimal(_row.Cells["AUDJS_CHARGES_USER"].Value.ToString());
                _AuditJobSerial.Audjs_ItemCode = _row.Cells["Audjs_ItemCode"].Value.ToString();
                _AuditJobSerial.Audjs_SerialId = Convert.ToInt32(_row.Cells["colSystemSerialId1"].Value.ToString());
                _AuditJobSerial.Audjs_SerialNo = _row.Cells["Audjs_SerialIdNO"].Value.ToString();
                _AuditJobSerial.Audjs_JobNo = _row.Cells[3].Value.ToString();
                _AuditJobSerial._IS_USER_charge_update = 1;
                _AuditSercharges.Add(_AuditJobSerial);


            }

            updateitmCharges(_AuditSercharges);

        }

        private void btnprocess_itm_charges_Click(object sender, EventArgs e)
        {
            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);

            if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;

            if (_AuditJobSerialcharges.Count > 0)
            {
                foreach (AuditJobSerial item in _AuditJobSerialcharges)
                {
                    if (item.Audjs_SerialNo == "201708281547")
                    //if (item.Audjs_ItemCode == "CWGWA1593")
                    {
                        int a = 0;
                    }
                    if (item.Audjs_RefStatus == "NOR")
                    {
                        int b = 0;
                    }
                    if (item.Audjs_ItemCode == "LGPH-G4FLPCVR")
                    {
                        int b = 0;
                    }

                    MasterItem _itmMasterItem = new MasterItem();
                    _itmMasterItem = CHNLSVC.General.GetItemMaster(item.Audjs_ItemCode);

                    decimal netamount = 0;
                    Boolean _ischarge = true;
                    List<AUD_CHARGES> _AUD_CHARGES = new List<AUD_CHARGES>();
                    _AUD_CHARGES = CHNLSVC.Inventory.get_AUD_CHARGES(BaseCls.GlbUserComCode, item.Audjs_RefStatus, null, item.Audjs_ItemStatus, null, null, null, 0, null, null, DateTime.Now.Date, DateTime.Now.Date, null, 1, null, item.Audjs_ItemCode, _itmMasterItem.Mi_cate_1, _itmMasterItem.Mi_brand);
                    if (_AUD_CHARGES.Count > 0 && _AUD_CHARGES != null)
                    {
                        foreach (AUD_CHARGES _AUD_CHARGES_ in _AUD_CHARGES)
                        {

                            #region resion type EXP
                            if (_AUD_CHARGES_.aud_aud_tp == "EXP")
                            {
                                List<AuditRemarkValue> _AuditRemarkValue = new List<AuditRemarkValue>();
                                _AuditRemarkValue = CHNLSVC.Inventory.GetPhicalStockRemark_FILTER(cmbSubJobSelection1.Text, 7, 0, item.Audjs_ItemCode, item.Audjs_ItemStatus, item.Audjs_SerialId, _AUD_CHARGES_.aud_aud_tp);
                                if (_AuditRemarkValue != null)
                                {
                                    foreach (AuditRemarkValue det in _AuditRemarkValue)
                                    {
                                        DataTable odata = CHNLSVC.Sales.getReqHdrByReqNo(null, null, det.Ausv_val);
                                        if (odata.Rows.Count > 0)
                                        {
                                            _ischarge = false;
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region resion type EXP
                            if (_AUD_CHARGES_.aud_aud_tp == "NOR")
                            {
                                _ischarge = false;
                            }

                            #endregion
                            #region resion type  DAD
                            if (_AUD_CHARGES_.aud_aud_tp == "DAD" && (_AUD_CHARGES_.aud_itm_stus == "AGE" || _AUD_CHARGES_.aud_itm_stus == "AGLP" ||
                                _AUD_CHARGES_.aud_itm_stus == "CLR" || _AUD_CHARGES_.aud_itm_stus == "CLRLP" || _AUD_CHARGES_.aud_itm_stus == "CONS" ||
                                _AUD_CHARGES_.aud_itm_stus == "DEMO" || _AUD_CHARGES_.aud_itm_stus == "DEMOLP" || _AUD_CHARGES_.aud_itm_stus == "DISP" ||
                                _AUD_CHARGES_.aud_itm_stus == "DISLP" || _AUD_CHARGES_.aud_itm_stus == "FIXCO" || _AUD_CHARGES_.aud_itm_stus == "FIXLP" ||
                                _AUD_CHARGES_.aud_itm_stus == "GOD" || _AUD_CHARGES_.aud_itm_stus == "GDLP" || _AUD_CHARGES_.aud_itm_stus == "GSDIS" ||
                                _AUD_CHARGES_.aud_itm_stus == "REDLP" || _AUD_CHARGES_.aud_itm_stus == "REDIS" || _AUD_CHARGES_.aud_itm_stus == "RVT" ||
                                _AUD_CHARGES_.aud_itm_stus == "RVTLP" || _AUD_CHARGES_.aud_itm_stus == "SRCON" || _AUD_CHARGES_.aud_itm_stus == "SCLP"))
                            {

                                DataTable odt = CHNLSVC.Inventory.Get_pending_DIN(item.Audjs_ItemCode, item.Audjs_SerialNo, item.Audjs_SerialId);
                                if (odt.Rows.Count > 0)
                                {
                                    _ischarge = false;
                                }

                            }
                            #endregion
                            #region resion PDO
                            if (_AUD_CHARGES_.aud_aud_tp == "PDO")
                            {
                                List<AuditRemarkValue> _AuditRemarkValue = new List<AuditRemarkValue>();
                                _AuditRemarkValue = CHNLSVC.Inventory.GetPhicalStockRemark_FILTER(cmbSubJobSelection1.Text, 7, 0, item.Audjs_ItemCode, item.Audjs_ItemStatus, item.Audjs_SerialId, _AUD_CHARGES_.aud_aud_tp);
                                if (_AuditRemarkValue != null)
                                {
                                    foreach (AuditRemarkValue det in _AuditRemarkValue)
                                    {

                                        List<InvoiceItem> _InvoiceItem = CHNLSVC.Sales.GetInvoiceItems(det.Ausv_val);
                                        if (_InvoiceItem != null)
                                        {
                                            int count = _InvoiceItem.Where(r => r.Sad_itm_cd == det.Ausv_itm).Count();
                                            if (count == 0)
                                            {
                                                MessageBox.Show("Selected Item " + item.Audjs_ItemCode + " not availeble in invoice " + det.Ausv_val, "Audit Charges", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                return;
                                            }
                                        }

                                    }
                                }
                            }
                            #endregion
                            #region revert duration
                            if (_AUD_CHARGES_.aud_aud_tp == "RVT")
                            {
                                InventoryHeader _invHdr = new InventoryHeader();
                                _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(item.Audjs_InDocNo);
                                if (_invHdr != null)
                                {
                                    DateTime sysdate = CHNLSVC.Security.GetServerDateTime().Date;
                                    DateTime docdate = Convert.ToDateTime(item.Audjs_DocInDate);
                                    int month = ((sysdate.Year - docdate.Year) * 12) + sysdate.Month - docdate.Month;
                                    if (month < 3)
                                    {
                                        _ischarge = false;
                                    }

                                }


                            }
                            #endregion
                            #region check RCC available
                            if (_AUD_CHARGES_.aud_aud_tp == "CUSTR")
                            {
                                Boolean _found = CHNLSVC.Inventory.IsRccSerialFound(item.Audjs_ItemCode, item.Audjs_SerialNo);
                                if (_found == true)
                                {
                                    _ischarge = false;
                                }
                            }
                            #endregion
                            #region check do doc and job date
                            //if (_AUD_CHARGES_.aud_aud_tp == "TBRCEX")
                            //{
                            //    DataTable odt = CHNLSVC.Inventory.Get_Delivery_det_by_ser(BaseCls.GlbUserComCode, item.Audjs_ItemCode, item.Audjs_SerialNo, BaseCls.GlbUserDefLoca, "DO");
                            //    if (odt.Rows.Count > 0)
                            //    {
                            //        _jobDetails = new DataTable();
                            //        _jobDetails = CHNLSVC.Inventory.GetAuditJobDetails(BaseCls.GlbUserComCode, txtLocation.Text.Trim(), txtJobNo.Text.Trim());
                            //        if (Convert.ToDateTime(_jobDetails.Rows[0]["Job Start Date"].ToString()) < Convert.ToDateTime(odt.Rows[0]["ITS_DOC_DT"].ToString()))
                            //        {
                            //            if (_AUD_CHARGES_.aud_charge_base_on == "Selling")
                            //            {
                            //                continue;
                            //            }
                            //        }
                            //    }
                            //    else
                            //    {
                            //        if (_AUD_CHARGES_.aud_charge_base_on == "Pre-define")
                            //        {
                            //            continue;
                            //        }

                            //    }
                            //}
                            #endregion

                            #region check do doc and job date
                            if (_AUD_CHARGES_.aud_aud_tp == "TRFWD")
                            {
                                //DataTable odt = CHNLSVC.Inventory.Get_Delivery_det_by_ser(BaseCls.GlbUserComCode, item.Audjs_ItemCode, item.Audjs_SerialNo, BaseCls.GlbUserDefLoca, "ADO");
                                //if (odt.Rows.Count > 0)
                                //{
                                //        if (_AUD_CHARGES_.aud_charge_base_on == "Selling")
                                //        {
                                //            continue;
                                //        }

                                //}
                                //else
                                //{
                                //    if (_AUD_CHARGES_.aud_charge_base_on == "Pre-define")
                                //    {
                                //        continue;
                                //    }

                                //}
                            }
                            if (_AUD_CHARGES_.aud_aud_tp == "UFA")
                            {
                                DataTable odata = CHNLSVC.Inventory.Get_get_fixed_asset(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, item.Audjs_SerialNo, item.Audjs_ItemCode);
                                if (odata.Rows.Count > 0)
                                {
                                    _AUD_CHARGES_.aud_charge_base_on = "Pre-define";
                                    _AUD_CHARGES_.aud_chargers = 2500;
                                }
                            }
                            #endregion

                            //if (item.Audjs_RefStatus == "IAR")
                            //{
                            //    DataTable odata = CHNLSVC.Financial.get_advance_dete(item.Audjs_ItemCode, BaseCls.GlbUserDefProf);
                            //}

                            if (_ischarge == true)
                            {


                                #region AUD_CHARGES_.aud_charge_base_on =="Selling"
                                if (_AUD_CHARGES_.aud_charge_base_on == "Selling")
                                {
                                    Decimal itm_charges = 0;
                                    if (!string.IsNullOrEmpty(_AUD_CHARGES_.aud_price_book) && !string.IsNullOrEmpty(_AUD_CHARGES_.aud_p_level))
                                    {

                                        List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
                                        _priceDetailRef = CHNLSVC.Sales.GetPrice_01(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null, _AUD_CHARGES_.aud_price_book.Trim(), _AUD_CHARGES_.aud_p_level.Trim(), null, item.Audjs_ItemCode, 1, DateTime.Now.Date);

                                        if (_priceDetailRef.Count > 0)
                                        {
                                            _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, _AUD_CHARGES_.aud_price_book, _AUD_CHARGES_.aud_p_level);
                                            if (_AUD_CHARGES_.aud_value == "V")
                                            {

                                                var _one = from _itm in _priceDetailRef
                                                           select _itm;
                                                int _priceType = 0;
                                                foreach (var _single in _one)
                                                {
                                                    _priceType = _single.Sapd_price_type;
                                                    PriceTypeRef _promotion = TakePromotion(_priceType);
                                                    decimal UnitPrice = FigureRoundUp(TaxCalculation(item.Audjs_ItemCode, _AUD_CHARGES_.aud_itm_stus, Convert.ToDecimal(1), _priceBookLevelRef, _priceDetailRef.FirstOrDefault().Sapd_itm_price, Convert.ToDecimal(0), Convert.ToDecimal(0), false), true);
                                                    netamount = Math.Round(CalculateItem(UnitPrice, 1, item.Audjs_ItemCode, _AUD_CHARGES_.aud_itm_stus), 0);
                                                    if (item.Audjs_RefStatus == "FLUE")
                                                    {
                                                        netamount = netamount * -1;
                                                    }
                                                }
                                                _AuditJobSerialcharges.Where(r => r.Audjs_SerialId == item.Audjs_SerialId && r.Audjs_ItemCode == item.Audjs_ItemCode && r.Audjs_ItemStatus == _AUD_CHARGES_.aud_itm_stus && r.Audjs_RefStatus == _AUD_CHARGES_.aud_aud_tp).ToList().ForEach(y => y.Aud_charges = netamount); break;//_priceDetailRef.FirstOrDefault().Sapd_itm_price
                                            }
                                            if (_AUD_CHARGES_.aud_value == "P")
                                            {
                                                var _one = from _itm in _priceDetailRef
                                                           select _itm;
                                                int _priceType = 0;
                                                foreach (var _single in _one)
                                                {
                                                    _priceType = _single.Sapd_price_type;
                                                    PriceTypeRef _promotion = TakePromotion(_priceType);
                                                    decimal UnitPrice = FigureRoundUp(TaxCalculation(item.Audjs_ItemCode, _AUD_CHARGES_.aud_itm_stus, Convert.ToDecimal(1), _priceBookLevelRef, _priceDetailRef.FirstOrDefault().Sapd_itm_price, Convert.ToDecimal(0), Convert.ToDecimal(0), false), true);
                                                    netamount = netamount = Math.Round(CalculateItem(UnitPrice, 1, item.Audjs_ItemCode, _AUD_CHARGES_.aud_itm_stus), 0);
                                                }

                                                itm_charges = netamount * _AUD_CHARGES_.aud_chargers / 100;
                                                //itm_charges = _priceDetailRef.FirstOrDefault().Sapd_itm_price * _AUD_CHARGES_.aud_chargers / 100;
                                                _AuditJobSerialcharges.Where(r => r.Audjs_SerialId == item.Audjs_SerialId && r.Audjs_ItemCode == item.Audjs_ItemCode && r.Audjs_ItemStatus == _AUD_CHARGES_.aud_itm_stus && r.Audjs_RefStatus == _AUD_CHARGES_.aud_aud_tp).ToList().ForEach(y => y.Aud_charges = itm_charges);
                                                break;
                                            }

                                        }
                                        else
                                        {
                                            _AuditJobSerialcharges.Where(r => r.Audjs_SerialId == item.Audjs_SerialId && r.Audjs_ItemCode == item.Audjs_ItemCode && r.Audjs_ItemStatus == _AUD_CHARGES_.aud_itm_stus && r.Audjs_RefStatus == _AUD_CHARGES_.aud_aud_tp).ToList().ForEach(y => y.Aud_charges = itm_charges);
                                            break;

                                            //this.Cursor = Cursors.Default;
                                            //MessageBox.Show("Tax rates not setup for selected item code ( " + item.Audjs_ItemCode + " ). Please contact Inventory Department.", "Item Tax", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            //return;
                                        }


                                    }
                                }
                                #endregion
                                #region AUD_CHARGES_.aud_charge_base_on =="Revt_bal"
                                if (_AUD_CHARGES_.aud_charge_base_on == "Revt_bal")
                                {
                                    DataTable results = CHNLSVC.Sales.GetRever_balance(item.Audjs_InDocNo);
                                    if (results.Rows.Count > 0)
                                    {
                                        netamount = results.Rows[0]["Rls Amt"] == DBNull.Value ? 0 : Convert.ToDecimal(results.Rows[0]["Rls Amt"].ToString());
                                        _AuditJobSerialcharges.Where(r => r.Audjs_SerialId == item.Audjs_SerialId && r.Audjs_ItemCode == item.Audjs_ItemCode && r.Audjs_ItemStatus == _AUD_CHARGES_.aud_itm_stus && r.Audjs_RefStatus == _AUD_CHARGES_.aud_aud_tp).ToList().ForEach(y => y.Aud_charges = netamount);

                                        break;
                                    }

                                }
                                #endregion
                                #region _AUD_CHARGES_.aud_charge_base_on == "Sel+Pre"

                                if (_AUD_CHARGES_.aud_charge_base_on == "Sel+Pre")
                                {
                                    Decimal itm_charges = 0;
                                    Decimal fixed_val = 0;
                                    if (!string.IsNullOrEmpty(_AUD_CHARGES_.aud_price_book) && !string.IsNullOrEmpty(_AUD_CHARGES_.aud_p_level))
                                    {

                                        List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
                                        _priceDetailRef = CHNLSVC.Sales.GetPrice_01(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "CS", _AUD_CHARGES_.aud_price_book, _AUD_CHARGES_.aud_p_level, null, item.Audjs_ItemCode, 1, DateTime.Now.Date);
                                        if (_priceDetailRef.Count > 0)
                                        {
                                            _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, _AUD_CHARGES_.aud_price_book, _AUD_CHARGES_.aud_p_level);
                                            if (_AUD_CHARGES_.aud_value == "V")
                                            {
                                                fixed_val = _AUD_CHARGES_.aud_chargers;
                                                //var _one = from _itm in _priceDetailRef
                                                //           select _itm;
                                                //int _priceType = 0;
                                                //foreach (var _single in _one)
                                                //{
                                                //    _priceType = _single.Sapd_price_type;
                                                //    PriceTypeRef _promotion = TakePromotion(_priceType);
                                                //    //decimal UnitPrice = FigureRoundUp(TaxCalculation(item.Audjs_ItemCode, _AUD_CHARGES_.aud_itm_stus, Convert.ToDecimal(1), _priceBookLevelRef, _priceDetailRef.FirstOrDefault().Sapd_itm_price, Convert.ToDecimal(0), Convert.ToDecimal(0), false), true);
                                                //    //netamount = CalculateItem(UnitPrice, 1, item.Audjs_ItemCode, _AUD_CHARGES_.aud_itm_stus);
                                                //    fixed_val = _AUD_CHARGES_.aud_chargers;
                                                //}
                                                //AuditJobSerialcharges.Where(r => r.Audjs_ItemCode == item.Audjs_ItemCode && r.Audjs_ItemStatus == _AUD_CHARGES_.aud_itm_stus && r.Audjs_RefStatus == _AUD_CHARGES_.aud_aud_tp).ToList().ForEach(y => y.Aud_charges = netamount); break;//_priceDetailRef.FirstOrDefault().Sapd_itm_price
                                            }
                                            if (_AUD_CHARGES_.aud_value == "P")
                                            {
                                                var _one = from _itm in _priceDetailRef
                                                           select _itm;
                                                int _priceType = 0;
                                                foreach (var _single in _one)
                                                {
                                                    _priceType = _single.Sapd_price_type;
                                                    PriceTypeRef _promotion = TakePromotion(_priceType);
                                                    decimal UnitPrice = FigureRoundUp(TaxCalculation(item.Audjs_ItemCode, _AUD_CHARGES_.aud_itm_stus, Convert.ToDecimal(1), _priceBookLevelRef, _priceDetailRef.FirstOrDefault().Sapd_itm_price, Convert.ToDecimal(0), Convert.ToDecimal(0), false), true);
                                                    netamount = CalculateItem(UnitPrice, 1, item.Audjs_ItemCode, _AUD_CHARGES_.aud_itm_stus);
                                                    netamount = netamount * _AUD_CHARGES_.aud_chargers / 100;
                                                }
                                            }
                                            itm_charges = _priceDetailRef.FirstOrDefault().Sapd_itm_price * _AUD_CHARGES_.aud_chargers / 100;
                                            _AuditJobSerialcharges.Where(r => r.Audjs_SerialId == item.Audjs_SerialId && r.Audjs_ItemCode == item.Audjs_ItemCode && r.Audjs_ItemStatus == _AUD_CHARGES_.aud_itm_stus && r.Audjs_RefStatus == _AUD_CHARGES_.aud_aud_tp).ToList().ForEach(y => y.Aud_charges = netamount + fixed_val);
                                            break;

                                        }
                                        else
                                        {
                                            _AuditJobSerialcharges.Where(r => r.Audjs_SerialId == item.Audjs_SerialId && r.Audjs_ItemCode == item.Audjs_ItemCode && r.Audjs_ItemStatus == _AUD_CHARGES_.aud_itm_stus && r.Audjs_RefStatus == _AUD_CHARGES_.aud_aud_tp).ToList().ForEach(y => y.Aud_charges = itm_charges);
                                            break;
                                            //this.Cursor = Cursors.Default;
                                            //MessageBox.Show("Tax rates not setup for selected item code ( " + _AUD_CHARGES_.aud_item_cd + " ). Please contact Inventory Department.", "Item Tax", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            //return;
                                        }


                                    }
                                }

                                #endregion
                                #region AUD_CHARGES_.aud_charge_base_on =="Pre-define"
                                if (_AUD_CHARGES_.aud_charge_base_on == "Pre-define")
                                {

                                    _AuditJobSerialcharges.Where(r => r.Audjs_SerialId == item.Audjs_SerialId && r.Audjs_ItemCode == item.Audjs_ItemCode && r.Audjs_ItemStatus == _AUD_CHARGES_.aud_itm_stus && r.Audjs_RefStatus == _AUD_CHARGES_.aud_aud_tp).ToList().ForEach(y => y.Aud_charges = _AUD_CHARGES_.aud_chargers);
                                    break;
                                }
                                #endregion
                            }
                        }
                    }
                }
            }

            #region update charges
            updateitmCharges(_AuditJobSerialcharges);
            //LoadAuditRemarkScreen(cmbSubJobSelection.SelectedItem.ToString(), _currentSerialId.ToString());
            btn_proc_itm_charges_Click(null, null);
            #endregion
        }

        private void btn_updatecharges_Click(object sender, EventArgs e)
        {
            string Audjs_JobNo = "";
            List<AuditJobSerial> _AuditSercharges = new List<AuditJobSerial>();
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16101))
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("You don't have permission to perform this action " + Environment.NewLine + "  Advice: Required permission code :16101", "Audit Job - Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dgvSubJobList.Focus();
                return;
            }
            foreach (DataGridViewRow _row in dgvitm_charges.Rows)
            {


                AuditJobSerial _AuditJobSerial = new AuditJobSerial();
                _AuditJobSerial.AUDJS_CHARGES_USER = Convert.ToDecimal(_row.Cells["AUDJS_CHARGES_USER1"].Value.ToString());
                _AuditJobSerial.Audjs_ItemCode = _row.Cells["Audjs_ItemCode1"].Value.ToString();
              
                _AuditJobSerial.Audjs_SerialId = Convert.ToInt32(_row.Cells["Audjs_SerialId1"].Value.ToString());
                _AuditJobSerial.Audjs_SerialNo = _row.Cells["Audjs_SerialNo1"].Value.ToString();
                _AuditJobSerial.Audjs_JobNo = cmbSubJobSelection1.Text.ToString().Trim();
                _AuditJobSerial.Audjs_Seq = _row.Cells["Audjs_Seq"].Value.ToString() == null ? 0 : Convert.ToInt32(_row.Cells["Audjs_Seq"].Value.ToString());
                _AuditJobSerial.Audjs_RefStatus = _row.Cells["Audjs_RefStatus"].Value.ToString();
                _AuditJobSerial.Audjs_RptType = _row.Cells["Audjs_RefStatus"].Value.ToString();
                _AuditJobSerial._IS_USER_charge_update = 1;
                _AuditJobSerial.Audjs_Remark = _row.Cells["Audjs_Charges_resoon"].Value.ToString();
                  
                _AuditJobSerial.Audjs_ModBy = BaseCls.GlbUserID;
                _AuditSercharges.Add(_AuditJobSerial);


            }

            updateitmCharges(_AuditSercharges);
            btn_proc_itm_charges_Click(null, null);
        }

        private void dgvitm_charges_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16101))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("You don't have permission to perform this action " + Environment.NewLine + "  Advice: Required permission code :16101", "Audit Job - Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dgvSubJobList.Focus();
                    return;
                }

                string Audjs_RefStatus = "";
                string Audjs_ItemStatus = "";
                string Audjs_ItemCode = "";

                Audjs_RefStatus = dgvitm_charges.Rows[e.RowIndex].Cells["colReasonStatus2"].Value == null ? string.Empty : Convert.ToString(dgvitm_charges.Rows[e.RowIndex].Cells["colReasonStatus2"].Value);
                Audjs_ItemStatus = dgvitm_charges.Rows[e.RowIndex].Cells["Audjs_ItemStatus1"].Value == null ? string.Empty : Convert.ToString(dgvitm_charges.Rows[e.RowIndex].Cells["Audjs_ItemStatus1"].Value);
                Audjs_ItemCode = dgvitm_charges.Rows[e.RowIndex].Cells["Audjs_ItemCode1"].Value == null ? string.Empty : Convert.ToString(dgvitm_charges.Rows[e.RowIndex].Cells["Audjs_ItemCode1"].Value);
                List<AUD_CHARGES> _AUD_CHARGES = new List<AUD_CHARGES>();
                _AUD_CHARGES = CHNLSVC.Inventory.get_AUD_CHARGES(BaseCls.GlbUserComCode, Audjs_RefStatus, null, Audjs_ItemStatus, null, null, null, 0, null, null, DateTime.Now.Date, DateTime.Now.Date, null, 1, null, Audjs_ItemCode, null, null);
                if (_AUD_CHARGES.Count > 0)
                {
                    if (_AUD_CHARGES.FirstOrDefault().aud_edit_charge == 0)
                    {
                        MessageBox.Show("Not allowed to chage charges", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvSubJobList.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Not allowed to chage charges", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvSubJobList.Focus();
                    return;
                }

            }
        }

        private void load_itm_charges_summery(string _sub_job_no)
        {
            Decimal _amount = 0;
            DataTable oDataTable = new DataTable();
            oDataTable = CHNLSVC.Inventory.get_audit_itm_charges_sum(_sub_job_no, BaseCls.GlbUserComCode);
            if (oDataTable.Rows.Count > 0)
            {
                grd_chrges_summary.AutoGenerateColumns = false;
                grd_chrges_summary.DataSource = oDataTable;
            }

            if (oDataTable.Rows.Count > 0)
            {
                foreach (DataRow drow in oDataTable.Rows)
                {
                    Decimal AB = drow["TOTAL"] == DBNull.Value ? 0 : Convert.ToDecimal(drow["TOTAL"].ToString());
                    _amount = _amount + AB;


                }
            }
            lbltotamount.Text = _amount.ToString();

        }

        private void btnapproved_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16102))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("You don't have permission to perform this action " + Environment.NewLine + "  Advice: Required permission code :16102", "Audit Job - Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dgvSubJobList.Focus();
                    return;
                }

                string _returnMessage = null;
                Int16 eff = CHNLSVC.Inventory.Updatei_JOB_Hdr_appby(cmbSubJobSelection1.SelectedItem.ToString(), BaseCls.GlbUserComCode, BaseCls.GlbUserID, 1, 0, out _returnMessage);
                if (string.IsNullOrEmpty(_returnMessage))
                {
                    this.Cursor = Cursors.Default;
                    _isSerialGridDataEdited = false;
                    MessageBox.Show("Audit Charges apporved successfully", "Stock Verification - Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_proc_itm_charges_Click(null, null);
                }
                else
                {
                    throw new Exception(_returnMessage);
                }

            }

            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
                MessageBox.Show("An error occurred while saving audit notes" + Environment.NewLine + ex.Message, "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnreqadj_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16106))
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("You don't have permission to perform this action " + Environment.NewLine + "  Advice: Required permission code :16106", "Audit Job - Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dgvSubJobList.Focus();
                return;
            }

            try
            {
                #region remark updeted item are procees by tharanga 2018/02/21
                List<string> _selectedSubJobs = new List<string>();
                if (dgvSubJobList.Rows.Count > 0)
                {
                    foreach (DataGridViewRow _dataRow in dgvSubJobList.Rows)
                    {
                        bool _isRowSelected = _dataRow.Cells["chkColSelectJob"].Value == DBNull.Value ? false : Convert.ToBoolean(_dataRow.Cells["chkColSelectJob"].Value);
                        if (_isRowSelected)
                        {
                            _selectedSubJobs.Add(_dataRow.Cells["txtColSubJobNo"].Value.ToString());
                        }
                    }
                }
                foreach (var item in _selectedSubJobs)
                {
                    _AuditJobSerialcharges = new List<AuditJobSerial>();
                    _AuditJobSerialcharges = CHNLSVC.Inventory.GetProcessedJobSerials_all(item, _processedSerialStartIndex, _processedSerialEndIndex, 0, BaseCls.GlbUserComCode); //0 - All, 1- Mismatch, 2- Match
                    _AuditJobSerialcharges.RemoveAll(r => r.Audjs_RefStatus == "" || r.Audjs_RefStatus == null);
                    Int32 count = _AuditJobSerialcharges.Where(r => r.audjs_charges_processed == 1).Count();
                    if (_AuditJobSerialcharges.Count != count)
                    {
                        MessageBox.Show("Please process item charges all updated seriales", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                foreach (var item in _selectedSubJobs)
                {
                    PhysicalStockVerificationHdr _PhysicalStockVerificationHdr = new PhysicalStockVerificationHdr();
                    _PhysicalStockVerificationHdr = CHNLSVC.Inventory.GET_STOCKVERF_HDR(item);
                    if (_PhysicalStockVerificationHdr.AUSH_CHARGES_APP == 0)
                    {
                        MessageBox.Show("Please approve item charges ", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (_PhysicalStockVerificationHdr.AUSH_ADJ_REQ_IS_SEND == 1)
                    {
                        MessageBox.Show("Adjustment Request already send ", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
                #endregion
                List<AuditJobSerial> _AuditJobSerialchargescopy = new List<AuditJobSerial>();
                _AuditJobSerialcharges = new List<AuditJobSerial>();
                _AuditJobSerialcharges = CHNLSVC.Inventory.GetProcessedJobSerials_all(cmbSubJobSelection1.SelectedItem.ToString(), _processedSerialStartIndex, _processedSerialEndIndex, 0, BaseCls.GlbUserComCode); //0 - All, 1- Mismatch, 2- Match
                //_processedAuditJobSerials.Where(r => r.Audjs_RefStatus != "").ToList();
                _AuditJobSerialcharges.RemoveAll(r => r.Audjs_RefStatus == "" || r.Audjs_RefStatus == null);
                List<InventoryRequest> _InventoryRequestlist = new List<InventoryRequest>();
                List<InventoryRequestItem> ScanItemList = new List<InventoryRequestItem>();
                List<InventoryRequestSerials> _InventoryRequestSerialsList = new List<InventoryRequestSerials>();
                InventoryRequest _inventoryRequestnew = new InventoryRequest();
                Int32 seq_no = 1;

                _AuditJobSerialcharges.RemoveAll(r => r.Audjs_RefStatus == "OTHC");
                List<string> reason = new List<string>();
                var result = _AuditJobSerialcharges.GroupBy(test => test.Audjs_RefStatus)
                   .Select(grp => grp.First().Audjs_RefStatus)
                   .ToList();
                _AuditJobSerialchargescopy = _AuditJobSerialcharges;
                foreach (var items in result)
                {
                    Int32 _line_no = 1;
                    Int32 ser_line_no = 1;
                    List<FF.BusinessObjects.General.ApprovalReqCategory> getAppReqCateList_New = new List<FF.BusinessObjects.General.ApprovalReqCategory>();
                    getAppReqCateList_New = CHNLSVC.General.getAppReqCateList_New(items, "ADJ");
                    if (getAppReqCateList_New.FirstOrDefault().MMCT_ALW_REQ == 0)
                    {
                        continue;
                    }

                    _AuditJobSerialcharges = new List<AuditJobSerial>();
                    _AuditJobSerialcharges = _AuditJobSerialchargescopy.Where(r => r.Audjs_RefStatus == items).ToList();
                    InventoryRequest _inventoryRequest = new InventoryRequest();



                    #region fill requst hdr
                    _inventoryRequest.Itr_seq_no = seq_no;
                    _inventoryRequest.Itr_com = BaseCls.GlbUserComCode;
                    _inventoryRequest.Itr_req_no = GetRequestNo();
                    _inventoryRequest.Itr_tp = "ADJREQ";
                    _inventoryRequest.Itr_anal1 = _AuditJobSerialcharges.FirstOrDefault().Audjs_JobNo;
                    _inventoryRequest.Itr_sub_tp = _AuditJobSerialcharges.FirstOrDefault().Audjs_RefStatus;
                    _inventoryRequest.Itr_loc = BaseCls.GlbUserDefLoca;
                    _inventoryRequest.Itr_ref = _AuditJobSerialcharges.FirstOrDefault().Audjs_JobNo;
                    _inventoryRequest.Itr_dt = DateTime.Now.Date;
                    _inventoryRequest.Itr_stus = "P";
                    _inventoryRequest.Itr_job_no = string.Empty;  //Invoice No.
                    _inventoryRequest.Itr_bus_code = string.Empty;  //Customer Code.
                    _inventoryRequest.Itr_note = txtRemarks.Text;
                    _inventoryRequest.Itr_anal2 = _AuditJobSerialcharges.FirstOrDefault().Audjs_JobNo;
                    _inventoryRequest.Itr_rec_to = "";
                    _inventoryRequest.Itr_direct = 0;
                    _inventoryRequest.Itr_country_cd = string.Empty;  //Country Code.
                    _inventoryRequest.Itr_town_cd = string.Empty;     //Town Code.
                    _inventoryRequest.Itr_cur_code = string.Empty;    //Currency Code.
                    _inventoryRequest.Itr_exg_rate = 0;              //Exchange rate.
                    _inventoryRequest.Itr_act = 1;
                    _inventoryRequest.Itr_cre_by = BaseCls.GlbUserID;
                    _inventoryRequest.Itr_session_id = BaseCls.GlbUserSessionID;
                    _inventoryRequest.Itr_system_module = "ADJREQ";
                    _inventoryRequest.Itr_tp = "ADJREQ";
                    _InventoryRequestlist.Add(_inventoryRequest);

                    #endregion

                    foreach (AuditJobSerial itemser in _AuditJobSerialcharges)
                    {
                        InventoryRequestSerials _InventoryRequestSerials = new InventoryRequestSerials();
                        InventoryRequestItem ScanItem = new InventoryRequestItem();
                        MasterItem _itmMasterItem = new MasterItem();
                        _itmMasterItem = CHNLSVC.General.GetItemMaster(itemser.Audjs_ItemCode);
                        List<AUD_CHARGES> _AUD_CHARGES = new List<AUD_CHARGES>();
                        _AUD_CHARGES = CHNLSVC.Inventory.get_AUD_CHARGES(BaseCls.GlbUserComCode, itemser.Audjs_RefStatus, null, itemser.Audjs_ItemStatus, null, null, null, 0, null, null, DateTime.Now.Date, DateTime.Now.Date, null, 1, null, itemser.Audjs_ItemCode, _itmMasterItem.Mi_cate_1, _itmMasterItem.Mi_brand);
                        if (_AUD_CHARGES.Count > 0)
                        {
                            #region serch availabe ser
                            InventorySerialN _Invser = new InventorySerialN();
                            _Invser.Ins_com = BaseCls.GlbUserComCode;
                            _Invser.Ins_loc = BaseCls.GlbUserDefLoca;
                            _Invser.Ins_itm_cd = itemser.Audjs_ItemCode;
                            _Invser.Ins_itm_stus = itemser.Audjs_ItemStatus;
                            _Invser.Ins_ser_id = itemser.Audjs_SerialId;
                            _Invser.Ins_ser_1 = itemser.Audjs_SerialNo;
                            _Invser.Ins_available = 1;
                            List<InventorySerialN> _InventorySerialN = new List<InventorySerialN>();
                            _InventorySerialN = CHNLSVC.Inventory.Get_INR_SER_DATA(_Invser);
                            #endregion
                            if (_InventorySerialN.Count > 0)
                            {
                                #region fill req det
                                ScanItem.Itri_seq_no = seq_no;
                                ScanItem.Itri_line_no = _line_no;
                                ScanItem.Itri_itm_cd = itemser.Audjs_ItemCode;
                                ScanItem.Itri_itm_stus = itemser.Audjs_ItemStatus;
                                ScanItem.Itri_qty = 1;
                                ScanItem.Itri_unit_price = _InventorySerialN.FirstOrDefault().Ins_unit_cost;
                                ScanItem.Itri_app_qty = 1;
                                ScanItem.Itri_res_no = "";
                                ScanItem.Itri_note = itemser.Audjs_ItemStatus;//new item states
                                ScanItem.Itri_mitm_cd = itemser.Audjs_ItemCode;
                                ScanItem.Itri_mitm_stus = itemser.Audjs_ItemStatus;
                                ScanItem.Itri_mqty = 1;
                                ScanItem.Itri_bqty = 0;
                                ScanItem.Itri_job_no = "";
                                ScanItem.Itri_job_line = 0;
                                ScanItem.Itri_com = BaseCls.GlbUserComCode;
                                ScanItem.Itri_loc = BaseCls.GlbUserDefLoca;
                                ScanItem.Itri_po_qty = 0;
                                ScanItem.Itri_issue_qty = 0;
                                ScanItem.Itri_res_qty = 0;
                                ScanItem.Itri_res_line = 0;
                                ScanItem.Itri_cncl_qty = 0;
                                ScanItem.Itri_shop_qty = 0;
                                ScanItem.Itri_fd_qty = 0;
                                ScanItem.Itri_git_qty = 0;
                                ScanItem.Itri_buffer = 0;
                                ScanItem.Itri_advan_qty = 0;
                                ScanItem.Itri_base_req_no = "";
                                ScanItem.Itri_base_req_line = 0;
                                ScanItem.Itri_old_itm_cd = "";
                                ScanItem.Itri_itm_mod_by = BaseCls.GlbUserID;
                                ScanItem.Itri_itm_mod_dt = DateTime.Now.Date;
                                ScanItem.Itri_itm_mod_session = BaseCls.GlbUserSessionID;

                                //ScanItem.MasterItem.Mi_cd = itemser.Audjs_ItemCode;
                                ScanItemList.Add(ScanItem);
                                #endregion
                                #region fill ser
                                _InventoryRequestSerials.Itrs_seq_no = seq_no;
                                _InventoryRequestSerials.Itrs_line_no = _line_no;

                                _InventoryRequestSerials.Itrs_ser_line = ser_line_no;
                                _InventoryRequestSerials.Itrs_itm_cd = itemser.Audjs_ItemCode;
                                _InventoryRequestSerials.Itrs_itm_stus = itemser.Audjs_ItemStatus;
                                _InventoryRequestSerials.Itrs_ser_1 = _InventorySerialN.FirstOrDefault().Ins_ser_1;
                                _InventoryRequestSerials.Itrs_ser_2 = _InventorySerialN.FirstOrDefault().Ins_ser_2;
                                _InventoryRequestSerials.Itrs_ser_3 = _InventorySerialN.FirstOrDefault().Ins_ser_3;
                                _InventoryRequestSerials.Itrs_ser_4 = _InventorySerialN.FirstOrDefault().Ins_ser_4;
                                _InventoryRequestSerials.Itrs_qty = 1;
                                _InventoryRequestSerials.Itrs_in_docno = _InventorySerialN.FirstOrDefault().Ins_doc_no;
                                _InventoryRequestSerials.Itrs_in_itmline = _line_no;
                                _InventoryRequestSerials.Itrs_in_batchline = _InventorySerialN.FirstOrDefault().Ins_batch_line;
                                _InventoryRequestSerials.Itrs_in_serline = ser_line_no;
                                _InventoryRequestSerials.Itrs_in_docdt = _InventorySerialN.FirstOrDefault().Ins_doc_dt;
                                _InventoryRequestSerials.Itrs_trns_tp = "ADJ";
                                _InventoryRequestSerials.Itrs_rmk = "";
                                _InventoryRequestSerials.Itrs_ser_id = _InventorySerialN.FirstOrDefault().Ins_ser_id;
                                _InventoryRequestSerials.Itrs_nitm_stus = _InventorySerialN.FirstOrDefault().Ins_itm_stus;
                                _InventoryRequestSerials.Itri_itm_new_ser = itemser.Audjs_Remark;
                                _InventoryRequestSerials.ITRS_ITM_NEW_CD = itemser.Audjs_ItemCode;
                                _InventoryRequestSerials.ITRS_ITM_SUP = _InventorySerialN.FirstOrDefault().Ins_exist_supp;

                                _InventoryRequestSerialsList.Add(_InventoryRequestSerials);
                                #endregion
                            }
                            else
                            {
                                #region fill req det
                                ScanItem.Itri_seq_no = seq_no;
                                ScanItem.Itri_line_no = _line_no;
                                ScanItem.Itri_itm_cd = itemser.Audjs_ItemCode;
                                ScanItem.Itri_itm_stus = itemser.Audjs_ItemStatus;
                                ScanItem.Itri_qty = 1;
                                ScanItem.Itri_unit_price = 0;
                                ScanItem.Itri_app_qty = 1;
                                ScanItem.Itri_res_no = "";
                                ScanItem.Itri_note = itemser.Audjs_ItemStatus;//new item states
                                ScanItem.Itri_mitm_cd = itemser.Audjs_ItemCode;
                                ScanItem.Itri_mitm_stus = itemser.Audjs_ItemStatus;
                                ScanItem.Itri_mqty = 1;
                                ScanItem.Itri_bqty = 0;
                                ScanItem.Itri_job_no = "";
                                ScanItem.Itri_job_line = 0;
                                ScanItem.Itri_com = BaseCls.GlbUserComCode;
                                ScanItem.Itri_loc = BaseCls.GlbUserDefLoca;
                                ScanItem.Itri_po_qty = 0;
                                ScanItem.Itri_issue_qty = 0;
                                ScanItem.Itri_res_qty = 0;
                                ScanItem.Itri_res_line = 0;
                                ScanItem.Itri_cncl_qty = 0;
                                ScanItem.Itri_shop_qty = 0;
                                ScanItem.Itri_fd_qty = 0;
                                ScanItem.Itri_git_qty = 0;
                                ScanItem.Itri_buffer = 0;
                                ScanItem.Itri_advan_qty = 0;
                                ScanItem.Itri_base_req_no = "";
                                ScanItem.Itri_base_req_line = 0;
                                ScanItem.Itri_old_itm_cd = "";
                                ScanItem.Itri_itm_mod_by = BaseCls.GlbUserID;
                                ScanItem.Itri_itm_mod_dt = DateTime.Now.Date;
                                ScanItem.Itri_itm_mod_session = BaseCls.GlbUserSessionID;

                                //ScanItem.MasterItem.Mi_cd = itemser.Audjs_ItemCode;
                                ScanItemList.Add(ScanItem);
                                #endregion
                                #region fill ser
                                _InventoryRequestSerials.Itrs_seq_no = seq_no;
                                _InventoryRequestSerials.Itrs_line_no = _line_no;
                                _InventoryRequestSerials.Itrs_ser_line = ser_line_no;
                                _InventoryRequestSerials.Itrs_itm_cd = itemser.Audjs_ItemCode;
                                _InventoryRequestSerials.Itrs_itm_stus = itemser.Audjs_ItemStatus;
                                _InventoryRequestSerials.Itrs_ser_1 = itemser.Audjs_SerialNo;
                                _InventoryRequestSerials.Itrs_ser_2 = "N/A";
                                _InventoryRequestSerials.Itrs_ser_3 = "N/A";
                                _InventoryRequestSerials.Itrs_ser_4 = "N/A";
                                _InventoryRequestSerials.Itrs_qty = 1;
                                _InventoryRequestSerials.Itrs_in_docno = "";
                                _InventoryRequestSerials.Itrs_in_itmline = _line_no;
                                _InventoryRequestSerials.Itrs_in_batchline = 1;
                                _InventoryRequestSerials.Itrs_in_serline = ser_line_no;
                                _InventoryRequestSerials.Itrs_in_docdt = DateTime.Now.Date;
                                _InventoryRequestSerials.Itrs_trns_tp = "ADJ";
                                _InventoryRequestSerials.Itrs_rmk = "";
                                _InventoryRequestSerials.Itrs_ser_id = CHNLSVC.Inventory.GetSerialID();
                                _InventoryRequestSerials.Itrs_nitm_stus = itemser.Audjs_ItemStatus;
                                _InventoryRequestSerials.Itri_itm_new_ser = itemser.Audjs_Remark;
                                _InventoryRequestSerials.ITRS_ITM_NEW_CD = itemser.Audjs_ItemCode;
                                _InventoryRequestSerials.ITRS_ITM_SUP = "N/A";

                                _InventoryRequestSerialsList.Add(_InventoryRequestSerials);
                                #endregion
                            }
                            _line_no++;
                        }
                        ser_line_no++;
                    }
                    seq_no++;
                }

                //CHNLSVC.Inventory.GetSerialID();
                _inventoryRequestnew.InventoryRequestSerialsList = _InventoryRequestSerialsList;
                _inventoryRequestnew.InventoryRequestItemList = ScanItemList;
                Int32 rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData_ADUIT(_InventoryRequestlist, ScanItemList, _InventoryRequestSerialsList, GenerateMasterAutoNumber_req());


                if (rowsAffected > 0)
                {
                    string _returnMessage = null;
                    //Int16 eff = CHNLSVC.Inventory.Updatei_JOB_Hdr_appby(cmbSubJobSelection1.SelectedItem.ToString(), BaseCls.GlbUserComCode, BaseCls.GlbUserID, 1, 1, out _returnMessage);
                    MessageBox.Show("Job Requset send successfully", "Stock Verification-Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_proc_itm_charges_Click(null, null);

                }
                else
                {
                    MessageBox.Show("An error occurred while saving audit notes", "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception)
            {

                MessageBox.Show("An error occurred while saving audit notes", "Stock Verification - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private string GetRequestNo()
        {
            string _reqNo = string.Empty;
            _reqNo = "DOC-" + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();
            return _reqNo;
        }
        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "ADJ", 0, BaseCls.GlbUserComCode);//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = "ADJ-S";
            RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
            RPH.Tuh_ischek_itmstus = true;//might change 
            RPH.Tuh_ischek_reqqty = true;//might change
            RPH.Tuh_ischek_simitm = true;//might change
            RPH.Tuh_session_id = BaseCls.GlbUserSessionID;
            RPH.Tuh_usr_com = BaseCls.GlbUserComCode;//might change 
            RPH.Tuh_usr_id = BaseCls.GlbUserID;
            RPH.Tuh_usrseq_no = generated_seq;
            RPH.Tuh_direct = false;
            RPH.Tuh_doc_no = generated_seq.ToString();
            //write entry to TEMP_PICK_HDR
            int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
            if (affected_rows == 1)
            {

                return generated_seq;
            }
            else
            {
                return 0;
            }
        }
        private MasterAutoNumber GenerateMasterAutoNumber_req()
        {
            //string moduleText = ddlRequestType.SelectedValue.Equals("MRN") ? "MRN" : "INTR";

            string moduleText = "ADJREQ";

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_cate_cd = string.IsNullOrEmpty(BaseCls.GlbUserDefLoca) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = moduleText;
            masterAuto.Aut_number = 0;
            masterAuto.Aut_start_char = moduleText;
            masterAuto.Aut_year = null;

            return masterAuto;
        }

        private void btnaddcherge_Click(object sender, EventArgs e)
        {
            int count = _AuditJobSerialcharges.Where(r => r.Audjs_RefStatus == "OTHC").Count();
            //if (count > 0)
            //{
            //    MessageBox.Show("Other Charges already added", "Stock Verification-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            List<AuditJobSerial> _AuditJobSerial_list = new List<AuditJobSerial>();
            string Audjs_JobNo = "";
            List<AuditJobSerial> _AuditSercharges = new List<AuditJobSerial>();

            AuditJobSerial _AuditJobSerial = new AuditJobSerial();
            _AuditJobSerial.AUDJS_CHARGES_USER = Convert.ToDecimal(txtcherge_amount.Text);
            _AuditJobSerial.Audjs_ItemCode = txtchrgecode.Text.ToString();
            _AuditJobSerial.Audjs_SerialId = 0;
            _AuditJobSerial.Audjs_SerialNo = "Other Charges";
            _AuditJobSerial.Audjs_JobNo = cmbSubJobSelection1.Text.ToString().Trim();
            _AuditJobSerial.Audjs_RefStatus = "OTHC";
            _AuditJobSerial.Audjs_ItemStatus = "GOD";
            _AuditJobSerial.Audjs_Type = "OTHC";
            _AuditJobSerial.Audjs_Remark = txtcgergeresion.Text.ToString();
            _AuditJobSerial.Audjs_Charges_resoon = txtcgergeresion.Text.ToString();
            _AuditJobSerial._IS_USER_charge_update = 1;
            _AuditJobSerial.Aud_charges = 0;
            _AuditJobSerial.Audjs_CreatedBy = BaseCls.GlbUserID;
            _AuditJobSerial.Audjs_ModBy = BaseCls.GlbUserID;
            _AuditJobSerial.Audjs_SessionId = BaseCls.GlbUserSessionID;
            _AuditJobSerial.Audjs_Seq = _AuditJobSerialcharges.FirstOrDefault().Audjs_Seq;
            _AuditJobSerial.Audjs_Seq = _AuditJobSerialcharges.FirstOrDefault().Audjs_Seq;
            _AuditJobSerial_list.Add(_AuditJobSerial);

            _AuditJobSerialcharges.AddRange(_AuditJobSerial_list);
            dgvitm_charges.DataSource = null;
            dgvitm_charges.AutoGenerateColumns = false;
            dgvitm_charges.DataSource = _AuditJobSerialcharges;
            //BindSerialDetails_with_Charges(_AuditJobSerialcharges);
        }

        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByTypeNew);
                DataTable _result = CHNLSVC.CommonSearch.GetItemByTypeNew(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtchrgecode;
                _CommonSearch.ShowDialog();
                txtchrgecode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtchrgecode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtchrgecode.Text))
            {
                //if (chkEditItm.Checked == false)
                //{
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByTypeNew);
                DataTable _result = CHNLSVC.CommonSearch.GetItemByTypeNew(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("ITEM") == txtchrgecode.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select valid item.", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtchrgecode.Clear();
                    txtchrgecode.Focus();
                    return;
                }
                else
                {
                    MasterItem _itmMas = new MasterItem();
                    _itmMas = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtchrgecode.Text.Trim());
                    if (_itmMas != null)
                    {
                        if (!string.IsNullOrEmpty(_itmMas.Mi_shortdesc))
                        {
                            txtcgergeresion.Text = _itmMas.Mi_shortdesc;
                        }   
                    }
                  
                 


                }
                //}
                //else
                //{
                //    MasterItem _itmMas = new MasterItem();
                //    _itmMas = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());

                //}
            }
        }

        private void sens_mail(string requst)
        {

            string email = "";
            string Item = "";
            string model = "";
            string qty = "";
            DataTable pc = new DataTable();
            //pc = CHNLSVC.CustService.get_profitcenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            pc = CHNLSVC.CustService.get_msg_info_MAIL(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "AUD"); //define mail and ph no
            foreach (DataRow dr in pc.Rows)
            {
                email = dr["MPC_EMAIL"].ToString();
            }
            if (IsValidEmail(email))
            {


                string _mail = "";
                _mail += "Invoice ref. Number -" + "" + Environment.NewLine;
                _mail += "Customer Code -" + "" + " " + Environment.NewLine;
                _mail += "Customer Name -" + "" + " " + Environment.NewLine;
                _mail += "Delivery Name -" + "" + " " + Environment.NewLine;
                _mail += "Deliver address 1 -" + "" + " " + Environment.NewLine;
                _mail += "Deliver address 2 -" + "" + " " + Environment.NewLine;

                _mail += "Delivery order number - " + "" + " " + Environment.NewLine;
                _mail += "Date -" + "" + " " + Environment.NewLine;
                _mail += "Item Code - " + "" + " " + Environment.NewLine;
                _mail += "Model - " + "" + " " + Environment.NewLine;
                _mail += "Qty- " + "" + " " + Environment.NewLine;
                _mail += "*** This is an automatically generated email, please do not reply ***" + Environment.NewLine;
                CHNLSVC.CommonSearch.Send_SMTPMail(email, "Deliverd Invoice Details", _mail);
            }
        }

        private void dgvProcessedSerials_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string value = "";

                value = dgvProcessedSerials.Rows[e.RowIndex].Cells["colReasonStatus"].Value == null ? string.Empty : dgvProcessedSerials.Rows[e.RowIndex].Cells["colReasonStatus"].Value.ToString();

                if (value.ToString() != "ISM")
                {
                    dgvProcessedSerials.Rows[e.RowIndex].Cells["colRemark"].ReadOnly = true;
                }
                else
                {
                    dgvProcessedSerials.Rows[e.RowIndex].Cells["colRemark"].ReadOnly = false;

                }
                if (value.ToString() != "ISM")
                {
                    dgvProcessedSerials.Rows[e.RowIndex].Cells["colRemark"].Value = "";
                }

                //if (e.ColumnIndex == 1)
                //{
                //    object values = dgvProcessedSerials.Rows[e.RowIndex].Cells[6].Value;
                //    if (values.ToString() != "ISM")
                //    {
                //        dgvProcessedSerials.Rows[e.RowIndex].Cells[7].ReadOnly = true;
                //    }
                //    else
                //    {
                //        dgvProcessedSerials.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                //    }
                //}
            }
        }

        private void txtRmk7_Leave(object sender, EventArgs e)
        {
            int _seqNo = 0;
            string _rmkStatus = string.Empty;

            if (_remarkedSerials.Count > 0)
            {
                _seqNo = Convert.ToInt32(_remarkedSerials.Where(x => x.Audjs_SerialNo == lblSerial.Text.Trim() && x.Audjs_ItemCode == lblItem.Text.Trim() && x.Audjs_SerialId == Convert.ToInt32(lblSerId.Text) && x.Audjs_RptType == cmbRmk.SelectedValue.ToString()).Select(x => x.Audjs_Seq).First());
                _rmkStatus = _remarkedSerials.Where(x => x.Audjs_SerialNo == lblSerial.Text.Trim() && x.Audjs_ItemCode == lblItem.Text.Trim() && x.Audjs_SerialId == Convert.ToInt32(lblSerId.Text)).Select(x => x.Audjs_RefStatus).First().ToString();

                //_seqNo = Convert.ToInt32(_remarkedSerials.Where(x => x.Audjs_SerialNo == lblSerial.Text.Trim() && x.Audjs_ItemCode == lblItem.Text.Trim() && x.Audjs_SerialId == Convert.ToInt32(lblSerId.Text)).Select(x => x.Audjs_Seq).First());
                //_rmkStatus = _remarkedSerials.Where(x => x.Audjs_SerialNo == lblSerial.Text.Trim() && x.Audjs_ItemCode == lblItem.Text.Trim() && x.Audjs_SerialId == Convert.ToInt32(lblSerId.Text)).Select(x => x.Audjs_RefStatus).First().ToString();
            }
            if (!string.IsNullOrEmpty(txtRmk7.Text))
            {


                if (_rmkStatus == "EXP")
                {
                    DataTable odata = CHNLSVC.Sales.getReqHdrByReqNo(null, null, txtRmk7.Text);
                    if (odata.Rows.Count == 0)
                    {
                        MessageBox.Show("Please select valid Exchange no", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRmk7.Clear();
                        txtRmk7.Focus();
                        return;
                    }
                }
                if (_rmkStatus == "PDO")
                {
                    List<InvoiceItem> _InvoiceItem = CHNLSVC.Sales.GetInvoiceItems(txtRmk7.Text);
                    if (_InvoiceItem != null)
                    {
                        int count = _InvoiceItem.Where(r => r.Sad_itm_cd == lblItem.Text.Trim() && r.Sad_qty > r.Sad_do_qty).Count();
                        if (count == 0)
                        {
                            MessageBox.Show("Selected Item " + lblItem.Text.Trim() + " not availeble in invoice " + txtRmk7.Text, "Audit Charges", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select valid Invoice number", "Job Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRmk7.Clear();
                        txtRmk7.Focus();
                        return;
                    }
                }

            }


        }

    }
}
