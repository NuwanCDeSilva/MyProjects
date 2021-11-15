using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FF.BusinessObjects;
using System.Collections.Generic;
using System.Linq;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceWIP_TechAllocation : Base
    {
        private string GblJobNum = string.Empty;
        private Int32 GbljobLineNum = 0;
        private List<Service_Tech_Aloc_Hdr> oMainListHdr = new List<Service_Tech_Aloc_Hdr>();
        private string Direction = string.Empty;

        public ServiceWIP_TechAllocation(string job, Int32 jobLine)
        {
            GblJobNum = job;
            GbljobLineNum = jobLine;
            InitializeComponent();
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
            }

            return paramsText.ToString();
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

        private void ServiceWIP_TechAllocation_Load(object sender, EventArgs e)
        {
            dgvAllocations.AutoGenerateColumns = false;
            dgvEMPSelect.AutoGenerateColumns = false;
            clearAll();
            pnlEmpSearch.Size = new Size(571, 316);
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

        private void txtALEmp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtALEmp_DoubleClick(null, null);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            txtALEmp_DoubleClick(null, null);
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
            }
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

        private void txtSkills_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtSkills_DoubleClick(null, null);
            }
        }

        private void btnSkillSrchTD_Click(object sender, EventArgs e)
        {
            txtSkills_DoubleClick(null, null);
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

        private void txtDesignation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtDesignation_DoubleClick(null, null);
            }
        }

        private void btnDesignationSrchTD_Click(object sender, EventArgs e)
        {
            txtDesignation_DoubleClick(null, null);
        }

        private void txtEmpCode_DoubleClick(object sender, EventArgs e)
        {
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
            if (e.KeyCode == Keys.F2)
            {
                txtEmpCode_DoubleClick(null, null);
            }
        }

        private void btnEmpSrchTD_Click(object sender, EventArgs e)
        {
            txtEmpCode_DoubleClick(null, null);
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

        private void clearAll()
        {
            dgvAllocations.DataSource = new List<Service_Tech_Aloc_Hdr>();
            pnlEmpSearch.Visible = false;
            txtEmpCode.Clear();
            txtSkills.Clear();
            txtDesignation.Clear();
            lblUtilizedCount.Text = "";
            ddlSlots.DataSource = null;
            lblSlot.Visible = false;
            ddlSlots.Visible = false;
            label4.Visible = false;
            lblUtilizedCount.Visible = false;
            txtALEmp.Clear();
            dtpFromAL.Value = DateTime.Now;
            dtpToAL.Value = DateTime.Now.AddHours(1);
            view();
        }

        private void btnSESearch_Click(object sender, EventArgs e)
        {
            getEMP();
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

        private void getEMP()
        {
            DataTable dt = CHNLSVC.CustService.GetEmployeBySkillDesignation(BaseCls.GlbUserComCode, txtSkills.Text, txtDesignation.Text, txtEmpCode.Text, BaseCls.GlbUserDefProf);
            dgvEMPSelect.DataSource = null;
            if (dt.Rows.Count > 0)
            {
                dgvEMPSelect.DataSource = dt;
            }
        }

        private void view()
        {
            List<Service_Tech_Aloc_Hdr> oDetails = CHNLSVC.CustService.GetJobAllocations(GblJobNum, GbljobLineNum, BaseCls.GlbUserComCode);
            oMainListHdr.Clear();
            oMainListHdr.AddRange(oDetails);
            dgvAllocations.DataSource = new List<Service_Tech_Aloc_Hdr>();
            dgvAllocations.DataSource = oDetails;
            if (oDetails.Count > 0)
            {
                ddlSlots.SelectedValue = oDetails[0].STH_TERMINAL.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Service_job_Det oItem = new Service_job_Det();
            List<Service_job_Det> oItms = CHNLSVC.CustService.GetJobDetails(GblJobNum, GbljobLineNum, BaseCls.GlbUserComCode);
            Service_JOB_HDR oHeader = CHNLSVC.CustService.GetServiceJobHeader(GblJobNum, BaseCls.GlbUserComCode);
            String TownDescription = string.Empty;
            DataTable dt = CHNLSVC.General.GetTownByCode(oHeader.SJB_B_TOWN);
            if (dt != null && dt.Rows.Count > 0)
            {
                TownDescription = dt.Rows[0]["mt_desc"].ToString();
            }

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

                if (dtpFromAL.Value > dtpToAL.Value)
                {
                    MessageBox.Show("Please select valid from date", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFromAL.Focus();
                    return;
                }



                if (oItms != null && oItms.Count > 0 && oHeader != null && oHeader.SJB_JOBNO != null)
                {
                    oItem = oItms[0];
                }
                else
                {
                    return;
                }

                if (dtpFromAL.Value < oHeader.SJB_DT)
                {
                    MessageBox.Show("From date can't be less than job date.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFromAL.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(txtALEmp.Text))
                {
                    if (oMainListHdr.Exists(x => x.STH_EMP_CD == txtALEmp.Text))
                    {
                        return;
                    }
                    Service_Tech_Aloc_Hdr oAloc_Hdr = new Service_Tech_Aloc_Hdr();

                    oAloc_Hdr.STH_SEQ = -1;
                    oAloc_Hdr.STH_ALOCNO = "";
                    oAloc_Hdr.STH_COM = BaseCls.GlbUserComCode;
                    oAloc_Hdr.STH_LOC = BaseCls.GlbUserDefLoca;
                    oAloc_Hdr.STH_TP = "J";
                    oAloc_Hdr.STH_JOBNO = GblJobNum;
                    oAloc_Hdr.STH_JOBLINE = GbljobLineNum;
                    oAloc_Hdr.STH_EMP_CD = txtALEmp.Text.Trim();
                    oAloc_Hdr.STH_STUS = "A";
                    oAloc_Hdr.STH_CRE_BY = BaseCls.GlbUserID;
                    oAloc_Hdr.STH_CRE_WHEN = DateTime.Now;
                    oAloc_Hdr.STH_MOD_BY = BaseCls.GlbUserID;
                    oAloc_Hdr.STH_MOD_WHEN = DateTime.Now;
                    oAloc_Hdr.STH_SESSION_ID = BaseCls.GlbUserSessionID;
                    oAloc_Hdr.STH_TOWN = "";
                    oAloc_Hdr.STH_TOWN = oHeader.SJB_B_TOWN;
                    oAloc_Hdr.MT_DESC = TownDescription;
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
                if (dtpFromAL.Value > dtpToAL.Value)
                {
                    MessageBox.Show("Please select valid from date", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFromAL.Focus();
                    return;
                }

                if (dtpFromAL.Value < oHeader.SJB_DT)
                {
                    MessageBox.Show("From date can't be less than job date.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFromAL.Focus();
                    return;
                }

                for (int j = 0; j < dgvEMPSelect.Rows.Count; j++)
                {
                    if (Convert.ToBoolean(dgvEMPSelect.Rows[j].Cells[0].Value) == true)
                    {
                        if (oMainListHdr.Exists(x => x.STH_EMP_CD == dgvEMPSelect.Rows[j].Cells[1].Value.ToString()))
                        {
                            continue;
                        }

                        Service_Tech_Aloc_Hdr oAloc_Hdr = new Service_Tech_Aloc_Hdr();

                        oAloc_Hdr.STH_SEQ = -1;
                        oAloc_Hdr.STH_ALOCNO = "";
                        oAloc_Hdr.STH_COM = BaseCls.GlbUserComCode;
                        oAloc_Hdr.STH_LOC = BaseCls.GlbUserDefLoca;
                        oAloc_Hdr.STH_TP = "R";
                        oAloc_Hdr.STH_JOBNO = GblJobNum;
                        oAloc_Hdr.STH_JOBLINE = GbljobLineNum;
                        oAloc_Hdr.STH_EMP_CD = dgvEMPSelect.Rows[j].Cells[1].Value.ToString();
                        oAloc_Hdr.STH_STUS = "A";
                        oAloc_Hdr.STH_CRE_BY = BaseCls.GlbUserID;
                        oAloc_Hdr.STH_CRE_WHEN = DateTime.Now;
                        oAloc_Hdr.STH_MOD_BY = BaseCls.GlbUserID;
                        oAloc_Hdr.STH_MOD_WHEN = DateTime.Now;
                        oAloc_Hdr.STH_SESSION_ID = BaseCls.GlbUserSessionID;
                        oAloc_Hdr.STH_TOWN = TownDescription;
                        oAloc_Hdr.MT_DESC = oHeader.SJB_B_TOWN;
                        oAloc_Hdr.STH_FROM_DT = dtpFromAL.Value;
                        oAloc_Hdr.STH_TO_DT = dtpToAL.Value;
                        oAloc_Hdr.STH_REQNO = "";
                        oAloc_Hdr.STH_REQLINE = 0;
                        oAloc_Hdr.STH_CURR_STUS = 1;
                        DataTable dtTemp = CHNLSVC.Sales.GetManagerDefProfit(BaseCls.GlbUserComCode, dgvEMPSelect.Rows[j].Cells["EPFES"].Value.ToString());
                        oAloc_Hdr.ESEP_FIRST_NAME = dtTemp.Rows[0]["ESEP_FIRST_NAME"].ToString();
                        if (ddlSlots.SelectedValue != null)
                        {
                            oAloc_Hdr.STH_TERMINAL = Convert.ToInt32(ddlSlots.SelectedValue.ToString());
                        }
                        oMainListHdr.Add(oAloc_Hdr);
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

        private bool validateAdd()
        {
            bool status = true;

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

                int effect = CHNLSVC.CustService.Save_TechnicianAllocatoinHEader(oMainListHdr, _ReqInsAuto, false);
                if (effect > 0)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("Technician allocated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearAll();
                    return;
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

        private void toolStripButton1_Click(object sender, EventArgs e)
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

                int effect = CHNLSVC.CustService.Save_TechnicianAllocatoinHEader(oMainListHdr, _ReqInsAuto, false);
                if (effect > 0)
                {
                    Service_Message_Template oTemplate = CHNLSVC.CustService.GetMessageTemplates_byID(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, 2);
                    if (oTemplate != null && oTemplate.Sml_templ_mail != null)
                    {

                        List<MST_BUSPRIT_LVL> oItems;
                        string custLevel = string.Empty;

                        Service_JOB_HDR oJOB_HDR = CHNLSVC.CustService.GetServiceJobHeader(oMainListHdr[0].STH_JOBNO, BaseCls.GlbUserComCode);

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

                    Cursor = Cursors.Default;
                    MessageBox.Show("Technician allocated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearAll();
                    return;
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

        private void btnCusFeedbck_Click(object sender, EventArgs e)
        {
            CustomerSatisfaction frm = new CustomerSatisfaction(GblJobNum, 0);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X, this.Location.Y + 60);
            frm.ShowDialog();
        }
    }
}