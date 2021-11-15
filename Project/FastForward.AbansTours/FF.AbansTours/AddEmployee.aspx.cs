using FF.AbansTours.UserControls;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FF.AbansTours
{
    public partial class AddEmployee : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    RefTitle();
                    EmployeeType();
                    LicenseCategory();
                    ProfitCenter();                    

                    grdProfitCenter.DataSource = new int[] { };
                    grdProfitCenter.DataBind();

                    txtEPFNo.Enabled = true;

                    PageClear();
                    txtEPFNo.Focus();
                }
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }
        }        
        protected void ddlEmployeeType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlEmployeeType.SelectedItem.Text == "DRIVER")
                {
                    LicenseDivClear();
                    LicenseDiv.Visible = true;
                }
                else 
                {
                    LicenseDivClear();
                    LicenseDiv.Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }
        }
        protected void imgbtnEmployeeCode_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page page = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)page.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeTBS);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.SearchEmployeeTBS(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtEPFNo.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtEPFNo.Focus();
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }    
        protected void txtEPFNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                EmployeeDetails();
                txtEPFNo.Focus();
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }
        }
        protected void imgbtnEmployeeCodeSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                EmployeeDetails();
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }
        }
        protected void txtNIC_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dobGeneration();
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }
        }
        protected void btnAAddProfitCenter_Click(object sender, EventArgs e)
        {
            try
            {               
                MST_PCEMP mst_pcemp = new MST_PCEMP();
                List<MST_PCEMP> list = new List<MST_PCEMP>();                
                if (Session["ProfitCenter"] != null)
                {
                    list = (List<MST_PCEMP>)Session["ProfitCenter"];                                     
                }                

                mst_pcemp.MPE_EPF = txtEPFNo.Text;
                mst_pcemp.MPE_COM = base.GlbUserComCode;
                mst_pcemp.MPE_PC = ddlProfitCenter.SelectedItem.Text;
                mst_pcemp.MPE_ASSN_DT = DateTime.Now;
                mst_pcemp.MPE_ACT = 1;

                MST_PCEMP em = (list.Find(P => P.MPE_PC == ddlProfitCenter.SelectedItem.Text));
                if (em == null)
                    list.Add(mst_pcemp);
                
                Session["ProfitCenter"] = list;
                grdProfitCenter.DataSource = list;
                grdProfitCenter.DataBind();
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }
        }
        protected void ChkActive_CheckedChanged(object sender, EventArgs e)
        {
            
            List<MST_PCEMP> list = new List<MST_PCEMP>();

            if (Session["ProfitCenter"] != null)
            {
                foreach (GridViewRow row in grdProfitCenter.Rows)
                {
                    CheckBox chk = row.Cells[1].FindControl("ChkActive") as CheckBox;
                    MST_PCEMP mst_pcemp = new MST_PCEMP();
                    mst_pcemp.MPE_EPF = txtEPFNo.Text;
                    mst_pcemp.MPE_COM = base.GlbUserComCode;
                    mst_pcemp.MPE_PC = row.Cells[0].Text;
                    mst_pcemp.MPE_ASSN_DT = DateTime.Now;
                    mst_pcemp.MPE_ACT = Convert.ToInt32(chk.Checked);                    
                                        
                    list.Add(mst_pcemp);
                }

                Session["ProfitCenter"] = list;
            }         
            
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int check = CHNLSVC.Tours.Check_Employeeepf(txtEPFNo.Text);
                if (check == 1)
                {
                    txtEPFNo.Text = string.Empty;                    
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Invalid EPF Number.');", true);
                }
                else
                {
                    string err = "0";
                    MST_EMPLOYEE_TBS mst_employee_tbs = new MST_EMPLOYEE_TBS();

                    mst_employee_tbs.MEMP_COM_CD = base.GlbUserComCode;
                    mst_employee_tbs.MEMP_CD = txtEPFNo.Text;
                    mst_employee_tbs.MEMP_CRE_BY = Convert.ToString(Session["UserID"]);
                    mst_employee_tbs.MEMP_CRE_DT = DateTime.Now;

                    mst_employee_tbs.MEMP_TITLE = ddlTitle.SelectedItem.Text;
                    mst_employee_tbs.MEMP_FIRST_NAME = txtFirstName.Text;
                    mst_employee_tbs.MEMP_LAST_NAME = txtLastName.Text;
                    mst_employee_tbs.MEMP_EPF = txtEPFNo.Text;
                    mst_employee_tbs.MEMP_NIC = txtNIC.Text;
                    if (txtDateOfBirth.Text != "")
                        mst_employee_tbs.MEMP_DOB = Convert.ToDateTime(txtDateOfBirth.Text);
                    if (txtDateOfJoin.Text != "")
                        mst_employee_tbs.MEMP_DOJ = Convert.ToDateTime(txtDateOfJoin.Text);
                    mst_employee_tbs.MEMP_LIVING_ADD_1 = txtAddress1.Text;
                    mst_employee_tbs.MEMP_LIVING_ADD_2 = txtAddress2.Text;
                    mst_employee_tbs.MEMP_LIVING_ADD_3 = txtAddress3.Text;
                    mst_employee_tbs.MEMP_TEL_HOME_NO = txtPhone.Text;
                    mst_employee_tbs.MEMP_MOBI_NO = txtMobile.Text;

                    mst_employee_tbs.MEMP_CON_PER = txtContactPerson.Text;
                    mst_employee_tbs.MEMP_CON_PER_MOB = txtMobile.Text;
                    mst_employee_tbs.MEMP_ACT = Convert.ToInt16(ddlStatus.SelectedItem.Value);

                    mst_employee_tbs.MEMP_CAT_SUBCD = ddlEmployeeSource.SelectedItem.Text;
                    mst_employee_tbs.MEMP_CAT_CD = ddlEmployeeType.SelectedItem.Text;
                    mst_employee_tbs.MEMP_TOU_LIC = txtTouristBoardLicNo.Text;

                    if (ddlEmployeeType.SelectedItem.Text == "DRIVER")
                    {
                        mst_employee_tbs.MEMP_LIC_NO = txtLicenseNo.Text;
                        mst_employee_tbs.MEMP_LIC_CAT = ddlLicenseCategory.SelectedItem.Text;
                        if (txtLicenceExpDate.Text != "")
                            mst_employee_tbs.MEMP_LIC_EXDT = Convert.ToDateTime(txtLicenceExpDate.Text);
                    }

                    List<MST_PCEMP> list = new List<MST_PCEMP>();
                    if (Session["ProfitCenter"] != null)
                    {
                        foreach (GridViewRow row in grdProfitCenter.Rows)
                        {
                            CheckBox chk = row.Cells[1].FindControl("ChkActive") as CheckBox;
                            MST_PCEMP mst_pcemp = new MST_PCEMP();
                            mst_pcemp.MPE_EPF = txtEPFNo.Text;
                            mst_pcemp.MPE_COM = base.GlbUserComCode;
                            mst_pcemp.MPE_PC = row.Cells[0].Text;
                            mst_pcemp.MPE_ASSN_DT = DateTime.Now;
                            mst_pcemp.MPE_ACT = Convert.ToInt32(chk.Checked);

                            list.Add(mst_pcemp);
                        }
                        //list = (List<MST_PCEMP>)Session["ProfitCenter"];
                    }

                    int result = CHNLSVC.Tours.SaveEmployee(mst_employee_tbs, list, out err);

                    if (result == 1)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Successfully Saved.');", true);
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Error: " + err + "');", true);
                    }
                    PageClear();
                }

            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string err = "0";
                MST_EMPLOYEE_TBS mst_employee_tbs = new MST_EMPLOYEE_TBS();

                mst_employee_tbs.MEMP_COM_CD = base.GlbUserComCode;
                mst_employee_tbs.MEMP_CD = txtEPFNo.Text;
                mst_employee_tbs.MEMP_CRE_BY = Convert.ToString(Session["UserID"]);
                mst_employee_tbs.MEMP_CRE_DT = DateTime.Now;

                mst_employee_tbs.MEMP_TITLE = ddlTitle.SelectedItem.Text;
                mst_employee_tbs.MEMP_FIRST_NAME = txtFirstName.Text;
                mst_employee_tbs.MEMP_LAST_NAME = txtLastName.Text;
                mst_employee_tbs.MEMP_EPF = txtEPFNo.Text;
                mst_employee_tbs.MEMP_NIC = txtNIC.Text;
                if (txtDateOfBirth.Text != "")
                    mst_employee_tbs.MEMP_DOB = Convert.ToDateTime(txtDateOfBirth.Text);
                if (txtDateOfJoin.Text != "")
                    mst_employee_tbs.MEMP_DOJ = Convert.ToDateTime(txtDateOfJoin.Text);
                mst_employee_tbs.MEMP_LIVING_ADD_1 = txtAddress1.Text;
                mst_employee_tbs.MEMP_LIVING_ADD_2 = txtAddress2.Text;
                mst_employee_tbs.MEMP_LIVING_ADD_3 = txtAddress3.Text;
                mst_employee_tbs.MEMP_TEL_HOME_NO = txtPhone.Text;
                mst_employee_tbs.MEMP_MOBI_NO = txtMobile.Text;

                mst_employee_tbs.MEMP_CON_PER = txtContactPerson.Text;
                mst_employee_tbs.MEMP_CON_PER_MOB = txtContactPersonMobile.Text;
                mst_employee_tbs.MEMP_ACT = Convert.ToInt16(ddlStatus.SelectedItem.Value);

                mst_employee_tbs.MEMP_CAT_SUBCD = ddlEmployeeSource.SelectedItem.Text;
                mst_employee_tbs.MEMP_CAT_CD = ddlEmployeeType.SelectedItem.Text;
                mst_employee_tbs.MEMP_TOU_LIC = txtTouristBoardLicNo.Text;

                if (ddlEmployeeType.SelectedItem.Text == "DRIVER")
                {
                    mst_employee_tbs.MEMP_LIC_NO = txtLicenseNo.Text;
                    mst_employee_tbs.MEMP_LIC_CAT = ddlLicenseCategory.SelectedItem.Text;
                    if (txtLicenceExpDate.Text != "")
                        mst_employee_tbs.MEMP_LIC_EXDT = Convert.ToDateTime(txtLicenceExpDate.Text);
                }

                List<MST_PCEMP> list = new List<MST_PCEMP>();
                if (Session["ProfitCenter"] != null)
                {
                    foreach (GridViewRow row in grdProfitCenter.Rows)
                    {
                        CheckBox chk = row.Cells[1].FindControl("ChkActive") as CheckBox;
                        MST_PCEMP mst_pcemp = new MST_PCEMP();
                        mst_pcemp.MPE_EPF = txtEPFNo.Text;
                        mst_pcemp.MPE_COM = base.GlbUserComCode;
                        mst_pcemp.MPE_PC = row.Cells[0].Text;
                        mst_pcemp.MPE_ASSN_DT = DateTime.Now;
                        mst_pcemp.MPE_ACT = Convert.ToInt32(chk.Checked);

                        list.Add(mst_pcemp);
                    }
                    //list = (List<MST_PCEMP>)Session["ProfitCenter"];
                }

                int result = CHNLSVC.Tours.UpdateEmployee(mst_employee_tbs, list, out err);

                if (result > 0)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Successfully Updated.');", true);
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Error: " + err + "');", true);
                }

                PageClear();
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }
        } 
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                PageClear();
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }
        }


        private void EmployeeDetails()
        {
            MST_EMPLOYEE_TBS mst_employee_tbs = new MST_EMPLOYEE_TBS();

            mst_employee_tbs = CHNLSVC.Tours.Get_mst_employee(txtEPFNo.Text);

            if (mst_employee_tbs.MEMP_EPF == "" || mst_employee_tbs.MEMP_EPF == null)
            {

            }
            else
            {
                ddlTitle.SelectedValue = mst_employee_tbs.MEMP_TITLE;
                txtFirstName.Text = mst_employee_tbs.MEMP_FIRST_NAME;
                txtLastName.Text = mst_employee_tbs.MEMP_LAST_NAME;
                txtNIC.Text = mst_employee_tbs.MEMP_NIC;
                txtDateOfBirth.Text = mst_employee_tbs.MEMP_DOB.ToString("dd/MMM/yyyy");
                txtDateOfJoin.Text = mst_employee_tbs.MEMP_DOJ.ToString("dd/MMM/yyyy");
                txtAddress1.Text = mst_employee_tbs.MEMP_LIVING_ADD_1;
                txtAddress2.Text = mst_employee_tbs.MEMP_LIVING_ADD_2;
                txtAddress3.Text = mst_employee_tbs.MEMP_LIVING_ADD_3;
                txtPhone.Text = mst_employee_tbs.MEMP_TEL_HOME_NO;
                txtMobile.Text = mst_employee_tbs.MEMP_MOBI_NO;

                txtContactPerson.Text = mst_employee_tbs.MEMP_CON_PER;
                txtContactPersonMobile.Text = mst_employee_tbs.MEMP_CON_PER_MOB;
                ddlStatus.SelectedValue = mst_employee_tbs.MEMP_ACT.ToString();

                ddlEmployeeSource.SelectedValue = mst_employee_tbs.MEMP_CAT_SUBCD;
                ddlEmployeeType.SelectedValue = mst_employee_tbs.MEMP_CAT_CD;
                txtTouristBoardLicNo.Text = mst_employee_tbs.MEMP_TOU_LIC;

                if (mst_employee_tbs.MEMP_CAT_CD == "DRIVER")
                {
                    LicenseDiv.Visible = true;
                    txtLicenseNo.Text = mst_employee_tbs.MEMP_LIC_NO;
                    ddlLicenseCategory.SelectedValue = mst_employee_tbs.MEMP_LIC_CAT;
                    txtLicenceExpDate.Text = mst_employee_tbs.MEMP_LIC_EXDT.ToString("dd/MMM/yyyy");
                }

                List<MST_PCEMP> list = new List<MST_PCEMP>();

                list = CHNLSVC.Tours.Get_mst_pcemp(txtEPFNo.Text);
                Session["ProfitCenter"] = list;
                grdProfitCenter.DataSource = list;
                grdProfitCenter.DataBind();

                txtEPFNo.Enabled = false;
                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
            }
        }
        private void RefTitle()
        {
            List<Ref_Title> ref_Title = CHNLSVC.Tours.GET_REF_TITLE();
            if (ref_Title.Count > 0)
            {
                ddlTitle.DataSource = ref_Title;
                ddlTitle.DataTextField = "RTIT_CD";
                ddlTitle.DataValueField = "RTIT_CD";
                ddlTitle.DataBind();
            }
            else 
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Invalid title selected.');", true);
            }
        }
        private void EmployeeType()
        {
            List<Mst_empcate> mst_empcate = CHNLSVC.Tours.Get_mst_empcate();
            if (mst_empcate.Count > 0)
            {
                ddlEmployeeType.DataSource = mst_empcate;
                ddlEmployeeType.DataTextField = "ECM_CAT";
                ddlEmployeeType.DataValueField = "ECM_CAT";
                ddlEmployeeType.DataBind();
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Invalid employee Type selected.');", true);
            }
        }
        private void LicenseCategory()
        {
            List<MST_VEH_TP> mst_empcate = CHNLSVC.Tours.Get_mst_veh_tp();
            if (mst_empcate.Count > 0)
            {
                ddlLicenseCategory.DataSource = mst_empcate;
                ddlLicenseCategory.DataTextField = "MVT_CD";
                ddlLicenseCategory.DataValueField = "MVT_CD";
                ddlLicenseCategory.DataBind();
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Invalid employee Type selected.');", true);
            }
        }
        private void ProfitCenter()
        {
            DataTable mst_empcate = CHNLSVC.Tours.Get_mst_profit_center(base.GlbUserComCode);
            if (mst_empcate.Rows.Count > 0 && ddlProfitCenter.Items.Count == 0)
            {
                ddlProfitCenter.DataSource = mst_empcate;
                ddlProfitCenter.DataTextField = "MPC_CD";
                ddlProfitCenter.DataValueField = "MPC_CD";
                ddlProfitCenter.DataBind();
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Invalid employee Type selected.');", true);
            }
        }
        private void dobGeneration()
        {
            try
            {
                String nic_ = txtNIC.Text.Trim().ToUpper();
                char[] nicarray = nic_.ToCharArray();
                string thirdNum = (nicarray[2]).ToString();

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

                DateTime dob = new DateTime(dobYear, dobMon, bornDate);
                //dtpDOB.Value = dob.Date;
                if (dob.Date.ToString("dd-MMM-yyyy") == "")
                    txtDateOfBirth.Text = String.Empty;
                else
                    txtDateOfBirth.Text = dob.Date.ToString("dd-MMM-yyyy");
            }
            catch (Exception ex)
            {
                txtDateOfBirth.Text = String.Empty;
                txtNIC.Text = String.Empty;
            }
        }
        private void LicenseDivClear()
        {
            txtLicenseNo.Text = string.Empty;
            if (ddlLicenseCategory.Items.Count > 0)
            ddlLicenseCategory.SelectedIndex = 0;
            txtLicenceExpDate.Text = string.Empty;
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.EmployeeTBS:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator);
                        break;
                    }                
                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void PageClear()
        {
            ddlTitle.SelectedIndex = 0;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtNIC.Text = string.Empty;
            txtDateOfBirth.Text = string.Empty;
            txtDateOfJoin.Text = string.Empty;
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtAddress3.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtEPFNo.Text = string.Empty;
            txtEPFNo.Enabled = true;
            ddlEmployeeSource.SelectedIndex = 0;
            ddlEmployeeType.SelectedIndex = 0;
            txtTouristBoardLicNo.Text = string.Empty;

            txtLicenseNo.Text = string.Empty;
            ddlLicenseCategory.SelectedIndex = 0;
            txtLicenceExpDate.Text = string.Empty;

            LicenseDiv.Visible = false;

            txtContactPerson.Text = string.Empty;
            txtContactPersonMobile.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            ddlProfitCenter.SelectedIndex = 0;

            Session["ProfitCenter"] = null;

            grdProfitCenter.DataSource = new int[] { };
            grdProfitCenter.DataBind();

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;            
            
        }        
    }
}