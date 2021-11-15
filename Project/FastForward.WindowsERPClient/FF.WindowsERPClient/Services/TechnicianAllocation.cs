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
    public partial class TechnicianAllocation : Base
    {
        private bool mouseIsDown = false;
        private Point firstPoint;
        private string Direction = string.Empty;
        private string SlotText = string.Empty;
        private DateTime StartDate;
        private Int32 TotalSlots = 0;
        private bool IsLoad = true;

        private List<Service_Tech_Aloc_Hdr> oMainListHdr = new List<Service_Tech_Aloc_Hdr>();

        public TechnicianAllocation()
        {
            InitializeComponent();
            dgvJobDetails.AutoGenerateColumns = false;
            dgvDefects.AutoGenerateColumns = false;
            dgvAllocationHistory.AutoGenerateColumns = false;
            //dgvTechnicianAssignments.AutoGenerateColumns = false;
            pnlEmpSearch.Visible = false;
            dgvEMPSelect.AutoGenerateColumns = false;
            dgvAllocations.AutoGenerateColumns = false;
            pnlEmpSearch.Size = new Size(571, 316);
            pnlcusdetails.Visible = false;

            Service_Chanal_parameter _Parameters = null;
            _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_Parameters != null)
            {
                SlotText = _Parameters.SP_DB_SLOT;
                //dgvJobDetails.Columns[5].HeaderText = SlotText;
                lblSlot.Text = SlotText;
            }

            StartDate = DateTime.Today.AddMonths(-1);

            dtpFrom.Value = StartDate;
            dtpFromH.Value = StartDate;
            //dtpFromAL.Value = StartDate;

            //dgvAllocations.DataSource = new List<Service_Tech_Aloc_Hdr>();
        }

        private void TechnicianAllocation_Load(object sender, EventArgs e)
        {
            clearAll();
            //GetJobDetails();
            //getHistory(DateTime.Now, DateTime.Now);
            btnshed_serch_Click(null, null);
            IsLoad = false;
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
                        String isCustExpect = "0";

                        isCustExpect = (chkCusExpect.Checked) ? "1" : "0";

                        if (chkAllocated.Checked)
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "1.1,3,4,4.1,5,5.1" + seperator + isCustExpect + seperator);
                            break;
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "1.1,2" + seperator + isCustExpect + seperator);
                            break;
                        }
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.tec_team_cd:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator );
                        break;
                    }
                    break;
            }

            return paramsText.ToString();
        }

        #region Events

        private void btnSearchJob_Click(object sender, EventArgs e)
        {
            dgvAllocations.DataSource = new List<Service_Tech_Aloc_Hdr>();
            dgvDefects.DataSource = new List<Service_Job_Defects>();
            oMainListHdr.Clear();
            GetJobDetails();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvAllocations.Rows.Count > 0)
            {
                if (!isAnySelected())
                {
                    MessageBox.Show("please select a active technician.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show("Are you want to Save", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                Cursor = Cursors.WaitCursor;

                MasterAutoNumber _ReqInsAuto = new MasterAutoNumber();
                _ReqInsAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _ReqInsAuto.Aut_cate_tp = "PC";
                _ReqInsAuto.Aut_direction = 1;
                _ReqInsAuto.Aut_modify_dt = null;
                _ReqInsAuto.Aut_moduleid = "TECH";
                _ReqInsAuto.Aut_number = 0;
                _ReqInsAuto.Aut_start_char = "TECH";
                _ReqInsAuto.Aut_year = null;

               
                Service_Chanal_parameter oChnlPara = CHNLSVC.General.GetChannelParamers(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                if (oChnlPara != null)
                {

                    oChnlPara.SP_AUTO_START_JOB = oChnlPara.SP_AUTO_START_JOB;
                }

                int effect = CHNLSVC.CustService.Save_TechnicianAllocatoinHEader(oMainListHdr, _ReqInsAuto, true, oChnlPara.SP_AUTO_START_JOB);
                if (effect > 0)
                {
                    int X = 0;
                    foreach (Service_Tech_Aloc_Hdr _lst in oMainListHdr)
                    {
                        Service_Message_Template oTemplate = CHNLSVC.CustService.GetMessageTemplates_byID(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, 2);
                        if (oTemplate != null && oTemplate.Sml_templ_mail != null)
                        {

                            List<MST_BUSPRIT_LVL> oItems;
                            string custLevel = string.Empty;

                            Service_JOB_HDR oJOB_HDR = CHNLSVC.CustService.GetServiceJobHeader(oMainListHdr[X].STH_JOBNO, BaseCls.GlbUserComCode);

                            oItems = CHNLSVC.CustService.GetCustomerPriorityLevel(oJOB_HDR.SJB_B_CUST_CD, BaseCls.GlbUserComCode);
                            if (oItems == null || oItems.Count == 0)
                            {
                                oItems = CHNLSVC.CustService.GetCustomerPriorityLevel("CASH", BaseCls.GlbUserComCode);
                            }

                            if (oItems != null && oItems.Count > 0)
                            {
                                MST_BUSPRIT_LVL oSelectedLvl = new MST_BUSPRIT_LVL();
                                String PartyCode = String.Empty;
                                String PartyType = String.Empty;
                                if (oItems.FindAll(x => x.Mbl_pty_tp == "LOC" && x.Mbl_pty_cd == BaseCls.GlbUserDefLoca).Count > 0)
                                {
                                    List<MST_BUSPRIT_LVL> ot1 = oItems.FindAll(x => x.Mbl_pty_tp == "LOC" && x.Mbl_pty_cd == BaseCls.GlbUserDefLoca);
                                    oSelectedLvl = ot1[0];
                                    PartyCode = BaseCls.GlbUserDefLoca;
                                    PartyType = "LOC";
                                    custLevel = ot1[0].SCP_DESC;
                                }
                                else if (oItems.FindAll(x => x.Mbl_pty_tp == "SCHNL" && x.Mbl_pty_cd == BaseCls.GlbDefSubChannel).Count > 0)
                                {
                                    List<MST_BUSPRIT_LVL> ot1 = oItems.FindAll(x => x.Mbl_pty_tp == "SCHNL" && x.Mbl_pty_cd == BaseCls.GlbDefSubChannel);

                                    if (ot1.Count > 0)
                                    {
                                        oSelectedLvl = ot1[0];
                                        PartyCode = BaseCls.GlbDefSubChannel;
                                        PartyType = "SCHNL";
                                        custLevel = ot1[0].SCP_DESC;
                                    }
                                }
                            }
                            string emailBody = oTemplate.Sml_templ_mail;
                            emailBody = emailBody.Replace("[B_Cust]", oJOB_HDR.SJB_B_CUST_NAME)
                                                 .Replace("[jobNo]", oJOB_HDR.SJB_JOBNO)
                                                 .Replace("[CustLevel]", custLevel);

                            String SmsBody = oTemplate.Sml_templ_sms;

                            SmsBody = SmsBody.Replace("[jobNo]", oJOB_HDR.SJB_JOBNO);
                            Service_Message oMessage;
                            oMessage = new Service_Message();
                            oMessage.Sm_com = oJOB_HDR.SJB_COM;
                            oMessage.Sm_jobno = oJOB_HDR.SJB_JOBNO;
                            oMessage.Sm_joboline = 1;
                            oMessage.Sm_jobstage = 3;
                            oMessage.Sm_ref_num = string.Empty;
                            oMessage.Sm_status = 0;
                            oMessage.Sm_msg_tmlt_id = 2;
                            oMessage.Sm_sms_text = SmsBody;
                            oMessage.Sm_sms_gap = 0;
                            oMessage.Sm_sms_done = 0;
                            oMessage.Sm_mail_text = emailBody;
                            oMessage.Sm_mail_gap = 0;
                            oMessage.Sm_email_done = 0;
                            oMessage.Sm_cre_by = BaseCls.GlbUserID;
                            oMessage.Sm_cre_dt = DateTime.Now;
                            oMessage.Sm_mod_by = BaseCls.GlbUserID;
                            oMessage.Sm_mod_dt = DateTime.Now;

                            string outMsg;
                            Int32 asdasd = CHNLSVC.CustService.SaveServiceMsg(oMessage, out outMsg);
                        }
                        X++;
                    }
                        Cursor = Cursors.Default;
                        MessageBox.Show("Technician allocated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearAll();
                        return;

                        //X++;
                    
                }
                else
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("Process Terminated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please add records to grid.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear the screen?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                clearAll();
            }
        }

        private void btnSearchTA_Click(object sender, EventArgs e)
        {
            //DataTable dt = CHNLSVC.CustService.GetAllocatedHistory(dtpFromTA.Value, dtoToTA.Value, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            //dgvTechnicianAssignments.DataSource = dt;
        }

        private void btnVouNo_Click(object sender, EventArgs e)
        {
            txtJobNo_DoubleClick(null, null);
        }

        private void btnEmpSrchTD_Click(object sender, EventArgs e)
        {
            txtEmpCode_DoubleClick(null, null);
        }

        private void btnSkillSrchTD_Click(object sender, EventArgs e)
        {
            txtSkills_DoubleClick(null, null);
        }

        private void btnDesignationSrchTD_Click(object sender, EventArgs e)
        {
            txtDesignation_DoubleClick(null, null);
        }

        private void btnAreaSrchA_Click(object sender, EventArgs e)
        {
        }

        private void txtJobNo_DoubleClick(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
            DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, dtpFrom.Value, dtpTo.Value);
            _CommonSearch.dtpFrom.Value = dtpFrom.Value;
            _CommonSearch.dtpTo.Value = dtpTo.Value.Date;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtJobNo;
            //_commonSearch.IsSearchEnter = true;
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
        }

        private void txtJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtJobNo_DoubleClick(null, null);
            }

            if (e.KeyCode == Keys.Enter)
            {
                //Tharindu

                string job = txtJobNo.Text;
                clrScreen();
                // txtJobNo.Text = job;                
                getJobJetails();
            }
        }

        private void txtJobNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                Service_JOB_HDR oJOB_HDR = CHNLSVC.CustService.GetServiceJobHeader(txtJobNo.Text, BaseCls.GlbUserComCode);
                if (oJOB_HDR.SJB_JOBNO == null)
                {
                    MessageBox.Show("Please enter correct job no", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtJobNo.Clear();
                    txtJobNo.Focus();
                    return;
                }
            }
        }

        private void txtEmpCode_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSkills.Text))
            {
                //DataTable dtEmp = CHNLSVC.CustService.GetEmployeByDefect(BaseCls.GlbUserComCode, txtSkills.Text);
                //if (dtEmp.Rows.Count > 0)
                //{
                //    string[] EmpList = dtEmp.AsEnumerable().Select(r => r.Field<string>("MED_EPF")).Distinct().ToArray();

                //    Cursor = Cursors.WaitCursor;
                //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                //    DataTable _result = new DataTable();
                //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EMP_ALL);
                //    _result = CHNLSVC.CommonSearch.GetAllEmp(_CommonSearch.SearchParams, null, null);

                //    string[] empFromDB = _result.AsEnumerable().Select(r => r.Field<string>("EPF")).Distinct().ToArray();

                //    string[] empSelected = EmpList.Intersect(empFromDB).ToArray();

                //    for (int i = 0; i < empSelected.Length; i++)
                //    {
                //        DataRow[] dr = _result.Select("EPF ='" + empSelected[i] + "'");

                //    }

                //    _CommonSearch.IsSearchEnter = true;
                //    _CommonSearch.dvResult.DataSource = _result;
                //    _CommonSearch.BindUCtrlDDLData(_result);
                //    _CommonSearch.obj_TragetTextBox = txtEmpCode;
                //    _CommonSearch.ShowDialog();
                //    txtEmpCode.Select();
                //    Cursor = Cursors.Default;
                //}
            }
            Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = new DataTable();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EMP_ALL);
            _result = CHNLSVC.CommonSearch.GetAllEmp(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtEmpCode;
            _CommonSearch.ShowDialog();
            txtEmpCode.Select();
            Cursor = Cursors.Default;
        }

        private void txtEmpCode_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void txtEmpCode_Leave(object sender, EventArgs e)
        {
        }

        private void txtSkills_DoubleClick(object sender, EventArgs e)
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
            _CommonSearch.obj_TragetTextBox = txtSkills;
            _CommonSearch.ShowDialog();
            txtSkills.Select();
            Cursor = Cursors.Default;
        }

        private void txtSkills_Leave(object sender, EventArgs e)
        {
        }

        private void txtSkills_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void txtDesignation_DoubleClick(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = new DataTable();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Designation);
            _result = CHNLSVC.CommonSearch.Get_Designations(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDesignation;
            _CommonSearch.ShowDialog();
            txtDesignation.Select();
            Cursor = Cursors.Default;
        }

        private void txtDesignation_Leave(object sender, EventArgs e)
        {
        }

        private void txtDesignation_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void dgvTechnicianAssignments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvJobDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //if (!CheckSingleRow(dgvJobDetails, "select"))
                if (dgvJobDetails.Rows[e.RowIndex].Cells["select"].Value == null || Convert.ToBoolean(dgvJobDetails.Rows[e.RowIndex].Cells["select"].Value) == false)
                {

                    lblTown.Text = "";
                    lblTown.Tag = "";

                    lblItem.Text = "";
                    lblSearial.Text = "";
                    //if (dgvJobDetails.Rows[e.RowIndex].Cells["select"].Value != null)
                    {
                        //for (int i = 0; i < dgvJobDetails.Rows.Count; i++)
                        //{
                        //    //dgvJobDetails.Rows[i].Cells["select"].Value = false;
                        //}
                        dgvJobDetails.Rows[e.RowIndex].Cells["select"].Value = true;

                        if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                        {
                            LoadDefects(dgvJobDetails.Rows[e.RowIndex].Cells["JobNo"].Value.ToString(), dgvJobDetails.Rows[e.RowIndex].Cells["JobLine"].Value.ToString());
                            lblItem.Text = dgvJobDetails.Rows[e.RowIndex].Cells["ItemCode"].Value.ToString();
                            lblSearial.Text = dgvJobDetails.Rows[e.RowIndex].Cells["SerialNo"].Value.ToString();

                            if (dgvJobDetails.Rows[e.RowIndex].Cells["SJB_TOWN"].Value != null)
                            {
                                //lblTown.Tag = dgvJobDetails.Rows[e.RowIndex].Cells["SJB_TOWN"].Value.ToString();
                                lblTown.Text = dgvJobDetails.Rows[e.RowIndex].Cells["SJB_TOWN"].Value.ToString();
                            }

                            //DataTable dt = CHNLSVC.General.GetTownByCode(lblTown.Tag.ToString());
                            //if (dt != null && dt.Rows.Count > 0)
                            //{
                            //    lblTown.Text = dt.Rows[0]["mt_desc"].ToString();
                            //}

                            Direction = dgvJobDetails.Rows[e.RowIndex].Cells["sc_direct"].Value.ToString();

                            string type = dgvJobDetails.Rows[e.RowIndex].Cells["sc_tp"].Value.ToString();

                            LoadCapacity(type);
      
                            //if (chkAllocated.Checked)
                            //{
                                List<Service_Tech_Aloc_Hdr> oDetails = CHNLSVC.CustService.GetJobAllocations(dgvJobDetails.Rows[e.RowIndex].Cells["JobNo"].Value.ToString(), Convert.ToInt32(dgvJobDetails.Rows[e.RowIndex].Cells["JobLine"].Value.ToString()), BaseCls.GlbUserComCode);
                                //oMainListHdr.Clear();
                                oMainListHdr.AddRange(oDetails);
                                dgvAllocations.DataSource = new List<Service_Tech_Aloc_Hdr>();
                                //dgvAllocations.DataSource = oDetails;
                                dgvAllocations.DataSource = oMainListHdr;
                                if (oDetails.Count > 0)
                                {
                                    ddlSlots.SelectedValue = oDetails[0].STH_TERMINAL.ToString();
                                }
                            //}
                        }
                        if (e.RowIndex >= 0 && e.ColumnIndex == 1)
                        {

                            lbl_jobno.Text = dgvJobDetails.Rows[e.RowIndex].Cells["JobNo"].Value.ToString();
                            lblcustname.Text = dgvJobDetails.Rows[e.RowIndex].Cells["sjb_b_cust_name"].Value.ToString();
                            lblcust_tp.Text = dgvJobDetails.Rows[e.RowIndex].Cells["sjb_b_mobino"].Value.ToString();
                            lblcustadd.Text = dgvJobDetails.Rows[e.RowIndex].Cells["address"].Value.ToString();
                            lbljobline.Text = dgvJobDetails.Rows[e.RowIndex].Cells["JobLine"].Value.ToString();
                            custex_date.Value = Convert.ToDateTime(dgvJobDetails.Rows[e.RowIndex].Cells["sjb_custexptdt"].Value.ToString());
                            pnl_job_detupdate.Visible = true;
                      
                            this.pnl_job_detupdate.Size = new System.Drawing.Size(619, 131);
           
                        }
                    }
                }
                else
                {
                    dgvJobDetails.Rows[e.RowIndex].Cells["select"].Value = false;
                    string JOBNO=dgvJobDetails.Rows[e.RowIndex].Cells["JobNo"].Value.ToString();
                    Int32 jobline = Convert.ToInt32(dgvJobDetails.Rows[e.RowIndex].Cells["JobLine"].Value.ToString());
                    oMainListHdr.RemoveAll(r => r.STH_JOBNO == JOBNO && r.STH_JOBLINE == jobline);
                    dgvAllocations.DataSource = new List<Service_Tech_Aloc_Hdr>();
                    dgvAllocations.AutoGenerateColumns = false;
                    dgvAllocations.DataSource = oMainListHdr;

                }
            }
        }

        private void dgvDefects_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (!CheckSingleRow(dgvDefects, "selectJD"))
            //{
            //    dgvDefects.Rows[e.RowIndex].Cells["selectJD"].Value = true;

            //    if (e.RowIndex > -1 && e.ColumnIndex > -1)
            //    {
            //    }
            //}
            //else
            //{
            //    dgvDefects.Rows[e.RowIndex].Cells["selectJD"].Value = false;
            //}

            //if (Convert.ToBoolean(dgvDefects.Rows[e.RowIndex].Cells["selectJD"].Value) == true)
            //{
            //    dgvDefects.Rows[e.RowIndex].Cells["selectJD"].Value = false;
            //}
            //else
            //{
            //    dgvDefects.Rows[e.RowIndex].Cells["selectJD"].Value = true;
            //}
        }

        private void btnShowEMP_Click(object sender, EventArgs e)
        {
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtALEmp.Text))
            {
                if (!string.IsNullOrEmpty(txtALEmp.Text))
                {
                    DataTable dtTemp = CHNLSVC.Sales.GetManagerDefProfit(BaseCls.GlbUserComCode, txtALEmp.Text);
                    if (dtTemp.Rows.Count == 0)
                    {
                        MessageBox.Show("Please enter correct EPF number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtALEmp.Clear();
                        txtALEmp.Focus();
                        return;
                    }
                }

                bool status = true;

                if (dgvJobDetails.Rows.Count <= 0)
                {
                    MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (dtpFromAL.Value > dtpToAL.Value)
                {
                    MessageBox.Show("Please select valid from date", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFromAL.Focus();
                    return;
                }

                if (dtpFromAL.Value < Convert.ToDateTime(dgvJobDetails.SelectedRows[0].Cells["Date"].Value.ToString()))
                {
                    MessageBox.Show("From date can't be less than job date.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFromAL.Focus();
                    return;
                }
                if (dgvJobDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvJobDetails.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgvJobDetails.Rows[i].Cells[0].Value) == true)
                        {
                            status = true;
                            break;
                        }
                        else
                        {
                            status = false;
                        }
                    }

                    if (status == false)
                    {
                        MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                //if (Direction == "F")
                //{
                //    if (string.IsNullOrEmpty(txtAHArea.Text))
                //    {
                //        MessageBox.Show("Please enter a area", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }
                //}

                for (int i = 0; i < dgvJobDetails.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvJobDetails.Rows[i].Cells[0].Value) == true)
                    {
                        if (!string.IsNullOrEmpty(txtALEmp.Text))
                        {
                            if (oMainListHdr.Exists(x => x.STH_EMP_CD == txtALEmp.Text))
                            {
                                continue;
                            }
                            Service_Tech_Aloc_Hdr oAloc_Hdr = new Service_Tech_Aloc_Hdr();

                            oAloc_Hdr.STH_SEQ = -1;
                            oAloc_Hdr.STH_ALOCNO = "";
                            oAloc_Hdr.STH_COM = BaseCls.GlbUserComCode;
                            oAloc_Hdr.STH_LOC = BaseCls.GlbUserDefProf;
                            oAloc_Hdr.STH_TP = "J";
                            oAloc_Hdr.STH_JOBNO = dgvJobDetails.Rows[i].Cells["JobNo"].Value.ToString();
                            oAloc_Hdr.STH_JOBLINE = Convert.ToInt32(dgvJobDetails.Rows[i].Cells["JobLine"].Value.ToString());
                            oAloc_Hdr.STH_EMP_CD = txtALEmp.Text.Trim();
                            oAloc_Hdr.STH_STUS = "A";
                            oAloc_Hdr.STH_CRE_BY = BaseCls.GlbUserID;
                            oAloc_Hdr.STH_CRE_WHEN = DateTime.Now;
                            oAloc_Hdr.STH_MOD_BY = BaseCls.GlbUserID;
                            oAloc_Hdr.STH_MOD_WHEN = DateTime.Now;
                            oAloc_Hdr.STH_SESSION_ID = BaseCls.GlbUserSessionID;
                            oAloc_Hdr.STH_TOWN = "";
                            oAloc_Hdr.STH_TOWN = lblTown.Tag.ToString();
                            oAloc_Hdr.MT_DESC = lblTown.Text;
                            oAloc_Hdr.STH_FROM_DT = dtpFromAL.Value;
                            oAloc_Hdr.STH_TO_DT = dtpToAL.Value;
                            oAloc_Hdr.STH_REQNO = "";
                            oAloc_Hdr.STH_REQLINE = 0;
                            oAloc_Hdr.STH_CURR_STUS = 1;
                           // oAloc_Hdr.esep_mobi_no = dgvJobDetails.Rows[i].Cells["Mobile"].Value.ToString();
                            if (ddlSlots.SelectedValue != null)
                            {
                                oAloc_Hdr.STH_TERMINAL = Convert.ToInt32(ddlSlots.SelectedValue.ToString());
                            }

                            DataTable dtTemp = CHNLSVC.Sales.GetManagerDefProfit(BaseCls.GlbUserComCode, txtALEmp.Text);
                            oAloc_Hdr.ESEP_FIRST_NAME = dtTemp.Rows[0]["ESEP_FIRST_NAME"].ToString();
                            oAloc_Hdr.esep_mobi_no = dtTemp.Rows[0]["ESEP_MOBI_NO"].ToString();
                            oMainListHdr.Add(oAloc_Hdr);
                        }
                    }
                }
                txtALEmp.Clear();
                dgvAllocations.DataSource = new List<Service_Tech_Aloc_Hdr>();
                if (oMainListHdr.Count > 0)
                {
                    dgvAllocations.DataSource = oMainListHdr;
                }
                return;
            }
            if (validateAdd())
            {
                //int asd = CHNLSVC.CustService.GetLocationCurrectSlotCount(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, ddlSlots.SelectedValue.ToString(), dtpFromAL.Value, dtpToAL.Value);
                //MessageBox.Show("Total " + SlotText + "s :-" + TotalSlots + "\n" + "utilized " + SlotText + "s :-" + asd.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (dtpFromAL.Value > dtpToAL.Value)
                {
                    MessageBox.Show("Please select valid from date", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFromAL.Focus();
                    return;
                }

                if (dtpFromAL.Value < Convert.ToDateTime(dgvJobDetails.SelectedRows[0].Cells["Date"].Value.ToString()))
                {
                    MessageBox.Show("From date can't be less than job date.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFromAL.Focus();
                    return;
                }

                for (int i = 0; i < dgvJobDetails.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvJobDetails.Rows[i].Cells[0].Value) == true)
                    {
                        for (int j = 0; j < dgvEMPSelect.Rows.Count; j++)
                        {
                            if (Convert.ToBoolean(dgvEMPSelect.Rows[j].Cells[0].Value) == true)
                            {
                                var val = oMainListHdr.Where(x => x.STH_JOBNO == dgvJobDetails.Rows[i].Cells["JobNo"].Value.ToString()).Where(y => y.STH_EMP_CD == dgvEMPSelect.Rows[j].Cells[1].Value.ToString());

                                if(val.Count() > 0 && val != null)
                                {
                                    continue;
                                }

                                //if (oMainListHdr.Exists(x => x.STH_EMP_CD == dgvEMPSelect.Rows[j].Cells[1].Value.ToString()))
                                //{
                                //    continue;
                                //}

                                Service_Tech_Aloc_Hdr oAloc_Hdr = new Service_Tech_Aloc_Hdr();

                                oAloc_Hdr.STH_SEQ = -1;
                                oAloc_Hdr.STH_ALOCNO = "";
                                oAloc_Hdr.STH_COM = BaseCls.GlbUserComCode;
                                oAloc_Hdr.STH_LOC = BaseCls.GlbUserDefLoca;
                                oAloc_Hdr.STH_TP = "J";
                                oAloc_Hdr.STH_JOBNO = dgvJobDetails.Rows[i].Cells["JobNo"].Value.ToString();
                                oAloc_Hdr.STH_JOBLINE = Convert.ToInt32(dgvJobDetails.Rows[i].Cells["JobLine"].Value.ToString());
                                oAloc_Hdr.STH_EMP_CD = dgvEMPSelect.Rows[j].Cells[1].Value.ToString();
                                oAloc_Hdr.STH_STUS = "A";
                                oAloc_Hdr.STH_CRE_BY = BaseCls.GlbUserID;
                                oAloc_Hdr.STH_CRE_WHEN = DateTime.Now;
                                oAloc_Hdr.STH_MOD_BY = BaseCls.GlbUserID;
                                oAloc_Hdr.STH_MOD_WHEN = DateTime.Now;
                                oAloc_Hdr.STH_SESSION_ID = BaseCls.GlbUserSessionID;
                                oAloc_Hdr.STH_TOWN = lblTown.Tag.ToString();
                                oAloc_Hdr.MT_DESC = lblTown.Text;
                                oAloc_Hdr.STH_FROM_DT = dtpFromAL.Value;
                                oAloc_Hdr.STH_TO_DT = dtpToAL.Value;
                                oAloc_Hdr.STH_REQNO = "";
                                oAloc_Hdr.STH_REQLINE = 0;
                                oAloc_Hdr.STH_CURR_STUS = 1;
                                //oAloc_Hdr.esep_mobi_no = dgvEMPSelect.Rows[i].Cells["MOBI_NO"].Value.ToString();

                                DataTable dtTemp = CHNLSVC.Sales.GetManagerDefProfit(BaseCls.GlbUserComCode, dgvEMPSelect.Rows[j].Cells["EPFES"].Value.ToString());
                                oAloc_Hdr.ESEP_FIRST_NAME = dtTemp.Rows[0]["ESEP_FIRST_NAME"].ToString();
                                oAloc_Hdr.esep_mobi_no = dtTemp.Rows[0]["ESEP_MOBI_NO"].ToString();
                                if (ddlSlots.SelectedValue != null)
                                {
                                    oAloc_Hdr.STH_TERMINAL = Convert.ToInt32(ddlSlots.SelectedValue.ToString());
                                }
                                oMainListHdr.Add(oAloc_Hdr);
                            }
                        }
                    }
                }
                dgvEMPSelect.DataSource = null;
                dgvAllocations.DataSource = new List<Service_Tech_Aloc_Hdr>();

                if (oMainListHdr.Count > 0)
                {
                    dgvAllocations.DataSource = oMainListHdr;
                }
            }
        }

        private void btnEMPSearch_Click(object sender, EventArgs e)
        {
            if (pnlEmpSearch.Visible == true)
            {
                pnlEmpSearch.Visible = false;
            }
            else
            {
                getEMP();
                pnlEmpSearch.Visible = true;
                pnlEmpSearch.Size = new Size(725, 321); 
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            if (pnlEmpSearch.Visible == true)
            {
                pnlEmpSearch.Visible = false;
            }
            else
            {
                pnlEmpSearch.Visible = true;
            }
        }

        private void label20_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }

        private void label20_MouseDown(object sender, MouseEventArgs e)
        {
            firstPoint = e.Location;
            mouseIsDown = true;
        }

        private void label20_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                int xDiff = firstPoint.X - e.Location.X;
                int yDiff = firstPoint.Y - e.Location.Y;

                int x = pnlEmpSearch.Location.X - xDiff;
                int y = pnlEmpSearch.Location.Y - yDiff;
                pnlEmpSearch.Location = new Point(x, y);
            }
        }

        private void dgvEMPSelect_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex > -1 && e.ColumnIndex > -1)
            //{
            //    txtEmpCode.Text = dgvEMPSelect.SelectedRows[0].Cells[1].Value.ToString();
            //    pnlEmpSearch.Visible = false;
            //}
        }

        private void dgvDefects_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                txtSkills.Text = dgvDefects.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (dgvEMPSelect.Rows.Count > 0)
            {
                bool status = false;

                for (int i = 0; i < dgvEMPSelect.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvEMPSelect.Rows[i].Cells[0].Value) == true)
                    {
                        status = true;
                        break;
                    }
                    else
                    {
                        status = false;
                    }
                }
                if (status == false)
                {
                    MessageBox.Show("Please select employee", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnEMPSearch.Focus();
                    return;
                }
            }
            if (MessageBox.Show("Do you want to confirm", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
            {
                pnlEmpSearch.Visible = false;

                string empList = string.Empty;

                for (int i = 0; i < dgvEMPSelect.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvEMPSelect.Rows[i].Cells[0].Value) == true)
                    {
                        empList += dgvEMPSelect.Rows[i].Cells[1].Value.ToString() + " |";
                    }
                }

                btnAdd_Click(null, null);
            }
        }

        private void dgvAllocations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (MessageBox.Show("Do you want to remove this record?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                        {
                            oMainListHdr.Remove(oMainListHdr.Find(x => x.STH_EMP_CD == dgvAllocations.Rows[e.RowIndex].Cells["TechnicianA"].Value.ToString() && x.STH_JOBNO == dgvAllocations.Rows[e.RowIndex].Cells["JOBNumber"].Value.ToString()));
                            dgvAllocations.DataSource = new List<Service_Tech_Aloc_Hdr>();
                            if (oMainListHdr.Count > 0)
                            {
                                dgvAllocations.DataSource = oMainListHdr;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void dgvEMPSelect_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (Convert.ToBoolean(dgvEMPSelect.Rows[e.RowIndex].Cells[0].Value) == true)
                {
                    dgvEMPSelect.Rows[e.RowIndex].Cells[0].Value = false;
                }
                else
                {
                    dgvEMPSelect.Rows[e.RowIndex].Cells[0].Value = true;
                }
            }
        }

        private void btnSearchHistory_Click(object sender, EventArgs e)
        {
            txtAHArea_Leave(null, null);
            getHistory(dtpFromH.Value, dtpToH.Value);
        }

        private void dtpFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchJob_Click(null, null);
            }
        }

        private void dtpTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchJob_Click(null, null);
            }
        }

        private void dtpFromH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchHistory_Click(null, null);
            }
        }

        private void dtpToH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchHistory_Click(null, null);
            }
        }

        private void dgvAllocationHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                string jobNo = dgvAllocationHistory.Rows[e.RowIndex].Cells["JobNumAH"].Value.ToString();
                ServiceJobDetailsViwer oJobDetailsViwer = new ServiceJobDetailsViwer(jobNo, -777);
                oJobDetailsViwer.ShowDialog();
            }
        }

        private void btnHideMI_Click(object sender, EventArgs e)
        {
            //pnlMoreDetails.Visible = false;
        }

        private void btnSESearchSkill_Click(object sender, EventArgs e)
        {
        }

        private void btnSESearchDesignation_Click(object sender, EventArgs e)
        {
        }

        private void btnSESearchEMP_Click(object sender, EventArgs e)
        {
        }

        private void btnSESearch_Click(object sender, EventArgs e)
        {
            getEMP();
        }

        private void txtAHArea_DoubleClick(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town_new);
            DataTable _result = CHNLSVC.CommonSearch.GetTown_new(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAHArea;
            _CommonSearch.ShowDialog();
            txtAHArea.Select();
        }

        private void ddlSlots_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSlots.SelectedValue == null)
            {
                return;
            }
            Int32 val = 0;
            if (!Int32.TryParse(ddlSlots.SelectedValue.ToString(), out val))
            {
                return;
            }

            if (ddlSlots.SelectedValue != null)
            {
                int asd = CHNLSVC.CustService.GetLocationCurrectSlotCount(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, ddlSlots.SelectedValue.ToString(), dtpFromAL.Value, dtpToAL.Value);
                lblUtilizedCount.Text = asd.ToString() + "/" + TotalSlots;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txtCustomer_DoubleClick(null, null);
        }

        private void txtCustomer_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtCustomer;
                _commonSearch.IsSearchEnter = true; //Add by Chamal 10/Aug/2013
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtCustomer.Select();
            }
            catch (Exception ex)
            { txtCustomer.Clear(); this.Cursor = Cursors.Default; }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtAHEmp_DoubleClick(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = new DataTable();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EMP_ALL);
            _result = CHNLSVC.CommonSearch.GetAllEmp(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAHEmp;
            _CommonSearch.ShowDialog();
            txtAHEmp.Select();
            Cursor = Cursors.Default;
        }

        private void btnAHEmpSearch_Click(object sender, EventArgs e)
        {
            txtAHEmp_DoubleClick(null, null);
        }

        private void btnAHSearchArea_Click(object sender, EventArgs e)
        {
            txtAHArea_DoubleClick(null, null);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtALEmp.Text))
                {
                    DataTable dtTemp = CHNLSVC.Sales.GetManagerDefProfit(BaseCls.GlbUserComCode, txtALEmp.Text);
                    //var query = from r in dtTemp.AsEnumerable()
                    //            where r.Field<string>("esep_com_cd") == BaseCls.GlbUserComCode &&
                    //            r.Field<string>("esep_def_profit") == BaseCls.GlbUserDefProf &&
                    //            r.Field<string>("esep_epf") == txtALEmp.Text.ToString() &&
                    //            r.Field<Int16>("esep_act") == Convert
                    //            select r;
                    var query = dtTemp.AsEnumerable().Where(x => x.Field<string>("esep_com_cd") == BaseCls.GlbUserComCode.ToString() && x.Field<string>("esep_def_profit") == BaseCls.GlbUserDefProf.ToString() && x.Field<string>("esep_epf") == txtALEmp.Text.ToString() && x.Field<Int16>("esep_act") == Convert.ToInt16("1")).ToList();
                    dtTemp = new DataTable();
                    if (query != null && query.Count > 0)
                    {
                        dtTemp = query.CopyToDataTable();
                        //return;
                    }
                    else
                    {
                        MessageBox.Show("Please enter correct EPF number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtEmpCode.Clear();
                        txtEmpCode.Focus();
                        return;
                    }
                }

                bool status = true;

                if (dgvJobDetails.Rows.Count <= 0)
                {
                    MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (dgvJobDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvJobDetails.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgvJobDetails.Rows[i].Cells[0].Value) == true)
                        {
                            status = true;
                            break;
                        }
                        else
                        {
                            status = false;
                        }
                    }

                    if (status == false)
                    {
                        MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                //if (Direction == "F")
                //{
                //    if (string.IsNullOrEmpty(txtAHArea.Text))
                //    {
                //        MessageBox.Show("Please enter a area", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }
                //}

                if (oMainListHdr.Exists(x => x.STH_EMP_CD == txtALEmp.Text))
                {
                    MessageBox.Show("Technician is already added.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                for (int i = 0; i < dgvJobDetails.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvJobDetails.Rows[i].Cells[0].Value) == true)
                    {
                        if (!string.IsNullOrEmpty(txtALEmp.Text))
                        {
                            if (oMainListHdr.Exists(x => x.STH_EMP_CD == txtALEmp.Text))
                            {
                                continue;
                            }

                            Service_Tech_Aloc_Hdr oAloc_Hdr = new Service_Tech_Aloc_Hdr();

                            oAloc_Hdr.STH_SEQ = -1;
                            oAloc_Hdr.STH_ALOCNO = "";
                            oAloc_Hdr.STH_COM = BaseCls.GlbUserComCode;
                            oAloc_Hdr.STH_LOC = BaseCls.GlbUserDefLoca;
                            oAloc_Hdr.STH_TP = "J";
                            oAloc_Hdr.STH_JOBNO = dgvJobDetails.Rows[i].Cells["JobNo"].Value.ToString();
                            oAloc_Hdr.STH_JOBLINE = Convert.ToInt32(dgvJobDetails.Rows[i].Cells["JobLine"].Value.ToString());
                            oAloc_Hdr.STH_EMP_CD = txtALEmp.Text.Trim();
                            oAloc_Hdr.STH_STUS = "A";
                            oAloc_Hdr.STH_CRE_BY = BaseCls.GlbUserID;
                            oAloc_Hdr.STH_CRE_WHEN = DateTime.Now;
                            oAloc_Hdr.STH_MOD_BY = BaseCls.GlbUserID;
                            oAloc_Hdr.STH_MOD_WHEN = DateTime.Now;
                            oAloc_Hdr.STH_SESSION_ID = BaseCls.GlbUserSessionID;
                            oAloc_Hdr.STH_TOWN = "";
                            oAloc_Hdr.STH_TOWN = lblTown.Tag.ToString();
                            oAloc_Hdr.MT_DESC = lblTown.Text;
                            oAloc_Hdr.STH_FROM_DT = dtpFromAL.Value;
                            oAloc_Hdr.STH_TO_DT = dtpToAL.Value;
                            oAloc_Hdr.STH_REQNO = "";
                            oAloc_Hdr.STH_REQLINE = 0;
                            oAloc_Hdr.STH_CURR_STUS = 1;
                            if (ddlSlots.SelectedValue != null)
                            {
                                oAloc_Hdr.STH_TERMINAL = Convert.ToInt32(ddlSlots.SelectedValue.ToString());
                            }
                            DataTable dtTemp = CHNLSVC.Sales.GetManagerDefProfit(BaseCls.GlbUserComCode, txtALEmp.Text);
                            oAloc_Hdr.ESEP_FIRST_NAME = dtTemp.Rows[0]["ESEP_FIRST_NAME"].ToString();
                            oMainListHdr.Add(oAloc_Hdr);
                        }
                    }
                }
                txtALEmp.Clear();
                dgvAllocations.DataSource = new List<Service_Tech_Aloc_Hdr>();
                if (oMainListHdr.Count > 0)
                {
                    dgvAllocations.DataSource = oMainListHdr;
                }
            }
            else if (e.KeyCode == Keys.F2)
            {
                txtALEmp_DoubleClick(null, null);
            }
        }

        //private void dgvAllocations_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        //{
        //    if (dgvAllocations.IsCurrentCellDirty)
        //    {
        //        dgvAllocations.CommitEdit(DataGridViewDataErrorContexts.Commit);
        //    }
        //}

        private void dgvJobDetails_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvJobDetails.IsCurrentCellDirty)
            {
                dgvJobDetails.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvAllocationHistory_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvAllocationHistory.IsCurrentCellDirty)
            {
                dgvAllocationHistory.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvDefects_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvDefects.IsCurrentCellDirty)
            {
                dgvDefects.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void txtCustomer_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustomer.Text))
            {
                DataTable dtTemp = CHNLSVC.Inventory.GetSupplier(BaseCls.GlbUserComCode, txtCustomer.Text.Trim());
                if (dtTemp.Rows.Count == 0)
                {
                    MessageBox.Show("Please enter correct customer code", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCustomer.Clear();
                    txtCustomer.Focus();
                    return;
                }
            }
        }

        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtCustomer_DoubleClick(null, null);
            }
        }

        private void txtAHEmp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtAHEmp_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtAHEmp_Leave(null, null);
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            txtALEmp_DoubleClick(null, null);
        }

        private void txtALEmp_DoubleClick(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = new DataTable();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EMP_ALL);
            _result = CHNLSVC.CommonSearch.GetAllEmp(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtALEmp;
            _CommonSearch.ShowDialog();
            txtALEmp.Select();
            Cursor = Cursors.Default;
        }

        private void txtAHArea_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAHArea.Text))
            {
                DataTable dtTemp = CHNLSVC.General.GetTownByDesc(txtAHArea.Text);
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    txtAHArea.Tag = dtTemp.Rows[0]["mt_cd"].ToString();
                    txtAHArea.Text = dtTemp.Rows[0]["mt_desc"].ToString();
                }
                else
                {
                    MessageBox.Show("Please enter correct town", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAHArea.Clear();
                    txtAHArea.Focus();
                    return;
                }
            }
        }

        private void txtAHArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAHArea_Leave(null, null);
            }
            else if (e.KeyCode == Keys.F2)
            {
                txtAHArea_DoubleClick(null, null);
            }
        }

        private void lblSearial_MouseHover(object sender, EventArgs e)
        {
            //System.Windows.Forms.ToolTip tooltip = new System.Windows.Forms.ToolTip();
            toolTip1.IsBalloon = true;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(lblSearial, lblSearial.Text);
        }

        private void txtAHEmp_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAHEmp.Text))
            {
                DataTable dtTemp = CHNLSVC.Sales.GetManagerDefProfit(BaseCls.GlbUserComCode, txtAHEmp.Text);
                if (dtTemp.Rows.Count == 0)
                {
                    MessageBox.Show("Please enter correct EPF number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAHEmp.Clear();
                    txtAHEmp.Focus();
                    return;
                }
            }
        }

        private void dtpFromAL_ValueChanged(object sender, EventArgs e)
        {
            //if (dtpFromAL.Value < DateTime.Now)
            //{
            // //   MessageBox.Show("Please select valied date", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    dtpFromAL.Value = DateTime.Now;
            //    return;
            //}
        }

        private void dtpToAL_ValueChanged(object sender, EventArgs e)
        {
            if (dtpToAL.Value < DateTime.Now)
            {
                dtpToAL.Value = DateTime.Now;
                return;
            }
        }

        private void txtALEmp_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtALEmp.Text))
            {
                DataTable dtTemp = CHNLSVC.Sales.GetManagerDefProfit(BaseCls.GlbUserComCode, txtALEmp.Text);
                var query = dtTemp.AsEnumerable().Where(x => x.Field<string>("esep_com_cd") == BaseCls.GlbUserComCode.ToString() && x.Field<string>("esep_def_profit") == BaseCls.GlbUserDefProf.ToString() && x.Field<string>("esep_epf") == txtALEmp.Text.ToString() && x.Field<Int16>("esep_act") == Convert.ToInt16("1")).ToList();
                dtTemp = new DataTable();
                if (query != null && query.Count > 0)
                {
                    dtTemp = query.CopyToDataTable();
                }
                else
                {
                    MessageBox.Show("Please enter correct EPF number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEmpCode.Clear();
                    txtEmpCode.Focus();
                    return;
                }
            }
        }

        private void dgvAllocations_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvAllocations.IsCurrentCellDirty)
            {
                dgvAllocations.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvAllocations_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAllocations.Rows.Count > 0)
            {
                if (e.RowIndex != -1)
                {
                    if (dgvAllocations.Rows[e.RowIndex].Cells["STH_CURR_STUS"].Value != null && Convert.ToInt32(dgvAllocations.Rows[e.RowIndex].Cells["STH_CURR_STUS"].Value) == 1)
                    {
                        Service_Tech_Aloc_Hdr oItem = oMainListHdr.Find(x => x.STH_EMP_CD == dgvAllocations.Rows[e.RowIndex].Cells["TechnicianA"].Value.ToString() && x.STH_JOBNO == dgvAllocations.Rows[e.RowIndex].Cells["JOBNumber"].Value.ToString());
                        oItem.STH_CURR_STUS = 1;
                    }
                    else
                    {
                        Service_Tech_Aloc_Hdr oItem = oMainListHdr.Find(x => x.STH_EMP_CD == dgvAllocations.Rows[e.RowIndex].Cells["TechnicianA"].Value.ToString() && x.STH_JOBNO == dgvAllocations.Rows[e.RowIndex].Cells["JOBNumber"].Value.ToString());
                        oItem.STH_CURR_STUS = 0;
                    }
                }
            }
        }

        private void chkAllocated_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllocated.Checked)
            {
                chkCusExpect.Checked = false;
            }
        }

        private void chkCusExpect_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCusExpect.Checked)
            {
                chkAllocated.Checked = false;
            }
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            if (dtpTo.Value < dtpFrom.Value)
            {
                MessageBox.Show("Please enter valid date range.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpTo.Value = dtpFrom.Value;
                return;
            }
        }

        private void dtpToH_ValueChanged(object sender, EventArgs e)
        {
            if (dtpToH.Value < dtpFromH.Value)
            {
                MessageBox.Show("Please enter valid date range.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpToH.Value = dtpFromH.Value;
                return;
            }
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            if (dtpTo.Value < dtpFrom.Value)
            {
                MessageBox.Show("Please enter valid date range.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpFrom.Value = dtpTo.Value;
                return;
            }
        }

        private void dtpFromH_ValueChanged(object sender, EventArgs e)
        {
            if (dtpToH.Value < dtpFromH.Value)
            {
                MessageBox.Show("Please enter valid date range.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpFromH.Value = dtpToH.Value;
                return;
            }
        }

        #endregion Events

        #region Methods

        private void GetJobDetails()
        
{
            string stage = string.Empty;
            Int32 IsCusExpected = 0;

            if (chkAllocated.Checked)
            {
                stage = "3,4,4.1,5,5.1";
            }
            else
            {
                stage = "2";
            }
            if (chkCusExpect.Checked)
            {
                IsCusExpected = 1;
            }
            DateTime from, to;
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                from = Convert.ToDateTime("01-01-1111");
                to = Convert.ToDateTime("31-12-2999");
            }
            else
            {
                from = dtpFrom.Value.Date;
                to = dtpTo.Value.Date;
            }

            DataTable DtDetails = new DataTable();
            DtDetails = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, from, to, txtJobNo.Text, stage, IsCusExpected, txtCustomer.Text, BaseCls.GlbUserDefProf, Convert.ToString( txtTown.Tag));//txtTown.Text);
            DataTable temp=CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, from, to, txtJobNo.Text, stage, IsCusExpected, txtCustomer.Text, BaseCls.GlbUserDefProf, txtTown.Text);//txtTown.Text);  

             DtDetails.Merge(temp);
            if (DtDetails.Rows.Count > 0)
            {
                dgvJobDetails.DataSource = DtDetails;

                if (DtDetails.Select("JBD_LOC = '" + txtLocation.Text + "'").Length > 0)
                {
                    dgvJobDetails.DataSource = null;
                    dgvJobDetails.DataSource = DtDetails.Select("JBD_LOC = '" + txtLocation.Text + "'").CopyToDataTable();
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtLocation.Text))
                    {
                        dgvJobDetails.DataSource = null;
                    }
                    else
                    {
                        dgvJobDetails.DataSource = DtDetails;
                    }
                }
            }
            else
            {
                if (!IsLoad)
                {
                    dgvJobDetails.DataSource = null;
                    dgvAllocations.DataSource = null;
                    dgvDefects.DataSource = null;
                    dgvAllocations.DataSource = new List<Service_Tech_Aloc_Hdr>();
                    dgvDefects.DataSource = new List<Service_Job_Defects>();
                    MessageBox.Show("No data found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private bool CheckSingleRow(DataGridView dgv, string columnName)
        {
            bool status = false;

            if (dgv.Rows.Count > 0)
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgv.Rows[i].Cells[columnName].Value) == true)
                    {
                        status = true;
                    }
                }
            }
            return status;
        }

        private void LoadDefects(string jobNo, string lineNo)
        {
            dgvDefects.DataSource = null;
            List<Service_Job_Defects> oDefets = new List<Service_Job_Defects>();
            oDefets = CHNLSVC.CustService.GetJobDefects(jobNo, Convert.ToInt32(lineNo), "J");
            dgvDefects.DataSource = oDefets;
        }

        private void getEMP()
        {
            DataTable dt = CHNLSVC.CustService.GetEmployeBySkillDesignation(BaseCls.GlbUserComCode, txtSkills.Text, txtDesignation.Text, txtEmpCode.Text, BaseCls.GlbUserDefProf);
            dgvEMPSelect.DataSource = null;
            if (dt.Rows.Count > 0)
            {
                dgvEMPSelect.DataSource = dt;
            }
        }

        private bool validateAdd()
        {
            bool status = true;

            if (dgvJobDetails.Rows.Count <= 0)
            {
                status = false;
                MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return status;
            }

            if (dgvJobDetails.Rows.Count > 0)
            {
                for (int i = 0; i < dgvJobDetails.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvJobDetails.Rows[i].Cells[0].Value) == true)
                    {
                        status = true;
                        break;
                    }
                    else
                    {
                        status = false;
                    }
                }

                if (status == false)
                {
                    MessageBox.Show("Please select a job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return status;
                }
            }
            if (Direction == "F")
            {
                if (string.IsNullOrEmpty(txtAHArea.Text))
                {
                    status = false;
                    MessageBox.Show("Please enter a area", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return status;
                }
            }
            //if (dgvDefects.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dgvDefects.Rows.Count; i++)
            //    {
            //        if (Convert.ToBoolean(dgvDefects.Rows[i].Cells[0].Value) == true)
            //        {
            //            status = true;
            //            break;
            //        }
            //        else
            //        {
            //            status = false;
            //            MessageBox.Show("Please select a defect", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            return status;
            //        }
            //    }
            //}
            if (dgvEMPSelect.Rows.Count <= 0)
            {
                status = false;
                MessageBox.Show("Please select technician", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnEMPSearch.Focus();
                return status;
            }
            if (dgvEMPSelect.Rows.Count > 0)
            {
                for (int i = 0; i < dgvEMPSelect.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvEMPSelect.Rows[i].Cells[0].Value) == true)
                    {
                        status = true;
                        break;
                    }
                    else
                    {
                        status = false;
                        status = false;
                    }
                }

                if (status == false)
                {
                    MessageBox.Show("Please select a technician", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return status;
                }
            }

            return status;
        }

        private void getHistory(DateTime From, DateTime To)
        {
            string area = string.Empty;
            dgvAllocationHistory.DataSource = null;
            if (!string.IsNullOrEmpty(txtAHArea.Text) && txtAHArea.Tag != null)
            {
                area = txtAHArea.Tag.ToString();
            }
            List<Service_TechAllocation> oItems;
            DataTable dt = CHNLSVC.CustService.GetAllocatedHistory(From, To, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, area, txtAHEmp.Text, out oItems);
            if (dt.Rows.Count > 0)
            {
                List<Service_TechAllocation> oItemsnEW = oItems.FindAll(X => X.STH_FROM_DT.AddSeconds(-X.STH_FROM_DT.Second) >= From.AddSeconds(-From.Second) && X.STH_TO_DT.AddSeconds(-X.STH_TO_DT.Second) <= To.AddSeconds(-To.Second));
                if (oItemsnEW.Count > 0)
                {
                    dgvAllocationHistory.DataSource = oItemsnEW;
                }
                else
                {
                    dgvAllocationHistory.DataSource = new List<Service_TechAllocation>();
                }
            }
            else
            {
                if (!IsLoad)
                {
                    dgvAllocationHistory.DataSource = null;
                    MessageBox.Show("No data found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LoadCapacity(String type)
        {
            int capacity = CHNLSVC.CustService.GetLocationCapacity(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, type);
            TotalSlots = capacity;
            if (capacity > 0)
            {
                lblSlot.Visible = true;
                ddlSlots.Visible = true;
                label4.Visible = true;
                lblUtilizedCount.Visible = true;

                List<ComboBoxObject> oComboBoxObjects = new List<ComboBoxObject>();
                for (int i = 1; i < capacity + 1; i++)
                {
                    ComboBoxObject item = new ComboBoxObject();
                    item.Value = i.ToString();
                    item.Text = SlotText + " " + i.ToString();
                    oComboBoxObjects.Add(item);
                }

                ddlSlots.DataSource = oComboBoxObjects;
                ddlSlots.DisplayMember = "Text";
                ddlSlots.ValueMember = "Value";
            }
            else
            {
                ddlSlots.DataSource = null;
                lblSlot.Visible = false;
                ddlSlots.Visible = false;
                label4.Visible = false;
                lblUtilizedCount.Visible = false;
            }
        }

        private bool isAnySelected()
        {
            bool status = false;

            if (dgvAllocations.Rows.Count > 0)
            {
                for (int i = 0; i < dgvAllocations.Rows.Count; i++)
                {
                    if (dgvAllocations.Rows[i].Cells["STH_CURR_STUS"].Value != null && Convert.ToInt64(dgvAllocations.Rows[i].Cells["STH_CURR_STUS"].Value.ToString()) == 1)
                    {
                        status = true;
                        break;
                    }
                }
            }
            return status;
        }

        private void clearAll()
        {
            chkAllocated.Checked = false;
            chkCusExpect.Checked = false;
            txtJobNo.Clear();
            dtpFrom.Value = StartDate;
            dtpTo.Value = DateTime.Today;

            dgvJobDetails.DataSource = null;

            dgvAllocationHistory.DataSource = null;

            dgvDefects.DataSource = new List<Service_Job_Defects>();

            dgvAllocations.DataSource = new List<Service_Tech_Aloc_Hdr>();

            txtEmpCode.Clear();
            txtSkills.Clear();
            txtDesignation.Clear();
            lblItem.Text = "";
            lblSearial.Text = "";
            lblItem.Text = "";

            Direction = string.Empty;
            oMainListHdr.Clear();
            lblUtilizedCount.Text = "";
            ddlSlots.DataSource = null;
            lblSlot.Visible = false;
            ddlSlots.Visible = false;
            label4.Visible = false;
            lblUtilizedCount.Visible = false;
            txtALEmp.Clear();
            txtAHEmp.Clear();
            txtAHArea.Clear();
            toolTip1.RemoveAll();
            dtpFromH.Value = StartDate;
            dtpFrom.Value = StartDate;
            dtpTo.Value = DateTime.Now;
            dtpToH.Value = DateTime.Now;
            txtAHArea.Tag = "";
            lblTown.Tag = "";
            lblTown.Text = "";
            pnlHistory.Hide();
            pnlHistory.Size = new Size(775, 504);

            txtLocation.Text = "";

            dtpFromAL.Value = DateTime.Now;
            dtpToAL.Value = DateTime.Now.AddHours(1);

            txtCustomer.Clear();

            dtpFromH.Value = DateTime.Now.AddMonths(-1);
        }

        #endregion Methods

        private void btnSearchloc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLocation;
                _CommonSearch.txtSearchbyword.Text = txtLocation.Text;
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

        private void txtLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchJob_Click(null, null);
            }
        }
              

        private void btnHist_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblSearial.Text))
            {
                if (lblSearial.Text.ToUpper().ToString() != "N/A")
                {
                    pnlMain.Enabled = false;
                    lblDefectHistyHeader.Text = ":: Job History :: [ Item :- " + lblItem.Text + " Serial :-" + lblSearial.Text + " ]";
                    ucDefectHistory1.Serial = lblSearial.Text;
                    ucDefectHistory1.Item = lblItem.Text;
                    ucDefectHistory1.loadData();
                    pnlHistory.Show();
                    //dtpFromAL.Focus();
                }
            }
        }

        private void btnCloseHistory_Click_1(object sender, EventArgs e)
        {
            pnlHistory.Hide();
            pnlMain.Enabled = true;
        }

        private void btn_serch_teamcd_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = new DataTable();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.tec_team_cd);
            _result = CHNLSVC.CommonSearch.Get_tec_by_teamcd(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtteamcd;
            _CommonSearch.ShowDialog();
            txtteamcd.Select();
            Cursor = Cursors.Default;
            load_tec_by_teamcd();
        }
        private void load_tec_by_teamcd()
        {
            DataTable dt = CHNLSVC.CustService.GET_ALLEMP_BY_TEAM_CD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtteamcd.Text);
            dgvEMPSelect.DataSource = null;
            if (dt.Rows.Count > 0)
            {
                dgvEMPSelect.DataSource = dt;
            }
        }

        private void txtteamcd_DoubleClick(object sender, EventArgs e)
        {
            btn_serch_teamcd_Click(null, null);
        }

        private void txtteamcd_Leave(object sender, EventArgs e)
        {
            load_tec_by_teamcd();
        }

        private void txtteamcd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                load_tec_by_teamcd();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSerchTown_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;

                _CommonSearch.IsReturnFullRow = true;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
                DataTable _result = CHNLSVC.CommonSearch.GetTown(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtTown;
                _CommonSearch.ShowDialog();

                txtTown.Select();
                string _selectedRow = _CommonSearch.UserSelectedRow;
                if (!string.IsNullOrEmpty(_selectedRow))
                {
                    var _tmpData = _selectedRow.Split(new string[] { "|" }, StringSplitOptions.None).ToList();
                    if (_tmpData != null && _tmpData.Count > 0)
                    {
                        txtTown.Tag = _tmpData[3];
                       
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

        //Tharindu
        private void getJobJetails()
        {
            DataTable DtDetails = new DataTable();
            string stage = string.Empty;
            Int32 IsCusExpected = 0;

            stage = "3,2,6,4,5.1,5";
         //   stage = ddlStages.SelectedValue.ToString();
            //DtDetails = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1911"), Convert.ToDateTime("31-12-2999"), txtJobNo.Text, stage, IsCusExpected, string.Empty, BaseCls.GlbUserDefProf);

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10816))
            {
                DtDetails = CHNLSVC.CustService.GetJObsFOrWIP(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1911"), Convert.ToDateTime("31-12-2999"), txtJobNo.Text, stage, IsCusExpected, string.Empty, BaseCls.GlbUserDefProf, "GET_ALL_JOBS");
            }
            else
            {
                DtDetails = CHNLSVC.CustService.GetJObsFOrWIP(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1911"), Convert.ToDateTime("31-12-2999"), txtJobNo.Text, stage, IsCusExpected, string.Empty, BaseCls.GlbUserDefProf, BaseCls.GlbUserID);
            }

            if (DtDetails.Rows.Count > 0)
            {
                DataView dv = DtDetails.DefaultView;
                dv.Sort = "jbd_jobline";
                DataTable sortedDT = dv.ToTable();

                dgvJobDetails.DataSource = sortedDT;
             //   modifyJobDetailGrid();


                Service_JOB_HDR oJOB_HDR = CHNLSVC.CustService.GetServiceJobHeader(txtJobNo.Text, BaseCls.GlbUserComCode);

                lblName.Text = oJOB_HDR.SJB_B_CUST_TIT + " " + oJOB_HDR.SJB_B_CUST_NAME;
                lblAddrss.Text = oJOB_HDR.SJB_B_ADD1 + "  " + oJOB_HDR.SJB_B_ADD2 + "  " + oJOB_HDR.SJB_B_ADD3;
                lblTele.Text = oJOB_HDR.SJB_B_MOBINO;
                lblContactPerson.Text = oJOB_HDR.SJB_CNT_PERSON;
                lblConPhone.Text = oJOB_HDR.SJB_CNT_PHNO;
                lblCustomerCode.Text = oJOB_HDR.SJB_CUST_CD;
                //kapila 23/2/2016
                lblContEmail.Text = oJOB_HDR.SJB_EMAIL;
                lblContNo.Text = oJOB_HDR.SJB_CNT_PHNO;
                lblContName.Text = oJOB_HDR.SJB_CNT_PERSON;

                //lblJobStage.Text = oJOB_HDR.SJB_JOBSTAGE_TEXT;
                ////lblJobCategori.Text = oJOB_HDR.SJB_JOBCAT;
                ////lblLevel.Text = oJOB_HDR.SJB_PRORITY;
                ////txtInstruction.Text = oJOB_HDR.SJB_TECH_RMK;
                ////txtJobRemarks.Text = oJOB_HDR.SJB_JOB_RMK;
                ////lblJobStageNew.Text = oJOB_HDR.SJB_JOBSTAGE.ToString();
                //if (oJOB_HDR.SJB_JOBSTAGE > 3)
                //{
                //    button1.Enabled = false;
                //}
                ////enableDisableBtns(false);
                //dgvJobDetails.Focus();

                //if (sortedDT.Rows.Count == 1)
                //{
                //    dgvJobDetails_CellClick(dgvJobDetails, new DataGridViewCellEventArgs(0, 0));
                //}
                int lineNo = int.Parse(sortedDT.Rows[0]["JBD_JOBLINE"].ToString());
              

                List<Service_job_Det> ojob_Det = CHNLSVC.CustService.GetJobDetails(txtJobNo.Text, lineNo, BaseCls.GlbUserComCode);
              //  lblStageText.Text = ojob_Det[0].StageText;
                pnlcusdetails.Visible = true;

                if (ojob_Det[0].Jbd_techst_dt_man != DateTime.MinValue)
                {
                    //lblStartOpenDate.Text = ojob_Det[0].Jbd_techst_dt_man.ToString("dd/MMM/yyyy  hh:mm tt");
                    //enableDisableBtns(true);
                    //btnStartOpenJob.Enabled = false;
                    FillItemDetails(ojob_Det[0]);
                }
                else
                {
                    //lblStartOpenDate.Text = "";
                    //enableDisableBtns(false);
                    FillItemDetails(ojob_Det[0]);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid job number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnClear_Click(null, null);
                return;
            }
        }


        private void FillItemDetails(Service_job_Det _warrItem)
        {
            lblItem.Text = _warrItem.Jbd_itm_cd;
            lblItemDesc.Text = _warrItem.Jbd_itm_desc;
            lblModel.Text = _warrItem.Jbd_model;
            lblBrand.Text = _warrItem.Jbd_brand;
            lblSerNo.Text = _warrItem.Jbd_ser1;
            lblWarNo.Text = _warrItem.Jbd_warr;
            string _warrStatus = string.Empty;

            if (_warrItem.Jbd_warrstartdt.AddMonths(_warrItem.Jbd_warrperiod).Date >= CHNLSVC.Security.GetServerDateTime().Date)
            {
                _warrStatus = "UNDER WARRANTY"; lblWarStus.ForeColor = Color.Green;
            }
            else
            {
                _warrStatus = "OVER WARRANTY"; lblWarStus.ForeColor = Color.Red;
            }

            lblWarStus.Text = _warrStatus;
            lblWarStart.Text = _warrItem.Jbd_warrstartdt.ToString("dd-MMM-yyyy");
            lblWarEnd.Text = _warrItem.Jbd_warrstartdt.AddMonths(_warrItem.Jbd_warrperiod).ToString("dd-MMM-yyyy");
            lblWarPrd.Text = _warrItem.Jbd_warrperiod.ToString();
            lblWarRemain.Text = (_warrItem.Jbd_warrstartdt.AddMonths(_warrItem.Jbd_warrperiod).Date - CHNLSVC.Security.GetServerDateTime().Date).TotalDays.ToString();
            lblWarRem.Text = _warrItem.Jbd_warrrmk;

            if (lblWarRemain.Text.Contains("-"))
            {
                lblWarRemain.Text = "N/A";
            }

            lblInv.Text = _warrItem.Jbd_invc_no;

            if (_warrItem.Jbd_date_pur < Convert.ToDateTime("01-01-1800"))
            {
                lblInvDt.Text = "";
            }
            else
            {
                lblInvDt.Text = _warrItem.Jbd_date_pur.ToString("dd-MMM-yyyy");
            }
            //lblAccNo.Text = _warrItem.Irsm_acc_no;
            //lblDelLoc.Text = _warrItem.Irsm_loc;
            //lblDelLocDesc.Text = _warrItem.Irsm_loc_desc;

            MasterItem _itemdetail = new MasterItem();
            _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _warrItem.Jbd_itm_cd);

            //txtMobile.Text = _warrItem.Irsm_cust_mobile;
            //txtCustCode.Text = _warrItem.Irsm_cust_cd;
            //txtCusName.Text = _warrItem.Irsm_cust_name;
            //txtAddress1.Text = _warrItem.Irsm_cust_addr;

            lblSuppCode.Text = _warrItem.Jbd_supp_cd;
            //lblItmTp.Text = _warrItem.Jbd_itmtp;
            lblItemCat.Text = (_itemdetail == null) ? string.Empty : _itemdetail.Mi_cate_1;

            //lblDelLoc.Text = _warrItem.Irsm_loc;
            //lblDelLocDesc.Text = _warrItem.Irsm_loc_desc;
            //kapila 23/6/2017 - due to slowness
            //if (_warrItem.Jbd_warr != "N/A")
            //    Load_Serial_Infor(_warrItem.Jbd_ser1, DateTime.Now.Date, _warrItem.Jbd_warr);

            Service_JOB_HDR _scvjobHdr = CHNLSVC.CustService.GetServiceJobHeader(_warrItem.Jbd_jobno, BaseCls.GlbUserComCode);

            lblBuyerCustCode.Text = _scvjobHdr.SJB_CUST_CD;
            lblBuyerCustName.Text = _scvjobHdr.SJB_CUST_NAME;
            lblBuyerCustAdd1.Text = _scvjobHdr.SJB_ADD1;
            lblBuyerCustAdd2.Text = _scvjobHdr.SJB_ADD2;
            lblBuyerCustMobi.Text = _scvjobHdr.SJB_MOBINO;
        }

        private void btnMore1_Click(object sender, EventArgs e)
        {
            //if (btnMore1.Text == "More >>")
            //{
            //    pnlWarr.Height = 293;
            //    btnMore1.Text = "<< More";
            //    btnMore1.BackColor = Color.Orange;
            //    btnMore1.ForeColor = Color.Maroon;
            //}
            //else
            //{
            //    pnlWarr.Height = 109;
            //    btnMore1.Text = "More >>";
            //    btnMore1.BackColor = Color.Maroon;
            //    btnMore1.ForeColor = Color.White;
            //}
            if (btnMore1.Text == "More >>")
            {
                pnlWarr.Height = 293;
                //if (optReq.Checked == true) pnlWarr.Height = 392;

                btnMore1.Text = "<< More";
                btnMore1.BackColor = Color.Orange;
                btnMore1.ForeColor = Color.Maroon;

                if (pnlSuppWarr.Visible == true)
                {
                    pnlWarr.Height = 488;
                    pnlcusdetails.Height = 488;
                }
                else
                {
                    pnlWarr.Height = 392;
                    pnlcusdetails.Height = 239;
                }
            }
            else
            {
                pnlWarr.Height = 109;
                btnMore1.Text = "More >>";
                btnMore1.BackColor = Color.Maroon;
                btnMore1.ForeColor = Color.White;
                pnlcusdetails.Height = 239;
            }
        }

        private void btnpnlcuscls_Click(object sender, EventArgs e)
        {
            pnlcusdetails.Visible = false;
        }

        private void clrScreen()
        {
           // txtJobNo.Clear();
            lblName.Text = "";
            lblTele.Text = "";
            lblContEmail.Text = "";
            lblContNo.Text = "";
            lblContName.Text = "";
            lblCustomerCode.Text = "";
         //   dgvJobDetails.DataSource = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1800"), Convert.ToDateTime("01-01-1800"), txtJobNo.Text, "0", 0, string.Empty, BaseCls.GlbUserDefProf);
          //  dgvDefects.DataSource = new List<Service_Job_Defects>();
            lblItem.Text = "";
            lblItemDesc.Text = "";
            lblModel.Text = "";
            lblBrand.Text = "";
            lblSerNo.Text = "";
            lblWarStus.Text = "";
            lblWarStart.Text = "";
            lblWarEnd.Text = "";
            lblAttempt.Text = "";
            lblWarPrd.Text = "";
            lblWarRemain.Text = "";
            lblWarRem.Text = "";
            lblWarNo.Text = "";
            lblAddrss.Text = "";
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
            {
                for (int i = 0; i < dgvJobDetails.RowCount; i++)
                {
                    dgvJobDetails[0, i].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < dgvJobDetails.RowCount; i++)
                {
                    dgvJobDetails[0, i].Value = false;
                }
            }
        }

       
        private void serch_jobbystage(string _stage)
        {
            string stage = string.Empty;
            Int32 IsCusExpected = 0;
            stage = _stage;

            if (chkCusExpect.Checked)
            {
                IsCusExpected = 1;
            }
            DateTime from, to;
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                from = Convert.ToDateTime("01-01-1111");
                to = Convert.ToDateTime("31-12-2999");
            }
            else
            {
                from = dtpFrom.Value.Date;
                to = dtpTo.Value.Date;
            }

            DataTable DtDetails = new DataTable();
            DtDetails = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, from, to, txtJobNo.Text, stage, IsCusExpected, txtCustomer.Text, BaseCls.GlbUserDefProf, Convert.ToString(txtTown.Tag));//txtTown.Text);
            //DataTable temp = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, from, to, txtJobNo.Text, stage, IsCusExpected, txtCustomer.Text, BaseCls.GlbUserDefProf, txtTown.Tag);//txtTown.Text);  

            //DtDetails.Merge(temp);
            if (DtDetails.Rows.Count > 0)
            {
                dgvJobDetails.DataSource = DtDetails;

                if (DtDetails.Select("JBD_LOC = '" + txtLocation.Text + "'").Length > 0)
                {
                    dgvJobDetails.DataSource = null;
                    dgvJobDetails.DataSource = DtDetails.Select("JBD_LOC = '" + txtLocation.Text + "'").CopyToDataTable();
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtLocation.Text))
                    {
                        dgvJobDetails.DataSource = null;
                    }
                    else
                    {
                        dgvJobDetails.DataSource = DtDetails;
                    }
                }
            }
            else
            {
                if (!IsLoad)
                {
                    dgvJobDetails.DataSource = null;
                    dgvAllocations.DataSource = null;
                    dgvDefects.DataSource = null;
                    dgvAllocations.DataSource = new List<Service_Tech_Aloc_Hdr>();
                    dgvDefects.DataSource = new List<Service_Job_Defects>();
                    MessageBox.Show("No data found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btn_workingpro_serch_Click(object sender, EventArgs e)
        {
            clearAll();
            Cursor.Current = Cursors.WaitCursor;
            string _stage = "4,4.1,5,5.1,5.2";
            serch_jobbystage(_stage);
           // set_grid_color("DeepSkyBlue");
            this.dgvJobDetails.Columns["sth_from_dt"].Visible = true;
            Cursor.Current = Cursors.Default;
        }

        private void btn_reshed_serch_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            clearAll();
            string _stage = "3";
            serch_jobbystage(_stage);
            set_grid_color("Red");
            this.dgvJobDetails.Columns["sth_from_dt"].Visible = true;
            Cursor.Current = Cursors.Default;
        }
        private void btnshed_serch_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            clearAll();
            string _stage = "2";
            serch_jobbystage(_stage);
          // set_grid_color("Orange");
           this.dgvJobDetails.Columns["sth_from_dt"].Visible = false;
           Cursor.Current = Cursors.Default;
           
        }
        private void set_grid_color(string _colurt)
        {
            if (_colurt=="DeepSkyBlue")
            {
                foreach (DataGridViewRow row in dgvJobDetails.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.DeepSkyBlue;
                    row.ReadOnly = true;
                }
            }
            if (_colurt == "Orange")
            {
                foreach (DataGridViewRow row in dgvJobDetails.Rows)
                {
                   // e.Row.Cells[index].Visible = false;

                    //row.DefaultCellStyle.BackColor = Color.Orange;
                    //row.ReadOnly = true;
                }
            }
            if (_colurt == "Red")
            {
                foreach (DataGridViewRow row in dgvJobDetails.Rows)
                {
                    row.DefaultCellStyle.ForeColor = Color.Red;
                    row.ReadOnly = true;
                }
            }
           
        }

        private void btnclspnljobupdate_Click(object sender, EventArgs e)
        {
            pnl_job_detupdate.Visible = false;
            cler_job_hdr();
        }

        private void btnupdateexpecteddt_Click(object sender, EventArgs e)
        {
            string _err=string.Empty;
            Int32 eff = CHNLSVC.CustService.update_svc_hdr_cst_exdate(lbl_jobno.Text.Trim(), Convert.ToInt32(lbljobline.Text), newcustex_date.Value.Date, BaseCls.GlbUserID, out _err);
            if (eff == -1)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Process Terminated. " + _err, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Technician allocated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cler_job_hdr();
                return;
            }
        }
        private void cler_job_hdr()
        {
            lblcustname.Text = "";
            lblcust_tp.Text = "";
            lblcustadd.Text = ""; ;
            lbljobline.Text = "";
            custex_date.Value = DateTime.Now.Date;
            newcustex_date.Value = DateTime.Now.Date;
            pnl_job_detupdate.Visible = false;
        }
      
    }
}