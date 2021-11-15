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

namespace FF.WindowsERPClient.Security
{
    public partial class PasswordPolicy : Base
    {
        public PasswordPolicy()
        {
            try
            {
                InitializeComponent();
                string _errString = string.Empty;
                SecurityPolicy _securityPolicy = CHNLSVC.Security.GetSecurityPolicy(1, out _errString);
                if (string.IsNullOrEmpty(_errString))
                {
                    txtMaxPw.Text = _securityPolicy.Spp_max_pw_age.ToString();
                    txtMinPw.Text = _securityPolicy.Spp_min_pw_age.ToString();
                    txtPwHist.Text = _securityPolicy.Spp_pw_histtory.ToString();
                    txtMinPwLength.Text = _securityPolicy.Spp_min_pw_length.ToString();
                    txtLockUserAttemtps.Text = _securityPolicy.Spp_lock_err_atmps.ToString();
                    txtIdenticalCharactors.Text = _securityPolicy.Spp_cons_ident_char.ToString();
                    chkPwNotMatchUser.Checked = _securityPolicy.Spp_notmatch_usr;
                    chkPwComplexity.Checked = _securityPolicy.Spp_pw_complexity;
                    chkPwDictionary.Checked = _securityPolicy.Spp_pw_dictionary;
                }
                else
                {
                    MessageBox.Show("Error : \n" + _errString, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message.ToString(), "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally 
            {
                CHNLSVC.CloseChannel(); 
            }
        }

        private void chkPwNotMatchUser_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPwNotMatchUser.Checked == true)
            {
                chkPwNotMatchUser.Text = "Enabled";
            }
            else
            {
                chkPwNotMatchUser.Text = "Disabled";
            }
        }

        private void chkPwComplexity_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPwComplexity.Checked == true)
            {
                chkPwComplexity.Text = "Enabled";
            }
            else
            {
                chkPwComplexity.Text = "Disabled";
            }
        }

        private void chkPwDictionary_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPwDictionary.Checked == true)
            {
                chkPwDictionary.Text = "Enabled";
            }
            else
            {
                chkPwDictionary.Text = "Disabled";
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 1500; i++)
            {

                if (i == 1200)
                {
                    i = 1200;
                }

                //List<SystemUserCompany> _systemUserCompanyList = CHNLSVC.Security.GetUserCompany(username);
                //_systemUserCompanyList = null; 
                List<MasterDepartment> _deptList = CHNLSVC.General.GetDepartment();
                _deptList = null;

            }
        }
    }
}
