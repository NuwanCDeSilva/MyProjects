using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using FF.WindowsERPClient.Reports.Sales;


namespace FF.WindowsERPClient.General
{
    public partial class CustomerCreation : Base
    {

        private MasterBusinessEntity _custProfile = new MasterBusinessEntity();
        private CustomerAccountRef _account = new CustomerAccountRef();
        private List<MasterBusinessEntityInfo> _busInfoList = new List<MasterBusinessEntityInfo>();

        private Boolean _isExsit = false;
        
        public Boolean _isFromOther = false;
        public TextBox obj_TragetTextBox;

        //constructor added by Shani 18-02-2013
        public CustomerCreation(string userID,string UserComCode, string _OrgPC)
        {
            InitializeComponent();           
            if (CHNLSVC.Inventory.CheckUserPermission(userID, UserComCode, _OrgPC, "HP4"))
            {
                btnAdvanceUpdate.Visible = true;
            }
            else
            {               
                btnAdvanceUpdate.Visible = false;
                //return;
            }
        }
        public CustomerCreation()
        {
            InitializeComponent();
        }

        private void CustomerCreation_Load(object sender, EventArgs e)
        {
           Clear_Data();
        }

        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion


        private void Clear_Data()
        {
            _custProfile = new MasterBusinessEntity();
            _account = new CustomerAccountRef();
            _busInfoList = new List<MasterBusinessEntityInfo>();

            txtCusCode.Text = "";
            txtCusCode.Enabled = true;
            _isExsit = false;
            cmbType.Text = "INDIVIDUAL";
            cmbSex.Text = "";
            cmbTitle.Text = "";
            txtNIC.Text = "";
            txtBR.Text = "";
            txtPP.Text = "";
            txtDL.Text = "";
            txtMob.Text = "";
            chkSMS.Checked = false;
            txtName.Text = "";
            txtPerAdd1.Text = "";
            txtPerAdd2.Text = "";
            txtPerTown.Text = "";
            txtPerDistrict.Text = "";
            txtPerPostal.Text = "";
            txtPerProvince.Text = "";
            txtPerCountry.Text = "";
            txtPerPhone.Text = "";
            txtPerEmail.Text = "";
            txtPreAdd1.Text = "";
            txtPreAdd2.Text = "";
            txtPreTown.Text = "";
            txtPreDistrict.Text = "";
            txtPreProvince.Text = "";
            txtPrePostal.Text = "";
            txtPreCountry.Text = "";
            txtPrePhone.Text = "";
            txtWorkName.Text = "";
            txtWorkAdd1.Text = "";
            txtWorkAdd2.Text = "";
            txtWorkDept.Text = "";
            txtWorkDesig.Text = "";
            txtWorkPhone.Text = "";
            txtWorkFax.Text = "";
            txtWorkEmail.Text = "";
            dtpDOB.Value = Convert.ToDateTime(DateTime.Today).Date;

            chkVAT.Checked = false;
            chkSVAT.Checked = false;
            chkVatEx.Checked = false;

            txtVatreg.Text = "";
            txtSVATReg.Text = "";
            txtVatreg.Enabled = false;
            txtSVATReg.Enabled = false;
            tbAdd.SelectedTab = tabPage1;
            btnCreate.Text = "Create";
            cmbType.Focus();
        }

       


       

        public bool IsValidNIC(string nic)
        {
            //THIS IS CASE SENSITIVE IF U DONT WONT CASE SENSITIVE PLEASE USE FOLLOWING CODE
            // email = email.ToLower();
            string pattern = @"\d{9}[V|v|x|X]";
            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            //boolean variable to return to calling method
            bool valid = false;
            //make sure an email address was provided
            if (string.IsNullOrEmpty(nic))
            {
                valid = false;
            }
            else
            {
                //use IsMatch to validate the address
                valid = check.IsMatch(nic);
            }
            //return the value to the calling method
            return valid;
        }

