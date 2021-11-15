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
    public partial class ServiceGatePass : Base
    {
        private Service_Chanal_parameter _scvParam = null;
        public int SelectedJobLine = -1;
        private List<Service_job_Det> _selectedItemList = null;
        private List<ReptPickSerials> _serList = null;
        private static string _custCode = "";
        private static string _jobSbType = "";
        private static Int32 _isStkUpdate = 0;
        private static string _rccNo = "";
        private static string _jobNo = "";
        private static Boolean _isAllowGatePass = false;
        private string _errorcorrection = "N";

        private Service_Req_Hdr _scvjobHdr = null;
        private List<Service_Req_Det> _scvItemList = null;
        private List<Service_Req_Def> _scvDefList = null;
        private List<Service_Tech_Aloc_Hdr> _scvEmpList = null;
        private List<Service_Req_Det_Sub> _scvItemSubList = null;
        private List<Service_Req_Det_Sub> _tempItemSubList = null;
        private List<Service_TempIssue> _scvStdbyList = null;

        private void SystemWarnningMessage(string _msg, string _title)
        { this.Cursor = Cursors.Default; MessageBox.Show(_msg, _title, MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        private void BackDatePermission()
        {
            try
            { 
                this.Cursor = Cursors.WaitCursor; 
                bool _allowCurrentTrans = false; 
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, dtDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans); 
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); }
            finally { this.Cursor = Cursors.Default; }
        }

   

        public ServiceGatePass()
        {
            InitializeComponent();
            dgvJobDetails.AutoGenerateColumns = false;
            clear();
            _scvDefList = new List<Service_Req_Def>();
            _scvItemSubList = new List<Service_Req_Det_Sub>();
            _scvItemList = new List<Service_Req_Det>();
            _scvStdbyList = new List<Service_TempIssue>();

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
                this.Close();
            }
            dgvService.Enabled = true;
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
                case CommonUIDefiniton.SearchUserControlType.ServiceGatePass:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + dtDate.Value.Date.ToShortDateString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceCustSatisfact:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbDefSubChannel + seperator);
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
                default:
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
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "7,8" + seperator);
                        break;
                    }
                    break;
            }

            return paramsText.ToString();
        }


        private void txtJobNo_DoubleClick(object sender, EventArgs sendmae)
        {
            //this.Cursor = Cursors.WaitCursor;
            //CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            //_CommonSearch.ReturnIndex = 0;
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
            //DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, Convert.ToDateTime("31-12-1111"), Convert.ToDateTime("31-12-2999"));
            //_CommonSearch.dtpFrom.Value = Convert.ToDateTime("01-01-1911");
            //_CommonSearch.dtpTo.Value = Convert.ToDateTime("31-12-2999");
            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = txtJobNo;
            //this.Cursor = Cursors.Default;
            //_CommonSearch.IsSearchEnter = true;
            //_CommonSearch.ShowDialog();
            //txtJobNo.Focus();
            this.Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
            DateTime dtTemp = DateTime.Today.AddMonths(-1);
            DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, dtTemp, DateTime.Today);
            _CommonSearch.dtpFrom.Value = dtTemp;
            _CommonSearch.dtpTo.Value = DateTime.Today;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtJobNo;
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtJobNo.Focus();

            dgvJobDetails.DataSource = null;
            Boolean _isField = CHNLSVC.CustService.checkFiedJob(txtJobNo.Text);
            if (_isField == true)
            {
                MessageBox.Show("Cannot select this job. This is a field job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtJobNo.Text = "";
                return;
            }

            _jobNo = txtJobNo.Text;

            getJobJetails();
            txtJobNo.Focus();
        }

        private void txtJobNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                getJobJetails();
            }
        }

        private void txtJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_add_ser.Focus();
                //txtJobNo_Leave(null, null);
            }
            else if (e.KeyCode == Keys.F2)
            {
                txtJobNo_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnMoreDetails_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_jobNo))
            {
                ServiceJobDetailsViwer oJobDetailsViwer = new ServiceJobDetailsViwer(_jobNo, -777);
                oJobDetailsViwer.ShowDialog();
            }
        }

        private void getJobJetails()
        {
            DataTable DtDetails = new DataTable();
            string stage = string.Empty;
            Int32 IsCusExpected = 0;

            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                ////stage = "3,2,6,4,5.1";
                stage = "7,8";
                DtDetails = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1911"), Convert.ToDateTime("31-12-2999"), txtJobNo.Text, stage, IsCusExpected, string.Empty, BaseCls.GlbUserDefProf);
                if (DtDetails.Rows.Count > 0)
                {
                    //1/9/2015
                    if (DtDetails.Rows[0]["SC_DIRECT"].ToString() != "W")
                    {
                        if (_errorcorrection == "N")
                        {
                            MessageBox.Show("This is field job cannot generate the gate pass.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clear();
                            return;
                        }
                    }
                    dgvJobDetails.DataSource = DtDetails;
                    modifyJobDetailGrid();
                    Service_JOB_HDR oJOB_HDR = CHNLSVC.CustService.GetServiceJobHeader(txtJobNo.Text, BaseCls.GlbUserComCode);

                    //_custCode = oJOB_HDR.SJB_B_CUST_CD;
                    _jobSbType = oJOB_HDR.SJB_JOBSTP;


                    lblName.Text = oJOB_HDR.SJB_B_CUST_TIT + " " + oJOB_HDR.SJB_B_CUST_NAME;
                    lblAddrss.Text = oJOB_HDR.SJB_B_ADD1 + "  " + oJOB_HDR.SJB_B_ADD2 + "  " + oJOB_HDR.SJB_B_ADD3;
                    lblTele.Text = oJOB_HDR.SJB_B_MOBINO;
                    lblContactPerson.Text = oJOB_HDR.SJB_CNT_PERSON;
                    lblConPhone.Text = oJOB_HDR.SJB_CNT_PHNO;
                    lblCustomerCode.Text = oJOB_HDR.SJB_CUST_CD;

                    //txtIssueLoc.ReadOnly = true;
                    //btnIssueLoc.Enabled = false;

                    //if (Convert.ToInt32(dgvJobDetails.Rows[0].Cells["jbd_isstockupdate"].Value) == 1)
                    //{
                    txtIssueLoc.Text = dgvJobDetails.Rows[0].Cells["jbd_aodissueloc"].Value.ToString();
                    //    if (_jobSbType != "RCC")
                    //    {
                    //        txtIssueLoc.ReadOnly = false;
                    //        btnIssueLoc.Enabled = true;
                    //    }
                    //}
                    foreach (DataGridViewRow row in dgvJobDetails.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    dgvJobDetails.EndEdit();

                    //lblJobStage.Text = oJOB_HDR.SJB_JOBSTAGE_TEXT;

                    //if (oJOB_HDR.SJB_JOBSTAGE > 3)
                    //{
                    //    button1.Enabled = false;
                    //}
                    //enableDisableBtns(false);

                   // dgvJobDetails.Focus();
                }
                //else
                //{
                //    MessageBox.Show("Please enter valid job number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    clear();
                //    return;
                //}
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

        private void clear()
        {
            dtDate.Value = DateTime.Now.Date;
            _selectedItemList = new List<Service_job_Det>();
            _serList = new List<ReptPickSerials>();

            txtJobNo.Clear();
            btnSave.Enabled = true;
            lblName.Text = "";
            lblTele.Text = "";
            lblCustomerCode.Text = "";
            _custCode = "";
            _jobSbType = "";
            _isStkUpdate = 0;
            _isAllowGatePass = false;
            _rccNo = "";
            dgvJobDetails.DataSource = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1800"), Convert.ToDateTime("01-01-1800"), txtJobNo.Text, "0", 0, string.Empty, BaseCls.GlbUserDefProf);
            SelectedJobLine = -1;
            lblAddrss.Text = "";
            txtSatisLevel.Text = "";
            txtSatRem.Text = "";
            dgvSelect.DataSource = null;
            txtRecBy.Text = "";
            txtRem.Text = "";
            cmbMethod.SelectedIndex = -1;
            txtRef.Text = "";

            serviceClear(); // AC Request Tharindu 20-04-2018
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (_errorcorrection == "N")
            {
                if (MessageBox.Show("Are you sure ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            }

            clear();

            //updated by akila 2017/09/02 - check back date enabale controls
            if (_errorcorrection == "N")
            {
                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty.ToUpper().ToString(), this.GlbModuleName, dtDate, lblBackDateInfor, dtDate.Value.Date.ToString(), out _allowCurrentTrans))
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (dtDate.Value.Date != DateTime.Now.Date)
                        {
                            dtDate.Enabled = true;
                        }
                    }
                    else
                    {
                        dtDate.Enabled = true;
                    }
                }
            }
        }

        private void btnVouNo_Click(object sender, EventArgs e)
        {
            txtJobNo_DoubleClick(null, null);
        }



        private void btn_add_ser_Click(object sender, EventArgs e)
        {
            _isAllowGatePass = false;
            Boolean _isSel = false;

            for (int i = 0; i < dgvJobDetails.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvJobDetails.Rows[i].Cells["select"].Value) == true)
                {
                    _isSel = true;
                    if (!string.IsNullOrEmpty(_custCode))
                        if (_custCode != dgvJobDetails.Rows[i].Cells["SJB_CUST_CD"].Value.ToString())
                        {
                            if (dgvSelect.Rows.Count > 0)
                            {
                                MessageBox.Show("Different customer cannot be added !", "Infor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                    _custCode = dgvJobDetails.Rows[i].Cells["SJB_CUST_CD"].Value.ToString();
                    //Check cust allow gate pass without invoice 
                    if (CHNLSVC.CustService.IsCustAllwGatePassWOutInv(BaseCls.GlbUserComCode, _custCode) == true)
                        _isAllowGatePass = true;

                    //check whether invoices this job item line
                    List<Service_Confirm_detail> oItmes = CHNLSVC.CustService.GetServiceConfirmDet(dgvJobDetails.Rows[i].Cells["JobNo"].Value.ToString(), Convert.ToInt32(dgvJobDetails.Rows[i].Cells["JobLine"].Value));
                    foreach (Service_Confirm_detail _lstjobconfDet in oItmes)
                    {
                        Boolean _isInvoiced = CHNLSVC.CustService.IsJobLineInvoiced(_lstjobconfDet.Jcd_jobno, _lstjobconfDet.Jcd_joblineno);
                        if (_isAllowGatePass == false && _isInvoiced == false)
                        {
                            if (_errorcorrection == "N")
                            {
                                MessageBox.Show("Invoice not found for this job !", "Infor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            return;
                        }
                    }
                    var _duplicate = from _dup in _selectedItemList
                                     where _dup.Jbd_jobno == dgvJobDetails.Rows[i].Cells["JobNo"].Value.ToString() && _dup.Jbd_jobline == Convert.ToInt32(dgvJobDetails.Rows[i].Cells["JobLine"].Value)
                                     select _dup;
                    if (_duplicate.Count() == 0)
                    {
                        Service_job_Det _jobDet = new Service_job_Det();
                        _jobDet.Jbd_jobno = dgvJobDetails.Rows[i].Cells["JobNo"].Value.ToString();
                        _jobDet.Jbd_jobline = Convert.ToInt32(dgvJobDetails.Rows[i].Cells["JobLine"].Value);
                        _jobDet.Jbd_itm_cd = dgvJobDetails.Rows[i].Cells["ItemCode"].Value.ToString();
                        _jobDet.Jbd_ser1 = dgvJobDetails.Rows[i].Cells["SerialNo"].Value.ToString();
                        _jobDet.Jbd_warr = dgvJobDetails.Rows[i].Cells["WarrentyNo"].Value.ToString();
                        _jobDet.Jbd_isstockupdate = Convert.ToInt32(dgvJobDetails.Rows[i].Cells["jbd_isstockupdate"].Value);
                        _isStkUpdate = Convert.ToInt32(dgvJobDetails.Rows[i].Cells["jbd_isstockupdate"].Value);
                        _rccNo = dgvJobDetails.Rows[i].Cells["jbd_reqno"].Value.ToString();
                        _jobDet.Sjb_jobstp = _jobSbType;
                        _jobDet.Jbd_aodissueloc = txtIssueLoc.Text;
                        _jobDet.Jbd_aodissueno = dgvJobDetails.Rows[i].Cells["Jbd_aodissueno"].Value.ToString();
                        _jobDet.Jbd_com = BaseCls.GlbUserComCode;
                        _jobDet.Jbd_loc = BaseCls.GlbUserDefLoca;


                        _jobDet.Jbd_invc_no = "";
                        _jobDet.Jbd_invc_line = 0;

                        _selectedItemList.Add(_jobDet);

                    }
                    else
                    {
                        MessageBox.Show("Already added this job number !", "Infor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }

            DataTable _dt = _selectedItemList.ToDataTable();
            dgvSelect.DataSource = null;
            dgvSelect.AutoGenerateColumns = false;
            dgvSelect.DataSource = _dt;

            _jobNo = txtJobNo.Text;

            if (_isSel == true)
            {
                dgvJobDetails.DataSource = null;
                txtJobNo.Text = "";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSatisLevel.Text))
            {
                MessageBox.Show("Please select satisfaction level !", "Infor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (cmbMethod.SelectedIndex == -1)
            {
                MessageBox.Show("Select the transport method", "Infor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (_selectedItemList.Count == 0)
            {
                if (_errorcorrection == "N")
                {
                    MessageBox.Show("Please add Job Item!", "Infor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                   return;
            }
            

            DataTable DtDetails = new DataTable();
            string stage = string.Empty;
            Int32 IsCusExpected = 0;

            if (_errorcorrection == "N")
            {
                if (!string.IsNullOrEmpty(txtJobNo.Text))
                {
                    //stage = "3,2,6,4,5.1";
                    stage = "7,8";
                    DtDetails = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1911"), Convert.ToDateTime("31-12-2999"), txtJobNo.Text, stage, IsCusExpected, string.Empty, BaseCls.GlbUserDefProf);
                    if (DtDetails.Rows.Count == 0)
                    {
                        MessageBox.Show("Please enter valid job number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                        return;
                    }
                }


                if (MessageBox.Show("Are you sure ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            }

            LoadJob(BaseCls.GlbUserComCode, txtJobNo.Text, "");

            MasterAutoNumber masterAutoNum = new MasterAutoNumber();
            masterAutoNum.Aut_cate_cd = BaseCls.GlbUserDefLoca;
            masterAutoNum.Aut_cate_tp = "LOC";
            masterAutoNum.Aut_direction = 0;
            masterAutoNum.Aut_moduleid = "GP";
            masterAutoNum.Aut_start_char = "GP";
            masterAutoNum.Aut_year = dtDate.Value.Date.Year;

            InventoryHeader _inventoryHeader = new InventoryHeader();
            MasterAutoNumber _inventoryAuto = new MasterAutoNumber();
            List<ReptPickSerials> _serialList = new List<ReptPickSerials>();

            //AOD Out---------
            _inventoryHeader = null;
            if (_isStkUpdate == 1)
            {
                _inventoryHeader = new InventoryHeader();
                #region Inventory Header Value Assign
                _inventoryHeader.Ith_acc_no = string.Empty;
                _inventoryHeader.Ith_anal_1 = string.Empty;
                _inventoryHeader.Ith_anal_10 = false;//Direct AOD
                _inventoryHeader.Ith_anal_11 = false;
                _inventoryHeader.Ith_anal_12 = false;
                _inventoryHeader.Ith_anal_2 = string.Empty;
                _inventoryHeader.Ith_anal_3 = string.Empty;
                _inventoryHeader.Ith_anal_4 = string.Empty;
                _inventoryHeader.Ith_anal_5 = string.Empty;
                _inventoryHeader.Ith_anal_6 = 0;
                _inventoryHeader.Ith_anal_7 = 0;
                _inventoryHeader.Ith_anal_8 = Convert.ToDateTime(dtDate.Value).Date;
                _inventoryHeader.Ith_anal_9 = Convert.ToDateTime(dtDate.Value).Date;
                _inventoryHeader.Ith_bus_entity = string.Empty;
                _inventoryHeader.Ith_cate_tp = "NOR";
                _inventoryHeader.Ith_channel = string.Empty;
                _inventoryHeader.Ith_com = BaseCls.GlbUserComCode;
                _inventoryHeader.Ith_com_docno = string.Empty;
                _inventoryHeader.Ith_cre_by = BaseCls.GlbUserID;
                _inventoryHeader.Ith_cre_when = DateTime.Now.Date;
                _inventoryHeader.Ith_del_add1 = string.Empty;
                _inventoryHeader.Ith_del_add2 = string.Empty;
                _inventoryHeader.Ith_del_code = string.Empty;
                _inventoryHeader.Ith_del_party = string.Empty;
                _inventoryHeader.Ith_del_town = string.Empty;
                _inventoryHeader.Ith_direct = false;
                _inventoryHeader.Ith_doc_date = Convert.ToDateTime(dtDate.Value).Date;
                _inventoryHeader.Ith_doc_no = string.Empty;
                _inventoryHeader.Ith_doc_tp = "AOD";
                _inventoryHeader.Ith_doc_year = Convert.ToDateTime(dtDate.Value).Date.Year;
                _inventoryHeader.Ith_entry_no = string.Empty;
                _inventoryHeader.Ith_entry_tp = string.Empty;
                _inventoryHeader.Ith_git_close = false;
                _inventoryHeader.Ith_git_close_date = Convert.ToDateTime(dtDate.Value).Date;
                _inventoryHeader.Ith_git_close_doc = string.Empty;
                _inventoryHeader.Ith_is_manual = false;
                _inventoryHeader.Ith_isprinted = false;
                _inventoryHeader.Ith_sub_docno = _rccNo;
                _inventoryHeader.Ith_loading_point = string.Empty;
                _inventoryHeader.Ith_loading_user = string.Empty;
                _inventoryHeader.Ith_loc = BaseCls.GlbUserDefLoca;
                _inventoryHeader.Ith_manual_ref = "0";
                _inventoryHeader.Ith_mod_by = BaseCls.GlbUserID;
                _inventoryHeader.Ith_mod_when = DateTime.Now.Date;
                _inventoryHeader.Ith_noofcopies = 0;
                _inventoryHeader.Ith_oth_loc = txtIssueLoc.Text;
                _inventoryHeader.Ith_oth_docno = _rccNo;
                _inventoryHeader.Ith_remarks = string.Empty;
                _inventoryHeader.Ith_sbu = string.Empty;
                //_inventoryHeader.Ith_seq_no = 0; removed by Chamal 12-05-2013
                _inventoryHeader.Ith_session_id = BaseCls.GlbUserSessionID;
                _inventoryHeader.Ith_stus = "A";
                _inventoryHeader.Ith_sub_tp = "SERVICE";    // string.Empty; 10/7/2013
                _inventoryHeader.Ith_cate_tp = "NOR";
                _inventoryHeader.Ith_vehi_no = string.Empty;
                _inventoryHeader.Ith_oth_com = BaseCls.GlbUserComCode;
                _inventoryHeader.Ith_anal_1 = "0";
                _inventoryHeader.Ith_anal_2 = string.Empty;
                _inventoryHeader.Ith_job_no = _jobNo;
                #endregion

                string _message = string.Empty;
                string _genSalesDoc = string.Empty;


                _inventoryAuto.Aut_moduleid = "AOD";
                _inventoryAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                _inventoryAuto.Aut_cate_tp = "LOC";
                _inventoryAuto.Aut_direction = 0;
                _inventoryAuto.Aut_modify_dt = null;
                _inventoryAuto.Aut_year = DateTime.Now.Year;
                _inventoryAuto.Aut_start_char = "AOD";

                //Serials
                Int32 _serID = 0;
                foreach (DataGridViewRow row in dgvSelect.Rows)
                {
                    DataTable _dtSer = CHNLSVC.CustService.getSerialIDByDocument(dgvSelect.Rows[row.Index].Cells["JBDAODISSUENO"].Value.ToString(), dgvSelect.Rows[row.Index].Cells["jbd_itm_cd"].Value.ToString(), dgvSelect.Rows[row.Index].Cells["jbd_ser1"].Value.ToString());
                    if (_dtSer.Rows.Count > 0)  //kapila 17/2/2016
                    {
                        _serID = Convert.ToInt32(_dtSer.Rows[0]["its_ser_id"]);
                    }
                    else
                    {
                        if (_errorcorrection == "N")
                        {
                            MessageBox.Show("Cannot process. Available serial not found !", "Infor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        return;
                    }

                    _serialList = new List<ReptPickSerials>();
                    ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                    _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbDefaultBin, dgvSelect.Rows[row.Index].Cells["jbd_itm_cd"].Value.ToString(), _serID);
                    if (string.IsNullOrEmpty(_reptPickSerial_.Tus_ser_1))
                    {
                        //28/8/2015
                        DataTable _dt = CHNLSVC.Inventory.GetSerialIDByJob(_jobNo, dgvSelect.Rows[row.Index].Cells["jbd_itm_cd"].Value.ToString());
                        if (_dt.Rows.Count > 0)
                        {
                            _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbDefaultBin, dgvSelect.Rows[row.Index].Cells["jbd_itm_cd"].Value.ToString(), Convert.ToInt32(_dt.Rows[0]["ins_ser_id"]));
                            if (string.IsNullOrEmpty(_reptPickSerial_.Tus_ser_1))
                            {
                                if (_errorcorrection == "N")
                                {
                                    MessageBox.Show("Cannot process. Available serial not found !", "Infor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                return;
                            }
                        }
                        else
                        {       //26/9/2015
                            if (_errorcorrection == "N")
                            {
                                MessageBox.Show("Cannot process. Available serial not found !", "Infor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            return;
                        }
                    }
                    _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                    _reptPickSerial_.Tus_usrseq_no = 1;
                    _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                    _reptPickSerial_.Tus_base_doc_no = "N/A";
                    _reptPickSerial_.Tus_base_itm_line = 0;
                    _reptPickSerial_.Tus_new_remarks = "AOD-OUT";       //kapila
                    _reptPickSerial_.Tus_session_id = BaseCls.GlbUserSessionID;

                    MasterItem msitem = new MasterItem();
                    msitem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, dgvSelect.Rows[row.Index].Cells["jbd_itm_cd"].Value.ToString());
                    _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                    _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                    _serialList.Add(_reptPickSerial_);
                }

            }

            RCC _rcc = new RCC();
            if (_jobSbType == "RCC")
            {
                _rcc.Inr_is_repaired = true;
                _rcc.Inr_no = _rccNo;
                _rcc.Inr_is_returned = true;
                _rcc.Inr_hollogram_no = "SCM2";

            }

            #region ACRequest Tharindu 2018-04-04


            MasterAutoNumber _masterAutoDo = new MasterAutoNumber();
            _masterAutoDo.Aut_cate_cd = BaseCls.GlbUserDefLoca;
            _masterAutoDo.Aut_cate_tp = "LOC";
            _masterAutoDo.Aut_direction = 0;
            _masterAutoDo.Aut_moduleid = "DO";
            _masterAutoDo.Aut_start_char = "DO";
            _masterAutoDo.Aut_year = dtDate.Value.Year;
          
            DataTable jobhdr = new DataTable();
            DataTable jobdet = new DataTable();
            Service_Req_Hdr _jobHeader = new Service_Req_Hdr();
            Service_Req_Det _jobdetail = new Service_Req_Det();

            jobhdr = CHNLSVC.CustService.GetJobHeader(_jobNo, BaseCls.GlbUserComCode);
            jobdet = CHNLSVC.CustService.GetJobDetail(_jobNo, BaseCls.GlbUserComCode);

            string jobcat = jobdet.Rows[0]["jbd_cate1"].ToString();
           // string jobcat = jobhdr.Rows[0]["sjb_jobcat"].ToString();
            string warrstatus = jobdet.Rows[0]["jbd_warr_stus"].ToString();

            if (jobcat == "AC" && warrstatus == "1") 
            {
                if (MessageBox.Show("Do you want continue without free service shedule ?", "Job Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                }
                else
                {

                    if (lstService.Count <= 0)
                    {
                        MessageBox.Show("Cannot process. Please fill The Service Deatils", "Infor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    #region JobHeaderData
                    foreach (DataRow _dr in jobhdr.Rows)
                    {
                        _jobHeader.Srb_seq_no = _dr["SJB_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(_dr["SJB_SEQ_NO"].ToString());
                        _jobHeader.Srb_reqno = _dr["SJB_REQNO"].ToString();
                        _jobHeader.Srb_dt = Convert.ToDateTime(_dr["SJB_DT"].ToString());
                        _jobHeader.Srb_com = _dr["SJB_COM"].ToString();
                        _jobHeader.Srb_jobcat = "FF";
                        _jobHeader.Srb_jobtp = _dr["SJB_JOBTP"].ToString();
                        _jobHeader.Srb_jobstp = _dr["SJB_JOBSTP"].ToString();
                        _jobHeader.Srb_manualref = _dr["SJB_MANUALREF"].ToString();
                        _jobHeader.Srb_otherref = _jobNo;
                        _jobHeader.Srb_jobstage = 1; // put due to the service level convertion always this request stage is 1 
                        _jobHeader.Srb_rmk = _dr["SJB_RMK"].ToString();
                        _jobHeader.Srb_prority = _dr["SJB_PRORITY"].ToString();
                        _jobHeader.Srb_st_dt = Convert.ToDateTime(_dr["SJB_ST_DT"].ToString());
                        _jobHeader.Srb_ed_dt = Convert.ToDateTime(_dr["SJB_ED_DT"].ToString());
                        _jobHeader.Srb_noofprint = Convert.ToInt32(_dr["SJB_NOOFPRINT"].ToString());
                        _jobHeader.Srb_lastprintby = _dr["SJB_LASTPRINTBY"].ToString();
                        _jobHeader.Srb_orderno = _dr["SJB_ORDERNO"].ToString();
                        _jobHeader.Srb_custexptdt = Convert.ToDateTime(_dr["SJB_CUSTEXPTDT"].ToString());
                        _jobHeader.Srb_substage = _dr["SJB_SUBSTAGE"].ToString();
                        _jobHeader.Srb_cust_cd = _dr["SJB_CUST_CD"].ToString();
                        _jobHeader.Srb_cust_tit = _dr["SJB_CUST_TIT"].ToString();
                        _jobHeader.Srb_cust_name = _dr["SJB_CUST_NAME"].ToString();
                        _jobHeader.Srb_nic = _dr["SJB_NIC"].ToString();
                        _jobHeader.Srb_dl = _dr["SJB_DL"].ToString();
                        _jobHeader.Srb_pp = _dr["SJB_PP"].ToString();
                        _jobHeader.Srb_mobino = _dr["SJB_MOBINO"].ToString();
                        _jobHeader.Srb_add1 = _dr["SJB_ADD1"].ToString();
                        _jobHeader.Srb_add2 = _dr["SJB_ADD2"].ToString();
                        _jobHeader.Srb_add3 = _dr["SJB_ADD3"].ToString();
                        _jobHeader.Srb_b_cust_cd = _dr["SJB_B_CUST_CD"].ToString();
                        _jobHeader.Srb_b_cust_tit = _dr["SJB_B_CUST_TIT"].ToString();
                        _jobHeader.Srb_b_cust_name = _dr["SJB_B_CUST_NAME"].ToString();
                        _jobHeader.Srb_b_nic = _dr["SJB_B_NIC"].ToString();
                        _jobHeader.Srb_b_dl = _dr["SJB_B_DL"].ToString();
                        _jobHeader.Srb_b_pp = _dr["SJB_B_PP"].ToString();
                        _jobHeader.Srb_mobino = _dr["SJB_B_MOBINO"].ToString();
                        _jobHeader.Srb_b_town = _dr["SJB_B_TOWN"].ToString();
                        _jobHeader.Srb_b_phno = _dr["SJB_B_PHNO"].ToString();
                        _jobHeader.Srb_b_fax = _dr["SJB_B_FAX"].ToString();
                        _jobHeader.Srb_b_email = _dr["SJB_B_EMAIL"].ToString();
                        _jobHeader.Srb_infm_person = _dr["SJB_INFM_PERSON"].ToString();
                        _jobHeader.Srb_infm_add1 = _dr["SJB_INFM_ADD1"].ToString();
                        _jobHeader.Srb_infm_add2 = _dr["SJB_INFM_ADD2"].ToString();
                        _jobHeader.Srb_infm_phno = _dr["SJB_INFM_PHNO"].ToString();
                        _jobHeader.Srb_stus = _dr["SJB_STUS"].ToString();
                        _jobHeader.Srb_cre_by = _dr["SJB_CRE_BY"].ToString();
                        _jobHeader.Srb_mod_by = _dr["SJB_MOD_BY"].ToString();
                        _jobHeader.Srb_mod_dt = Convert.ToDateTime(_dr["SJB_MOD_DT"].ToString());

                    }
                    #endregion

                    #region JobDetailData
                    _scvItemList = new List<Service_Req_Det>();
                    foreach (DataRow _dr in jobdet.Rows)
                    {
                        _jobdetail.Jrd_seq_no = Convert.ToInt32(_dr["JBD_SEQ_NO"].ToString());
                        _jobdetail.Jrd_reqno = _dr["JBD_JOBNO"].ToString();
                        //_jobdetail.Jrd_reqline = Convert.ToInt32(_dr["JBD_REQLINE"].ToString());
                        _jobdetail.Jrd_reqline = 1;
                        _jobdetail.Jrd_sjobno = _dr["JBD_JOBNO"].ToString();
                        _jobdetail.Jrd_loc = _dr["JBD_LOC"].ToString();
                        _jobdetail.Jrd_pc = _dr["JBD_PC"].ToString();
                        _jobdetail.Jrd_sjobno = _dr["JBD_SJOBNO"].ToString();
                        _jobdetail.Jrd_loc = txtscvloac.Text.ToString();// _dr["JBD_LOC"].ToString();
                        _jobdetail.Jrd_pc = txtscvprocnter.Text.ToString();// _dr["JBD_PC"].ToString();
                        _jobdetail.Jrd_itm_cd = _dr["JBD_ITM_CD"].ToString();
                        _jobdetail.Jrd_itm_stus = _dr["JBD_ITM_STUS"].ToString();
                        _jobdetail.Jrd_itm_desc = _dr["JBD_ITM_DESC"].ToString();
                        _jobdetail.Jrd_brand = _dr["JBD_BRAND"].ToString();
                        _jobdetail.Jrd_model = _dr["JBD_MODEL"].ToString();
                        _jobdetail.Jrd_itm_cost = Convert.ToDecimal(_dr["JBD_ITM_COST"].ToString());
                        _jobdetail.Jrd_ser1 = _dr["JBD_SER1"].ToString();
                        _jobdetail.Jrd_ser2 = _dr["JBD_SER2"].ToString();
                        _jobdetail.Jrd_warr = _dr["JBD_WARR"].ToString();
                        _jobdetail.Jrd_regno = _dr["JBD_REGNO"].ToString();
                        _jobdetail.Jrd_milage = Convert.ToInt32(_dr["JBD_MILAGE"].ToString());
                        _jobdetail.Jrd_warr_stus = Convert.ToInt32(_dr["JBD_WARR_STUS"].ToString());
                        _jobdetail.Jrd_onloan = Convert.ToInt32(_dr["JBD_ONLOAN"].ToString());
                        _jobdetail.Jrd_chg_warr_stdt = Convert.ToDateTime(_dr["JBD_CHG_WARR_STDT"].ToString());
                        _jobdetail.Jrd_chg_warr_rmk = _dr["JBD_CHG_WARR_RMK"].ToString();
                        //  _jobHeader.Jrd_sentwcn
                        _jobdetail.Jrd_isinsurance = Convert.ToInt32(_dr["JBD_ISINSURANCE"].ToString());
                        _jobdetail.Jrd_ser_term = Convert.ToInt32(_dr["JBD_SER_TERM"].ToString());
                        _jobdetail.Jrd_lastwarr_stdt = Convert.ToDateTime(_dr["JBD_LASTWARR_STDT"].ToString());
                        _jobdetail.Jrd_issued = Convert.ToInt32(_dr["JBD_ISSUED"].ToString());
                        _jobdetail.Jrd_mainitmcd = _dr["JBD_MAINITMCD"].ToString();
                        _jobdetail.Jrd_mainitmser = _dr["JBD_MAINITMSER"].ToString();
                        _jobdetail.Jrd_mainitmwarr = _dr["JBD_MAINITMWARR"].ToString();
                        _jobdetail.Jrd_itmmfc = _dr["JBD_ITMMFC"].ToString();
                        _jobdetail.Jrd_mainitmmfc = _dr["JBD_MAINITMMFC"].ToString();
                        _jobdetail.Jrd_availabilty = Convert.ToInt32(_dr["JBD_AVAILABILTY"].ToString());
                        _jobdetail.Jrd_usejob = _dr["JBD_USEJOB"].ToString();
                        _jobdetail.Jrd_msnno = _dr["JBD_MSNNO"].ToString();
                        _jobdetail.Jrd_itmtp = _dr["JBD_ITMTP"].ToString();
                        _jobdetail.Jrd_serlocchr = _dr["JBD_SERLOCCHR"].ToString();
                        _jobdetail.Jrd_custnotes = _dr["JBD_CUSTNOTES"].ToString();
                        _jobdetail.Jrd_mainreqno = _dr["JBD_MAINREQNO"].ToString();
                        _jobdetail.Jrd_mainreqloc = _dr["JBD_MAINREQLOC"].ToString();
                        _jobdetail.Jrd_mainjobno = _dr["JBD_MAINJOBNO"].ToString();
                        _jobdetail.Jrd_isstockupdate = Convert.ToInt32(_dr["JBD_ISSTOCKUPDATE"].ToString());
                        _jobdetail.Jrd_needgatepass = Convert.ToInt32(_dr["JBD_ISGATEPASS"].ToString());
                        _jobdetail.Jrd_iswrn = Convert.ToInt32(_dr["JBD_ISWRN"].ToString());
                        _jobdetail.Jrd_warrperiod = Convert.ToInt32(_dr["JBD_WARRPERIOD"].ToString());
                        _jobdetail.Jrd_warrrmk = _dr["JBD_WARRRMK"].ToString();
                        _jobdetail.Jrd_warrstartdt = Convert.ToDateTime(_dr["JBD_WARRSTARTDT"].ToString());
                        _jobdetail.Jrd_warrreplace = Convert.ToInt32(_dr["JBD_WARRREPLACE"].ToString());
                        _jobdetail.Jrd_date_pur = Convert.ToDateTime(_dr["JBD_DATE_PUR"].ToString());
                        _jobdetail.Jrd_invc_no = _dr["JBD_INVC_NO"].ToString();
                        _jobdetail.Jrd_waraamd_seq = _dr["JBD_WARAAMD_SEQ"].ToString();
                        _jobdetail.Jrd_waraamd_by = _dr["JBD_WARAAMD_BY"].ToString();
                        _jobdetail.Jrd_waraamd_dt = Convert.ToDateTime(_dr["JBD_WARAAMD_DT"].ToString());
                        _jobdetail.Jrd_invc_showroom = _dr["JBD_INVC_SHOWROOM"].ToString();
                        _jobdetail.Jrd_aodissueloc = _dr["JBD_AODISSUELOC"].ToString();
                        _jobdetail.Jrd_aodissuedt = Convert.ToDateTime(_dr["JBD_AODISSUEDT"].ToString());
                        _jobdetail.Jrd_aodissueno = _dr["JBD_AODRECNO"].ToString();
                        _jobdetail.Jrd_aodrecno = _dr["JBD_AODISSUENO"].ToString();
                        _jobdetail.Jrd_techst_dt = Convert.ToDateTime(_dr["JBD_TECHST_DT"].ToString());
                        _jobdetail.Jrd_techfin_dt = Convert.ToDateTime(_dr["JBD_TECHFIN_DT"].ToString());
                        //    _jobdetail.Jrd_msn_no = _dr["JBD_MSNNO"].ToString();
                        _jobdetail.Jrd_isexternalitm = Convert.ToInt32(_dr["JBD_ISEXTERNALITM"].ToString());
                        _jobdetail.Jrd_conf_dt = Convert.ToDateTime(_dr["JBD_CONF_DT"].ToString());
                        _jobdetail.Jrd_conf_cd = _dr["JBD_CONF_CD"].ToString();
                        _jobdetail.Jrd_conf_desc = _dr["JBD_CONF_DESC"].ToString();
                        _jobdetail.Jrd_conf_rmk = _dr["JBD_CONF_RMK"].ToString();
                        _jobdetail.Jrd_tranf_by = _dr["JBD_TRANF_BY"].ToString();
                        _jobdetail.Jrd_tranf_dt = Convert.ToDateTime(_dr["JBD_TRANF_DT"].ToString());
                        _jobdetail.Jrd_do_invoice = Convert.ToInt32(_dr["JBD_DO_INVOICE"].ToString());
                        _jobdetail.Jrd_insu_com = _dr["JBD_INSU_COM"].ToString();
                        _jobdetail.Jrd_agreeno = _dr["JBD_AGREENO"].ToString();
                        _jobdetail.Jrd_issrn = Convert.ToInt32(_dr["JBD_ISSRN"].ToString());
                        _jobdetail.Jrd_isagreement = _dr["JBD_ISAGREEMENT"].ToString();
                        _jobdetail.Jrd_cust_agreeno = _dr["JBD_CUST_AGREENO"].ToString();
                        _jobdetail.Jrd_quo_no = _dr["JBD_QUO_NO"].ToString();
                        _jobdetail.Jrd_stage = 1;
                        _jobdetail.Jrd_com = _dr["JBD_COM"].ToString();
                        _jobdetail.Jrd_ser_id = _dr["JBD_SER_ID"].ToString();
                        _jobdetail.Jrd_used = 0;
                        _jobdetail.Jrd_jobno = "";
                        _jobdetail.Jrd_jobline = 0;
                        //     _jobdetail.Jrd_Select = Convert.ToBoolean(_dr["JBD_SJOBNO"].ToString());
                        _jobdetail.Jrd_supp_cd = _dr["JBD_SUPP_CD"].ToString();

                        //_jobdetail.Jrd_used = Convert.ToInt32(_dr["JBD_ACT"].ToString()); due to the qa issues
                        //_jobdetail.Jrd_jobno = _dr["JBD_JOBNO"].ToString();
                        //_jobdetail.Jrd_jobline = Convert.ToInt32(_dr["JBD_JOBLINE"].ToString());
                        //_jobdetail.Jrd_stage = Convert.ToDecimal(_dr["JBD_STAGE"].ToString());

                        _scvItemList.Add(_jobdetail);
                    }
                    #endregion

                    string jobNo;
                    string receiptNo = string.Empty;
                    string _msg1 = "";
                    MasterAutoNumber _jobAuto1 = new MasterAutoNumber();
                    Int32 _warStus = 0;


                    #region Job Auto Number
                    _jobAuto1.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                    _jobAuto1.Aut_cate_tp = "LOC";
                    _jobAuto1.Aut_moduleid = "SVREQ";
                    _jobAuto1.Aut_direction = 0;
                    _jobAuto1.Aut_year = _jobHeader.Srb_dt.Year;
                    _jobAuto1.Aut_start_char = "SVREQ";
                    #endregion

                    for (int i = 0; i < lstService.Count; i++)
                    {
                        _jobHeader.Srb_dt = lstService[i].Servicedates;
                        _jobHeader.Srb_st_dt = lstService[i].Servicedates;
                        _jobHeader.Srb_ed_dt = lstService[i].Servicedates;
                        _jobHeader.Srb_custexptdt = lstService[i].Servicedates;

                        int eff = CHNLSVC.CustService.Save_Req(_jobHeader, _scvItemList, _scvDefList, _scvItemSubList, _jobAuto1, BaseCls.GlbDefSubChannel, "", "", _warStus, _jobAuto1, out _msg1, out jobNo, 0, DateTime.Now.Date, DateTime.Now.Date);

                    }
                }
            }

            #endregion


            //save---------
            string _msg = "";
            string _docNo = "";
            Int32 _eff = CHNLSVC.CustService.Process_Gate_Pass(_selectedItemList, _rcc, _inventoryHeader, _inventoryAuto, _serialList, BaseCls.GlbUserDefLoca, dtDate.Value.Date, txtSatisLevel.Text, txtSatRem.Text, BaseCls.GlbUserID, cmbMethod.Text, txtRef.Text, masterAutoNum, _masterAutoDo, BaseCls.GlbUserSessionID, out _msg, out _docNo);
          
            if (_eff == 1)
            {

                if (_jobSbType == "RCC")
                {
                    Service_Chanal_parameter _Parameters = null;
                    _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
                    if (_Parameters != null)
                    {
                        if (_Parameters.SP_CLS_STAGE_WS == 11)
                        {
                            RCC _RCCN = new RCC();
                            Service_JOB_HDR oJobHeader = CHNLSVC.CustService.GetServiceJobHeader(_jobNo, BaseCls.GlbUserComCode);
                            _RCCN = CHNLSVC.Inventory.GetRccByNo(oJobHeader.SJB_REQNO);
                            if (_RCCN.Inr_no != null)
                                CHNLSVC.CustService.SendConfirmationMail(BaseCls.GlbUserComCode, _RCCN.Inr_loc_cd, BaseCls.GlbUserDefLoca, _jobNo, _RCCN.Inr_no, dtDate.Value.Date, "", BaseCls.GlbDefSubChannel, BaseCls.GlbUserID);

                        }
                    }
                }

                if (_errorcorrection == "N")
                {
                    MessageBox.Show(_msg, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                clear();


                if (_errorcorrection == "N")
                {
                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    _view.GlbReportName = "Job_Gatepass.rpt";
                    BaseCls.GlbReportName = "Job_Gatepass.rpt";
                    BaseCls.GlbReportTp = "GPASS";
                    _view.GlbReportDoc = _docNo;
                    BaseCls.GlbReportDoc = _docNo;
                    _view.Show();
                    _view = null;
                }

            }
            else
            {
                if (_errorcorrection == "N")
                {
                    MessageBox.Show(_msg, "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void sendMsg(string _jobNo)
        {
            string outMsg;
            Service_Message_Template oTemplate = CHNLSVC.CustService.GetMessageTemplates_byID(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, 9);
            Service_Message oMessage = new Service_Message();
            if (oTemplate != null && oTemplate.Sml_templ_mail != null)
            {
                Service_JOB_HDR oJobHeader = CHNLSVC.CustService.GetServiceJobHeader(_jobNo, BaseCls.GlbUserComCode);
                List<Service_job_Det> oItms = CHNLSVC.CustService.GetJobDetails(_jobNo, 1, BaseCls.GlbUserComCode);
                string emailBody = oTemplate.Sml_templ_mail;
                foreach (Service_job_Det _jDet in oItms)
                {
                    emailBody = emailBody.Replace("[rccNo]", _jDet.Jbd_reqno);
                    emailBody = emailBody.Replace("[ItmSerial]", _jDet.Jbd_ser1);
                }

                emailBody = emailBody.Replace("[jobNo]", oJobHeader.SJB_JOBNO);
                emailBody = emailBody.Replace("[Cust]", oJobHeader.SJB_CUST_NAME);

                oMessage.Sm_com = BaseCls.GlbUserComCode;
                oMessage.Sm_jobno = oJobHeader.SJB_JOBNO;
                oMessage.Sm_joboline = 1;
                oMessage.Sm_jobstage = oJobHeader.SJB_JOBSTAGE;
                oMessage.Sm_ref_num = string.Empty;
                oMessage.Sm_status = 0;
                oMessage.Sm_msg_tmlt_id = 9;
                oMessage.Sm_sms_text = string.Empty;
                oMessage.Sm_sms_gap = 0;
                oMessage.Sm_sms_done = 0;
                oMessage.Sm_mail_text = emailBody;
                oMessage.Sm_mail_gap = 0;
                oMessage.Sm_email_done = 0;
                oMessage.Sm_cre_by = BaseCls.GlbUserID;
                oMessage.Sm_cre_dt = DateTime.Now;
                oMessage.Sm_mod_by = BaseCls.GlbUserID;
                oMessage.Sm_mod_dt = DateTime.Now;
                int result = CHNLSVC.CustService.SaveServiceMsg(oMessage, out outMsg);
            }
        }

        private void ServiceGatePass_Load(object sender, EventArgs e)
        {
            _scvParam = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_scvParam == null)
            {
                SystemWarnningMessage("Service parameter(s) not setup!", "Default Parameter(s)");
                this.Enabled = false;
            }
            if (_errorcorrection == "N")
            {
                btn_errcorr.Visible=false;
                if (_scvParam.SP_ISNEEDGATEPASS == 0)
                {
                    SystemWarnningMessage("Not allowed for gatepass !", "Default Parameter(s)");
                    this.Enabled = false;
                }
            }

            BackDatePermission();

            pnlService.Visible = false;

            //updated by akila 2017/09/02 - check back date enabale controls
            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty.ToUpper().ToString(), this.GlbModuleName, dtDate, lblBackDateInfor, dtDate.Value.Date.ToString(), out _allowCurrentTrans))
            {
                if (_allowCurrentTrans == true){if (dtDate.Value.Date != DateTime.Now.Date){dtDate.Enabled = true;}}
                else{dtDate.Enabled = true;}
            }

            DataTable _dt = CHNLSVC.CustService.getTransportMethod();
            cmbMethod.DataSource = _dt;
            cmbMethod.DisplayMember = "RTM_TP";
            cmbMethod.ValueMember = "RTM_TP";
            cmbMethod.SelectedIndex = -1;
        }

        private void txtSatisLevel_Leave(object sender, EventArgs e)
        {
            load_satisDet();
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

        private void btnGatePassNo_Click(object sender, EventArgs e)
        {
            txtReqNo_DoubleClick(null, null);
            btnSave.Enabled = false;
        }

        private void txtReqNo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = null;
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceGatePass);
                _result = CHNLSVC.CommonSearch.GatePassSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtReqNo;
                _CommonSearch.ShowDialog();
                txtReqNo.Select();

                load_det_by_gatepass_no(txtReqNo.Text);

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void load_det_by_gatepass_no(string _docno)
        {
            DataTable _dt = CHNLSVC.CustService.sp_get_gatepass_details(_docno);
            if (_dt.Rows.Count>0)
                txtJobNo.Text=_dt.Rows[0]["SGP_JOBNO"].ToString();
            else
                txtJobNo.Text="";
        }

        private void txtReqNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                txtReqNo_DoubleClick(null, null);
        }

        private void txtSatisLevel_DoubleClick(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = null;
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceCustSatisfact);
            _result = CHNLSVC.CommonSearch.CustSatisfacSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSatisLevel;
            _CommonSearch.ShowDialog();
            txtSatisLevel.Select();
            load_satisDet();
        }

        private void load_satisDet()
        {
            if (!string.IsNullOrEmpty(txtSatisLevel.Text))
            {
                MasterLocation _mstLoc = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                DataTable _dt = CHNLSVC.CustService.getCustSatisData(BaseCls.GlbUserComCode, _mstLoc.Ml_cate_3, txtSatisLevel.Text);
                if (_dt.Rows.Count > 0)
                    lblSatis.Text = _dt.Rows[0]["SCST_DESC"].ToString();
                else
                {
                    MessageBox.Show("Invalid Satisfaction level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSatisLevel.Text = "";
                    lblSatis.Text = "";
                    txtSatisLevel.Focus();
                }

            }
        }

        private void btnSrchCustSat_Click(object sender, EventArgs e)
        {
            txtSatisLevel_DoubleClick(null, null);
        }

        private void txtSatisLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                txtSatisLevel_DoubleClick(null, null);
            if (e.KeyCode == Keys.Enter)
                txtSatRem.Focus();
        }

        private void dgvSelect_DoubleClick(object sender, EventArgs e)
        {

        }

        private void dgvSelect_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {

                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _selectedItemList.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _selectedItemList;
                    dgvSelect.DataSource = _source;
                }
            }
        }

        private void btnJobTask_Click(object sender, EventArgs e)
        {
            ServiceTasks frm = new ServiceTasks(txtJobNo.Text, 0);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
            frm.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
         
                Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                _view.GlbReportName = "Job_Gatepass.rpt";
                BaseCls.GlbReportName = "Job_Gatepass.rpt";
                BaseCls.GlbReportTp = "GPASS";
                _view.GlbReportDoc = txtReqNo.Text;
                BaseCls.GlbReportDoc = txtReqNo.Text;
                _view.Show();
                _view = null;
            
        }

        private void btnIssueLoc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtIssueLoc;
                _CommonSearch.ShowDialog();
                txtIssueLoc.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void SystemErrorMessage(Exception ex)
        {
            CHNLSVC.CloseChannel();
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        private void txtIssueLoc_DoubleClick(object sender, EventArgs e)
        {
            btnIssueLoc_Click(null, null);
        }

        private void txtIssueLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnIssueLoc_Click(null, null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string _rccno = "";
            string _rccLoc = "";

            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10145))
            {
                MessageBox.Show("Sorry, You have no permission to cancel the gate pass!\n( Advice: Required permission code :10145 )", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please select the job number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty.ToUpper().ToString(), this.GlbModuleName, dtDate, lblBackDateInfor, dtDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (dtDate.Value.Date != DateTime.Now.Date)
                    {
                        dtDate.Enabled = true;
                        MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtDate.Focus();
                        return;
                    }
                }
                else
                {
                    dtDate.Enabled = true;
                    MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtDate.Focus();
                    return;
                }
            }

            DataTable _dt = CHNLSVC.CustService.get_gatepass_byjob(txtJobNo.Text,dtDate.Value.Date);
            if (_dt.Rows.Count == 0)
            {
                MessageBox.Show("Gate pass is not found for the job number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                if (_dt.Rows[0]["SJB_JOBSTP"].ToString() == "RCC")
                {
                    //check whether RCC is completed
                    RCC _rcc = CHNLSVC.Inventory.GetRccByNo(_dt.Rows[0]["SJB_REQNO"].ToString());
                    if (_rcc.Inr_stage == 4)
                    {
                        MessageBox.Show("Cannot Cancel. RCC is already completed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    //_rccno = _rcc.Inr_no;
                   // _rccLoc = _rcc.Inr_loc_cd;
                }
               // else
               // {
                    string _err = "";
                    Int32 _eff = CHNLSVC.CustService.Cancel_Gate_Pass(BaseCls.GlbUserComCode, txtJobNo.Text,dtDate.Value.Date, _rccno,_rccLoc,BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, out _err);
                    if (_eff == 1)
                    {
                        MessageBox.Show("Successfully Cancelled !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                    else
                    {
                        MessageBox.Show(_err, "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                //}
                
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
         
        }

        //private void insert_missing_inv_items(string _jobno)
        //{


        //}

        private void btn_errcorr_Click(object sender, EventArgs e)
        {
            if (_errorcorrection == "Y")
            {
                DataTable _errgp = CHNLSVC.CustService.GetErrJobList();

                if (_errgp.Rows.Count > 0)
                {
                    foreach (DataRow drow in _errgp.Rows)
                    {
                        //insert_missing_inv_items(drow["job_no"].ToString());
                        txtJobNo.Text = drow["job_no"].ToString();
                        txtJobNo_Leave(null, null);
                        int x = CHNLSVC.CustService.UpdatesatitmDOqty_GP(drow["job_no"].ToString());

                        btn_add_ser_Click(null, null);

                        txtSatisLevel.Text = "1";
                        cmbMethod.SelectedIndex = 2;

                        btnSave_Click(null, null);

                        btnClear_Click(null, null);
                    }
                    MessageBox.Show("Completed !", "Infor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("No Records Avaialble !", "Infor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnCreateservice_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtJobNo.Text))
                {
                      MessageBox.Show("Please enter valid job number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                        return;
                }
                if (pnlService.Visible)
                    pnlService.Visible = false;
                else
                    pnlService.Visible = true;

                txtDuration.Enabled = false;
            }
            catch (Exception ex )
            {
                
                 MessageBox.Show(ex.Message, "Infor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnServiceClose_Click(object sender, EventArgs e)
        {
            pnlService.Visible = false;
        }

        int Count = 0;
        int duration = 0;
        public List<Service_free_det> lstService = new List<Service_free_det>();
        private void btnServiceadd_Click(object sender, EventArgs e)
        {
            //Service_free_det objlist = new Service_free_det();
            if (Convert.ToDateTime(dtDate.Text).Date > Convert.ToDateTime(dtpFirstservceday.Text).Date)
            {
                MessageBox.Show("First service can;t back date", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                serviceClear();
                return;
            }
            if (string.IsNullOrEmpty(txtscvloac.Text))
            {
                MessageBox.Show("Select Free service Loaction", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtscvprocnter.Text))
            {
                MessageBox.Show("Select Free Profitcenter Loaction", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }  
            DateTime ct = dtpFirstservceday.Value.Date;
            DateTime val = DateTime.Now.Date;
         
            Count = Convert.ToInt32(string.IsNullOrEmpty(txtNoofservice.Text) ? "0" : txtNoofservice.Text);
            duration = Convert.ToInt32(string.IsNullOrEmpty(txtDuration.Text) ? "0" : txtDuration.Text);

            if(cbAutocreate.Checked)
            {
                lstService = new List<Service_free_det>();
                for (int i = 1; i < Count +1; i++)
                {
                    
                    Service_free_det objlist = new Service_free_det();

                    if (i == 1)
                    {
                        val = ct.AddDays(duration);
                    }
                    else
                    {
                        val = val.AddDays(duration);
                    }

                    objlist.Servicedates = val;
                    objlist.Description = "Serivce " + i;
                    objlist.Noofservices = i;
                    lstService.Add(objlist);
                }
              
            }
            else
            {
                Service_free_det objlist = new Service_free_det();
                objlist.Servicedates = ct;
                objlist.Description = "Serivce ";
                objlist.Noofservices = Count;
                lstService.Add(objlist);
            }

            if (cbAutocreate.Checked)
            {
                DataTable _dt1 = lstService.ToDataTable();
                dgvService.DataSource = null;
                dgvService.AutoGenerateColumns = false;
                dgvService.DataSource = _dt1;
                dgvService.ReadOnly = true;
            }
            else
            {
                DataTable _dt1 = lstService.ToDataTable();
                dgvService.DataSource = null;
                dgvService.AutoGenerateColumns = false;
                dgvService.DataSource = _dt1;
                dgvService.ReadOnly = false;
            }
                   
        }

        private void btnServiceclear_Click(object sender, EventArgs e)
        {
            serviceClear();
        }

        private void serviceClear()
        {
            txtNoofservice.Text = "";
            txtDuration.Text = "";
            cbAutocreate.Checked = false;
            lstService = new List<Service_free_det>();
            if (dgvService.DataSource != null)
            {
                dgvService.DataSource = null;
            }
        }

        private void LoadJob(string _com, string _jobNo, string _jobStage)
        {
            int _returnStatus = 0;
            string _returnMsg = string.Empty;

            _scvjobHdr = new Service_Req_Hdr();
            _scvItemList = new List<Service_Req_Det>();
            _scvDefList = new List<Service_Req_Def>();
            _scvEmpList = new List<Service_Tech_Aloc_Hdr>();
            _scvItemSubList = new List<Service_Req_Det_Sub>();
            _tempItemSubList = new List<Service_Req_Det_Sub>();
            _scvStdbyList = new List<Service_TempIssue>();


            _returnStatus = CHNLSVC.CustService.GetScvReq(_com, _jobNo, out _scvjobHdr, out  _scvItemList, out  _scvItemSubList, out  _scvDefList, out  _returnMsg);
            if (_returnStatus != 1)
            {
                //   SystemInformationMessage(_returnMsg, "Service Job");
                txtReqNo.Clear();
                txtReqNo.Focus();
                return;
            }

            if (_jobStage == "1.1")
            {
                if (_scvjobHdr.Srb_jobstage != Convert.ToDecimal(1.1))
                {
                    //    SystemInformationMessage("The job is not inspection stage!", "Inspection Job");
                    txtReqNo.Clear();
                    txtReqNo.Focus();
                    return;
                }
            }

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtscvloac.Text))
            {
                MessageBox.Show("Select Free service Loaction", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtscvprocnter.Text))
            {
                MessageBox.Show("Select Free Profitcenter Loaction", "Job Confirmation - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }  

            pnlService.Visible = false;
        }

        private void cbAutocreate_CheckedChanged(object sender, EventArgs e)
        {
            if(cbAutocreate.Checked)
            {
                txtDuration.Enabled = true;
                dgvService.ReadOnly = false;

                if(dgvService.DataSource != null)
                {
                    if (lstService.Count > 0)
                    {
                        lstService = new List<Service_free_det>();
                        DataTable _dt1 = lstService.ToDataTable();
                        dgvService.DataSource = null;
                        dgvService.AutoGenerateColumns = false;
                        dgvService.DataSource = _dt1;
                    }                   
                }
                
            }
            else
            {
                txtDuration.Enabled = false;
                dgvService.ReadOnly = true;
                txtDuration.Text = "";

                if (dgvService.DataSource != null)
                {
                    if (lstService.Count > 0)
                    {
                        lstService = new List<Service_free_det>();
                        DataTable _dt1 = lstService.ToDataTable();
                        dgvService.DataSource = null;
                        dgvService.AutoGenerateColumns = false;
                        dgvService.DataSource = _dt1;
                    }
                }
            }
            
        }

        private void dgvService_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(cbAutocreate.Checked == false)
            {
                if(lstService.Count > 0)
                {
                    lstService.RemoveAt(e.RowIndex);

                    DataTable _dt1 = lstService.ToDataTable();
                    dgvService.DataSource = null;
                    dgvService.AutoGenerateColumns = false;
                    dgvService.DataSource = _dt1;
                }
               
            }
          
        }

        private void dgvService_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCusComm_Click(object sender, EventArgs e)
        {
            CustomerSatisfaction frm = new CustomerSatisfaction(txtJobNo.Text, 0);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X, this.Location.Y + 60);
            frm.ShowDialog();
        }

        private void brnacsevloc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AC_SVC_ALW_LOC);
                _result = CHNLSVC.CommonSearch.Get_CLS_ALW_LOC_SearchData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtscvloac;
                _CommonSearch.ShowDialog();
                txtscvloac.Select();
                ValidateServiceLocation(txtscvloac.Text);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void ValidateServiceLocation(string _location)
        {
            try
            {
                DataTable _locationDetails = new DataTable();
                _locationDetails = CHNLSVC.Sales.GetLocationCode(BaseCls.GlbUserComCode, _location);
                if (_locationDetails.Rows.Count > 0)
                {
                    foreach (DataRow _loc in _locationDetails.Rows)
                    {
                        string _tmpLocType = _loc["ml_loc_tp"] == DBNull.Value ? string.Empty : _loc["ml_loc_tp"].ToString();
                        if (_tmpLocType == "SERC")
                        {
                            txtscvprocnter.Text = _loc["ml_def_pc"] == DBNull.Value ? string.Empty : _loc["ml_def_pc"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Select Valid Service Location", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtscvloac.Text = "";
                            txtscvloac.Focus();

                            return;
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Select Valid Service Location", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtscvloac.Text = "";
                    txtscvloac.Focus();

                    return;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtscvloac_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtscvloac.Text))
            {
                ValidateServiceLocation(txtscvloac.Text);
            }
        }
    }
}
