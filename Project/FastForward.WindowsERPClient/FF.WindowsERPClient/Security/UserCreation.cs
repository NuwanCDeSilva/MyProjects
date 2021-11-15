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
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace FF.WindowsERPClient.Security
{
    //pkg_search.sp_SearchAllRoles  =NEW
    //pkg_search.sp_Search_systemUser  =NEW
    //pkg_search.sp_getuser_category  =NEW

    //sp_save_sec_user_perm
    //sp_getuserloc =UPDATE

    //--------------approve permission------------------
    //pkg_search.sp_SearchApprPermissionLevels =NEW
    //pkg_search.sp_SearchApprovePermission  =NEW
    //sp_save_sec_app_usr_prem =NEW
    //sp_GetApprovePermission  =new
    //sp_Get_Sec_app_usr_prem =NEW
    //sp_update_user_role_NEW
    //SP_GETUSERROLE_NEW =NEW
    //sp_del_user_role_NEW =NEW
    //--------------------------------------------------

    public partial class UserCreation : Base
    {
        string Select_company = "";
        public UserCreation()
        {
            InitializeComponent();
            ucLoactionSearch1.Company = BaseCls.GlbUserComCode;
            ucProfitCenterSearch1.Company = BaseCls.GlbUserComCode;
            txtID.Focus();

        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");


            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SystemRole:
                    {
                        paramsText.Append(txtCom_R.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SystemUser:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SecUsrPermTp:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Designation:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Department:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(txtCom_S.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append(txtCom_S.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserRole:
                    {
                        paramsText.Append(Select_company + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ApprovePermCode:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ApprovePermLevelCode:
                    {
                        paramsText.Append(txtAppr_Code.Text.Trim() + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                UserCreation formnew = new UserCreation();
                formnew.MdiParent = this.MdiParent;
                formnew.Location = this.Location;
                formnew.Show();
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
                txtID.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        bool CheckSpaces(string input)
        {
            string regex = @"^[\S]*$";
            bool isMatch = false;
            isMatch = Regex.IsMatch(input, regex);
            return isMatch;
        }

        private void btnUpdateAdvnDet_Click(object sender, EventArgs e)
        {
            try
            {
                //check user ID for empty
                if (txtID.Text == string.Empty || txtID.Text == "")
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the user ID!");
                    MessageBox.Show("Please select the user ID first!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtID.Focus();
                    return;
                }

                //check user name for empty
                if (txtName.Text == string.Empty || txtName.Text == "")
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the user name!");
                    MessageBox.Show("Please select the user name!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                //check description for empty
                if (txtDescription.Text == string.Empty || txtDescription.Text == "")
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter user description!");
                    MessageBox.Show("Please enter user description!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDescription.Focus();
                    return;
                }

                //check email for empty
                if (txtEMail.Text == string.Empty || txtEMail.Text == "")
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter user email!");
                    MessageBox.Show("Please enter user email!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEMail.Focus();
                    return;
                }

                #region KPMG - 6.8 System Administration Module Issues :: edit by Chamal 21/07/2014
                if (string.IsNullOrEmpty(txtMobile.Text))
                {
                    MessageBox.Show("Please enter user mobile no!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMobile.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPhone.Text))
                {
                    MessageBox.Show("Please enter user phone no!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtEmpID.Text))
                {
                    MessageBox.Show("Please enter user employee no(EPF)!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmpID.Focus();
                    return;
                }


                if (txtEmpID.Text.Length != 7)
                {
                    MessageBox.Show("Employee no should seven(7) characters!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmpID.Focus();
                    return;
                }

                if (IsNumeric(txtEmpID.Text) == false)
                {
                    MessageBox.Show("Invalid employee no(EPF)!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmpID.Select();
                    txtEmpID.Focus();
                    return;
                }

                if (CheckSpaces(txtID.Text) == false)
                {
                    MessageBox.Show("Invalid character(s) in user id!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtID.SelectAll();
                    txtID.Focus();
                    return;
                }

                if (CheckSpaces(txtMobile.Text) == false)
                {
                    MessageBox.Show("Invalid character(s) in mobile no!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMobile.SelectAll();
                    txtMobile.Focus();
                    return;
                }

                if (CheckSpaces(txtPhone.Text) == false)
                {
                    MessageBox.Show("Invalid character(s) in phone no!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.SelectAll();
                    txtPhone.Focus();
                    return;
                }

                if (CheckSpaces(txtEmpID.Text) == false)
                {
                    MessageBox.Show("Invalid character(s) in user employee no(EPF)!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmpID.SelectAll();
                    txtEmpID.Focus();
                    return;
                }

                if (IsNumeric(txtMobile.Text) == false)
                {
                    MessageBox.Show("Invalid mobile no!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMobile.Select();
                    txtMobile.Focus();
                    return;
                }

                if (IsNumeric(txtPhone.Text) == false)
                {
                    MessageBox.Show("Invalid phone no!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Select();
                    txtPhone.Focus();
                    return;
                }
                #endregion

                //TODO: VALIDATE email -  check @ sigh in email

                //check is domain then domain id exsist
                if (chkIsDomain.Checked == true)
                {
                    if (txtDomainID.Text == string.Empty || txtDomainID.Text == "")
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter domain id!");
                        MessageBox.Show("Please enter domain ID!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtDomainID.Focus();
                        return;
                    }
                }

                // check pw exprire is set then pw valid period
                int outputvalue = 0;
                bool isNumber = false;

                if (chkPWExpire.Checked == true)
                {
                    if (txtValid.Text == string.Empty || txtValid.Text == "")
                    {
                        // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter PW valid period!");
                        MessageBox.Show("Please enter PW valid period!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtValid.Focus();
                        return;
                    }
                    else
                    {
                        isNumber = int.TryParse(txtValid.Text, out outputvalue);
                        {
                            if (!isNumber)
                            {
                                // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "PW valid period should be numeric!");
                                MessageBox.Show("PW valid period should be numeric!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtValid.Text = "";
                                txtValid.Focus();
                                return;
                            }
                        }
                    }
                }

                //Validate enter password
                if (txtPW.Text != txtCPW.Text)
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Password validation is fail!");
                    MessageBox.Show("Password validation is fail!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCPW.Text = "";
                    txtCPW.Focus();
                    return;
                }

                //Check Pw valid period
                if (string.IsNullOrEmpty(txtValid.Text))
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter password valid period!");
                    MessageBox.Show("Please enter password valid period!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtValid.Text = "0";
                    txtValid.Focus();
                    return;
                }

                //Check user category select status
                if (txtCate.Text.Trim() == "")
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select user designation!");
                    MessageBox.Show("Please enter password valid period!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCate.Focus();
                    return;
                }

                //Check user department
                if (txtDept.Text.Trim() == "")
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select user department!");
                    MessageBox.Show("Please enter password valid period!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDept.Focus();
                    return;
                }

                UpdateSystemUser();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        private void UpdateSystemUser()
        {
            Int32 row_aff = 0;
            string _userId = string.Empty;
            string _msg = string.Empty;
            //   MembershipCreateStatus result;   ????????????

            try
            {

                SystemUser _SystemUser = new SystemUser();
                _SystemUser.Se_usr_id = txtID.Text;
                _SystemUser.Se_usr_name = txtName.Text;
                _SystemUser.Se_usr_desc = txtDescription.Text;
                _SystemUser.Se_usr_pw = txtPW.Text;
                _SystemUser.Se_usr_cat = txtCate.Text.Trim();
                _SystemUser.Se_dept_id = txtDept.Text.Trim();
                _SystemUser.Se_emp_id = txtEmpID.Text;
                _SystemUser.Se_isdomain = (chkIsDomain.Checked == true) ? 1 : 0;
                _SystemUser.Se_iswinauthend = (chkIsWinAuth.Checked == true) ? 1 : 0;
                _SystemUser.Se_domain_id = txtDomainID.Text;
                _SystemUser.Se_noofdays = Convert.ToInt16(txtValid.Text);
                _SystemUser.Se_ischange_pw = (chkPWChange.Checked == true) ? 1 : 0;
                _SystemUser.Se_pw_mustchange = (chkMustChange.Checked == true) ? 1 : 0;
                //_SystemUser.Se_act = (chkStatus.Checked == true) ? 1 : 0;
                //Edit by Chamal 09-Jun-2014
                if (optLock.Checked == true)
                { MessageBox.Show("Can't update the user account as lock status!", "User account status", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                else if (optInactive.Checked == true)
                { _SystemUser.Se_act = 0; }
                else if (optActive.Checked == true)
                { _SystemUser.Se_act = 1; }
                else if (optDisable.Checked == true)
                {
                    if (MessageBox.Show("User update with DISABLE status!\nPlease confirm?\n\nNote-\nAfter update the user account as DISABLE, your never activate again.", "User account disable", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.No)
                    {
                        return;
                    }
                    if (string.IsNullOrEmpty(txtDisableRmks.Text))
                    {
                        MessageBox.Show("Please enter the resons for disable this user account.", "User account disable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtDisableRmks.Focus();
                        return;
                    }
                    _SystemUser.Se_act = -2;
                }
                else
                { _SystemUser.Se_act = 0; }
                _SystemUser.Se_pw_expire = (chkPWExpire.Checked == true) ? 1 : 0;
                _SystemUser.se_Email = txtEMail.Text;
                _SystemUser.se_Mob = txtMobile.Text;
                _SystemUser.se_Phone = txtPhone.Text;
                _SystemUser.Se_cre_by = BaseCls.GlbUserID;
                _SystemUser.Se_mod_by = BaseCls.GlbUserID;
                _SystemUser.Se_session_id = BaseCls.GlbUserSessionID;
                _SystemUser.Se_SUN_ID = txtSunUserID.Text; //Add by Chamal 16-Sep-2013

                _SystemUser.Se_emp_cd = txtEmpCode.Text; // Add by Tharaka 2015-02-24

                //using (TransactionScope tr = new TransactionScope())
                //{

                //    ////Membership.DeleteUser(txtID.Text);
                //    ////MembershipUser _memberShipUser = Membership.CreateUser(txtID.Text, txtPW.Text, txtEMail.Text, "Not Applicable", "Not Applicable", true, out result);

                //    ////switch (result)
                //    ////{
                //    ////   case MembershipCreateStatus.Success:
                //    ////        {
                //    ////_userId = _memberShipUser.UserName;
                //    row_aff = CHNLSVC.Security.UpdateUser(_SystemUser);
                //    tr.Complete();
                //    ////break;
                //    ////}

                //    ////case MembershipCreateStatus.InvalidUserName:
                //    ////    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "The invalid Username.  Please enter a valid username.");
                //    ////    _msg = "The invalid Username.  Please enter a valid username.";
                //    ////    break;

                //    ////case MembershipCreateStatus.InvalidPassword:
                //    ////    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "The password was invalid:  A password cannot be an empty string and must also meet the pasword " + "\n" + " strength requirements of the configured provider.  Please enter a new password.");
                //    ////    _msg = "The password was invalid:  A password cannot be an empty string and must also meet the pasword " + "\n" + " strength requirements.  Please enter a new password.";
                //    ////    break;

                //    ////case MembershipCreateStatus.InvalidEmail:
                //    ////    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "The invalid Email.Please enter a valid Email.");
                //    ////    _msg = "The invalid Email.Please enter a valid Email.";
                //    ////    break;

                //    ////case MembershipCreateStatus.DuplicateUserName:
                //    ////    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "The username is already in use.Please enter a new username.");
                //    ////    _msg = "The username is already in use.Please enter a new username.";
                //    ////    break;

                //    ////default:
                //    ////    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "An error occurred while creating the user.");
                //    ////    _msg = "An error occurred while creating the user.";
                //    ////   break;
                //    //// //return _userId;
                //    ////}
                //}
                if (MessageBox.Show("Are you sure to save ?", "Confirm Save", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                row_aff = CHNLSVC.Security.UpdateUser(_SystemUser);

                if (row_aff == 1)
                {
                    // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Updated.");
                    MessageBox.Show("Successfully Updated!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtID.Text = "";
                    this.btnClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Update Failed!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
            //catch (UIValidationException ex)
            //{
            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            //}
            catch (Exception ex)
            {
                CHNLSVC.CloseChannel();
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e.Message);
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                //check user ID for empty
                if (txtID.Text == string.Empty || txtID.Text == "")
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the user ID!");
                    MessageBox.Show("Please select the user ID!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtID.Focus();
                    return;
                }

                //check user name for empty
                if (txtName.Text == string.Empty || txtName.Text == "")
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the user name!");
                    MessageBox.Show("Please select the user name!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                #region KPMG - 6.8 System Administration Module Issues :: edit by Chamal 21/07/2014
                if (string.IsNullOrEmpty(txtMobile.Text))
                {
                    MessageBox.Show("Please enter user mobile no!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMobile.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPhone.Text))
                {
                    MessageBox.Show("Please enter user phone no!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtEmpID.Text))
                {
                    MessageBox.Show("Please enter user employee no(EPF)!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmpID.Focus();
                    return;
                }


                if (txtEmpID.Text.Length != 7)
                {
                    MessageBox.Show("Employee no should seven(7) characters!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmpID.Focus();
                    return;
                }

                if (IsNumeric(txtEmpID.Text) == false)
                {
                    MessageBox.Show("Invalid employee no(EPF)!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmpID.Select();
                    txtEmpID.Focus();
                    return;
                }

                if (CheckSpaces(txtID.Text) == false)
                {
                    MessageBox.Show("Invalid character(s) in user id!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtID.SelectAll();
                    txtID.Focus();
                    return;
                }

                if (CheckSpaces(txtMobile.Text) == false)
                {
                    MessageBox.Show("Invalid character(s) in mobile no!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMobile.SelectAll();
                    txtMobile.Focus();
                    return;
                }

                if (CheckSpaces(txtPhone.Text) == false)
                {
                    MessageBox.Show("Invalid character(s) in phone no!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.SelectAll();
                    txtPhone.Focus();
                    return;
                }

                if (CheckSpaces(txtEmpID.Text) == false)
                {
                    MessageBox.Show("Invalid character(s) in user employee no(EPF)!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmpID.SelectAll();
                    txtEmpID.Focus();
                }

                if (IsNumeric(txtMobile.Text) == false)
                {
                    MessageBox.Show("Invalid mobile no!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMobile.Select();
                    txtMobile.Focus();
                    return;
                }

                if (IsNumeric(txtPhone.Text) == false)
                {
                    MessageBox.Show("Invalid phone no!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Select();
                    txtPhone.Focus();
                    return;
                }
                #endregion

                //check email for empty
                if (txtEMail.Text == string.Empty || txtEMail.Text == "")
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter user email!");
                    MessageBox.Show("Please enter user email!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEMail.Focus();
                    return;
                }

                //check @ sigh in email
                Boolean valid = BaseCls.IsValidEmail(txtEMail.Text.Trim());
                if (valid == false)
                {
                    MessageBox.Show("Invalid email address", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEMail.Focus();
                    return;
                }

                Boolean _isValid = IsValidMobileOrLandNo(txtMobile.Text.Trim());
                if (_isValid == false)
                {
                    MessageBox.Show("Invalid mobile number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMobile.Focus();
                    return;
                }

                Boolean _isValidPHN = IsValidMobileOrLandNo(txtPhone.Text.Trim());
                if (_isValidPHN == false)
                {
                    MessageBox.Show("Invalid phone number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    return;
                }

                if (txtEmpID.Text == string.Empty || txtEmpID.Text == "")
                {
                    MessageBox.Show("Please enter employee ID!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmpID.Focus();
                    return;
                }
                //check is domain then domain id exsist
                if (chkIsDomain.Checked == true)
                {
                    if (txtDomainID.Text == string.Empty || txtDomainID.Text == "")
                    {
                        // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter domain id!");
                        MessageBox.Show("Please enter domain id!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtDomainID.Focus();
                        return;
                    }
                }

                // check pw exprire is set then pw valid period
                int outputvalue = 0;
                bool isNumber = false;

                if (chkPWExpire.Checked == true)
                {
                    if (txtValid.Text == string.Empty || txtValid.Text == "")
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter PW valid period!");
                        MessageBox.Show("Please enter PW valid period!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtValid.Focus();
                        return;
                    }
                    else
                    {
                        isNumber = int.TryParse(txtValid.Text, out outputvalue);
                        {
                            if (!isNumber)
                            {
                                // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "PW valid period should be numeric!");
                                MessageBox.Show("PW valid period should be numeric!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtValid.Text = "";
                                txtValid.Focus();
                                return;
                            }
                        }
                    }
                }


                //Check password for empty
                if (txtPW.Text == string.Empty || txtPW.Text == "")
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter default password!");
                    MessageBox.Show("Please enter default password!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPW.Focus();
                    return;
                }

                //Check confirm password for empty
                if (txtCPW.Text == string.Empty || txtCPW.Text == "")
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter confirm password!");
                    MessageBox.Show("Please enter confirm password!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCPW.Focus();
                    return;
                }

                //Validate enter password
                if (txtPW.Text != txtCPW.Text)
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Password validation is fail!");
                    MessageBox.Show("Password validation is fail!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCPW.Text = "";
                    txtCPW.Focus();
                    return;
                }

                //Check Pw valid period
                if (string.IsNullOrEmpty(txtValid.Text))
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter password valid period!");
                    MessageBox.Show("Please enter password valid period!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtValid.Text = "0";
                    txtValid.Focus();
                    return;
                }

                //Check user category select status
                if (txtCate.Text.Trim() == "")
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select user designation!");
                    MessageBox.Show("Please select user designation!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCate.Focus();
                    return;
                }

                //Check user department
                if (txtDept.Text.Trim() == "")
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select user department!");
                    MessageBox.Show("Please select user department!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDept.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtSunUserID.Text))
                {
                    txtSunUserID.Text = "N/A";
                }

                SaveSystemUser();
            }
            catch (Exception ex)
            {

                CHNLSVC.CloseChannel();
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void SaveSystemUser()
        {
            Int32 row_aff = 0;
            string _userId = string.Empty;
            string _msg = string.Empty;
            //    MembershipCreateStatus result;  ????????
            try
            {

                SystemUser _SystemUser = new SystemUser();
                _SystemUser.Se_usr_id = txtID.Text;
                _SystemUser.Se_usr_name = txtName.Text;
                _SystemUser.Se_usr_desc = txtDescription.Text;
                _SystemUser.Se_usr_pw = txtPW.Text;
                _SystemUser.Se_usr_cat = txtCate.Text.Trim();
                _SystemUser.Se_dept_id = txtDept.Text.Trim();
                _SystemUser.Se_emp_id = txtEmpID.Text;
                _SystemUser.Se_isdomain = (chkIsDomain.Checked == true) ? 1 : 0;
                _SystemUser.Se_iswinauthend = (chkIsWinAuth.Checked == true) ? 1 : 0;
                _SystemUser.Se_domain_id = txtDomainID.Text;
                _SystemUser.Se_noofdays = Convert.ToInt16(txtValid.Text);
                _SystemUser.Se_ischange_pw = (chkPWChange.Checked == true) ? 1 : 0;
                _SystemUser.Se_pw_mustchange = (chkMustChange.Checked == true) ? 1 : 0;
                //_SystemUser.Se_act = (chkStatus.Checked == true) ? 1 : 0;
                //Edit by Chamal 09-Jun-2014
                if (optLock.Checked == true)
                { MessageBox.Show("Can't create new user account as LOCK status!", "User account status", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                else if (optInactive.Checked == true)
                {
                    if (MessageBox.Show("User creating with INACTIVE status!/nPlease confirm?", "User account status", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                    _SystemUser.Se_act = 0;
                }
                else if (optActive.Checked == true)
                { _SystemUser.Se_act = 1; }
                else if (optDisable.Checked == true)
                { MessageBox.Show("Can't create new user account as DISABLE status!", "User account status", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                else
                { _SystemUser.Se_act = 0; }
                _SystemUser.Se_pw_expire = (chkPWExpire.Checked == true) ? 1 : 0;
                _SystemUser.se_Email = txtEMail.Text;
                _SystemUser.se_Mob = txtMobile.Text;
                _SystemUser.se_Phone = txtPhone.Text;
                _SystemUser.Se_cre_by = BaseCls.GlbUserID;
                _SystemUser.Se_mod_by = BaseCls.GlbUserID;
                _SystemUser.Se_session_id = BaseCls.GlbUserSessionID;
                _SystemUser.Se_SUN_ID = txtSunUserID.Text; //Add by Chamal 16-Sep-2013
                _SystemUser.Se_emp_cd = txtEmpCode.Text; // Add by Tharaka 2015-02-24

                //  using (TransactionScope tr = new TransactionScope())
                //  {

                //  MembershipUser _memberShipUser = Membership.CreateUser(txtID.Text, txtPW.Text, txtEMail.Text, "Not Applicable", "Not Applicable", true, out result);

                //switch (result)
                //{
                //    case MembershipCreateStatus.Success:
                //        {
                //            _userId = _memberShipUser.UserName;
                //            row_aff = CHNLSVC.Security.SaveNewUser(_SystemUser);
                //          //  tr.Complete();
                //            break;
                //        }

                //    case MembershipCreateStatus.InvalidUserName:
                //        //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "The invalid Username.  Please enter a valid username.");
                //        _msg = "The invalid Username.  Please enter a valid username.";
                //        break;

                //    case MembershipCreateStatus.InvalidPassword:
                //        //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "The password was invalid:  A password cannot be an empty string and must also meet the pasword " + "\n" + " strength requirements of the configured provider.  Please enter a new password.");
                //        _msg = "The password was invalid: Password strength required. Please enter a new password.";
                //        break;

                //    case MembershipCreateStatus.InvalidEmail:
                //        //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "The invalid Email.Please enter a valid Email.");
                //        _msg = "The invalid Email.Please enter a valid Email.";
                //        break;

                //    case MembershipCreateStatus.DuplicateUserName:
                //        //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "The username is already in use.Please enter a new username.");
                //        _msg = "The username is already in use.Please enter a new username.";
                //        break;

                //    default:
                //        //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "An error occurred while creating the user.");
                //        _msg = "An error occurred while creating the user.";
                //        break;
                //    // return _userId;
                //}
                // }
                if (MessageBox.Show("Are you sure to save ?", "Confirm Save", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                //------------------------------------------------------------------------------------------------------------
                row_aff = CHNLSVC.Security.SaveNewUser(_SystemUser);
                //------------------------------------------------------------------------------------------------------------

                if (row_aff == 1)
                {
                    // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully created.");
                    MessageBox.Show("Successfully created!", "Create", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtID.Text = "";
                    //ClearData();
                    this.btnClear_Click(null, null);
                }
                else
                {
                    //if (!string.IsNullOrEmpty(_msg))
                    //{
                    //  //  this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _msg);
                    //    MessageBox.Show(_msg, "Create", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                    //else
                    //{
                    //   // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Creation Fail.");
                    //    MessageBox.Show("Creation Fail", "Create", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                    MessageBox.Show("Creation Failed", "Create", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
            //catch (UIValidationException ex)
            //{
            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            //}
            catch (Exception e)
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e.Message);
                MessageBox.Show(e.Message, "Create", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void LoadDomainDetails()
        {
            SystemUser _systemaduser = new SystemUser();
            //SystemUser _systemuser = null;
            if (!CHNLSVC.Security.CheckValidADUser(txtDomainID.Text, out _systemaduser))
            {

                // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid domain user");
                MessageBox.Show("Invalid domain user", "Create", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {

                lblDDept.Text = _systemaduser.Ad_department;
                lblDName.Text = _systemaduser.Ad_full_name;
                lblDTitle.Text = _systemaduser.Ad_title;

            }


        }
        protected void LoadUserDetails()
        {
            //TODO: UNCOMMENT THE METHOD BODY


            SystemUser _systemUser = null;
            _systemUser = CHNLSVC.Security.GetUserByUserID(txtID.Text);
            if (_systemUser == null)
            {
                if (MessageBox.Show("Do you want to create new user?", "Confirm Save", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    txtID.Text = "";
                    // return;
                }

                return;
            }
            if (_systemUser != null)
            {
                // MessageBox.Show("You can update user details!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtID.Enabled = false;
                txtName.Text = _systemUser.Se_usr_name;
                txtDescription.Text = _systemUser.Se_usr_desc;
                txtPW.Text = _systemUser.Se_usr_pw;
                txtCPW.Text = _systemUser.Se_usr_pw;
                txtCate.Text = _systemUser.Se_usr_cat;
                txtDept.Text = _systemUser.Se_dept_id;
                txtValid.Text = Convert.ToString(_systemUser.Se_noofdays);
                txtEMail.Text = _systemUser.se_Email;
                txtMobile.Text = _systemUser.se_Mob;
                txtPhone.Text = _systemUser.se_Phone;

                if (_systemUser.Se_emp_id == null)
                {
                    txtEmpID.Text = "";
                }
                else
                {
                    txtEmpID.Text = _systemUser.Se_emp_id;
                }

                // check domain user
                if (_systemUser.Se_isdomain == 0)
                {
                    chkIsDomain.Checked = false;
                }
                else if (_systemUser.Se_isdomain == 1)
                {
                    chkIsDomain.Checked = true;
                }
                else
                {
                    chkIsDomain.Checked = false;
                }

                txtDomainID.Text = _systemUser.Se_domain_id;
                if (chkIsDomain.Checked == true)
                {
                    LoadDomainDetails();
                }

                //check windows authentivate
                if (_systemUser.Se_iswinauthend == 0)
                { chkIsWinAuth.Checked = false; }
                else if (_systemUser.Se_iswinauthend == 1)
                { chkIsWinAuth.Checked = true; }
                else
                { chkIsWinAuth.Checked = false; }
                //  check password change
                if (_systemUser.Se_ischange_pw == 0)
                { chkPWChange.Checked = false; }
                else if (_systemUser.Se_ischange_pw == 1)
                { chkPWChange.Checked = true; }
                else
                { chkPWChange.Checked = false; }

                // check password expire
                if (_systemUser.Se_pw_expire == 0)
                { chkPWExpire.Checked = false; }
                else if (_systemUser.Se_pw_expire == 1)
                { chkPWExpire.Checked = true; }
                else
                { chkPWExpire.Checked = false; }

                //Check pw change required in next login
                if (_systemUser.Se_pw_mustchange == 0)
                { chkMustChange.Checked = false; }
                else if (_systemUser.Se_pw_mustchange == 1)
                { chkMustChange.Checked = true; }
                else
                { chkMustChange.Checked = false; }


                btnAddNew.Enabled = false;
                btnUpdateAdvnDet.Enabled = true;

                //Check user id status
                //Chamal 09-Jun-2014
                if (_systemUser.Se_act == -1)
                { optLock.Checked = true; }
                else if (_systemUser.Se_act == 0)
                { optInactive.Checked = true; }
                else if (_systemUser.Se_act == 1)
                { optActive.Checked = true; }
                else if (_systemUser.Se_act == -2)
                {
                    optDisable.Checked = true;
                    btnUpdateAdvnDet.Enabled = false;
                    btnAddNew.Enabled = false;
                }
                else
                { optInactive.Checked = true; }

                txtSunUserID.Text = _systemUser.Se_SUN_ID;
                txtEmpCode.Text = _systemUser.Se_emp_cd;
            }
            else
            {
                // txtID.Text = "";
                //this.ClearData();
                //BindUserCategory();
                //BindUserDepartment();

            }


        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                bool _spChar = false;
                if (e.KeyChar != (char)Keys.Enter) _spChar = true;
                if (e.KeyChar != (char)Keys.Back) _spChar = true;
                if (e.KeyChar == (char)Keys.Space) e.Handled = true;

                if (_spChar == false)
                {
                    var regex = new Regex(@"[^a-zA-Z0-9\s]");
                    if (regex.IsMatch(e.KeyChar.ToString())) e.Handled = true;
                }

                if (e.KeyChar == (char)Keys.Enter)
                {
                    LoadUserDetails();
                    if (txtID.Text.Trim() != "") txtName.Focus();
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (txtUser.Text.Trim() != "")
                    {
                        //----------------------------------------------------------------
                        SystemUser _systemUser = null;
                        _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser.Text.Trim());
                        if (_systemUser == null)
                        {
                            MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtUser.Text = "";
                            txtFullName.Text = "";
                            txtDesn.Text = "";
                            txtCat.Text = "";
                            txtDept_.Text = "";
                            //txtEmpID_.Text = "";
                            //return;
                        }
                        else
                        {
                            Load_UserDetails(ref txtUser, ref txtFullName, ref txtDesn, ref txtCat, ref txtDept_, ref txtEmpID_);
                            txtCom_C.Focus();
                        }
                        //----------------------------------------------------------------                    
                        // txtName.Focus();
                        Load_UserCompanies();
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void LoadUserRole()
        {
            gvUserRole.DataSource = null;
            gvUserRole.AutoGenerateColumns = false;
            // gvUserRole.DataSource = CHNLSVC.Security.GetUserRole(txtUser_R.Text.Trim()); GetUserRole_NEW
            gvUserRole.DataSource = CHNLSVC.Security.GetUserRole_NEW(txtUser_R.Text.Trim());
            //gvUserRole.DataBind();

        }
        private void txtUser_R_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    Load_UserDetails(ref txtUser_R, ref txtFullName_R, ref txtDesn_R, ref txtCat_R, ref txtDept_R, ref txtEmpID_R);
                    LoadUserRole();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void LoadUserLoc()
        {
            gvUserLoc.DataSource = null;
            gvUserLoc.AutoGenerateColumns = false;
            gvUserLoc.DataSource = CHNLSVC.Security.GetUserLocations(txtUser_L.Text.Trim(), null);//CHNLSVC.Security.GetUserLoc(txtUser_L.Text.Trim());          
        }
        private void LoadUserPC()
        {
            gvUserPC.DataSource = null;
            gvUserPC.AutoGenerateColumns = false;
            gvUserPC.DataSource = CHNLSVC.Security.GetAllUserPC(txtUser_P.Text.Trim());

        }
        private void txtUser_L_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    Load_UserDetails(ref txtUser_L, ref txtFullName_L, ref txtDesn_L, ref txtCat_L, ref txtDept_L, ref txtEmpID_L);
                    LoadUserLoc();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtUser_P_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    Load_UserDetails(ref txtUser_P, ref txtFullName_P, ref txtDesn_P, ref txtCat_P, ref txtDept_P, ref txtEmpID_P);
                    LoadUserPC();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtUser_S_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    Load_UserDetails(ref txtUser_S, ref txtFullName_S, ref txtDesn_S, ref txtCat_S, ref txtDept_S, ref txtEmpID_S);
                    //Get_SpecialUser_Perm
                    DataTable DT = CHNLSVC.Security.Get_SpecialUser_Perm(txtUser_S.Text.Trim());
                    grdUserPerm.DataSource = null;
                    grdUserPerm.AutoGenerateColumns = false;
                    grdUserPerm.DataSource = DT;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #region Loading functions
        protected void Load_UserDetails(ref TextBox userID, ref TextBox fullName, ref TextBox desctription, ref TextBox designation, ref TextBox department, ref TextBox empID)
        {
            try
            {
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(userID.Text.Trim());
                if (_systemUser == null)
                {
                    MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    desctription.Text = "";
                    designation.Text = "";
                    fullName.Text = "";
                    empID.Text = "";
                    department.Text = "";

                    return;
                }
                desctription.Text = _systemUser.Se_usr_desc;
                MasterUserCategory _masterUsercat = _masterUsercat = CHNLSVC.General.GetUserCatByCode(_systemUser.Se_usr_cat);
                designation.Text = _masterUsercat.Mec_desc;
                fullName.Text = _systemUser.Se_usr_name;
                empID.Text = _systemUser.Se_emp_id;
                MasterDepartment _masterDept = null;
                _masterDept = CHNLSVC.General.GetDeptByCode(_systemUser.Se_dept_id);
                department.Text = _masterDept.Msdt_desc;

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchCom_C_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCom_C;
                _CommonSearch.ShowDialog();

                txtCom_C.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }




        protected void Load_UserCompanies()
        {
            grdUserComp.DataSource = null;
            grdUserComp.AutoGenerateColumns = false;
            grdUserComp.DataSource = CHNLSVC.Security.GetUser_Company(txtUser.Text.Trim());

        }

        private void txtCom_C_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Load_UserCompanies();
                if (txtCom_C.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtCom_C.Text.Trim());
                    if (com != null)
                    {
                        txtComDesc_C.Text = com.Mc_desc;
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }
        private void SaveSystemUserCompany()
        {
            try
            {
                if (txtUser.Text.Trim() == "")
                {
                    //throw new UIValidationException("Please select a user");
                    MessageBox.Show("Please select a user.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtCom_C.Text.Trim() == "")
                {
                    //throw new UIValidationException("Please select a company");
                    MessageBox.Show("Please select a company.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SystemUserCompany _systemUserComp = new SystemUserCompany();
                _systemUserComp.SEC_USR_ID = txtUser.Text.Trim();
                _systemUserComp.SEC_COM_CD = txtCom_C.Text.Trim();//cmb_Company.SelectedValue;
                _systemUserComp.SEC_DEF_COMCD = (chkDefault.Checked == true) ? 1 : 0;
                _systemUserComp.SEC_ACT = (chkActive.Checked == true) ? 1 : 0;
                _systemUserComp.SEC_CRE_BY = BaseCls.GlbUserID;
                _systemUserComp.SEC_MOD_BY = BaseCls.GlbUserID;
                _systemUserComp.SEC_SESSION_ID = BaseCls.GlbUserSessionID;

                int row_aff = CHNLSVC.Security.UpdateSystemUserCompany(_systemUserComp);

                Load_UserCompanies();
                //  this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully updated");
                MessageBox.Show("Successfully updated!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //ClearUserComp();
            }
            //catch (UIValidationException ex)
            //{
            //    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            catch (Exception ex)
            {
                // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAddNewCom_Click(object sender, EventArgs e)
        {
            try
            {
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser.Text.Trim());
                if (_systemUser == null)
                {
                    MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //------------------------------------
                MasterCompany COM = CHNLSVC.General.GetCompByCode(txtCom_C.Text.Trim());
                if (COM == null)
                {
                    MessageBox.Show("Invalid company code!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCom_C.Focus();
                    return;
                }
                //------------------------------------
                if (chkDefault.Checked == true)
                {
                    Int16 is_Def_Comp = CHNLSVC.Security.Check_User_Def_Comp(txtUser.Text.Trim());
                    if (is_Def_Comp == 1)
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "More than one default company cannot be assigned");
                        MessageBox.Show("More than one default company cannot be assigned", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }
                SaveSystemUserCompany();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void DeleteSystemUserComp()
        {
            try
            {
                if (txtUser.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a user.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtCom_C.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a company.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int row_arr = CHNLSVC.Security.DeleteUserComp(txtUser.Text.Trim(), txtCom_C.Text.Trim());

                Load_UserCompanies();

                MessageBox.Show("Successfully deleted!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // ClearUserComp();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void btnDeleteCom_Click(object sender, EventArgs e)
        {
            try
            {
                //------------------------------------
                MasterCompany COM = CHNLSVC.General.GetCompByCode(txtCom_C.Text.Trim());
                if (COM == null)
                {
                    MessageBox.Show("Invalid company code!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCom_C.Focus();
                    return;
                }
                //------------------------------------
                DeleteSystemUserComp();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtCom_R_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtRoleID.Text = string.Empty;
                txtRoleDesn.Text = string.Empty;
            }
        }

        private void btnSearchRole_Click(object sender, EventArgs e)
        {
            try
            {
                //TODO: TAKE FROM FOLLOWING METHOD
                //CHNLSVC.Security.GetRoleByCompany(ddlCompCode.SelectedValue, 1);
                if (txtCom_R.Text.Trim() == "")
                {
                    MessageBox.Show("Please select company", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemRole);
                //DataTable _result = CHNLSVC.CommonSearch.Get_All_Roles(_CommonSearch.SearchParams, null, null);
                //_CommonSearch.ReturnIndex = 1;
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = txtRoleID;
                //_CommonSearch.ShowDialog();

                //txtRoleID.Focus();


                Select_company = txtCom_R.Text.Trim().ToUpper();

                TextBox txtBox = new TextBox();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
                DataTable _result = CHNLSVC.CommonSearch.Get_system_role(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRoleID; //txtBox;
                _CommonSearch.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void DeleteSystemUserRole()
        {
            try
            {
                if (txtCom_R.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a company.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //------------------------------------
                MasterCompany COM = CHNLSVC.General.GetCompByCode(txtCom_R.Text.Trim());
                if (COM == null)
                {
                    MessageBox.Show("Invalid company code!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCom_R.Focus();
                    return;
                }
                //------------------------------------

                if (txtUser_R.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a user.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (txtRoleID.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a role.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                //int row_arr = CHNLSVC.Security.DeleteUserRole(txtUser_R.Text.Trim(), txtCom_R.Text.Trim(), Convert.ToInt32(txtRoleID.Text.Trim()));
                //int row_arr = CHNLSVC.Security.DeleteUserRole_NEW(txtUser_R.Text.Trim(), txtCom_R.Text.Trim(), Convert.ToInt32(txtRoleID.Text.Trim()));               


                //Edit by Chamal 09-Jun-2014
                SystemUserRole _systemUserRole = new SystemUserRole();
                _systemUserRole.SER_COM_CD = txtCom_R.Text.Trim();
                _systemUserRole.SER_ROLE_ID = Convert.ToInt32(txtRoleID.Text.Trim());
                _systemUserRole.SER_USR_ID = txtUser_R.Text.Trim();
                _systemUserRole.Se_cre_by = BaseCls.GlbUserID;
                _systemUserRole.Se_session_id = BaseCls.GlbUserSessionID;

                int row_arr = CHNLSVC.Security.DeleteSystemUserRole_NEW(_systemUserRole);
                if (row_arr > 0)
                {
                    LoadUserRole();
                    MessageBox.Show("Successfully deleted!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCom_R.Clear();
                    txtRoleID.Clear();
                    txtRoleDesn.Clear();
                }
                else
                {
                    MessageBox.Show("User role not exist under the user name!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            //catch (UIValidationException ex)
            //{
            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            //}
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void btnDeleteRole_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteSystemUserRole();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAddNewRole_Click(object sender, EventArgs e)
        {
            try
            {
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser_R.Text.Trim());
                if (_systemUser == null)
                {
                    MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SaveSystemUserRole();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void SaveSystemUserRole()
        {
            try
            {
                if (txtCom_R.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a company.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtUser_R.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a user.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtRoleID.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a role.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //------------------------------------
                MasterCompany COM = CHNLSVC.General.GetCompByCode(txtCom_R.Text.Trim());
                if (COM == null)
                {
                    MessageBox.Show("Invalid company code!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCom_R.Focus();
                    return;
                }
                //------------------------------------

                SystemUserRole _systemUserRole = new SystemUserRole();
                _systemUserRole.SER_COM_CD = txtCom_R.Text.Trim();
                _systemUserRole.SER_ROLE_ID = Convert.ToInt32(txtRoleID.Text.Trim());
                _systemUserRole.SER_USR_ID = txtUser_R.Text.Trim();
                _systemUserRole.Se_cre_by = BaseCls.GlbUserID;
                _systemUserRole.Se_session_id = BaseCls.GlbUserSessionID;

                //  int row_aff = CHNLSVC.Security.SaveSystemUserRole(_systemUserRole);
                int row_aff = CHNLSVC.Security.SaveSystemUserRole_NEW(_systemUserRole);

                LoadUserRole();
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully updated");
                MessageBox.Show("Successfully updated!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtCom_R.Clear();
                txtRoleID.Clear();
                txtRoleDesn.Clear();
                // ClearUserRole();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchComR_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCom_R;
                _CommonSearch.ShowDialog();

                txtCom_R.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnSearchUsrIDCom_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
                DataTable _result = CHNLSVC.CommonSearch.Get_All_SystemUsers(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtUser;
                _CommonSearch.ShowDialog();

                txtUser.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchUser_R_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
                DataTable _result = CHNLSVC.CommonSearch.Get_All_SystemUsers(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtUser_R;
                _CommonSearch.ShowDialog();

                txtUser_R.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchUser_L_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
                DataTable _result = CHNLSVC.CommonSearch.Get_All_SystemUsers(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtUser_L;
                _CommonSearch.ShowDialog();

                txtUser_L.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchUser_P_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
                DataTable _result = CHNLSVC.CommonSearch.Get_All_SystemUsers(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtUser_P;
                _CommonSearch.ShowDialog();

                txtUser_P.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void btnSearchUser_S_Click(object sender, EventArgs e)
        {
            try
            {
                //txtUser_S
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
                DataTable _result = CHNLSVC.CommonSearch.Get_All_SystemUsers(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtUser_S;
                _CommonSearch.ShowDialog();

                txtUser_S.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchCom_S_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCom_S;
                _CommonSearch.ShowDialog();

                txtCom_S.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchPerm_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SecUsrPermTp);
                DataTable _result = CHNLSVC.CommonSearch.Get_All_SecUsersPerimssionTypes(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPermCd;
                _CommonSearch.ShowDialog();

                txtPermCd.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAddUserPerm_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUser_S.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a user", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //----------------------------------------------------------------
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser_S.Text.Trim());
                if (_systemUser == null)
                {
                    MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //----------------------------------------------------------------
                if (txtPermCd.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a permission", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (txtParty.Text.Trim() == "")
                {
                    if (rdoLoc.Checked == true)
                    {
                        MessageBox.Show("Please select the location", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (rdoPC.Checked == true)
                    {
                        MessageBox.Show("Please select the profit center", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
                if (rdoAnyParty.Checked == true)
                {
                    txtParty.Text = "";
                }

                if (txtCom_S.Text.Trim() != "")
                {  //------------------------------------
                    MasterCompany COM = CHNLSVC.General.GetCompByCode(txtCom_S.Text.Trim());
                    if (COM == null)
                    {
                        MessageBox.Show("Invalid company code!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCom_S.Focus();
                        return;
                    }
                    //------------------------------------

                }
                //txtPermCd
                SecUserPerm usrPerm = new SecUserPerm();
                usrPerm.Seur_act = true;
                usrPerm.Seur_cd = txtPermCd.Text.Trim();
                usrPerm.Seur_com = txtCom_S.Text.Trim();
                usrPerm.Seur_cre_by = BaseCls.GlbUserID;
                usrPerm.Seur_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;
                usrPerm.Seur_mod_by = CHNLSVC.Security.GetServerDateTime().Date;
                usrPerm.Seur_mod_dt = BaseCls.GlbUserID;
                usrPerm.Seur_usr_id = txtUser_S.Text.Trim();
                if (rdoLoc.Checked == true || rdoPC.Checked == true)
                {
                    usrPerm.Seur_party = txtParty.Text.Trim() != "" ? txtParty.Text.Trim() : "";
                }
                else
                {
                    usrPerm.Seur_party = "";
                }

                if (MessageBox.Show("Are you sure to save?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                Int32 effect = CHNLSVC.Security.Save_SecUserPerm(usrPerm);
                //

                if (effect > 0)
                {
                    MessageBox.Show("Sucessfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable DT = CHNLSVC.Security.Get_SpecialUser_Perm(txtUser_S.Text.Trim());
                    grdUserPerm.DataSource = null;
                    grdUserPerm.AutoGenerateColumns = false;
                    grdUserPerm.DataSource = DT;
                    //Load_UserDetails(ref txtUser_S, ref txtFullName_S, ref txtDesn_S, ref txtCat_S, ref txtDept_S, ref txtEmpID_S);
                }
                else
                {
                    MessageBox.Show("Not created.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchParty_Click(object sender, EventArgs e)
        {
            try
            {
                txtParty.Text = "";
                if (txtCom_S.Text.Trim() == "")
                {
                    MessageBox.Show("Please select company", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (rdoLoc.Checked == true)
                {
                    //TODO: LOAD LOCATIONS

                    //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    //if (_isAllLocation == false)
                    //{
                    //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                    //    DataTable _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    //    _CommonSearch.dvResult.DataSource = _result;
                    //    _CommonSearch.BindUCtrlDDLData(_result);
                    //}
                    //--------------------------------------------------------
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                    DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtParty;
                    _CommonSearch.ShowDialog();

                    txtParty.Focus();
                }
                else if (rdoPC.Checked == true)
                {
                    if (txtCom_S.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter Company Code");
                        return;
                    }

                    //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    //if (_isAllProfitCenter == false)
                    //{
                    //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    //    DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    //    _CommonSearch.dvResult.DataSource = _result;
                    //    _CommonSearch.BindUCtrlDDLData(_result);
                    //}

                    //------------------------------------------------
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtParty;
                    _CommonSearch.ShowDialog();

                    txtParty.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void txtCom_S_TextChanged(object sender, EventArgs e)
        {
            txtParty.Text = "";
            rdoAnyParty.Checked = true;
        }

        private void txtCom_S_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnDeleteUserPerm_Click(object sender, EventArgs e)
        {
            try
            {
                grdUserPerm.EndEdit();
                List<SecUserPerm> del_permList = new List<SecUserPerm>();

                List<string> list = new List<string>();
                foreach (DataGridViewRow dgvr in grdUserPerm.Rows)
                {
                    DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        SecUserPerm usrPerm = new SecUserPerm();
                        usrPerm.Seur_act = true;
                        usrPerm.Seur_cd = dgvr.Cells["SEUR_CD"].Value.ToString();
                        usrPerm.Seur_com = dgvr.Cells["SEUR_COM"].Value.ToString();
                        //usrPerm.Seur_cre_by = BaseCls.GlbUserID;
                        //usrPerm.Seur_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;
                        usrPerm.Seur_mod_by = CHNLSVC.Security.GetServerDateTime().Date;
                        usrPerm.Seur_mod_dt = BaseCls.GlbUserID;
                        usrPerm.Seur_usr_id = txtUser_S.Text.Trim();
                        usrPerm.Seur_party = dgvr.Cells["SEUR_PARTY"].Value.ToString();

                        del_permList.Add(usrPerm);
                    }
                }

                if (del_permList.Count < 1)
                {
                    MessageBox.Show("Please select permissions to delete!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //TODO: DELETE
                Int32 effect = CHNLSVC.Security.Inactivate_SecUserPerm(del_permList);
                if (effect > 0)
                {
                    MessageBox.Show("Successfully Updated!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DataTable DT = CHNLSVC.Security.Get_SpecialUser_Perm(txtUser_S.Text.Trim());
                    grdUserPerm.DataSource = null;
                    grdUserPerm.AutoGenerateColumns = false;
                    grdUserPerm.DataSource = DT;
                    return;
                }
                else
                {
                    MessageBox.Show("No records updated!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private List<SystemUserLoc> GetSelectedLocationList(out string company, out Int32 Default_count)
        {
            Default_count = 0;
            company = "";
            List<SystemUserLoc> list = new List<SystemUserLoc>();
            foreach (DataGridViewRow dgvr in grvLocs.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    // list.Add(dgvr.Cells["LOCATION"].Value.ToString());
                    //company = dgvr.Cells["l_SEL_COM_CD"].Value.ToString();
                    company = ucLoactionSearch1.Company;
                    SystemUserLoc _systemUserLoc = new SystemUserLoc();
                    _systemUserLoc.SEL_COM_CD = ucLoactionSearch1.Company;//dgvr.Cells["l_SEL_COM_CD"].Value.ToString();
                    _systemUserLoc.SEL_LOC_CD = dgvr.Cells["LOCATION"].Value.ToString();
                    _systemUserLoc.SEL_USR_ID = txtUser_L.Text.Trim();

                    DataGridViewCheckBoxCell chk_def = dgvr.Cells["Is_default"] as DataGridViewCheckBoxCell;
                    bool isDefault = Convert.ToBoolean(chk_def.Value == DBNull.Value ? 0 : chk_def.Value);
                    if (isDefault == true)
                    {
                        Default_count = Default_count + 1;
                    }
                    _systemUserLoc.SEL_DEF_LOCCD = isDefault == true ? 1 : 0;//(chkDefLoc.Checked == true) ? 1 : 0;

                    list.Add(_systemUserLoc);
                }
            }
            return list;
        }
        private void btnAddLoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUser_L.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a user", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //-------------------------------------------------------------
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser_L.Text.Trim());
                if (_systemUser == null)
                {
                    MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //----------------------------------------------------------------
                Int32 Default_CNT = 0;
                string company;
                List<SystemUserLoc> loc_list = GetSelectedLocationList(out company, out  Default_CNT);
                if (loc_list.Count < 1)
                {
                    MessageBox.Show("Please select location(s)", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Int16 is_Def_Loc = CHNLSVC.Security.Check_User_Def_Loc(txtUser_L.Text.Trim(), company);
                if (is_Def_Loc > 0 && Default_CNT > 0)
                {
                    MessageBox.Show("User has a default location already.\nMore than one default location cannot be assigned.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Default_CNT > 1)
                {
                    MessageBox.Show("More than one default location cannot be assigned.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Int32 eff = CHNLSVC.Security.UpdateSystemUserLoc_NEW(loc_list);
                MessageBox.Show("Successfully Updated!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUserLoc();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
            //return;
            //
            //if (eff > 0)
            //{               
            //}
            //else
            //{
            //    MessageBox.Show("Not Updated!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}           
        }

        private void btnAddLocs_Click(object sender, EventArgs e)
        {
            try
            {
                string com = ucLoactionSearch1.Company;
                string chanel = ucLoactionSearch1.Channel;
                string subChanel = ucLoactionSearch1.SubChannel;
                string area = ucLoactionSearch1.Area;
                string region = ucLoactionSearch1.Regien;
                string zone = ucLoactionSearch1.Zone;
                string pc = ucLoactionSearch1.ProfitCenter;

                DataTable dt = CHNLSVC.Inventory.GetLOC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
                // select_LOC_List.Merge(dt);
                grvLocs.DataSource = null;
                grvLocs.AutoGenerateColumns = false;
                grvLocs.DataSource = dt;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAddPc_Click(object sender, EventArgs e)
        {
            try
            {
                string com = ucProfitCenterSearch1.Company;
                string chanel = ucProfitCenterSearch1.Channel;
                string subChanel = ucProfitCenterSearch1.SubChannel;
                string area = ucProfitCenterSearch1.Area;
                string region = ucProfitCenterSearch1.Regien;
                string zone = ucProfitCenterSearch1.Zone;
                string pc = ucProfitCenterSearch1.ProfitCenter;

                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
                grvPCs.DataSource = null;
                grvPCs.AutoGenerateColumns = false;
                grvPCs.DataSource = dt;

                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("Please check profit center hirachy setup.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                    return;
                }

                //select_PC_List.Merge(dt);
                //grvProfCents.DataSource = null;
                //grvProfCents.AutoGenerateColumns = false;
                //// grvProfCents.DataSource = dt;
                //grvProfCents.DataSource = select_PC_List;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        private List<SystemUserProf> GetSelectedProfitCenter_List(out string company, out Int32 Default_count)
        {
            Default_count = 0;
            company = "";
            List<SystemUserProf> list = new List<SystemUserProf>();
            foreach (DataGridViewRow dgvr in grvPCs.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    //**************************************************************

                    company = ucProfitCenterSearch1.Company;
                    SystemUserProf _systemUserPc = new SystemUserProf();
                    //_systemUserPc.MasterPC;
                    _systemUserPc.Sup_com_cd = ucProfitCenterSearch1.Company;
                    // _systemUserPc.Sup_def_pccd;
                    _systemUserPc.Sup_pc_cd = dgvr.Cells["PROFIT_CENTER"].Value.ToString();
                    _systemUserPc.Sup_usr_id = txtUser_P.Text.Trim();

                    DataGridViewCheckBoxCell chk_def = dgvr.Cells["Is_defPC"] as DataGridViewCheckBoxCell;
                    bool isDefault = Convert.ToBoolean(chk_def.Value == DBNull.Value ? 0 : chk_def.Value);
                    if (isDefault == true)
                    {
                        Default_count = Default_count + 1;
                    }
                    _systemUserPc.Sup_def_pccd = isDefault;//(chkDefLoc.Checked == true) ? 1 : 0;

                    list.Add(_systemUserPc);
                    //**************************************************************
                }
            }
            return list;
        }

        private void btn_AddPC_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUser_P.Text.Trim() == "")
                {
                    MessageBox.Show("Please select a user", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //----------------------------------------------------------------
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser_P.Text.Trim());
                if (_systemUser == null)
                {
                    MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //----------------------------------------------------------------

                Int32 Default_CNT = 0;
                string company;
                List<SystemUserProf> pc_list = GetSelectedProfitCenter_List(out company, out  Default_CNT);
                if (pc_list.Count < 1)
                {
                    MessageBox.Show("Please select profit center(s)!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Int16 is_Def_PC = CHNLSVC.Security.Check_User_Def_PC(txtUser_P.Text.Trim(), company);
                if (is_Def_PC > 0 && Default_CNT > 0)
                {
                    MessageBox.Show("User has a default profit center already.\nMore than one default location cannot be assigned.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Default_CNT > 1)
                {
                    MessageBox.Show("More than one default profit center cannot be assigned.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //TODO: SAVE PROFIT CENTERS
                Int32 effect = CHNLSVC.Security.UpdateSystemUserPC_NEW(pc_list);
                MessageBox.Show("Successfully Updated!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUserPC();
                //  Int32 eff=CHNLSVC.Security.save

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void check_allLoc_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (check_allLoc.Checked == true)
                {
                    try
                    {
                        foreach (DataGridViewRow row in grvLocs.Rows)
                        {
                            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                            chk.Value = true;
                        }
                        grvLocs.EndEdit();
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    try
                    {
                        foreach (DataGridViewRow row in grvLocs.Rows)
                        {
                            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                            chk.Value = false;
                        }
                        grvLocs.EndEdit();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void check_allPc_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (check_allPc.Checked == true)
                {
                    try
                    {
                        foreach (DataGridViewRow row in grvPCs.Rows)
                        {
                            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                            chk.Value = true;
                        }
                        grvPCs.EndEdit();
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    try
                    {
                        foreach (DataGridViewRow row in grvPCs.Rows)
                        {
                            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                            chk.Value = false;
                        }
                        grvPCs.EndEdit();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchDesig_Click(object sender, EventArgs e)
        {
            try
            {
                //pkg_search.sp_getuser_category 
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Designation);
                DataTable _result = CHNLSVC.CommonSearch.Get_Designations(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate;
                _CommonSearch.ShowDialog();

                txtCate.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchDept_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Department);
                DataTable _result = CHNLSVC.CommonSearch.Get_Departments(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDept;
                _CommonSearch.ShowDialog();

                txtDept.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private List<SystemUserLoc> GetSelectedLocationList_toUpdate()
        {
            List<SystemUserLoc> list = new List<SystemUserLoc>();
            foreach (DataGridViewRow dgvr in gvUserLoc.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    //company = ucLoactionSearch1.Company;
                    SystemUserLoc _systemUserLoc = new SystemUserLoc();
                    _systemUserLoc.SEL_COM_CD = dgvr.Cells["SEL_COM_CD"].Value.ToString();
                    _systemUserLoc.SEL_LOC_CD = dgvr.Cells["SEL_LOC_CD"].Value.ToString();
                    _systemUserLoc.SEL_USR_ID = txtUser_L.Text.Trim();

                    DataGridViewCheckBoxCell chk_def = dgvr.Cells["SEL_DEF_LOCCD"] as DataGridViewCheckBoxCell;
                    bool isDefault = Convert.ToBoolean(chk_def.Value == DBNull.Value ? 0 : chk_def.Value);
                    _systemUserLoc.SEL_DEF_LOCCD = isDefault == true ? 1 : 0;

                    list.Add(_systemUserLoc);
                }
            }
            return list;
        }
        private void btnDelLoc_Click(object sender, EventArgs e)
        {
            try
            {
                List<SystemUserLoc> delete_list = GetSelectedLocationList_toUpdate();
                if (delete_list.Count == 0)
                {
                    MessageBox.Show("Please select locations to delete!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtUser_L.Text == "")
                {
                    MessageBox.Show("Please select user!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int row_arr = CHNLSVC.Security.DeleteUserLoc_NEW(delete_list);
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully deleted");
                MessageBox.Show("Successfully deleted!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUserLoc();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private List<SystemUserProf> GetSelectedProfitCenterList_toUpdate()
        {
            List<SystemUserProf> list = new List<SystemUserProf>();
            foreach (DataGridViewRow dgvr in gvUserPC.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    //company = ucLoactionSearch1.Company;
                    SystemUserProf _systemUserPC = new SystemUserProf();
                    _systemUserPC.Sup_com_cd = dgvr.Cells["p_SUP_COM_CD"].Value.ToString();
                    _systemUserPC.Sup_pc_cd = dgvr.Cells["p_SUP_PC_CD"].Value.ToString();
                    _systemUserPC.Sup_usr_id = txtUser_P.Text.Trim();

                    DataGridViewCheckBoxCell chk_def = dgvr.Cells["p_SUP_DEF_PCCD"] as DataGridViewCheckBoxCell;
                    bool isDefault = Convert.ToBoolean(chk_def.Value == DBNull.Value ? 0 : chk_def.Value);
                    _systemUserPC.Sup_def_pccd = isDefault;

                    list.Add(_systemUserPC);
                }
            }
            return list;
        }

        private void btn_DelPC_Click(object sender, EventArgs e)
        {
            try
            {
                List<SystemUserProf> delete_list = GetSelectedProfitCenterList_toUpdate();
                if (delete_list.Count == 0)
                {
                    MessageBox.Show("Please select profit centers to delete!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtUser_P.Text == "")
                {
                    MessageBox.Show("Please select user!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //TODO: DELETE

                int row_arr = CHNLSVC.Security.DeleteUserPC_NEW(delete_list);

                MessageBox.Show("Successfully deleted!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUserPC();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtDescription.Focus();
            }
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPW.Focus();
            }
        }

        private void txtPW_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtCPW.Focus();
            }
        }

        private void txtCPW_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtEMail.Focus();
            }
        }

        private void txtEMail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtEMail.Text.Trim() != "")
                {
                    Boolean valid = BaseCls.IsValidEmail(txtEMail.Text.Trim());
                    if (valid == false)
                    {
                        MessageBox.Show("Invalid email address", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEMail.Focus();
                        return;
                    }
                }
                txtMobile.Focus();
            }
        }

        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtMobile.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Invalid mobile number.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMobile.Focus();
                    return;
                }
                txtPhone.Focus();
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Boolean _isValidPHN = IsValidMobileOrLandNo(txtPhone.Text.Trim());
                if (_isValidPHN == false)
                {
                    MessageBox.Show("Invalid phone number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    return;
                }
                txtCate.Focus();
            }
        }
        private void txtCate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtDept.Focus();
            }
        }
        private void txtDept_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtEmpID.Focus();
            }

        }
        private void txtDomainID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtSunUserID.Focus();
                Load_DomainDetails();
            }
        }

        private void chkIsDomain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                chkIsWinAuth.Focus();
                chkIsWinAuth.Select();
            }
        }

        private void chkIsWinAuth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtDomainID.Focus();
            }
        }

        private void txtEmpID_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool _spChar = false;
            if (e.KeyChar != (char)Keys.Enter) _spChar = true;
            if (e.KeyChar != (char)Keys.Back) _spChar = true;
            if (e.KeyChar == (char)Keys.Space) e.Handled = true;

            if (_spChar == false)
            {
                var regex = new Regex(@"[^a-zA-Z0-9\s]");
                if (regex.IsMatch(e.KeyChar.ToString())) e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                chkIsDomain.Focus();
                chkIsDomain.Select();
            }
        }

        private void txtValid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                chkPWChange.Focus();
                chkPWChange.Select();
            }
        }

        private void txtCom_C_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCom_C.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtCom_C.Text.Trim());
                    if (com != null)
                    {
                        txtComDesc_C.Text = com.Mc_desc;
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtRoleID_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtRoleID.Text.Trim() != "")
                {
                    //TODO: LOAD ROLE DESCRIPTION
                    // USE THIS SP = SP_GETUSERROLEBYROLEDATA 
                    SystemRole role = new SystemRole();
                    role.CompanyCode = txtCom_R.Text.Trim();
                    role.RoleId = Convert.ToInt32(txtRoleID.Text.Trim());

                    //role = CHNLSVC.Security.GetSystemRoleByRoleData(role);
                    role = CHNLSVC.Security.GetSystemRoleByRoleData_new(role);
                    if (role != null)
                    {
                        if (role.IsActive != 1)
                        {
                            MessageBox.Show("This role inactive!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtRoleID.Clear();
                            txtRoleDesn.Clear();
                            txtRoleID.Focus();
                            return;
                        }
                        txtRoleDesn.Text = role.Description;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void txtRoleID_TextChanged(object sender, EventArgs e)
        {
            this.txtRoleID_Leave(null, null);
        }

        private void txtCom_R_TextChanged(object sender, EventArgs e)
        {
            txtRoleID.Text = "";
            txtRoleDesn.Text = "";
        }

        private void rdoAnyParty_Click(object sender, EventArgs e)
        {
            if (rdoAnyParty.Checked == true)
            {
                txtParty.Text = "";
            }

        }

        private void rdoAnyParty_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAnyParty.Checked == true)
            {
                btnSearchParty.Visible = false;
                txtParty.Visible = false;
            }
            else
            {
                btnSearchParty.Visible = true;
                txtParty.Visible = true;
            }
            txtParty.Text = "";

        }
        private void rdoLoc_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAnyParty.Checked == true)
            {
                btnSearchParty.Visible = false;
                txtParty.Visible = false;
            }
            else
            {
                btnSearchParty.Visible = true;
                txtParty.Visible = true;
            }
            txtParty.Text = "";
        }
        private void rdoPC_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAnyParty.Checked == true)
            {
                btnSearchParty.Visible = false;
                txtParty.Visible = false;
            }
            else
            {
                btnSearchParty.Visible = true;
                txtParty.Visible = true;
            }
            txtParty.Text = "";
        }

        private void txtID_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text.Trim() != "")
                {
                    SystemUser _systemUser = null;
                    _systemUser = CHNLSVC.Security.GetUserByUserID(txtID.Text);
                    if (_systemUser != null)
                    {
                        LoadUserDetails();
                    }
                    txtName.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtUser.Text.Trim() != "")
                {
                    SystemUser _systemUser = null;
                    _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser.Text.Trim());
                    if (_systemUser != null)
                    {
                        Load_UserDetails(ref txtUser, ref txtFullName, ref txtDesn, ref txtCat, ref txtDept_, ref txtEmpID_);
                    }
                    Load_UserCompanies();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtUser_R_Leave(object sender, EventArgs e)
        {
            try
            {
                //Load_UserDetails(ref txtUser_R, ref txtFullName_R, ref txtDesn_R, ref txtCat_R, ref txtDept_R, ref txtEmpID_R);
                //LoadUserRole();
                if (txtUser_R.Text.Trim() != "")
                {
                    SystemUser _systemUser = null;
                    _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser_R.Text.Trim());
                    if (_systemUser != null)
                    {
                        Load_UserDetails(ref txtUser_R, ref txtFullName_R, ref txtDesn_R, ref txtCat_R, ref txtDept_R, ref txtEmpID_R);
                    }
                    Load_UserCompanies();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtUser_L_Leave(object sender, EventArgs e)
        {
            try
            {
                //Load_UserDetails(ref txtUser_L, ref txtFullName_L, ref txtDesn_L, ref txtCat_L, ref txtDept_L, ref txtEmpID_L);
                //LoadUserLoc();
                if (txtUser_L.Text.Trim() != "")
                {
                    SystemUser _systemUser = null;
                    _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser_L.Text.Trim());
                    if (_systemUser != null)
                    {
                        Load_UserDetails(ref txtUser_L, ref txtFullName_L, ref txtDesn_L, ref txtCat_L, ref txtDept_L, ref txtEmpID_L);
                    }
                    Load_UserCompanies();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtUser_P_Leave(object sender, EventArgs e)
        {
            try
            {
                //Load_UserDetails(ref txtUser_P, ref txtFullName_P, ref txtDesn_P, ref txtCat_P, ref txtDept_P, ref txtEmpID_P);
                //LoadUserPC();
                if (txtUser_P.Text.Trim() != "")
                {
                    SystemUser _systemUser = null;
                    _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser_P.Text.Trim());
                    if (_systemUser != null)
                    {
                        Load_UserDetails(ref txtUser_P, ref txtFullName_P, ref txtDesn_P, ref txtCat_P, ref txtDept_P, ref txtEmpID_P);
                    }
                    Load_UserCompanies();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtUser_S_Leave(object sender, EventArgs e)
        {
            try
            {
                Load_UserDetails(ref txtUser_S, ref txtFullName_S, ref txtDesn_S, ref txtCat_S, ref txtDept_S, ref txtEmpID_S);
                //Get_SpecialUser_Perm
                DataTable DT = CHNLSVC.Security.Get_SpecialUser_Perm(txtUser_S.Text.Trim());
                grdUserPerm.DataSource = null;
                grdUserPerm.AutoGenerateColumns = false;
                grdUserPerm.DataSource = DT;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
                DataTable _result = CHNLSVC.CommonSearch.Get_All_SystemUsers(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtID;
                _CommonSearch.ShowDialog();

                txtID.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        //Load Domain user details
        private void Load_DomainDetails()
        {
            SystemUser _systemaduser = new SystemUser();
            //SystemUser _systemuser = null;
            if (!CHNLSVC.Security.CheckValidADUser(txtDomainID.Text, out _systemaduser))
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid domain user");
                MessageBox.Show("Invalid domain user", "Domain", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblDDept.Text = "";
                lblDName.Text = "";
                lblDTitle.Text = "";
                return;
            }
            else
            {
                lblDDept.Text = _systemaduser.Ad_department;
                lblDName.Text = _systemaduser.Ad_full_name;
                lblDTitle.Text = _systemaduser.Ad_title;
            }
        }

        private void btnSearchCom_A_Click(object sender, EventArgs e)
        {
            //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            //DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = txtCom_A;
            //_CommonSearch.ShowDialog();

            //txtCom_A.Focus();
        }

        private void btnSearchApprPermCode_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermCode);
                DataTable _result = CHNLSVC.CommonSearch.Search_Approve_Permissions(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAppr_Code;
                _CommonSearch.ShowDialog();

                txtAppr_Code.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchApprLvl_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAppr_Code.Text.Trim() == "")
                {
                    MessageBox.Show("Please select approval code first!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermLevelCode);
                DataTable _result = CHNLSVC.CommonSearch.Search_ApprovePermission_Levels(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPermLvl;
                _CommonSearch.ShowDialog();

                txtPermLvl.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
                DataTable _result = CHNLSVC.CommonSearch.Get_All_SystemUsers(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtUser_A;
                _CommonSearch.ShowDialog();

                txtUser_A.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtUser_A_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    Load_UserDetails(ref txtUser_A, ref txtFullName_A, ref txtDesn_A, ref txtCat_A, ref txtDept_A, ref txtEmpID_A);
                    //LoadUserLoc();
                    DataTable DT = CHNLSVC.Security.Get_UserApprove_Permissions(txtUser_A.Text.Trim(), string.Empty);
                    grvApprLevel.DataSource = null;
                    grvApprLevel.AutoGenerateColumns = false;
                    grvApprLevel.DataSource = DT;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddAppPerm_Click(object sender, EventArgs e)
        {
            SecApproveUserPerm appUsrPerm = new SecApproveUserPerm();
            appUsrPerm.Saup_act = true;
            appUsrPerm.Saup_cre_by = BaseCls.GlbUserID;
            appUsrPerm.Saup_cre_when = CHNLSVC.Security.GetServerDateTime().Date;
            appUsrPerm.Saup_max_app_limit = Convert.ToInt32(txtFinApprLevel.Text.Trim());
            appUsrPerm.Saup_mod_by = BaseCls.GlbUserID;
            appUsrPerm.Saup_mod_when = appUsrPerm.Saup_cre_when;
            appUsrPerm.Saup_prem_cd = txtPermLvl.Text.Trim();
            appUsrPerm.Saup_session_id = BaseCls.GlbUserSessionID; ;
            appUsrPerm.Saup_usr_id = txtUser_A.Text.Trim();
            appUsrPerm.Saup_val_limit = 0;

            Int32 eff = CHNLSVC.Security.Save_Sec_App_Usr_Prem(appUsrPerm);
            if (eff > 0)
            {
                MessageBox.Show("Sucessfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //TODO: LOAD GRID
            }
            else
            {
                MessageBox.Show("Not Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAppr_Code_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtAppr_Code.Text.Trim() != "")
                {
                    DataTable DT = CHNLSVC.Security.Get_Approve_PermissionInfo(txtAppr_Code.Text.Trim());

                    string Discription = DT.Rows[0]["sart_desc"].ToString();
                    txtApprCdDesc.Text = Discription;

                    string FinalApprLvl = DT.Rows[0]["sart_app_lvl"].ToString();
                    txtFinApprLevel.Text = FinalApprLvl;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtUser_A_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtUser_A.Text.Trim() != "")
                {
                    SystemUser _systemUser = null;
                    _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser_A.Text.Trim());
                    if (_systemUser != null)
                    {
                        Load_UserDetails(ref txtUser_A, ref txtFullName_A, ref txtDesn_A, ref txtCat_A, ref txtDept_A, ref txtEmpID_A);
                    }
                    //TODO: LOAD GRID
                    DataTable DT = CHNLSVC.Security.Get_UserApprove_Permissions(txtUser_A.Text.Trim(), string.Empty);

                    grvApprLevel.DataSource = null;
                    //grvApprLevel.AutoGenerateColumns = false;
                    grvApprLevel.DataSource = DT;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtSunUserID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtValid.Focus();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "SEC1") == false)
                {
                    MessageBox.Show("No permission for 'Assign Role' task!", "Power User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl1.SelectedTab = tabPage1;
                    return;
                }
            }
            else if (tabControl1.SelectedIndex == 5)
            {
                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "SEC1") == false)
                {
                    MessageBox.Show("No permission for 'User Special Permission' task!", "Power User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl1.SelectedTab = tabPage1;
                    return;
                }
            }
        }

        private void gvUserRole_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (gvUserRole.ColumnCount > 0)
            {
                Int32 _rowIndex = e.RowIndex;
                Int32 _colIndex = e.ColumnIndex;

                if (_rowIndex != -1)
                {
                    txtCom_R.Text = gvUserRole.Rows[_rowIndex].Cells["SERM_COM_CD"].Value.ToString();
                    txtRoleID.Text = gvUserRole.Rows[_rowIndex].Cells["SERM_ROLE_ID"].Value.ToString();
                    txtRoleDesn.Text = gvUserRole.Rows[_rowIndex].Cells["ssrr_rolename"].Value.ToString();
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void txtID_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnDeleteApprPerm_Click(object sender, EventArgs e)
        {
            string _user = "";
            string _perCd = "";
            if (grvApprLevel.Rows.Count <= 0)
            {
                MessageBox.Show("Cannot find any permission.", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Boolean _appItm = false;
            foreach (DataGridViewRow row in grvApprLevel.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells["col_Chk"] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chk.Value) == true)
                {
                    _appItm = true;
                    goto L4;
                }
            }
        L4:

            if (_appItm == false)
            {
                MessageBox.Show("Please select permission which need to de-activate.", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        foreach (DataGridViewRow newRow in grvApprLevel.Rows)
        {
            DataGridViewCheckBoxCell chk = newRow.Cells["col_Chk"] as DataGridViewCheckBoxCell;

            if (Convert.ToBoolean(chk.Value) == true)
            {
                _user = txtUser_A.Text.Trim();
                _perCd = newRow.Cells["saup_prem_cd"].Value.ToString();

                SecApproveUserPerm appUsrPerm = new SecApproveUserPerm();
                appUsrPerm.Saup_act = false;
                appUsrPerm.Saup_cre_by = BaseCls.GlbUserID;
                appUsrPerm.Saup_cre_when = CHNLSVC.Security.GetServerDateTime().Date;
                appUsrPerm.Saup_max_app_limit = 0;
                appUsrPerm.Saup_mod_by = BaseCls.GlbUserID;
                appUsrPerm.Saup_mod_when = appUsrPerm.Saup_cre_when;
                appUsrPerm.Saup_prem_cd = _perCd;
                appUsrPerm.Saup_session_id = BaseCls.GlbUserSessionID; ;
                appUsrPerm.Saup_usr_id = _user;
                appUsrPerm.Saup_val_limit = 0;

                Int32 eff = CHNLSVC.Security.Save_Sec_App_Usr_Prem(appUsrPerm);
                if (eff > 0)
                {
                    MessageBox.Show("Sucessfully de-activated!", "De-Activated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //TODO: LOAD GRID
                    DataTable DT = CHNLSVC.Security.Get_UserApprove_Permissions(txtUser_A.Text.Trim(), string.Empty);

                    grvApprLevel.DataSource = null;
                    //grvApprLevel.AutoGenerateColumns = false;
                    grvApprLevel.DataSource = DT;
                }
                else
                {
                    MessageBox.Show("Not de-activated!", "De-Activated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        }

        private void grvApprLevel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in grvApprLevel.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chk.Value) == true)
                {
                    chk.Value = false;
                }

            }

            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)grvApprLevel.Rows[grvApprLevel.CurrentRow.Index].Cells[0];

            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "False":
                    {
                        ch1.Value = true;
                        break;
                    }
            }
        }

        //TODO:
        //protected void LoadAssignedUserLoc()
        //{
        //    if (txtCom_C.Text.Trim()!="")
        //    {
        //        SystemUserLoc _userLoc = null;
        //        _userLoc = CHNLSVC.Security.GetAssignedUserLocation(txtUser.Text.Trim(), txtCom_C.Text.Trim(), txtLoc.Text);
        //        if (_userLoc != null)
        //        {
        //            chkDefLoc.Checked = Convert.ToBoolean(_userLoc.SEL_DEF_LOCCD);

        //        }
        //        else
        //        {
        //            chkDefLoc.Checked = false;
        //        }
        //    }
        //}
        /*
     protected void LoadAssignedUserComp()
     {
         if (!cmbUser.SelectedValue.Equals("-1"))
         {
             SystemUserCompany _userComp = null;
             _userComp = CHNLSVC.Security.GetAssignedUserCompany(cmb_Company.SelectedValue, cmbUser.SelectedValue);
             if (_userComp != null)
             {
                 chkDefault.Checked = Convert.ToBoolean(_userComp.SEC_DEF_COMCD);
                 chkActive.Checked = Convert.ToBoolean(_userComp.SEC_ACT);
             }
             else
             {
                 chkDefault.Checked = false;
                 chkActive.Checked = false;
             }
         }
     }

     protected void LoadRoleDesn()
     {
         SystemRole _systemRole = null;
         _systemRole = CHNLSVC.Security.GetRoleByCode(ddlCompCode.SelectedValue, Convert.ToInt32(ddlRoleID.SelectedValue));
         txtRoleDesn.Text = _systemRole.RoleName;

     }
     */
        #endregion
    }
}
