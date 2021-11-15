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
using FF.WindowsERPClient.General;
using System.Globalization;
using FF.WindowsERPClient.Reports.Sales;

//Written By kapila on 26/12/2012
namespace FF.WindowsERPClient.General
{
    public partial class SignOff : Base
    {
        private static Decimal _cashTot = 0;
        private static Decimal _LessTotal = 0;
        bool _isDecimalAllow = false;
        Int32 _sig_seq = 0;

        public SignOff()
        {
            InitializeComponent();
            InitializeEnv();
            txtCashier.Text = BaseCls.GlbUserID;
            get_user_name(txtCashier.Text);
          //  load_opBal();
            BindGridView();
            txtOffDt.Text = DateTime.Now.ToShortDateString();
            txtOffTime.Text = DateTime.Now.ToShortTimeString();
            txtLogTime.Text = DateTime.Now.ToShortDateString();
        }

        private void BindGridView()
        {
            DataTable _dt = null;
            _dt = CHNLSVC.Financial.get_denom_types(BaseCls.GlbUserComCode, 1);
            grvDenom.AutoGenerateColumns = false;
            grvDenom.DataSource = _dt;

            _dt = CHNLSVC.Financial.get_denom_types(BaseCls.GlbUserComCode, 0);
            grvOth.AutoGenerateColumns = false;
            grvOth.DataSource = _dt;

        }

        private void get_user_name(string _ID)
        {
            DataTable _tbl = CHNLSVC.Inventory.GetUserNameByUserID(_ID);
            if (_tbl != null && _tbl.Rows.Count > 0)
                txtUser.Text = _tbl.Rows[0].Field<string>("se_usr_name");
            else
                txtUser.Text = "";

           
        }

