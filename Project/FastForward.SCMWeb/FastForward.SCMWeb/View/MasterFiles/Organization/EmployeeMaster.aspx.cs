using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
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

namespace FastForward.SCMWeb.View.MasterFiles.Organization
{
    public partial class EmployeeMaster : Base
    {
        Employee _employeeNew = new Employee();
        List<MasterProfitCenter_LocationEmployee> _MPcE = new List<MasterProfitCenter_LocationEmployee>();
        List<MasterProfitCenter_LocationEmployee> _MLE = new List<MasterProfitCenter_LocationEmployee>();
        List<MasterCustomerEmployee> _MstCusEmp = new List<MasterCustomerEmployee>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (RadioButtonList1.Items.Count > 0)
                //{
                //    RadioButtonList1.Items[0].Selected = true;
                //}
                loadDefault();

            }

            else
            {
                txtEmpDOB.Text = Request[txtEmpDOB.UniqueID];
                txtPCAssDate.Text = Request[txtPCAssDate.UniqueID];
                txtLAssDate.Text = Request[txtLAssDate.UniqueID];
            }
        }

        private void pageClear()
        {
            Session["Multiplecom"] = "";

            loadDefault();
            try
            {
                Response.Redirect(Request.RawUrl, false);
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }


        }

        private void loadDefault()
        {
            txtEPFNo.Text = "";
            txtEPFNo.ReadOnly = false;
            txtEmpCode.Text = "";
            txtEmpCode.ReadOnly = false;
            ddlSex.Enabled = true;
            ddlTitle.Enabled = true;
            ddlSex.SelectedValue = "M";
            ddlTitle.SelectedValue = "Mr.";
            txtEmpFirstName.Text = "";
            txtEmpLastName.Text = "";
            txtEmpNameInt.Text = "";
            txtEmpNIC.Text = "";
            txtEmpDOB.Text = System.DateTime.Now.ToShortDateString();

            txtManager.Text = "";
            txtCategory.Text = "";
            txtSubCat.Text = "";
            txtContractor.Text = "";
            txtDepProfit.Text = "";
            txtSupvsr.Text = "";

            txtEmpEMail.Text = "";
            txtEmpMobile.Text = "";
            txtEmpHomePhone.Text = "";
            txtEmpOfficePhone.Text = "";
            txtEmpPolice.Text = "";
            txtEmpPAL1.Text = "";
            txtEmpPAL2.Text = "";
            txtEmpPAL3.Text = "";
            txtEmpCAL1.Text = "";
            txtEmpCAL2.Text = "";
            txtEmpCAL3.Text = "";

            optActive.Checked = true;
            optInactive.Checked = false;
            chkSVChange.Checked = false;
            txtMaxSVal.Enabled = false;
            txtMaxSVal.Text = "0.00";


            txtPCPrftCntr.Text = "";
            txtPCAssDate.Text = System.DateTime.Now.ToShortDateString();
            txtPCRptCode.Text = "";
            txtPCMaxSVal.Text = "0.00";
            txtPCManager.Text = "";
            chkPCStatus.Checked = true;
            chkPCPermission.Checked = false;
            grdemployeemaster_profitmaster.DataSource = new List<MasterProfitCenter_LocationEmployee>();
            grdemployeemaster_profitmaster.DataBind();

            txtLLocation.Text = "";
            txtLAssDate.Text = System.DateTime.Now.ToShortDateString();
            txtLRptCde.Text = "";
            txtLMaxSVal.Text = "0.00";
            txtLManager.Text = "";
            chkLStatus.Checked = true;
            chkLPermission.Checked = false;
            grdemployeemaster_location.DataSource = new List<MasterProfitCenter_LocationEmployee>();
            grdemployeemaster_location.DataBind();

            txtCusCde.Text = "";
            txtCusName.Text = "";
            chkCusAssStatus.Checked = true;
            grdcustomer_employee.DataSource = new List<MasterCustomerEmployee>();
            grdcustomer_employee.DataBind();

            CompanyBasedCurrency();
        }

        protected void lbtnAddNewEmp_Click(object sender, EventArgs e)
        {
            try
            {
                WarningEmployee.Visible = false;
                SuccessEmployee.Visible = false;


                if (!validateinputString(txtEPFNo.Text))
                {
                    DisplayMessage("Invalid charactor found in employee code.", 2);
                    txtEPFNo.Focus();
                    return;
                }
                if (!validateinputString(txtEmpCode.Text))
                {
                    DisplayMessage("Invalid charactor found in EPF.", 2);
                    txtEmpCode.Focus();
                    return;
                }
                if (!validateinputString(txtEmpFirstName.Text))
                {
                    DisplayMessage("Invalid charactor found in first name.", 2);
                    txtEmpFirstName.Focus();
                    return;
                }
                if (!validateinputString(txtEmpLastName.Text))
                {
                    DisplayMessage("Invalid charactor found in last name.", 2);
                    txtEmpLastName.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtEmpNameInt.Text))
                {
                    DisplayMessage("Invalid charactor found in name with initials.", 2);
                    txtEmpNameInt.Focus();
                    return;
                }
                if (!validateinputString(txtEmpNIC.Text))
                {
                    DisplayMessage("Invalid charactor found in NIC.", 2);
                    txtEmpNIC.Focus();
                    return;
                }
                if (!validateinputString(txtEmpMobile.Text))
                {
                    DisplayMessage("Invalid charactor found in mobile no.", 2);
                    txtEmpMobile.Focus();
                    return;
                }
                if (!validateinputString(txtEmpHomePhone.Text))
                {
                    DisplayMessage("Invalid charactor found in home phone no.", 2);
                    txtEmpHomePhone.Focus();
                    return;
                }
                if (!validateinputString(txtEmpOfficePhone.Text))
                {
                    DisplayMessage("Invalid charactor found in office phone no.", 2);
                    txtEmpOfficePhone.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtEmpPolice.Text))
                {
                    DisplayMessage("Invalid charactor found in police station.", 2);
                    txtEmpPolice.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtEmpPAL1.Text))
                {
                    DisplayMessage("Invalid charactor found in permanant address line 1.", 2);
                    txtEmpPAL1.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtEmpPAL2.Text))
                {
                    DisplayMessage("Invalid charactor found in permanant address line 2.", 2);
                    txtEmpPAL2.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtEmpPAL3.Text))
                {
                    DisplayMessage("Invalid charactor found in permanant address line 3.", 2);
                    txtEmpPAL3.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtEmpCAL1.Text))
                {
                    DisplayMessage("Invalid charactor found in current address line 1.", 2);
                    txtEmpCAL1.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtEmpCAL2.Text))
                {
                    DisplayMessage("Invalid charactor found in current address line 2.", 2);
                    txtEmpCAL2.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtEmpCAL3.Text))
                {
                    DisplayMessage("Invalid charactor found in current address line 3.", 2);
                    txtEmpCAL3.Focus();
                    return;
                }
                if (!validateinputString(txtManager.Text))
                {
                    DisplayMessage("Invalid charactor found in manager code.", 2);
                    txtManager.Focus();
                    return;
                }
                if (!validateinputString(txtContractor.Text))
                {
                    DisplayMessage("Invalid charactor found in contractor code.", 2);
                    txtContractor.Focus();
                    return;
                }
                if (!validateinputString(txtCategory.Text))
                {
                    DisplayMessage("Invalid charactor found in designation code.", 2);
                    txtCategory.Focus();
                    return;
                }
                if (!validateinputString(txtDepProfit.Text))
                {
                    DisplayMessage("Invalid charactor found in department code.", 2);
                    txtDepProfit.Focus();
                    return;
                }
                if (!validateinputString(txtSubCat.Text))
                {
                    DisplayMessage("Invalid charactor found in sub category code.", 2);
                    txtSubCat.Focus();
                    return;
                }
                if (!validateinputString(txtSupvsr.Text))
                {
                    DisplayMessage("Invalid charactor found in supervisor code.", 2);
                    txtSupvsr.Focus();
                    return;
                }
                if (!validateinputString(txtPCManager.Text))
                {
                    DisplayMessage("Invalid charactor found in emp profit center manager code.", 2);
                    txtPCManager.Focus();
                    return;
                }

                //check firstname for empty
                if (string.IsNullOrEmpty(txtEmpFirstName.Text))
                {
                    DisplayMessage("Please enter the first name!", 2);
                    txtEmpFirstName.Focus();
                    return;
                }

                //check last name for empty
                if (string.IsNullOrEmpty(txtEmpLastName.Text))
                {
                    DisplayMessage("Please enter the last name!", 2);
                    txtEmpLastName.Focus();
                    return;
                }

                //check name with initials for empty
                if (string.IsNullOrEmpty(txtEmpNameInt.Text))
                {
                    DisplayMessage("Please enter name with initials!", 2);
                    txtEmpNameInt.Focus();
                    return;
                }

                //check email for empty
                if (string.IsNullOrEmpty(txtEmpNIC.Text))
                {
                    DisplayMessage("Please enter the NIC number!", 2);
                    txtEmpNIC.Focus();
                    return;
                }

                //check Date of Birth for empty
                if (string.IsNullOrEmpty(txtEmpDOB.Text))
                {
                    DisplayMessage("Please enter the date of birth!", 2);
                    txtEmpDOB.Focus();
                    return;
                }

                //check email for empty
                if (string.IsNullOrEmpty(txtEmpEMail.Text))
                {
                    DisplayMessage("Please enter the email address!", 2);
                    txtEmpEMail.Focus();
                    return;
                }

                //check mobile number for empty
                if (string.IsNullOrEmpty(txtEmpMobile.Text))
                {
                    DisplayMessage("Please enter the mobile number!", 2);
                    txtEmpMobile.Focus();
                    return;
                }

                ////check home telephone number for empty
                //if (string.IsNullOrEmpty(txtEmpHomePhone.Text))
                //{
                //    DisplayMessage("Please enter home telephone number!!!", 2);                    
                //    txtEmpHomePhone.Focus();
                //    return;
                //}

                ////check office telephone numberfor empty
                //if (string.IsNullOrEmpty(txtEmpOfficePhone.Text))
                //{
                //    DisplayMessage("Please enter office telephone number!!!", 2);
                //    txtEmpOfficePhone.Focus();
                //    return;
                //}

                ////check police station for empty
                //if (string.IsNullOrEmpty(txtEmpPolice.Text))
                //{
                //    DisplayMessage("Please enter police station!!!", 2);
                //    txtEmpPolice.Focus();
                //    return;
                //}

                /*
                //check permanant address line 1 for empty
                if (string.IsNullOrEmpty(txtEmpPAL1.Text))
                {
                    DisplayMessage("Please enter permanant address line 1!!!", 2);
                    txtEmpPAL1.Focus();
                    return;
                }

                //check permanant address line 2 for empty
                if (string.IsNullOrEmpty(txtEmpPAL2.Text))
                {
                    DisplayMessage("Please enter permanant address line 2!!!", 2);
                    txtEmpPAL2.Focus();
                    return;
                }

                //check permanant address line 3 for empty
                if (string.IsNullOrEmpty(txtEmpPAL3.Text))
                {
                    DisplayMessage("Please enter permanant address line 3!!!", 2);
                    txtEmpPAL3.Focus();
                    return;
                }

                //check current address line 1 for empty
                if (string.IsNullOrEmpty(txtEmpCAL1.Text))
                {
                    DisplayMessage("Please enter current address line 1!!!", 2);
                    txtEmpCAL1.Focus();
                    return;
                }

                //check current address line 2 for empty
                if (string.IsNullOrEmpty(txtEmpCAL2.Text))
                {
                    DisplayMessage("Please enter current address line 2!!!", 2);
                    txtEmpCAL2.Focus();
                    return;
                }

                //check permanant address line 3 for empty
                if (string.IsNullOrEmpty(txtEmpCAL3.Text))
                {
                    DisplayMessage("Please enter current address line 3!!!", 2);
                    txtEmpCAL3.Focus();
                    return;
                }
                */
                //check EPF number for empty
                if (string.IsNullOrEmpty(txtEPFNo.Text))
                {
                    DisplayMessage("Please enter the EPF number!", 2);
                    txtEPFNo.Focus();
                    return;
                }

                //check manager for empty
                if (string.IsNullOrEmpty(txtManager.Text))
                {
                    DisplayMessage("Please enter the manager name!", 2);
                    txtManager.Focus();
                    return;
                }

                //check employee code for empty
                if (string.IsNullOrEmpty(txtEmpCode.Text))
                {
                    DisplayMessage("Please enter the employee code!", 2);
                    txtEmpCode.Focus();
                    return;
                }

                ////check contractor name for empty
                //if (string.IsNullOrEmpty(txtContractor.Text))
                //{
                //    DisplayMessage("Please enter contractor name!!!", 2);
                //    txtContractor.Focus();
                //    return;
                //}

                //check designation for empty
                if (string.IsNullOrEmpty(txtCategory.Text))
                {
                    DisplayMessage("Please enter the designation!", 2);
                    txtCategory.Focus();
                    return;
                }

                //check department profit for empty
                if (string.IsNullOrEmpty(txtDepProfit.Text))
                {
                    DisplayMessage("Please enter the department!", 2);
                    txtDepProfit.Focus();
                    return;
                }

                ////check subcategory for empty
                //if (string.IsNullOrEmpty(txtSubCat.Text))
                //{
                //    DisplayMessage("Please enter the subcategory!", 2);
                //    txtSubCat.Focus();
                //    return;
                //}

                ////check supervisor for empty
                //if (string.IsNullOrEmpty(txtSupvsr.Text))
                //{
                //    DisplayMessage("Please enter the supervisor!", 2);
                //    txtSupvsr.Focus();
                //    return;
                //}

                //save employee details to databse 
                if (txtSaveconformmessageValue.Value == "Yes")
                {
                    SaveEmployee();
                }

                _MPcE = ViewState["_MPcE"] as List<MasterProfitCenter_LocationEmployee>;
                _MLE = ViewState["_MLE"] as List<MasterProfitCenter_LocationEmployee>;
                _MstCusEmp = ViewState["_MstCusEmp"] as List<MasterCustomerEmployee>;

                string _err;

                // _lstcomItem = null;

                int row_aff = CHNLSVC.General.SaveNewEmployee(_employeeNew, _MPcE, _MLE, _MstCusEmp, Session["UserCompanyCode"].ToString(), out _err);
                if (row_aff == 1)
                {
                    // string msg = "Employee sucessfully created!," + "Employee Code:" + txtEmpCode.Text + "EPF No:" + txtEPFNo.Text;
                    string msg2 = "Employee successfully created! Employee Code : " + txtEPFNo.Text.ToString() + " , EPF No : " + txtEmpCode.Text.ToString();
                    DisplayMessage(msg2, 3);
                    loadDefault();
                }
                else
                {
                    string Msg = _err.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
                    DisplayMessage(Msg, 2);
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

        protected void lbtnUpdateEmp_Click(object sender, EventArgs e)
        {
            try
            {
                WarningEmployee.Visible = false;
                SuccessEmployee.Visible = false;

                if (!validateinputString(txtEmpFirstName.Text))
                {
                    DisplayMessage("Invalid charactor found in first name.", 2);
                    txtEmpFirstName.Focus();
                    return;
                }
                if (!validateinputString(txtEmpLastName.Text))
                {
                    DisplayMessage("Invalid charactor found in last name.", 2);
                    txtEmpLastName.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtEmpNameInt.Text))
                {
                    DisplayMessage("Invalid charactor found in name with initials.", 2);
                    txtEmpNameInt.Focus();
                    return;
                }
                if (!validateinputString(txtEmpNIC.Text))
                {
                    DisplayMessage("Invalid charactor found in NIC.", 2);
                    txtEmpNIC.Focus();
                    return;
                }
                if (!validateinputString(txtEmpMobile.Text))
                {
                    DisplayMessage("Invalid charactor found in mobile no.", 2);
                    txtEmpMobile.Focus();
                    return;
                }
                if (!validateinputString(txtEmpHomePhone.Text))
                {
                    DisplayMessage("Invalid charactor found in home phone no.", 2);
                    txtEmpHomePhone.Focus();
                    return;
                }
                if (!validateinputString(txtEmpOfficePhone.Text))
                {
                    DisplayMessage("Invalid charactor found in office phone no.", 2);
                    txtEmpOfficePhone.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtEmpPolice.Text))
                {
                    DisplayMessage("Invalid charactor found in police station.", 2);
                    txtEmpPolice.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtEmpPAL1.Text))
                {
                    DisplayMessage("Invalid charactor found in permanant address line 1.", 2);
                    txtEmpPAL1.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtEmpPAL2.Text))
                {
                    DisplayMessage("Invalid charactor found in permanant address line 2.", 2);
                    txtEmpPAL2.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtEmpPAL3.Text))
                {
                    DisplayMessage("Invalid charactor found in permanant address line 3.", 2);
                    txtEmpPAL3.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtEmpCAL1.Text))
                {
                    DisplayMessage("Invalid charactor found in current address line 1.", 2);
                    txtEmpCAL1.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtEmpCAL2.Text))
                {
                    DisplayMessage("Invalid charactor found in current address line 2.", 2);
                    txtEmpCAL2.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtEmpCAL3.Text))
                {
                    DisplayMessage("Invalid charactor found in current address line 3.", 2);
                    txtEmpCAL3.Focus();
                    return;
                }
                if (!validateinputString(txtManager.Text))
                {
                    DisplayMessage("Invalid charactor found in manager code.", 2);
                    txtManager.Focus();
                    return;
                }
                if (!validateinputString(txtContractor.Text))
                {
                    DisplayMessage("Invalid charactor found in contractor code.", 2);
                    txtContractor.Focus();
                    return;
                }
                if (!validateinputString(txtCategory.Text))
                {
                    DisplayMessage("Invalid charactor found in designation code.", 2);
                    txtCategory.Focus();
                    return;
                }
                if (!validateinputString(txtDepProfit.Text))
                {
                    DisplayMessage("Invalid charactor found in department code.", 2);
                    txtDepProfit.Focus();
                    return;
                }
                if (!validateinputString(txtSubCat.Text))
                {
                    DisplayMessage("Invalid charactor found in sub category code.", 2);
                    txtSubCat.Focus();
                    return;
                }
                if (!validateinputString(txtSupvsr.Text))
                {
                    DisplayMessage("Invalid charactor found in supervisor code.", 2);
                    txtSupvsr.Focus();
                    return;
                }
                if (!validateinputString(txtPCManager.Text))
                {
                    DisplayMessage("Invalid charactor found in emp profit center manager code.", 2);
                    txtPCManager.Focus();
                    return;
                }

                //check firstname for empty
                if (string.IsNullOrEmpty(txtEmpFirstName.Text))
                {
                    DisplayMessage("Please enter the first name!", 2);
                    txtEmpFirstName.Focus();
                    return;
                }

                //check last name for empty
                if (string.IsNullOrEmpty(txtEmpLastName.Text))
                {
                    DisplayMessage("Please enter the last name!", 2);
                    txtEmpLastName.Focus();
                    return;
                }

                //check name with initials for empty
                if (string.IsNullOrEmpty(txtEmpNameInt.Text))
                {
                    DisplayMessage("Please enter name with initials!", 2);
                    txtEmpNameInt.Focus();
                    return;
                }

                //check email for empty
                if (string.IsNullOrEmpty(txtEmpNIC.Text))
                {
                    DisplayMessage("Please enter the NIC number!", 2);
                    txtEmpNIC.Focus();
                    return;
                }

                //check Date of Birth for empty
                if (string.IsNullOrEmpty(txtEmpDOB.Text))
                {
                    DisplayMessage("Please enter the date of birth!", 2);
                    txtEmpDOB.Focus();
                    return;
                }

                //check email for empty
                if (string.IsNullOrEmpty(txtEmpEMail.Text))
                {
                    DisplayMessage("Please enter the email address!", 2);
                    txtEmpEMail.Focus();
                    return;
                }

                //check mobile number for empty
                if (string.IsNullOrEmpty(txtEmpMobile.Text))
                {
                    DisplayMessage("Please enter the mobile number!", 2);
                    txtEmpMobile.Focus();
                    return;
                }

                ////check home telephone number for empty
                //if (string.IsNullOrEmpty(txtEmpHomePhone.Text))
                //{
                //    DisplayMessage("Please enter home telephone number!!!", 2);
                //    txtEmpHomePhone.Focus();
                //    return;
                //}

                ////check office telephone numberfor empty
                //if (string.IsNullOrEmpty(txtEmpOfficePhone.Text))
                //{
                //    DisplayMessage("Please enter office telephone number!!!", 2);
                //    txtEmpOfficePhone.Focus();
                //    return;
                //}

                ////check police station for empty
                //if (string.IsNullOrEmpty(txtEmpPolice.Text))
                //{
                //    DisplayMessage("Please enter police station!!!", 2);
                //    txtEmpPolice.Focus();
                //    return;
                //}

                ////check permanant address line 1 for empty
                //if (string.IsNullOrEmpty(txtEmpPAL1.Text))
                //{
                //    DisplayMessage("Please enter permanant address line 1!!!", 2);
                //    txtEmpPAL1.Focus();
                //    return;
                //}

                ////check permanant address line 2 for empty
                //if (string.IsNullOrEmpty(txtEmpPAL2.Text))
                //{
                //    DisplayMessage("Please enter permanant address line 2!!!", 2);
                //    txtEmpPAL2.Focus();
                //    return;
                //}

                ////check permanant address line 3 for empty
                //if (string.IsNullOrEmpty(txtEmpPAL3.Text))
                //{
                //    DisplayMessage("Please enter permanant address line 3!!!", 2);
                //    txtEmpPAL3.Focus();
                //    return;
                //}

                ////check current address line 1 for empty
                //if (string.IsNullOrEmpty(txtEmpCAL1.Text))
                //{
                //    DisplayMessage("Please enter current address line 1!!!", 2);
                //    txtEmpCAL1.Focus();
                //    return;
                //}

                ////check current address line 2 for empty
                //if (string.IsNullOrEmpty(txtEmpCAL2.Text))
                //{
                //    DisplayMessage("Please enter current address line 2!!!", 2);
                //    txtEmpCAL2.Focus();
                //    return;
                //}

                ////check permanant address line 3 for empty
                //if (string.IsNullOrEmpty(txtEmpCAL3.Text))
                //{
                //    DisplayMessage("Please enter current address line 3!!!", 2);
                //    txtEmpCAL3.Focus();
                //    return;
                //}

                //check EPF number for empty
                if (string.IsNullOrEmpty(txtEPFNo.Text))
                {
                    DisplayMessage("Please enter the EPF number!", 2);
                    txtEPFNo.Focus();
                    return;
                }

                //check manager for empty
                if (string.IsNullOrEmpty(txtManager.Text))
                {
                    DisplayMessage("Please enter the manager name!", 2);
                    txtManager.Focus();
                    return;
                }

                //check employee code for empty
                if (string.IsNullOrEmpty(txtEmpCode.Text))
                {
                    DisplayMessage("Please enter the employee code!", 2);
                    txtEmpCode.Focus();
                    return;
                }

                ////check contractor name for empty
                //if (string.IsNullOrEmpty(txtContractor.Text))
                //{
                //    DisplayMessage("Please enter contractor name!!!", 2);
                //    txtContractor.Focus();
                //    return;
                //}

                //check designation for empty
                if (string.IsNullOrEmpty(txtCategory.Text))
                {
                    DisplayMessage("Please enter the designation!", 2);
                    txtCategory.Focus();
                    return;
                }

                //check department profit for empty
                if (string.IsNullOrEmpty(txtDepProfit.Text))
                {
                    DisplayMessage("Please enter the department!", 2);
                    txtDepProfit.Focus();
                    return;
                }

                ////check subcategory for empty
                //if (string.IsNullOrEmpty(txtSubCat.Text))
                //{
                //    DisplayMessage("Please enter the subcategory!", 2);
                //    txtSubCat.Focus();
                //    return;
                //}

                ////check supervisor for empty
                //if (string.IsNullOrEmpty(txtSupvsr.Text))
                //{
                //    DisplayMessage("Please enter the supervisor!", 2);
                //    txtSupvsr.Focus();
                //    return;
                //}
                if (txtSaveconformmessageValue.Value == "No")
                {
                    return;

                }

                //save employee details to databse 
                if (txtSaveconformmessageValue.Value == "Yes")
                {
                    _MPcE = ViewState["_MPcE"] as List<MasterProfitCenter_LocationEmployee>;
                    _MLE = ViewState["_MLE"] as List<MasterProfitCenter_LocationEmployee>;
                    _MstCusEmp = ViewState["_MstCusEmp"] as List<MasterCustomerEmployee>;

                    if (_MPcE != null)
                    {
                        foreach (GridViewRow pcgrv in grdemployeemaster_profitmaster.Rows)
                        {
                            string _profitcenter = (pcgrv.FindControl("lbl_PrftCntr") as Label).Text;
                            CheckBox pcpermission = (CheckBox)pcgrv.FindControl("chk_PCPermission");
                            CheckBox pcstatus = (CheckBox)pcgrv.FindControl("chk_PCStatus");

                            var _chngviewstate_profitcenter = _MPcE.Where(x => x._mpce_pc == _profitcenter).FirstOrDefault();
                            if (_chngviewstate_profitcenter != null)
                            {
                                _chngviewstate_profitcenter._mpce_act = (pcstatus.Checked == true) ? 1 : 0;
                                _chngviewstate_profitcenter._mpce_is_rest = (pcpermission.Checked == true) ? 1 : 0;
                            }
                            /*
                                  MasterProfitCenter_LocationEmployee _pcEmp = new MasterProfitCenter_LocationEmployee();
                                  _pcEmp._mpce_epf = txtEPFNo.Text.ToUpper();
                                  _pcEmp._mpce_com = Session["UserCompanyCode"].ToString();
                                  _pcEmp._mpce_pc = txtPCPrftCntr.Text;
                                  _pcEmp._mpce_assn_dt = Convert.ToDateTime(txtPCAssDate.Text);
                                  _pcEmp._mpce_rep_cd = txtPCRptCode.Text;
                                  _pcEmp._mpce_anal_1 = "P";
                                  _pcEmp._mpce_act = (pcpermission.Checked == true) ? 1 : 0;
                                  _pcEmp._mpce_max_stk_val = Convert.ToInt32(txtPCMaxSVal.Text);
                                  _pcEmp._mpce_mgr = txtPCManager.Text;
                                  _pcEmp._mpce_is_rest = (pcstatus.Checked == true) ? 1 : 0;
                                  _MPcE.Add(_pcEmp);
                           */

                        }
                    }

                    if (_MLE != null)
                    {
                        foreach (GridViewRow lgrv in grdemployeemaster_location.Rows)
                        {
                            string _location = (lgrv.FindControl("lbl_LLocation") as Label).Text;
                            CheckBox lpermission = (CheckBox)lgrv.FindControl("chk_LPermission");
                            CheckBox lstatus = (CheckBox)lgrv.FindControl("chk_LStatus");

                            var _chngviewstate_location = _MLE.Single(x => x._mpce_pc == _location);
                            if (_chngviewstate_location != null)
                            {
                                _chngviewstate_location._mpce_act = (lstatus.Checked == true) ? 1 : 0;
                                _chngviewstate_location._mpce_is_rest = (lpermission.Checked == true) ? 1 : 0;
                            }

                            /*                                                       
                                                         
                            MasterProfitCenter_LocationEmployee _lEmp = new MasterProfitCenter_LocationEmployee();
                            _lEmp._mpce_epf = txtEPFNo.Text.ToUpper();
                            _lEmp._mpce_com = Session["UserCompanyCode"].ToString();
                            _lEmp._mpce_pc = txtLLocation.Text;
                            _lEmp._mpce_assn_dt = Convert.ToDateTime(txtLAssDate.Text);
                            _lEmp._mpce_rep_cd = txtLRptCde.Text;
                            _lEmp._mpce_anal_1 = "L";
                            _lEmp._mpce_act = (lpermission.Checked == true) ? 1 : 0;
                            _lEmp._mpce_max_stk_val = Convert.ToInt32(txtLMaxSVal.Text);
                            _lEmp._mpce_mgr = txtLManager.Text;
                            _lEmp._mpce_is_rest = (lstatus.Checked == true) ? 1 : 0;
                            _MLE.Add(_lEmp);
                             
                            */
                        }
                    }
                    if (_MstCusEmp != null)
                    {
                        foreach (GridViewRow cgrv in grdcustomer_employee.Rows)
                        {
                            string _customer = (cgrv.FindControl("lbl_CusCde") as Label).Text;
                            CheckBox cusstatus = (CheckBox)cgrv.FindControl("chk_CusStatus");

                            var _chngviewstate_customers = _MstCusEmp.Single(x => x._mpce_cus_cd == _customer);

                            if (_chngviewstate_customers != null)
                            {
                                _chngviewstate_customers._mpce_stus = (cusstatus.Checked == true) ? 1 : 0;
                                _chngviewstate_customers._mpce_mod_by = Session["UserID"].ToString();
                            }

                        }
                    }


                    UpdateEmployee();


                }


                // _MPcE = ViewState["_MPcE"] as List<MasterProfitCenter_LocationEmployee>;
                // _MLE = ViewState["_MLE"] as List<MasterProfitCenter_LocationEmployee>;
                // _MstCusEmp = ViewState["_MstCusEmp"] as List<MasterCustomerEmployee>;

                string _err;

                // _lstcomItem = null;


                int row_aff = CHNLSVC.General.UpdateEmployee(_employeeNew, _MPcE, _MLE, _MstCusEmp, Session["UserCompanyCode"].ToString(), out _err);
                if (row_aff == 1)
                {
                    string msg = "Employee Updated Successfully !";
                    DisplayMessage(msg, 3);
                    loadDefault();
                }
                else
                {
                    string Msg = _err.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
                    DisplayMessage(Msg, 2);
                }

                // pageClear();


                txtSaveconformmessageValue.Value = "";

                if (txtSaveconformmessageValue.Value == "Yes")
                {
                    UpdateEmployee();

                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 2);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnClearEmp_Click(object sender, EventArgs e)
        {
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

        //  Save Employee Data
        private void SaveEmployee()
        {
            Int32 row_aff = 0;
            string _userId = string.Empty;
            string _msg = string.Empty;

            try
            {

                _employeeNew.ESEP_epf = txtEPFNo.Text.ToUpper();
                _employeeNew.ESEP_com_cd = Session["UserCompanyCode"].ToString();//
                _employeeNew.ESEP_cd = txtEmpCode.Text.ToUpper();
                _employeeNew.ESEP_cat_cd = txtCategory.Text.ToUpper();
                _employeeNew.ESEP_cat_subcd = txtSubCat.Text.ToUpper();
                _employeeNew.ESEP_manager_cd = txtManager.Text.ToUpper();

                ////Mr
                //if (optMr.Checked == true)
                //{
                //    _employeeNew.ESEP_title = "Mr.";            
                //}

                ////Mrs
                //if (optMrs.Checked == true)
                //{
                //    _employeeNew.ESEP_title = "Mrs.";            
                //}

                ////Ms
                //if (optMs.Checked == true)
                //{
                //    _employeeNew.ESEP_title = "Ms.";          
                //}
                _employeeNew.ESEP_title = ddlTitle.SelectedValue;

                _employeeNew.ESEP_name_initials = txtEmpNameInt.Text.ToUpper();
                _employeeNew.ESEP_first_name = txtEmpFirstName.Text.ToUpper();
                _employeeNew.ESEP_last_name = txtEmpLastName.Text.ToUpper();

                _employeeNew.ESEP_sex = ddlSex.SelectedValue;
                ////Male
                //if (optMale.Checked == true)
                //{
                //    _employeeNew.ESEP_sex = "M";          
                //}

                ////Female
                //if (optFemale.Checked == true)
                //{
                //    _employeeNew.ESEP_sex = "F";              
                //}

                _employeeNew.ESEP_dob = Convert.ToDateTime(txtEmpDOB.Text).Date;

                _employeeNew.ESEP_per_add_1 = txtEmpPAL1.Text.ToUpper();
                _employeeNew.ESEP_per_add_2 = txtEmpPAL2.Text.ToUpper();
                _employeeNew.ESEP_per_add_3 = txtEmpPAL3.Text.ToUpper();
                _employeeNew.ESEP_living_add_1 = txtEmpCAL1.Text.ToUpper();
                _employeeNew.ESEP_living_add_2 = txtEmpCAL2.Text.ToUpper();
                _employeeNew.ESEP_living_add_3 = txtEmpCAL3.Text.ToUpper();
                _employeeNew.ESEP_police_station = txtEmpPolice.Text.ToUpper();
                _employeeNew.ESEP_mobi_no = txtEmpMobile.Text;
                _employeeNew.ESEP_tel_home_no = txtEmpHomePhone.Text;
                _employeeNew.ESEP_tel_off_no = txtEmpOfficePhone.Text;
                _employeeNew.ESEP_nic = txtEmpNIC.Text.ToUpper();
                _employeeNew.ESEP_contractor = txtContractor.Text.ToUpper();
                _employeeNew.ESEP_is_max_stock_val = (chkSVChange.Checked == true) ? 1 : 0;
                _employeeNew.ESEP_max_stock_val = Convert.ToDecimal(txtMaxSVal.Text);

                //Active
                if (optActive.Checked == true)
                {
                    _employeeNew.ESEP_act = 1;
                }

                //Inactive
                if (optInactive.Checked == true)
                {
                    _employeeNew.ESEP_act = 0;
                }

                _employeeNew.ESEP_cre_by = Session["UserID"].ToString();
                _employeeNew.ESEP_mod_by = Session["UserID"].ToString();
                _employeeNew.ESEP_session_id = Session["SessionID"].ToString();
                _employeeNew.ESEP_dep_profit = txtDepProfit.Text;
                _employeeNew.ESEP_supwise_cd = txtSupvsr.Text.ToUpper();
                _employeeNew.ESEP_email = txtEmpEMail.Text;

                string confirmValue1 = Request.Form["confirm_value"];
                if (confirmValue1 == "No")
                {
                    return;
                }
                //if (MessageBox.Show("Are you sure to save ?", "Confirm Save", MessageBoxButtons.YesNo) == DialogResult.No)
                //{
                //    return;
                //}

                /*
                //------------------------------------------------------------------------------------------------------------
                
                row_aff = CHNLSVC.General.SaveNewEmployee(_employeeNew, _MPcLE, Session["UserCompanyCode"].ToString(), out _err);
                //------------------------------------------------------------------------------------------------------------

                if (row_aff == 1)
                {
                    // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully created.");
                    lblWEmployee.Text = "Successfully created!";
                    SuccessEmployee.Visible = true;
                }
                else
                {
                    lblWEmployee.Text = "Creation Failed";
                    WarningEmployee.Visible = true;
                    // MessageBox.Show("Creation Failed", "Create", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                */

            }

            catch (Exception e)
            {
                //  MessageBox.Show(e.Message, "Create", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        //  Update Employee Data
        private void UpdateEmployee()
        {
            Int32 row_aff = 0;
            string _userId = string.Empty;
            string _msg = string.Empty;

            try
            {

                _employeeNew.ESEP_epf = txtEPFNo.Text.ToUpper();
                _employeeNew.ESEP_com_cd = Session["UserCompanyCode"].ToString();//
                _employeeNew.ESEP_cd = txtEmpCode.Text.ToUpper();
                _employeeNew.ESEP_cat_cd = txtCategory.Text.ToUpper();
                _employeeNew.ESEP_cat_subcd = txtSubCat.Text.ToUpper();
                _employeeNew.ESEP_manager_cd = txtManager.Text.ToUpper();

                ////Mr
                //if (optMr.Checked == true)
                //{
                //    _employeeNew.ESEP_title = "Mr.";
                //}

                ////Mrs
                //if (optMrs.Checked == true)
                //{
                //    _employeeNew.ESEP_title = "Mrs.";
                //}

                ////Ms
                //if (optMs.Checked == true)
                //{
                //    _employeeNew.ESEP_title = "Ms.";
                //}
                _employeeNew.ESEP_title = ddlTitle.SelectedValue;

                _employeeNew.ESEP_name_initials = txtEmpNameInt.Text.ToUpper();
                _employeeNew.ESEP_first_name = txtEmpFirstName.Text.ToUpper();
                _employeeNew.ESEP_last_name = txtEmpLastName.Text.ToUpper();

                _employeeNew.ESEP_sex = ddlSex.SelectedValue;
                ////Male
                //if (optMale.Checked == true)
                //{
                //    _employeeNew.ESEP_sex = "M";
                //}

                ////Female
                //if (optFemale.Checked == true)
                //{
                //    _employeeNew.ESEP_sex = "F";
                //}

                _employeeNew.ESEP_dob = Convert.ToDateTime(txtEmpDOB.Text);

                _employeeNew.ESEP_per_add_1 = txtEmpPAL1.Text.ToUpper();
                _employeeNew.ESEP_per_add_2 = txtEmpPAL2.Text.ToUpper();
                _employeeNew.ESEP_per_add_3 = txtEmpPAL3.Text.ToUpper();
                _employeeNew.ESEP_living_add_1 = txtEmpCAL1.Text.ToUpper();
                _employeeNew.ESEP_living_add_2 = txtEmpCAL2.Text.ToUpper();
                _employeeNew.ESEP_living_add_3 = txtEmpCAL3.Text.ToUpper();
                _employeeNew.ESEP_police_station = txtEmpPolice.Text.ToUpper();
                _employeeNew.ESEP_mobi_no = txtEmpMobile.Text;
                _employeeNew.ESEP_tel_home_no = txtEmpHomePhone.Text;
                _employeeNew.ESEP_tel_off_no = txtEmpOfficePhone.Text;
                _employeeNew.ESEP_nic = txtEmpNIC.Text.ToUpper();
                _employeeNew.ESEP_contractor = txtContractor.Text.ToUpper();
                _employeeNew.ESEP_is_max_stock_val = (chkSVChange.Checked == true) ? 1 : 0;
                _employeeNew.ESEP_max_stock_val = Convert.ToDecimal(txtMaxSVal.Text);

                //Active
                if (optActive.Checked == true)
                {
                    _employeeNew.ESEP_act = 1;
                }

                //Inactive
                if (optInactive.Checked == true)
                {
                    _employeeNew.ESEP_act = 0;
                }

                _employeeNew.ESEP_cre_by = Session["UserID"].ToString();
                _employeeNew.ESEP_mod_by = Session["UserID"].ToString();
                _employeeNew.ESEP_session_id = Session["SessionID"].ToString();
                _employeeNew.ESEP_dep_profit = txtDepProfit.Text;
                _employeeNew.ESEP_supwise_cd = txtSupvsr.Text.ToUpper();
                _employeeNew.ESEP_email = txtEmpEMail.Text;

                string confirmValue1 = Request.Form["confirm_value"];
                if (confirmValue1 == "No")
                {
                    return;
                }


            }

            catch (Exception e)
            {
                //  MessageBox.Show(e.Message, "Create", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        //  Load Employee Data
        void Load_employees()
        {
            try
            {
                Employee _employee = new Employee();
                _employee = CHNLSVC.General.GetEmployeeMaster(txtEPFNo.Text.ToUpper(), Session["UserCompanyCode"].ToString());
                if (_employee != null)
                {
                    lbactive.Text = "";
                    if (_employee.ESEP_act == 0)
                    {
                        lbactive.Text = "Inactive Employee";
                        optInactive.Checked = true;
                    }
                    txtEPFNo.Text = _employee.ESEP_epf;
                    //_employee.ESEP_com_cd = Session["UserCompanyCode"].ToString();//
                    txtEmpCode.Text = _employee.ESEP_cd;
                    // txtCategory.Text = _employee.ESEP_cat_cd;
                    //txtSubCat.Text = _employee.ESEP_cat_subcd;
                    txtManager.Text = _employee.ESEP_manager_cd;


                    string[] manager = { };
                    manager = GetManger(_employee.ESEP_manager_cd);

                    if (manager == null || manager.Length == 0)
                    {
                        txtManager.Text = String.Empty;
                        txtManager.ToolTip = "";
                        txtManager.Focus();
                    }
                    else
                    {
                        txtManager.Text = manager[0];
                        txtManager.ToolTip = manager[1];
                        txtManager.Focus();
                    }

                    string[] category = { };
                    category = GetCategory(_employee.ESEP_cat_cd);

                    if (category == null || category.Length == 0)
                    {
                        txtCategory.Text = String.Empty;
                        txtCategory.ToolTip = "";
                        txtCategory.Focus();
                    }
                    else
                    {
                        txtCategory.Text = category[0];
                        txtCategory.ToolTip = category[1];
                        txtCategory.Focus();
                    }

                    string[] subcategory = { };
                    subcategory = GetsubCategory(_employee.ESEP_cat_subcd);

                    if (subcategory == null || subcategory.Length == 0)
                    {
                        txtSubCat.Text = String.Empty;
                        txtSubCat.ToolTip = "";
                        txtSubCat.Focus();
                    }
                    else
                    {
                        txtSubCat.Text = subcategory[0];
                        txtSubCat.ToolTip = subcategory[1];
                        txtSubCat.Focus();
                    }



                    ////Mr
                    //if (_employee.ESEP_title == "Mr.")
                    //{
                    //    optMr.Checked = true;
                    //}

                    ////Mrs
                    //if (_employee.ESEP_title == "Mrs.")
                    //{
                    //    optMrs.Checked = true;
                    //}

                    ////Ms
                    //if (_employee.ESEP_title == "Ms.")
                    //{
                    //    optMs.Checked = true;
                    //}
                    ddlTitle.SelectedItem.Text = _employee.ESEP_title;

                    txtEmpNameInt.Text = _employee.ESEP_name_initials;
                    txtEmpFirstName.Text = _employee.ESEP_first_name;
                    txtEmpLastName.Text = _employee.ESEP_last_name;

                    ddlSex.SelectedValue = _employee.ESEP_sex;
                    ////Male
                    //if (_employee.ESEP_sex == "M")
                    //{
                    //    optMale.Checked = true;
                    //}

                    ////Female
                    //if (_employee.ESEP_sex == "F")
                    //{
                    //    optFemale.Checked = true;
                    //}

                    txtEmpDOB.Text = _employee.ESEP_dob.ToString("dd/MMM/yyyy");

                    //txtDepProfit.Text = _employee.ESEP_dep_profit;

                    string[] department = { };
                    department = GetDepartment(_employee.ESEP_dep_profit);

                    if (department == null || department.Length == 0)
                    {
                        txtDepProfit.Text = String.Empty;
                        txtDepProfit.ToolTip = "";
                        txtDepProfit.Focus();
                    }
                    else
                    {
                        txtDepProfit.Text = department[0];
                        txtDepProfit.ToolTip = department[1];
                        txtDepProfit.Focus();
                    }
                    //txtSupvsr.Text = _employee.ESEP_supwise_cd;                   

                    string[] supervisor = { };
                    supervisor = GetSupervisor(_employee.ESEP_supwise_cd);

                    if (supervisor == null || supervisor.Length == 0)
                    {
                        txtSupvsr.Text = String.Empty;
                        txtSupvsr.ToolTip = "";
                        txtSupvsr.Focus();
                    }
                    else
                    {
                        txtSupvsr.Text = supervisor[0];
                        txtSupvsr.ToolTip = supervisor[1];
                        txtSupvsr.Focus();
                    }

                    txtEmpPAL1.Text = _employee.ESEP_per_add_1;
                    txtEmpPAL2.Text = _employee.ESEP_per_add_2;
                    txtEmpPAL3.Text = _employee.ESEP_per_add_3;
                    txtEmpCAL1.Text = _employee.ESEP_living_add_1;
                    txtEmpCAL2.Text = _employee.ESEP_living_add_2;
                    txtEmpCAL3.Text = _employee.ESEP_living_add_3;
                    txtEmpPolice.Text = _employee.ESEP_police_station;
                    txtEmpEMail.Text = _employee.ESEP_email;
                    txtEmpMobile.Text = _employee.ESEP_mobi_no;
                    txtEmpHomePhone.Text = _employee.ESEP_tel_home_no;
                    txtEmpOfficePhone.Text = _employee.ESEP_tel_off_no;
                    txtEmpNIC.Text = _employee.ESEP_nic;
                    txtContractor.Text = _employee.ESEP_contractor;
                    chkSVChange.Checked = (_employee.ESEP_is_max_stock_val == 1) ? true : false;
                    txtMaxSVal.Text = (_employee.ESEP_max_stock_val).ToString("#,##0.00");
                    checkStockValue();

                    //Profit Center
                    _MPcE = CHNLSVC.General.getEmployeeProfitCenter_Location(txtEPFNo.Text.ToUpper(), Session["UserCompanyCode"].ToString(), "P");
                    if (_MPcE != null)
                    {
                        _MPcE.Where(w => w._mpce_act == 1).ToList().ForEach(s => s._mpce_act = 1);
                        _MPcE.Where(w => w._mpce_act == 0).ToList().ForEach(s => s._mpce_act = 0);
                        _MPcE.Where(w => w._mpce_is_rest == 1).ToList().ForEach(s => s._mpce_is_rest = 1);
                        _MPcE.Where(w => w._mpce_is_rest == 0).ToList().ForEach(s => s._mpce_is_rest = 0);
                    }

                    // grdemployeemaster_profitmaster.DataSource = null;
                    grdemployeemaster_profitmaster.DataSource = new List<MasterProfitCenter_LocationEmployee>();
                    grdemployeemaster_profitmaster.DataSource = _MPcE;
                    if (grdemployeemaster_profitmaster.DataSource == null)
                    {
                        grdemployeemaster_profitmaster.DataSource = new string[] { };
                    }
                    grdemployeemaster_profitmaster.DataBind();
                    ViewState["_MPcE"] = _MPcE;

                    //_Location
                    _MLE = CHNLSVC.General.getEmployeeProfitCenter_Location(txtEPFNo.Text.ToUpper(), Session["UserCompanyCode"].ToString(), "L");
                    if (_MLE != null)
                    {
                        _MLE.Where(w => w._mpce_act == 1).ToList().ForEach(s => s._mpce_act = 1);
                        _MLE.Where(w => w._mpce_act == 0).ToList().ForEach(s => s._mpce_act = 0);
                        _MLE.Where(w => w._mpce_is_rest == 1).ToList().ForEach(s => s._mpce_is_rest = 1);
                        _MLE.Where(w => w._mpce_is_rest == 0).ToList().ForEach(s => s._mpce_is_rest = 0);
                    }

                    //grdemployeemaster_location.DataSource = null;
                    grdemployeemaster_location.DataSource = new List<MasterProfitCenter_LocationEmployee>();
                    grdemployeemaster_location.DataSource = _MLE;
                    if (grdemployeemaster_location.DataSource == null)
                    {
                        grdemployeemaster_location.DataSource = new string[] { };
                    }
                    grdemployeemaster_location.DataBind();
                    ViewState["_MLE"] = _MLE;

                    //Customers
                    _MstCusEmp = CHNLSVC.General.getEmployeeCustomers(txtEPFNo.Text.ToUpper(), Session["UserCompanyCode"].ToString());
                    if (_MstCusEmp != null)
                    {
                        _MstCusEmp.Where(w => w._mpce_stus == 1).ToList().ForEach(s => s._mpce_stus = 1);
                        _MstCusEmp.Where(w => w._mpce_stus == 0).ToList().ForEach(s => s._mpce_stus = 0);

                    }

                    // grdcustomer_employee.DataSource = null;
                    grdcustomer_employee.DataSource = new List<MasterCustomerEmployee>();
                    grdcustomer_employee.DataSource = _MstCusEmp;
                    if (grdcustomer_employee.DataSource == null)
                    {
                        grdcustomer_employee.DataSource = new string[] { };
                    }
                    grdcustomer_employee.DataBind();
                    ViewState["_MstCusEmp"] = _MstCusEmp;

                    lbtnAddNewEmp.Visible = false;

                    txtEPFNo.ReadOnly = true;
                    txtEmpCode.ReadOnly = true;
                }


            }

            catch (Exception err)
            {
                DisplayMessage(err.Message, 2);
            }

            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }


        #region Employee Personal Details

        //Get EPF No 
        protected void lbtnEPFNo_Click(object sender, EventArgs e)
        {

            //"87:|"
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeEPF);
            DataTable _result = CHNLSVC.General.Get_EPFNo(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "87";
            BindUCtrlDDLData(_result);
            txtSearchbyword.Text = "";
            UserPopoup.Show();
        }

        protected void txtEPFNo_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtEPFNo.Text))
            {
                DisplayMessage("Invalid charactor found in employee code.", 2);
                txtEPFNo.Focus();
                return;
            }
            Load_employees();
        }

        //Get Employee code
        protected void lbtnEmpCode_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            //"459:|"
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeCode);
            DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "459";
            BindUCtrlDDLData(_result);
            txtSearchbyword.Text = "";
            UserPopoup.Show();
        }


        protected void txtEmpCode_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtEmpCode.Text))
            {
                DisplayMessage("Invalid charactor found in EPF no.", 2);
                txtEmpCode.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtEmpCode.Text.Trim().ToUpper()))
            {
                return;
            }

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeCode);
            DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, "Code", "%" + txtEmpCode.Text.Trim().ToUpper().ToString());

            //var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtEmpCode.Text.Trim().ToUpper().ToString()).ToList();
            //if (_validate == null || _validate.Count <= 0)
            //{
            //    DisplayMessage("Invalid employee code", 2);
            //    txtPCManager.Text = string.Empty;
            //    txtPCManager.Focus();
            //    return;
            //}
            if (_result.Rows.Count > 0)
            {
                //txtDefExRt.Text = _result.;
                DataView dv = new DataView(_result);
                dv.RowFilter = "CODE ='" + txtEmpCode.Text.Trim().ToUpper() + "'";

                if (dv.Count > 0)
                {
                    txtEmpCode.Text = dv[0]["CODE"].ToString();
                    txtEPFNo.Text = dv[0]["EPF"].ToString();

                    Load_employees();
                }


            }

        }
        //Dulaj 2018/Sep/-01
        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        protected void txtEmpNIC_TextChanged(object sender, EventArgs e)
        {           
            if (txtEmpNIC.Text.Trim().Length==10)
            {
                if (!(IsDigitsOnly(txtEmpNIC.Text.Trim().Substring(0, 9))))
                {
                    DisplayMessage("NIC is not valid", 2);
                    return;
                }
            }
            if (txtEmpNIC.Text.Trim().Length == 12)
            {
                if(!(IsDigitsOnly(txtEmpNIC.Text.Trim())))
                {
                    DisplayMessage("NIC is not valid", 2);
                    return;
                }
            }
            if (txtEmpNIC.Text.Trim().Length!=10&&txtEmpNIC.Text.Trim().Length!=12)
            {
                DisplayMessage("Invalid NIC please check",2);
                return;
            }
            if (!validateinputString(txtEmpNIC.Text))
            {
                DisplayMessage("Invalid charactor found in NIC.", 2);
                txtEmpNIC.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(txtEmpNIC.Text.Trim().ToUpper()))
            {
                if (txtEmpNIC.Text.Trim().Length == 10)
                {
                    string dob = GetDOBbyNIC(txtEmpNIC.Text.Trim().ToUpper());
                    if (!string.IsNullOrEmpty(dob))
                    {
                        txtEmpDOB.Text = dob;
                    }
                }
                if (txtEmpNIC.Text.Trim().Length == 12)
                {
                    string dob = GetDOBNICNew(txtEmpNIC.Text.Trim().ToUpper());
                    if (!string.IsNullOrEmpty(dob))
                    {
                        txtEmpDOB.Text = dob;
                    }
                }
                string epf = GetEPFbyNIC(txtEmpNIC.Text.Trim().ToUpper());
                if (!string.IsNullOrEmpty(epf))
                {
                    txtEPFNo.Text = epf;
                    txtEmpNIC.ReadOnly = true;
                    Load_employees();
                }
            }

        }
        private string GetDOBNICNew(string nicarray)
        {
            ///assign Title and Sex

                string thirdNum = (nicarray[4]).ToString();
                if (thirdNum == "5" || thirdNum == "6" || thirdNum == "7" || thirdNum == "8" || thirdNum == "9")
                {
                    ddlSex.SelectedValue = "F";
                    ddlTitle.SelectedValue = "Ms.";
                }
                else
                {
                    ddlSex.SelectedValue = "M";
                    ddlTitle.SelectedValue = "Mr.";
                }

                //---------DOB generation----------------------
                string threechar = (nicarray[4]).ToString() + (nicarray[5]).ToString() + (nicarray[6]).ToString();
                Int32 DPBnum = Convert.ToInt32(threechar);
                if (DPBnum > 500)
                {
                    DPBnum = DPBnum - 500;
                }

                // Int32 JAN = 1, FEF = 2, MAR = 3, APR = 4, MAY = 5, JUN = 6, JUL = 7, AUG = 8, SEP = 9, OCT = 10, NOV = 11, DEC = 12;

                Dictionary<string, Int32> monthDict = new Dictionary<string, int>();
                monthDict.Add("JAN", 31);
                monthDict.Add("FEF", 29);
                monthDict.Add("MAR", 31);
                monthDict.Add("APR", 30);
                monthDict.Add("MAY", 31);
                monthDict.Add("JUN", 30);
                monthDict.Add("JUL", 31);
                monthDict.Add("AUG", 31);
                monthDict.Add("SEP", 30);
                monthDict.Add("OCT", 31);
                monthDict.Add("NOV", 30);
                monthDict.Add("DEC", 31);

                string bornMonth = string.Empty;
                Int32 bornDate = 0;

                Int32 leftval = DPBnum;
                foreach (var itm in monthDict)
                {
                    bornDate = leftval;

                    if (leftval <= itm.Value)
                    {
                        bornMonth = itm.Key;

                        break;
                    }

                    leftval = leftval - itm.Value;
                }

                //-------------------------------
                // string bornMonth1 = bornMonth;
                // Int32 bornDate1 = bornDate;

                Dictionary<string, Int32> monthDict2 = new Dictionary<string, int>();
                monthDict2.Add("JAN", 1);
                monthDict2.Add("FEF", 2);
                monthDict2.Add("MAR", 3);
                monthDict2.Add("APR", 4);
                monthDict2.Add("MAY", 5);
                monthDict2.Add("JUN", 6);
                monthDict2.Add("JUL", 7);
                monthDict2.Add("AUG", 8);
                monthDict2.Add("SEP", 9);
                monthDict2.Add("OCT", 10);
                monthDict2.Add("NOV", 11);
                monthDict2.Add("DEC", 12);
                Int32 dobMon = 0;
                foreach (var itm in monthDict2)
                {
                    if (itm.Key == bornMonth)
                    {
                        dobMon = itm.Value;
                    }
                }
                Int32 dobYear =Convert.ToInt32 ((nicarray[0]).ToString() + (nicarray[1]).ToString() + (nicarray[2]).ToString() + (nicarray[3]).ToString());

                return  + bornDate + "/"+bornMonth+"/"+dobYear;
        }

        private string GetDOBbyNIC(string nic)
        {
            try
            {
                String nic_ = nic.Trim().ToUpper();
                char[] nicarray = nic_.ToCharArray();

                ///assign Title and Sex

                string thirdNum = (nicarray[2]).ToString();
                if (thirdNum == "5" || thirdNum == "6" || thirdNum == "7" || thirdNum == "8" || thirdNum == "9")
                {
                    ddlSex.SelectedValue = "F";
                    ddlTitle.SelectedValue = "Ms.";
                }
                else
                {
                    ddlSex.SelectedValue = "M";
                    ddlTitle.SelectedValue = "Mr.";
                }

                //---------DOB generation----------------------
                string threechar = (nicarray[2]).ToString() + (nicarray[3]).ToString() + (nicarray[4]).ToString();
                Int32 DPBnum = Convert.ToInt32(threechar);
                if (DPBnum > 500)
                {
                    DPBnum = DPBnum - 500;
                }

                // Int32 JAN = 1, FEF = 2, MAR = 3, APR = 4, MAY = 5, JUN = 6, JUL = 7, AUG = 8, SEP = 9, OCT = 10, NOV = 11, DEC = 12;

                Dictionary<string, Int32> monthDict = new Dictionary<string, int>();
                monthDict.Add("JAN", 31);
                monthDict.Add("FEF", 29);
                monthDict.Add("MAR", 31);
                monthDict.Add("APR", 30);
                monthDict.Add("MAY", 31);
                monthDict.Add("JUN", 30);
                monthDict.Add("JUL", 31);
                monthDict.Add("AUG", 31);
                monthDict.Add("SEP", 30);
                monthDict.Add("OCT", 31);
                monthDict.Add("NOV", 30);
                monthDict.Add("DEC", 31);

                string bornMonth = string.Empty;
                Int32 bornDate = 0;

                Int32 leftval = DPBnum;
                foreach (var itm in monthDict)
                {
                    bornDate = leftval;

                    if (leftval <= itm.Value)
                    {
                        bornMonth = itm.Key;

                        break;
                    }

                    leftval = leftval - itm.Value;
                }

                //-------------------------------
                // string bornMonth1 = bornMonth;
                // Int32 bornDate1 = bornDate;

                Dictionary<string, Int32> monthDict2 = new Dictionary<string, int>();
                monthDict2.Add("JAN", 1);
                monthDict2.Add("FEF", 2);
                monthDict2.Add("MAR", 3);
                monthDict2.Add("APR", 4);
                monthDict2.Add("MAY", 5);
                monthDict2.Add("JUN", 6);
                monthDict2.Add("JUL", 7);
                monthDict2.Add("AUG", 8);
                monthDict2.Add("SEP", 9);
                monthDict2.Add("OCT", 10);
                monthDict2.Add("NOV", 11);
                monthDict2.Add("DEC", 12);
                Int32 dobMon = 0;
                foreach (var itm in monthDict2)
                {
                    if (itm.Key == bornMonth)
                    {
                        dobMon = itm.Value;
                    }
                }
                Int32 dobYear = 1900 + Convert.ToInt32((nicarray[0].ToString())) * 10 + Convert.ToInt32((nicarray[1].ToString()));
                try
                {
                    DateTime dob = new DateTime(dobYear, dobMon, bornDate);
                    nic = dob.Date.ToShortDateString();
                    //dob.ToString("dd/MM/yyyy");
                    return nic;
                }
                catch (Exception err)
                {
                    DisplayMessage("Please enter a valid NIC no!", 2);
                    txtEmpNIC.Text = "";
                    txtEmpNIC.Focus();
                }
            }
            catch (Exception err)
            {
                DisplayMessage("Please enter a valid NIC no!", 2);
                txtEmpNIC.Text = "";
                txtEmpNIC.Focus();
            }
            return null;
        }

        private string GetEPFbyNIC(string nic)
        {
            string epfNo = "";

            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeEPF);
                DataTable _result = CHNLSVC.General.Get_EPFNo(SearchParams, "NIC", "%" + nic.Trim().ToUpper().ToString());

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("NIC") == nic.Trim().ToUpper().ToString()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    return null;
                }

                else
                {
                    epfNo = _validate[0]["CODE"].ToString();
                    return epfNo;
                }

            }

            catch (Exception)
            {
                DisplayMessage("Please select a valid NIC number!", 2);
            }

            return null;

        }

        //Get Manager
        protected void lbtnManager_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            //"462:|"
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Manager);
            DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "462";
            BindUCtrlDDLData(_result);
            txtSearchbyword.Text = "";
            UserPopoup.Show();
        }


        protected void txtManager_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtManager.Text))
            {
                DisplayMessage("Invalid charactor found in manager code.", 2);
                txtManager.Focus();
                return;
            }
            string[] manager = { };
            manager = GetManger(txtManager.Text);

            if (manager == null || manager.Length == 0)
            {
                DisplayMessage("Please enter a valid manager code!", 2);
                txtManager.Text = String.Empty;
                txtManager.ToolTip = "";
                txtManager.Focus();
            }
            else
            {
                txtManager.Text = manager[0];
                txtManager.ToolTip = manager[1];
                txtManager.Focus();
            }
        }

        //Get category 
        protected void lbtnCategory_Click(object sender, EventArgs e)
        {

            txtSearchbyword.Text = "";
            //"460:|"
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Category);
            DataTable _result = CHNLSVC.General.Get_Category(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "460";
            BindUCtrlDDLData(_result);
            txtSearchbyword.Text = "";
            UserPopoup.Show();
        }


        protected void txtCategory_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtCategory.Text))
            {
                DisplayMessage("Invalid charactor found in designation code.", 2);
                txtCategory.Focus();
                return;
            }
            string[] category = { };
            category = GetCategory(txtCategory.Text);

            if (category == null || category.Length == 0)
            {
                DisplayMessage("Please enter a valid designation code!", 2);
                txtCategory.Text = String.Empty;
                txtCategory.ToolTip = "";
                txtCategory.Focus();
            }
            else
            {
                txtCategory.Text = category[0];
                txtCategory.ToolTip = category[1];
                txtCategory.Focus();
            }

        }

        //Get subcategory 
        protected void lbtnSubCat_Click(object sender, EventArgs e)
        {

            //"460:|"
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Subacategory);
            DataTable _result = CHNLSVC.General.Get_SubCategory(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "461";
            BindUCtrlDDLData(_result);
            txtSearchbyword.Text = "";
            UserPopoup.Show();
        }


        protected void txtSubCat_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtSubCat.Text))
            {
                DisplayMessage("Invalid charactor found in sub category code.", 2);
                txtSubCat.Focus();
                return;
            }
            string[] subcategory = { };
            subcategory = GetsubCategory(txtCategory.Text);

            if (subcategory == null || subcategory.Length == 0)
            {
                DisplayMessage("Please enter a valid sub category code!", 2);
                txtSubCat.Text = String.Empty;
                txtSubCat.ToolTip = "";
                txtSubCat.Focus();
            }
            else
            {
                txtSubCat.Text = subcategory[0];
                txtSubCat.ToolTip = subcategory[1];
                txtSubCat.Focus();
            }
        }

        //Get departments 
        protected void lbtnDepProfit_Click(object sender, EventArgs e)
        {

            //"184:|"
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Department);
            DataTable _result = CHNLSVC.CommonSearch.Get_Departments(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "184";
            BindUCtrlDDLData(_result);
            txtSearchbyword.Text = "";
            UserPopoup.Show();
        }


        protected void txtDepProfit_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtDepProfit.Text))
            {
                DisplayMessage("Invalid charactor found in department code.", 2);
                txtDepProfit.Focus();
                return;
            }
            string[] department = { };
            department = GetDepartment(txtDepProfit.Text);

            if (department == null || department.Length == 0)
            {
                DisplayMessage("Please enter a valid department code!", 2);
                txtDepProfit.Text = String.Empty;
                txtDepProfit.ToolTip = "";
                txtDepProfit.Focus();
            }
            else
            {
                txtDepProfit.Text = department[0];
                txtDepProfit.ToolTip = department[1];
                txtDepProfit.Focus();
            }
        }

        //Get Supervisor
        protected void lbtnSupvsr_Click(object sender, EventArgs e)
        {

            //"463:|"
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supervisor);
            DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "463";
            BindUCtrlDDLData(_result);
            txtSearchbyword.Text = "";
            UserPopoup.Show();
        }

        private string[] GetManger(string managerCode)
        {
            try
            {
                List<string> string_List = new List<string>();

                if (managerCode != null)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Manager);
                    DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, "Code", "%" + managerCode.Trim().ToUpper().ToString());

                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == managerCode.Trim().ToUpper().ToString()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        return null;
                    }

                    else
                    {
                        string_List.Add(_validate[0]["Code"].ToString());
                        string_List.Add(_validate[0]["Description"].ToString());

                    }

                    return string_List.ToArray();
                }

            }

            catch (Exception)
            {
                DisplayMessage("Error Occurred while processing", 2);
            }

            return null;
        }

        private string[] GetProfitCenter_Location(string PCorLCode)
        {
            try
            {
                List<string> string_List = new List<string>();

                if (PCorLCode != null)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(SearchParams, "Code", "%" + PCorLCode.Trim().ToUpper().ToString());

                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == PCorLCode.Trim().ToUpper().ToString()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        return null;
                    }
                    else
                    {
                        string_List.Add(_validate[0]["Code"].ToString());
                        string_List.Add(_validate[0]["Description"].ToString());
                    }

                    return string_List.ToArray();
                }
            }

            catch (Exception)
            {
                DisplayMessage("Error Occurred while processing", 2);
            }

            return null;
        }

        private string[] GetSupervisor(string supvisorCode)
        {
            try
            {
                List<string> string_List = new List<string>();

                if (supvisorCode != null)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supervisor);
                    DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, "Code", "%" + supvisorCode.Trim().ToUpper().ToString());

                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == supvisorCode.Trim().ToUpper().ToString()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        return null;
                    }
                    else
                    {
                        string_List.Add(_validate[0]["Code"].ToString());
                        string_List.Add(_validate[0]["Description"].ToString());
                    }

                    return string_List.ToArray();
                }
            }

            catch (Exception)
            {
                DisplayMessage("Error Occurred while processing", 2);
            }

            return null;
        }

        private string[] GetCategory(string catCode)
        {
            try
            {
                List<string> string_List = new List<string>();

                if (catCode != null)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Category);
                    DataTable _result = CHNLSVC.General.Get_Category(SearchParams, "Code", "%" + catCode.Trim().ToUpper().ToString());

                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == catCode.Trim().ToUpper().ToString()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        return null;
                    }
                    else
                    {
                        string_List.Add(_validate[0]["Code"].ToString());
                        string_List.Add(_validate[0]["Description"].ToString());
                    }

                    return string_List.ToArray();
                }
            }

            catch (Exception)
            {
                DisplayMessage("Error Occurred while processing", 2);
            }

            return null;
        }

        private string[] GetsubCategory(string subcatCode)
        {
            try
            {
                List<string> string_List = new List<string>();

                if (subcatCode != null)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Subacategory);
                    DataTable _result = CHNLSVC.General.Get_SubCategory(SearchParams, "Code", "%" + subcatCode.Trim().ToUpper().ToString());

                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == subcatCode.Trim().ToUpper().ToString()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        return null;
                    }
                    else
                    {
                        string_List.Add(_validate[0]["Code"].ToString());
                        string_List.Add(_validate[0]["Description"].ToString());
                    }

                    return string_List.ToArray();
                }
            }

            catch (Exception)
            {
                DisplayMessage("Error Occurred while processing", 2);
            }

            return null;
        }

        private string[] GetDepartment(string depCode)
        {
            try
            {
                List<string> string_List = new List<string>();

                if (depCode != null)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Department);
                    DataTable _result = CHNLSVC.CommonSearch.Get_Departments(SearchParams, "Code", "%" + depCode.Trim().ToUpper().ToString());

                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == depCode.Trim().ToUpper().ToString()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        return null;
                    }
                    else
                    {
                        string_List.Add(_validate[0]["Code"].ToString());
                        string_List.Add(_validate[0]["Description"].ToString());
                    }

                    return string_List.ToArray();
                }
            }

            catch (Exception)
            {
                DisplayMessage("Error Occurred while processing", 2);
            }

            return null;
        }

        protected void txtSupvsr_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtSupvsr.Text))
            {
                DisplayMessage("Invalid charactor found in supervisor code.", 2);
                txtSupvsr.Focus();
                return;
            }
            string[] supervisor = { };
            supervisor = GetSupervisor(txtSupvsr.Text);

            if (supervisor == null || supervisor.Length == 0)
            {
                DisplayMessage("Please enter a valid supervisor code!", 2);
                txtSupvsr.Text = String.Empty;
                txtSupvsr.ToolTip = "";
                txtSupvsr.Focus();
            }
            else
            {
                txtSupvsr.Text = supervisor[0];
                txtSupvsr.ToolTip = supervisor[1];
                txtSupvsr.Focus();
            }
        }

        protected void txtEmpEMail_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmpEMail.Text))
            {
                Boolean _isValid = IsValidEmail(txtEmpEMail.Text.Trim());

                if (_isValid == false)
                {
                    DisplayMessage("Please enter a valid email address!", 2);
                    txtEmpEMail.Text = "";
                    txtEmpEMail.Focus();
                    return;
                }
            }
        }

        protected void txtEmpMobile_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtEmpMobile.Text))
            {
                DisplayMessage("Invalid charactor found in mobile no.", 2);
                txtEmpMobile.Focus();
                return;
            }
            if (!IsValidMobileOrLandNo(txtEmpMobile.Text))
            {
                DisplayMessage("Please select the valid mobile no!", 2);
                txtEmpMobile.Text = "";
                txtEmpMobile.Focus();
                return;
            }
        }

        protected void txtEmpHomePhone_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtEmpHomePhone.Text))
            {
                DisplayMessage("Invalid charactor found in home phone no.", 2);
                txtEmpHomePhone.Focus();
                return;
            }
            if (!IsValidMobileOrLandNo(txtEmpHomePhone.Text))
            {
                DisplayMessage("Please select the valid home telephone no!", 2);
                txtEmpHomePhone.Text = "";
                txtEmpHomePhone.Focus();
                return;
            }
        }

        protected void txtEmpOfficePhone_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtEmpOfficePhone.Text))
            {
                DisplayMessage("Invalid charactor found in office phone no.", 2);
                txtEmpOfficePhone.Focus();
                return;
            }
            if (!IsValidMobileOrLandNo(txtEmpOfficePhone.Text))
            {
                DisplayMessage("Please select the valid office telephone no!", 2);
                txtEmpOfficePhone.Text = "";
                txtEmpOfficePhone.Focus();
                return;
            }
        }

        protected void chkSVChange_CheckChange(object sender, EventArgs e)
        {
            checkStockValue();
        }

        private void checkStockValue()
        {
            if (chkSVChange.Checked == true)
            {
                txtMaxSVal.Enabled = true;
            }
            else
            {
                txtMaxSVal.Enabled = false;
            }
        }

        protected void txtMaxSVal_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaxSVal.Text))
            {
                txtMaxSVal.Text = checkPositiveDecimals(txtMaxSVal.Text.Trim());
            }
        }



        #endregion


        #region Employee Profit Center

        //   Assign Profit center          

        protected void lbtnPCPrftCntr_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchbyword.Text = "";
                //"75:|"
                dvResultUser.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                Label1.Text = "75";
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing", 2);
            }
        }

        protected void lbtnPCManager_Click(object sender, EventArgs e)
        {
            WarningEmployee.Visible = false;
            SuccessEmployee.Visible = false;
            try
            {
                txtSearchbyword.Text = "";
                //"75:|"
                dvResultUser.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PCManager);
                DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                Label1.Text = "464";
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                WarningEmployee.Visible = true;
                lblWEmployee.Text = ex.Message;
            }
        }

        protected void lbtnAddPrftCntr_Click(object sender, EventArgs e)
        {
            try
            {
                _MPcE = ViewState["_MPcE"] as List<MasterProfitCenter_LocationEmployee>;

                if (string.IsNullOrEmpty(txtPCPrftCntr.Text))
                {
                    DisplayMessage("Please enter profit center!", 2);
                    return;
                }
                /*
                if (string.IsNullOrEmpty(txtPCAssDate.Text))
                {
                    DisplayMessage("Select Assigned Date", 2);
                    return;
                }*/
                if (string.IsNullOrEmpty(txtPCRptCode.Text))
                {
                    DisplayMessage("Please enter representative code!", 2);
                    return;
                }
                //if (string.IsNullOrEmpty(txtPCMaxSVal.Text))
                //{
                //    DisplayMessage("Please enter Max Stock Value!", 2);
                //    return;
                //}
                if (string.IsNullOrEmpty(txtPCManager.Text))
                {
                    DisplayMessage("Please enter manager!", 2);
                    return;
                }

                if (_MPcE != null)
                {
                    MasterProfitCenter_LocationEmployee result = _MPcE.Find(x => x._mpce_pc == txtPCPrftCntr.Text);
                    if (result != null)
                    {
                        DisplayMessage("This Profit Center already exist", 2);
                        return;
                    }
                    /*
                    foreach (GridViewRow pcgrv in grdemployeemaster_profitmaster.Rows)
                    {
                        string _profitcenter = (pcgrv.FindControl("lbl_PrftCntr") as Label).Text;
                        CheckBox pcpermission = (CheckBox)pcgrv.FindControl("chk_PCPermission");
                        CheckBox pcstatus = (CheckBox)pcgrv.FindControl("chk_PCStatus");

                        if (_profitcenter == txtPCPrftCntr.Text)
                        {
                            WarningEmployee.Visible = false;
                            WarningEmployee.Visible = true;
                            lblWEmployee.Text = "inserted profit center is alredy added";

                            return;
                        }

                        else
                        {
                            MasterProfitCenter_LocationEmployee _pcEmp = new MasterProfitCenter_LocationEmployee();
                            _pcEmp._mpce_epf = txtEPFNo.Text.ToUpper();
                            _pcEmp._mpce_com = Session["UserCompanyCode"].ToString();
                            _pcEmp._mpce_pc = txtPCPrftCntr.Text;
                            _pcEmp._mpce_assn_dt = Convert.ToDateTime(txtPCAssDate.Text);
                            _pcEmp._mpce_rep_cd = txtPCRptCode.Text;
                            _pcEmp._mpce_anal_1 = "P";
                            _pcEmp._mpce_act = (pcstatus.Checked == true) ? 1 : 0;
                            _pcEmp._mpce_max_stk_val = Convert.ToInt32(txtPCMaxSVal.Text);
                            _pcEmp._mpce_mgr = txtPCManager.Text;
                            _pcEmp._mpce_is_rest = (pcpermission.Checked == true) ? 1 : 0;
                            _MPcE.Add(_pcEmp);

                        }
                    }

                    */
                }

                else
                {
                    _MPcE = new List<MasterProfitCenter_LocationEmployee>();

                }


                MasterProfitCenter_LocationEmployee _pcEmp = new MasterProfitCenter_LocationEmployee();
                _pcEmp._mpce_epf = txtEPFNo.Text.ToUpper();
                _pcEmp._mpce_com = Session["UserCompanyCode"].ToString();
                _pcEmp._mpce_pc = txtPCPrftCntr.Text;
                _pcEmp._mpce_assn_dt = Convert.ToDateTime(txtPCAssDate.Text);
                _pcEmp._mpce_rep_cd = txtPCRptCode.Text;
                _pcEmp._mpce_anal_1 = "P";
                // _pcEmp._mpce_anal_2 = ;
                // _pcEmp._mpce_anal_2 = ;            
                // _pcEmp._mpce_act = chkPCStatus.Checked == true ? 1 : 0;
                _pcEmp._mpce_act = (chkPCStatus.Checked) == true ? 1 : 0;
                _pcEmp._mpce_max_stk_val = Convert.ToDecimal(txtPCMaxSVal.Text);
                _pcEmp._mpce_mgr = txtPCManager.Text;
                _pcEmp._mpce_is_rest = chkPCPermission.Checked == true ? 1 : 0;

                _pcEmp.companyname = GetCompanyDesc(_pcEmp._mpce_com);
                _pcEmp.profitcenterorlocation = txtPCPrftCntr.ToolTip;
                _pcEmp.manager = txtPCManager.ToolTip;

                _MPcE.Add(_pcEmp);



                grdemployeemaster_profitmaster.DataSource = null;
                grdemployeemaster_profitmaster.DataSource = new List<MasterProfitCenter_LocationEmployee>();
                grdemployeemaster_profitmaster.DataSource = _MPcE;

                grdemployeemaster_profitmaster.DataBind();

                ViewState["_MPcE"] = _MPcE;
                cleargrids("PC");
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 2);
            }

            finally
            {
                CHNLSVC.CloseAllChannels();
            }


        }

        protected void txtPCPrftCntr_TextChanged(object sender, EventArgs e)
        {
            string[] pcprftCenter = { };
            pcprftCenter = GetProfitCenter_Location(txtPCPrftCntr.Text);

            if (pcprftCenter == null || pcprftCenter.Length == 0)
            {
                DisplayMessage("Please enter a valid profit center/location code!", 2);
                txtPCPrftCntr.Text = String.Empty;
                txtPCPrftCntr.ToolTip = "";
                txtPCPrftCntr.Focus();
            }
            else
            {
                txtPCPrftCntr.Text = pcprftCenter[0];
                txtPCPrftCntr.ToolTip = pcprftCenter[1];
                txtPCPrftCntr.Focus();
            }
        }

        protected void txtPCMaxSVal_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPCMaxSVal.Text))
            {
                txtPCMaxSVal.Text = checkPositiveDecimals(txtPCMaxSVal.Text.Trim());
            }
        }

        protected void txtPCManager_TextChanged(object sender, EventArgs e)
        {
            string[] pcmanager = { };
            pcmanager = GetManger(txtPCManager.Text);

            if (pcmanager == null || pcmanager.Length == 0)
            {
                DisplayMessage("Please enter a valid manager code!", 2);
                txtPCManager.Text = String.Empty;
                txtPCManager.Text = "";
                txtPCManager.Focus();
            }
            else
            {
                txtPCManager.Text = pcmanager[0];
                txtPCManager.ToolTip = pcmanager[1];
                txtPCManager.Focus();
            }

        }



        #endregion


        #region Employee Location

        //  Assign Location         

        protected void lbtnLLocation_Click(object sender, EventArgs e)
        {
            WarningEmployee.Visible = false;
            SuccessEmployee.Visible = false;
            try
            {
                txtSearchbyword.Text = "";
                //"75:|"
                dvResultUser.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                Label1.Text = "41";
                BindUCtrlDDLData(_result);
                UserPopoup.Show();

            }

            catch (Exception ex)
            {
                WarningEmployee.Visible = true;
                lblWEmployee.Text = ex.Message;
            }
        }


        protected void lbtnLManager_Click(object sender, EventArgs e)
        {
            WarningEmployee.Visible = false;
            SuccessEmployee.Visible = false;
            try
            {
                txtSearchbyword.Text = "";
                //"75:|"
                dvResultUser.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.LManager);
                DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                Label1.Text = "465";
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                WarningEmployee.Visible = true;
                lblWEmployee.Text = ex.Message;
            }
        }


        protected void txtLManager_TextChanged(object sender, EventArgs e)
        {
            string[] locmanager = { };
            locmanager = GetManger(txtLManager.Text);

            if (locmanager == null || locmanager.Length == 0)
            {
                DisplayMessage("Please enter a valid manager code!", 2);
                txtLManager.Text = String.Empty;
                txtLManager.ToolTip = "";
                txtLManager.Focus();
            }
            else
            {
                txtLManager.Text = locmanager[0];
                txtLManager.ToolTip = locmanager[1];
                txtLManager.Focus();
            }
        }


        protected void lbtnAddLocation_Click(object sender, EventArgs e)
        {
            _MLE = ViewState["_MLE"] as List<MasterProfitCenter_LocationEmployee>;

            if (string.IsNullOrEmpty(txtLLocation.Text))
            {
                DisplayMessage("Please select location!", 2);
                return;
            }
            /*
            if (string.IsNullOrEmpty(txtPCAssDate.Text))
            {
                DisplayMessage("Select Assigned Date", 2);
                return;
            }*/
            if (string.IsNullOrEmpty(txtLRptCde.Text))
            {
                DisplayMessage("Please enter a valid representative code!", 2);
                return;
            }
            //if (string.IsNullOrEmpty(txtLMaxSVal.Text))
            //{
            //    DisplayMessage("Enter Max Stock Value", 2);
            //    return;
            //}
            if (string.IsNullOrEmpty(txtLManager.Text))
            {
                DisplayMessage("Select the Manger", 2);
                return;
            }

            if (_MLE != null)
            {
                MasterProfitCenter_LocationEmployee result = _MLE.Find(x => x._mpce_pc == txtLLocation.Text);
                if (result != null)
                {
                    DisplayMessage("This Location already exist", 2);
                    return;
                }

                /*
                foreach (GridViewRow lgrv in grdemployeemaster_location.Rows)
                {
                    string _location = (lgrv.FindControl("lbl_LLocation") as Label).Text;
                    CheckBox lpermission = (CheckBox)lgrv.FindControl("chk_LPermission");
                    CheckBox lstatus = (CheckBox)lgrv.FindControl("chk_LStatus");

                    if (_location == txtLLocation.Text)
                    {
                        WarningEmployee.Visible = false;
                        WarningEmployee.Visible = true;
                        lblWEmployee.Text = "inserted location is alredy added";
                        return;
                    }

                    else
                    {
                        MasterProfitCenter_LocationEmployee _lEmp = new MasterProfitCenter_LocationEmployee();
                        _lEmp._mpce_epf = txtEPFNo.Text.ToUpper();
                        _lEmp._mpce_com = Session["UserCompanyCode"].ToString();
                        _lEmp._mpce_pc = txtLLocation.Text;
                        _lEmp._mpce_assn_dt = Convert.ToDateTime(txtLAssDate.Text);
                        _lEmp._mpce_rep_cd = txtLRptCde.Text;
                        _lEmp._mpce_anal_1 = "L";
                        _lEmp._mpce_act = (lstatus.Checked == true) ? 1 : 0;
                        _lEmp._mpce_max_stk_val = Convert.ToInt32(txtLMaxSVal.Text);
                        _lEmp._mpce_mgr = txtLManager.Text;
                        _lEmp._mpce_is_rest = (lpermission.Checked == true) ? 1 : 0;
                        _MLE.Add(_lEmp);

                    }
                }
                */
            }

            else
            {
                _MLE = new List<MasterProfitCenter_LocationEmployee>();
            }
            /**/

            MasterProfitCenter_LocationEmployee _lEmp = new MasterProfitCenter_LocationEmployee();
            _lEmp._mpce_epf = txtEPFNo.Text.ToUpper();
            _lEmp._mpce_com = Session["UserCompanyCode"].ToString();
            _lEmp._mpce_pc = txtLLocation.Text;
            _lEmp._mpce_assn_dt = Convert.ToDateTime(txtLAssDate.Text);
            _lEmp._mpce_rep_cd = txtLRptCde.Text;
            _lEmp._mpce_anal_1 = "L";
            _lEmp._mpce_act = (chkLStatus.Checked == true) ? 1 : 0;
            _lEmp._mpce_max_stk_val = Convert.ToDecimal(txtLMaxSVal.Text);
            _lEmp._mpce_mgr = txtLManager.Text;
            _lEmp._mpce_is_rest = (chkLPermission.Checked == true) ? 1 : 0;

            _lEmp.companyname = GetCompanyDesc(_lEmp._mpce_com);
            _lEmp.profitcenterorlocation = txtLLocation.ToolTip;
            _lEmp.manager = txtLManager.ToolTip;

            _MLE.Add(_lEmp);

            grdemployeemaster_location.DataSource = null;
            grdemployeemaster_location.DataSource = new List<MasterProfitCenter_LocationEmployee>();
            grdemployeemaster_location.DataSource = _MLE;
            grdemployeemaster_location.DataBind();

            ViewState["_MLE"] = _MLE;

            cleargrids("LOC");
        }


        protected void txtLLocation_TextChanged(object sender, EventArgs e)
        {
            string[] pclocation = { };
            pclocation = GetProfitCenter_Location(txtLLocation.Text);

            if (pclocation == null || pclocation.Length == 0)
            {
                DisplayMessage("Please enter a valid profit center/location code!", 2);
                txtLLocation.Text = String.Empty;
                txtLLocation.ToolTip = "";
                txtLLocation.Focus();
            }
            else
            {
                txtLLocation.Text = pclocation[0];
                txtLLocation.ToolTip = pclocation[1];
                txtLLocation.Focus();
            }
        }


        protected void txtLMaxSVal_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLMaxSVal.Text))
            {
                txtLMaxSVal.Text = checkPositiveDecimals(txtLMaxSVal.Text.Trim());
            }
        }


        #endregion


        #region Customer Assign

        //  Assign Customer Data          

        protected void lbtnCusCde_Click(object sender, EventArgs e)
        {
            try
            {

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams2 = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result2 = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams2, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                result2.Columns.Remove("Code1");
                dvResultUser.DataSource = result2;
                dvResultUser.DataBind();
                Label1.Text = "13";
                BindUCtrlDDLData(result2);
                ViewState["SEARCH"] = result2;
                UserPopoup.Show(); Session["UserPopoup"] = "UserPopoup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }

            catch (Exception)
            {
                DisplayMessage("Error Occurred while processing", 2);
            }
        }

        protected void lbtnAddCusAss_Click(object sender, EventArgs e)
        {
            _MstCusEmp = ViewState["_MstCusEmp"] as List<MasterCustomerEmployee>;

            if (string.IsNullOrEmpty(txtCusCde.Text))
            {
                DisplayMessage("Please select a customer!", 2);
                return;
            }

            if (_MstCusEmp != null)
            {
                MasterCustomerEmployee result = _MstCusEmp.Find(x => x._mpce_cus_cd == txtCusCde.Text);
                if (result != null)
                {
                    DisplayMessage("This Customer already exist!", 2);
                    return;
                }
                /*
                foreach (GridViewRow cgrv in grdcustomer_employee.Rows)
                {
                    string _customer = (cgrv.FindControl("lbl_CusCde") as Label).Text;
                    CheckBox cusstatus = (CheckBox)cgrv.FindControl("chk_CusStatus");

                    if (_customer == txtCusCde.Text)
                    {
                        WarningEmployee.Visible = false;
                        WarningEmployee.Visible = true;
                        lblWEmployee.Text = "inserted customer is alredy added";

                        return;
                    }

                    else
                    {
                        MasterCustomerEmployee _cEmp = new MasterCustomerEmployee();
                        _cEmp._mpce_com = Session["UserCompanyCode"].ToString();
                        _cEmp._mpce_emp_cd = txtEPFNo.Text.ToUpper();
                        _cEmp._mpce_cus_cd = txtCusCde.Text;
                        _cEmp._mpce_stus = (cusstatus.Checked == true) ? 1 : 0;
                        _cEmp._mpce_cre_by = Session["UserID"].ToString();
                        _cEmp._mpce_mod_by = Session["UserID"].ToString();
                        _MstCusEmp.Add(_cEmp);
                    }
                }
                */
            }
            else
            {
                _MstCusEmp = new List<MasterCustomerEmployee>();

            }

            MasterCustomerEmployee _cEmp = new MasterCustomerEmployee();
            _cEmp._mpce_com = Session["UserCompanyCode"].ToString();
            _cEmp._mpce_emp_cd = txtEPFNo.Text.ToUpper();
            _cEmp._mpce_cus_cd = txtCusCde.Text;
            _cEmp.Customer = txtCusName.Text;
            _cEmp._mpce_stus = (chkCusAssStatus.Checked == true) ? 1 : 0;
            _cEmp._mpce_cre_by = Session["UserID"].ToString();
            _cEmp._mpce_mod_by = Session["UserID"].ToString();
            _MstCusEmp.Add(_cEmp);


            grdcustomer_employee.DataSource = null;
            grdcustomer_employee.DataSource = new List<MasterCustomerEmployee>();
            grdcustomer_employee.DataSource = _MstCusEmp;

            grdcustomer_employee.DataBind();

            ViewState["_MstCusEmp"] = _MstCusEmp;

            cleargrids("CUS");
        }

        protected void txtCusCde_TextChanged(object sender, EventArgs e)
        {
            txtCusCde.Text = GetCustomer(txtCusCde.Text);
            txtCusCde.Focus();
        }

        #endregion

        private string checkPositiveDecimals(string amount)
        {
            try
            {
                if (Convert.ToDecimal(amount) < 0)
                {
                    DisplayMessage("Max stock value cannot be a negative value.", 2);
                    return amount = "0.00";
                }
            }
            catch (Exception)
            {
                DisplayMessage("Error Occurred while processing", 2);
            }

            double value = double.Parse(amount);
            string result = value.ToString("f2");

            return result;

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

        private void DisplayMessage(String _err, Int32 option)
        {
            string Msg = _err.Replace("\"", " ").Replace("'", " ").Replace("\n", " ").Replace(@"\", " ");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msg + "');", true);
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.EmployeeEPF:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EmployeeCode:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Category:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Subacategory:
                    {
                        paramsText.Append(txtCategory.Text + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Manager:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Department:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Supervisor:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PCManager:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.LManager:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.ApprovePermCode:
                //    {
                //        paramsText.Append("" + seperator);
                //        break;
                //    }

                default:
                    break;
            }
            return paramsText.ToString();
        }

        protected void ImageSearch_Click(object sender, EventArgs e)
        {
            if (Label1.Text == "87")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeEPF);
                DataTable _result = CHNLSVC.General.Get_EPFNo(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "459")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeCode);
                DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "460")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Category);
                DataTable _result = CHNLSVC.General.Get_Category(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "461")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Subacategory);
                DataTable _result = CHNLSVC.General.Get_SubCategory(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "462")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Manager);
                DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "184")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Department);
                DataTable _result = CHNLSVC.CommonSearch.Get_Departments(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();

            }

            else if (Label1.Text == "463")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supervisor);
                DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }

            else if (Label1.Text == "75")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }

            else if (Label1.Text == "41")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }

            else if (Label1.Text == "13")
            {
                //dvResultUser.DataSource = null;
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                //DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                //dvResultUser.DataSource = _result;
                //dvResultUser.DataBind();
                //UserPopoup.Show();

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, cmbSearchbykey.SelectedItem.Text, txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                dvResultUser.DataSource = result;
                result.Columns.Remove("Code1");
                dvResultUser.DataBind();
                Label1.Text = "13";
                ViewState["SEARCH"] = result;
                UserPopoup.Show(); Session["UserPopoup"] = "UserPopoup";
                txtSearchbyword.Focus();
            }
        }

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {

            if (Label1.Text == "87")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeEPF);
                DataTable _result = CHNLSVC.General.Get_EPFNo(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "459")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeCode);
                DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "460")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Category);
                DataTable _result = CHNLSVC.General.Get_Category(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "461")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Subacategory);
                DataTable _result = CHNLSVC.General.Get_SubCategory(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "462")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Manager);
                DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
            else if (Label1.Text == "184")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Department);
                DataTable _result = CHNLSVC.CommonSearch.Get_Departments(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();

            }

            else if (Label1.Text == "463")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supervisor);
                DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }

            else if (Label1.Text == "75")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }

            else if (Label1.Text == "41")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }

            else if (Label1.Text == "13")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                dvResultUser.DataSource = _result;
                _result.Columns.Remove("Code1");
                dvResultUser.DataBind();
                UserPopoup.Show();
            }

            else if (Label1.Text == "464")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Manager);
                DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }

            else if (Label1.Text == "465")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Manager);
                DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                UserPopoup.Show();
            }
        }

        protected void dvResultUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string code = "";
            string name = dvResultUser.SelectedRow.Cells[1].Text;
            string value = dvResultUser.SelectedRow.Cells[2].Text;
            if (Label1.Text == "459")
            {
                code = dvResultUser.SelectedRow.Cells[3].Text;
            }

            // int value = searchvalue;
            if (Label1.Text == "87")
            {
                txtEPFNo.Text = name;
                Load_employees();
                UserPopoup.Hide();
            }
            else if (Label1.Text == "462")
            {
                txtManager.Text = name;
                UserPopoup.Hide();
            }
            else if (Label1.Text == "459")
            {

                txtEmpCode.Text = name;
                txtEPFNo.Text = code;
                Load_employees();
                UserPopoup.Hide();
            }
            else if (Label1.Text == "460")
            {
                txtCategory.Text = name;
                UserPopoup.Hide();
            }
            else if (Label1.Text == "184")
            {
                txtDepProfit.Text = name;
                UserPopoup.Hide();
            }
            else if (Label1.Text == "461")
            {
                txtSubCat.Text = name;
                UserPopoup.Hide();
            }
            else if (Label1.Text == "463")
            {
                txtSupvsr.Text = name;
                UserPopoup.Hide();
            }

            else if (Label1.Text == "75")
            {
                txtPCPrftCntr.Text = name;
                txtPCPrftCntr.ToolTip = value;
                UserPopoup.Hide();
            }

            else if (Label1.Text == "464")
            {
                txtPCManager.Text = name;
                txtPCManager.ToolTip = value;
                UserPopoup.Hide();
            }

            else if (Label1.Text == "41")
            {
                txtLLocation.Text = name;
                txtLLocation.ToolTip = value;
                UserPopoup.Hide();

            }

            else if (Label1.Text == "465")
            {
                txtLManager.Text = name;
                txtLManager.ToolTip = value;
                UserPopoup.Hide();

            }

            else if (Label1.Text == "13")
            {
                txtCusCde.Text = name;
                txtCusName.Text = value;
                UserPopoup.Hide();
            }

            UserPopoup.CancelControlID = "btnClose";
            UserPopoup.Hide();
            Label1.Text = "";
        }

        protected void dvResultUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvResultUser.PageIndex = e.NewPageIndex;

            if (Label1.Text == "87")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeEPF);
                DataTable _result = CHNLSVC.General.Get_EPFNo(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                dvResultUser.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            else if (Label1.Text == "459")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeCode);
                DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                dvResultUser.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            else if (Label1.Text == "460")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Category);
                DataTable _result = CHNLSVC.General.Get_Category(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                dvResultUser.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            else if (Label1.Text == "461")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Subacategory);
                DataTable _result = CHNLSVC.General.Get_SubCategory(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                dvResultUser.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            else if (Label1.Text == "462")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Manager);
                DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                dvResultUser.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            else if (Label1.Text == "184")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Department);
                DataTable _result = CHNLSVC.CommonSearch.Get_Departments(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                dvResultUser.PageIndex = 0;
                UserPopoup.Show();
                return;

            }

            else if (Label1.Text == "463")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supervisor);
                DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                dvResultUser.PageIndex = 0;
                UserPopoup.Show();
                return;
            }

            else if (Label1.Text == "75")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                dvResultUser.PageIndex = 0;
                UserPopoup.Show();
                return;
            }

            else if (Label1.Text == "41")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                dvResultUser.PageIndex = 0;
                UserPopoup.Show();
                return;
            }

            else if (Label1.Text == "13")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                dvResultUser.DataSource = _result;
                _result.Columns.Remove("Code1");
                dvResultUser.DataBind();
                dvResultUser.PageIndex = 0;
                UserPopoup.Show();
                return;
            }

            else if (Label1.Text == "464")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Manager);
                DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                dvResultUser.PageIndex = 0;
                UserPopoup.Show();
                return;
            }

            else if (Label1.Text == "465")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Manager);
                DataTable _result = CHNLSVC.General.Get_EMPCode(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                dvResultUser.PageIndex = 0;
                UserPopoup.Show();
                return;
            }


        }

        private string GetCompanyDesc(string code)
        {
            try
            {
                MasterCompany company = CHNLSVC.General.GetCompByCode(code);
                if (company != null)
                {
                    return company.Mc_desc;
                }

            }
            catch (Exception)
            {
                DisplayMessage("Error Occurred while processing", 2);
            }

            return null;
        }

        private string GetCustomer(string cusCode)
        {
            try
            {
                if (cusCode != null)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, "Code", "%" + cusCode.Trim().ToUpper().ToString(), CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    _result.Columns.Remove("Code1");

                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == cusCode.Trim().ToUpper().ToString()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        DisplayMessage("Please enter a valid customer code!", 2);
                        return null;
                    }

                    return cusCode;
                }
            }

            catch (Exception)
            {
                DisplayMessage("Error Occurred while processing!", 2);
            }

            return null;
        }

        //call the service layer from client
        private void cleargrids(string p)
        {
            if (p == "PC")
            {
                txtPCPrftCntr.Text = "";
                txtPCAssDate.Text = Convert.ToDateTime(DateTime.Now.Date).ToString("dd/MMM/yyyy");
                txtPCRptCode.Text = "";
                txtPCMaxSVal.Text = "0.00";
                txtPCManager.Text = "";
                chkPCStatus.Checked = true;
                chkPCPermission.Checked = false;
            }
            if (p == "LOC")
            {
                txtLLocation.Text = "";
                txtLAssDate.Text = Convert.ToDateTime(DateTime.Now.Date).ToString("dd/MMM/yyyy");
                txtLRptCde.Text = "";
                txtLMaxSVal.Text = "0.00";
                txtLManager.Text = "";
                chkLStatus.Checked = true;
                chkLPermission.Checked = false;
            }
            if (p == "CUS")
            {
                txtCusCde.Text = "";
                txtCusName.Text = "";
                chkCusAssStatus.Checked = true;
            }

        }

        private void Sextype()
        {
            ddlSex.Items.Clear();
            List<ListItem> items = new List<ListItem>();
            items.Add(new ListItem("Male", "M"));
            items.Add(new ListItem("Female", "F"));
            ddlSex.Items.AddRange(items.ToArray());

        }

        private void Titletype()
        {
            ddlTitle.Items.Clear();
            List<ListItem> items = new List<ListItem>();
            items.Add(new ListItem("Mr.", "Mr."));
            items.Add(new ListItem("Ms.", "Ms."));
            items.Add(new ListItem("Mrs.", "Mrs."));
            items.Add(new ListItem("Miss.", "Miss."));
            items.Add(new ListItem("Dr.", "Dr."));
            items.Add(new ListItem("Rev.", "Rev."));
            ddlTitle.Items.AddRange(items.ToArray());

        }

        protected void CompanyBasedCurrency()
        {
            try
            {
                MasterCompany _mstComp = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                if (_mstComp != null)
                {
                    if (_mstComp.Mc_cur_cd != null)
                    {
                        LoadCurrency(_mstComp.Mc_cur_cd.Trim().ToUpper());
                    }
                }
            }
            catch (Exception)
            {
                DisplayMessage("Error Occurred while processing", 2);
            }

        }

        protected void LoadCurrency(string currencycode)
        {
            try
            {
                if (!string.IsNullOrEmpty(currencycode))
                {

                    DataTable _result = CHNLSVC.CommonSearch.GetCurrencyData(currencycode, null, null);
                    if (_result != null)
                    {
                        //txtDefExRt.Text = _result.;
                        DataView dv = new DataView(_result);
                        dv.RowFilter = "Code ='" + currencycode + "'";

                        if (dv.Count > 0)
                        {
                            Currency.Text = dv[0]["Code"].ToString();
                            Currency.ToolTip = dv[0]["Description"].ToString();

                            PCCurrency.Text = dv[0]["Code"].ToString();
                            PCCurrency.ToolTip = dv[0]["Description"].ToString();

                            LCurrency.Text = dv[0]["Code"].ToString();
                            LCurrency.ToolTip = dv[0]["Description"].ToString();
                        }

                    }

                }

            }
            catch (Exception)
            {
                DisplayMessage("Error Occurred while processing", 2);
            }

        }

        protected void lbtngrdemployeemaster_profitmasterDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDeleteconformmessageValue.Value == "Yes")
                {
                    LinkButton btn = (LinkButton)sender;
                    GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                    _MPcE = ViewState["_MPcE"] as List<MasterProfitCenter_LocationEmployee>;
                    _MPcE.RemoveAt(grdr.RowIndex);

                    ViewState["_MPcE"] = _MPcE;
                    grdemployeemaster_profitmaster.DataSource = _MPcE;
                    grdemployeemaster_profitmaster.DataBind();
                }
            }
            catch (Exception)
            {
                DisplayMessage("Error Occurred while processing", 2);
            }
        }

        protected void lbtngrdemployeemaster_locationDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDeleteconformmessageValue.Value == "Yes")
                {
                    LinkButton btn = (LinkButton)sender;
                    GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                    _MLE = ViewState["_MLE"] as List<MasterProfitCenter_LocationEmployee>;
                    _MLE.RemoveAt(grdr.RowIndex);

                    ViewState["_MLE"] = _MLE;
                    grdemployeemaster_location.DataSource = _MLE;
                    grdemployeemaster_location.DataBind();
                }
            }
            catch (Exception)
            {
                DisplayMessage("Error Occurred while processing", 2);
            }
        }

        protected void lbtngrdcustomer_employeeDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDeleteconformmessageValue.Value == "Yes")
                {
                    LinkButton btn = (LinkButton)sender;
                    GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                    _MstCusEmp = ViewState["_MstCusEmp"] as List<MasterCustomerEmployee>;
                    _MstCusEmp.RemoveAt(grdr.RowIndex);

                    ViewState["_MstCusEmp"] = _MstCusEmp;
                    grdcustomer_employee.DataSource = _MstCusEmp;
                    grdcustomer_employee.DataBind();
                }
            }
            catch (Exception)
            {
                DisplayMessage("Error Occurred while processing", 2);
            }
        }

        protected void lbtnepfupdate_Click(object sender, EventArgs e)
        {
            try
            {
                upSunaccount.Show();
            }
            catch (Exception ex)
            {

            }
        }

        protected void lbtnPriceClose_Click(object sender, EventArgs e)
        {
            try
            {
                upSunaccount.Hide();
            }
            catch (Exception ex)
            {

            }
        }

        protected void txtepfnew_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnupdateepfnew_Click(object sender, EventArgs e)
        {
            try
            {
                string empcd = txtEPFNo.Text.ToString();
                string com = Session["UserCompanyCode"].ToString();
                string newepf = txtepfnew.Text.ToString();
                if (empcd == "")
                {
                    DisplayMessage("Please select Emp code!!!", 2);
                    return;
                }
                //update process
                int effect = CHNLSVC.General.AmmendEPFNew(empcd, newepf, com);

                if (effect > 0)
                {
                    DisplayMessage("Successfull Ammend!!!", 3);
                    txtEmpCode.Text = newepf;
                    return;
                }
                else
                {
                    DisplayMessage("Please Check Emp Code!!!", 4);
                    return;
                }


            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
                return;
            }
        }
        public bool validateinputString(string input)
        {
            var regexItem = new Regex("^[a-zA-Z0-9-/|]*$");
            if (!regexItem.IsMatch(input))
            {
                return false;
            }
            return true;
        }
        public bool validateinputStringWithSpace(string input)
        {
            var regexItem = new Regex("^[a-zA-Z0-9-/|]*$");
            if (!(regexItem.IsMatch(input) || input.Contains(" ")))
            {
                return false;
            }
            return true;
        }

        protected void txtEmpFirstName_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtEmpFirstName.Text))
            {
                DisplayMessage("Invalid charactor found in first name.", 2);
                txtEmpFirstName.Focus();
                return;
            }
        }

        protected void txtEmpLastName_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtEmpLastName.Text))
            {
                DisplayMessage("Invalid charactor found in last name.", 2);
                txtEmpLastName.Focus();
                return;
            }
        }

        protected void txtEmpNameInt_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtEmpNameInt.Text))
            {
                DisplayMessage("Invalid charactor found in name with initials.", 2);
                txtEmpNameInt.Focus();
                return;
            }
        }

        protected void txtEmpPolice_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtEmpPolice.Text))
            {
                DisplayMessage("Invalid charactor found in police station.", 2);
                txtEmpPolice.Focus();
                return;
            }
        }

        protected void txtEmpPAL1_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtEmpPAL1.Text))
            {
                DisplayMessage("Invalid charactor found in permanant address 1.", 2);
                txtEmpPAL1.Focus();
                return;
            }
        }

        protected void txtEmpPAL2_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtEmpPAL2.Text))
            {
                DisplayMessage("Invalid charactor found in permanant address 2.", 2);
                txtEmpPAL2.Focus();
                return;
            }
        }

        protected void txtEmpPAL3_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtEmpPAL3.Text))
            {
                DisplayMessage("Invalid charactor found in permanant address 3.", 2);
                txtEmpPAL3.Focus();
                return;
            }
        }

        protected void txtEmpCAL1_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtEmpCAL1.Text))
            {
                DisplayMessage("Invalid charactor found in current address 1.", 2);
                txtEmpCAL1.Focus();
                return;
            }
        }

        protected void txtEmpCAL2_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtEmpCAL2.Text))
            {
                DisplayMessage("Invalid charactor found in current address 2.", 2);
                txtEmpCAL2.Focus();
                return;
            }
        }

        protected void txtEmpCAL3_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtEmpCAL3.Text))
            {
                DisplayMessage("Invalid charactor found in current address 3.", 2);
                txtEmpCAL3.Focus();
                return;
            }
        }

        protected void txtContractor_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtContractor.Text))
            {
                DisplayMessage("Invalid charactor found in contractor code.", 2);
                txtContractor.Focus();
                return;
            }
        }
    }
}