        private void txtperTown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
                DataTable _result = CHNLSVC.CommonSearch.GetTown(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPerTown;
                _CommonSearch.ShowDialog();
                txtPerTown.Select();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtPerPhone.Focus();
            }
        }


        private void txtPerTown_Leave(object sender, EventArgs e)
        {
            txtPerDistrict.Text = "";
            txtPerProvince.Text = "";
            txtPerPostal.Text = "";
            txtPerCountry.Text = "";

            if (!string.IsNullOrEmpty(txtPerTown.Text))
            {
                DataTable dt = new DataTable();

                dt = CHNLSVC.General.Get_DetBy_town(txtPerTown.Text.Trim());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string district = dt.Rows[0]["DISTRICT"].ToString();
                        string province = dt.Rows[0]["PROVINCE"].ToString();
                        string postalCD = dt.Rows[0]["POSTAL_CD"].ToString();
                        string countryCD = dt.Rows[0]["COUNTRY_CD"].ToString();

                        txtPerDistrict.Text = district;
                        txtPerProvince.Text = province;
                        txtPerPostal.Text = postalCD;
                        txtPerCountry.Text = countryCD;

                    }
                    else
                    {
                        MessageBox.Show("Invalid town.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPerTown.Text = "";
                        txtPerTown.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid town.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPerTown.Text = "";
                    txtPerTown.Focus();
                }
            }
        }

        private void txtPreTown_Leave(object sender, EventArgs e)
        {
            txtPreDistrict.Text = "";
            txtPreProvince.Text = "";
            txtPrePostal.Text = "";
            txtPreCountry.Text = "";

            if (!string.IsNullOrEmpty(txtPreTown.Text))
            {
                DataTable dt = new DataTable();

                dt = CHNLSVC.General.Get_DetBy_town(txtPreTown.Text.Trim());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string district = dt.Rows[0]["DISTRICT"].ToString();
                        string province = dt.Rows[0]["PROVINCE"].ToString();
                        string postalCD = dt.Rows[0]["POSTAL_CD"].ToString();
                        string countryCD = dt.Rows[0]["COUNTRY_CD"].ToString();

                        txtPreDistrict.Text = district;
                        txtPreProvince.Text = province;
                        txtPrePostal.Text = postalCD;
                        txtPreCountry.Text = countryCD;

                    }
                    else
                    {
                        MessageBox.Show("Invalid town.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPreTown.Text = "";
                        txtPreTown.Focus();
                    }

                }
                else
                {
                    MessageBox.Show("Invalid town.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPreTown.Text = "";
                    txtPreTown.Focus();
                }

            }
        }

