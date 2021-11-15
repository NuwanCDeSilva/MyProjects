using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using FF.BusinessObjects;
using System.Net;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace FF.WindowsERPClient
{
    public partial class Login : Base
    {
        private int _loginRetryInCounter = 0;
        private int _loginRetryOutCounter = 0;
        public Boolean _isLog = false;
        public Login()
        {
            InitializeComponent();
            splitContChangePw.Visible = false;
            splitContLogin.Visible = true;
            if (System.Configuration.ConfigurationManager.AppSettings["SystemType"].ToString() == "1")
            {
                lblTestVersion.Visible = true;
                lblTestVersion.Text = "*** Test Version - I ***";
                lblTestVersion.ForeColor = Color.Crimson;
            }
            if (System.Configuration.ConfigurationManager.AppSettings["SystemType"].ToString() == "2")
            {
                lblTestVersion.Visible = true;
                lblTestVersion.Text = "*** Test Version - Parallel ***";
                lblTestVersion.ForeColor = Color.MediumVioletRed;
            }
            if (System.Configuration.ConfigurationManager.AppSettings["SystemType"].ToString() == "3")
            {
                lblTestVersion.Visible = true;
                lblTestVersion.Text = "- Version - II -";
                lblTestVersion.ForeColor = Color.BlueViolet;
            }
            if (System.Configuration.ConfigurationManager.AppSettings["SystemType"].ToString() == "4")
            {
                lblTestVersion.Visible = true;
                lblTestVersion.Text = "- Singhagiri -";
                lblTestVersion.ForeColor = Color.MediumBlue;
            }

            lblVersionNo.Text = "Version - " + BaseCls.GlbVersionNo;
            txtUser.Focus();
            txtUser.Select();
        }

        #region Load Session Variables
        protected void Load_Session_Variables()
        {
            List<SystemUserProf> _userprofs = CHNLSVC.Security.GetUserProfCenters(BaseCls.GlbUserID, cmbCompany.Text.ToString());
            if (_userprofs != null)
            {
                var _userprofQuery =
                from userProf in _userprofs
                where userProf.Sup_usr_id == BaseCls.GlbUserID && userProf.Sup_com_cd == cmbCompany.Text.ToString() && userProf.Sup_def_pccd == true
                select userProf;

                foreach (var _userprof in _userprofQuery)
                {
                    BaseCls.GlbUserDefProf = _userprof.Sup_pc_cd.ToString().ToUpper();
                    break;
                }
            }

            List<SystemUserLoc> _userLocas = CHNLSVC.Security.GetUserLoc(BaseCls.GlbUserID, cmbCompany.Text.ToString());
            if (_userLocas != null)
            {
                var _userLocaQuery =
                    from userLoca in _userLocas
                    where userLoca.SEL_USR_ID == BaseCls.GlbUserID && userLoca.SEL_COM_CD == cmbCompany.Text.ToString() && userLoca.SEL_DEF_LOCCD == 1
                    select userLoca;

                foreach (var _userLoca in _userLocaQuery)
                {
                    BaseCls.GlbUserDefLoca = _userLoca.SEL_LOC_CD.ToString().ToUpper();
                    break;
                }
            }
            CHNLSVC.CloseAllChannels();
        }

        public void BindUserCompanyDDLData(ComboBox ddl, String username)
        {
            ddl.DataSource = new List<SystemUserCompany>();

            if (!string.IsNullOrEmpty(username))
            {
                string intime = DateTime.Now.ToString();
                List<SystemUserCompany> _systemUserCompanyList = CHNLSVC.Security.GetUserCompany(username);

                if (_systemUserCompanyList != null)
                {
                    var _lst = _systemUserCompanyList.Select(x => x.SEC_COM_CD).ToList();
                    //_lst.Add("--Select--");
                    ddl.DataSource = _lst;
                }
            }
            CHNLSVC.CloseAllChannels();
        }

        #endregion

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUser_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //txtUser.Text = "ADMIN";
                txtPw.Focus();
            }

        }

        private void txtPw_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //txtPw.Text = "ADMIN@123";
                cmbCompany.Focus();
            }
        }

        private void cmbCompany_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //cmbCompany.Items.Add("ABL");
                //cmbCompany.SelectedIndex = 0;
                btnLogin.Focus();
            }
        }

        #region Login
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login_Process();

            //try
            //{
            //    bool _pwMustChg = false; //Chamal 16-Sep-2013
            //    bool _pwExpireMustChg = false; //Chamal 21-Sep-2013
            //    bool _pwExpire = false;
            //    int _remainingDays = -1;
            //    if (!string.IsNullOrEmpty(txtUser.Text) && !string.IsNullOrEmpty(txtPw.Text) && !string.IsNullOrEmpty(cmbCompany.Text))
            //    {

            //        if (CHNLSVC.Security.GetServerDateTime().Date != DateTime.Now.Date)
            //        {
            //            MessageBox.Show("Your system date is not equal to server date! \nPlease contact system administrator....", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            Application.Exit();
            //            return;
            //        }

            //        CultureInfo ci = CultureInfo.CurrentCulture;
            //        if (ci.DateTimeFormat.ShortDatePattern.ToString().ToUpper() != "DD/MMM/YYYY")
            //        {
            //            MessageBox.Show("Your system date format is incorrect! \nPlease contact system administrator....", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            Application.Exit();
            //            return;
            //        }

            //        string _msg = string.Empty;
            //        if (_loginRetryInCounter < 2)
            //        {

            //            SystemUser _systemaduser = new SystemUser();
            //            SystemUser _systemuser = null;
            //            _systemuser = CHNLSVC.Security.GetUserByUserID(txtUser.Text.ToString().ToUpper());

            //            if (_systemuser == null)
            //            {
            //                _loginRetryInCounter++; // increment retry counter
            //                txtPw.Clear();
            //                txtPw.Focus();
            //                _msg = "Unsuccessful login.  Please contact your system administrator.";
            //                MessageBox.Show(_msg, "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                return;
            //            }

            //            if (!(_systemuser.Se_usr_id.ToUpper().ToString() == txtUser.Text.ToUpper().ToString() && _systemuser.Se_usr_pw == txtPw.Text.ToString()))
            //            {
            //                _loginRetryInCounter++; // increment retry counter
            //                txtPw.Clear();
            //                txtPw.Focus();
            //                _msg = "Invalid user name or password entering.";
            //                MessageBox.Show(_msg, "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                return;
            //            }
            //            else
            //            {
            //                BaseCls.GlbUserName = _systemuser.Se_usr_name;
            //                BaseCls.GlbUserDeptID = _systemuser.Se_dept_id;
            //                BaseCls.GlbUserCategory = _systemuser.Se_usr_cat;
            //                if (_systemuser.Se_pw_mustchange == 1) _pwMustChg = true;
            //                if (_systemuser.Se_pw_expire == 1)
            //                {
            //                    _pwExpire = true;
            //                    int _daysDiff = _systemuser.Se_noofdays - (DateTime.Now.Date - _systemuser.Se_pw_chng_dt.Date).Days;
            //                    if (_daysDiff < 0)
            //                    {
            //                        _remainingDays = _daysDiff;
            //                        _pwExpireMustChg = true; 
            //                    }
            //                    else
            //                    {
            //                        if (_systemuser.Se_mindays >= _daysDiff) _remainingDays = _daysDiff;
            //                    }
            //                }
            //            }

            //            if (_systemuser.Se_act == 0)
            //            {
            //                _loginRetryInCounter++; // increment retry counter
            //                txtPw.Clear();
            //                txtPw.Focus();
            //                _msg = "Invalid user name or password entering.";
            //                MessageBox.Show(_msg, "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                return;
            //            }
            //            if ((_systemuser.Se_isdomain == 1) && (!CHNLSVC.Security.CheckValidADUser(_systemuser.Se_domain_id, out _systemaduser)))
            //            {
            //                _loginRetryInCounter++; // increment retry counter
            //                txtPw.Clear();
            //                txtPw.Focus();
            //                _msg = "Unsuccessful login. Your domain user id has been disable.";
            //                MessageBox.Show(_msg, "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                return;
            //            }

            //            List<MasterDepartment> _deptList = CHNLSVC.General.GetDepartment();
            //            if (_deptList != null)
            //            {
            //                var _lst = _deptList.Where(y => y.Msdt_cd == _systemuser.Se_dept_id).Select(x => x.Msdt_desc).ToList();
            //                if (_lst != null)
            //                {
            //                    if (_lst.Count > 0)
            //                    {
            //                        BaseCls.GlbUserDeptName = _lst[0].ToString();
            //                    }
            //                    else
            //                    {
            //                        _loginRetryInCounter++; // increment retry counter
            //                        txtPw.Clear();
            //                        txtPw.Focus();
            //                        _msg = "Unsuccessful login. Your department not setup.";
            //                        MessageBox.Show(_msg, "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                        return;
            //                    }
            //                }
            //                else
            //                {
            //                    _loginRetryInCounter++; // increment retry counter
            //                    txtPw.Clear();
            //                    txtPw.Focus();
            //                    _msg = "Unsuccessful login. Your department not setup.";
            //                    MessageBox.Show(_msg, "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                    return;
            //                }
            //            }

            //            //Check System Version Chamal 07-02-2013
            //            string _newSystemVersionNo = CHNLSVC.Security.GetCurrentVersion().ToString();

            //            if (_newSystemVersionNo != BaseCls.GlbVersionNo)
            //            {
            //                if (string.IsNullOrEmpty(_newSystemVersionNo)) _newSystemVersionNo = "'UNKNOWN'";
            //                _msg = "Your current system version is expired! \n New system version is " + _newSystemVersionNo + " \n Please download new system update or contact IT department.";
            //                MessageBox.Show(_msg, "System version", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                Application.Exit();
            //                return;
            //            }
            //        }
            //        else
            //        {
            //            _msg = "You have failed to remember your details. \nContact Administration for further instructions";
            //            MessageBox.Show(_msg, "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //            Application.Exit();
            //            return;
            //        }
            //        string _ipAdd = string.Empty;
            //        string _pcName = string.Empty;
            //        string _loginTime = string.Empty;

            //        if (txtUser.Text.ToString().ToUpper() != "ADMIN")
            //        {
            //            if (CHNLSVC.Security.IsActiveSessions(txtUser.Text.ToString().ToUpper(), cmbCompany.Text.ToString().ToUpper(), out _ipAdd, out _pcName, out _loginTime) == true)
            //            {
            //                _msg = "Active session is exist for the user '" + txtUser.Text.ToString() + "'" + "\n \n * Computer Name       :  " + _pcName + "\n * Computer IP              :  " + _ipAdd + "\n * Logged on                 :  " + _loginTime + "\n\nDo you want to close the existing session and log to system?";
            //                if (MessageBox.Show(_msg, "Fast Forward - SCM-II", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
            //                {
            //                    CHNLSVC.Security.ExitLoginSession(txtUser.Text.ToString().ToUpper(), cmbCompany.Text.ToString().ToUpper(), "-1");
            //                }
            //                else
            //                {
            //                    Application.Exit();
            //                    return;
            //                }
            //            }
            //        }

            //        BaseCls.GlbUserID = txtUser.Text.ToString().ToUpper();
            //        BaseCls.GlbUserComCode = cmbCompany.Text.ToString().ToUpper();
            //        BaseCls.GlbUserIP = GetIP();

            //        Load_Session_Variables();
            //        CheckLocation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);//Add by Chamal 28-Aug-2013
            //        CheckProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);//Add by Chamal 28-Aug-2013
            //        LoadProfitCenterDetail();
            //        LoadLocationDetail();
            //        LoadCompanyDetail(); //Added by Prabhath on 11/06/2013

            //        BaseCls.GlbUserSessionID = Convert.ToString(CHNLSVC.Security.SaveLoginSession(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserIP, BaseCls.GlbHostName));

            //        if (_pwMustChg == true)
            //        {
            //            _msg = "The user's password must be changed before logging on the this time.";
            //            MessageBox.Show(_msg, "Change password", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //            splitContChangePw.Visible = true;
            //            splitContLogin.Visible = false;
            //            txtNewPw.Clear();
            //            txtConfirmPw.Clear();
            //            txtNewPw.Focus();
            //            txtNewPw.Select();
            //            return;
            //        }

            //        if (txtUser.Text.ToString().ToUpper() == "1800019")
            //        {
            //            if (_pwExpire == true)
            //            {
            //                if (_remainingDays > 0)
            //                {
            //                    _msg = "Your password will expire in " + _remainingDays + " days. Would you like to change it now?";
            //                    if (MessageBox.Show(_msg, "Expire password", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            //                    {
            //                        splitContChangePw.Visible = true;
            //                        splitContLogin.Visible = false;
            //                        txtNewPw.Clear();
            //                        txtConfirmPw.Clear();
            //                        txtNewPw.Focus();
            //                        txtNewPw.Select();
            //                        return;
            //                    }
            //                }
            //                else if (_remainingDays == 0)
            //                {
            //                    _msg = "Your password expires today. Would you like to change it now?";
            //                    if (MessageBox.Show(_msg, "Expire password", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            //                    {
            //                        splitContChangePw.Visible = true;
            //                        splitContLogin.Visible = false;
            //                        txtNewPw.Clear();
            //                        txtConfirmPw.Clear();
            //                        txtNewPw.Focus();
            //                        txtNewPw.Select();
            //                        return;
            //                    }
            //                }

            //                if (_pwExpireMustChg ==true) 
            //                {
            //                    _msg = "Your password has expired and must be changed!";
            //                    MessageBox.Show(_msg, "Expire password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    splitContChangePw.Visible = true;
            //                    splitContLogin.Visible = false;
            //                    txtNewPw.Clear();
            //                    txtConfirmPw.Clear();
            //                    txtNewPw.Focus();
            //                    txtNewPw.Select();
            //                    return;
            //                }
            //            }
            //        }
            //        this.Hide();
            //        MainMenu _mainMenu = new MainMenu();
            //        _mainMenu.Show();
            //        _isLog = true; 
            //    }

            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show(err.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    CHNLSVC.CloseChannel();
            //    GC.Collect();
            //}
        }
        #endregion

        private void txtUser_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUser.Text))
                {
                    BindUserCompanyDDLData(cmbCompany, txtUser.Text.ToUpper().ToString());
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //private string GetIP()
        //{
        //    string strHostName = "";
        //    strHostName = System.Net.Dns.GetHostName();

        //    BaseCls.GlbHostName = strHostName;

        //    IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

        //    IPAddress[] addr = ipEntry.AddressList;
        //    return addr[addr.Length - 1].ToString();

        //}

        //Edit by Chamal 22/05/2013 (InterNetworkV6, InterNetwork)
        public string GetIP()
        {
            IPHostEntry host;
            string localIP = "";
            BaseCls.GlbHostName = Dns.GetHostName();
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //cmbCompany.Items.Add("--Select--");
            //cmbCompany.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //txtUser.Clear();
            //txtPw.Clear();
            //BindUserCompanyDDLData(cmbCompany, string.Empty);
            //txtUser.Focus();
            Application.Exit();

            //Login_Process();
        }

        #region Reset Password
        private void txtNewPw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtConfirmPw.Focus();
            }
        }

        private void txtConfirmPw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnResetLogin.Focus();
            }
        }

        private void btnResetLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string _err = string.Empty;
                string _newPw = txtNewPw.Text.Trim();
                string _confirmPw = txtConfirmPw.Text.Trim();

                if (_newPw != _confirmPw)
                {
                    MessageBox.Show("The passwords you typed don't match. Please try again!", "Change password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewPw.Focus();
                    txtNewPw.SelectAll();
                    return;
                }

                if (_newPw.ToUpper() == txtPw.Text.ToUpper())
                {
                    _err = "Your new password was rejected because it does not meet the minimum security requirements.\n";
                    _err = _err + "\n" + "Your password was rejected because it:";
                    _err = _err + "\n" + "- was similar to your current password";

                    MessageBox.Show(_err, "Change password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewPw.Focus();
                    txtNewPw.SelectAll();
                    return;
                }

                if (CHNLSVC.Security.CheckPasswordPolicy(BaseCls.GlbUserID, _newPw, out  _err) == false)
                {
                    //MessageBox.Show("The password you entered doesn't meet the minimum security requirements!", "Change password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MessageBox.Show(_err, "Change password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewPw.Focus();
                    txtNewPw.SelectAll();
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                Int32 _ref = UpdateSystemUser(BaseCls.GlbUserID, _newPw);

                if (_ref > 0)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Your password has been changed!", "Change password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    MainMenu _mainMenu = new MainMenu();
                    _mainMenu.Show();
                    _isLog = true;
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Unable to change password! Try later...", "Change password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private Int32 UpdateSystemUser(string _user, string _newPw)
        {
            SystemUser _SystemUser = new SystemUser();
            _SystemUser = CHNLSVC.Security.GetUserByUserID(_user);
            _SystemUser.Se_usr_pw = _newPw;
            _SystemUser.Se_pw_chng_by = _user;
            if (_SystemUser.Se_pw_mustchange == 1) _SystemUser.Se_pw_mustchange = 0;
            return CHNLSVC.Security.Change_Password(_SystemUser);
        }

        private void btnResetCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Login Process
        private void Login_Process()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUser.Text) && !string.IsNullOrEmpty(txtPw.Text))
                {
                    if (cmbCompany.Items.Count <= 0)
                    {
                        MessageBox.Show("Your have not assign any companies to login.", "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        Application.Exit();
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(txtUser.Text) && !string.IsNullOrEmpty(txtPw.Text) && !string.IsNullOrEmpty(cmbCompany.Text))
                {

                    if (CheckServerDateTime() == false)
                    {
                        Application.Exit();
                        return;
                    }

                    CultureInfo ci = CultureInfo.CurrentCulture;
                    if (ci.DateTimeFormat.ShortDatePattern.ToString().ToUpper() != "DD/MMM/YYYY")
                    {
                        MessageBox.Show("Your system date format is incorrect! \nPlease contact system administrator....", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Exit();
                        return;
                    }

                    string _msg = string.Empty;
                    string _msgTitle = string.Empty;
                    int _msgStatus = 0;

                    BaseCls.GlbUserID = txtUser.Text.ToString().ToUpper();
                    BaseCls.GlbUserComCode = cmbCompany.Text.ToString().ToUpper();
                    BaseCls.GlbUserIP = GetIP();
                    string _WindowsLogonName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    string _WindowsUser = Environment.UserName;

                    LoginUser _loginUser = CHNLSVC.Security.LoginToSystem(txtUser.Text.ToString().ToUpper(), txtPw.Text.ToString(), cmbCompany.Text.ToString().ToUpper(), BaseCls.GlbVersionNo, BaseCls.GlbUserIP, BaseCls.GlbHostName, _loginRetryInCounter, out _loginRetryOutCounter, out _msgStatus, out _msg, out _msgTitle);
                    if (_msgStatus == -1)
                    {
                        if (_loginUser.Login_attempts >= _loginRetryOutCounter)
                        {
                            _loginRetryInCounter = _loginRetryOutCounter;
                            txtPw.Clear();
                            txtPw.Focus();
                            MessageBox.Show(_msg, _msgTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        else
                        {
                            CHNLSVC.Security.Save_User_Falis(txtUser.Text.ToString(), txtPw.Text.ToString(), BaseCls.GlbUserComCode.ToString(), BaseCls.GlbUserIP.ToString(), _WindowsLogonName, _WindowsUser);   
                            _msg = "You have failed to remember your details. \nContact Administration for further instructions";
                            MessageBox.Show(_msg, "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            Application.Exit();
                            return;
                        }
                    }
                    else if (_msgStatus == -2)
                    {
                        _loginRetryInCounter = _loginRetryOutCounter;
                        MessageBox.Show(_msg, _msgTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        Application.Exit();
                        return;
                    }
                    else if (_msgStatus == 1 || _msgStatus == -3)
                    {
                        string _ipAdd = string.Empty;
                        string _pcName = string.Empty;
                        string _loginTime = string.Empty;

                        if (txtUser.Text.ToString().ToUpper() != "ADMIN")
                        {
                            if (CHNLSVC.Security.IsActiveSessions(txtUser.Text.ToString().ToUpper(), cmbCompany.Text.ToString().ToUpper(), out _ipAdd, out _pcName, out _loginTime) == true)
                            {
                                _msg = "Active session is exist for the user '" + txtUser.Text.ToString() + "'" + "\n \n * Computer Name       :  " + _pcName + "\n * Computer IP              :  " + _ipAdd + "\n * Logged on                 :  " + _loginTime + "\n\nDo you want to close the existing session and log to system?";
                                if (MessageBox.Show(_msg, "Fast Forward - SCM-II", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
                                {
                                    CHNLSVC.Security.ExitLoginSession(txtUser.Text.ToString().ToUpper(), cmbCompany.Text.ToString().ToUpper(), "-1");
                                }
                                else
                                {
                                    Application.Exit();
                                    return;
                                }
                            }
                        }

                        SetLoginCacheLayer(_loginUser,_WindowsLogonName,_WindowsUser);

                        if (_loginUser.Pw_must_change == true)
                        {
                            _msg = "The user's password must be changed before logging on the this time.";
                            MessageBox.Show(_msg, _msgTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            ShowChangePasswordPanel();
                            return;
                        }

                        //if (txtUser.Text.ToString().ToUpper() == "1800019")
                        //{
                        if (_loginUser.Pw_expire == true)
                        {
                            if (_loginUser.Remaining_days > 0)
                            {
                                _msg = "Your password will expire in " + _loginUser.Remaining_days.ToString() + " days. Would you like to change it now?";
                                if (MessageBox.Show(_msg, "Expire password", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                {
                                    ShowChangePasswordPanel();
                                    return;
                                }
                            }
                            else if (_loginUser.Remaining_days == 0)
                            {
                                _msg = "Your password expires today. Would you like to change it now?";
                                if (MessageBox.Show(_msg, "Expire password", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                {
                                    ShowChangePasswordPanel();
                                    return;
                                }
                            }

                            if (_loginUser.Pw_expire_must_change == true)
                            {
                                _msg = "Your password has expired and must be changed!";
                                MessageBox.Show(_msg, "Expire password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ShowChangePasswordPanel();
                                return;
                            }
                        }
                        //}

                        this.Hide();
                        MainMenu _mainMenu = new MainMenu();
                        _mainMenu.Show();
                        _isLog = true;
                    }
                    else if (_msgStatus == -4)
                    {
                        MessageBox.Show(_msg, _msgTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        Application.Exit();
                        return;
                    }
                    else if (_msgStatus == -99)
                    {
                        MessageBox.Show(_msg, _msgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseChannel();
                GC.Collect();
            }
        }

        private void ShowChangePasswordPanel()
        {
            splitContChangePw.Visible = true;
            splitContLogin.Visible = false;
            txtNewPw.Clear();
            txtConfirmPw.Clear();
            txtNewPw.Focus();
            txtNewPw.Select();
        }
        #endregion

    }
}