        private void InitializeEnv()
        {
          //  dtpMonth.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
         //   gvRec.AutoGenerateColumns = false;

            txtUser.Text = BaseCls.GlbUserID;
            txtCashier.Text = BaseCls.GlbUserID;
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text))
            {
                MessageBox.Show("Please select the cashier!", "Sign Off", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCashier.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Please enter the station ID!", "Sign Off", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtID.Focus();
                return;
            }
            if (MessageBox.Show("Are you sure ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            Sign_On _signon = new Sign_On();
            _signon.Sig_com = BaseCls.GlbUserComCode;
            _signon.Sig_pc = BaseCls.GlbUserDefProf;
            _signon.Sig_terminal = "";
            _signon.Sig_session = BaseCls.GlbUserSessionID;
            _signon.Sig_cashier = BaseCls.GlbUserID;
            // _signon.Sig_sign_by = BaseCls.GlbUserID;
             _signon.Sig_on_dt = Convert.ToDateTime(txtLogTime.Text);
            // _signon.Sig_op_bal = Convert.ToDecimal(txtOpenBal.Text);
            _signon.Sig_sign_off_by = BaseCls.GlbUserID;
            _signon.Sig_off_dt = DateTime.Now;
            _signon.Sig_close_bal = Convert.ToDecimal(txtCloseBal.Text);
            _signon.Sig_stus = "F";
            _signon.Sig_rem = "";
            _signon.Sig_create_by = BaseCls.GlbUserID;
            _signon.Sig_mod_by = BaseCls.GlbUserID;
            _signon.Sig_sys_opbal = 0;
            _signon.Sig_sys_clsbal = 0;
            _signon.Is_Sign_On = 0;

            Int32 _x = 0;
            Int32 _eff = CHNLSVC.General.SaveSignOnOff(_signon, out _x);
            if (_eff == 1)
            {
                MessageBox.Show("Successfully Sign Off", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnProcess.Enabled = false;

                clsSalesRep obj = new clsSalesRep();
                BaseCls.GlbReportParaLine1 = _sig_seq;

                obj.POSSignOffDirectPrint();

                txtSeq.Text = _sig_seq.ToString();
                loadDetBySeq();

                clearAll();
                
            }
            else
                MessageBox.Show("Process terminated", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void clearAll()
        {
           // gvRec.DataSource = null;

            txtOpenBal.Text = "0.00";
            lbl_CIH.Text = "0.00";
            lbl_comm_wdr.Text = "0.00";
            lbl_diff.Text = "0.00";
            lbl_TotRem.Text = "0.00";
            txtCloseBal.Text = "0.00";
            txtLogTime.Text = DateTime.Now.ToShortDateString();


        }

        private void btn_View_Click(object sender, EventArgs e)
        {

        }

        private void GetCompanyDet(out string _comp, out string _addr)
        {

            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            if (_masterComp != null)
            {
                _comp = _masterComp.Mc_desc;
                _addr = _masterComp.Mc_add1 + _masterComp.Mc_add2;
            }
            else
            {
                _comp = "";
                _addr = "";
            }
        }

        private void load_opBal()
        {
            decimal _opBal = 0;
            decimal _closeBal = 0;
            _sig_seq = 0;
         //   txtSeq.Enabled = true;

            btnSave.Enabled = false;
            btnProcess.Enabled = false;

            //pending sign on
            DataTable _dt = CHNLSVC.Financial.get_SignOn(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtCashier.Text, Convert.ToDateTime(txtLogTime.Text));
            if (_dt.Rows.Count == 1)
            {
              //  txtSignOn.Text = _dt.Rows[0]["sig_on_dt"].ToString();
              //  txtLogTime.Text = _dt.Rows[0]["sig_on_dt"].ToString();
             //   txtSignOff.Text = _dt.Rows[0]["sig_off_dt"].ToString();
                txtOpenBal.Text = _dt.Rows[0]["sig_op_bal"].ToString();
                _sig_seq = Convert.ToInt32(_dt.Rows[0]["sig_seq_no"]);
               // txtSeq.Text = _sig_seq.ToString();
                txtSeq.Enabled = false;

                btnProcess.Enabled = true;
            }
            else
            {
                 _dt = CHNLSVC.Financial.getPendingDenom(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtCashier.Text);
                if (_dt.Rows.Count == 1)
                {
                    txtSignOn.Text = _dt.Rows[0]["sig_on_dt"].ToString();
                  //  txtLogTime.Text = _dt.Rows[0]["sig_on_dt"].ToString();
                    txtSignOff.Text = _dt.Rows[0]["sig_off_dt"].ToString();
                   // txtOpenBal.Text = _dt.Rows[0]["sig_op_bal"].ToString();
                    _sig_seq = Convert.ToInt32(_dt.Rows[0]["sig_seq_no"]);
                    txtSeq.Text = _sig_seq.ToString();
                    txtSeq.Enabled = false;

                    btnSave.Enabled = true;
                }
            }
           
         //   DataTable _dt1=CHNLSVC.Financial.GetOpenBal((BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf,  txtCashier.Text,)

        }

        private void btn_View_Click_1(object sender, EventArgs e)
        {

            _LessTotal = 0;
            //DataTable dt = CHNLSVC.Financial.PrintSignOff(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(DateTime.Now.Date).Date, Convert.ToDateTime(DateTime.Now.Date).Date, BaseCls.GlbUserID, 1, 0);
            //gvRec.DataSource = dt;

            //DataTable dtLess = CHNLSVC.Financial.get_SignOff_less(BaseCls.GlbUserDefProf, Convert.ToDateTime(DateTime.Now.Date).Date, Convert.ToDateTime(DateTime.Now.Date).Date);
            //gvLess.DataSource = dtLess;
            //for (int i = 0; i < gvLess.RowCount; i++)
            //{
            //    _LessTotal = _LessTotal + Convert.ToDecimal(gvLess.Rows[i].Cells["rem_val_final"].Value);
            //}

            //txtSysBal.Text = (Convert.ToDecimal(txtOpenBal.Text) + _cashTot - _LessTotal).ToString();
            //txtDif.Text = Convert.ToString(Convert.ToDecimal(txtCloseBal.Text) - Convert.ToDecimal(txtSysBal.Text));

            btnProcess.Enabled = true;
        }

        private void txtCloseBal_TextChanged(object sender, EventArgs e)
        {
           // if (!string.IsNullOrEmpty(txtCloseBal.Text))
             //   txtDif.Text = Convert.ToString(Convert.ToDecimal(txtCloseBal.Text) - Convert.ToDecimal(txtSysBal.Text));
        }

        private void txtCloseBal_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(_isDecimalAllow, sender, e);
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void txtOpenBal_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(_isDecimalAllow, sender, e);
        }

        private void btnSearchGV_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
            DataTable _result = CHNLSVC.CommonSearch.Get_All_SystemUsers(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCashier;
            _CommonSearch.ShowDialog();
            txtCashier.Select();

            get_user_name(txtCashier.Text);
            txtPW.Text = "";
            txtPW_Leave(null, null);
        }

        private void getPendingDeno()
        {

        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SystemUser:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SignOnSeq:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + txtCashier.Text + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void txtPW_Leave(object sender, EventArgs e)
        {
            Int32 _msgStatus = 0;
            string _msg = "";
            string _msgTitle = "";
            int _loginRetryOutCounter = 1;
            btn_View.Enabled = false;
            btnProcess.Enabled = false;

            if (!string.IsNullOrEmpty(txtCashier.Text))
            {
                if (!string.IsNullOrEmpty(txtPW.Text))
                {
                    LoginUser _loginUser = CHNLSVC.Security.LoginToSystem(txtCashier.Text.ToString().ToUpper(), txtPW.Text.ToString(), BaseCls.GlbUserComCode, BaseCls.GlbVersionNo, BaseCls.GlbUserIP, BaseCls.GlbHostName, 1, out _loginRetryOutCounter, out _msgStatus, out _msg, out _msgTitle);
                    if (_msgStatus == -1)
                    {
                        txtPW.Clear();
                        txtPW.Focus();
                        MessageBox.Show(_msg, _msgTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                        load_opBal();
                }
            }
        }

        private void txtPW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPW_Leave(null, null);
        }

        private void txtCashier_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCashier_Leave(object sender, EventArgs e)
        {
            txtPW.Text = "";
        }

        private void dtLogDt_ValueChanged(object sender, EventArgs e)
        {
            txtPW.Text = "";
            btn_View.Enabled = false;
            btnProcess.Enabled = false;
            txtOpenBal.Text = "0.00";
        }

        private void grvDenom_Sorted(object sender, EventArgs e)
        {

        }

        private void grvDenom_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 1)
            {
                if (grvDenom.Rows[e.RowIndex].Cells["GDT_DENOM"].Value.ToString()=="Coins")
                    grvDenom.Rows[e.RowIndex].Cells["value"].Value = 1 * Convert.ToDecimal(grvDenom.Rows[e.RowIndex].Cells["count"].Value);
                else
                grvDenom.Rows[e.RowIndex].Cells["value"].Value = Convert.ToDecimal(grvDenom.Rows[e.RowIndex].Cells["GDT_DENOM"].Value) * Convert.ToDecimal(grvDenom.Rows[e.RowIndex].Cells["count"].Value);
            }

            decimal _totDeno = 0;
            foreach (DataGridViewRow row in grvDenom.Rows)
            {
                _totDeno = _totDeno + Convert.ToDecimal(grvDenom.Rows[row.Index].Cells["value"].Value);
            }
            txtTotDeno.Text = _totDeno.ToString("0.00");

        }

        private void grvDenom_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSeq.Text))
            {
                MessageBox.Show("Please select the seq #", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBox.Show("Are you sure ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            List<DenomiDet> _lstDeno = new List<DenomiDet>();
            List<DenomiSum> _lstDenoSum = new List<DenomiSum>();
            
            foreach (DataGridViewRow row in grvDenom.Rows)
            {
                DenomiDet _objDeno = new DenomiDet();
                _objDeno.Gdd_amt= Convert.ToDecimal(row.Cells["value"].Value) ;
                _objDeno.Gdd_cre_by=BaseCls.GlbUserID;
                _objDeno.Gdd_mod_by=BaseCls.GlbUserID;
                _objDeno.Gdd_pay_subtp = row.Cells["GDT_DENOM"].Value.ToString();
                _objDeno.Gdd_pay_tp= "CASH"  ;
                _objDeno.Gdd_unit= Convert.ToInt32(row.Cells["count"].Value) ;
                _objDeno.Gdd_sum_line = 1;
                _objDeno.Gdd_seq =Convert.ToInt32(txtSeq.Text);
                _lstDeno.Add(_objDeno);
            }
            //add cash value
            DenomiSum _objDenoSum = new DenomiSum();
            _objDenoSum.Gds_cashier = txtCashier.Text;
            _objDenoSum.Gds_cre_by = BaseCls.GlbUserID;
            _objDenoSum.Gds_mod_by = BaseCls.GlbUserID;
            _objDenoSum.Gds_pay_tp = "CASH"; ;
            _objDenoSum.Gds_phy_amt = Convert.ToDecimal(txtTotDeno.Text);
            _objDenoSum.Gds_sys_amt = 0;
            _objDenoSum.Gds_seq_no = Convert.ToInt32(txtSeq.Text);
            _lstDenoSum.Add(_objDenoSum);

            foreach (DataGridViewRow row1 in grvOth.Rows)
            {
                _objDenoSum = new DenomiSum();
                _objDenoSum.Gds_cashier = txtCashier.Text;
                _objDenoSum.Gds_cre_by = BaseCls.GlbUserID;
                _objDenoSum.Gds_mod_by = BaseCls.GlbUserID;
                _objDenoSum.Gds_pay_tp = row1.Cells["GDT_PAY_TP"].Value.ToString();
                _objDenoSum.Gds_phy_amt = Convert.ToDecimal(row1.Cells["val"].Value);
                _objDenoSum.Gds_sys_amt = 0;
                _objDenoSum.Gds_seq_no = Convert.ToInt32(txtSeq.Text); 
                _lstDenoSum.Add(_objDenoSum);
            }


            int _eff = CHNLSVC.Financial.SaveDenominationDet(_lstDeno, _lstDenoSum);
            if (_eff > 0)
            {

                MessageBox.Show("Successfully Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                clsSalesRep obj = new clsSalesRep();
                BaseCls.GlbReportParaLine1 = _sig_seq;

                obj.POSDenoDirectPrint();

                clear();
            }
            else
                MessageBox.Show("Not Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void clear()
        {
            SignOff formnew = new SignOff();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }
        private void cmbSeq_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void loadDetBySeq()
        {
            DataTable _dt = null;
            btnSave.Enabled = false;
            decimal _val = 0;
            if (!string.IsNullOrEmpty(txtSeq.Text))
            {
                _dt = CHNLSVC.Financial.getSignOnBySeq(Convert.ToInt32(txtSeq.Text));
                if (_dt.Rows.Count > 0)
                {
                    txtSignOff.Text = _dt.Rows[0]["SIG_OFF_DT"].ToString();
                    txtSignOn.Text = _dt.Rows[0]["SIG_ON_DT"].ToString();
                    _val = Convert.ToDecimal(_dt.Rows[0]["sig_op_bal"]);
                    txtOpenBal.Text =_val.ToString("#,###,##0.00");
                    btnSave.Enabled = true;
                }
            }
        }

        private void btn_srch_seq_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SignOnSeq);
            DataTable _result = CHNLSVC.CommonSearch.GetSignOnSeq(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSeq;
            _CommonSearch.ShowDialog();
            txtSeq.Select();

            if (!string.IsNullOrEmpty(txtSeq.Text))
            _sig_seq = Convert.ToInt32(txtSeq.Text);

            loadDetBySeq();
        }

        private void SignOff_Load(object sender, EventArgs e)
        {

        }

        private void SignOff_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void dtpMonth_DateChanged(object sender, DateRangeEventArgs e)
        {
            txtLogTime.Text = dtpMonth.SelectionRange.Start.ToShortDateString();
            txtCashier.Text = "";
            txtPW.Text = "";
            btnProcess.Enabled = false;
            txtOpenBal.Text = "0.00";
        }
    }
}


