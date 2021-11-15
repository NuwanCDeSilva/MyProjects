using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.ADMIN
{
    public partial class User_Creation : Base
    {
        DataTable _result;
        string Select_company = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageClear();
            }
            //txtSearchbyword.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('ctl00_ContentPlaceHolder1_Button1').click();return false;}} else {return true}; ");
        }
        private void PageClear()
        {
            grdSbu.DataSource = new int[] { };
            grdSbu.DataBind();
            grdUserComp.DataSource = new int[] { };
            grdUserComp.DataBind();
            gvUserRole.DataSource = new int[] { };
            gvUserRole.DataBind();
            grvLocs.DataSource = new int[] { };
            grvLocs.DataBind();
            gvUserLoc.DataSource = new int[] { };
            gvUserLoc.DataBind();
            grdUserPerm.DataSource = new int[] { };
            grdUserPerm.DataBind();
            grvApprLevel.DataSource = new int[] { };
            grvApprLevel.DataBind();
            grvPCs.DataSource = new int[] { };
            grvPCs.DataBind();
            gvUserPC.DataSource = new int[] { };
            gvUserPC.DataBind();
        }
        private void MsgClear()
        {
            WarningUser.Visible = false;
            SuccessUser.Visible = false;
            AlertUser.Visible = false;
            tests.Visible = false;
            DivSuccess.Visible = false;
            errorDiv.Visible = false;
            successDiv.Visible = false;
            WarnningLoc.Visible = false;
            SuccessLoc.Visible = false;
            WarnnPer.Visible = false;
            SuccesPer.Visible = false;
            AprovalWarning.Visible = false;
            ApprovalSuccess.Visible = false;
            WarrningPro.Visible = false;
            SuccessPro.Visible = false;
            SBUerror.Visible = false;
            SBUSuccess.Visible = false;
        }

        #region Grid Check
        private void RememberOldCompanyValues()
        {
            ArrayList categoryIDList = new ArrayList();

            string index = null;
            foreach (GridViewRow row in grdUserComp.Rows)
            {
                index = grdUserComp.DataKeys[row.RowIndex].Value.ToString();
                bool result = ((CheckBox)row.FindControl("ChkUserComr")).Checked;


                // Check in the Session
                if (Session["CHECKED_Company"] != null)
                    categoryIDList = (ArrayList)Session["CHECKED_Company"];
                if (result)
                {
                    if (!categoryIDList.Contains(index))
                        categoryIDList.Add(index);
                }
                else
                    categoryIDList.Remove(index);



            }
            if (categoryIDList != null && categoryIDList.Count > 0)
                Session["CHECKED_Company"] = categoryIDList;

        }
        private void RePopulateCompanyValues()
        {
            ArrayList categoryIDList = (ArrayList)Session["CHECKED_Company"];

            if (categoryIDList != null && categoryIDList.Count > 0)
            {
                foreach (GridViewRow row in grdUserComp.Rows)
                {
                    string index = grdUserComp.DataKeys[row.RowIndex].Value.ToString();
                    if (categoryIDList.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)row.FindControl("ChkUserComr");
                        myCheckBox.Checked = true;
                    }
                }
            }

        }

        private void RememberOldRoleValues()
        {
            ArrayList categoryIDList = new ArrayList();

            string index = null;
            foreach (GridViewRow row in gvUserRole.Rows)
            {
                index = gvUserRole.DataKeys[row.RowIndex].Value.ToString();
                bool result = ((CheckBox)row.FindControl("ChkUserRole")).Checked;


                // Check in the Session
                if (Session["CHECKED_Role"] != null)
                    categoryIDList = (ArrayList)Session["CHECKED_Role"];
                if (result)
                {
                    if (!categoryIDList.Contains(index))
                        categoryIDList.Add(index);
                }
                else
                    categoryIDList.Remove(index);
            }
            if (categoryIDList != null && categoryIDList.Count > 0)
                Session["CHECKED_Role"] = categoryIDList;

        }
        private void RePopulateRoleValues()
        {
            ArrayList categoryIDList = (ArrayList)Session["CHECKED_Role"];

            if (categoryIDList != null && categoryIDList.Count > 0)
            {
                foreach (GridViewRow row in gvUserRole.Rows)
                {
                    string index = gvUserRole.DataKeys[row.RowIndex].Value.ToString();
                    if (categoryIDList.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)row.FindControl("ChkUserRole");
                        myCheckBox.Checked = true;
                    }
                }
            }

        }
        #endregion

        #region  Method
       
        private void RememberOldValues()
        {
            ArrayList categoryIDList = new ArrayList();
            
            string index = null;
            foreach (GridViewRow row in gvUserLoc.Rows)
            {
                index = gvUserLoc.DataKeys[row.RowIndex].Value.ToString();
                bool result = ((CheckBox)row.FindControl("selectchkU")).Checked;
                

                // Check in the Session
                if (Session["CHECKED_ITEMS"] != null)
                    categoryIDList = (ArrayList)Session["CHECKED_ITEMS"];
                if (result)
                {
                    if (!categoryIDList.Contains(index))
                        categoryIDList.Add(index);
                }
                else
                    categoryIDList.Remove(index);

                

            }
            if (categoryIDList != null && categoryIDList.Count > 0)
                Session["CHECKED_ITEMS"] = categoryIDList;
           
        }

        private void RePopulateValues()
        {
            ArrayList categoryIDList = (ArrayList)Session["CHECKED_ITEMS"];
           
            if (categoryIDList != null && categoryIDList.Count > 0)
            {
                foreach (GridViewRow row in gvUserLoc.Rows)
                {
                    string index = gvUserLoc.DataKeys[row.RowIndex].Value.ToString();
                    if (categoryIDList.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)row.FindControl("selectchkU");
                        myCheckBox.Checked = true;
                    }
                }
            }
           
        }


        private void RemembergrvLocsValues()
        {
            ArrayList categoryIDList = new ArrayList();

            string index = null;
            foreach (GridViewRow row in grvLocs.Rows)
            {
                index = grvLocs.DataKeys[row.RowIndex].Value.ToString();
                bool result = ((CheckBox)row.FindControl("selectchk")).Checked;


                // Check in the Session
                if (Session["CHECKED_ITEMSgrvLocs"] != null)
                    categoryIDList = (ArrayList)Session["CHECKED_ITEMSgrvLocs"];
                if (result)
                {
                    if (!categoryIDList.Contains(index))
                        categoryIDList.Add(index);
                }
                else
                    categoryIDList.Remove(index);



            }
            if (categoryIDList != null && categoryIDList.Count > 0)
                Session["CHECKED_ITEMSgrvLocs"] = categoryIDList;

        }

        private void RePopulategrvLocsValues()
        {
            ArrayList categoryIDList = (ArrayList)Session["CHECKED_ITEMSgrvLocs"];

            if (categoryIDList != null && categoryIDList.Count > 0)
            {
                foreach (GridViewRow row in grvLocs.Rows)
                {
                    string index = grvLocs.DataKeys[row.RowIndex].Value.ToString();
                    if (categoryIDList.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)row.FindControl("selectchk");
                        myCheckBox.Checked = true;
                    }
                }
            }

        }

        private void RemembergrvLocsDValues()
        {
            ArrayList categoryIDList = new ArrayList();

            string index = null;
            foreach (GridViewRow row in grvLocs.Rows)
            {
                index = grvLocs.DataKeys[row.RowIndex].Value.ToString();
                bool result = ((CheckBox)row.FindControl("Is_default")).Checked;


                // Check in the Session
                if (Session["CHECKED_ITEMSgrvLocsD"] != null)
                    categoryIDList = (ArrayList)Session["CHECKED_ITEMSgrvLocsD"];
                if (result)
                {
                    if (!categoryIDList.Contains(index))
                        categoryIDList.Add(index);
                }
                else
                    categoryIDList.Remove(index);



            }
            if (categoryIDList != null && categoryIDList.Count > 0)
                Session["CHECKED_ITEMSgrvLocsD"] = categoryIDList;

        }
        private void RePopulategrvLocsDValues()
        {
            ArrayList categoryIDList = (ArrayList)Session["CHECKED_ITEMSgrvLocsD"];

            if (categoryIDList != null && categoryIDList.Count > 0)
            {
                foreach (GridViewRow row in grvLocs.Rows)
                {
                    string index = grvLocs.DataKeys[row.RowIndex].Value.ToString();
                    if (categoryIDList.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)row.FindControl("Is_default");
                        myCheckBox.Checked = true;
                    }
                }
            }

        }

        private void RemembergrvPCsValues1()
        {
            ArrayList categoryIDListgrvPCs = new ArrayList();

            string index = null;
            foreach (GridViewRow row in grvPCs.Rows)
            {
                index = grvPCs.DataKeys[row.RowIndex].Value.ToString();
                bool result = ((CheckBox)row.FindControl("chkPr")).Checked;


                // Check in the Session
                if (Session["CHECKED_ITEMSgrvPCs1"] != null)
                    categoryIDListgrvPCs = (ArrayList)Session["CHECKED_ITEMSgrvPCs1"];
                if (result)
                {
                    if (!categoryIDListgrvPCs.Contains(index))
                        categoryIDListgrvPCs.Add(index);
                }
                else
                    categoryIDListgrvPCs.Remove(index);



            }
            if (categoryIDListgrvPCs != null && categoryIDListgrvPCs.Count > 0)
                Session["CHECKED_ITEMSgrvPCs1"] = categoryIDListgrvPCs;

        }
        private void RePopulategrvPCsValues1()
        {
            ArrayList categoryIDListgrvPCs = (ArrayList)Session["CHECKED_ITEMSgrvPCs1"];

            if (categoryIDListgrvPCs != null && categoryIDListgrvPCs.Count > 0)
            {
                foreach (GridViewRow row in grvPCs.Rows)
                {
                    string index = grvPCs.DataKeys[row.RowIndex].Value.ToString();
                    if (categoryIDListgrvPCs.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)row.FindControl("chkPr");
                        myCheckBox.Checked = true;
                    }
                }
            }

        }
        private void RemembergrvPCsValues2()
        {
            ArrayList categoryIDList = new ArrayList();

            string index = null;
            foreach (GridViewRow row in grvPCs.Rows)
            {
                index = grvPCs.DataKeys[row.RowIndex].Value.ToString();
                bool result = ((CheckBox)row.FindControl("Is_defPC")).Checked;


                // Check in the Session
                if (Session["CHECKED_ITEMSgrvPCs2"] != null)
                    categoryIDList = (ArrayList)Session["CHECKED_ITEMSgrvPCs2"];
                if (result)
                {
                    if (!categoryIDList.Contains(index))
                        categoryIDList.Add(index);
                }
                else
                    categoryIDList.Remove(index);



            }
            if (categoryIDList != null && categoryIDList.Count > 0)
                Session["CHECKED_ITEMSgrvPCs2"] = categoryIDList;

        }
        private void RePopulategrvPCsValues2()
        {
            ArrayList categoryIDList = (ArrayList)Session["CHECKED_ITEMSgrvPCs2"];

            if (categoryIDList != null && categoryIDList.Count > 0)
            {
                foreach (GridViewRow row in grvPCs.Rows)
                {
                    string index = grvPCs.DataKeys[row.RowIndex].Value.ToString();
                    if (categoryIDList.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)row.FindControl("Is_defPC");
                        myCheckBox.Checked = true;
                    }
                }
            }

        }
        private void RemembergvUserPCValues()
        {
            ArrayList categoryIDList = new ArrayList();

            string index = null;
            foreach (GridViewRow row in gvUserPC.Rows)
            {
                index = gvUserPC.DataKeys[row.RowIndex].Value.ToString();
                bool result = ((CheckBox)row.FindControl("chkDelPc")).Checked;


                // Check in the Session
                if (Session["CHECKED_ITEMSgvUserPC"] != null)
                    categoryIDList = (ArrayList)Session["CHECKED_ITEMSgvUserPC"];
                if (result)
                {
                    if (!categoryIDList.Contains(index))
                        categoryIDList.Add(index);
                }
                else
                    categoryIDList.Remove(index);



            }
            if (categoryIDList != null && categoryIDList.Count > 0)
                Session["CHECKED_ITEMSgvUserPC"] = categoryIDList;

        }
        private void RePopulategvUserPCValues()
        {
            ArrayList categoryIDList = (ArrayList)Session["CHECKED_ITEMSgvUserPC"];

            if (categoryIDList != null && categoryIDList.Count > 0)
            {
                foreach (GridViewRow row in gvUserPC.Rows)
                {
                    string index = gvUserPC.DataKeys[row.RowIndex].Value.ToString();
                    if (categoryIDList.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)row.FindControl("chkDelPc");
                        myCheckBox.Checked = true;
                    }
                }
            }

        }
        private void DeleteSystemUserComp(string username, string Company)
        {
            try
            {
                if (username.Trim() == "")
                {
                    AlertID.Text = "Please select a user.";
                    tests.Visible = true;
                  
                    // MessageBox.Show("Please select a user.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Company.Trim() == "")
                {
                    AlertID.Text = "Please select a company.";
                    tests.Visible = true;
                    
                    //MessageBox.Show("Please select a company.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int row_arr = CHNLSVC.Security.DeleteUserComp(username.Trim(), Company.Trim());

                Load_UserCompanies();
                
                lblSuccess.Text = "Successfully deleted!";
                DivSuccess.Visible = true;
                //MessageBox.Show("Successfully deleted!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // ClearUserComp();
            }
            catch (Exception ex)
            {

                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void SaveUserSBU()
        {
            try
            {
                SBUerror.Visible = false;
                SBUSuccess.Visible = false;
                if (txtUserIdSBU.Text.Trim() == "")
                {
                    lblSBUerror.Text = "Please select a user.";
                    SBUerror.Visible = true;
                    return;
                }
                if (txtCompanySBU.Text.Trim() == "")
                {
                    lblSBUerror.Text = "Please select a Company.";
                    SBUerror.Visible = true;
                    return;
                }
                if (txtSbuCode.Text.Trim() == "")
                {
                    lblSBUerror.Text = "Please select a SBU Code.";
                    SBUerror.Visible = true;
                    return;
                }
                if (chk_DefSbu.Checked == true)
                {
                    DataTable SBUTbl = CHNLSVC.Security.GetSBU_User(txtCompanySBU.Text, txtUserIdSBU.Text,null);
                    foreach(DataRow dr in SBUTbl.Rows){
                        int value =Convert.ToInt32(dr[1].ToString());
                        if (value == 1)
                        {
                            lblSBUerror.Text = "More than one default SBU cannot be assigned";
                            SBUerror.Visible = true;
                            return;
                        }
                    }
                    
                }
                StrategicBusinessUnits _SBU = new StrategicBusinessUnits();
                _SBU.Seo_act = (chk_ActSbu.Checked == true) ? true : false;
                _SBU.Seo_com_cd = txtCompanySBU.Text.Trim();
                _SBU.Seo_cre_by = Session["UserID"].ToString(); 
                _SBU.Seo_cre_dt = System.DateTime.Now;
                _SBU.Seo_def_opecd = (chk_DefSbu.Checked == true) ? true : false;
                _SBU.Seo_ope_cd = txtSbuCode.Text.Trim();
                _SBU.Seo_session_id = Session["SessionID"].ToString();
                _SBU.Seo_usr_id = txtUserIdSBU.Text.Trim();
                _SBU.Seo_mod_by = Session["UserID"].ToString();
                _SBU.Seo_mod_dt = System.DateTime.Now;
                int row_aff = CHNLSVC.Security.Save_User_SBU(_SBU);
                if (row_aff > 0)
                {
                    lblSBUSuccess.Text = "Successfully Saved!";
                    SBUSuccess.Visible = true;
                    txtSbuCode.Text = "";
                    txtSBUDes.Text = "";
                }
            }
            catch (Exception ex)
            {

                //  MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveSystemUserCompany()
        {
            try
            {
                errorDiv.Visible = false;
                successDiv.Visible = false;
                DivSuccess.Visible = false;
                if (txtUser.Text.Trim() == "")
                {
                    AlertID.Text = "Please select a user.";
                    tests.Visible = true;
                   
                    // MessageBox.Show("Please select a user.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtCom_C.Text.Trim() == "")
                {
                    AlertID.Text = "Please select a company.";
                    tests.Visible = true;
                   
                    // MessageBox.Show("Please select a company.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SystemUserCompany _systemUserComp = new SystemUserCompany();
                _systemUserComp.SEC_USR_ID = txtUser.Text.Trim();
                _systemUserComp.SEC_COM_CD = txtCom_C.Text.Trim();//cmb_Company.SelectedValue;
                _systemUserComp.SEC_DEF_COMCD = (chkDefault.Checked == true) ? 1 : 0;
                _systemUserComp.SEC_ACT = (chkActive.Checked == true) ? 1 : 0;
                _systemUserComp.SEC_CRE_BY = Session["UserID"].ToString();
                _systemUserComp.SEC_MOD_BY = Session["UserID"].ToString();
                _systemUserComp.SEC_SESSION_ID = Session["SessionID"].ToString();

                int row_aff = CHNLSVC.Security.UpdateSystemUserCompany(_systemUserComp);

                Load_UserCompanies();
                lblSuccess.Text = "Successfully updated!";
                DivSuccess.Visible = true;
                // MessageBox.Show("Successfully updated!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCom_C.Text = "";
                txtComDesc_C.Text = "";
            }

            catch (Exception ex)
            {

                //  MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadDomainDetails()
        {
            SystemUser _systemaduser = new SystemUser();
            //SystemUser _systemuser = null;
            if (!CHNLSVC.Security.CheckValidADUser(txtDomainID.Text, out _systemaduser))
            {

                // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid domain user");
                //MessageBox.Show("Invalid domain user", "Create", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            clearmessagers();
            SystemUser _systemUser = null;
            _systemUser = CHNLSVC.Security.GetUserByUserID(txtID.Text);
            // Session["SearchID"] = _systemUser.Se_usr_id;
            //if (_systemUser == null)
            //{

            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "CallConfirmBox", "NewConfirm();", true);
            //   // if (txtAlertValue.Value == "Yes")
            //   // {
            //    txtName.Focus();
            //       // return;
            //   // }
                   
               

               
            //}
           // txtName.Focus();
            if (_systemUser != null)
            {
                // MessageBox.Show("You can update user details!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
               // lblUAlert.Text = "You can update user details!";
              //  AlertUser.Visible = true;

                txtID.Enabled = false;
                txtName.Text = _systemUser.Se_usr_name;
                txtDescription.Text = _systemUser.Se_usr_desc;
                //txtPW.Text = _systemUser.Se_usr_pw;
                string Password = _systemUser.Se_usr_pw;
                txtPW.Attributes.Add("value", Password);


                //txtCPW.Text = _systemUser.Se_usr_pw;
                string CPassword = _systemUser.Se_usr_pw;
                txtCPW.Attributes.Add("value", CPassword);

                txtCate.Text = _systemUser.Se_usr_cat;
                txtDept.Text = _systemUser.Se_dept_id;
                txtValid.Text = Convert.ToString(_systemUser.Se_noofdays);
                txtEMail.Text = _systemUser.se_Email;
                txtMobile.Text = _systemUser.se_Mob;
                txtPhone.Text = _systemUser.se_Phone;
                txtFullName.Text = _systemUser.Se_usr_name;
                txtDesn.Text = _systemUser.Se_usr_desc;
                txtCat.Text = _systemUser.Se_usr_cat;
                txtDept_.Text = _systemUser.Se_dept_id;
                txtEmpID_.Text = _systemUser.Se_emp_id;

                txtFullName_R.Text = _systemUser.Se_usr_name;
                txtDesn_R.Text = _systemUser.Se_usr_desc;
                txtCat_R.Text = _systemUser.Se_usr_cat;
                txtDept_R.Text = _systemUser.Se_dept_id;
                txtEmpID_R.Text = _systemUser.Se_emp_id;

                txtFullName_A.Text = _systemUser.Se_usr_name;
                txtDesn_A.Text = _systemUser.Se_usr_desc;
                txtCat_A.Text = _systemUser.Se_usr_cat;
                txtDept_A.Text = _systemUser.Se_dept_id;
                txtEmpID_A.Text = _systemUser.Se_emp_id;

                txtFullName_S.Text = _systemUser.Se_usr_name;
                txtDesn_S.Text = _systemUser.Se_usr_desc;
                txtCat_S.Text = _systemUser.Se_usr_cat;
                txtDept_S.Text = _systemUser.Se_dept_id;
                txtEmpID_S.Text = _systemUser.Se_emp_id;


                txtFullName_L.Text = _systemUser.Se_usr_name;
                txtDesn_L.Text = _systemUser.Se_usr_desc;
                txtCat_L.Text = _systemUser.Se_usr_cat;
                txtDept_L.Text = _systemUser.Se_dept_id;
                txtEmpID_L.Text = _systemUser.Se_emp_id;

                txtFullName_P.Text = _systemUser.Se_usr_name;
                txtDesn_P.Text = _systemUser.Se_usr_desc;
                txtCat_P.Text = _systemUser.Se_usr_cat;
                txtDept_P.Text = _systemUser.Se_dept_id;
                txtEmpID_P.Text = _systemUser.Se_emp_id;

                txtID.Text = txtID.Text;
                txtUser.Text = txtID.Text;
                txtUser_R.Text = txtID.Text;
                txtUser_A.Text = txtID.Text;
                txtUser_S.Text = txtID.Text;
                txtUser_L.Text = txtID.Text;
                txtUser_P.Text = txtID.Text;

                txtFullNameSBU.Text = _systemUser.Se_usr_name;
                txtDesSBU.Text = _systemUser.Se_usr_desc;
                txtDesigSBU.Text = _systemUser.Se_usr_cat;
                txtDepSBU.Text = _systemUser.Se_dept_id;
                txtEmpIDSBU.Text = _systemUser.Se_emp_id;

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
                //if (_systemUser.Se_ischange_pw == 0)
                //{
                //    chkPWChange.Checked = false;
                //    txtPW.Enabled = false;
                //    txtCPW.Enabled = false;
                //}
                //else if (_systemUser.Se_ischange_pw == 1)
                //{
                //    chkPWChange.Checked = true;
                //    txtPW.Enabled = true;
                //    txtCPW.Enabled = true;
                //   // string Password4 = _systemUser.Se_usr_pw;
                //   // txtPW.Attributes.Add("value", Password4);
                //}
                //else
                //{ chkPWChange.Checked = false; }

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

                btnAddNew.Visible = false;

                btnAddNew.Enabled = false;
                btnUpdateAdvnDet.Enabled = true;
                btnUpdateAdvnDet.Visible = true;
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

                    btnUpdateAdvnDet.Visible = false;
                    btnAddNew.Visible = false;
                }
                else
                { optInactive.Checked = true; }

                txtSunUserID.Text = _systemUser.Se_SUN_ID;
                txtEmpCode.Text = _systemUser.Se_emp_cd;
            }
            else
            {
                txtName.Focus();
                // txtID.Text = "";
                //this.ClearData();
                //BindUserCategory();
                //BindUserDepartment();

            }


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
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
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
        private void LoadUserRole()
        {
            
            gvUserRole.DataSource = null;
            gvUserRole.AutoGenerateColumns = false;
            // gvUserRole.DataSource = CHNLSVC.Security.GetUserRole(txtUser_R.Text.Trim()); GetUserRole_NEW
            gvUserRole.DataSource = CHNLSVC.Security.GetUserRole_NEW(txtUser_R.Text.Trim());

            gvUserRole.DataBind();

        }
        protected void Load_UserCompanies()
        {
            grdUserComp.DataSource = null;
            grdUserComp.AutoGenerateColumns = false;
            DataTable ComTbl = CHNLSVC.Security.GetUser_Company(txtUser.Text.Trim());
            grdUserComp.DataSource = ComTbl;
            grdUserComp.DataBind();

        }
        public static Boolean IsNumeric(string stringToTest)
        {
            decimal result;
            return decimal.TryParse(stringToTest, out result);
        }

        public static bool IsValidMobileOrLandNo(string mobile)
        {
            string pattern = @"^[0-9]{10}$";

            System.Text.RegularExpressions.Match match = Regex.Match(mobile.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }
        bool CheckSpaces(string input)
        {
            string regex = @"^[\S]*$";
            bool isMatch = false;
            isMatch = Regex.IsMatch(input, regex);
            return isMatch;
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
                {
                    lblWUser.Text = "Can't update the user account as lock status!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Can't update the user account as lock status!", "User account status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (optInactive.Checked == true)
                { _SystemUser.Se_act = 0; }
                else if (optActive.Checked == true)
                { _SystemUser.Se_act = 1; }
                else if (optDisable.Checked == true)
                {
                    string confirmValue = Request.Form["User account disable"];
                    if (confirmValue == "No")
                    {
                        return;
                    }
                    //if (MessageBox.Show("User update with DISABLE status!\nPlease confirm?\n\nNote-\nAfter update the user account as DISABLE, your never activate again.", "User account disable", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.No)
                    //{
                    //    return;
                    //}
                   if (string.IsNullOrEmpty(txtDisableRmks.Text))
                    {
                    //    // MessageBox.Show("Please enter the resons for disable this user account.", "User account disable", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        lblWUser.Text = "Please enter the reasons for disable this user account.";
                        WarningUser.Visible = true;
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
                _SystemUser.Se_cre_by = Session["UserID"].ToString();
                _SystemUser.Se_mod_by = Session["UserID"].ToString();
                _SystemUser.Se_session_id = Session["SessionID"].ToString();
                _SystemUser.Se_SUN_ID = txtSunUserID.Text; //Add by Chamal 16-Sep-2013

                _SystemUser.Se_emp_cd = txtEmpCode.Text; // Add by Tharaka 2015-02-24

                string confirmValue1 = Request.Form["confirm_value"];
                if (confirmValue1 == "No")
                {
                    return;
                }
                //if (MessageBox.Show("Are you sure to save ?", "Confirm Save", MessageBoxButtons.YesNo) == DialogResult.No)
                //{
                //    return;
                //}

                row_aff = CHNLSVC.Security.UpdateUser(_SystemUser);

                if (row_aff == 1)
                {
                    LoadUserRole();
                    lblSUser.Text = "Successfully Updated!";
                    SuccessUser.Visible = true;
                    //// this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Updated.");
                    // MessageBox.Show("Successfully Updated!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   // txtID.Text = "";
                    // this.btnClear_Click(null, null);
                    //Response.Redirect("~/View/ADMIN/User_Creation.aspx");
                    //Response.Redirect(Request.RawUrl, false);
                }
                else
                {
                    lblWUser.Text = "Update Failed!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Update Failed!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }

            catch (Exception ex)
            {
                CHNLSVC.CloseChannel();
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e.Message);
                // MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void DeleteSystemUserRole(string CompanyCode, string code)
        {
            try
            {
                errorDiv.Visible = false;
                successDiv.Visible = false;
                //if (txtCom_R.Text.Trim() == "")
                //{
                //    lblWarn.Text = "Please select a company.";
                //    errorDiv.Visible = true;
                //    // MessageBox.Show("Please select a company.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
                ////------------------------------------
                //MasterCompany COM = CHNLSVC.General.GetCompByCode(CompanyCode.Trim());
                //if (COM == null)
                //{
                //    lblWarn.Text = "Invalid company code!";
                //    errorDiv.Visible = true;
                //    // MessageBox.Show("Invalid company code!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    txtCom_R.Focus();
                //    return;
                //}
                //------------------------------------

                if (txtUser_R.Text.Trim() == "")
                {
                    lblWarn.Text = "Please select a user.";
                    errorDiv.Visible = true;
                    // MessageBox.Show("Please select a user.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //if (txtRoleID.Text.Trim() == "")
                //{
                //    lblWarn.Text = "Please select a role.";
                //    errorDiv.Visible = true;
                //    // MessageBox.Show("Please select a role.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}

                SystemUserRole _systemUserRole = new SystemUserRole();
                _systemUserRole.SER_COM_CD = CompanyCode;

                _systemUserRole.SER_ROLE_ID = Convert.ToInt32(code);
                _systemUserRole.SER_USR_ID = txtUser_R.Text.Trim();
                _systemUserRole.Se_cre_by = Session["UserID"].ToString();
                _systemUserRole.Se_session_id = Session["SessionID"].ToString();

                int row_arr = CHNLSVC.Security.DeleteSystemUserRole_NEW(_systemUserRole);
                if (row_arr > 0)
                {
                    LoadUserRole();
                    lblWarn.Text = "Successfully deleted!";
                    successDiv.Visible = true;
                    // MessageBox.Show("Successfully deleted!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCom_R.Text = "";
                    txtRoleID.Text = "";
                    txtRoleDesn.Text = "";

                }
                else
                {
                    // MessageBox.Show("User role not exist under the user name!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }

            catch (Exception ex)
            {

                // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
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
            gvUserLoc.DataBind();
        }
        private void LoadUserPC()
        {
            gvUserPC.DataSource = null;
            gvUserPC.AutoGenerateColumns = false;
            gvUserPC.DataSource = CHNLSVC.Security.GetAllUserPC(txtUser_P.Text.Trim());
            gvUserPC.DataBind();

        }
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.cmbSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.cmbSearchbykey.Items.Add(col.ColumnName);
            }

            this.cmbSearchbykey.SelectedIndex = 0;
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
                _SystemUser.Se_usr_id = txtID.Text.ToUpper();
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
                {
                    lblWUser.Text = "Can't create new user account as LOCK status!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Can't create new user account as LOCK status!", "User account status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (optInactive.Checked == true)
                {
                    string confirmValue = Request.Form["confirm_value"];
                    if (confirmValue == "No")
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked No!')", true);
                        return;
                    }
                    //else
                    //{
                    //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
                    //}
                    //if (MessageBox.Show("User creating with INACTIVE status!/nPlease confirm?", "User account status", MessageBoxButtons.YesNo) == DialogResult.No)
                    //{
                    //    return;
                    //}
                    _SystemUser.Se_act = 0;
                }
                else if (optActive.Checked == true)
                { _SystemUser.Se_act = 1; }
                else if (optDisable.Checked == true)
                {
                    lblWUser.Text = "Can't create new user account as DISABLE status!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Can't create new user account as DISABLE status!", "User account status", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; 
                }
                else
                { _SystemUser.Se_act = 0; }
                _SystemUser.Se_pw_expire = (chkPWExpire.Checked == true) ? 1 : 0;
                _SystemUser.se_Email = txtEMail.Text;
                _SystemUser.se_Mob = txtMobile.Text;
                _SystemUser.se_Phone = txtPhone.Text;
                _SystemUser.Se_cre_by = Session["UserID"].ToString();
                _SystemUser.Se_mod_by = Session["UserID"].ToString();
                _SystemUser.Se_session_id = Session["SessionID"].ToString();
                _SystemUser.Se_SUN_ID = txtSunUserID.Text; //Add by Chamal 16-Sep-2013
                _SystemUser.Se_emp_cd = txtEmpCode.Text; // Add by Tharaka 2015-02-24

                string confirmValue1 = Request.Form["confirm_value"];
                if (confirmValue1 == "No")
                {
                    return;
                }
                //if (MessageBox.Show("Are you sure to save ?", "Confirm Save", MessageBoxButtons.YesNo) == DialogResult.No)
                //{
                //    return;
                //}

                //------------------------------------------------------------------------------------------------------------
                row_aff = CHNLSVC.Security.SaveNewUser(_SystemUser);
                //------------------------------------------------------------------------------------------------------------

                if (row_aff == 1)
                {
                    // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully created.");
                    lblSUser.Text = "Successfully created!";
                    SuccessUser.Visible = true;
                    // MessageBox.Show("Successfully created!", "Create", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtID.Text = "";
                   // Response.Redirect("~/View/ADMIN/User_Creation.aspx");
                    // this.btnClear_Click(null, null);
                }
                else
                {
                    lblWUser.Text = "Creation Failed";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Creation Failed", "Create", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }

            catch (Exception e)
            {
                //  MessageBox.Show(e.Message, "Create", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private List<SystemUserProf> GetSelectedProfitCenter_List(out string company, out Int32 Default_count)
        {
            Default_count = 0;
            company = "";
            List<SystemUserProf> list = new List<SystemUserProf>();
            foreach (GridViewRow dgvr in grvPCs.Rows)
            {
                // DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                CheckBox chk = (CheckBox)dgvr.FindControl("chkPr");
                if (chk.Checked == true)
                {
                    //**************************************************************

                    company = ucProfitCenterSearch.Company;
                    SystemUserProf _systemUserPc = new SystemUserProf();
                    //_systemUserPc.MasterPC;
                    _systemUserPc.Sup_com_cd = ucProfitCenterSearch.Company;
                    // _systemUserPc.Sup_def_pccd;
                    _systemUserPc.Sup_pc_cd = (dgvr.FindControl("PROFIT_CENTER") as Label).Text;
                    _systemUserPc.Sup_usr_id = txtUser_P.Text.Trim();

                    // DataGridViewCheckBoxCell chk_def = dgvr.Cells["Is_defPC"] as DataGridViewCheckBoxCell;
                    CheckBox chk_def = (CheckBox)dgvr.FindControl("Is_defPC");
                    // bool isDefault = Convert.ToBoolean(chk_def.Checked == DBNull.Value ? 0 : chk_def.Value);
                    bool isDefault = Convert.ToBoolean(chk_def.Checked);
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
        private void UserDetails(string name)
        {

            txtID.Text = name;
            txtUser.Text = name;
            txtUser_R.Text = name;
            txtUser_A.Text = name;
            txtUser_S.Text = name;
            txtUser_L.Text = name;
            txtUser_P.Text = name;
            txtUserIdSBU.Text = name;
            LoadUserDetails();
            Load_UserCompanies();
            Load_UserDetails(ref txtUser_R, ref txtFullName_R, ref txtDesn_R, ref txtCat_R, ref txtDept_R, ref txtEmpID_R);
            LoadUserRole();


            //Approval 
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
                grvApprLevel.DataBind();
            }

            Load_UserDetails(ref txtUser_S, ref txtFullName_S, ref txtDesn_S, ref txtCat_S, ref txtDept_S, ref txtEmpID_S);
            //Get_SpecialUser_Perm
            DataTable DTN = CHNLSVC.Security.Get_SpecialUser_Perm(txtUser_S.Text.Trim());
            grdUserPerm.DataSource = null;
            grdUserPerm.AutoGenerateColumns = false;
            grdUserPerm.DataSource = DTN;
            grdUserPerm.DataBind();

            Load_UserDetails(ref txtUser_L, ref txtFullName_L, ref txtDesn_L, ref txtCat_L, ref txtDept_L, ref txtEmpID_L);
            LoadUserLoc();

            Load_UserDetails(ref txtUser_P, ref txtFullName_P, ref txtDesn_P, ref txtCat_P, ref txtDept_P, ref txtEmpID_P);
            LoadUserPC();


            DataTable SBUTbl = CHNLSVC.Security.GetSBU_User(null, txtUserIdSBU.Text, null);
            grdSbu.DataSource = SBUTbl;
            grdSbu.DataBind();
            UserPopoup.Hide();
        }
        private void SaveSystemUserRole()
        {
            try
            {
                errorDiv.Visible = false;
                successDiv.Visible = false;
                if (txtCom_R.Text.Trim() == "")
                {
                    lblWarn.Text = "Please select a company.";
                    errorDiv.Visible = true;
                    // MessageBox.Show("Please select a company.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtUser_R.Text.Trim() == "")
                {
                    lblWarn.Text = "Please select a user.";
                    errorDiv.Visible = true;
                    // MessageBox.Show("Please select a user.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtRoleID.Text.Trim() == "")
                {
                    lblWarn.Text = "Please select a role.";
                    errorDiv.Visible = true;
                    // MessageBox.Show("Please select a role.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //------------------------------------
                MasterCompany COM = CHNLSVC.General.GetCompByCode(txtCom_R.Text.Trim());
                if (COM == null)
                {
                    lblWarn.Text = "Invalid company code!";
                    errorDiv.Visible = true;
                    // MessageBox.Show("Invalid company code!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCom_R.Focus();
                    return;
                }
                //------------------------------------

                SystemUserRole _systemUserRole = new SystemUserRole();
                _systemUserRole.SER_COM_CD = txtCom_R.Text.Trim();
                _systemUserRole.SER_ROLE_ID = Convert.ToInt32(txtRoleID.Text.Trim());
                _systemUserRole.SER_USR_ID = txtUser_R.Text.Trim();
                _systemUserRole.Se_cre_by = Session["UserID"].ToString();
                _systemUserRole.Se_session_id = Session["SessionID"].ToString();


                int row_aff = CHNLSVC.Security.SaveSystemUserRole_NEW(_systemUserRole);

                LoadUserRole();
                lblWarn.Text = "Successfully updated!";
                successDiv.Visible = true;
                //MessageBox.Show("Successfully updated!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtCom_R.Text = "";
                txtRoleID.Text = "";
                txtRoleDesn.Text = "";
                // ClearUserRole();
            }
            catch (Exception ex)
            {
                lblWarn.Text = "Invalid company code!";
                errorDiv.Visible = true;

                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
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
            foreach (GridViewRow dgvr in gvUserLoc.Rows)
            {
                // DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                CheckBox chk = (CheckBox)dgvr.FindControl("selectchkU");
                if (chk.Checked)
                {


                    //company = ucLoactionSearch1.Company;
                    SystemUserLoc _systemUserLoc = new SystemUserLoc();
                    _systemUserLoc.SEL_COM_CD = (dgvr.FindControl("SEL_COM_CD") as Label).Text;
                    _systemUserLoc.SEL_LOC_CD = (dgvr.FindControl("SEL_LOC_CD") as Label).Text;
                    _systemUserLoc.SEL_USR_ID = txtUser_L.Text.Trim();

                    //  DataGridViewCheckBoxCell chk_def = dgvr.Cells["SEL_DEF_LOCCD"] as DataGridViewCheckBoxCell;
                    CheckBox chk_def = (CheckBox)dgvr.FindControl("SEL_DEF_LOCCD");
                    // bool isDefault = Convert.ToBoolean(chk_def.Value == DBNull.Value ? 0 : chk_def.Value);
                    _systemUserLoc.SEL_DEF_LOCCD = chk_def.Checked == true ? 1 : 0;

                    list.Add(_systemUserLoc);
                }
            }
            return list;
        }
        private List<SystemUserProf> GetSelectedProfitCenterList_toUpdate()
        {
            List<SystemUserProf> list = new List<SystemUserProf>();
            foreach (GridViewRow dgvr in gvUserPC.Rows)
            {
                CheckBox chk = (CheckBox)dgvr.FindControl("chkDelPc");
                //DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (chk.Checked == true)
                {
                    //company = ucLoactionSearch1.Company;
                    SystemUserProf _systemUserPC = new SystemUserProf();
                    _systemUserPC.Sup_com_cd = (dgvr.FindControl("p_SUP_COM_CD") as Label).Text;
                    _systemUserPC.Sup_pc_cd = (dgvr.FindControl("p_SUP_PC_CD") as Label).Text;
                    _systemUserPC.Sup_usr_id = txtUser_P.Text.Trim();

                    CheckBox chk_def = (CheckBox)dgvr.FindControl("p_SUP_DEF_PCCD");
                    // DataGridViewCheckBoxCell chk_def = dgvr.Cells["p_SUP_DEF_PCCD"] as DataGridViewCheckBoxCell;
                    bool isDefault = Convert.ToBoolean(chk_def.Checked);
                    _systemUserPC.Sup_def_pccd = isDefault;

                    list.Add(_systemUserPC);
                }
            }
            return list;
        }
        private List<SystemUserLoc> GetSelectedLocationList(out string company, out Int32 Default_count)
        {
            Default_count = 0;
            company = "";
            List<SystemUserLoc> list = new List<SystemUserLoc>();
            foreach (GridViewRow dgvr in grvLocs.Rows)
            {
                CheckBox chk = (CheckBox)dgvr.FindControl("selectchk");
                // GridViewCheckBoxCell chk = dgvr.Cells[0] as GridViewCheckBoxCell;
                if (chk != null & chk.Checked)
                {
                    // list.Add(dgvr.Cells["LOCATION"].Value.ToString());
                    //company = dgvr.Cells["l_SEL_COM_CD"].Value.ToString();
                    company = ucLoactionSearch.Company;
                    SystemUserLoc _systemUserLoc = new SystemUserLoc();
                    _systemUserLoc.SEL_COM_CD = ucLoactionSearch.Company;//dgvr.Cells["l_SEL_COM_CD"].Value.ToString();
                    _systemUserLoc.SEL_LOC_CD = (dgvr.FindControl("LOCATION") as Label).Text;
                    _systemUserLoc.SEL_USR_ID = txtUser_L.Text.Trim();

                    // DataGridViewCheckBoxCell chk_def = dgvr.Cells["Is_default"] as DataGridViewCheckBoxCell;
                    CheckBox chk_def = (CheckBox)dgvr.FindControl("Is_default");

                    //  bool isDefault = Convert.ToBoolean(chk_def.Value == DBNull.Value ? 0 : chk_def.Value);
                    ///bool isDefault = Convert.ToBoolean(chk_def);
                    if (chk_def != null & chk_def.Checked)
                    {
                        Default_count = Default_count + 1;
                    }
                    _systemUserLoc.SEL_DEF_LOCCD = chk_def.Checked == true ? 1 : 0;//(chkDefLoc.Checked == true) ? 1 : 0;
                    //if(chk_def.Checked==true){
                    //}

                    // _systemUserLoc.SEL_DEF_LOCCD 
                    list.Add(_systemUserLoc);
                }
            }
            return list;
        }
        protected void Load_UserDetails(ref TextBox userID, ref TextBox fullName, ref TextBox desctription, ref TextBox designation, ref TextBox department, ref TextBox empID)
        {
            try
            {
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(userID.Text.Trim());
                if (_systemUser == null)
                {
                    // MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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

                // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        #endregion
        #region Model Popoup Event
        protected void ImageSearch_Click(object sender, EventArgs e)
        {
            if (Label1.Text == "178")
            {
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_All_Users("178:|", cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;

                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "183")
            {
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_Designations("183:|", cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;

                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "SBU")
            {
                DataTable SbuTbl = CHNLSVC.Security.GetSBU_Company(txtCompanySBU.Text.Trim(), txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = SbuTbl;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "179")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SecUsrPermTp);
                DataTable _result = CHNLSVC.CommonSearch.Get_All_SecUsersPerimssionTypes(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "184")
            {
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_Departments("184:|", cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "69")
            {
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData("69:||||||Pc_HIRC_Company|", cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;

                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "SBUCompany")
            {
                dvResultUser.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);

                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                //_result.Columns.RemoveAt(1);
               // _result.AcceptChanges();
                dvResultUser.DataSource = _result;

                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "162")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_system_role(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;

                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "41")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "75")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "191")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermCode);
                DataTable _result = CHNLSVC.CommonSearch.Search_Approve_Permissions(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "192")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermLevelCode);
                DataTable _result = CHNLSVC.CommonSearch.Search_ApprovePermission_Levels(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
          
            if (Label1.Text == "178")
            {
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_All_SystemUsers("178:|", cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;

                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "183")
            {
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_Designations("183:|", cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;

                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "179")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SecUsrPermTp);
                DataTable _result = CHNLSVC.CommonSearch.Get_All_SecUsersPerimssionTypes(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "184")
            {
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_Departments("184:|", cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "69")
            {
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData("69:||||||Pc_HIRC_Company|", cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;

                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "162")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_system_role(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;

                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "41")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "75")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "191")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermCode);
                DataTable _result = CHNLSVC.CommonSearch.Search_Approve_Permissions(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "192")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermLevelCode);
                DataTable _result = CHNLSVC.CommonSearch.Search_ApprovePermission_Levels(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
        }
      
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            if (Label1.Text == "178")
            {
               
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_All_SystemUsers("178:|", cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;

                dvResultUser.DataBind();
              
                UserPopoup.Show();
                
               
            }

            else if (Label1.Text == "183")
            {
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_Designations("183:|", cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;

                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "184")
            {
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_Departments("184:|", cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "179")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SecUsrPermTp);
                DataTable _result = CHNLSVC.CommonSearch.Get_All_SecUsersPerimssionTypes(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "69")
            {
                dvResultUser.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);

                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;

                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "SBUCompany")
            {
                dvResultUser.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);

                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                //_result.Columns.RemoveAt(1);              
               // _result.AcceptChanges();
                dvResultUser.DataSource = _result;

                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "162")
            {
                Select_company = txtCom_R.Text.Trim().ToUpper();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_system_role(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;

                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "41")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "75")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "191")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermCode);
                DataTable _result = CHNLSVC.CommonSearch.Search_Approve_Permissions(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "192")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermLevelCode);
                DataTable _result = CHNLSVC.CommonSearch.Search_ApprovePermission_Levels(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "SBU")
            {
                DataTable SbuTbl = CHNLSVC.Security.GetSBU_Company(txtCompanySBU.Text.Trim(), txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = SbuTbl;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
           // UserPopoup.Show();
           
           
        }

        protected void dvResultUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvResultUser.PageIndex = e.NewPageIndex;
            if (Label1.Text == "178")
            {
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_All_SystemUsers("178:|", null, null);
                dvResultUser.DataSource = _result;

                dvResultUser.DataBind();
                UserPopoup.Show();
            }

            else if (Label1.Text == "183")
            {
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_Designations("183:|", null, null);
                dvResultUser.DataSource = _result;

                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "184")
            {
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_Departments("184:|", null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "179")
            {
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SecUsrPermTp);
                //DataTable _result = CHNLSVC.CommonSearch.Get_All_SecUsersPerimssionTypes(SearchParams, null, null);

                Select_company = txtPermCd.Text.Trim().ToUpper();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SecUsrPermTp);
                dvResultUser.DataSource = null;
                DataTable _result = new DataTable();
                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    _result = CHNLSVC.CommonSearch.Get_All_SecUsersPerimssionTypes(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.Trim());
               
                }
                else
                {
                    _result = CHNLSVC.CommonSearch.Get_All_SecUsersPerimssionTypes(SearchParams, null, null);
                }



                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "69")
            {
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData("69:||||||Pc_HIRC_Company|", null, null);
                dvResultUser.DataSource = _result;

                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "SBUCompany")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);

                //DataTable ComTbl = CHNLSVC.Security.GetUser_Company(txtUser.Text.Trim());
                //ComTbl.Columns.RemoveAt(0);
                //ComTbl.Columns.RemoveAt(2);
                //ComTbl.Columns.RemoveAt(2);
                //ComTbl.Columns.RemoveAt(2);
                //ComTbl.Columns.RemoveAt(2);
                //ComTbl.Columns.RemoveAt(2);
                //ComTbl.Columns.RemoveAt(2);
                //ComTbl.Columns.RemoveAt(2);
                //ComTbl.Columns.RemoveAt(1);
                //ComTbl.Columns[0].ColumnName = "Code";
                //ComTbl.AcceptChanges();
                dvResultUser.DataSource = _result;

                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "162")
            {
                Select_company = txtCom_R.Text.Trim().ToUpper();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
                dvResultUser.DataSource = null;
                DataTable _result = new DataTable();
                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    _result = CHNLSVC.CommonSearch.Get_system_role(SearchParams, cmbSearchbykey.SelectedValue, "%"+txtSearchbyword.Text.Trim());
                }
                else
                {
                    _result = CHNLSVC.CommonSearch.Get_system_role(SearchParams, null, null);
                }
                dvResultUser.DataSource = _result;
               
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "41")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "75")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "191")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermCode);
                DataTable _result = CHNLSVC.CommonSearch.Search_Approve_Permissions(SearchParams, null, null);

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "192")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermLevelCode);
                DataTable _result = CHNLSVC.CommonSearch.Search_ApprovePermission_Levels(SearchParams, null, null);

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "SBU")
            {
                DataTable SbuTbl = CHNLSVC.Security.GetSBU_Company(txtCompanySBU.Text.Trim(), null);
                dvResultUser.DataSource = SbuTbl;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            UserPopoup.Show();
        }

        protected void dvResultUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
        
            //if (Label1.Text == "162")
            //{
            //    Select_company = txtCom_R.Text.Trim().ToUpper();
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
            //    dvResultUser.DataSource = null;
            //    DataTable _result = CHNLSVC.CommonSearch.Get_system_role(SearchParams, null, null);
            //    dvResultUser.DataSource = _result;
               
            //    dvResultUser.DataBind();
            //    UserPopoup.CancelControlID = "btnClose";
            //}
           
            
           
            string name = dvResultUser.SelectedRow.Cells[1].Text;

           
            // int value = searchvalue;
            if (Label1.Text == "178")
            {
                txtID.Text = name;
                txtUser.Text = name;
                txtUser_R.Text = name;
                txtUser_A.Text = name;
                txtUser_S.Text = name;
                txtUser_L.Text = name;
                txtUser_P.Text = name;
                txtUserIdSBU.Text = name;
                LoadUserDetails();
                Load_UserCompanies();
                Load_UserDetails(ref txtUser_R, ref txtFullName_R, ref txtDesn_R, ref txtCat_R, ref txtDept_R, ref txtEmpID_R);
                LoadUserRole();


                //Approval 
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
                    grvApprLevel.DataBind();
                }

                Load_UserDetails(ref txtUser_S, ref txtFullName_S, ref txtDesn_S, ref txtCat_S, ref txtDept_S, ref txtEmpID_S);
                //Get_SpecialUser_Perm
                DataTable DTN = CHNLSVC.Security.Get_SpecialUser_Perm(txtUser_S.Text.Trim());
                grdUserPerm.DataSource = null;
                grdUserPerm.AutoGenerateColumns = false;
                grdUserPerm.DataSource = DTN;
                grdUserPerm.DataBind();

                Load_UserDetails(ref txtUser_L, ref txtFullName_L, ref txtDesn_L, ref txtCat_L, ref txtDept_L, ref txtEmpID_L);
                LoadUserLoc();
                Load_UserDetails(ref txtUser_P, ref txtFullName_P, ref txtDesn_P, ref txtCat_P, ref txtDept_P, ref txtEmpID_P);
                LoadUserPC();
                UserPopoup.Hide();
            }
            else if (Label1.Text == "183")
            {
                txtCate.Text = name;
                UserPopoup.Hide();
            }
            else if (Label1.Text == "184")
            {
                txtDept.Text = name; UserPopoup.Hide();
            }
            else if (Label1.Text == "69")
            {
                txtCom_C.Text = name;
                txtCom_R.Text = name;
                txtCom_S.Text = name;            
                rdoAnyParty.Checked = true;
                txtParty.Visible = false;
                btnSearchParty.Visible = false;


                if (txtCom_C.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtCom_C.Text.Trim());
                    if (com != null)
                    {
                        txtComDesc_C.Text = com.Mc_desc;
                      
                    }

                } UserPopoup.Hide();
                Label1.Text = "";
            }
            else if (Label1.Text == "162")
            {
                string Des = dvResultUser.SelectedRow.Cells[2].Text;
                txtRoleDesn.Text = Des;
                txtRoleID.Text = name;
                UserPopoup.Hide();
            }
            else if (Label1.Text == "191")
            {
                string Type = dvResultUser.SelectedRow.Cells[3].Text;
                txtAppr_Code.Text = name;
                //txtApprCdDesc.Text = Des;
                //txtFinApprLevel.Text = Type;
                if (txtAppr_Code.Text.Trim() != "")
                {
                    DataTable DT = CHNLSVC.Security.Get_Approve_PermissionInfo(txtAppr_Code.Text.Trim());

                    string Discription = DT.Rows[0]["sart_desc"].ToString();
                    txtApprCdDesc.Text = Discription;

                    string FinalApprLvl = DT.Rows[0]["sart_app_lvl"].ToString();
                    txtFinApprLevel.Text = FinalApprLvl;
                } UserPopoup.Hide();
            }
            else if (Label1.Text == "192")
            {

                txtPermLvl.Text = name; UserPopoup.Hide();
            }
            else if (Label1.Text == "179")
            {

                txtPermCd.Text = name;
                UserPopoup.Hide();
            }
            else if (Label1.Text == "41")
            {

                txtParty.Text = name;
                UserPopoup.Hide();
            }
            else if (Label1.Text == "75")
            {

                txtParty.Text = name;
                UserPopoup.Hide();
            }
            else if (Label1.Text == "SBUCompany")
            {
                txtCompanySBU.Text = name;
                string Des = dvResultUser.SelectedRow.Cells[2].Text;
                txtCDesSBU.Text = Des;
                txtSbuCode.Text = "";
                txtSBUDes.Text = "";
                //if (txtCompanySBU.Text.Trim() != "")
                //{
                //    ////TODO: LOAD COMPANY DESCRIPTION
                //    //MasterCompany com = CHNLSVC.General.GetCompByCode(txtCompanySBU.Text.Trim());
                //    //if (com != null)
                //    //{
                //    //    txtCDesSBU.Text = com.Mc_desc;
                //    //    txtSbuCode.Text = "";
                //    //    txtSBUDes.Text = "";
                //    //}
                //    txtCDesSBU.Text = name;
                //    txtSbuCode.Text = "";
                //    txtSBUDes.Text = "";
                //}
                DataTable SBUTbl = CHNLSVC.Security.GetSBU_User(txtCompanySBU.Text, txtUserIdSBU.Text,null);
                grdSbu.DataSource = SBUTbl;
                grdSbu.DataBind();
                UserPopoup.Hide();
            }
            else if (Label1.Text == "SBU")
            {
                string Des = dvResultUser.SelectedRow.Cells[2].Text;
                txtSbuCode.Text = name;
                txtSBUDes.Text = Des;
            }
            UserPopoup.CancelControlID = "btnClose";
            UserPopoup.Hide();
            Label1.Text = "";
        }


        #endregion 
        #region User

        protected void ImgbtnUID_Click(object sender, EventArgs e)
        {
           
            WarningUser.Visible = false;
            SuccessUser.Visible = false;
            errorDiv.Visible = false;
            successDiv.Visible = false;
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
            _result = CHNLSVC.CommonSearch.Get_All_Users(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();

            BindUCtrlDDLData(_result);

            UserPopoup.Show();

            Label1.Text = "178";
        }
        protected void ImgbtnDesignation_Click(object sender, EventArgs e)
        {
          
            string Password = txtPW.Text;
            txtPW.Attributes.Add("value", Password);

            string CPassword = txtCPW.Text;
            txtCPW.Attributes.Add("value", CPassword);


            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Designation);
            DataTable _result = CHNLSVC.CommonSearch.Get_Designations(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "183";
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }
        protected void ImgbtnDept_Click(object sender, EventArgs e)
        {
           
            string Password = txtPW.Text;
            txtPW.Attributes.Add("value", Password);

            string CPassword = txtCPW.Text;
            txtCPW.Attributes.Add("value", CPassword);

            txtSearchbyword.Text = "";
            //"184:|"
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Department);
            DataTable _result = CHNLSVC.CommonSearch.Get_Departments(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "184";
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void txtID_TextChanged(object sender, EventArgs e)
        {
            SystemUser _systemUser = null;
            _systemUser = CHNLSVC.Security.GetUserByUserID(txtID.Text);
            // Session["SearchID"] = _systemUser.Se_usr_id;
            if (_systemUser == null)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallConfirmBox", "NewConfirm();", true);
                // if (txtAlertValue.Value == "Yes")
                // {
                txtName.Focus();
                // return;
                // }
                txtID.Text = txtID.Text.ToUpper();



            }
            else {
                UserDetails(txtID.Text);
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                WarningUser.Visible = false;
                SuccessUser.Visible = false;
                //check user ID for empty
                if (string.IsNullOrEmpty(txtID.Text))
                {
                    lblWUser.Text = "Please select the user ID!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Please select the user ID!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtID.Focus();
                    return;
                }

                //check user name for empty
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    lblWUser.Text = "Please select the user name!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Please select the user name!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                #region KPMG - 6.8 System Administration Module Issues :: edit by Chamal 21/07/2014
                if (string.IsNullOrEmpty(txtMobile.Text))
                {
                    lblWUser.Text = "Please enter user mobile no!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Please enter user mobile no!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMobile.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPhone.Text))
                {
                    lblWUser.Text = "Please enter user phone no!";
                    WarningUser.Visible = true;
                    //MessageBox.Show("Please enter user phone no!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtEmpID.Text))
                {
                    lblWUser.Text = "Please enter user employee no(EPF)!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Please enter user employee no(EPF)!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmpID.Focus();
                    return;
                }


                if (txtEmpID.Text.Length != 7)
                {
                    lblWUser.Text = "Employee no should seven(7) characters!";
                    WarningUser.Visible = true;
                    //MessageBox.Show("Employee no should seven(7) characters!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmpID.Focus();
                    return;
                }

                if (IsNumeric(txtEmpID.Text) == false)
                {
                    lblWUser.Text = "Invalid employee no(EPF)!";
                    WarningUser.Visible = true;
                    //MessageBox.Show("Invalid employee no(EPF)!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtEmpID.Select();
                    txtEmpID.Focus();
                    return;
                }

                if (CheckSpaces(txtID.Text) == false)
                {
                    lblWUser.Text = "Invalid character(s) in user id!";
                    WarningUser.Visible = true;
                    //MessageBox.Show("Invalid character(s) in user id!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // txtID.SelectAll();
                    txtID.Focus();
                    return;
                }

                if (CheckSpaces(txtMobile.Text) == false)
                {
                    lblWUser.Text = "Invalid character(s) in mobile no!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Invalid character(s) in mobile no!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // txtMobile.SelectAll();
                    txtMobile.Focus();
                    return;
                }

                if (CheckSpaces(txtPhone.Text) == false)
                {
                    lblWUser.Text = "Invalid character(s) in phone no!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Invalid character(s) in phone no!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtPhone.SelectAll();
                    txtPhone.Focus();
                    return;
                }

                if (CheckSpaces(txtEmpID.Text) == false)
                {
                    lblWUser.Text = "Invalid character(s) in user employee no(EPF)!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Invalid character(s) in user employee no(EPF)!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtEmpID.SelectAll();
                    txtEmpID.Focus();
                }

                if (IsNumeric(txtMobile.Text) == false)
                {
                    lblWUser.Text = "Invalid mobile no!";
                    WarningUser.Visible = true;
                    //MessageBox.Show("Invalid mobile no!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtMobile.Select();
                    txtMobile.Focus();
                    return;
                }

                if (IsNumeric(txtPhone.Text) == false)
                {
                    lblWUser.Text = "Invalid phone no!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Invalid phone no!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ///txtPhone.Select();
                    txtPhone.Focus();
                    return;
                }
                #endregion

                //check email for empty
                if (txtEMail.Text == string.Empty || txtEMail.Text == "")
                {
                    lblWUser.Text = "Please enter user email!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Please enter user email!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEMail.Focus();
                    return;
                }

                //check @ sigh in email
                Boolean valid = BaseCls.IsValidEmail(txtEMail.Text.Trim());
                if (valid == false)
                {
                    lblWUser.Text = "Invalid email address";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Invalid email address", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEMail.Focus();
                    return;
                }

                Boolean _isValid = IsValidMobileOrLandNo(txtMobile.Text.Trim());
                if (_isValid == false)
                {
                    lblWUser.Text = "Invalid mobile number.";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Invalid mobile number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMobile.Focus();
                    return;
                }

                Boolean _isValidPHN = IsValidMobileOrLandNo(txtPhone.Text.Trim());
                if (_isValidPHN == false)
                {
                    lblWUser.Text = "Invalid phone number.";
                    WarningUser.Visible = true;
                    //  MessageBox.Show("Invalid phone number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    return;
                }

                if (txtEmpID.Text == string.Empty || txtEmpID.Text == "")
                {
                    lblWUser.Text = "Please enter employee ID!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Please enter employee ID!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmpID.Focus();
                    return;
                }
                //check is domain then domain id exsist
                if (chkIsDomain.Checked == true)
                {
                    if (txtDomainID.Text == string.Empty || txtDomainID.Text == "")
                    {
                        lblWUser.Text = "Please enter domain id!";
                        WarningUser.Visible = true;
                        // MessageBox.Show("Please enter domain id!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtDomainID.Focus();
                        return;
                    }
                }

                // check pw exprire is set then pw valid period
                int outputvalue = 0;
                bool isNumber = false;

                //if (chkPWExpire.Checked == true)
                //{
                //    if (txtValid.Text == string.Empty || txtValid.Text == "")
                //    {
                //        lblWUser.Text = "Please enter PW valid period!";
                //        WarningUser.Visible = true;
                //        // MessageBox.Show("Please enter PW valid period!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        txtValid.Focus();
                //        return;
                //    }
                //    else
                //    {
                //        isNumber = int.TryParse(txtValid.Text, out outputvalue);
                //        {
                //            if (!isNumber)
                //            {
                //                lblWUser.Text = "Please enter PW valid period!";
                //                WarningUser.Visible = true;
                //                // MessageBox.Show("PW valid period should be numeric!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //                txtValid.Text = "";
                //                txtValid.Focus();
                //                return;
                //            }
                //        }
                //    }
                //}


                //Check password for empty
                if (txtPW.Text == string.Empty || txtPW.Text == "")
                {
                    lblWUser.Text = "Please enter default password!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Please enter default password!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPW.Focus();
                    return;
                }

                //Check confirm password for empty
                if (txtCPW.Text == string.Empty || txtCPW.Text == "")
                {
                    lblWUser.Text = "Please enter confirm password!";
                    WarningUser.Visible = true;
                    //MessageBox.Show("Please enter confirm password!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCPW.Focus();
                    return;
                }

                //Validate enter password
                if (txtPW.Text != txtCPW.Text)
                {
                    lblWUser.Text = "Password validation is fail!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Password validation is fail!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCPW.Text = "";
                    txtCPW.Focus();
                    return;
                }

                //Check Pw valid period
                if (string.IsNullOrEmpty(txtValid.Text))
                {
                    lblWUser.Text = "Please enter password valid period!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Please enter password valid period!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtValid.Text = "0";
                    txtValid.Focus();
                    return;
                }

                //Check user category select status
                if (txtCate.Text.Trim() == "")
                {
                    lblWUser.Text = "Please select user designation!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Please select user designation!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCate.Focus();
                    return;
                }

                //Check user department
                if (txtDept.Text.Trim() == "")
                {
                    lblWUser.Text = "Please select user department!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Please select user department!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDept.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtSunUserID.Text))
                {
                    txtSunUserID.Text = "N/A";
                }
                if (txtSaveconformmessageValue.Value == "Yes")
                {
                    SaveSystemUser();
                }
                
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
                }
                txtSaveconformmessageValue.Value = "";
            }
            catch (Exception ex)
            {

                CHNLSVC.CloseChannel();
                // MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnUpdateAdvnDet_Click(object sender, EventArgs e)
        {
            try
            {
                WarningUser.Visible = false;
                SuccessUser.Visible = false;
                //check user ID for empty
                if (txtID.Text == string.Empty || txtID.Text == "")
                {
                    lblWUser.Text = "Please select the user ID first!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Please select the user ID first!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtID.Focus();
                    return;
                }

                //check user name for empty
                if (txtName.Text == string.Empty || txtName.Text == "")
                {
                    lblWUser.Text = "Please select the user name!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Please select the user name!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                //check description for empty
                if (txtDescription.Text == string.Empty || txtDescription.Text == "")
                {
                    lblWUser.Text = "Please enter user description!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Please enter user description!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDescription.Focus();
                    return;
                }

                //check email for empty
                if (txtEMail.Text == string.Empty || txtEMail.Text == "")
                {
                    lblWUser.Text = "Please enter user email!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Please enter user email!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEMail.Focus();
                    return;
                }

                #region KPMG - 6.8 System Administration Module Issues :: edit by Chamal 21/07/2014
                if (string.IsNullOrEmpty(txtMobile.Text))
                {
                    lblWUser.Text = "Please enter user mobile no!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Please enter user mobile no!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMobile.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPhone.Text))
                {
                    lblWUser.Text = "Please enter user phone no!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Please enter user phone no!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtEmpID.Text))
                {
                    lblWUser.Text = "Please enter user employee no(EPF)!";
                    WarningUser.Visible = true;
                    //MessageBox.Show("Please enter user employee no(EPF)!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmpID.Focus();
                    return;
                }


                if (txtEmpID.Text.Length != 7)
                {
                    lblWUser.Text = "Employee no should seven(7) characters!";
                    WarningUser.Visible = true;
                    //MessageBox.Show("Employee no should seven(7) characters!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmpID.Focus();
                    return;
                }

                if (IsNumeric(txtEmpID.Text) == false)
                {
                    lblWUser.Text = "Invalid employee no(EPF)!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Invalid employee no(EPF)!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // txtEmpID.Select();
                    txtEmpID.Focus();
                    return;
                }

                if (CheckSpaces(txtID.Text) == false)
                {
                    lblWUser.Text = "Invalid character(s) in user id!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Invalid character(s) in user id!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // txtID.SelectAll();
                    txtID.Focus();
                    return;
                }

                if (CheckSpaces(txtMobile.Text) == false)
                {
                    lblWUser.Text = "Invalid character(s) in mobile no!";
                    WarningUser.Visible = true;
                    //MessageBox.Show("Invalid character(s) in mobile no!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtMobile.SelectAll();
                    txtMobile.Focus();
                    return;
                }

                if (CheckSpaces(txtPhone.Text) == false)
                {
                    lblWUser.Text = "Invalid character(s) in phone no!";
                    WarningUser.Visible = true;
                    //MessageBox.Show("Invalid character(s) in phone no!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtPhone.SelectAll();
                    txtPhone.Focus();
                    return;
                }

                if (CheckSpaces(txtEmpID.Text) == false)
                {
                    lblWUser.Text = "Invalid character(s) in user employee no(EPF)!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Invalid character(s) in user employee no(EPF)!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // txtEmpID.SelectAll();
                    txtEmpID.Focus();
                    return;
                }

                if (IsNumeric(txtMobile.Text) == false)
                {
                    lblWUser.Text = "Invalid mobile no!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Invalid mobile no!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtMobile.Select();
                    txtMobile.Focus();
                    return;
                }

                if (IsNumeric(txtPhone.Text) == false)
                {
                    //  MessageBox.Show("Invalid phone no!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtPhone.Select();
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
                        lblWUser.Text = "Please enter domain ID!";
                        WarningUser.Visible = true;
                        ////MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter domain id!");
                        // MessageBox.Show("Please enter domain ID!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        lblWUser.Text = "Please enter PW valid period!";
                        WarningUser.Visible = true;
                        // // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter PW valid period!");
                        // MessageBox.Show("Please enter PW valid period!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtValid.Focus();
                        return;
                    }
                    else
                    {
                        isNumber = int.TryParse(txtValid.Text, out outputvalue);
                        {
                            if (!isNumber)
                            {
                                lblWUser.Text = "PW valid period should be numeric!";
                                WarningUser.Visible = true;
                                // // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "PW valid period should be numeric!");
                                //MessageBox.Show("PW valid period should be numeric!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtValid.Text = "";
                                txtValid.Focus();
                                return;
                            }
                        }
                    }
                }

                if (txtPW.Text == string.Empty || txtPW.Text == "")
                {
                    lblWUser.Text = "Please enter default password!";
                    WarningUser.Visible = true;
                    // MessageBox.Show("Please enter default password!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPW.Focus();
                    return;
                }

                //Check confirm password for empty
                if (txtCPW.Text == string.Empty || txtCPW.Text == "")
                {
                    lblWUser.Text = "Please enter confirm password!";
                    WarningUser.Visible = true;
                    //MessageBox.Show("Please enter confirm password!", "New User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCPW.Focus();
                    return;
                }


                //Validate enter password
                if (txtPW.Text != txtCPW.Text)
                {
                    lblWUser.Text = "Password validation is fail!";
                    WarningUser.Visible = true;
                    // // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Password validation is fail!");
                    //MessageBox.Show("Password validation is fail!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCPW.Text = "";
                    txtCPW.Focus();
                    return;
                }

                //Check Pw valid period
                if (string.IsNullOrEmpty(txtValid.Text))
                {
                    lblWUser.Text = "Please enter password valid period!";
                    WarningUser.Visible = true;
                    ////MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter password valid period!");
                    //MessageBox.Show("Please enter password valid period!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtValid.Text = "0";
                    txtValid.Focus();
                    return;
                }

                //Check user category select status
                if (txtCate.Text.Trim() == "")
                {
                    lblWUser.Text = "Please select user designation!";
                    WarningUser.Visible = true;
                    // // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select user designation!");
                    //MessageBox.Show("Please enter password valid period!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCate.Focus();
                    return;
                }
                int distance;
                if (!(int.TryParse(txtMobile.Text, out distance)))
                {
                    // it's a valid integer => you could use the distance variable here
                    //lblWUser.Text = "only allow Numeric Value!";
                    lblWUser.Text = "Mobile No only allow Numeric Value!";
                    WarningUser.Visible = true;
                    //WarningUser.Visible = true;
                    return;
                }
                if (!(int.TryParse(txtPhone.Text, out distance)))
                {
                    // it's a valid integer => you could use the distance variable here
                    //lblWUser.Text = "only allow Numeric Value!";
                    lblWUser.Text = "Phone No only allow Numeric Value!";
                    WarningUser.Visible = true;
                    //WarningUser.Visible = true;
                    return;
                }

               // Check user department
                if (txtDept.Text.Trim() == "")
                {
                    lblWUser.Text = "Please select user department!";
                    WarningUser.Visible = true;
                    // // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select user department!");
                    // MessageBox.Show("Please enter password valid period!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDept.Focus();
                    return;
                }
                if (txtSaveconformmessageValue.Value == "Yes")
                {
                    UpdateSystemUser();
                    clearall();

                }
            }
            catch (Exception ex)
            {
                lblWUser.Text = "Error Occurred while processing..";
                WarningUser.Visible = true;
                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lblUClear_Click(object sender, EventArgs e)
        {
            //string confirmValue = Request.Form["ClearConfirm_value"];
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                try
                {
                    Response.Redirect(Request.RawUrl, false);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
           
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }
            txtClearlconformmessageValue.Value = "";
        }

        #endregion
        #region Assign Compnay
        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            
            WarningUser.Visible = false;
            SuccessUser.Visible = false;
            errorDiv.Visible = false;
            successDiv.Visible = false;
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
            _result = CHNLSVC.CommonSearch.Get_All_SystemUsers(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();

            BindUCtrlDDLData(_result);

            UserPopoup.Show();

            Label1.Text = "178";
        }

        protected void txtUser_TextChanged(object sender, EventArgs e)
        {
            if (txtUser.Text.Trim() != "")
            {
                //----------------------------------------------------------------
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser.Text.Trim());
                if (_systemUser == null)
                {
                    // MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AlertID.Text = "Invalid username!";
                    tests.Visible = true;
                    txtUser.Text = "";
                    txtFullName.Text = "";
                    txtDesn.Text = "";
                    txtCat.Text = "";
                    txtDept_.Text = "";
                    //txtEmpID_.Text = "";
                    return;
                }
                else
                {
                    Load_UserDetails(ref txtUser, ref txtFullName, ref txtDesn, ref txtCat, ref txtDept_, ref txtEmpID_);
                    //txtCom_C.Focus();
                }
                //----------------------------------------------------------------                    
                // txtName.Focus();
                tests.Visible = false ;
                UserDetails(txtUser.Text);
            }
           
        }

        protected void txtUserIdSBU_TextChanged(object sender, EventArgs e)
        {
            if (txtUserIdSBU.Text.Trim() != "")
            {
                //----------------------------------------------------------------
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(txtUserIdSBU.Text.Trim());
                if (_systemUser == null)
                {
                    // MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AlertID.Text = "Invalid username!";
                    tests.Visible = true;
                    txtUser.Text = "";
                    txtFullName.Text = "";
                    txtDesn.Text = "";
                    txtCat.Text = "";
                    txtDept_.Text = "";
                    //txtEmpID_.Text = "";
                    return;
                }
                else
                {
                    Load_UserDetails(ref txtUser, ref txtFullName, ref txtDesn, ref txtCat, ref txtDept_, ref txtEmpID_);
                    //txtCom_C.Focus();
                }
                //----------------------------------------------------------------                    
                // txtName.Focus();
                tests.Visible = false;
                UserDetails(txtUserIdSBU.Text);
            }

        }
        protected void ImgbtnCompany_Click(object sender, EventArgs e)
        {
           
            WarningUser.Visible = false;
            SuccessUser.Visible = false;
            tests.Visible = false;
            DivSuccess.Visible = false;
            errorDiv.Visible = false;
            successDiv.Visible = false;
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);

          

            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "69";
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

       
        protected void btnDeleteCom_Click(object sender, EventArgs e)
        {
            tests.Visible = false;
            DivSuccess.Visible = false;
            WarningUser.Visible = false;
            SuccessUser.Visible = false;
            bool ISDelete = false;
            try
            {
                ////------------------------------------
                //MasterCompany COM = CHNLSVC.General.GetCompByCode(txtCom_C.Text.Trim());
                //if (COM == null)
                //{
                //    DisplayMessages("Invalid company code!");
                //    //MessageBox.Show("Invalid company code!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    txtCom_C.Focus();
                //    return;
                //}
                ////------------------------------------
                //DeleteSystemUserComp();
                if (txtDeleteconformmessageValue.Value == "Yes")
                {
                    if (!(txtFullName.Text == ""))
                    {
                        foreach (GridViewRow dgvr in grdUserComp.Rows)
                        {
                            CheckBox chk = (CheckBox)dgvr.FindControl("ChkUserComr");
                            // DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;

                            string company = (dgvr.FindControl("CompanyCode") as Label).Text;
                            if (chk != null & chk.Checked)
                            {
                                //string confirmValue = Request.Form["DeleteConfirm_value"];
                                if (txtDeleteconformmessageValue.Value == "Yes")
                                {
                                    DeleteSystemUserComp(txtUser.Text, company);
                                    ISDelete = true;
                                }
                                else
                                {
                                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
                                }
                                txtDeleteconformmessageValue.Value = "";
                            }

                        }
                        if (ISDelete == false)
                        {
                            DivSuccess.Visible = false;
                            //AlertID.Text = "Please select a company code.";
                            tests.Visible = true;
                        }
                    }
                    else
                    {
                        DivSuccess.Visible = false;
                        AlertID.Text = "Please select a User.";
                        tests.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {

                // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
       
           
        protected void btnAddNewCom_Click(object sender, EventArgs e)
        {
  
            try
            {
                WarningUser.Visible = false;
                SuccessUser.Visible = false;
                tests.Visible = false;
                DivSuccess.Visible = false;
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser.Text.Trim());

                if (txtSaveconformmessageValue.Value == "Yes")
                {
                if (_systemUser == null)
                {
                    AlertID.Text = "Invalid username!";
                    tests.Visible = true;
                   // DisplayMessages("Invalid username!");
                    //MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //------------------------------------
                MasterCompany COM = CHNLSVC.General.GetCompByCode(txtCom_C.Text.Trim());
                if (COM == null)
                {
                    AlertID.Text = "Invalid company code!";
                    tests.Visible = true;
                    txtCom_C.Text = "";
                   // DisplayMessages("Invalid company code!");
                    // MessageBox.Show("Invalid company code!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCom_C.Focus();
                    return;
                }
                //------------------------------------
                if (chkDefault.Checked == true)
                {
                    Int16 is_Def_Comp = CHNLSVC.Security.Check_User_Def_Comp(txtUser.Text.Trim());
                    if (is_Def_Comp == 1)
                    {
                        AlertID.Text = "More than one default company cannot be assigned";
                        tests.Visible = true;
                       // DisplayMessages("More than one default company cannot be assigned");
                        // MessageBox.Show("More than one default company cannot be assigned", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }
                          string confirmValue = Request.Form["confirm_value"];
                          

                              SaveSystemUserCompany();
                          }
                         

                          txtSaveconformmessageValue.Value = "";
            }
            catch (Exception ex)
            {
                AlertID.Text = "Error Occurred while processing..";
                tests.Visible = true;
                //  MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

      
        #endregion
        #region Role
        protected void txtUser_R_TextChanged(object sender, EventArgs e)
        {
            if (txtUser_R.Text.Trim() != "")
            {
                //----------------------------------------------------------------
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser_R.Text.Trim());
                if (_systemUser == null)
                {
                    // MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblWarn.Text = "Invalid username!";
                    errorDiv.Visible = true;
                    txtUser.Text = "";
                    txtFullName.Text = "";
                    txtDesn.Text = "";
                    txtCat.Text = "";
                    txtDept_.Text = "";
                    //txtEmpID_.Text = "";
                    return;
                }
                else
                {
                    Load_UserDetails(ref txtUser, ref txtFullName, ref txtDesn, ref txtCat, ref txtDept_, ref txtEmpID_);
                    txtCom_C.Focus();
                }
                //----------------------------------------------------------------                    
                // txtName.Focus();
                errorDiv.Visible = false; ;
                UserDetails(txtUser_R.Text);
            }
          
        }
        protected void btnAddNewRole_Click(object sender, EventArgs e)
        {
            errorDiv.Visible = false;
            successDiv.Visible = false;
            try
            {
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser_R.Text.Trim());
                if (_systemUser == null)
                {
                    lblWarn.Text = "Invalid username!";
                    errorDiv.Visible = true;
                    // MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                  //string confirmValue = Request.Form["confirm_value"];
                  if (txtSaveconformmessageValue.Value == "Yes")
                  {
                      SaveSystemUserRole();
                      LoadUserRole();
                     
                  }
                  else
                  {
                      this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cacel Save!')", true);
                  }
                  txtSaveconformmessageValue.Value = "";
            }
            catch (Exception ex)
            {

                // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        protected void btnDeleteRole_Click(object sender, EventArgs e)
        {
            errorDiv.Visible = false;
            successDiv.Visible = false;
            
            try
            {
                if (!(txtFullName_R.Text == ""))
                {

                    foreach (GridViewRow dgvr in gvUserRole.Rows)
                    {
                        CheckBox chk = (CheckBox)dgvr.FindControl("ChkUserRole");
                        // DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;

                        string company = (dgvr.FindControl("SERM_COM_CD") as Label).Text;
                        string Code = (dgvr.FindControl("SERM_ROLE_ID") as Label).Text;
                        if (chk != null & chk.Checked)
                        {

                            //string confirmValue = Request.Form["DeleteConfirm_value"];
                            if (txtDeleteconformmessageValue.Value == "Yes")
                            {
                                DeleteSystemUserRole(company, Code);
                                LoadUserRole();
                            }
                            else
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cacel Delete!')", true);
                            }
                            txtDeleteconformmessageValue.Value = "";
                        }
                        else
                        {
                            lblWarn.Text = "Please select a Company.";
                            errorDiv.Visible = true;
                        }
                    }
                }
                else
                {
                    lblWarn.Text = "Please select a user.";
                    errorDiv.Visible = true;
                }
                
            }
            catch (Exception ex)
            {

                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }

            finally
            {
                CHNLSVC.CloseAllChannels();
            }
                
        }
        protected void LinkButton19_Click(object sender, EventArgs e)
        {
             errorDiv.Visible = false;
            successDiv.Visible = false;
            txtSearchbyword.Text = "";
            if (txtCom_R.Text.Trim() == "")
            {
                lblWarn.Text = "Please select company";
                errorDiv.Visible = true;
                // MessageBox.Show("Please select company", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Select_company = txtCom_R.Text.Trim().ToUpper();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
                dvResultUser.DataSource = null;
                DataTable _result = CHNLSVC.CommonSearch.Get_system_role(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                Label1.Text = "162";
                BindUCtrlDDLData(_result);
                dvResultUser.DataBind();
                UserPopoup.Show();
                

              



                //Select_company = txtCom_R.Text.Trim().ToUpper();
                //dvResultUser.DataSource = null;
                ////string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);
                ////DataTable _result = CHNLSVC.CommonSearch.Get_system_role(SearchParams, null, null);


                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserRole);

                //DataTable _result = CHNLSVC.CommonSearch.Get_system_role(SearchParams, null, null);
                //dvResultUser.DataSource = _result;

                
              
               
               
            }
        }
        #endregion
        #region Special Permision
        protected void btnSearchPerm_Click(object sender, EventArgs e)
        {
            try
            {
                WarnnPer.Visible = false;
                SuccesPer.Visible = false;

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SecUsrPermTp);
                DataTable _result = CHNLSVC.CommonSearch.Get_All_SecUsersPerimssionTypes(SearchParams, null, null);

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                BindUCtrlDDLData(_result);

                Label1.Text = "179";

                UserPopoup.Show();
                txtPermCd.Focus();
            }
            catch (Exception ex)
            {

                // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void rdoAnyParty_CheckedChanged(object sender, EventArgs e)
        {
            WarnnPer.Visible = false;
            SuccesPer.Visible = false;
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

        protected void rdoLoc_CheckedChanged(object sender, EventArgs e)
        {
            WarnnPer.Visible = false;
            SuccesPer.Visible = false;
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

        protected void rdoPC_CheckedChanged(object sender, EventArgs e)
        {
            WarnnPer.Visible = false;
            SuccesPer.Visible = false;
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

        protected void btnSearchParty_Click(object sender, EventArgs e)
        {
            try
            {
                WarnnPer.Visible = false;
                SuccesPer.Visible = false;
                txtParty.Text = "";
                if (txtCom_S.Text.Trim() == "")
                {
                    lblWper.Text = "Please select company";
                    WarnnPer.Visible = true;
                    // MessageBox.Show("Please select company", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (rdoLoc.Checked == true)
                {



                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                    DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);

                    dvResultUser.DataSource = _result;
                    dvResultUser.DataBind();
                    BindUCtrlDDLData(_result);

                    Label1.Text = "41";

                    UserPopoup.Show();
                    txtParty.Focus();
                }
                else if (rdoPC.Checked == true)
                {
                    if (txtCom_S.Text.Trim() == "")
                    {
                        lblWper.Text = "Enter Company Code";
                        WarnnPer.Visible = true;
                        // MessageBox.Show("Enter Company Code");
                        return;
                    }




                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);

                    dvResultUser.DataSource = _result;
                    dvResultUser.DataBind();
                    BindUCtrlDDLData(_result);

                    Label1.Text = "75";

                    UserPopoup.Show();
                    txtParty.Focus();
                }
            }
            catch (Exception ex)
            {

                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnAddUserPerm_Click(object sender, EventArgs e)
        {
            try
            {
                WarnnPer.Visible = false;
                SuccesPer.Visible = false;
                if (txtUser_S.Text.Trim() == "")
                {
                    lblWper.Text = "Please select a user";
                    WarnnPer.Visible = true;
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "CallConfirmBox", "CallConfirmBox();", true);
                    //MessageBox.Show("Please select a user", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //----------------------------------------------------------------
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser_S.Text.Trim());
                if (_systemUser == null)
                {
                    lblWper.Text = "Invalid username!";
                    WarnnPer.Visible = true;
                    //MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //----------------------------------------------------------------
                if (txtPermCd.Text.Trim() == "")
                {
                    lblWper.Text = "Please select a permission";
                  
                    WarnnPer.Visible = true;
                    // MessageBox.Show("Please select a permission", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (txtParty.Text.Trim() == "")
                {
                    if (rdoLoc.Checked == true)
                    {
                        lblWper.Text = "Please select the location";
                        WarnnPer.Visible = true;
                        // MessageBox.Show("Please select the location", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (rdoPC.Checked == true)
                    {
                        lblWper.Text = "Please select the profit center";
                        WarnnPer.Visible = true;
                        // MessageBox.Show("Please select the profit center", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        lblWper.Text = "Invalid company code!";
                        WarnnPer.Visible = true;
                        // MessageBox.Show("Invalid company code!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                usrPerm.Seur_cre_by = Session["UserID"].ToString();
                usrPerm.Seur_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;
                usrPerm.Seur_mod_by = CHNLSVC.Security.GetServerDateTime().Date;
                usrPerm.Seur_mod_dt = Session["UserID"].ToString();
                usrPerm.Seur_usr_id = txtUser_S.Text.Trim();
                if (rdoLoc.Checked == true || rdoPC.Checked == true)
                {
                    usrPerm.Seur_party = txtParty.Text.Trim() != "" ? txtParty.Text.Trim() : "";
                }
                else
                {
                    usrPerm.Seur_party = "";
                }

                //if (MessageBox.Show("Are you sure to save?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                //{
                //    return;
                //}
                //     ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage();", true);
                //string confirmValue = Request.Form["confirm_value"];
                //if (confirmValue == "No")
                //{
                //    return;
                //}
                //string confirmValue = Request.Form["confirm_value"];
                if (txtSaveconformmessageValue.Value == "Yes")
                {
                    Int32 effect = CHNLSVC.Security.Save_SecUserPerm(usrPerm);
                    if (effect > 0)
                    {
                        lblSPer.Text = "Sucessfully Saved!.";
                        SuccesPer.Visible = true;
                        //MessageBox.Show("Sucessfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DataTable DT = CHNLSVC.Security.Get_SpecialUser_Perm(txtUser_S.Text.Trim());
                        grdUserPerm.DataSource = null;
                        grdUserPerm.AutoGenerateColumns = false;
                        grdUserPerm.DataSource = DT;
                        grdUserPerm.DataBind();

                    }
                    else
                    {
                        lblWper.Text = "Not created.";
                        WarnnPer.Visible = true;
                        // MessageBox.Show("Not created.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
                }
                //
                txtSaveconformmessageValue.Value = "";
               
            }
            catch (Exception ex)
            {

                // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnDeleteUserPerm_Click(object sender, EventArgs e)
        {
            try
            {
                WarnnPer.Visible = false;
                SuccesPer.Visible = false;
                // grdUserPerm.EndEdit();
                List<SecUserPerm> del_permList = new List<SecUserPerm>();

                List<string> list = new List<string>();
                foreach (GridViewRow dgvr in grdUserPerm.Rows)
                {
                    CheckBox chk = (CheckBox)dgvr.FindControl("ChkPer");
                    // DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;

                    if (chk != null & chk.Checked)
                    {
                        SecUserPerm usrPerm = new SecUserPerm();
                        usrPerm.Seur_act = true;
                        usrPerm.Seur_cd = (dgvr.FindControl("SEUR_CD") as Label).Text;
                        usrPerm.Seur_com = (dgvr.FindControl("SEUR_COM") as Label).Text;

                        usrPerm.Seur_mod_by = CHNLSVC.Security.GetServerDateTime().Date;
                        usrPerm.Seur_mod_dt = Session["UserID"].ToString();
                        usrPerm.Seur_usr_id = txtUser_S.Text.Trim();
                        usrPerm.Seur_party = (dgvr.FindControl("SEUR_PARTY") as Label).Text;

                        del_permList.Add(usrPerm);
                    }
                }

                if (del_permList.Count < 1)
                {
                    lblWper.Text = "Please select permissions to delete!";
                    WarnnPer.Visible = true;
                    //MessageBox.Show("Please select permissions to delete!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //TODO: DELETE
                 //string confirmValue = Request.Form["DeleteConfirm_value"];
                 if (txtDeleteconformmessageValue.Value == "Yes")
                 {
                     Int32 effect = CHNLSVC.Security.Inactivate_SecUserPerm(del_permList);
                     if (effect > 0)
                     {
                         lblSPer.Text = "Successfully Updated!";
                         SuccesPer.Visible = true;
                         //MessageBox.Show("Successfully Updated!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallConfirmBox", "CallConfirmBox();", true);
                         DataTable DT = CHNLSVC.Security.Get_SpecialUser_Perm(txtUser_S.Text.Trim());
                         grdUserPerm.DataSource = null;
                         grdUserPerm.AutoGenerateColumns = false;
                         grdUserPerm.DataSource = DT;
                         grdUserPerm.DataBind();
                         return;
                     }
                     else
                     {
                         lblWper.Text = "No records updated!";
                         WarnnPer.Visible = true;
                         //MessageBox.Show("No records updated!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                     }
                 }
                 else
                 {
                     this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cacel Delete!')", true);
                 }
                 txtDeleteconformmessageValue.Value = "";
            }
            catch (Exception ex)
            {

                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        #endregion
        protected void lbtnlayplaneclose_Click(object sender, EventArgs e)
        {
            MsgClear();
        }
        protected void btnSearchApprPermCode_Click(object sender, EventArgs e)
        {
          
            AprovalWarning.Visible = false;
            ApprovalSuccess.Visible = false;
            try
            {


                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermCode);
                DataTable _result = CHNLSVC.CommonSearch.Search_Approve_Permissions(SearchParams, null, null);

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                BindUCtrlDDLData(_result);
                UserPopoup.Show();

                Label1.Text = "191";

                txtAppr_Code.Focus();
            }
            catch (Exception ex)
            {
                lblWApp.Text = "Error Occurred while processing.";
                AprovalWarning.Visible = true;
                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnSearchApprLvl_Click(object sender, EventArgs e)
        {
           
            AprovalWarning.Visible = false;
            ApprovalSuccess.Visible = false;
            try
            {
                if (txtAppr_Code.Text.Trim() == "")
                {
                    lblWApp.Text = "Please select approval code first!.";
                    AprovalWarning.Visible = true;
                    // MessageBox.Show("Please select approval code first!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermLevelCode);
                DataTable _result = CHNLSVC.CommonSearch.Search_ApprovePermission_Levels(SearchParams, null, null);

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                BindUCtrlDDLData(_result);
                UserPopoup.Show();

                Label1.Text = "192";

                txtPermLvl.Focus();
            }
            catch (Exception ex)
            {
                lblWApp.Text = "Error Occurred while processing.";
                AprovalWarning.Visible = true;
                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnAddAppPerm_Click(object sender, EventArgs e)
        {
          string _FLevel,_PerCode;
          if (grvApprLevel.Rows.Count <= 0)
          {
              lblWApp.Text = "Cannot find any permission.!";
              AprovalWarning.Visible = true;
              //MessageBox.Show("Cannot find any permission.", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Information);
              return;
          }
          if (txtFullName_A.Text == null)
          {
              lblWApp.Text = "Please select a user.!";
              AprovalWarning.Visible = true;
              return;
          }
          Boolean _appItm = false;
          foreach (GridViewRow row in grvApprLevel.Rows)
          {
              //DataGridViewCheckBoxCell chk = row.Cells["col_Chk"] as DataGridViewCheckBoxCell;
              CheckBox chk = (CheckBox)row.FindControl("col_Chk");
              if (chk.Checked == true)
              {
                  _appItm = true;
                  goto L4;
              }
          }
      L4:

          if (_appItm == false)
          {
              lblWApp.Text = "Please select permission which need to de-activate.!";
              AprovalWarning.Visible = true;
              // MessageBox.Show("Please select permission which need to de-activate.", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Information);
              return;
          }

            //if (_appItm == false)
            //{
            //    lblWApp.Text = "Please select permission which need to activate.!";
            //    AprovalWarning.Visible = true;
            //    // MessageBox.Show("Please select permission which need to de-activate.", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            foreach (GridViewRow newRow in grvApprLevel.Rows)
            {
                // DataGridViewCheckBoxCell chk = newRow.Cells["col_Chk"] as DataGridViewCheckBoxCell;
                CheckBox chk = (CheckBox)newRow.FindControl("col_Chk");
                if (chk.Checked == true)
                {

                    // _perCd = newRow.Cells["saup_prem_cd"].Value.ToString();
                    _FLevel = (newRow.FindControl("sart_app_lvl") as Label).Text;
                    _PerCode = (newRow.FindControl("saup_prem_cd") as Label).Text;     
                    AprovalWarning.Visible = false;
                    ApprovalSuccess.Visible = false;
                    SecApproveUserPerm appUsrPerm = new SecApproveUserPerm();
                    appUsrPerm.Saup_act = true;
                    appUsrPerm.Saup_cre_by = Session["UserID"].ToString();
                    appUsrPerm.Saup_cre_when = CHNLSVC.Security.GetServerDateTime().Date;
                    appUsrPerm.Saup_max_app_limit = Convert.ToInt32(_FLevel);//txtFinApprLevel.Text.Trim()
                    appUsrPerm.Saup_mod_by = Session["UserID"].ToString();
                    appUsrPerm.Saup_mod_when = appUsrPerm.Saup_cre_when;
                    appUsrPerm.Saup_prem_cd = _PerCode;// txtPermLvl.Text.Trim();
                    appUsrPerm.Saup_session_id = Session["SessionID"].ToString(); ;
                    appUsrPerm.Saup_usr_id = txtUser_A.Text.Trim();
                    appUsrPerm.Saup_val_limit = 0;

                    //string confirmValue = Request.Form["confirm_value"];
                    if (txtSaveconformmessageValue.Value == "Yes")
                    {
                        Int32 eff = CHNLSVC.Security.Save_Sec_App_Usr_Prem(appUsrPerm);
                        if (eff > 0)
                        {
                            lblSApp.Text = "Sucessfully Saved!";
                            ApprovalSuccess.Visible = true;
                            //MessageBox.Show("Sucessfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //TODO: LOAD GRID
                            DataTable DT = CHNLSVC.Security.Get_UserApprove_Permissions(txtUser_A.Text.Trim(), string.Empty);

                            grvApprLevel.DataSource = null;
                            //grvApprLevel.AutoGenerateColumns = false;
                            grvApprLevel.DataSource = DT;
                            grvApprLevel.DataBind();
                        }
                        else
                        {
                            lblSApp.Text = "Not Saved!";
                            AprovalWarning.Visible = true;
                            // MessageBox.Show("Not Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Save Cancel!')", true);
                    }
                    txtSaveconformmessageValue.Value = "";
                }
            }
        }

        protected void btnDeleteApprPerm_Click(object sender, EventArgs e)
        {
            AprovalWarning.Visible = false;
            ApprovalSuccess.Visible = false;
            string _user = "";
            string _perCd = "";
            if (grvApprLevel.Rows.Count <= 0)
            {
                lblWApp.Text = "Cannot find any permission.!";
                AprovalWarning.Visible = true;
                //MessageBox.Show("Cannot find any permission.", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtFullName_A.Text == null)
            {
                lblWApp.Text = "Please select a user.!";
                AprovalWarning.Visible = true;
                return;
            }
            Boolean _appItm = false;
            foreach (GridViewRow row in grvApprLevel.Rows)
            {
                //DataGridViewCheckBoxCell chk = row.Cells["col_Chk"] as DataGridViewCheckBoxCell;
                CheckBox chk = (CheckBox)row.FindControl("col_Chk");
                if (chk.Checked == true)
                {
                    _appItm = true;
                    goto L4;
                }
            }
        L4:

            if (_appItm == false)
            {
                lblWApp.Text = "Please select permission which need to de-activate.!";
                AprovalWarning.Visible = true;
                // MessageBox.Show("Please select permission which need to de-activate.", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (GridViewRow newRow in grvApprLevel.Rows)
            {
                // DataGridViewCheckBoxCell chk = newRow.Cells["col_Chk"] as DataGridViewCheckBoxCell;
                CheckBox chk = (CheckBox)newRow.FindControl("col_Chk");
                if (chk.Checked == true)
                {
                    _user = txtUser_A.Text.Trim();
                    // _perCd = newRow.Cells["saup_prem_cd"].Value.ToString();
                    _perCd = (newRow.FindControl("saup_prem_cd") as Label).Text;
                    SecApproveUserPerm appUsrPerm = new SecApproveUserPerm();
                    appUsrPerm.Saup_act = false;
                    appUsrPerm.Saup_cre_by = Session["UserID"].ToString();
                    appUsrPerm.Saup_cre_when = CHNLSVC.Security.GetServerDateTime().Date;
                    appUsrPerm.Saup_max_app_limit = 0;
                    appUsrPerm.Saup_mod_by = Session["UserID"].ToString();
                    appUsrPerm.Saup_mod_when = appUsrPerm.Saup_cre_when;
                    appUsrPerm.Saup_prem_cd = _perCd;
                    appUsrPerm.Saup_session_id = Session["SessionID"].ToString(); ;
                    appUsrPerm.Saup_usr_id = _user;
                    appUsrPerm.Saup_val_limit = 0;
                    //string confirmValue = Request.Form["DeleteConfirm_value"];
                    if (txtDeleteconformmessageValue.Value == "Yes")
                    {
                        Int32 eff = CHNLSVC.Security.Save_Sec_App_Usr_Prem(appUsrPerm);
                        if (eff > 0)
                        {
                            lblSApp.Text = "Sucessfully de-activated!";
                            ApprovalSuccess.Visible = true;
                            //MessageBox.Show("Sucessfully de-activated!", "De-Activated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //TODO: LOAD GRID
                            DataTable DT = CHNLSVC.Security.Get_UserApprove_Permissions(txtUser_A.Text.Trim(), string.Empty);

                            grvApprLevel.DataSource = null;
                            //grvApprLevel.AutoGenerateColumns = false;
                            grvApprLevel.DataSource = DT;
                            grvApprLevel.DataBind();
                        }
                        else
                        {
                            lblWApp.Text = "Not de-activated!";
                            AprovalWarning.Visible = true;
                            //MessageBox.Show("Not de-activated!", "De-Activated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cacel de-activate!')", true);
                    }
                    txtDeleteconformmessageValue.Value = "";
                }
            }
        }

        protected void btnAddLocs_Click(object sender, EventArgs e)
        {
            try
            {
                WarnningLoc.Visible = false;
                string com = ucLoactionSearch.Company;
                string chanel = ucLoactionSearch.Channel;
                string subChanel = ucLoactionSearch.SubChannel;
                string area = ucLoactionSearch.Area;
                string region = ucLoactionSearch.Regien;
                string zone = ucLoactionSearch.Zone;
                string pc = ucLoactionSearch.ProfitCenter;


                //DataTable DT = CHNLSVC.Security.GetUserLocations(txtUser_L.Text, null);
                //foreach (DataRow row in DT.Rows)
                //{
                //    if (row["SEL_LOC_CD"].ToString() == pc)
                //    {
                //        lblWLoc.Text = "Already Add Location..";
                //        WarnningLoc.Visible = true;
                //        return;
                //        //return row["CountryID"];
                //    }
                //}



                DataTable dt = CHNLSVC.Inventory.GetLOC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
                // select_LOC_List.Merge(dt);
                grvLocs.DataSource = null;
                grvLocs.AutoGenerateColumns = false;
                grvLocs.DataSource = dt;
                grvLocs.DataBind();
            }
            catch (Exception ex)
            {

                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnLocationSave_Click(object sender, EventArgs e)
        {
            SuccessLoc.Visible = false;
            WarnningLoc.Visible = false;

            try
            {
                if (txtUser_L.Text.Trim() == "")
                {
                    lblWLoc.Text = "Please select a user..";
                    WarnningLoc.Visible = true;
                    //MessageBox.Show("Please select a user", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //-------------------------------------------------------------
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser_L.Text.Trim());
                if (_systemUser == null)
                {
                    lblWLoc.Text = "Invalid username!.";
                    WarnningLoc.Visible = true;
                    // MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //----------------------------------------------------------------
                Int32 Default_CNT = 0;
                string company;
                List<SystemUserLoc> loc_list = GetSelectedLocationList(out company, out  Default_CNT);
                if (loc_list.Count < 1)
                {//
                    lblWLoc.Text = "Please select location(s).";
                    WarnningLoc.Visible = true;
                    //MessageBox.Show("Please select location(s)", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Int16 is_Def_Loc = CHNLSVC.Security.Check_User_Def_Loc(txtUser_L.Text.Trim(), company);
                if (is_Def_Loc > 0 && Default_CNT > 0)
                {
                    lblWLoc.Text = "User has a default location already";
                    WarnningLoc.Visible = true;
                    // MessageBox.Show("User has a default location already.\nMore than one default location cannot be assigned.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Default_CNT > 1)
                {
                    lblWLoc.Text = "More than one default location cannot be assigned";
                    WarnningLoc.Visible = true;
                    //MessageBox.Show("More than one default location cannot be assigned.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //string confirmValue = Request.Form["confirm_value"];
                if (txtSaveconformmessageValue.Value == "Yes")
                {
                    Int32 eff = CHNLSVC.Security.UpdateSystemUserLoc_NEW(loc_list);
                    lblSuccLoc.Text = "Successfully Updated!.";
                    SuccessLoc.Visible = true;
                    // MessageBox.Show("Successfully Updated!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUserLoc();
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(Cancel 'Update!')", true);
                }
                txtSaveconformmessageValue.Value = "";
            }
            catch (Exception ex)
            {
                lblWLoc.Text = "Error Occurred while processing..";
                WarnningLoc.Visible = true;
                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
                   
        }

        protected void btnLocationDete_Click(object sender, EventArgs e)
        {
            SuccessLoc.Visible = false;
            WarnningLoc.Visible = false;
            try
            {
                List<SystemUserLoc> delete_list = GetSelectedLocationList_toUpdate();
                if (delete_list.Count == 0)
                {
                    lblWLoc.Text = "Please select locations to delete!";
                    WarnningLoc.Visible = true;
                    //MessageBox.Show("Please select locations to delete!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtUser_L.Text == "")
                {
                    lblWLoc.Text = "Please select user!";
                    WarnningLoc.Visible = true;
                    // MessageBox.Show("Please select user!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //string confirmValue = Request.Form["DeleteConfirm_value"];
                if (txtDeleteconformmessageValue.Value == "Yes")
                 {
                     int row_arr = CHNLSVC.Security.DeleteUserLoc_NEW(delete_list);
                     lblSuccLoc.Text = "Successfully deleted!";
                     SuccessLoc.Visible = true;
                     //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully deleted");
                     // MessageBox.Show("Successfully deleted!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     LoadUserLoc();
                 }
                 else
                 {
                     this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cacel Deleted!')", true);
                 }
                txtDeleteconformmessageValue.Value = "";
            }
            catch (Exception ex)
            {
                lblWLoc.Text = "Error Occurred while processing..";
                WarnningLoc.Visible = true;
                // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btn_AddPC_Click(object sender, EventArgs e)
        {
            WarrningPro.Visible = false;
            SuccessPro.Visible = false;
            try
            {
                if (txtUser_P.Text.Trim() == "")
                {
                    lblWPro.Text = "Please select a user";
                    WarrningPro.Visible = true;
                    // MessageBox.Show("Please select a user", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //----------------------------------------------------------------
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser_P.Text.Trim());
                if (_systemUser == null)
                {
                    lblWPro.Text = "Invalid username!";
                    WarrningPro.Visible = true;
                    // MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //----------------------------------------------------------------

                Int32 Default_CNT = 0;
                string company;
                List<SystemUserProf> pc_list = GetSelectedProfitCenter_List(out company, out  Default_CNT);
                if (pc_list.Count < 1)
                {
                    lblWPro.Text = "Please select profit center(s)!";
                    WarrningPro.Visible = true;
                    //MessageBox.Show("Please select profit center(s)!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Int16 is_Def_PC = CHNLSVC.Security.Check_User_Def_PC(txtUser_P.Text.Trim(), company);
                if (is_Def_PC > 0 && Default_CNT > 0)
                {
                    lblWPro.Text = "User has a default profit center already.\nMore than one default location cannot be assigned.";
                    WarrningPro.Visible = true;
                    // MessageBox.Show("User has a default profit center already.\nMore than one default location cannot be assigned.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Default_CNT > 1)
                {
                    lblWPro.Text = "More than one default profit center cannot be assigned.";
                    WarrningPro.Visible = true;
                    //MessageBox.Show("More than one default profit center cannot be assigned.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                  //string confirmValue = Request.Form["confirm_value"];
                  if (txtSaveconformmessageValue.Value == "Yes")
                  {
                      //TODO: SAVE PROFIT CENTERS
                      Int32 effect = CHNLSVC.Security.UpdateSystemUserPC_NEW(pc_list);
                      lblSuPro.Text = "Successfully Updated!";
                      SuccessPro.Visible = true;
                      //MessageBox.Show("Successfully Updated!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      LoadUserPC();
                      //  Int32 eff=CHNLSVC.Security.save
                  }
                  else
                  {
                      this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(Cancel 'Update!')", true);
                  }
                  txtSaveconformmessageValue.Value = "";

            }
            catch (Exception ex)
            {
                lblWPro.Text = "Error Occurred while processing...";
                WarrningPro.Visible = true;
                // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btn_DelPC_Click(object sender, EventArgs e)
        {
            try
            {
                WarrningPro.Visible = false;
                SuccessPro.Visible = false;
                List<SystemUserProf> delete_list = GetSelectedProfitCenterList_toUpdate();
                if (delete_list.Count == 0)
                {
                    lblWPro.Text = "Please select profit centers to delete!.";
                    WarrningPro.Visible = true;
                    // MessageBox.Show("Please select profit centers to delete!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtUser_P.Text == "")
                {
                    lblWPro.Text = "Please select user!.";
                    WarrningPro.Visible = true;
                    //MessageBox.Show("Please select user!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //TODO: DELETE
                 //string confirmValue = Request.Form["DeleteConfirm_value"];
                if (txtDeleteconformmessageValue.Value == "Yes")
                 {
                     int row_arr = CHNLSVC.Security.DeleteUserPC_NEW(delete_list);
                     lblSuPro.Text = "Successfully deleted!";
                     SuccessPro.Visible = true;
                     // MessageBox.Show("Successfully deleted!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     LoadUserPC();
                 }
                 else
                 {
                     this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cacel Delete!')", true);
                 }
                txtDeleteconformmessageValue.Value = "";
            }
            catch (Exception ex)
            {
                lblWPro.Text = "Error Occurred while processing..";
                WarrningPro.Visible = true;
                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnAddPc_Click(object sender, EventArgs e)
        {
            WarrningPro.Visible = false;
            SuccessPro.Visible = false;
            try
            {
                string com = ucProfitCenterSearch.Company;
                string chanel = ucProfitCenterSearch.Channel;
                string subChanel = ucProfitCenterSearch.SubChannel;
                string area = ucProfitCenterSearch.Area;
                string region = ucProfitCenterSearch.Regien;
                string zone = ucProfitCenterSearch.Zone;
                string pc = ucProfitCenterSearch.ProfitCenter;

                //DataTable DT = CHNLSVC.Security.GetAllUserPC(txtUser_P.Text.Trim());
                //foreach (DataRow row in DT.Rows)
                //{
                //    if (row["SUP_PC_CD"].ToString() == pc)
                //    {
                //        lblWLoc.Text = "Already Add Profile Center..";
                //        WarnningLoc.Visible = true;
                //        return;
                //        //return row["CountryID"];
                //    }
                //}



                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
                grvPCs.DataSource = null;
                grvPCs.AutoGenerateColumns = false;
                grvPCs.DataSource = dt;
                grvPCs.DataBind();
                if (dt.Rows.Count <= 0)
                {
                    lblWPro.Text = "Please check profit center hierarchy setup.";
                    WarrningPro.Visible = true;
                    //MessageBox.Show("Please check profit center hirachy setup.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                    return;
                }


            }
            catch (Exception ex)
            {
                lblWPro.Text = "Error Occurred while processing..";
                WarrningPro.Visible = true;
                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void txtUser_L_TextChanged(object sender, EventArgs e)
        {
            if (txtUser_L.Text.Trim() != "")
            {
                //----------------------------------------------------------------
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser_L.Text.Trim());
                if (_systemUser == null)
                {
                    // MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblWLoc.Text = "Invalid username!";
                    WarnningLoc.Visible = true;
                    txtUser.Text = "";
                    txtFullName.Text = "";
                    txtDesn.Text = "";
                    txtCat.Text = "";
                    txtDept_.Text = "";
                    //txtEmpID_.Text = "";
                    return;
                }
                else
                {
                    Load_UserDetails(ref txtUser, ref txtFullName, ref txtDesn, ref txtCat, ref txtDept_, ref txtEmpID_);
                    txtCom_C.Focus();
                }
                //----------------------------------------------------------------                    
                // txtName.Focus();
                WarnningLoc.Visible = false; ;
                UserDetails(txtUser_L.Text);
            }
          
        }

        protected void txtUser_S_TextChanged(object sender, EventArgs e)
        {
            if (txtUser_S.Text.Trim() != "")
            {
                //----------------------------------------------------------------
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser_S.Text.Trim());
                if (_systemUser == null)
                {
                    // MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblWper.Text = "Invalid username!";
                    WarnnPer.Visible = true;
                    txtUser.Text = "";
                    txtFullName.Text = "";
                    txtDesn.Text = "";
                    txtCat.Text = "";
                    txtDept_.Text = "";
                    //txtEmpID_.Text = "";
                    return;
                }
                else
                {
                    Load_UserDetails(ref txtUser, ref txtFullName, ref txtDesn, ref txtCat, ref txtDept_, ref txtEmpID_);
                    txtCom_C.Focus();
                }
                //----------------------------------------------------------------                    
                // txtName.Focus();
                WarnnPer.Visible = false ;
                UserDetails(txtUser_S.Text);
            }
           
           
        }

        protected void txtUser_A_TextChanged(object sender, EventArgs e)
        {
            if (txtUser_A.Text.Trim() != "")
            {
                //----------------------------------------------------------------
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser_A.Text.Trim());
                if (_systemUser == null)
                {
                    // MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblWApp.Text = "Invalid username!";
                    AprovalWarning.Visible = true;
                    txtUser.Text = "";
                    txtFullName.Text = "";
                    txtDesn.Text = "";
                    txtCat.Text = "";
                    txtDept_.Text = "";
                    //txtEmpID_.Text = "";
                    return;
                }
                else
                {
                    Load_UserDetails(ref txtUser, ref txtFullName, ref txtDesn, ref txtCat, ref txtDept_, ref txtEmpID_);
                    txtCom_C.Focus();
                }
                //----------------------------------------------------------------                    
                // txtName.Focus();
                AprovalWarning.Visible = false;
                UserDetails(txtUser_A.Text);
            }
           
        }

        protected void txtUser_P_TextChanged(object sender, EventArgs e)
        {
            if (txtUser_P.Text.Trim() != "")
            {
                //----------------------------------------------------------------
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(txtUser_P.Text.Trim());
                if (_systemUser == null)
                {
                   // MessageBox.Show("Invalid username!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblWPro.Text = "Invalid username!";
                    WarrningPro.Visible = true;
                    txtUser.Text = "";
                    txtFullName.Text = "";
                    txtDesn.Text = "";
                    txtCat.Text = "";
                    txtDept_.Text = "";
                    //txtEmpID_.Text = "";
                    return;
                }
                else
                {
                    Load_UserDetails(ref txtUser, ref txtFullName, ref txtDesn, ref txtCat, ref txtDept_, ref txtEmpID_);
                    txtCom_C.Focus();
                }
                //----------------------------------------------------------------                    
                // txtName.Focus();
                WarrningPro.Visible = false;
                UserDetails(txtUser_P.Text);
            }
           
        }

        protected void grdUserComp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RememberOldCompanyValues();
            grdUserComp.PageIndex = e.NewPageIndex;;
            grdUserComp.DataSource = null;
            grdUserComp.AutoGenerateColumns = false;
            DataTable ComTbl = CHNLSVC.Security.GetUser_Company(txtUser.Text.Trim());
            grdUserComp.DataSource = ComTbl;
            grdUserComp.DataBind();
            RePopulateCompanyValues();
        }
 
        protected void grvLocs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RemembergrvLocsValues();
            RemembergrvLocsDValues(); 
            grvLocs.PageIndex = e.NewPageIndex;
            try
            {
                WarnningLoc.Visible = false;
                string com = ucLoactionSearch.Company;
                string chanel = ucLoactionSearch.Channel;
                string subChanel = ucLoactionSearch.SubChannel;
                string area = ucLoactionSearch.Area;
                string region = ucLoactionSearch.Regien;
                string zone = ucLoactionSearch.Zone;
                string pc = ucLoactionSearch.ProfitCenter;

                DataTable dt = CHNLSVC.Inventory.GetLOC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
                // select_LOC_List.Merge(dt);
                grvLocs.DataSource = null;
                grvLocs.AutoGenerateColumns = false;
                grvLocs.DataSource = dt;
                grvLocs.DataBind();
              RePopulategrvLocsValues();
              RePopulategrvLocsDValues();
            }
            catch (Exception ex)
            {

                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void gvUserLoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RememberOldValues();
            gvUserLoc.PageIndex = e.NewPageIndex;
            Load_UserDetails(ref txtUser_L, ref txtFullName_L, ref txtDesn_L, ref txtCat_L, ref txtDept_L, ref txtEmpID_L);
            LoadUserLoc();
            RePopulateValues();
            //foreach (GridViewRow gvr in gvUserLoc.Rows)
            //{
            //    CheckBox chkSelect = gvr.FindControl("selectchkU") as CheckBox;
            //    if (chkSelect != null)
            //    {
            //        string productID = gvUserLoc.DataKeys[gvr.RowIndex]["LOCATION"].ToString() ;
            //        if (chkSelect.Checked && !this.ProductIDs.Contains(productID))
            //        {
            //            this.ProductIDs.Add(productID);
            //        }
            //        else if (!chkSelect.Checked && this.ProductIDs.Contains(productID))
            //        {
            //            this.ProductIDs.Remove(productID);
            //        }
            //    }
            //}

            //int d = gvUserLoc.PageCount;
            //bool[] values = new bool[gvUserLoc.PageSize];
            //CheckBox chb;
            //for (int i = 0; i < gvUserLoc.Rows.Count; i++)
            //{
            //    chb = (CheckBox)gvUserLoc.Rows[i].FindControl("selectchkU");
            //    if (chb != null)
            //    {
            //        values[i] = chb.Checked;
            //    }
            //}
            //Session["page" + gvUserLoc.PageIndex] = values;

            //gvUserLoc.PageIndex = e.NewPageIndex;
            //gvUserLoc.DataBind();

           

        }

        protected void grvPCs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RemembergrvPCsValues1();
            RemembergrvPCsValues2();
            grvPCs.PageIndex = e.NewPageIndex;
            WarrningPro.Visible = false;
            SuccessPro.Visible = false;
            try
            {
                string com = ucProfitCenterSearch.Company;
                string chanel = ucProfitCenterSearch.Channel;
                string subChanel = ucProfitCenterSearch.SubChannel;
                string area = ucProfitCenterSearch.Area;
                string region = ucProfitCenterSearch.Regien;
                string zone = ucProfitCenterSearch.Zone;
                string pc = ucProfitCenterSearch.ProfitCenter;

                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
                grvPCs.DataSource = null;
                grvPCs.AutoGenerateColumns = false;
                grvPCs.DataSource = dt;
                grvPCs.DataBind();
                RePopulategrvPCsValues1();
                RePopulategrvPCsValues2();
                if (dt.Rows.Count <= 0)
                {
                    lblWPro.Text = "Please check profit center hirachy setup.";
                    WarrningPro.Visible = true;
                    //MessageBox.Show("Please check profit center hirachy setup.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                    return;
                }
                

            }
            catch (Exception ex)
            {
                lblWPro.Text = "Error Occurred while processing..";
                WarrningPro.Visible = true;
                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void gvUserPC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RemembergvUserPCValues();
            gvUserPC.PageIndex = e.NewPageIndex;
            Load_UserDetails(ref txtUser_P, ref txtFullName_P, ref txtDesn_P, ref txtCat_P, ref txtDept_P, ref txtEmpID_P);
            LoadUserPC();
            RePopulategvUserPCValues();
        }

    

        protected void grvLocs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void grvLocs_DataBound(object sender, EventArgs e)
        {
           
        }

        protected void txtCPW_TextChanged(object sender, EventArgs e)
        {
            WarningUser.Visible = false;
            if (txtPW.Text != txtCPW.Text)
            {
                lblWUser.Text = "Password validation is fail!";
                WarningUser.Visible = true;
                // // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Password validation is fail!");
                //MessageBox.Show("Password validation is fail!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCPW.Text = "";
                txtCPW.Focus();
                string CPassword3 = "";
                txtCPW.Attributes.Add("value", CPassword3);
                return;
            }
            string Password = txtPW.Text;
            txtPW.Attributes.Add("value", Password);

            string CPassword = txtCPW.Text;
            txtCPW.Attributes.Add("value", CPassword);
        }

        protected void txtPW_TextChanged(object sender, EventArgs e)
        {
            WarningUser.Visible = false;
            if (!(txtCPW.Text == ""))
            { 
          
            if (txtPW.Text != txtCPW.Text)
            {
                lblWUser.Text = "Password validation is fail!";
                WarningUser.Visible = true;
                // // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Password validation is fail!");
                //MessageBox.Show("Password validation is fail!", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCPW.Text = "";
                txtCPW.Focus();
                string Password1 = txtPW.Text;
                txtPW.Attributes.Add("value", Password1);
                return;
            }
           
            }
            string Password3 = txtPW.Text;
            txtPW.Attributes.Add("value", Password3);

            //string CPassword2 = txtCPW.Text;
           // txtCPW.Attributes.Add("value", CPassword2);
        }

        protected void txtMobile_TextChanged(object sender, EventArgs e)
        {
            WarningUser.Visible = false;
            SuccessUser.Visible = false;
            //Regex regex = new Regex(@"[^+^\-^\/^\*^\(^\)]");
            
            //MatchCollection matches = regex.Matches(txtMobile.Text);
           // string pattern = @"[^-^]";

           // System.Text.RegularExpressions.Match match = Regex.Match(txtMobile.Text.Trim(), pattern);

           // //WarningUser.Visible = false;
           // if ((match.Success))
           //{
           //    lblWUser.Text = "only allow Numeric Value!";
           //    WarningUser.Visible = true;
           //    return;
           //}

            int distance;
            if (int.TryParse(txtMobile.Text, out distance))
            {
                // it's a valid integer => you could use the distance variable here
                //lblWUser.Text = "only allow Numeric Value!";
                //WarningUser.Visible = true;
                return;
            }
            lblWUser.Text = "only allow Numeric Value!";
            WarningUser.Visible = true;
           
            //if (!(IsNumeric(txtMobile.Text)))
            //{
            //    lblWUser.Text = "only allow Numeric Value!";
            //    WarningUser.Visible = true;
            //    return;
            //}
            
        }

      

        protected void txtPhone_TextChanged1(object sender, EventArgs e)
        {
            SuccessUser.Visible = false;
            WarningUser.Visible = false;
            int distance;
            if (int.TryParse(txtPhone.Text, out distance))
            {
                // it's a valid integer => you could use the distance variable here
                //lblWUser.Text = "only allow Numeric Value!";
                //WarningUser.Visible = true;
                return;
            }
            lblWUser.Text = "only allow Numeric Value!";
            WarningUser.Visible = true;
        }

        protected void gvUserRole_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the value in the c04_oprogrs column. You'll have to use
                // some trial and error here to find the right control. The line
                // below may provide the desired value but I'm not entirely sure.
                string value = e.Row.Cells[0].Text;

                // Next find the label in the template field.
                Label myLabel = (Label)e.Row.FindControl("SERM_COM_CD");
                if (value == "1")
                {
                    myLabel.Text = "Take";
                }
                else if (value == "2")
                {
                    myLabel.Text = "Available";
                }
            }
        }

        private void clearmessagers()
        {
            WarningUser.Visible = false;
            SuccessUser.Visible = false;
            tests.Visible = false;
            DivSuccess.Visible = false;
            errorDiv.Visible = false;
            successDiv.Visible = false;
            WarnningLoc.Visible = false;
            SuccessLoc.Visible = false;
            WarnnPer.Visible = false;
            SuccesPer.Visible = false;
            AprovalWarning.Visible = false;
            ApprovalSuccess.Visible = false;
            WarrningPro.Visible = false;
            SuccessPro.Visible = false;
            SBUerror.Visible = false;
        }

        protected void lbtnSBUCompany_Click(object sender, EventArgs e)
        {
            SBUerror.Visible = false;
            if (string.IsNullOrEmpty(txtUser.Text))
            {
                SBUerror.Visible = true;
                lblSBUerror.Text = "Please select User ID";
                return;
            }

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
            //DataTable ComTbl = CHNLSVC.Security.GetUser_Company(txtUser.Text.Trim());
            //ComTbl.Columns.RemoveAt(0);
            //ComTbl.Columns.RemoveAt(2);
            //ComTbl.Columns.RemoveAt(2);
            //ComTbl.Columns.RemoveAt(2);
            //ComTbl.Columns.RemoveAt(2);
            //ComTbl.Columns.RemoveAt(2);
            //ComTbl.Columns.RemoveAt(2);
            //ComTbl.Columns.RemoveAt(2);
            //ComTbl.Columns.RemoveAt(1);
            //ComTbl.Columns[0].ColumnName = "Code";
            //ComTbl.AcceptChanges();
            dvResultUser.DataSource = _result;
           
            dvResultUser.DataBind();
            BindUCtrlDDLData(_result);
            Label1.Text = "SBUCompany";
            UserPopoup.Show();
        }

        protected void lbtnSbu_Click(object sender, EventArgs e)
        {
            SBUerror.Visible = false;
            if (string.IsNullOrEmpty(txtCompanySBU.Text))
            {
                SBUerror.Visible = true;
                lblSBUerror.Text = "Please select User Company";
                return;
            }
            DataTable SbuTbl = CHNLSVC.Security.GetSBU_Company(txtCompanySBU.Text.Trim(),null);
            dvResultUser.DataSource = SbuTbl;
            dvResultUser.DataBind();
            BindUCtrlDDLData(SbuTbl);
            Label1.Text = "SBU";
            UserPopoup.Show();
        }

        protected void txtSbuCode_TextChanged(object sender, EventArgs e)
        {
            SBUerror.Visible = false;
            DataTable SbuTbl = CHNLSVC.Security.GetSBU_Company(txtCompanySBU.Text.Trim(), txtSbuCode.Text);
            if ((SbuTbl.Rows.Count > 0) && SbuTbl!=null)
            {
                txtSBUDes.Text = SbuTbl.Rows[0][1].ToString();
                return;
            }
            SBUerror.Visible = true;
            lblSBUerror.Text = "Please Enter Valid SBU Code";
            txtSbuCode.Text = "";
        }

        protected void txtCompanySBU_TextChanged(object sender, EventArgs e)
        {
            SBUerror.Visible = false;
            if (string.IsNullOrEmpty(txtUser.Text))
            {
                SBUerror.Visible = true;
                lblSBUerror.Text = "Please select User ID";
                //return;
            }
            MasterCompany com = CHNLSVC.General.GetCompByCode(txtCompanySBU.Text.Trim());
            if (com != null)
            {
                txtCDesSBU.Text = com.Mc_desc;
                txtSbuCode.Text = "";
                txtSBUDes.Text = "";
                return;
            }            
            SBUerror.Visible = true;
            lblSBUerror.Text = "Please Enter Valid Company Code";
            txtCompanySBU.Text = "";
        }

        protected void txtCom_C_TextChanged(object sender, EventArgs e)
        {
           
            MasterCompany com = CHNLSVC.General.GetCompByCode(txtCom_C.Text.Trim());
            if (com == null)
            {
                tests.Visible = true;
                AlertID.Text = "Please Enter Valid Company Code";
                txtCom_C.Text = "";
               return;

            }
           
        }


        protected void txtCom_R_TextChanged(object sender, EventArgs e)
        {

            MasterCompany com = CHNLSVC.General.GetCompByCode(txtCom_R.Text.Trim());
            if (com == null)
            {
                errorDiv.Visible = true;
                lblWarn.Text = "Please Enter Valid Company Code";
                txtCom_R.Text = "";
                // return;

            }

        }


        protected void txtCom_S_TextChanged(object sender, EventArgs e)
        {

            MasterCompany com = CHNLSVC.General.GetCompByCode(txtCom_S.Text.Trim());
            if (com == null)
            {
                WarnnPer.Visible = true;
                lblWper.Text = "Please Enter Valid Company Code";
                txtCom_S.Text = "";
                // return;

            }

        }


        protected void txtPermCd_TextChanged(object sender, EventArgs e)
        {

           
           string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SecUsrPermTp);
           DataTable _result = CHNLSVC.CommonSearch.Get_All_SecUsersPerimssionTypes(SearchParams, "PERMISSION CODE", txtPermCd.Text);
            if (_result.Rows.Count == 0)
            {
                WarnnPer.Visible = true;
                lblWper.Text = "Please Enter Valid Permission Code";
                txtPermCd.Text = "";
                // return;

            }

        }


        protected void txtAppr_Code_TextChanged(object sender, EventArgs e)
        {
           
        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermCode);
        DataTable _result1 = CHNLSVC.CommonSearch.Search_Approve_Permissions(SearchParams, "PERMISSION_CODE", txtAppr_Code.Text);
            if (_result1.Rows.Count == 0)
            {
                AprovalWarning.Visible = true;
                lblWApp.Text = "Please Enter Valid Permission Code";
                txtAppr_Code.Text = "";


                // return;

            }

        }

        protected void txtPermLvl_TextChanged(object sender, EventArgs e)
        {
           
           string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermLevelCode);
            DataTable _result2 = CHNLSVC.CommonSearch.Search_ApprovePermission_Levels(SearchParams, "LEVEL_CODE", txtAppr_Code.Text);
            if (_result2.Rows.Count == 0)
            {
                AprovalWarning.Visible = true;
                lblWApp.Text = "Please Enter Valid Level Code";
                txtPermLvl.Text = "";
                // return;

            }

        }
            
            
                  
        protected void lbtnSaveSBu_Click(object sender, EventArgs e)
        {
            if (txtSaveconformmessageValue.Value == "Yes")
            {
                if (chk_ActSbu.Checked == false)
                {
                    SBUSuccess.Visible = false;
                    SBUerror.Visible = true;
                    lblSBUerror.Text = "please Active SBU before you save ";
                    return;
                }
                foreach (GridViewRow dgvr in grdSbu.Rows)
                {

                    string _sbu = (dgvr.FindControl("col_sbucode") as Label).Text;
                    CheckBox chk = (CheckBox)dgvr.FindControl("col_chkAct");
                    CheckBox Dchk = (CheckBox)dgvr.FindControl("col_chkDef");
                    if (_sbu == txtSbuCode.Text)
                    {
                        if (chk.Checked == chk_ActSbu.Checked)
                        {
                            if (Dchk.Checked == chk_DefSbu.Checked)
                            {
                                SBUSuccess.Visible = false;
                                SBUerror.Visible = true;
                                lblSBUerror.Text = "selected sbu is alredy added";

                                return;
                            }
                        }
                    }
                }
                SaveUserSBU();
                DataTable SBUTbl = CHNLSVC.Security.GetSBU_User(txtCompanySBU.Text, txtUserIdSBU.Text,null);
                grdSbu.DataSource = SBUTbl;
                grdSbu.DataBind();
            }
        }

        protected void lbtnDeleteSbu_Click(object sender, EventArgs e)
        {
            tests.Visible = false;
            DivSuccess.Visible = false;
            WarningUser.Visible = false;
            SuccessUser.Visible = false;
            SBUerror.Visible = false;
            bool ISDelete = false;
            try
            {
                SBUerror.Visible = false;
                if (string.IsNullOrEmpty(txtCompanySBU.Text))
                {
                    SBUerror.Visible = true;
                    lblSBUerror.Text = "Please select User Company";
                    return;
                }
                if (string.IsNullOrEmpty(txtUser.Text))
                {
                    SBUerror.Visible = true;
                    lblSBUerror.Text = "Please select User ID";
                    return;
                }
                
                    foreach (GridViewRow dgvr in grdSbu.Rows)
                    {
                        CheckBox chk = (CheckBox)dgvr.FindControl("chk_sbu");
                        CheckBox col_chkAct = (CheckBox)dgvr.FindControl("col_chkAct");
                        CheckBox col_chkDef = (CheckBox)dgvr.FindControl("col_chkDef");
                        
                        string SBUCODE = (dgvr.FindControl("col_sbucode") as Label).Text;
                        if (chk != null & chk.Checked)
                        {
                            if (col_chkAct.Checked == true)
                            {
                                SBUSuccess.Visible = false;
                                lblSBUerror.Text = "inactive your sub code before you delete it!";
                                SBUerror.Visible = true;
                                return;
                            }
                            //string confirmValue = Request.Form["DeleteConfirm_value"];
                            if (txtDeleteconformmessageValue.Value == "Yes")
                            {
                                StrategicBusinessUnits _SBU = new StrategicBusinessUnits();
                                _SBU.Seo_act = (col_chkAct.Checked == true) ? true : false;
                                _SBU.Seo_com_cd = txtCompanySBU.Text.Trim();
                                _SBU.Seo_mod_by = Session["UserID"].ToString();
                                _SBU.Seo_mod_dt = System.DateTime.Now;
                                _SBU.Seo_def_opecd = (col_chkDef.Checked == true) ? true : false;
                                _SBU.Seo_ope_cd = SBUCODE;
                                _SBU.Seo_session_id = Session["SessionID"].ToString();
                                _SBU.Seo_usr_id = txtUserIdSBU.Text.Trim();

                                int row_aff = CHNLSVC.Security.Save_User_SBU(_SBU);
                                if (row_aff > 0)
                                {
                               
                                  
                                    ISDelete = true;
                                }
                              
                            }
                            else
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
                            }
                            txtDeleteconformmessageValue.Value = "";
                        }

                    }
                    if (ISDelete == true)
                    {
                        
                        SBUerror.Visible = false;
                        lblSBUSuccess.Text = "Successfully Deleted!";
                        SBUSuccess.Visible = true;
                        DataTable SBUTbl = CHNLSVC.Security.GetSBU_User(txtCompanySBU.Text, txtUserIdSBU.Text, null);
                        grdSbu.DataSource = SBUTbl;
                        grdSbu.DataBind();
                    }
                    if (ISDelete == false)
                    {
                        SBUSuccess.Visible = false;
                        lblSBUerror.Text = "Please select a CompanyCode.";
                        SBUerror.Visible = true;
                    }

            }
            catch (Exception ex)
            {

                // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

       
        protected void gvUserRole_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RememberOldRoleValues();
            gvUserRole.PageIndex = e.NewPageIndex;
            LoadUserRole();
            RePopulateRoleValues();
        }

        protected void grdSbu_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }


        private void clearall()
        {
            try
            {
                txtID.Text = ""; txtName.Text = ""; txtDescription.Text = "";  txtEMail.Text = ""; txtMobile.Text = "";
                txtPhone.Text = ""; txtCate.Text = ""; txtDept.Text = ""; txtEmpID.Text = ""; txtEmpCode.Text = ""; txtDomainID.Text = "";
                txtSunUserID.Text = ""; txtDisableRmks.Text = ""; lblDName.Text = ""; lblDTitle.Text = ""; lblDDept.Text = "";
                txtPW.Text = ""; txtCPW.Text = "";
                txtPW.Attributes["value"] = ""; txtCPW.Attributes["value"] = "";
             
            }
            catch (Exception ex)
            {
            
            }
        
        }
      

       
    }

    
}