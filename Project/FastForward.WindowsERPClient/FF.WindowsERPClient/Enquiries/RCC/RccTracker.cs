using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Threading;
using System.Collections;
using FF.WindowsERPClient.Services;
using FF.WindowsERPClient.CommonSearch;

namespace FF.WindowsERPClient.Enquiries.RCC
{
    public partial class RccTracker : Base
    {
        //SP_SEARCH_rcc =new
        //SP_SEARCH_rccByDate =new
        //get_INT_RCC_STAGES_INFO =NEW

        DataTable DT_Progress = new DataTable();
        string selected_rcc_stage = "";

        private void createProgressTable()
        {
            DT_Progress = new DataTable();
            DT_Progress.Columns.Add("stage", typeof(string));
            DT_Progress.Columns.Add("stageName", typeof(string));
            DT_Progress.Columns.Add("date", typeof(string));
            DT_Progress.Columns.Add("dateDiff", typeof(string));

            DataRow dr1, dr2, dr3, dr4, dr5;
            //-----stage 1----------------
            dr1 = DT_Progress.NewRow();
            dr1["stage"] = 1;
            dr1["stageName"] = ("RCC Raised On").ToUpper();
            //dr1["date"] = ;
            DT_Progress.Rows.Add(dr1);

            //-----stage 2----------------
            dr2 = DT_Progress.NewRow();
            dr2["stage"] = 2;
            dr2["stageName"] = ("Job Opened On").ToUpper();
            //dr2["date"] = ;
            DT_Progress.Rows.Add(dr2);

            //-----stage 3----------------
            dr3 = DT_Progress.NewRow();
            dr3["stage"] = 3;
            dr3["stageName"] = ("RCC Compleated On").ToUpper();
            //dr3["date"] = ;
            DT_Progress.Rows.Add(dr3);

            //-----stage 4----------------
            dr4 = DT_Progress.NewRow();
            dr4["stage"] = 4;
            dr4["stageName"] = ("RCC Finished On").ToUpper();
            //dr4["date"] = ;
            DT_Progress.Rows.Add(dr4);

            ////-----stage 5----------------
            //dr5 = DT_Progress.NewRow();
            //dr5["stage"] = 5;
            //dr5["stageName"] = "Job Started on";
            ////dr4["date"] = ;
            //DT_Progress.Rows.Add(dr5);

            //gtvProgress.DataSource = null;
            //gtvProgress.AutoGenerateColumns = false;
            //gtvProgress.DataSource = DT_Progress;
        }
        public RccTracker()
        {
            InitializeComponent();
            InitializeValuesNDefaultValueSet();
            txtLoc.Text = BaseCls.GlbUserDefLoca;
            createProgressTable();

            //DT_Progress.Columns.Add("stage", typeof(string));
            //DT_Progress.Columns.Add("stageName", typeof(string));
            //DT_Progress.Columns.Add("date", typeof(string));
            //DT_Progress.Columns.Add("dateDiff", typeof(string));

            //DataRow dr1,dr2,dr3,dr4,dr5;
            ////-----stage 1----------------
            //dr1 = DT_Progress.NewRow();
            //dr1["stage"] = 1;
            //dr1["stageName"] = ("RCC Raised On").ToUpper();
            ////dr1["date"] = ;
            //DT_Progress.Rows.Add(dr1);

            ////-----stage 2----------------
            //dr2 = DT_Progress.NewRow();
            //dr2["stage"] = 2;
            //dr2["stageName"] = ("Job Started On").ToUpper();
            ////dr2["date"] = ;
            //DT_Progress.Rows.Add(dr2);

            ////-----stage 3----------------
            //dr3 = DT_Progress.NewRow();
            //dr3["stage"] = 3;
            //dr3["stageName"] = ("Job Compleated On").ToUpper();
            ////dr3["date"] = ;
            //DT_Progress.Rows.Add(dr3);

            ////-----stage 4----------------
            //dr4 = DT_Progress.NewRow();
            //dr4["stage"] = 4;
            //dr4["stageName"] = ("Finished On").ToUpper();
            ////dr4["date"] = ;
            //DT_Progress.Rows.Add(dr4);

            //////-----stage 5----------------
            ////dr5 = DT_Progress.NewRow();
            ////dr5["stage"] = 5;
            ////dr5["stageName"] = "Job Started on";
            //////dr4["date"] = ;
            ////DT_Progress.Rows.Add(dr5);

            ////gtvProgress.DataSource = null;
            ////gtvProgress.AutoGenerateColumns = false;
            ////gtvProgress.DataSource = DT_Progress;

        }
        private void InitializeValuesNDefaultValueSet()
        {
            try
            {
                // txtDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MM/yyyy");
                txtCloseDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MM/yyyy");

                //cmbRccType.Items.Clear();
                //cmbRccType.DataSource = null;
                //cmbRccType.DataSource = CHNLSVC.General.GetAllType("RCC");
                //cmbRccType.DisplayMember = "Mtp_desc";
                //cmbRccType.ValueMember = "Mtp_cd";
                //cmbRccType.SelectedIndex = -1;

                cmbColMethod.Items.Clear();
                cmbColMethod.DataSource = null;
                cmbColMethod.DataSource = CHNLSVC.Inventory.GetRCCDef("COLMETHOD");
                cmbColMethod.DisplayMember = "ird_desc";
                cmbColMethod.ValueMember = "ird_cd";
                cmbColMethod.SelectedIndex = -1;

                cmbAcc.Items.Clear();
                cmbAcc.DataSource = null;
                cmbAcc.DataSource = CHNLSVC.Inventory.GetRCCDef("ACC");
                cmbAcc.DisplayMember = "ird_desc";
                cmbAcc.ValueMember = "ird_cd";
                cmbAcc.SelectedIndex = -1;

                cmbCond.Items.Clear();
                cmbCond.DataSource = null;
                cmbCond.DataSource = CHNLSVC.Inventory.GetRCCDef("COND");
                cmbCond.DisplayMember = "ird_desc";
                cmbCond.ValueMember = "ird_cd";
                cmbCond.SelectedIndex = -1;

                cmbDefect.Items.Clear();
                cmbDefect.DataSource = null;
                cmbDefect.DataSource = CHNLSVC.Inventory.GetRCCDef("DEF");
                cmbDefect.DisplayMember = "ird_desc";
                cmbDefect.ValueMember = "ird_cd";
                cmbDefect.SelectedIndex = -1;

                cmbRetCond.Items.Clear();
                cmbRetCond.DataSource = null;
                cmbRetCond.DataSource = CHNLSVC.Inventory.GetRCCDef("RETCON");
                cmbRetCond.DisplayMember = "ird_desc";
                cmbRetCond.ValueMember = "ird_cd";
                cmbRetCond.SelectedIndex = -1;

                cmbReason.Items.Clear();
                cmbReason.DataSource = null;
                cmbReason.DataSource = CHNLSVC.Inventory.GetRCCDef("REPDET");
                cmbReason.DisplayMember = "ird_desc";
                cmbReason.ValueMember = "ird_cd";
                cmbReason.SelectedIndex = -1;

                cmbClosureType.Items.Clear();
                cmbClosureType.DataSource = null;
                cmbClosureType.DataSource = CHNLSVC.Inventory.GetRCCDef("CLOSE");
                cmbClosureType.DisplayMember = "ird_desc";
                cmbClosureType.ValueMember = "ird_cd";
                cmbClosureType.SelectedIndex = -1;
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
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                RccTracker formnew = new RccTracker();
                formnew.MdiParent = this.MdiParent;
                formnew.Location = this.Location;
                formnew.Show();
                this.Close();
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
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.RCC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtLoc.Text.Trim().ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                //case CommonUIDefiniton.SearchUserControlType.ServiceAgent:
                //    {
                //        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                //        break;
                //    }
                //case CommonUIDefiniton.SearchUserControlType.Item:
                //    {
                //        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                //        break;
                //    }

                default:
                    break;
            }
            return paramsText.ToString();
        }
        private void btnView_Click(object sender, EventArgs e)
        {

        }

