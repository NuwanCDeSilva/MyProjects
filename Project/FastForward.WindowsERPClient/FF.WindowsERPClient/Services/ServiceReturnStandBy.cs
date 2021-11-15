using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceReturnStandBy : Base
    {
        private Int32 _jobLine = 0;
        private string _jobNo = "";
        private bool transferJob = true;

        public ServiceReturnStandBy()
        {
            InitializeComponent();

            dgvItems.AutoGenerateColumns = false;
        }

        private void getJobJetails()
        {
            DataTable DtDetails = new DataTable();
            string stage = string.Empty;
            Int32 IsCusExpected = 0;
            transferJob = true;
            stage = "3,2,6,4,5.1,5,7";
            this.Text = "Stand-By Return (Transfered Jobs)";

            //txtTransFrom.Text = "";
            txtTransFrom.ReadOnly = true;
            //txtTransFrom.Enabled = false;
            label3.Text = "Transfer From";


            DtDetails = CHNLSVC.CustService.GetJObsFOrWIP(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1911"), Convert.ToDateTime("31-12-2999"), txtJobNo.Text, stage, IsCusExpected, string.Empty, BaseCls.GlbUserDefProf, "TRANS_JOBS");
            if (DtDetails.Rows.Count <= 0)
            {
                DtDetails = new DataTable();
                DtDetails = CHNLSVC.CustService.GetJObsFOrWIP(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1911"), Convert.ToDateTime("31-12-2999"), txtJobNo.Text, stage, IsCusExpected, string.Empty, BaseCls.GlbUserDefProf, "GET_ALL_JOBS");

                DataTable dt = CHNLSVC.General.get_loc_services(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "S");
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtTransFrom.Text = dt.Rows[0][0].ToString();
                }
                else
                {
                    MessageBox.Show("Service Stores not define!", "Stand by return", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnClear_Click(null, null);
                    return;
                }
                transferJob = false;
                this.Text = "Stand-By Return";
                label3.Text = "Transfer From";
                //txtTransFrom.Text = "";
                //txtTransFrom.ReadOnly = false;
                //txtTransFrom.Enabled = true; 
            }


            if (DtDetails.Rows.Count > 0)
            {
                DataView dv = DtDetails.DefaultView;
                dv.Sort = "jbd_jobline";
                DataTable sortedDT = dv.ToTable();

                dgvJobDetails.DataSource = sortedDT;
                //modifyJobDetailGrid();

                if (transferJob == true) txtTransFrom.Text = DtDetails.Rows[0]["stl_from_loc"].ToString();

                Service_JOB_HDR oJOB_HDR = CHNLSVC.CustService.GetServiceJobHeader(txtJobNo.Text, BaseCls.GlbUserComCode);

                lblName.Text = oJOB_HDR.SJB_B_CUST_TIT + " " + oJOB_HDR.SJB_B_CUST_NAME;
                lblAddrss.Text = oJOB_HDR.SJB_B_ADD1 + "  " + oJOB_HDR.SJB_B_ADD2 + "  " + oJOB_HDR.SJB_B_ADD3;
                lblTele.Text = oJOB_HDR.SJB_B_MOBINO;
                lblContactPerson.Text = oJOB_HDR.SJB_CNT_PERSON;
                lblConPhone.Text = oJOB_HDR.SJB_CNT_PHNO;
                lblCustomerCode.Text = oJOB_HDR.SJB_CUST_CD;


                //lblJobStage.Text = oJOB_HDR.SJB_JOBSTAGE_TEXT;
                //lblJobCategori.Text = oJOB_HDR.SJB_JOBCAT;
                //lblLevel.Text = oJOB_HDR.SJB_PRORITY;
                //txtInstruction.Text = oJOB_HDR.SJB_TECH_RMK;
                //txtJobRemarks.Text = oJOB_HDR.SJB_JOB_RMK;
                //lblJobStageNew.Text = oJOB_HDR.SJB_JOBSTAGE.ToString();
                //if (oJOB_HDR.SJB_JOBSTAGE > 3)
                //{
                //    button1.Enabled = false;
                //}
                //enableDisableBtns(false);

                load_temp_issue_items(0);

                //dgvJobDetails.Focus();

                if (sortedDT.Rows.Count == 1)
                {
                    //dgvJobDetails_CellClick(dgvJobDetails, new DataGridViewCellEventArgs(0, 0));
                }
            }
            else
            {
                MessageBox.Show("Please enter valid job number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnClear_Click(null, null);
                return;
            }
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


        private void btnVouNo_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
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


            _CommonSearch.ShowDialog();

            _CommonSearch.dvResult.Refresh();
            txtJobNo.Focus();
            txtJobNo_Leave(null, null);
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
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "3,6,4,5.1" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearchWIP:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "3,6,4,5.1" + seperator + "TRANS_JOBS" + seperator);
                        break;
                    }

                    break;
            }

            return paramsText.ToString();
        }

        private void txtJobNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                string job = txtJobNo.Text;
                clrScreen();
                txtJobNo.Text = job;
                getJobJetails();
            }
        }

        private void clrScreen()
        {
            dgvItems.AutoGenerateColumns = false;
            dgvItems.DataSource = null;

            dgvJobDetails.AutoGenerateColumns = false;
            dgvJobDetails.DataSource = null;

            lblName.Text = "";
            lblAddrss.Text = "";
            lblTele.Text = "";
            lblContactPerson.Text = "";
            lblConPhone.Text = "";
            lblCustomerCode.Text = "";

        }

        private void btnMoreDetails_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                ServiceJobDetailsViwer oJobDetailsViwer = new ServiceJobDetailsViwer(txtJobNo.Text, -777);
                oJobDetailsViwer.ShowDialog();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

            clearAll();
        }

        private void clearAll()
        {
            transferJob = true;
            dgvItems.DataSource = null;
            dgvJobDetails.DataSource = null;
            txtJobNo.Text = "";
            lblAddrss.Text = "";
            lblConPhone.Text = "";
            lblContactPerson.Text = "";
            lblCustomerCode.Text = "";
            lblName.Text = "";
            lblTele.Text = "";
            txtTransFrom.Text = "";
        }

        private void dgvJobDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                load_temp_issue_items(e.RowIndex);
            }
        }

        private void load_temp_issue_items(Int32 _index)
        {
            _jobNo = dgvJobDetails.Rows[_index].Cells["JobNo"].Value.ToString();
            _jobLine = Convert.ToInt32(dgvJobDetails.Rows[_index].Cells["JobLine"].Value);

            DataTable _dt = CHNLSVC.CustService.GetTempIssueItemsByJobline(BaseCls.GlbUserComCode, _jobNo, _jobLine, "STBYI");
            if (_dt == null || _dt.Rows.Count <= 0)
            {
                MessageBox.Show("Not found stand by issue items", "Stand by return", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                clearAll();
                return;
            }
            dgvItems.AutoGenerateColumns = false;
            dgvItems.DataSource = _dt;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (dgvItems.Rows.Count == 0)
            {
                MessageBox.Show("Temporary issue item not selected", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(txtTransFrom.Text))
            {
                MessageBox.Show("Plese select the transfer location?", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTransFrom.Focus(); 
                return; 
            }

            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            //aod OUT----------------------------------------------------------------------------------------------------------------------------
            #region AOD OUT
            InventoryHeader _inventoryHeader = new InventoryHeader();
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
            _inventoryHeader.Ith_anal_8 = DateTime.Now.Date;
            _inventoryHeader.Ith_anal_9 = DateTime.Now.Date;
            _inventoryHeader.Ith_bus_entity = string.Empty;
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
            _inventoryHeader.Ith_doc_date = DateTime.Now.Date;
            _inventoryHeader.Ith_doc_no = string.Empty;
            _inventoryHeader.Ith_doc_tp = "AOD";
            _inventoryHeader.Ith_doc_year = DateTime.Now.Date.Year;
            _inventoryHeader.Ith_entry_no = string.Empty;
            _inventoryHeader.Ith_entry_tp = string.Empty;
            _inventoryHeader.Ith_git_close = false;
            _inventoryHeader.Ith_git_close_date = DateTime.Now.Date;
            _inventoryHeader.Ith_git_close_doc = string.Empty;
            _inventoryHeader.Ith_is_manual = false;
            _inventoryHeader.Ith_isprinted = false;
            _inventoryHeader.Ith_sub_docno = txtJobNo.Text;
            _inventoryHeader.Ith_loading_point = string.Empty;
            _inventoryHeader.Ith_loading_user = string.Empty;
            _inventoryHeader.Ith_manual_ref = string.Empty;
            _inventoryHeader.Ith_mod_by = BaseCls.GlbUserID;
            _inventoryHeader.Ith_mod_when = DateTime.Now.Date;
            _inventoryHeader.Ith_noofcopies = 0;
            if (transferJob == true)
            {
                _inventoryHeader.Ith_loc = txtTransFrom.Text;
                _inventoryHeader.Ith_oth_loc = BaseCls.GlbUserDefLoca;
                _inventoryHeader.Ith_manual_ref = "Transfer Job";
                _inventoryHeader.Ith_stus = "F";
            }
            else
            {
                _inventoryHeader.Ith_stus = "A";
                _inventoryHeader.Ith_oth_loc = txtTransFrom.Text;
                _inventoryHeader.Ith_loc = BaseCls.GlbUserDefLoca;
            }
            _inventoryHeader.Ith_oth_docno = txtJobNo.Text;
            _inventoryHeader.Ith_remarks = string.Empty;
            _inventoryHeader.Ith_sbu = string.Empty;
            //_inventoryHeader.Ith_seq_no = 0; removed by Chamal 12-05-2013
            _inventoryHeader.Ith_session_id = BaseCls.GlbUserSessionID;
            
            _inventoryHeader.Ith_sub_tp = "STDBY";    // string.Empty; 10/7/2013
            _inventoryHeader.Ith_cate_tp = "SERVICE";
            _inventoryHeader.Ith_vehi_no = string.Empty;
            _inventoryHeader.Ith_oth_com = BaseCls.GlbUserComCode;
            _inventoryHeader.Ith_anal_1 = "0";
            _inventoryHeader.Ith_anal_2 = string.Empty;
            _inventoryHeader.Ith_isjobbase = true;

            string _message = string.Empty;
            string _genSalesDoc = string.Empty;

            MasterAutoNumber _inventoryAuto = new MasterAutoNumber();
            _inventoryAuto.Aut_moduleid = "AOD";
            if (transferJob == true)
            { _inventoryAuto.Aut_cate_cd = txtTransFrom.Text; }
            else
            { _inventoryAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca; }
            _inventoryAuto.Aut_cate_tp = "LOC";
            _inventoryAuto.Aut_direction = 0;
            _inventoryAuto.Aut_modify_dt = null;
            _inventoryAuto.Aut_year = DateTime.Now.Year;
            _inventoryAuto.Aut_start_char = "AOD";


            //Serials
            List<ReptPickSerials> _serialList = new List<ReptPickSerials>();
            ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbDefaultBin, dgvItems.Rows[0].Cells["STI_ISSUEITMCD"].Value.ToString(), Convert.ToInt32(dgvItems.Rows[0].Cells["sti_issueserid"].Value));
            _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
            _reptPickSerial_.Tus_usrseq_no = 0;
            _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
            _reptPickSerial_.Tus_base_doc_no = "N/A";
            _reptPickSerial_.Tus_base_itm_line = 0;
            _reptPickSerial_.Tus_new_remarks = "AOD-OUT";       //kapila

            MasterItem msitem = new MasterItem();
            msitem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, dgvItems.Rows[0].Cells["STI_ISSUEITMCD"].Value.ToString());
            _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
            _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
            _serialList.Add(_reptPickSerial_);
            if (transferJob == false)
            {
                foreach (ReptPickSerials _outSer in _serialList)
                {
                    _outSer.Tus_loc = BaseCls.GlbUserDefLoca;
                    _outSer.Tus_bin = CHNLSVC.Inventory.Get_default_binCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                }
            }

            #endregion
            //aod IN---------------------------------------------------------------------------------------------------------------------------------
            #region AOD IN
            InventoryHeader _inventoryINHeader = new InventoryHeader();
            _inventoryINHeader.Ith_acc_no = string.Empty;
            _inventoryINHeader.Ith_anal_1 = string.Empty;
            _inventoryINHeader.Ith_anal_10 = false;//Direct AOD
            _inventoryINHeader.Ith_anal_11 = false;
            _inventoryINHeader.Ith_anal_12 = false;
            _inventoryINHeader.Ith_anal_2 = string.Empty;
            _inventoryINHeader.Ith_anal_3 = string.Empty;
            _inventoryINHeader.Ith_anal_4 = string.Empty;
            _inventoryINHeader.Ith_anal_5 = string.Empty;
            _inventoryINHeader.Ith_anal_6 = 0;
            _inventoryINHeader.Ith_anal_7 = 0;
            _inventoryINHeader.Ith_anal_8 = DateTime.Now.Date;
            _inventoryINHeader.Ith_anal_9 = DateTime.Now.Date;
            _inventoryINHeader.Ith_bus_entity = string.Empty;
            _inventoryINHeader.Ith_channel = string.Empty;
            _inventoryINHeader.Ith_com = BaseCls.GlbUserComCode;
            _inventoryINHeader.Ith_com_docno = string.Empty;
            _inventoryINHeader.Ith_cre_by = BaseCls.GlbUserID;
            _inventoryINHeader.Ith_cre_when = DateTime.Now.Date;
            _inventoryINHeader.Ith_del_add1 = string.Empty;
            _inventoryINHeader.Ith_del_add2 = string.Empty;
            _inventoryINHeader.Ith_del_code = string.Empty;
            _inventoryINHeader.Ith_del_party = string.Empty;
            _inventoryINHeader.Ith_del_town = string.Empty;
            _inventoryINHeader.Ith_direct = false;
            _inventoryINHeader.Ith_doc_date = DateTime.Now.Date;
            _inventoryINHeader.Ith_doc_no = string.Empty;
            _inventoryINHeader.Ith_doc_tp = "AOD";
            _inventoryINHeader.Ith_doc_year = DateTime.Now.Date.Year;
            _inventoryINHeader.Ith_entry_no = string.Empty;
            _inventoryINHeader.Ith_entry_tp = string.Empty;
            _inventoryINHeader.Ith_git_close = false;
            _inventoryINHeader.Ith_git_close_date = DateTime.Now.Date;
            _inventoryINHeader.Ith_git_close_doc = string.Empty;
            _inventoryINHeader.Ith_is_manual = false;
            _inventoryINHeader.Ith_isprinted = false;
            _inventoryINHeader.Ith_sub_docno = txtJobNo.Text;
            _inventoryINHeader.Ith_loading_point = string.Empty;
            _inventoryINHeader.Ith_loading_user = string.Empty;
            _inventoryINHeader.Ith_mod_by = BaseCls.GlbUserID;
            _inventoryINHeader.Ith_mod_when = DateTime.Now.Date;
            _inventoryINHeader.Ith_noofcopies = 0;
            _inventoryINHeader.Ith_manual_ref = string.Empty;
            if (transferJob == true)
            {
                _inventoryINHeader.Ith_loc = BaseCls.GlbUserDefLoca;
                _inventoryINHeader.Ith_oth_loc = txtTransFrom.Text;
                _inventoryINHeader.Ith_manual_ref = "Transfer Job";
            }
            else
            {
                _inventoryINHeader.Ith_oth_loc = BaseCls.GlbUserDefLoca;
                _inventoryINHeader.Ith_loc = txtTransFrom.Text;
            }
            _inventoryINHeader.Ith_oth_docno = txtJobNo.Text;
            _inventoryINHeader.Ith_remarks = string.Empty;
            _inventoryINHeader.Ith_sbu = string.Empty;
            //_inventoryINHeader.Ith_seq_no = 0; removed by Chamal 12-05-2013
            _inventoryINHeader.Ith_session_id = BaseCls.GlbUserSessionID;
            _inventoryINHeader.Ith_stus = "A";
            _inventoryINHeader.Ith_sub_tp = "STDBY";    // string.Empty; 10/7/2013
            _inventoryINHeader.Ith_cate_tp = "SERVICE";
            _inventoryINHeader.Ith_vehi_no = string.Empty;
            _inventoryINHeader.Ith_oth_com = BaseCls.GlbUserComCode;
            _inventoryINHeader.Ith_anal_1 = "0";
            _inventoryINHeader.Ith_anal_2 = string.Empty;
            _inventoryINHeader.Ith_isjobbase = false;

            List<ReptPickSerials> _serialINList = new List<ReptPickSerials>();
            // _serialINList.AddRange(_serialList);
            ReptPickSerials _reptPickINSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbDefaultBin, dgvItems.Rows[0].Cells["STI_ISSUEITMCD"].Value.ToString(), Convert.ToInt32(dgvItems.Rows[0].Cells["sti_issueserid"].Value));
            _reptPickINSerial_.Tus_cre_by = BaseCls.GlbUserID;
            _reptPickINSerial_.Tus_usrseq_no = 0;
            _reptPickINSerial_.Tus_cre_by = BaseCls.GlbUserID;
            _reptPickINSerial_.Tus_base_doc_no = "N/A";
            _reptPickINSerial_.Tus_base_itm_line = 0;
            _reptPickINSerial_.Tus_new_remarks = "AOD-IN";

            msitem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, dgvItems.Rows[0].Cells["STI_ISSUEITMCD"].Value.ToString());
            _reptPickINSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
            _reptPickINSerial_.Tus_itm_model = msitem.Mi_model;
            _serialINList.Add(_reptPickINSerial_);

            if (transferJob == false)
            {
                foreach (ReptPickSerials _inSer in _serialINList)
                {
                    _inSer.Tus_loc = txtTransFrom.Text;
                    _inSer.Tus_bin = CHNLSVC.Inventory.Get_default_binCD(BaseCls.GlbUserComCode, txtTransFrom.Text);
                }
            }

            MasterAutoNumber _inventoryAutoIN = new MasterAutoNumber();
            _inventoryAutoIN.Aut_moduleid = "AOD";
            if (transferJob == true)
            { _inventoryAutoIN.Aut_cate_cd = BaseCls.GlbUserDefLoca; }
            else
            { _inventoryAutoIN.Aut_cate_cd = txtTransFrom.Text; }
            _inventoryAutoIN.Aut_cate_tp = "LOC";
            _inventoryAutoIN.Aut_direction = 1;
            _inventoryAutoIN.Aut_modify_dt = null;
            _inventoryAutoIN.Aut_year = DateTime.Now.Year;
            _inventoryAutoIN.Aut_start_char = "AOD";


            #endregion
            //------------------------------------------------------------------------------------------------------------------------
            #region TEMP ISSUE
            MasterAutoNumber _ReqInsAuto = new MasterAutoNumber();
            _ReqInsAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _ReqInsAuto.Aut_cate_tp = "LOC";
            _ReqInsAuto.Aut_direction = 0;
            _ReqInsAuto.Aut_modify_dt = null;
            _ReqInsAuto.Aut_moduleid = "TMPR";
            _ReqInsAuto.Aut_number = 0;
            _ReqInsAuto.Aut_start_char = "TMPR";
            _ReqInsAuto.Aut_year = DateTime.Today.Year;

            Service_TempIssue oItem = new Service_TempIssue();
            List<Service_TempIssue> oMainList = new List<Service_TempIssue>();
            //oItem.STI_SEQNO             = "";
            oItem.STI_LINE = 1;
            //oItem.STI_DOC = "";
            oItem.STI_DT = DateTime.Now.Date;
            oItem.STI_COM = BaseCls.GlbUserComCode;
            oItem.STI_LOC = BaseCls.GlbUserDefLoca;
            oItem.STI_DOC_TP = "STBYI";
            oItem.STI_JOBNO = txtJobNo.Text;
            oItem.STI_JOBLINENO = _jobLine;
            oItem.STI_ISSUEITMCD = dgvItems.Rows[0].Cells["STI_ISSUEITMCD"].Value.ToString();
            oItem.STI_ISSUEITMSTUS = dgvItems.Rows[0].Cells["STI_ISSUEITMSTUS"].Value.ToString();
            oItem.STI_ISSUESERIALNO = dgvItems.Rows[0].Cells["serial_no"].Value.ToString();
            if (dgvItems.Rows[0].Cells["serial_no"].Value.ToString() != "N/A")
            {
                oItem.STI_ISSUESERID = Convert.ToInt32(dgvItems.Rows[0].Cells["sti_issueserid"].Value.ToString());
            }
            oItem.STI_ISSUEITMQTY = Convert.ToDecimal(dgvItems.Rows[0].Cells["STI_ISSUEITMQTY"].Value.ToString());
            oItem.STI_BALQTY = Convert.ToDecimal(dgvItems.Rows[0].Cells["STI_ISSUEITMQTY"].Value.ToString());
            oItem.STI_CROSS_SEQNO = 0;
            oItem.STI_CROSS_LINE = 0;
            oItem.STI_ISRECEIVE = 0;
            oItem.STI_TECHCODE = "";
            oItem.STI_REFDOCNO = "";
            oItem.STI_REFDOCLINE = 0;
            oItem.STI_STUS = "A";
            // oItem.STI_RMK = txtr.Text;
            oItem.STI_CRE_BY = BaseCls.GlbUserID;
            oItem.STI_CRE_DT = DateTime.Now;
            oItem.STI_ISRECEIVE = 1;
            //if (!String.IsNullOrEmpty(lblvisitNum.Text))
            //{
            //    oItem.STI_VISIT_SEQ = Convert.ToInt32(lblvisitNum.Text);
            //}
            oMainList.Add(oItem);

            #endregion

            Int32 result = 0;
            string _msg = "";

            Int32 SeqNo = Convert.ToInt32(dgvItems.Rows[0].Cells["sti_seqno"].Value.ToString());
            Int32 SeqNoLine = Convert.ToInt32(dgvItems.Rows[0].Cells["sti_line"].Value.ToString());

            result = CHNLSVC.Inventory.Process_Return_StandBy(oMainList, _inventoryHeader, _inventoryINHeader, _serialList, _serialINList, null, null, _inventoryAuto, _inventoryAutoIN, _ReqInsAuto, SeqNo, SeqNoLine, out _msg);


            if (result != -99 && result >= 0)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Successfully Saved!" + System.Environment.NewLine + "AOD Out # :" + _msg, "Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                clearAll();
                //chkReturn.Checked = false;
                //btnView_Click(null, null);
            }
            else
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Process Terminated!\n" + _msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtJobNo.Text)) txtTransFrom.Focus();
            }
        }



    }
}