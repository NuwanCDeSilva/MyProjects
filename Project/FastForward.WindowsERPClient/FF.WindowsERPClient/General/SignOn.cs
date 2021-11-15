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
    public partial class SignOn : Base
    {
        bool _isDecimalAllow = false;

        public SignOn()
        {
            InitializeComponent();
            txtLogDt.Text = DateTime.Now.Date.ToShortDateString();
            txtLogTime.Text = DateTime.Now.ToShortTimeString();
            txtCashier.Text = BaseCls.GlbUserID;
            get_user_name(BaseCls.GlbUserID);

        }

        private void get_user_name(string _ID)
        {
            DataTable _tbl = CHNLSVC.Inventory.GetUserNameByUserID(_ID);
            if (_tbl != null && _tbl.Rows.Count > 0)
                txtUser.Text = _tbl.Rows[0].Field<string>("se_usr_name");
            else
                txtUser.Text = "";
        }

        private void clearAll()
        {
            txtOpenBal.Text = "0.00";
        }

        private void txtOpenBal_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(_isDecimalAllow, sender, e);
        }

        private void btnSignOn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text))
            {
                MessageBox.Show("Please select the cashier!", "Sign On", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCashier.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Please enter the station ID!", "Sign On", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtID.Focus();
                return;
            }

            if (MessageBox.Show("Are you sure ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            string _date = txtLogDt.Text;
            string _time = DateTime.Now.ToString("h:mm:ss tt");
            DateTime _dt = DateTime.ParseExact(_date + " " + _time, "dd/MMM/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

            Sign_On _signon = new Sign_On();
            _signon.Sig_com = BaseCls.GlbUserComCode;
            _signon.Sig_pc = BaseCls.GlbUserDefProf;
            _signon.Sig_terminal = txtID.Text;   //station ID
            _signon.Sig_session = BaseCls.GlbUserSessionID;
            _signon.Sig_cashier = BaseCls.GlbUserID;
            _signon.Sig_sign_by = BaseCls.GlbUserID;
            _signon.Sig_on_dt = _dt;
            _signon.Sig_op_bal = Convert.ToDecimal(txtOpenBal.Text);
            _signon.Sig_sign_off_by = "";
            //_signon.Sig_off_dt = 
            //_signon.Sig_close_bal = 
            _signon.Sig_stus = "P";
            _signon.Sig_rem = "";
            _signon.Sig_create_by = BaseCls.GlbUserID;
            _signon.Sig_mod_by = BaseCls.GlbUserID;
            _signon.Sig_sys_opbal = 0;
            _signon.Sig_sys_clsbal = 0;
            _signon.Is_Sign_On = 1;

            Int32 _sig_seq = 0;
            Int32 x = CHNLSVC.General.SaveSignOnOff(_signon, out _sig_seq);
            if (x > 0)
            {
                MessageBox.Show("Successfully Sign On", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSignOn.Enabled = false;
                clearAll();

                clsSalesRep obj = new clsSalesRep();
                BaseCls.GlbReportParaLine1 = _sig_seq;

                obj.POSSignOnDirectPrint();


            }
            else
                MessageBox.Show("Process terminated", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                case CommonUIDefiniton.SearchUserControlType.SignOnSeqbyDt:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + txtCashier.Text + seperator + Convert.ToDateTime (txtLogDt.Text).Date.ToShortDateString() + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
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

            txtPW.Text = "";
            btnSignOn.Enabled = false;
            get_user_name(txtCashier.Text);

        }

        private void txtPW_Leave(object sender, EventArgs e)
        {
            Int32 _msgStatus = 0;
            string _msg = "";
            string _msgTitle = "";
            int _loginRetryOutCounter = 1;
            btnSignOn.Enabled = false;

            if (!string.IsNullOrEmpty(txtPW.Text))
            {
                if (!string.IsNullOrEmpty(txtCashier.Text))
                {
                    LoginUser _loginUser = CHNLSVC.Security.LoginToSystem(txtCashier.Text.ToString().ToUpper(), txtPW.Text.ToString(), BaseCls.GlbUserComCode, BaseCls.GlbVersionNo, BaseCls.GlbUserIP, BaseCls.GlbHostName, 1, out _loginRetryOutCounter, out _msgStatus, out _msg, out _msgTitle);
                    if (_msgStatus == -1)
                    {
                        txtPW.Clear();
                        txtPW.Focus();
                        MessageBox.Show(_msg, _msgTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        DataTable _dt = CHNLSVC.Financial.get_SignOn(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtCashier.Text, Convert.ToDateTime(txtLogDt.Text));
                        if (_dt.Rows.Count == 1)
                        {
                            MessageBox.Show("Already sign on !", "Sign On", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtPW.Text = "";
                            txtPW.Focus();
                            return;
                        }
                        else
                            btnSignOn.Enabled = true;
                    }
                }
            }
        }

        private void txtPW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPW_Leave(null, null);
        }

        private void txtCashier_Leave(object sender, EventArgs e)
        {
            txtPW.Text = "";
        }

        private void SignOn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void dtpMonth_DateChanged(object sender, DateRangeEventArgs e)
        {
            txtLogDt.Text = dtpMonth.SelectionRange.Start.ToShortDateString();
            txtCashier.Text = "";
            txtSignOff.Text = "";
            txtSignOn.Text = "";
            txtSeq.Text = "";
            txtPW.Text = "";
        }

        private void btn_srch_seq_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCashier.Text))
            {
                MessageBox.Show("Please select the cashier!", "Sign On", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCashier.Focus();
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SignOnSeqbyDt);
            DataTable _result = CHNLSVC.CommonSearch.GetSignOnSeqByDate(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSeq;
            _CommonSearch.ShowDialog();
            txtSeq.Select();
            if (!string.IsNullOrEmpty(txtSeq.Text))
            {
                DataTable _dt = CHNLSVC.Financial.getSignOnBySeq(Convert.ToInt32(txtSeq.Text));
                if (_dt.Rows.Count > 0)
                {
                    txtSignOff.Text = _dt.Rows[0]["SIG_OFF_DT"].ToString();
                    txtSignOn.Text = _dt.Rows[0]["SIG_ON_DT"].ToString();
                }
            }
        }

        private void btnPrintOn_Click(object sender, EventArgs e)
        {
            clsSalesRep obj = new clsSalesRep();
            BaseCls.GlbReportParaLine1 = Convert.ToInt32(txtSeq.Text);

            obj.POSSignOnDirectPrint();


        }

        private void btnPrintOff_Click(object sender, EventArgs e)
        {
            clsSalesRep obj = new clsSalesRep();
            BaseCls.GlbReportParaLine1 = Convert.ToInt32(txtSeq.Text);

            obj.POSSignOffDirectPrint();

            if (MessageBox.Show("Do you want to print denominations ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            obj.POSDenoDirectPrint();
        }


    }
}