        private void btnGetRCC_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLoc.Text.Trim() == "")
                {
                    MessageBox.Show("Please select location first", "Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //TODO: GET RCC LIST
                DataTable dt = CHNLSVC.Inventory.Search_RCC(BaseCls.GlbUserComCode, txtLoc.Text.Trim(), txtRCC_no.Text.Trim().ToUpper(), TextBoxFromDate.Value.Date, TextBoxToDate.Value.Date, string.Empty, string.Empty, string.Empty, string.Empty);
                grvRCCAll.DataSource = null;
                grvRCCAll.AutoGenerateColumns = false;
                grvRCCAll.DataSource = dt;
                colorGrid();

                if (grvRCCAll.Rows.Count == 0)
                {
                    MessageBox.Show("No Records found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        protected void GetItemData()
        {
            try
            {
                MasterItem _Item = null;
                if (txtItem.Text == "")
                {
                    txtItemDesn.Text = "";
                    txtModel.Text = "";
                    return;
                }
                _Item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                if (_Item != null)
                {
                    txtItemDesn.Text = _Item.Mi_shortdesc;
                    txtModel.Text = _Item.Mi_model;
                }
                else
                {
                    txtItemDesn.Text = "";
                    txtModel.Text = "";
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
        protected void LoadRCCDetails(string rccNo)
        {
            try
            {
                FF.BusinessObjects.RCC _RCC = null;
                _RCC = CHNLSVC.Inventory.GetRccByNo(rccNo);

                //------------------------------------------------
                txtAcknowDt.Text = (_RCC.Inr_acknoledge_dt.ToString() == null) ? string.Empty : _RCC.Inr_acknoledge_dt.ToString("dd/MMM/yyyy");
                txtDespDt1.Text = (_RCC.Inr_disprem1_dt.ToString() == null) ? string.Empty : _RCC.Inr_disprem1_dt.ToString("dd/MMM/yyyy"); ;
                txtDespDt2.Text = (_RCC.Inr_disprem2_dt.ToString() == null) ? string.Empty : _RCC.Inr_disprem2_dt.ToString("dd/MMM/yyyy");
                txtDespDt3.Text = (_RCC.Inr_disprem3_dt.ToString() == null) ? string.Empty : _RCC.Inr_disprem3_dt.ToString("dd/MMM/yyyy");
                txtDespRmk1.Text = (_RCC.Inr_disp_rem1 == null) ? string.Empty : _RCC.Inr_disp_rem1;
                txtDespRmk2.Text = (_RCC.Inr_disp_rem2 == null) ? string.Empty : _RCC.Inr_disp_rem2;
                txtDespRmk3.Text = (_RCC.Inr_disp_rem3 == null) ? string.Empty : _RCC.Inr_disp_rem3;
                //-------------------------------------------------

                //cmbRccType.SelectedValue = _RCC.Inr_tp;
                //cmbRccSubType.SelectedValue = _RCC.Inr_sub_tp;
                //txtRCC.Text = _RCC.Inr_no;
                //txtDate.Text = _RCC.Inr_dt.ToShortDateString();
                //txtManual.Text = _RCC.Inr_manual_ref;

                txtAccNo.Text = (_RCC.Inr_acc_no == null) ? string.Empty : _RCC.Inr_acc_no;
                txtCustAddr.Text = (_RCC.Inr_addr == null) ? string.Empty : _RCC.Inr_addr;
                txtCustName.Text = (_RCC.Inr_cust_name == null) ? string.Empty : _RCC.Inr_cust_name;
                txtInvDate.Text = (_RCC.Inr_inv_dt.ToString() == null) ? string.Empty : _RCC.Inr_inv_dt.ToString("dd/MMM/yyyy");
                txtInvoice.Text = (_RCC.Inr_inv_no == null) ? string.Empty : _RCC.Inr_inv_no;
                txtTel.Text = (_RCC.Inr_tel == null) ? string.Empty : _RCC.Inr_tel;

                txtEasyLoc.Text = (_RCC.Inr_easy_loc == null) ? string.Empty : _RCC.Inr_easy_loc;
                txtInsp.Text = (_RCC.Inr_insp_by == null) ? string.Empty : _RCC.Inr_insp_by;
                txtItem.Text = (_RCC.Inr_itm == null) ? string.Empty : _RCC.Inr_itm;
                GetItemData();
                txtRem.Text = (_RCC.Inr_rem1 == null) ? string.Empty : _RCC.Inr_rem1;
                txtRepairRem.Text = (_RCC.Inr_rem2 == null) ? string.Empty : _RCC.Inr_rem2;
                txtCompleteRem.Text = (_RCC.Inr_rem3 == null) ? string.Empty : _RCC.Inr_rem3;
                txtSerial.Text = (_RCC.Inr_ser == null) ? string.Empty : _RCC.Inr_ser;
                txtWarranty.Text = (_RCC.Inr_warr == null) ? string.Empty : _RCC.Inr_warr;

                try
                {
                    txtCloseDate.Text = (_RCC.Inr_complete_dt).ToString();
                    txtWarPeriod.Text = (_RCC.INR_WAR_PERIOD.ToString() == null) ? string.Empty : _RCC.INR_WAR_PERIOD.ToString();
                    txtRetDate.Value = _RCC.Inr_return_dt;

                }
                catch (Exception ex)
                {
                    txtRetDate.Value = DateTime.Now.Date;//_RCC.Inr_return_dt;
                }


                try
                {
                    cmbAcc.SelectedValue = Convert.ToInt32(_RCC.Inr_accessories);
                }
                catch (Exception ex)
                {
                    cmbAcc.SelectedIndex = -1;
                }
                cmbAcc.Enabled = false;//***********
                try
                {
                    cmbCond.SelectedValue = Convert.ToInt32(_RCC.Inr_condition);
                }
                catch (Exception ex)
                {
                    cmbCond.SelectedIndex = -1;
                }

                cmbCond.Enabled = false; //**************
                try
                {
                    cmbDefect.SelectedValue = Convert.ToInt32(_RCC.Inr_def_cd);
                }
                catch (Exception ex)
                {
                    cmbDefect.SelectedIndex = -1;
                }

                cmbDefect.Enabled = false; //***********

                txtAgent.Text = _RCC.Inr_agent;

                try
                {
                    cmbColMethod.SelectedValue = Convert.ToInt32(_RCC.Inr_col_method);
                }
                catch (Exception ex)
                {
                    cmbColMethod.SelectedIndex = -1;
                }

                txtJob1.Text = (_RCC.Inr_jb_no == null) ? string.Empty : _RCC.Inr_jb_no;
                //kapila 12/8/2015 load job tasks
                load_job_tasks();

                //  txtJob2.Text = (_RCC.Inr_anal7 == null) ? string.Empty : _RCC.Inr_anal7;
                txtOrdNo.Text = (_RCC.Inr_anal1 == null) ? string.Empty : _RCC.Inr_anal1;
                txtDispatchNo.Text = (_RCC.Inr_anal2 == null) ? string.Empty : _RCC.Inr_anal2;
                txtHologram.Text = (_RCC.Inr_hollogram_no == null) ? string.Empty : _RCC.Inr_hollogram_no;

                if (!string.IsNullOrEmpty(txtJob1.Text))
                {
                    DataTable _dtConf = CHNLSVC.CustService.GetJobConfByJob(BaseCls.GlbUserComCode, txtJob1.Text, null);
                    if (_dtConf.Rows.Count > 0)
                        txtRemarks.Text = _dtConf.Rows[0]["jch_rmk"].ToString();
                }

                Int32 RccStage = _RCC.Inr_stage;
                //RccNo = _RCC.Inr_no;
                //btnRej.Enabled = false;
                //btnApp.Enabled = false;

                if (_RCC.Inr_repair_stus != "")
                {
                    cmbReason.SelectedValue = Convert.ToInt32(_RCC.Inr_repair_stus);
                }
                else //add 17-07-2013
                {
                    cmbReason.SelectedIndex = -1;
                }

                if (_RCC.Inr_ret_condition != "")
                {
                    cmbRetCond.SelectedValue = Convert.ToInt32(_RCC.Inr_ret_condition);
                }
                else//add 17-07-2013
                {
                    cmbRetCond.SelectedIndex = -1;
                }

                chkRepaired.Checked = _RCC.Inr_is_repaired;
                if (_RCC.Inr_closure_tp != "")
                {
                    cmbClosureType.SelectedValue = Convert.ToInt32(_RCC.Inr_closure_tp);
                }
                else//add 17-07-2013
                {
                    cmbClosureType.SelectedIndex = -1;
                }

                cmbReason.Enabled = false; //*******
                cmbRetCond.Enabled = false; //*******
                cmbClosureType.Enabled = false; //*******
                //----------------------------------------------------------------
                if (RccStage == 1)
                {
                    txtStatus.Text = "Raised";
                }
                if (RccStage == 2)
                {
                    txtStatus.Text = "Opened";
                }
                if (RccStage == 3)
                {
                    txtStatus.Text = "Repaired and returned to SR";
                    //if (_RCC.Inr_tp == "STK" || _RCC.Inr_tp == "FIXED")
                    //{
                    //    if (_RCC.Inr_in_stus == true && _RCC.Inr_out_stus == true)
                    //    {
                    //        btnConfirm.Enabled = true;
                    //        btnUpd.Enabled = false;
                    //    }
                    //    else
                    //    {
                    //        btnConfirm.Enabled = false;
                    //        btnUpd.Enabled = true;
                    //    }
                    //}
                }
                if (RccStage == 4)
                {
                    txtStatus.Text = "Completed";
                }
                if (RccStage == 5)
                {
                    if (_RCC.Inr_stus == "P")
                    {
                        txtStatus.Text = "Pending Request";
                        //btnRej.Enabled = true;
                        //btnApp.Enabled = true;
                        //btnUpd.Enabled = false;
                    }
                    if (_RCC.Inr_stus == "A")
                    {
                        txtStatus.Text = "Approved Request";
                        //btnUpd.Enabled = true;
                    }
                    if (_RCC.Inr_stus == "R")
                    {
                        txtStatus.Text = "Rejected Request";
                    }
                }

                //kapila 9/4/2015
                string _serLocation = "";
                Boolean _isOnlineSCM2 = false;
                DataTable _dtserLoc = CHNLSVC.Sales.GetServiceAgentbyLoc(BaseCls.GlbUserComCode, txtAgent.Text);
                if (_dtserLoc != null)
                {
                    _serLocation = _dtserLoc.Rows[0]["mbe_acc_cd"].ToString();
                    //DataTable _dtLoc = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, _serLocation);
                    //if (_dtLoc.Rows.Count != 0)
                    //    _serChannel = _dtLoc.Rows[0]["ml_cate_3"].ToString();

                    MasterLocation _mstLoc = CHNLSVC.General.GetLocationInfor(BaseCls.GlbUserComCode, _serLocation);
                    if (_mstLoc != null)
                        if (_mstLoc.Ml_anal1 == "SCM2")
                            _isOnlineSCM2 = true;
                        else
                            _isOnlineSCM2 = false;
                }

                //9/2/2015 load job stage 
                Boolean _isExternal = CHNLSVC.Inventory.IsExternalServiceAgent(BaseCls.GlbUserComCode, txtAgent.Text);
                if (_isExternal == false)
                {
                    DataTable _dt = CHNLSVC.CustService.getJobStageByJobNo(txtJob1.Text, BaseCls.GlbUserComCode, Convert.ToInt32(_isOnlineSCM2));
                    if (_dt.Rows.Count > 0)
                        lblJobStage.Text = _dt.Rows[0]["jbs_desc"].ToString();
                    else
                        lblJobStage.Text = "Job Pending";
                }
                else
                    lblJobStage.Text = txtStatus.Text;

                if (_RCC.Inr_stus == "C")   //kapila 20/1/2016
                {
                    lblJobStage.Text = "Cancelled";
                    txtStatus.Text = "Cancelled";
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

        private void load_job_tasks()
        {
            List<JobTasks> _lstJobTasks = new List<JobTasks>();

            //Request
            //MRN
            DataTable dtReq = CHNLSVC.CustService.GetReqByJobline(BaseCls.GlbUserComCode, txtJob1.Text, 0);
            if (dtReq != null && dtReq.Rows.Count > 0)
            {
                JobTasks _jobTaks = new JobTasks();
                _jobTaks.Task_Desc = "REQUEST";
                _jobTaks.Task_Date = Convert.ToDateTime(dtReq.Rows[0]["task_dt"]);
                _jobTaks.Task_Ref = dtReq.Rows[0]["task_ref"].ToString();
                _jobTaks.Task_UpdBy = dtReq.Rows[0]["task_by"].ToString();
                _jobTaks.Task_UpdTime = Convert.ToDateTime(dtReq.Rows[0]["task_when"]);
                _lstJobTasks.Add(_jobTaks);
            }

            //Job Stage Log
            DataTable dtLog = CHNLSVC.CustService.GetJobTaskByJobline(BaseCls.GlbUserComCode, txtJob1.Text, 0);
            if (dtLog != null && dtLog.Rows.Count > 0)
            {
                foreach (DataRow dr in dtLog.Rows)
                {
                    JobTasks _jobTaks = new JobTasks();
                    _jobTaks.Task_Desc = dr["task_desc"].ToString().Trim();
                    _jobTaks.Task_Date = Convert.ToDateTime(dr["task_dt"]);
                    _jobTaks.Task_Ref = dr["task_ref"].ToString().Trim();
                    _jobTaks.Task_UpdBy = dr["task_by"].ToString().Trim();
                    _jobTaks.Task_UpdTime = Convert.ToDateTime(dr["task_when"]);
                    _lstJobTasks.Add(_jobTaks);
                }
            }

            //MRN
            //DataTable dtTemp = CHNLSVC.CustService.GetJobMRNByJobline(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum);
            //if (dtTemp != null && dtTemp.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dtTemp.Rows)
            //    {
            //        JobTasks _jobTaks = new JobTasks();
            //        _jobTaks.Task_Desc = "MRN";
            //        _jobTaks.Task_Date = Convert.ToDateTime(dr["task_dt"]);
            //        _jobTaks.Task_Ref = dr["task_ref"].ToString();
            //        _jobTaks.Task_UpdBy = dr["task_by"].ToString();
            //        _jobTaks.Task_UpdTime = Convert.ToDateTime(dr["task_when"]);
            //        _lstJobTasks.Add(_jobTaks);
            //    }
            //}

            //Part remove
            DataTable dtPart = CHNLSVC.CustService.GetPartRemoveByJobline(BaseCls.GlbUserComCode, txtJob1.Text, 0);
            if (dtPart != null && dtPart.Rows.Count > 0)
            {
                JobTasks _jobTaks = new JobTasks();
                _jobTaks.Task_Desc = "PART REMOVE";
                _jobTaks.Task_Date = Convert.ToDateTime(dtPart.Rows[0]["task_dt"]);
                _jobTaks.Task_Ref = dtPart.Rows[0]["task_ref"].ToString();
                _jobTaks.Task_UpdBy = dtPart.Rows[0]["task_by"].ToString();
                _jobTaks.Task_UpdTime = Convert.ToDateTime(dtPart.Rows[0]["task_when"]);
                _lstJobTasks.Add(_jobTaks);
            }

            //part transfered
            List<Service_Enquiry_PartTrasferd> oItem = CHNLSVC.CustService.GET_SCV_PART_TRSFER_ENQRY(txtJob1.Text, 0, BaseCls.GlbUserComCode);
            for (int i = 0; i < oItem.Count; i++)
            {

            }

            //temporary issue details

            List<Service_TempIssue> stockReturnItems = new List<Service_TempIssue>();
            stockReturnItems = CHNLSVC.CustService.Get_ServiceWIP_TempIssued_Items(BaseCls.GlbUserComCode, txtJob1.Text, 0, "", BaseCls.GlbUserDefProf, "TMPI");
            for (int i = 0; i < stockReturnItems.Count; i++)
            {
                JobTasks _jobTaks = new JobTasks();
                _jobTaks.Task_Desc = "TEMP ISSUE";
                _jobTaks.Task_Date = Convert.ToDateTime(stockReturnItems[i].STI_DT);
                _jobTaks.Task_Ref = stockReturnItems[i].STI_DOC.ToString();
                _jobTaks.Task_UpdBy = stockReturnItems[i].STI_CRE_BY.ToString();
                _jobTaks.Task_UpdTime = Convert.ToDateTime(stockReturnItems[i].STI_CRE_DT);
                _lstJobTasks.Add(_jobTaks);
            }

            //receipts
            DataTable dtRec = null;
            if (txtJob1.Text != "N/A")
            {
                dtRec = CHNLSVC.CustService.GetRecByJobline(BaseCls.GlbUserComCode, txtJob1.Text, 0);
                if (dtRec != null && dtRec.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRec.Rows)
                    {
                        JobTasks _jobTaks = new JobTasks();
                        _jobTaks.Task_Desc = "RECEIPT";
                        _jobTaks.Task_Date = Convert.ToDateTime(dr["task_dt"]);
                        _jobTaks.Task_Ref = dr["task_ref"].ToString().Trim();
                        _jobTaks.Task_UpdBy = dr["task_by"].ToString().Trim();
                        _jobTaks.Task_UpdTime = Convert.ToDateTime(dr["task_when"]);
                        _lstJobTasks.Add(_jobTaks);
                    }
                }
            }

            //MRN / Quotation / estimate / sup war clain req / job transfer / war claim req send / receive / hold
            DataTable dtQ = null;
            if (txtJob1.Text != "N/A")
            {
                dtQ = CHNLSVC.CustService.GetJobTaskByJob(BaseCls.GlbUserComCode, txtJob1.Text, 0);
                if (dtQ != null && dtQ.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtQ.Rows)
                    {
                        JobTasks _jobTaks = new JobTasks();
                        _jobTaks.Task_Desc = dr["task_desc"].ToString().Trim();
                        _jobTaks.Task_Date = Convert.ToDateTime(dr["task_dt"]);
                        _jobTaks.Task_Ref = dr["task_ref"].ToString().Trim();
                        _jobTaks.Task_UpdBy = dr["task_by"].ToString().Trim();
                        _jobTaks.Task_UpdTime = Convert.ToDateTime(dr["task_when"]);
                        _lstJobTasks.Add(_jobTaks);
                    }
                }
            }

            _lstJobTasks = _lstJobTasks.OrderBy(x => x.Task_UpdTime).ToList();

            BindingSource _source = new BindingSource();
            grvJobStus.AutoGenerateColumns = false;
            _source.DataSource = _lstJobTasks;
            grvJobStus.DataSource = _source;

        }

        private void grvRCCAll_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Int32 rowIndex = e.RowIndex;
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {
                    DataGridViewRow row = grvRCCAll.Rows[rowIndex];
                    selected_rcc_stage = row.Cells["inr_stage"].Value.ToString();
                    string rccNo = row.Cells["inr_no"].Value.ToString();
                    LoadRCCDetails(rccNo);
                    LoadProgressGrid(rccNo);

                    // ddlPendingVehIns_req.SelectedItem = Req_no;

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
        private void LoadProgressGrid(string rccNo)
        {
            try
            {
                createProgressTable();

                gtvProgress.DataSource = null;
                gtvProgress.AutoGenerateColumns = false;
                gtvProgress.DataSource = DT_Progress;

                DataTable dt = CHNLSVC.Inventory.Get_INT_RCC_STAGES_INFO(rccNo);
                if (dt != null)
                {
                    string stage1Dt, stage2Dt, stage3Dt, stage4Dt;
                    try
                    {
                        DateTime date_stage1 = Convert.ToDateTime(dt.Rows[0]["inr_dt"].ToString());
                        stage1Dt = date_stage1.ToShortDateString();
                    }
                    catch (Exception ex)
                    {
                        stage1Dt = "";
                    }
                    //----------
                    try
                    {
                        DateTime date_stage2 = Convert.ToDateTime(dt.Rows[0]["inr_job_dt"].ToString());
                        stage2Dt = date_stage2.ToShortDateString();
                    }
                    catch (Exception ex)
                    {
                        stage2Dt = "";
                    }
                    //-------------
                    try
                    {
                        DateTime date_stage3 = Convert.ToDateTime(dt.Rows[0]["INR_REPAIR_DT"].ToString());
                        stage3Dt = date_stage3.ToShortDateString();
                    }
                    catch (Exception ex)
                    {
                        stage3Dt = "";
                    }
                    //-------------
                    try
                    {
                        DateTime date_stage4 = Convert.ToDateTime(dt.Rows[0]["inr_complete_dt"].ToString());
                        stage4Dt = date_stage4.ToShortDateString();
                    }
                    catch (Exception ex)
                    {
                        stage4Dt = "";
                    }

                    //int diff_1 = Convert.ToInt32(dt.Rows[0]["STAGE_1_DEFF"].ToString());
                    //int diff_2 = Convert.ToInt32(dt.Rows[0]["STAGE_2_DEFF"].ToString());
                    //int diff_3 = Convert.ToInt32(dt.Rows[0]["STAGE_3_DEFF"].ToString());
                    string diff_0 = Convert.ToString(0);
                    string diff_1 = Convert.ToString(dt.Rows[0]["STAGE_1_DEFF"].ToString());
                    string diff_2 = Convert.ToString(dt.Rows[0]["STAGE_2_DEFF"].ToString());
                    string diff_3 = Convert.ToString(dt.Rows[0]["STAGE_3_DEFF"].ToString());

                    foreach (DataGridViewRow dgvr in gtvProgress.Rows)
                    {
                        string row_stage = dgvr.Cells["stage"].Value.ToString();
                        if (row_stage == "1")
                        {
                            dgvr.Cells["date"].Value = stage1Dt;
                            //dgvr.Cells["dateDiff"].Value = diff_1;
                            //dgvr.Cells["dateDiff"].Value = diff_0;

                            if (selected_rcc_stage == "5")
                            {
                                dgvr.Cells["date"].Value = "";
                                dgvr.Cells["dateDiff"].Value = "";
                            }
                            //---------------
                            dgvr.DefaultCellStyle.BackColor = Color.LemonChiffon;
                        }
                        else if (row_stage == "2")
                        {
                            dgvr.Cells["date"].Value = stage2Dt;
                            dgvr.Cells["dateDiff"].Value = diff_1;
                            //---------------
                            dgvr.DefaultCellStyle.BackColor = Color.LightCyan;
                        }
                        else if (row_stage == "3")
                        {
                            dgvr.Cells["date"].Value = stage3Dt;
                            dgvr.Cells["dateDiff"].Value = diff_2;
                            //---------------
                            dgvr.DefaultCellStyle.BackColor = Color.PaleTurquoise;
                        }
                        else if (row_stage == "4" && selected_rcc_stage == "4")
                        {
                            dgvr.Cells["date"].Value = stage4Dt;
                            //---------------
                            dgvr.DefaultCellStyle.BackColor = Color.LightSkyBlue;

                        }
                        //------------------------------------------------------------
                        if (row_stage == "4")
                        {
                            dgvr.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                            dgvr.Cells["dateDiff"].Value = diff_3;

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
        private void BtnSearcgRccNo_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _result = null;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RCC);

                _result = CHNLSVC.CommonSearch.GetRCC(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRCC_no;
                _CommonSearch.ShowDialog();
                txtRCC_no.Focus();
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

        private void btnSearchByDT_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLoc.Text.Trim() == "")
                {
                    MessageBox.Show("Please select location first", "Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //SEARCH_rccByDate
                //DataTable DT = CHNLSVC.Inventory.SEARCH_rccByDate(BaseCls.GlbUserComCode, txtLoc.Text.Trim().ToUpper(), string.Empty, string.Empty, null);
                btnSearchByDT.Select();
                //-------------------------------------------------------------------------------
                string stage = "";
                if (ddlRccStage.SelectedIndex != -1)
                {
                    stage = ddlRccStage.SelectedItem.ToString();
                    stage = stage[0].ToString();
                }
                if (chkAnyStage.Checked == true)
                {
                    stage = "";
                }
                //--------------------------------------------------------------------------------
                string isSR_AccPending = "0";
                if (chkSRacceptPending.Checked == true && stage == "3")
                {
                    isSR_AccPending = "1";
                }
                //--------------------------------------------------------------------------------
                DataTable DT = null;
                if (chkAllRCCinLoc.Checked == true)
                {
                    DT = CHNLSVC.Inventory.SEARCH_rccByDate(BaseCls.GlbUserComCode, txtLoc.Text.Trim(), stage, string.Empty, string.Empty, isSR_AccPending);
                }
                else
                {

                    DT = CHNLSVC.Inventory.SEARCH_rccByDate(BaseCls.GlbUserComCode, txtLoc.Text.Trim(), stage, TextBoxFromDate.Value.Date.ToShortDateString(), (TextBoxToDate.Value.Date).ToShortDateString(), isSR_AccPending);
                }

                grvRCCAll.DataSource = null;
                grvRCCAll.AutoGenerateColumns = false;
                grvRCCAll.DataSource = DT;
                colorGrid();

                if (grvRCCAll.Rows.Count == 0)
                {
                    MessageBox.Show("No Records found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void colorGrid()
        {
            if (grvRCCAll.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvr in grvRCCAll.Rows)
                {
                    if (dgvr.Cells["inr_stage"].Value.ToString() == "4")
                    {
                        dgvr.DefaultCellStyle.BackColor = Color.LightSkyBlue; //Color.MediumAquamarine;
                    }
                    if (dgvr.Cells["inr_stage"].Value.ToString() == "3")
                    {
                        dgvr.DefaultCellStyle.BackColor = Color.PaleTurquoise;
                    }
                    if (dgvr.Cells["inr_stage"].Value.ToString() == "2")
                    {
                        dgvr.DefaultCellStyle.BackColor = Color.LightCyan;
                    }
                    if (dgvr.Cells["inr_stage"].Value.ToString() == "1")
                    {
                        dgvr.DefaultCellStyle.BackColor = Color.LemonChiffon;
                    }
                    //if (dgvr.Cells["inr_stage"].Value.ToString() == "5")
                    //{
                    //    dgvr.DefaultCellStyle.BackColor = Color.MediumAquamarine;
                    //}
                }
            }
        }
        private void chkAnyStage_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAnyStage.Checked == true)
            {
                ddlRccStage.SelectedIndex = -1;
            }
        }

        private void ddlRccStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRccStage.SelectedIndex != -1)
            {
                chkAnyStage.Checked = false;
            }
            chkSRacceptPending.Checked = false;
        }

        private void TextBoxFromDate_ValueChanged(object sender, EventArgs e)
        {
            chkAllRCCinLoc.Checked = false;
        }

        private void TextBoxToDate_ValueChanged(object sender, EventArgs e)
        {
            chkAllRCCinLoc.Checked = false;
        }

        private void imgBtnLoc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLoc;
                _CommonSearch.ShowDialog();
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

        private void grvRCCAll_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void txtRCC_no_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                BtnSearcgRccNo_Click(null, null);
            }
        }

        private void txtLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                imgBtnLoc_Click(null, null);
            }
        }

        private void txtRCC_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnGetRCC_Click(null, null);
            }
        }

        private void imgBtnLoc_Click_1(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLoc;
                // _CommonSearch.txtSearchbyword.Text = txtLoc.Text;
                _CommonSearch.ShowDialog();
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

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void txtRCC_no_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.BtnSearcgRccNo_Click(null, null);
        }

        private void lblJob_Click(object sender, EventArgs e)
        {
            lblJob.BackColor = Color.LightSalmon;
            lblRCC.BackColor = Color.White;
            pnlJobStus.Visible = true;
            pnlRCCStus.Visible = false;
        }

        private void lblRCC_Click(object sender, EventArgs e)
        {
            lblRCC.BackColor = Color.LightSalmon;
            lblJob.BackColor = Color.White;
            pnlRCCStus.Visible = true;
            pnlJobStus.Visible = false;
        }

        //private void pnlJob_Paint(object sender, PaintEventArgs e)
        //{

        //}
    }
}
