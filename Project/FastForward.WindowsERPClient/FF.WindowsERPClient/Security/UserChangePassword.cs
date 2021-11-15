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
using System.Text.RegularExpressions;
using System.Web.Security;

namespace FF.WindowsERPClient.Security
{
    public partial class UserChangePassword : Base
    {
        string userID, password;

        public UserChangePassword()
        {
            InitializeComponent();
            txtNewUserName.Text = BaseCls.GlbUserID;
        }
        public bool CheckPasswordComplexity(string password)
        {
            //  return CheckPasswordComplexity(Membership.Provider, password);
            if (string.IsNullOrEmpty(password)) return false;
            //if (password.Length < Membership.MinRequiredPasswordLength) return false;
            if (password.Length < 8) return false;

            int nonAlnumCount = 0;
            for (int i = 0; i < password.Length; i++)
            {
                if (!char.IsLetterOrDigit(password, i)) nonAlnumCount++;
            }

            if (nonAlnumCount < 1)//Membership.MinRequiredNonAlphanumericCharacters)
                return false;
            // if (!string.IsNullOrEmpty(Membership.PasswordStrengthRegularExpression) && !Regex.IsMatch(password, Membership.PasswordStrengthRegularExpression))
            //if (!Regex.IsMatch(password, "[A-Z]+")) //should contain atleast one capital letter
            //{
            //    return false;
            //}
            if (!Regex.IsMatch(password, "[\\d]+")) //should contain atleast one numeric character
            {
                return false;
            }
            return true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string _err = string.Empty;

                userID = txtNewUserName.Text.Trim().ToUpper();
                password = txtCurrentPassword.Text.Trim();
                string _newPw = txtNewPassword.Text.Trim();
                string _confirmPw = txtConfirmNewPassword.Text.Trim();

                bool isvaliduser = checkValidUser();
                if (isvaliduser == false)
                {
                    return;
                }

                if (_newPw != _confirmPw)
                {
                    MessageBox.Show("The passwords you typed don't match. Please try again!", "Change password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewPassword.Focus();
                    txtNewPassword.SelectAll();
                    return;
                }

                if (_newPw.ToUpper() == txtCurrentPassword.Text.ToUpper())
                {
                    _err = "Your new password was rejected because it does not meet the minimum security requirements.\n";
                    _err = _err + "\n" + "Your password was rejected because it:";
                    _err = _err + "\n" + "- was similar to your current password";

                    MessageBox.Show(_err, "Change password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewPassword.Focus();
                    txtNewPassword.SelectAll();
                    return;
                }

                if (CHNLSVC.Security.CheckPasswordPolicy(BaseCls.GlbUserID, _newPw, out  _err) == false)
                {
                    //MessageBox.Show("The password you entered doesn't meet the minimum security requirements!", "Change password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MessageBox.Show(_err, "Change password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewPassword.Focus();
                    txtNewPassword.SelectAll();
                    return;
                }

                if (MessageBox.Show("Are you sure to change password?", "Confirm Change Password", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                int _ref = UpdateSystemUser(BaseCls.GlbUserID, _newPw);
                if (_ref > 0)
                {
                    MessageBox.Show("Your password successfully changed!\nPlease sign in with your new password.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("Unable to change password!", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Int32 UpdateSystemUser(string _user, string _newPw)
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;
            // var _membershipUser = Membership.GetUser(userID);

            SystemUser _SystemUser = new SystemUser();
            _SystemUser = CHNLSVC.Security.GetUserByUserID(_user);
            _SystemUser.Se_usr_pw = _newPw;
            if (_SystemUser.Se_pw_mustchange == 1)
            {
                _SystemUser.Se_pw_mustchange = 0;
            }

            _SystemUser.Se_pw_chng_by = BaseCls.GlbUserID; //Add by Chamal 16-Sep-2013

            //using (TransactionScope tr = new TransactionScope()) TODO:
            //{
            //  _membershipUser.ChangePassword(_membershipUser.GetPassword(), _newPw); TODO:
            row_aff = CHNLSVC.Security.Change_Password(_SystemUser);

            //tr.Complete();
            //}
            return row_aff;
        }
        private bool checkValidUser()
        {
            SystemUser _systemaduser = new SystemUser();
            SystemUser _systemuser = null;
            _systemuser = CHNLSVC.Security.GetUserByUserID(txtNewUserName.Text.ToString().ToUpper());

            if (_systemuser == null)
            {
                string _msg = "Invalid user name!";
                MessageBox.Show(_msg, "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (!(_systemuser.Se_usr_id.ToUpper().ToString() == txtNewUserName.Text.ToUpper().ToString() && _systemuser.Se_usr_pw == txtCurrentPassword.Text.ToString()))
            {
                string _msg = "Invalid password!";
                MessageBox.Show(_msg, "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
              //  BaseCls.GlbUserName = _systemuser.Se_usr_name;
              //  BaseCls.GlbUserDeptID = _systemuser.Se_dept_id;
            }

            if (_systemuser.Se_act == 0)
            {
                string _msg = "In-avctive user!";
                MessageBox.Show(_msg, "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if ((_systemuser.Se_isdomain == 1) && (!CHNLSVC.Security.CheckValidADUser(_systemuser.Se_domain_id, out _systemaduser)))
            {
                string _msg = "Unsuccessful login. Your domain user id has been disabled.";
                MessageBox.Show(_msg, "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            List<MasterDepartment> _deptList = CHNLSVC.General.GetDepartment();
            if (_deptList != null)
            {
                var _lst = _deptList.Where(y => y.Msdt_cd == _systemuser.Se_dept_id).Select(x => x.Msdt_desc).ToList();
                if (_lst != null)
                {
                    if (_lst.Count > 0)
                    {
                        BaseCls.GlbUserDeptName = _lst[0].ToString();
                    }
                    else
                    {
                        string _msg = "Unsuccessful login. Your department not setup.";
                        MessageBox.Show(_msg, "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }
                else
                {
                    string _msg = "Unsuccessful login. Your department not setup.";
                    MessageBox.Show(_msg, "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }

            //Check System Version Chamal 07-02-2013
            string _newSystemVersionNo = CHNLSVC.Security.GetCurrentVersion().ToString();

            if (_newSystemVersionNo != BaseCls.GlbVersionNo)
            {
                if (string.IsNullOrEmpty(_newSystemVersionNo)) _newSystemVersionNo = "'UNKNOWN'";
                string _msg = "Your current system version is expired! \n New system version is " + _newSystemVersionNo + " \n Please download new system update or contact IT department.";
                MessageBox.Show(_msg, "System version", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
                return false;
            }

            return true;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //txtNewUserName.Text = "";
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmNewPassword.Text = "";
        }

        private void txtNewUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtNewUserName.Text.ToString().Trim()=="")
                {
                    MessageBox.Show("Please enter user name!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtNewUserName.Focus();
                    return;
                }
                SystemUser _systemaduser = new SystemUser();
                SystemUser _systemuser = null;
                _systemuser = CHNLSVC.Security.GetUserByUserID(txtNewUserName.Text.ToString().ToUpper());

                if (_systemuser == null)
                {
                    string _msg = "Invalid user name";
                    MessageBox.Show(_msg, "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtNewUserName.Focus();
                    return;
                }
                txtCurrentPassword.Focus();
            }
        }

        private void txtNewUserName_Leave(object sender, EventArgs e)
        {
            if (txtNewUserName.Text.ToString().Trim() == "")
            {
                return;
            }
            SystemUser _systemaduser = new SystemUser();
            SystemUser _systemuser = null;
            _systemuser = CHNLSVC.Security.GetUserByUserID(txtNewUserName.Text.ToString().ToUpper());

            if (_systemuser == null)
            {
                string _msg = "Invalid user name";
                MessageBox.Show(_msg, "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtNewUserName.Focus();
            }
        }

        private void txtCurrentPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtNewPassword.Focus();
            }
        }

        private void txtConfirmNewPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSubmit.Select();
            }
        }

        private void txtNewPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtConfirmNewPassword.Focus();
            }
        }
    }
}
