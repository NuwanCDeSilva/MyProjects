using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.ADMIN
{
    public partial class UserPasswordChange : BasePage
    {
        string userID, password, _userid, _companycode;

        public bool CheckPasswordComplexity(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            if (password.Length < 8) return false;

            int nonAlnumCount = 0;
            for (int i = 0; i < password.Length; i++)
            {
                if (!char.IsLetterOrDigit(password, i)) nonAlnumCount++;
            }

            if (nonAlnumCount < 1)
                return false;
            if (!Regex.IsMatch(password, "[\\d]+"))
            {
                return false;
            }
            return true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    _userid = (string)Session["UserID"];
                    txtNewUserName.Text = _userid;
                    CheckUserCanChangePassword();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private Int32 UpdateSystemUser(string _user, string _newPw)
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;

            SystemUser _SystemUser = new SystemUser();
            _SystemUser = CHNLSVC.Security.GetUserByUserID(_user);
            _SystemUser.Se_usr_pw = _newPw;
            if (_SystemUser.Se_pw_mustchange == 1)
            {
                _SystemUser.Se_pw_mustchange = 0;
            }

            _SystemUser.Se_pw_chng_by = Session["UserID"].ToString();

            row_aff = CHNLSVC.Security.Change_Password(_SystemUser);
            return row_aff;
        }

        private bool checkValidUser()
        {
            SystemUser _systemaduser = new SystemUser();
            SystemUser _systemuser = null;

            userID = txtNewUserName.Text.Trim().ToUpper();
            password = txtCurrentPassword.Text.Trim();
            string _newPw = txtNewPassword.Text.Trim();
            string _confirmPw = txtConfirmNewPassword.Text.Trim();
            string _err = string.Empty;

            _systemuser = CHNLSVC.Security.GetUserByUserID(txtNewUserName.Text.ToString().ToUpper());

            Div1.Visible = false;

            if ((_systemuser.Se_isdomain == 1) && (!CHNLSVC.Security.CheckValidADUser(_systemuser.Se_domain_id, out _systemaduser)))
            {
                Div1.Visible = true;
                Label1.Text = "Unsuccessful login. Your domain user id has been disabled !!!";
                return false;
            }

            if (_systemuser.Se_act == 0)
            {
                Div1.Visible = true;
                Label1.Text = "Inactive user !!!";
                return false;
            }

            if (_systemuser == null)
            {
                Div1.Visible = true;
                Label1.Text = "Invalid user name !!!";
                txtNewUserName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                Div1.Visible = true;
                Label1.Text = "Please enter the current password !!!";
                txtCurrentPassword.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                Div1.Visible = true;
                Label1.Text = "Please enter a new password !!!";
                txtNewPassword.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtConfirmNewPassword.Text.Trim()))
            {
                Div1.Visible = true;
                Label1.Text = "Please confirm new password !!!";
                txtConfirmNewPassword.Focus();
                return false;
            }

            if (_newPw != _confirmPw)
            {
                Div1.Visible = true;
                Label1.Text = "The passwords you typed do not match. Please try again !!!";
                txtNewPassword.Focus();
                return false;
            }

            if (_newPw.ToUpper() == txtCurrentPassword.Text.ToUpper())
            {
                _err = "Your new password was rejected because it was similar to your current password !!!";
                Div1.Visible = true;
                Label1.Text = _err;
                txtNewPassword.Focus();
                return false;
            }

            if (!(_systemuser.Se_usr_id.ToUpper().ToString() == txtNewUserName.Text.ToUpper().ToString() && _systemuser.Se_usr_pw == txtCurrentPassword.Text.ToString()))
            {
                Div1.Visible = true;
                Label1.Text = "Invalid password !!!";
                txtCurrentPassword.Focus();
                return false;
            }

            _userid = (string)Session["UserID"];
            if (CHNLSVC.Security.CheckPasswordPolicy(_userid, _newPw, out  _err) == false)
            {
                Div1.Visible = true;
                Label1.Text = _err;
                txtNewPassword.Focus();
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
                        Div1.Visible = true;
                        Label1.Text = "Unsuccessful login. Your department not setup !!!";
                        return false;
                    }
                }
                else
                {
                    Div1.Visible = true;
                    Label1.Text = "Unsuccessful login. Your department not setup !!!";
                    return false;
                }
            }

            string _newSystemVersionNo = CHNLSVC.Security.GetCurrentVersion().ToString();

            if (_newSystemVersionNo != BaseCls.GlbVersionNo)
            {
                if (string.IsNullOrEmpty(_newSystemVersionNo)) _newSystemVersionNo = "'UNKNOWN'";
                string _msg = "Your current system version is expired! New system version is " + _newSystemVersionNo + " Please download new system update or contact IT department.";
                Div1.Visible = true;
                Label1.Text = _msg;
                return false;
            }
            return true;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            if (txtconfirmclear.Value == "Yes")
            {
                try
                {
                    txtCurrentPassword.Text = string.Empty;
                    txtNewPassword.Text = string.Empty;
                    txtConfirmNewPassword.Text = string.Empty;
                    txtCurrentPassword.Attributes.Add("value", "");
                    txtNewPassword.Attributes.Add("value", "");
                    txtConfirmNewPassword.Attributes.Add("value", "");
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
            }
            else
            {
                string curpw = txtCurrentPassword.Text;
                txtCurrentPassword.Attributes.Add("value", curpw);

                string newpw =txtNewPassword.Text;
                txtNewPassword.Attributes.Add("value", newpw);

                string newconfpw = txtNewPassword.Text;
                txtConfirmNewPassword.Attributes.Add("value", newconfpw);
            }
            txtconfirmclear.Value = "";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            _userid = (string)Session["UserID"];
            _companycode = (string)Session["UserCompanyCode"];

            if (txtconformmessageValue.Value == "Yes")
            {
                DivAsk.Visible = false;
                Div1.Visible = false;
                Divinfo.Visible = false;
                try
                {
                    if (txtNewUserName.Text.ToString().Trim() == "")
                    {
                        Div1.Visible = true;
                        Label1.Text = "Please enter user name !!!";
                        txtNewUserName.Focus();
                        return;
                    }

                    txtCurrentPassword.Focus();

                    string _err = string.Empty;

                    bool isvaliduser = checkValidUser();
                    if (isvaliduser == false)
                    {
                        return;
                    }

                    _userid = (string)Session["UserID"];
                    _companycode = (string)Session["UserCompanyCode"];

                    int _ref = UpdateSystemUser(_userid, txtNewPassword.Text.Trim());
                    if (_ref > 0)
                    {
                        DivAsk.Visible = true;
                        Div1.Visible = false;
                        lblAsk.Text = "Your password successfully changed!.Please sign in with your new password !!!";
                    }
                    else
                    {
                        Div1.Visible = true;
                        Label1.Text = "Unable to change password !!!";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
                finally
                {
                    CHNLSVC.CloseChannel();
                    CHNLSVC.Security.ExitLoginSession(_userid, _companycode, "-1");
                }
            }
        }

        protected void lbtColse_Click(object sender, EventArgs e)
        {
            DivAsk.Visible = false;
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Div1.Visible = false;
        }

        private void CheckUserCanChangePassword()
        {
            try
            {
                Divinfo.Visible = false;
                string value = string.Empty;
                DataTable dtpwstatus = CHNLSVC.Security.SP_SCM2_GET_USRPW_STATUS(Session["UserID"].ToString());
                if (dtpwstatus.Rows.Count > 0)
                {
                    foreach (DataRow item in dtpwstatus.Rows)
                    {
                        value = item[1].ToString();
                    }
                }
                if (value == "0")
                {
                    Divinfo.Visible = true;
                    lblinfo.Text = "You are not allowed to change your password";
                    lbtnSubmit.Visible = false;

                    txtCurrentPassword.Enabled = false;
                    txtNewPassword.Enabled = false;
                    txtConfirmNewPassword.Enabled = false;
                }
                else
                {
                    lbtnSubmit.Visible = true;
                    Divinfo.Visible = false;

                    txtCurrentPassword.Enabled = true;
                    txtNewPassword.Enabled = true;
                    txtConfirmNewPassword.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Divinfo.Visible = false;
        }
    }
}