        private void cmbType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCusCode.Focus();
            }
            
        }


        private void txtCusCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNIC.Focus();

            }
            else if (e.KeyCode == Keys.F2)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCusCode;
                _CommonSearch.ShowDialog();
                txtCusCode.Select();
            }
        }

        private void txtNIC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBR.Focus();
            }
        }

        private void txtBR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPP.Focus();
            }
        }

        private void txtPP_keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMob.Focus();
            }
        }

        private void txtMob_keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDL.Focus();
            }
        }

        private void txtDL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkSMS.Focus();
            }
        }

        private void chkSMS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbTitle.Focus();
            }
        }

        private void cmbTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbSex.Focus();
            }
        }

        private void cmbTitle_Leave(object sender, EventArgs e)
        {
            if (txtName.TextLength <= 5)
            {
                txtName.Text = cmbTitle.Text;
            }

        }

        private void cmbSex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtName.Focus();
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpDOB.Focus();
            }
        }

        private void dtpDOB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbAdd.SelectedTab = tabPage1;
                txtPerAdd1.Focus();
            }
        }

        private void txtPerAdd1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPerAdd2.Focus();
            }
        }

        private void txtPerAdd2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPerTown.Focus();
            }
        }

        private void txtPerPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPerEmail.Focus();
            }
        }

        private void txtWorkName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWorkAdd1.Focus();
            }
        }

        private void txtWorkAdd1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWorkAdd2.Focus();
            }
        }

        private void txtWorkAdd2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWorkDept.Focus();
            }
        }

        private void txtWorkDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWorkDesig.Focus();
            }
        }

        private void txtWorkDesig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWorkPhone.Focus();
            }
        }


        private void txtWorkPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWorkFax.Focus();
            }
        }

        private void txtWorkFax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWorkEmail.Focus();
            }
        }

        private void txtWorkFax_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWorkFax.Text))
            {
                Boolean _isvalid = IsValidMobileOrLandNo(txtWorkFax.Text.Trim());

                if (_isvalid == false)
                {
                    MessageBox.Show("Invalid phone number.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtWorkFax.Text = "";
                    txtWorkFax.Focus();
                    return;
                }
            }
        }


        private void txtWorkPhone_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWorkPhone.Text))
            {
                Boolean _isvalid = IsValidMobileOrLandNo(txtWorkPhone.Text.Trim());

                if (_isvalid == false)
                {
                    MessageBox.Show("Invalid phone number.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtWorkPhone.Text = "";
                    txtWorkPhone.Focus();
                    return;
                }
            }
        }


        private void txtWorkEmail_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWorkEmail.Text))
            {
                Boolean _isValid = IsValidEmail(txtWorkEmail.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Invalid email address.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtWorkEmail.Text = "";
                    txtWorkEmail.Focus();
                    return;
                }
            }
        }

        private void txtWorkEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtWorkEmail.Text))
                {
                    Boolean _isValid = IsValidEmail(txtWorkEmail.Text.Trim());

                    if (_isValid == false)
                    {
                        MessageBox.Show("Invalid email address.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtWorkEmail.Text = "";
                        txtWorkEmail.Focus();
                        return;
                    }
                }

                tbAdd.SelectedTab = tabPage4;
                chkVAT.Focus();
            }
        }


        private void txtPerEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtPerEmail.Text))
                {
                    Boolean _isValid = IsValidEmail(txtPerEmail.Text.Trim());

                    if (_isValid == false)
                    {
                        MessageBox.Show("Invalid email address.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPerEmail.Text = "";
                        txtPerEmail.Focus();
                        return;
                    }
                }
                tbAdd.SelectedTab = tabPage2;
                txtPreAdd1.Focus();
            }
        }

        private void txtPrePhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtPrePhone.Text))
                {
                    Boolean _isvalid = IsValidMobileOrLandNo(txtPrePhone.Text.Trim());

                    if (_isvalid == false)
                    {
                        MessageBox.Show("Invalid phone number.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPrePhone.Text = "";
                        txtPrePhone.Focus();
                        return;
                    }
                }
                tbAdd.SelectedTab = tabPage3;
                txtWorkName.Focus();
            }
        }

        private void txtPreAdd1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPreAdd2.Focus();
            }
        }

        private void txtPreAdd2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPreTown.Focus();
            }
        }

        private void txtPreTown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
                DataTable _result = CHNLSVC.CommonSearch.GetTown(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPreTown;
                _CommonSearch.ShowDialog();
                txtPreTown.Select();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtPrePhone.Focus();
            }
        }


        private void txtPerAdd1_Leave(object sender, EventArgs e)
        {
            txtPreAdd1.Text = txtPerAdd1.Text.Trim();
        }

        private void txtPerAdd2_Leave(object sender, EventArgs e)
        {
            txtPreAdd2.Text = txtPerAdd2.Text.Trim();
        }

        private void txtPrePhone_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPrePhone.Text))
            {
                Boolean _isvalid = IsValidMobileOrLandNo(txtPrePhone.Text.Trim());

                if (_isvalid == false)
                {
                    MessageBox.Show("Invalid phone number.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrePhone.Text = "";
                    txtPrePhone.Focus();
                    return;
                }
            }
        }


        private void txtPerPhone_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPerPhone.Text))
            {
                Boolean _isvalid = IsValidMobileOrLandNo(txtPerPhone.Text.Trim());

                if (_isvalid == false)
                {
                    MessageBox.Show("Invalid phone number.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPerPhone.Text = "";
                    txtPerPhone.Focus();
                    return;
                }
            }
        }

        private void txtPerEmail_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPerEmail.Text))
            {
                Boolean _isValid = IsValidEmail(txtPerEmail.Text.Trim());

                if (_isValid ==false)
                {
                    MessageBox.Show("Invalid email address.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPerEmail.Text = "";
                    txtPerEmail.Focus();
                    return;
                }
            }
        }

        private void txtPerPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                e.Handled = !char.IsNumber(e.KeyChar);
            }
        }

        private void txtMob_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                e.Handled = !char.IsNumber(e.KeyChar);
            }
        }

        private void txtPrePhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                e.Handled = !char.IsNumber(e.KeyChar);
            }
        }

        private void txtMob_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMob.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtMob.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Invalid mobile number.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMob.Text = "";
                    txtMob.Focus();
                    return;
                }
            }
        }

        private void txtNIC_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNIC.Text))
            {
                Boolean _isValid = IsValidNIC(txtNIC.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Invalid NIC.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNIC.Text = "";
                    txtNIC.Focus();
                    return;
                }
                else
                {
                    MasterBusinessEntity custProf = GetbyNIC(txtNIC.Text.Trim().ToUpper());
                    if (custProf.Mbe_cd != null)
                    {
                        _isExsit = true;
                        btnCreate.Text = "Update";
                        LoadCustProf(custProf);
                    }
                    else//added on 01/10/2012
                    {
                        if (_isExsit == true)
                        {
                            string nic = txtNIC.Text.Trim().ToUpper();
                            MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            LoadCustProf(cust_null);
                            txtNIC.Text = nic;
                        }
                        _isExsit = false;
                        btnCreate.Text = "Create";
                        String nic_ = txtNIC.Text.Trim().ToUpper();
                        char[] nicarray = nic_.ToCharArray();
                        string thirdNum = (nicarray[2]).ToString();
                        if (thirdNum == "5" || thirdNum == "6" || thirdNum == "7" || thirdNum == "8" || thirdNum == "9")
                        {
                            cmbSex.Text = "FEMALE";
                            cmbTitle.Text = "Ms.";
                        }
                        else
                        {
                            cmbSex.Text = "MALE";
                            cmbTitle.Text = "Mr.";
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
                            dtpDOB.Value = dob.Date;
                            //dob.ToString("dd/MM/yyyy");
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
        }


        private void txtCusCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCusCode.Text))
            {
                MasterBusinessEntity custProf = GetbyCustCD(txtCusCode.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null)
                {
                    _isExsit = true;
                    btnCreate.Text = "Update";
                    LoadCustProf(custProf);
                }
                else
                {
                    if (_isExsit == true)
                    {
                        string cusCD = txtCusCode.Text.Trim().ToUpper();
                        MasterBusinessEntity cust_null = new MasterBusinessEntity();
                        LoadCustProf(cust_null);
                        txtCusCode.Text = cusCD;
                    }
                    _isExsit = false;
                    btnCreate.Text = "Create";
                }
                //else//added on 01/10/2012
                //{
                //    string nic = txtNIC.Text.Trim().ToUpper();
                //    MasterBusinessEntity cust_null = new MasterBusinessEntity();
                //    LoadCustProf(cust_null);
                //    txtNIC.Text = nic;
                //}
            }
        }

        private void txtBR_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBR.Text))
            {
                MasterBusinessEntity custProf = GetbyBrNo(txtBR.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null)
                {
                    _isExsit = true;
                    btnCreate.Text = "Update";
                    LoadCustProf(custProf);
                }
                else
                {
                    if (_isExsit == true)
                    {
                        string BR = txtBR.Text.Trim().ToUpper();
                        MasterBusinessEntity cust_null = new MasterBusinessEntity();
                        LoadCustProf(cust_null);
                        txtCusCode.Text = BR;
                    }
                    _isExsit = false;
                    btnCreate.Text = "Create";
                }
            }
        }

        private void txtPP_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPP.Text))
            {
                MasterBusinessEntity custProf = GetbyPPno(txtPP.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null)
                {
                    _isExsit = true;
                    btnCreate.Text = "Update";
                    LoadCustProf(custProf);
                }
                else
                {
                    if (_isExsit == true)
                    {
                        string PP = txtPP.Text.Trim().ToUpper();
                        MasterBusinessEntity cust_null = new MasterBusinessEntity();
                        LoadCustProf(cust_null);
                        txtPP.Text = PP;
                    }
                    _isExsit = false;
                    btnCreate.Text = "Create";
                }
            }
        }

        private void txtDL_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDL.Text))
            {
                MasterBusinessEntity custProf = GetbyDL(txtDL.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null)
                {
                    _isExsit = true;
                    btnCreate.Text = "Update";
                    LoadCustProf(custProf);
                }
                else
                {
                    if (_isExsit == true)
                    {
                        string DL = txtDL.Text.Trim().ToUpper();
                        MasterBusinessEntity cust_null = new MasterBusinessEntity();
                        LoadCustProf(cust_null);
                        txtDL.Text = DL;
                    }
                    _isExsit = false;
                    btnCreate.Text = "Create";
                }
            }
        }

        #region LoadCustProfile

        public MasterBusinessEntity GetbyCustCD(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(custCD, null, null, null, null, BaseCls.GlbUserComCode);
        }
        public MasterBusinessEntity GetbyNIC(string nic)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, nic, null, null, null, BaseCls.GlbUserComCode);
        }
        public MasterBusinessEntity GetbyDL(string dl)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, null, dl, null, null, BaseCls.GlbUserComCode);
        }
        public MasterBusinessEntity GetbyPPno(string ppno)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, null, null, ppno, null, BaseCls.GlbUserComCode);
        }
        public MasterBusinessEntity GetbyBrNo(string brNo)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, null, null, null, brNo, BaseCls.GlbUserComCode);
        }
        #endregion LoadCustProfile

        public void LoadCustProf(MasterBusinessEntity cust)
        {
            //string typed_nic = txtNicNo.Text.Trim();
            //string typed_ppno = txtPassportNo.Text.Trim().ToUpper();
            //string typed_dl = txtDLno.Text.Trim().ToUpper();
            //string typed_br = txtBrNo.Text.Trim().ToUpper();
            //------------------------------------------
            // ddlCustSupType.SelectedValue = cust.Mbe_sub_tp;
            cmbType.Text = cust.Mbe_cate;
            txtNIC.Text = cust.Mbe_nic;
            txtPP.Text = cust.Mbe_pp_no;
            txtBR.Text = cust.Mbe_br_no;
            txtCusCode.Text = cust.Mbe_cd;
            txtDL.Text = cust.Mbe_dl_no;
            txtMob.Text = cust.Mbe_mob;
            chkSMS.Checked = cust.Mbe_agre_send_sms;
            //------------------------------------------
            cmbSex.Text = cust.Mbe_sex;
            txtName.Text = cust.Mbe_name;
           
            if (cust.Mbe_dob.Date > Convert.ToDateTime("01-01-1000").Date)
            {
                dtpDOB.Value = Convert.ToDateTime(cust.Mbe_dob).Date;// Convert.ToString(cust.Mbe_dob.Date); 
            }
            else
            {
                
            }
            //-------------------------------------------

            txtPerAdd1.Text = cust.Mbe_add1;
            txtPerAdd2.Text = cust.Mbe_add2;
            txtPerTown.Text = cust.Mbe_town_cd;
            txtPerPostal.Text = cust.Mbe_postal_cd;
            txtPerCountry.Text = cust.Mbe_country_cd;
            txtPerDistrict.Text = cust.Mbe_distric_cd;
            txtPerProvince.Text = cust.Mbe_province_cd;
            txtPerPhone.Text = cust.Mbe_tel;
            txtPerEmail.Text = cust.Mbe_email;

            txtPreAdd1.Text = cust.Mbe_cr_add1;
            txtPreAdd2.Text = cust.Mbe_cr_add2;
            txtPreTown.Text = cust.Mbe_cr_town_cd;
            txtPrePostal.Text = cust.Mbe_cr_postal_cd;
            txtPreCountry.Text = cust.Mbe_cr_country_cd;
            txtPreDistrict.Text = cust.Mbe_cr_distric_cd;
            txtPreProvince.Text = cust.Mbe_cr_province_cd;
            txtPrePhone.Text = cust.Mbe_cr_tel;

            txtWorkAdd1.Text = cust.Mbe_wr_add1;
            txtWorkAdd2.Text = cust.Mbe_wr_add2;
            txtWorkName.Text = cust.Mbe_wr_com_name;
            txtWorkDept.Text = cust.Mbe_wr_dept;
            txtWorkDesig.Text = cust.Mbe_wr_designation;
            txtWorkEmail.Text = cust.Mbe_wr_email;
            txtWorkFax.Text = cust.Mbe_wr_fax;
            txtWorkPhone.Text = cust.Mbe_wr_tel;

            chkVAT.Checked = cust.Mbe_is_tax;
            chkVatEx.Checked = cust.Mbe_tax_ex;
            chkSVAT.Checked = cust.Mbe_is_svat;
            txtVatreg.Text = cust.Mbe_tax_no;
            txtSVATReg.Text = cust.Mbe_svat_no;

            //------------------------------------------

            txtCusCode.Enabled = false;
            //if (cust.Mbe_cd == "" || cust.Mbe_cd == null)
            //{
            //    txtNicNo.Text = typed_nic;
            //    txtPassportNo.Text = typed_ppno;
            //    txtDLno.Text = typed_dl;
            //    txtBrNo.Text = typed_br;
            //}

            
        }

       

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string _cusCode = "";
            Int32 _effect = 0;

            if (txtNIC.Text == "" && txtBR.Text == "" && txtPP.Text == "" && txtDL.Text == "" && txtMob.Text == "")
            {
                MessageBox.Show("One of required information not enterd.", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNIC.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please enter name of customer", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return;
            }

            Collect_Cust();

            if (_isExsit == false)
            {
                 _effect = CHNLSVC.Sales.SaveBusinessEntityDetail(_custProfile, _account, _busInfoList, out _cusCode);
            }
            else
            {
                _cusCode = txtCusCode.Text.Trim();
                 _effect = CHNLSVC.Sales.UpdateBusinessEntityProfile(_custProfile, BaseCls.GlbUserID, Convert.ToDateTime(DateTime.Today).Date, 0, _busInfoList);
            }

            if (_effect == 1)
            {
                if (_isExsit == false)
                {
                    MessageBox.Show("New customer created. Customer Code : " + _cusCode, "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Exsiting customer updated.", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (_isFromOther == true)
            {
                HP.AccountCreation _acc = new HP.AccountCreation();
                obj_TragetTextBox.Text = _cusCode;
                this.Close();
            }
            this.Clear_Data();
        }

        private void Collect_Cust()
        {
            Boolean _isSMS = false;
            Boolean _isSVAT = false;
            Boolean _isVAT = false;
            Boolean _TaxEx = false;

             _custProfile = new MasterBusinessEntity();
            _custProfile.Mbe_acc_cd = null;
            _custProfile.Mbe_act = true;
            _custProfile.Mbe_add1 = txtPerAdd1.Text.Trim();
            _custProfile.Mbe_add2 = txtPreAdd2.Text.Trim();
            if (chkSMS.Checked == true)
            {
                _isSMS = true;
            }
            else
            {
                _isSMS = false;
            }
            _custProfile.Mbe_agre_send_sms = _isSMS;
            _custProfile.Mbe_br_no = txtBR.Text.Trim();
            _custProfile.Mbe_cate = cmbType.Text;
            if (_isExsit == false)
            {
                _custProfile.Mbe_cd = null;
            }
            else
            {
                _custProfile.Mbe_cd = txtCusCode.Text.Trim();
            }
            _custProfile.Mbe_com = BaseCls.GlbUserComCode;
            _custProfile.Mbe_contact = null;
            _custProfile.Mbe_country_cd = null;
            _custProfile.Mbe_cr_add1 = txtPreAdd1.Text.Trim();
            _custProfile.Mbe_cr_add2 = txtPreAdd2.Text.Trim();
            _custProfile.Mbe_cr_country_cd = txtPreCountry.Text.Trim();
            _custProfile.Mbe_cr_distric_cd = txtPreDistrict.Text;
            _custProfile.Mbe_cr_email = null;
            _custProfile.Mbe_cr_fax = null;
            _custProfile.Mbe_cr_postal_cd = txtPrePostal.Text.Trim();
            _custProfile.Mbe_cr_province_cd = txtPreProvince.Text.Trim();
            _custProfile.Mbe_cr_tel = txtPrePhone.Text.Trim();
            _custProfile.Mbe_cr_town_cd = txtPreTown.Text.Trim();
            _custProfile.Mbe_cre_by = BaseCls.GlbUserID;
            _custProfile.Mbe_cre_dt = Convert.ToDateTime(DateTime.Today).Date;
            _custProfile.Mbe_cre_pc = BaseCls.GlbUserDefProf;
            _custProfile.Mbe_cust_com =BaseCls.GlbUserComCode;
            _custProfile.Mbe_cust_loc = BaseCls.GlbUserDefLoca;
            _custProfile.Mbe_distric_cd = txtPerDistrict.Text;
            _custProfile.Mbe_dl_no = txtDL.Text.Trim();
            _custProfile.Mbe_dob = Convert.ToDateTime(dtpDOB.Text).Date;
            _custProfile.Mbe_email = txtPerEmail.Text.Trim();
            _custProfile.Mbe_fax = null;
            _custProfile.Mbe_ho_stus = "GOOD";
            _custProfile.Mbe_income_grup = null;
            _custProfile.Mbe_intr_com = false;
            _custProfile.Mbe_is_suspend = false;

            if (chkSVAT.Checked == true)
            {
                _isSVAT = true;
            }
            else
            {
                _isSVAT = false;
            }

            _custProfile.Mbe_is_svat = _isSVAT;

            if (chkVAT.Checked == true)
            {
                _isVAT = true;
            }
            else
            {
                _isVAT = false;
            }
            _custProfile.Mbe_is_tax = _isVAT;
            _custProfile.Mbe_mob = txtMob.Text.Trim();
            _custProfile.Mbe_name = txtName.Text.Trim();
            _custProfile.Mbe_nic = txtNIC.Text.Trim();
            _custProfile.Mbe_oth_id_no = null;
            _custProfile.Mbe_oth_id_tp = null;
            _custProfile.Mbe_pc_stus = "GOOD";
            _custProfile.Mbe_postal_cd = txtPerPostal.Text.Trim();
            _custProfile.Mbe_pp_no = txtPP.Text.Trim();
            _custProfile.Mbe_province_cd = txtPerProvince.Text.Trim();
            _custProfile.Mbe_sex = cmbSex.Text;
            _custProfile.Mbe_sub_tp = null;
            _custProfile.Mbe_svat_no = txtSVATReg.Text.Trim();

            if (chkVatEx.Checked == true)
            {
                _TaxEx = true;
            }
            else
            {
                _TaxEx = false;
            }
            _custProfile.Mbe_tax_ex = _TaxEx;
            _custProfile.Mbe_tax_no = txtVatreg.Text.Trim();
            _custProfile.Mbe_tel = txtPerPhone.Text.Trim();
            _custProfile.Mbe_town_cd = txtPreTown.Text.Trim();
            _custProfile.Mbe_tp = "C";
            _custProfile.Mbe_wr_add1 = txtWorkAdd1.Text.Trim();
            _custProfile.Mbe_wr_add2 = txtWorkAdd2.Text.Trim();
            _custProfile.Mbe_wr_com_name = txtWorkName.Text.Trim();
            _custProfile.Mbe_wr_country_cd = null;
            _custProfile.Mbe_wr_dept = txtWorkDept.Text.Trim();
            _custProfile.Mbe_wr_designation = txtWorkDesig.Text.Trim();
            _custProfile.Mbe_wr_distric_cd = null;
            _custProfile.Mbe_wr_email = txtWorkEmail.Text.Trim();
            _custProfile.Mbe_wr_fax = txtWorkFax.Text.Trim();
            _custProfile.Mbe_wr_proffesion = null;
            _custProfile.Mbe_wr_province_cd = null;
            _custProfile.Mbe_wr_tel = txtWorkPhone.Text.Trim();
            _custProfile.Mbe_wr_town_cd = null;
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Clear_Data();
        }

        private void chkVat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (chkVAT.Checked == true)
                {
                    txtVatreg.Focus();
                }
                else
                {
                    btnCreate.Focus();
                }
            }
        }

        private void txtVatreg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkSVAT.Focus();
            }
        }


        private void chkVAT_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVAT.Checked == true)
            {
                txtVatreg.Enabled = true;
                chkVatEx.Enabled = true;
                chkSVAT.Enabled = true;
                txtVatreg.Focus();
            }
            else
            {
                chkVatEx.Enabled = false;
                chkSVAT.Enabled = false;
                txtSVATReg.Text = "";
                txtVatreg.Text = "";
                txtSVATReg.Enabled = false;
                txtVatreg.Enabled = false;
            }
        }

        private void chkSVAT_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSVAT.Checked == true)
            {
                txtSVATReg.Enabled = true;
                txtSVATReg.Focus();
            }
            else
            {
                txtSVATReg.Text = "";
                txtSVATReg.Enabled = false;
            }
        }

        private void chkSVAT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (chkSVAT.Checked == true)
                {
                    txtSVATReg.Focus();
                }
                else
                {
                    btnCreate.Focus();
                }
            }
        }

        private void txtSVATReg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCreate.Focus();
            }

        }

        private void cmbTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtName.TextLength <= 5)
            {
                txtName.Text = cmbTitle.Text;
            }
        }

        private void btnAdvanceUpdate_Click(object sender, EventArgs e)
        {
            string _cusCode = "";
            Int32 _effect = 0;

            if (txtNIC.Text == "" && txtBR.Text == "" && txtPP.Text == "" && txtDL.Text == "" && txtMob.Text == "")
            {
                MessageBox.Show("One of required information not enterd.", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNIC.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please enter name of customer", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return;
            }

            Collect_Cust();

            if (_isExsit == false)
            {
                _effect = CHNLSVC.Sales.SaveBusinessEntityDetail(_custProfile, _account, _busInfoList, out _cusCode);
            }
            else
            {
                _cusCode = txtCusCode.Text.Trim();
                _effect = CHNLSVC.Sales.UpdateBizEntity_OnPermission(_custProfile);
            }

            if (_effect == 1)
            {
                if (_isExsit == false)
                {
                    MessageBox.Show("New customer created. Customer Code : " + _cusCode, "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Exsiting customer updated.", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (_isFromOther == true)
            {
                HP.AccountCreation _acc = new HP.AccountCreation();
                obj_TragetTextBox.Text = _cusCode;
                this.Close();
            }
            this.Clear_Data();
        }

       

       
    }
}
