using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;


namespace FF.WindowsERPClient.Finance
{
    public partial class ESDProcess : Base
    {
        List<ESDTxn> ESDTxnList;
        public ESDProcess()
        {
            InitializeComponent();
            bind_Combo_Types();
            ESDTxnList = new List<ESDTxn>();
            txtComp.Text = BaseCls.GlbUserComCode;
        }

        private void bind_Combo_Types()
        {
            Dictionary<string, string> Types = new Dictionary<string, string>();
            Types.Add("REC", "Receipt");
            Types.Add("EADJP", "ESD Adjustment (+)");
            Types.Add("EADJM", "ESD Adjustment (-)");
            Types.Add("IADJP", "Interest Adjustment (+)");
            Types.Add("IADJM", "Interest Adjustment (-)");
            //PartyTypes.Add("", "");
            //PartyTypes.Add("", "");
            //PartyTypes.Add("", "");

            ddlDocTypes.DataSource = new BindingSource(Types, null);
            ddlDocTypes.DisplayMember = "Value";
            ddlDocTypes.ValueMember = "Key";

            ddlDocTypes.SelectedIndex = -1;

            dtJoin.Value = Convert.ToDateTime("31/Dec/2999");
            dtHO.Value = Convert.ToDateTime("31/Dec/2999");
            dtSR.Value = Convert.ToDateTime("31/Dec/2999");
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.VoucherNo:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EmployeeEPF:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "SRMGR");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(txtComp.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + txtZone.Text + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }



        private void chkJoin_CheckedChanged(object sender, EventArgs e)
        {
            if (chkJoin.Checked == true)
                dtJoin.Enabled = true;
            else
                dtJoin.Enabled = false;
        }

