using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FF.BusinessObjects;

namespace FF.WindowsERPClient.HP
{
    public partial class AccountTrim : Base
    {
        decimal _minimumBalance = 0;
        DataTable _dtable = new DataTable();

        public AccountTrim()
        {
            InitializeComponent();
            txtComp.Text = BaseCls.GlbUserComCode;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            AccountTrim formnew = new AccountTrim();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        #region pc search
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            string com = txtComp.Text;
            string chanel = txtChanel.Text;
            string subChanel = txtSChanel.Text;
            string area = txtArea.Text;
            string region = txtRegion.Text;
            string zone = txtZone.Text;
            string pc = txtPC.Text;

            lstPC.Clear();
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPS"))
            {
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(com, chanel, subChanel, area, region, zone, pc);
                foreach (DataRow drow in dt.Rows)
                {
                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                }
            }
            else
            {
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                foreach (DataRow drow in dt.Rows)
                {
                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                }
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {

            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lstPC.Clear();
        }

        private void txtComp_Leave(object sender, EventArgs e)
        {

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
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GPC:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OPE:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void txtComp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtComp;
                _CommonSearch.ShowDialog();
                txtComp.Select();
            }
        }

        private void txtChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtChanel_MouseDoubleClick(null, null);
            }
        }

        private void txtChanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtChanel;
            _CommonSearch.ShowDialog();
            txtChanel.Select();

        }

        private void txtSChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtSChanel_MouseDoubleClick(null, null);
            }
        }

        private void txtSChanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSChanel;
            _CommonSearch.ShowDialog();
            txtSChanel.Select();
        }

        private void txtArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtArea_MouseDoubleClick(null, null);
            }
        }

        private void txtArea_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtArea;
            _CommonSearch.ShowDialog();
            txtArea.Select();
        }

        private void txtRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtRegion_MouseDoubleClick(null, null);
            }
        }

        private void txtRegion_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtRegion;
            _CommonSearch.ShowDialog();
            txtRegion.Select();
        }

        private void txtZone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtZone_MouseDoubleClick(null, null);
            }
        }

        private void txtZone_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtZone;
            _CommonSearch.ShowDialog();
            txtZone.Select();
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtPC_MouseDoubleClick(null, null);
            }
        }

        private void txtPC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPC;
            _CommonSearch.ShowDialog();
            txtPC.Select();
        } 
        #endregion

        private void btnProcess_Click(object sender, EventArgs e)
        {
            /*
             * 01 Get acive accounts from selected pcs
             * 02.check cls balance for each account
             * 03.check txansaction from date to current date
             */
            try
            {
                List<string> _pcList = new List<string>();
                foreach (ListViewItem itm in lstPC.Items)
                {
                    if (itm.Checked)
                    {
                        _pcList.Add(itm.Text);
                    }
                }
                if (_minimumBalance == 0)
                {
                    MessageBox.Show("Minimum balance can not be 0", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_pcList.Count <= 0)
                {
                    MessageBox.Show("Please select atleaset on profit center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string _error;
                DataTable _dt = CHNLSVC.Sales.GetTrimmingAccounts(BaseCls.GlbUserComCode, _pcList, dtDate.Value, _minimumBalance, out _error);
                if (_error != "")
                {
                    MessageBox.Show("Error occured while processing\n" + _error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    gvAccounts.DataSource = _dt;
                    _dtable = _dt;
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error occured while processing!!\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CHNLSVC.CloseAllChannels();
            }

        }

        private void AccountTrim_Load(object sender, EventArgs e)
        {
            List<Hpr_SysParameter> _list = CHNLSVC.Sales.GetAll_hpr_Para("MCBAL", "COM", BaseCls.GlbUserComCode);
            if (_list != null && _list.Count > 0) {
                lblMessage.Text = "Minimum balance is " + _list[0].Hsy_val;
                _minimumBalance = _list[0].Hsy_val;
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            //select all gv rows
            if (chkAll.Checked)
            {
                foreach (DataGridViewRow gvr in gvAccounts.Rows)
                {
                    DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells[0];
                    chkSelect.Value = "true";
                }
            }
            else {
                foreach (DataGridViewRow gvr in gvAccounts.Rows)
                {
                    DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells[0];
                    chkSelect.Value = "false";
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            /*
             * 01.save record to hpt_acc_log with sattus as T
             * 02 update hpt_acc with status T and cls date as current date
             * 
             */
            try
            {
               // btnProcess_Click(null, null);

                //List<HpAccount> _accoutList = GetAccountList();
                //if (_accoutList == null || _accoutList.Count <= 0)
                //{
                //    MessageBox.Show("No Accounts to process", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                List<HPAccountLog> _logList = new List<HPAccountLog>();

                //foreach (HpAccount _acc in _accoutList)
                //{
                //    HPAccountLog _log = new HPAccountLog();
                //    _log.Hal_acc_no = _acc.Hpa_acc_no;
                //    _log.Hal_af_val = _acc.Hpa_af_val;
                //    _log.Hal_bank = _acc.Hpa_bank;
                //    _log.Hal_buy_val = _acc.Hpa_buy_val;
                //    _log.Hal_cash_val = _acc.Hpa_cash_val;
                //    _log.Hal_cls_dt = dtDate.Value.Date;
                //    _log.Hal_com = _acc.Hpa_com;
                //    _log.Hal_cre_by = BaseCls.GlbUserID;
                //    _log.Hal_cre_dt = DateTime.Now;
                //    _log.Hal_dp_comm = _acc.Hpa_dp_comm;
                //    _log.Hal_dp_val = _acc.Hpa_dp_val;
                //    _log.Hal_ecd_stus = _acc.Hpa_ecd_stus;
                //    _log.Hal_ecd_tp = _acc.Hpa_ecd_tp;
                //    _log.Hal_flag = _acc.Hpa_flag;
                //    _log.Hal_grup_cd = _acc.Hpa_grup_cd;
                //    _log.Hal_hp_val = _acc.Hpa_hp_val;
                //    _log.Hal_init_ins = _acc.Hpa_init_ins;
                //    _log.Hal_init_ser_chg = _acc.Hpa_init_ser_chg;
                //    _log.Hal_init_stm = _acc.Hpa_init_stm;
                //    _log.Hal_init_vat = _acc.Hpa_init_vat;
                //    _log.Hal_inst_comm = _acc.Hpa_inst_comm;
                //    _log.Hal_inst_ins = _acc.Hpa_inst_ins;
                //    _log.Hal_inst_ser_chg = _acc.Hpa_inst_ser_chg;
                //    _log.Hal_inst_stm = _acc.Hpa_inst_stm;
                //    _log.Hal_inst_vat = _acc.Hpa_inst_vat;
                //    _log.Hal_intr_rt = _acc.Hpa_intr_rt;
                //    _log.Hal_invc_no = _acc.Hpa_invc_no;
                //    _log.Hal_is_rsch = _acc.Hpa_is_rsch;
                //    _log.Hal_mgr_cd = _acc.Hpa_mgr_cd;
                //    _log.Hal_log_dt = DateTime.Now.Date;
                //    _log.Hal_net_val = _acc.Hpa_net_val;
                //    _log.Hal_oth_chg = _acc.Hpa_oth_chg;
                //    _log.Hal_pc = _acc.Hpa_pc;
                //    _log.Hal_rls_dt = _acc.Hpa_rls_dt;
                //    _log.Hal_rsch_dt = _acc.Hpa_rsch_dt;
                //    _log.Hal_rv_dt = _acc.Hpa_rv_dt;
                //    _log.Hal_sch_cd = _acc.Hpa_sch_cd;
                //    _log.Hal_sch_tp = _acc.Hpa_sch_tp;
                //    _log.Hal_seq = _acc.Hpa_seq;
                //    _log.Hal_ser_chg = _acc.Hpa_ser_chg;
                //    _log.Hal_stus = "T";
                //    _log.Hal_tc_val = _acc.Hpa_tc_val;
                //    _log.Hal_term = _acc.Hpa_term;
                //    _log.Hal_tot_intr = _acc.Hpa_tot_intr;
                //    _log.Hal_tot_vat = _acc.Hpa_tot_vat;
                //    _log.Hal_val_01 = _acc.Hpa_val_01;
                //    _log.Hal_val_02 = _acc.Hpa_val_02;
                //    _log.Hal_val_03 = _acc.Hpa_val_03;
                //    _log.Hal_val_04 = _acc.Hpa_val_04;
                //    _log.Hal_val_05 = _acc.Hpa_val_05;
                //    _log.Hal_sa_sub_tp = "TRIM";
                //    _logList.Add(_log);


                //    _acc.Hpa_cls_dt = dtDate.Value.Date;
                //    _acc.Hpa_stus = "T";
                //}

                List<string> _pcList = new List<string>();
                foreach (ListViewItem itm in lstPC.Items)
                {
                    if (itm.Checked)
                    {
                        _pcList.Add(itm.Text);
                    }
                }
                if (_minimumBalance == 0)
                {
                    MessageBox.Show("Minimum balance can not be 0", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_pcList.Count <= 0)
                {
                    MessageBox.Show("Please select atleaset on profit center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string _error;
                int _result = CHNLSVC.Sales.SaveAccountTrim(out _error, BaseCls.GlbUserComCode, _pcList, dtDate.Value, _minimumBalance,dtDate.Value.Date,BaseCls.GlbUserID);
                if (!string.IsNullOrEmpty(_error))
                {
                    MessageBox.Show("Error occured while processing!!!\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show("Sucessfully saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                btnClear_Click(null, null);
            }

            catch (Exception ex) {
                MessageBox.Show("Error occured while processing!!\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CHNLSVC.CloseAllChannels();
            }

        }

        private List<HpAccount> GetAccountList()
        {
            try
            {
                List<HpAccount> _tempAcc = new List<HpAccount>();
                foreach (DataRow dr in _dtable.Rows) {
                    string _accNo = dr["acc_no"].ToString();
                    HpAccount _acc = CHNLSVC.Sales.GetHP_Account_onAccNo(_accNo);
                    if (_acc != null)
                    {
                        _tempAcc.Add(_acc);
                    }
                }
                return _tempAcc;
                /*
                List<HpAccount> _tempAcc = new List<HpAccount>();
                foreach (DataGridViewRow gvr in gvAccounts.Rows)
                {
                    //DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells[0];
                    //if (chkSelect.Value!=null && (bool)chkSelect.Value)
                    //{
                    //    string _accNo = gvr.Cells[1].Value.ToString();
                    //    HpAccount _acc = CHNLSVC.Sales.GetHP_Account_onAccNo(_accNo);
                    //    if (_acc != null)
                    //    {
                    //        _tempAcc.Add(_acc);
                    //    }

                    //}

                    if (gvr.Cells[0].Value != null)
                    {
                        if ((bool)(gvr.Cells[0].Value) == true)
                        {
                            string _accNo = gvr.Cells[1].Value.ToString();
                            HpAccount _acc = CHNLSVC.Sales.GetHP_Account_onAccNo(_accNo);
                            if (_acc != null)
                            {
                                _tempAcc.Add(_acc);
                            }
                        }
                    }

                }
                return _tempAcc;
                 */
            }
            catch (Exception ex) {
                MessageBox.Show("Error occured while processing!!\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }



    }
}