        private void chkHO_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHO.Checked == true)
                dtHO.Enabled = true;
            else
                dtHO.Enabled = false;
        }

        private void chkAD_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAD.Checked == true)
                dtSR.Enabled = true;
            else
                dtSR.Enabled = false;
        }

        private void ImgBtnPC_Click(object sender, EventArgs e)
        {
            try
            {
                txtProfitCenter.Text = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtProfitCenter;
                _CommonSearch.txtSearchbyword.Text = txtProfitCenter.Text;
                _CommonSearch.ShowDialog();
                txtProfitCenter.Focus();

                get_PCDet();
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

        private void get_PCDet()
        {
            try
            {
                txtPCDesn.Text = "";
                MasterProfitCenter _masterPC = null;
                _masterPC = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, txtProfitCenter.Text);
                if (_masterPC != null)
                    txtPCDesn.Text = _masterPC.Mpc_desc;

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void get_Man_Name()
        {
            txtManName.Text = "";
            try
            {
                DataTable _dt = CHNLSVC.Sales.GetEmployee(BaseCls.GlbUserComCode, txtManCode.Text);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    txtManName.Text = _dt.Rows[0]["ESEP_FIRST_NAME"].ToString() + ' ' + _dt.Rows[0]["ESEP_LAST_NAME"].ToString();
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

        private void get_SR_Det()
        {
            try
            {
                DataTable _dt = CHNLSVC.Sales.GetESDSrDet(BaseCls.GlbUserComCode, txtProfitCenter.Text, txtManCode.Text);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    if (Convert.ToDateTime(_dt.Rows[0]["ESDM_DT_JOIN"]) != Convert.ToDateTime("31-Dec-2999"))
                    {
                        chkJoin.Checked = true;
                        dtJoin.Text = _dt.Rows[0]["ESDM_DT_JOIN"].ToString();
                    }
                    if (Convert.ToDateTime(_dt.Rows[0]["ESDM_DT_HO"]) != Convert.ToDateTime("31-Dec-2999"))
                    {
                        chkHO.Checked = true;
                        dtHO.Text = _dt.Rows[0]["ESDM_DT_HO"].ToString();
                    }
                    if (Convert.ToDateTime(_dt.Rows[0]["ESDM_DT_AD"]) != Convert.ToDateTime("31-Dec-2999"))
                    {
                        chkAD.Checked = true;
                        dtSR.Text = _dt.Rows[0]["ESDM_DT_AD"].ToString();
                    }
                    txtSUNAcc.Text = _dt.Rows[0]["ESDM_SUN_ACC"].ToString();
                    dtMonth_ValueChanged(null, null);
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




        private void txtPC_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtProfitCenter.Text))
                {
                    DataTable _result = CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbUserComCode, txtProfitCenter.Text.Trim());
                    if (_result == null)
                    {
                        MessageBox.Show("Enter valid profit center!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtProfitCenter.Focus();
                        return;
                    }
                    if (_result.Rows.Count <= 0)
                    {
                        MessageBox.Show("Enter valid profit center!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtProfitCenter.Focus();
                        return;
                    }
                    get_PCDet();
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

        private void txtManCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtManCode.Text))
            {
                get_Man_Name();
                get_SR_Det();
            }
        }

        private void btnSaveSR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPCDesn.Text))
            {
                MessageBox.Show("Select profit center!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProfitCenter.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtManName.Text))
            {
                MessageBox.Show("Select the manager!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtManCode.Focus();
                return;
            }
            if (chkJoin.Checked == true)
            {
                if (string.IsNullOrEmpty(txtSUNAcc.Text))
                {
                    MessageBox.Show("Enter the SUN account number!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSUNAcc.Focus();
                    return;
                }
            }

            if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            DateTime _dtJoin = chkJoin.Checked == true ? Convert.ToDateTime(dtJoin.Text) : Convert.ToDateTime("31/Dec/2999");
            DateTime _dtHO = chkHO.Checked == true ? Convert.ToDateTime(dtHO.Text) : Convert.ToDateTime("31/Dec/2999");
            DateTime _dtAD = chkAD.Checked == true ? Convert.ToDateTime(dtSR.Text) : Convert.ToDateTime("31/Dec/2999");

            try
            {
                Int32 x = CHNLSVC.General.Update_ESD_SR_Details(BaseCls.GlbUserComCode, txtProfitCenter.Text, txtManCode.Text, _dtJoin, _dtHO, _dtAD, txtSUNAcc.Text, BaseCls.GlbUserID);
                MessageBox.Show("Successfully updated", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void ESDProcess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void ddlDocTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDocTypes.SelectedValue == "REC")
                txtRecNo.Enabled = true;
            else
            {
                txtRecNo.Text = "";
                txtRecNo.Enabled = false;
            }
        }

        private void dtMonth_ValueChanged(object sender, EventArgs e)
        {
            DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dtMonth.Value.Month + "/" + dtMonth.Value.Year).Date;
            try
            {
                Boolean _isFin = CHNLSVC.General.checkESDStatus(BaseCls.GlbUserComCode, txtProfitCenter.Text, txtManCode.Text, _tmpMonth.Date);
                if (_isFin == true)
                {
                    lblStus.Text = "Finalized";
                    btnConfirm.Enabled = false;
                    btnProcess.Enabled = false;
                }
                else
                {
                    lblStus.Text = "Pending";
                    btnConfirm.Enabled = true;
                    btnProcess.Enabled = true;
                }

                get_ESD_Txns();
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

        private void get_ESD_Txns()
        {
            ESDTxnList = new List<ESDTxn>();
            DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dtMonth.Value.Month + "/" + dtMonth.Value.Year).Date;
            try
            {
                DataTable _tem = CHNLSVC.General.GetESDTransactions(BaseCls.GlbUserComCode, txtProfitCenter.Text, txtManCode.Text, _tmpMonth.Date);
                foreach (DataRow dr in _tem.Rows)
                {
                    ESDTxn _esdTxn = new ESDTxn();
                    _esdTxn.Esd_anal_1 = 0;
                    _esdTxn.Esd_com = dr["ESD_COM"].ToString();
                    _esdTxn.Esd_cr = Convert.ToDecimal(dr["Esd_cr"]);
                    _esdTxn.Esd_cre_by = dr["Esd_cre_by"].ToString();
                    _esdTxn.Esd_cre_when = Convert.ToDateTime(dr["Esd_cre_when"]);
                    _esdTxn.Esd_desc = dr["Esd_desc"].ToString();
                    _esdTxn.Esd_dr = Convert.ToDecimal(dr["Esd_dr"]);
                    _esdTxn.Esd_dt = Convert.ToDateTime(dr["Esd_dt"]);
                    _esdTxn.Esd_epf = dr["Esd_epf"].ToString();
                    _esdTxn.Esd_mod_by = dr["Esd_mod_by"].ToString();
                    _esdTxn.Esd_mod_when = Convert.ToDateTime(dr["Esd_mod_when"]);
                    _esdTxn.Esd_month = Convert.ToDateTime(dr["Esd_month"]);
                    _esdTxn.Esd_pc = dr["Esd_pc"].ToString();
                    _esdTxn.Esd_ref = dr["Esd_ref"].ToString();
                    _esdTxn.Esd_rem = dr["Esd_rem"].ToString();
                    _esdTxn.Esd_tp = dr["Esd_tp"].ToString();
                    ESDTxnList.Add(_esdTxn);
                }

                grvDet.AutoGenerateColumns = false;
                BindingSource _source = new BindingSource();
                _source.DataSource = ESDTxnList;
                grvDet.DataSource = _source;

                lblESD_Adj.Text = "0.00";
                lblESD_opbal.Text = "0.00";
                lblESD_Cont.Text = "0.00";
                lblESD_Clbal.Text = "0.00";
                lblInt_opbal.Text = "0.00";
                lblInt_Adj.Text = "0.00";
                lblInt_Cont.Text = "0.00";
                lblInt_Clbal.Text = "0.00";

                DataTable _dt = CHNLSVC.General.GetESDBalance(BaseCls.GlbUserComCode, txtProfitCenter.Text, txtManCode.Text, _tmpMonth.Date);
                foreach (DataRow dr1 in _dt.Rows)
                {
                    Decimal _EAdj = Convert.ToDecimal(dr1["ESDB_ESD_ADJ"]);
                    lblESD_Adj.Text = _EAdj.ToString("0.00");
                    Decimal _EOBal = Convert.ToDecimal(dr1["ESDB_ESD_OPBAL"]);
                    lblESD_opbal.Text = _EOBal.ToString("0.00");
                    Decimal _ECont = Convert.ToDecimal(dr1["ESDB_ESD_CONT"]);
                    lblESD_Cont.Text = _ECont.ToString("0.00");
                    Decimal _ECBal = Convert.ToDecimal(dr1["ESDB_ESD_CLBAL"]);
                    lblESD_Clbal.Text = _ECBal.ToString("0.00");
                    Decimal _IOBal = Convert.ToDecimal(dr1["ESDB_INT_OPBAL"]);
                    lblInt_opbal.Text = _IOBal.ToString("0.00");
                    Decimal _IAdj = Convert.ToDecimal(dr1["ESDB_INT_ADJ"]);
                    lblInt_Adj.Text = _IAdj.ToString("0.00");
                    Decimal _ICont = Convert.ToDecimal(dr1["ESDB_INT_CONT"]);
                    lblInt_Cont.Text = _ICont.ToString("0.00");
                    Decimal _ICBal = Convert.ToDecimal(dr1["ESDB_INT_CLBAL"]);
                    lblInt_Clbal.Text = _ICBal.ToString("0.00");
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPCDesn.Text))
            {
                MessageBox.Show("Select profit center!", "Process", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProfitCenter.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtManName.Text))
            {
                MessageBox.Show("Select the manager!", "Process", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtManCode.Focus();
                return;
            }
            if (Convert.ToDecimal(txtAmt.Text) <= 0)
            {
                MessageBox.Show("Invalid Amount !", "Process", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmt.Focus();
                return;
            }
            DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dtMonth.Value.Month + "/" + dtMonth.Value.Year).Date;

            ESDTxn _esdtxn = new ESDTxn();
            _esdtxn.Esd_com = BaseCls.GlbUserComCode;
            _esdtxn.Esd_cre_by = BaseCls.GlbUserID;
            _esdtxn.Esd_desc = ddlDocTypes.Text;
            _esdtxn.Esd_dt = dtDate.Value.Date;
            _esdtxn.Esd_epf = txtManCode.Text;
            _esdtxn.Esd_mod_by = BaseCls.GlbUserID;
            _esdtxn.Esd_month = _tmpMonth.Date;
            _esdtxn.Esd_pc = txtProfitCenter.Text;
            _esdtxn.Esd_ref = txtRecNo.Text;
            _esdtxn.Esd_rem = txtRem.Text;
            _esdtxn.Esd_tp = ddlDocTypes.SelectedValue.ToString();
            if (ddlDocTypes.SelectedValue == "REC" || ddlDocTypes.SelectedValue == "EADJP" || ddlDocTypes.SelectedValue == "IADJP")
                _esdtxn.Esd_cr = Convert.ToDecimal(txtAmt.Text);
            else if (ddlDocTypes.SelectedValue == "EADJM" || ddlDocTypes.SelectedValue == "IADJM")
                _esdtxn.Esd_dr = Convert.ToDecimal(txtAmt.Text);

            _esdtxn.Esd_anal_1 = 1;
            ESDTxnList.Add(_esdtxn);

            grvDet.AutoGenerateColumns = false;
            grvDet.DataSource = new List<ESDTxn>();
            grvDet.DataSource = ESDTxnList;

            clear_det();

            lblESD_Adj.Text = "0.00";
            lblESD_Clbal.Text = "0.00";
            lblESD_Cont.Text = "0.00";
            lblESD_opbal.Text = "0.00";
            lblInt_Adj.Text = "0.00";
            lblInt_Clbal.Text = "0.00";
            lblInt_Cont.Text = "0.00";
            lblInt_opbal.Text = "0.00";
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPCDesn.Text))
            {
                MessageBox.Show("Select profit center!", "Process", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProfitCenter.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtManName.Text))
            {
                MessageBox.Show("Select the manager!", "Process", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtManCode.Focus();
                return;
            }
            try
            {
                Boolean isPeriodClose = CHNLSVC.Financial.IsPeriodClosed(BaseCls.GlbUserComCode, txtProfitCenter.Text, "FIN_REM", dtMonth.Value.Date);
                if (isPeriodClose == false)
                {
                    MessageBox.Show("Not Finalized the period!", "Process", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are you sure ?", "Process", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                Int32 X = CHNLSVC.General.SaveESDTransaction(ESDTxnList);

                int _effCal = calc(txtProfitCenter.Text, txtManCode.Text, dtMonth.Value.Date);
                if (_effCal > 0)
                {
                    DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dtMonth.Value.Month + "/" + dtMonth.Value.Year).Date;

                    ESDBal _esdBal = new ESDBal();
                    _esdBal.Esdb_com = BaseCls.GlbUserComCode;
                    _esdBal.Esdb_epf = txtManCode.Text;
                    _esdBal.Esdb_esd_adj = Convert.ToDecimal(lblESD_Adj.Text);
                    _esdBal.Esdb_esd_clbal = Convert.ToDecimal(lblESD_Clbal.Text);
                    _esdBal.Esdb_esd_cont = Convert.ToDecimal(lblESD_Cont.Text);
                    _esdBal.Esdb_esd_opbal = Convert.ToDecimal(lblESD_opbal.Text);
                    _esdBal.Esdb_int_adj = Convert.ToDecimal(lblInt_Adj.Text);
                    _esdBal.Esdb_int_clbal = Convert.ToDecimal(lblInt_Clbal.Text);
                    _esdBal.Esdb_int_cont = Convert.ToDecimal(lblInt_Cont.Text);
                    _esdBal.Esdb_int_opbal = Convert.ToDecimal(lblInt_opbal.Text);
                    _esdBal.Esdb_mod_by = BaseCls.GlbUserID;
                    _esdBal.Esdb_month = _tmpMonth.Date;
                    _esdBal.Esdb_pc = txtProfitCenter.Text;
                    _esdBal.Esdb_stus = "P";

                    Int32 Y = CHNLSVC.General.SaveESDBalance(_esdBal);
                    int count = ESDTxnList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        ESDTxnList[i].Esd_anal_1 = 0;
                    }

                    MessageBox.Show("Successfully processed", "Proces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //btnProcess.Enabled = false;
                    btnConfirm.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Previous month not finalized !", "Finalize", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtMonth.Focus();
                    btnProcess.Enabled = true;
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

        private int calc(string _pc, string _epf, DateTime _dtMnth)
        {
            Decimal _ESDBal = 0;
            Decimal _IntBal = 0;
            Decimal _ESDCont = 0;
            Decimal _ESDAdj = 0;
            Decimal _IntAdj = 0;
            Decimal _intRate = 0;
            int _ret = 0;

            DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + _dtMnth.Month + "/" + _dtMnth.Year).Date;
            int _eff = CHNLSVC.General.GetESDPrvBalance(BaseCls.GlbUserComCode, _pc, _epf, _tmpMonth, _tmpMonth.Date.AddMonths(-1).Date, out _ESDBal, out _IntBal, out _ESDCont, out _ESDAdj, out _IntAdj);

            lblInt_opbal.Text = _IntBal.ToString("0.00");
            lblInt_Adj.Text = _IntAdj.ToString("0.00");

            lblESD_opbal.Text = _ESDBal.ToString("0.00");
            lblESD_Adj.Text = _ESDAdj.ToString("0.00");
            lblESD_Cont.Text = _ESDCont.ToString("0.00");

            HpSystemParameters _SystemPara = new HpSystemParameters();
            _SystemPara = CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "ESDINTRATE", DateTime.Now.Date);

            if (_SystemPara.Hsy_cd != null)
                _intRate = _SystemPara.Hsy_val;

            lblInt_Cont.Text = ((_ESDBal + _IntBal + _IntAdj) / 100 * _intRate).ToString("0.00");

            lblESD_Clbal.Text = (_ESDBal + _ESDCont + _ESDAdj).ToString("0.00");
            lblInt_Clbal.Text = (_IntBal + Convert.ToDecimal(lblInt_Cont.Text) + _IntAdj).ToString("0.00");

            Boolean _isfound = CHNLSVC.General.checkESDBalFound(BaseCls.GlbUserComCode, _pc, _epf);
            if (_isfound == true)
            {
                if (_eff > 0)
                {
                    return 1;
                }
                else
                {
                    return _ret;
                }
            }
            else
            {
                return 1;
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear_data();
        }

        private void clear_det()
        {
            txtAmt.Text = "0";
            txtRecNo.Text = "";
            txtRem.Text = "";
        }

        private void clear_data()
        {
            lblESD_Adj.Text = "0.00";
            lblESD_Clbal.Text = "0.00";
            lblESD_Cont.Text = "0.00";
            lblESD_opbal.Text = "0.00";
            lblInt_Adj.Text = "0.00";
            lblInt_Clbal.Text = "0.00";
            lblInt_Cont.Text = "0.00";
            lblInt_opbal.Text = "0.00";
            grvDet.DataSource = new List<ESDTxn>();
            txtProfitCenter.Text = "";
            txtPCDesn.Text = "";
            txtManCode.Text = "";
            txtManName.Text = "";
            ESDTxnList = new List<ESDTxn>();

            clear_det();

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPCDesn.Text))
            {
                MessageBox.Show("Select profit center!", "Finalize", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProfitCenter.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtManName.Text))
            {
                MessageBox.Show("Select the manager!", "Finalize", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtManCode.Focus();
                return;
            }

            if (lblStus.Text == "Finalized")
            {
                MessageBox.Show("Already finalized", "Finalize", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtMonth.Focus();
                return;
            }

            if (MessageBox.Show("Are you sure ?", "Finalize", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dtMonth.Value.Month + "/" + dtMonth.Value.Year).Date;

            try
            {
                Int32 X = CHNLSVC.General.updateESDStatus(BaseCls.GlbUserComCode, txtProfitCenter.Text, txtManCode.Text, _tmpMonth.Date);
                MessageBox.Show("Successfully finalized", "Finalize", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnConfirm.Enabled = false;
                btnProcess.Enabled = false;
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

        private void btn_Srch_chnl_Click(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChanel;
                _CommonSearch.ShowDialog();
                txtChanel.Select();

                DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "CHNL", txtChanel.Text);
                foreach (DataRow row2 in LocDes.Rows)
                {
                    txtChnlDesc.Text = row2["descp"].ToString();
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

            txtProfitCenter.Text = "";
            txtSChanel.Text = "";
            txtArea.Text = "";
            txtRegion.Text = "";
            txtZone.Text = "";
        }

        private void btn_Srch_schnl_Click(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSChanel;
                _CommonSearch.ShowDialog();
                txtSChanel.Select();

                DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "SCHNL", txtSChanel.Text);
                foreach (DataRow row2 in LocDes.Rows)
                {
                    txtSChnlDesc.Text = row2["descp"].ToString();
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
            txtProfitCenter.Text = "";
            txtArea.Text = "";
            txtRegion.Text = "";
            txtZone.Text = "";
        }

        private void btn_Srch_area_Click(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtArea;
                _CommonSearch.ShowDialog();
                txtArea.Select();

                DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "AREA", txtArea.Text);
                foreach (DataRow row2 in LocDes.Rows)
                {
                    txtAreaDesc.Text = row2["descp"].ToString();
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

            txtProfitCenter.Text = "";
            txtRegion.Text = "";
            txtZone.Text = "";
        }

        private void btn_Srch_reg_Click(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRegion;
                _CommonSearch.ShowDialog();
                txtRegion.Select();

                DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "REGION", txtRegion.Text);
                foreach (DataRow row2 in LocDes.Rows)
                {
                    txtRegDesc.Text = row2["descp"].ToString();
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
            txtProfitCenter.Text = "";
            txtZone.Text = "";
        }

        private void btn_Srch_zone_Click(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtZone;
                _CommonSearch.ShowDialog();
                txtZone.Select();

                DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "ZONE", txtZone.Text);
                foreach (DataRow row2 in LocDes.Rows)
                {
                    txtZoneDesc.Text = row2["descp"].ToString();
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
            txtProfitCenter.Text = "";
        }

        private void btn_Srch_pc_Click(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
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
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
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
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPF"))
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10046))
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_chnl_Click(null, null);
            }
        }

        private void txtSChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_schnl_Click(null, null);
            }
        }

        private void txtArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_area_Click(null, null);
            }
        }

        private void txtRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_reg_Click(null, null);
            }
        }

        private void txtZone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_zone_Click(null, null);
            }
        }



        private void btn_srch_man_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeEPF);
                DataTable _result = CHNLSVC.CommonSearch.Get_employee_EPF(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtManCode;
                _CommonSearch.txtSearchbyword.Text = txtManCode.Text;
                _CommonSearch.ShowDialog();
                txtManCode.Focus();

                get_Man_Name();
                get_SR_Det();
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

        private void txtProfitCenter_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProfitCenter.Text))
                get_PCDet();
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_pc_Click(null, null);
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem Item in lstPC.Items)
                {
                    Item.Checked = true;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem Item in lstPC.Items)
                {
                    Item.Checked = false;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnClearPC_Click(object sender, EventArgs e)
        {
            lstPC.Clear();
        }

        private void btnProcessAll_Click(object sender, EventArgs e)
        {
            Int32 _CNT = 0;
            Int32 _AllCNT = 0;
            string _err = "";
            string _manEPF = "";

            if (lstPC.Items.Count == 0)
            {
                MessageBox.Show("Select the locations !", "Process", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure ?", "Process", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dtMonthAll.Value.Month + "/" + dtMonthAll.Value.Year).Date;
            DateTime _from = _tmpMonth.AddDays(1 - _tmpMonth.Day);
            DateTime _to = _from.AddMonths(1).AddSeconds(-1);

            foreach (ListViewItem Item in lstPC.Items)
            {
                string pc = Item.Text;
                if (Item.Checked == true)
                {
                    Boolean isPeriodClose = CHNLSVC.Financial.IsPeriodClosed(BaseCls.GlbUserComCode, pc, "FIN_REM", dtMonthAll.Value.Date);
                    if (isPeriodClose == false)
                    {
                        MessageBox.Show("Process halted. " + pc + " is not Finalized the period !", "Process", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _AllCNT = _AllCNT + 1;
                }
            }
            foreach (ListViewItem Item in lstPC.Items)
            {
                string pc = Item.Text;
                if (Item.Checked == true)
                {
                    //DataTable _dtEPF = CHNLSVC.Sales.GetManagerEPF(BaseCls.GlbUserComCode, pc);
                    DataTable _dtEPF = CHNLSVC.Sales.GetESDManagerEPF(BaseCls.GlbUserComCode, pc, _from, _to);
                    if (_dtEPF.Rows.Count == 0)
                    {
                        _err = pc + " - manager not found";
                        lstErr.Items.Add(_err.ToString());
                        goto NotFound;
                    }
                    else
                    {
                        foreach (DataRow dr in _dtEPF.Rows)
                        {
                            _manEPF = _dtEPF.Rows[0]["rem_man_cd"].ToString();

                            int _effCal = calc(pc, _manEPF, dtMonthAll.Value.Date);
                            if (_effCal > 0)
                            {

                                ESDBal _esdBal = new ESDBal();
                                _esdBal.Esdb_com = BaseCls.GlbUserComCode;
                                _esdBal.Esdb_epf = _manEPF;
                                _esdBal.Esdb_esd_adj = Convert.ToDecimal(lblESD_Adj.Text);
                                _esdBal.Esdb_esd_clbal = Convert.ToDecimal(lblESD_Clbal.Text);
                                _esdBal.Esdb_esd_cont = Convert.ToDecimal(lblESD_Cont.Text);
                                _esdBal.Esdb_esd_opbal = Convert.ToDecimal(lblESD_opbal.Text);
                                _esdBal.Esdb_int_adj = Convert.ToDecimal(lblInt_Adj.Text);
                                _esdBal.Esdb_int_clbal = Convert.ToDecimal(lblInt_Clbal.Text);
                                _esdBal.Esdb_int_cont = Convert.ToDecimal(lblInt_Cont.Text);
                                _esdBal.Esdb_int_opbal = Convert.ToDecimal(lblInt_opbal.Text);
                                _esdBal.Esdb_mod_by = BaseCls.GlbUserID;
                                _esdBal.Esdb_month = _tmpMonth.Date;
                                _esdBal.Esdb_pc = pc;
                                _esdBal.Esdb_stus = "P";

                                Int32 Y = CHNLSVC.General.SaveESDBalance(_esdBal);

                                // MessageBox.Show("Successfully processed", "Proces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //btnProcess.Enabled = false;
                                //  btnConfirm.Enabled = true;
                            }
                            else
                            {
                                _err = pc + " - previous month not finalized";
                                lstErr.Items.Add(_err.ToString());
                            }
                        }
                        
                    }

                NotFound:
                    _CNT = _CNT + 1;
                    progressBar1.Value = _CNT / _AllCNT * 100;
                }
            }
            MessageBox.Show("Successfully processed", "Proces", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnProcessAll.Enabled = false;
            btnConfirmAll.Enabled = true;
        }

        private void btnConfirmAll_Click(object sender, EventArgs e)
        {
            string _manEPF = "";
            string _err = "";
            Int32 _CNT = 0;
            Int32 _AllCNT = 0;


            if (lstPC.Items.Count == 0)
            {
                MessageBox.Show("Select the locations !", "Process", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure ?", "Process", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            lstErr.Clear();

            DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dtMonthAll.Value.Month + "/" + dtMonthAll.Value.Year).Date;

            foreach (ListViewItem Item in lstPC.Items)
            {
                string pc = Item.Text;
                if (Item.Checked == true)
                {
                    DataTable _dtEPF = CHNLSVC.Sales.GetManagerEPF(BaseCls.GlbUserComCode, pc);
                    if (_dtEPF != null && _dtEPF.Rows.Count > 0)
                        _manEPF = _dtEPF.Rows[0]["esep_epf"].ToString();

                    Boolean _isFin = CHNLSVC.General.checkESDStatus(BaseCls.GlbUserComCode, pc, _manEPF, _tmpMonth.Date);
                    if (_isFin == true)
                    {
                        _err = pc + " - already finalized";
                        lstErr.Items.Add(_err.ToString());
                        //return;
                    }
                    _AllCNT = _AllCNT + 1;
                }
            }

            foreach (ListViewItem Item in lstPC.Items)
            {
                string pc = Item.Text;
                if (Item.Checked == true)
                {
                    DataTable _dtEPF = CHNLSVC.Sales.GetManagerEPF(BaseCls.GlbUserComCode, pc);
                    if (_dtEPF != null && _dtEPF.Rows.Count > 0)
                        _manEPF = _dtEPF.Rows[0]["esep_epf"].ToString();
                    else
                    {
                        _err = pc + " - manager not found";
                        lstErr.Items.Add(_err.ToString());
                        goto NotFound;
                    }
                    _CNT = _CNT + 1;
                    progressBar1.Value = _CNT / _AllCNT * 100;
                    Int32 X = CHNLSVC.General.updateESDStatus(BaseCls.GlbUserComCode, pc, _manEPF, _tmpMonth.Date);

                }
            NotFound:
                _CNT = _CNT;
                // progressBar1.Value =  100;
            }
            MessageBox.Show("Successfully finalized", "Finalize", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnConfirmAll.Enabled = false;
            btnProcessAll.Enabled = false;
        }


    }
}
