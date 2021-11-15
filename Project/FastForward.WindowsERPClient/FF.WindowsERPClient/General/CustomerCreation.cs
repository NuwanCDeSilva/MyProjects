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
using System.Runtime.InteropServices;
using CrystalDecisions.Windows.Forms.Internal.Win32;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using System.IO;
namespace FF.WindowsERPClient.General
{
    public partial class CustomerCreation : Base
    {
        private Service_Chanal_parameter _scvParam = null;
        private MasterBusinessEntity _custProfile = new MasterBusinessEntity();
        private GroupBussinessEntity _custGroup = new GroupBussinessEntity();
        private CustomerAccountRef _account = new CustomerAccountRef();
        private List<MasterBusinessEntityInfo> _busInfoList = new List<MasterBusinessEntityInfo>();

        private Boolean _isExsit = false;
        private Boolean _isGroup = false;

        public Boolean _isFromOther = false;
        public TextBox obj_TragetTextBox;
        private Boolean _isUpdate = false;
        private string _err = string.Empty;
        public string _saleType = string.Empty; //kapila 13/10/2016
        private Int32 _eff = 0;
        string _cusCode = "";
        string NationalityCd = "SL";
        //constructor added by Shani 18-02-2013
        public CustomerCreation(string userID, string UserComCode, string _OrgPC)
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

            //07-MAR-2015 Nadeeka
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11062))
            {
                btnAdvanceUpdate.Visible = true;

            }
            //else
            //{
            //    btnAdvanceUpdate.Visible = false;
            //}
        }
        public CustomerCreation()
        {
            InitializeComponent();
        }

        private void CustomerCreation_Load(object sender, EventArgs e)
        {
            _scvParam = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            Clear_Data();
            cmbPrefLang.SelectedIndex = 0;
            LoadLanguage();
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
                case CommonUIDefiniton.SearchUserControlType.Nationality:
                    {
                        paramsText.Append(seperator);
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
            txtNationality.Text = string.Empty;
            NationalityCd = "SL";

            _custProfile = new MasterBusinessEntity();
            _account = new CustomerAccountRef();
            _busInfoList = new List<MasterBusinessEntityInfo>();
            _custGroup = new GroupBussinessEntity();
            _isUpdate = false;
            //kapila 18/2/2015 req. by christina
            txtInit.Text = "";
            txtFname.Text = "";
            txtSName.Text = "";


            txtDL.ReadOnly = false;
            txtPP.ReadOnly = false;
            txtNIC.ReadOnly = false;
            txtBR.ReadOnly = false;
            txtMob.ReadOnly = false;

            chkMail.Checked = false;
            txtCusCode.Text = "";
            txtCusCode.Enabled = true;
            _isExsit = false;
            _isGroup = false;
            cmbType.Text = "INDIVIDUAL";
            cmbPrefLang.Text = "ENGLISH";
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

            chkSVAT.Enabled = false;
            chkVatEx.Enabled = false;
            chkVatEx.Enabled = true;

            txtVatreg.Text = "";
            txtSVATReg.Text = "";
            txtVatreg.Enabled = false;
            txtSVATReg.Enabled = false;
            tbAdd.SelectedTab = tabPage1;
            btnCreate.Enabled = true;
            btnCreate.Text = "Create";
            cmbType.Focus();
            txtSerNo.Text = "";
            txtSerNo.Enabled = false;
            chkisloyalty.Enabled = false;
            chkisloyalty.Checked = false;
        }


        private void dobGeneration()
        {// NADEEKA 18-12-2014
            String nic_ = txtNIC.Text.Trim().ToUpper();
            char[] nicarray = nic_.ToCharArray();
            string thirdNum = "";
            if (nic_.Length == 10)     //kapila 14/1/2016
                thirdNum = (nicarray[2]).ToString();
            else if (nic_.Length == 12)
                thirdNum = (nicarray[4]).ToString();

            //---------DOB generation----------------------
            string threechar = "";
            if (nic_.Length == 10)
                threechar = (nicarray[2]).ToString() + (nicarray[3]).ToString() + (nicarray[4]).ToString();
            else if (nic_.Length == 12)
                threechar = (nicarray[4]).ToString() + (nicarray[5]).ToString() + (nicarray[6]).ToString();

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
            Int32 dobYear = 0;
            if (nic_.Length == 10)
                dobYear = 1900 + Convert.ToInt32((nicarray[0].ToString())) * 10 + Convert.ToInt32((nicarray[1].ToString()));
            else if (nic_.Length == 12)
                dobYear = 1900 + Convert.ToInt32((nicarray[2].ToString())) * 10 + Convert.ToInt32((nicarray[3].ToString()));

            try
            {
                DateTime dob = new DateTime(dobYear, dobMon, bornDate);
                dtpDOB.Value = dob.Date;

            }
            catch (Exception ex)
            {
            }
        }



        public bool IsValidNIC(string nic)
        {
            //THIS IS CASE SENSITIVE IF U DONT WONT CASE SENSITIVE PLEASE USE FOLLOWING CODE
            // email = email.ToLower();
            string pattern = "";
            if (nic.Length == 10)     //kapila 14/1/2016
                pattern = @"^[0-9]{9}[V,X]{1}$";
            else if (nic.Length == 12)
                pattern = @"^[0-9]{12}$";
            else
                return false;

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

                        txtPreTown.Text = txtPerTown.Text; // Added by Nadeeka 29-05-2015 Requested By Dilanda

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
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtCusCode.Select();
            }
            BindLoyalatyDetails(txtCusCode.Text.Trim());
        }

        private void txtNIC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBR.Focus();
                BindLoyalatyDetails(txtCusCode.Text);
            }
        }

        private void txtBR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPP.Focus();
                BindLoyalatyDetails(txtCusCode.Text);
            }
        }

        private void txtPP_keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMob.Focus();
                BindLoyalatyDetails(txtCusCode.Text);
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
                dtpDOB.Focus();
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbAdd.SelectedTab = tabPage1;
                txtPerAdd1.Focus();
            }
        }

        private void dtpDOB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //tbAdd.SelectedTab = tabPage1;
                //txtPerAdd1.Focus();
                //txtInit.Focus();
                txtNationality.Focus();
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
                    MessageBox.Show("Please enter a valid email address", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                if (_isValid == false)
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
                btnCreate.Enabled = true;
                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                Boolean _isValid = IsValidMobileOrLandNo(txtMob.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Invalid mobile number.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMob.Text = "";
                    txtMob.Focus();
                    return;
                }

                string _current_cust = string.Empty;
                Int32 _cnt = 0;
                //List<MasterBusinessEntity> _fk = CHNLSVC.Sales.GetCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtMob.Text, "C");
                List<MasterBusinessEntity> _fk = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtMob.Text, "C");
                if (_fk != null && _fk.Count > 0)
                {
                    _cnt = _fk.Count;
                    _current_cust = _fk[0].Mbe_cd;
                    //if (_fk.Count > 1)
                    //{
                    //    MessageBox.Show("There are " + _fk.Count + " number of customers available for the selected mobile.", "Customers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    txtMob.Text = "";
                    //    txtMob.Focus();
                    //    return;
                    //}
                }
                Int32 _attemt = 0;
                if (_current_cust != txtCusCode.Text || _cnt > 1)
                {
                    //_masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtMob.Text, "C");
                    _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtMob.Text, "C");
                    if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_act == true)
                    {
                        _isUpdate = true;  //kapila 15/3/2017 to stop duplicate customer creation
                        txtDL.ReadOnly = true;
                        txtPP.ReadOnly = true;
                        txtNIC.ReadOnly = true;
                        txtBR.ReadOnly = true;
                        txtMob.ReadOnly = true;
                        Disableloyaltymembership();
                        #region Mobileno
                        if (_isUpdate == true)
                        {
                            if (MessageBox.Show("Customer already exists for this  mobile number, Do you want to recall the existing  customer details ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {

                                _isExsit = true;
                                btnCreate.Text = "Update";
                                LoadCustProf(_masterBusinessCompany);
                                BindLoyalatyDetails(txtCusCode.Text.Trim());
                                // Nadeeka

                            }
                            else
                            {
                                Int32 cutstVeri = CHNLSVC.General.GetCutomerValidationCode(txtMob.Text);

                                Int32 _enteredCode;
                                string _enteredValue = "";        //kapila 13/7/2017
                            err:
                                _enteredValue = Interaction.InputBox("Pls enter valid verification code", "verification code", "", 400, 100).ToString();
                                if (string.IsNullOrEmpty(_enteredValue))
                                {
                                    txtMob.Text = "";
                                    txtMob.Focus();
                                    return;
                                }
                                _enteredCode = Convert.ToInt32(_enteredValue);

                                if (cutstVeri == _enteredCode)
                                {
                                    _eff = CHNLSVC.General.UpdateCutomerMobile(txtMob.Text, "MOB", out _err);


                                    if (_eff == -1)
                                    {
                                        MessageBox.Show(_err, "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    //btnCreate_Click(null, null);
                                    //kapila 16/3/2017
                                    txtDL.ReadOnly = false;
                                    txtPP.ReadOnly = false;
                                    txtNIC.ReadOnly = false;
                                    txtBR.ReadOnly = false;
                                    txtMob.ReadOnly = false;
                                }
                                else
                                {
                                    MessageBox.Show("Invalid varification code.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    if (_attemt == 5)
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        _attemt = _attemt + 1;
                                        goto err;

                                    }
                                }


                            }
                        }
                        #endregion


                        else if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_act == false)
                        {
                            MessageBox.Show("Customer is inactivated.Please contact accounts dept.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            btnCreate.Enabled = false;
                            LoadCustProf(_masterBusinessCompany);
                        }
                    }
                }
                else
                {
                    //kapila 15/7/2017 - issue of new customer code creation for the same NIC/mobile in dif company
                    txtName.Enabled = true;
                    txtFname.Enabled = true;
                    txtSName.Enabled = true;

                    List<MasterBusinessEntity> _custListtmp = CHNLSVC.Sales.GetCustomerByKeys(null, txtNIC.Text.Trim(), "", "", "", "", 1);
                    if (_custListtmp != null && _custListtmp.Count >= 1 && txtNIC.Text.ToUpper() != "N/A")
                    {
                        _isExsit = true;
                        btnCreate.Text = "Update";
                        LoadCustProfbyList(_custListtmp);
                    }
                    else
                    {

                        _custListtmp = null;
                        _custListtmp = CHNLSVC.Sales.GetCustomerByKeys(null, "", txtMob.Text.Trim(), "", "", "", 2);
                        if (_custListtmp != null && _custListtmp.Count >= 1 && txtMob.Text.ToUpper() != "N/A")
                        {
                            _isExsit = true;
                            btnCreate.Text = "Update";
                            LoadCustProfbyList(_custListtmp);
                            Disableloyaltymembership();
                        }
                    }

                    if (_isExsit == true)
                    {
                        if (_isUpdate == true)
                        { }
                        else
                        {
                            string Mob = txtMob.Text.Trim().ToUpper();
                            MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            // LoadCustProf(cust_null);
                            txtMob.Text = Mob;
                        }
                        //Check the group level
                        GroupBussinessEntity _grupProf = GetbyMobGrup(txtMob.Text.Trim().ToUpper());
                        if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                        {
                            LoadCustProfByGrup(_grupProf);
                            _isGroup = true;
                            _isExsit = false;
                            btnCreate.Text = "Create";
                            return;
                        }
                        else
                        {
                            _isGroup = false;
                        }
                        _isExsit = false;
                        btnCreate.Text = "Create";
                    }

                }


            }
            BindLoyalatyDetails(txtCusCode.Text);
        }

        private void txtNIC_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNIC.Text))
            {
                btnCreate.Enabled = true;
                Boolean _isValid = IsValidNIC(txtNIC.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Please enter a valid NIC", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNIC.Text = "";
                    txtNIC.Focus();
                    return;
                }
                else
                {
                    //check multiple Add By Chamal 24/04/2014
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, txtNIC.Text.Trim(), "", "", "", "", 1);
                    if (_custList != null && _custList.Count > 1 && txtNIC.Text.ToUpper() != "N/A")
                    {
                        string _custNIC = "Duplicate customers found!\n";
                        foreach (var _nicCust in _custList)
                        {
                            _custNIC = _custNIC + _nicCust.Mbe_cd.ToString() + " | " + _nicCust.Mbe_name.ToString() + "\n";
                        }
                        // _custNIC = _custNIC + "\nPlease contact accounts department";
                        MessageBox.Show(_custNIC, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtNIC.Text = "";
                        txtNIC.Focus();
                        return;
                    }

                    MasterBusinessEntity custProf = GetbyNIC(txtNIC.Text.Trim().ToUpper());
                    if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                    {
                        _isExsit = true;
                        btnCreate.Text = "Update";
                        dobGeneration();
                        LoadCustProf(custProf);
                        txtDL.ReadOnly = true;
                        txtPP.ReadOnly = true;
                        txtNIC.ReadOnly = true;
                        txtBR.ReadOnly = true;
                        txtMob.ReadOnly = true;
                        txtSerNo.Enabled = false;
                        txtSerNo.Text = "";
                        chkisloyalty.Checked = false;
                        chkisloyalty.Enabled = false;
                        BindLoyalatyDetails(txtCusCode.Text);


                        Int32 _attemt = 0;
                        //#region NIC
                        //if (_isUpdate == true)
                        //{
                        //    if (MessageBox.Show("Customer already exists for this  NIC number, Do you want to recall the existing  customer details ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        //    {

                        //        _isExsit = true;
                        //        btnCreate.Text = "Update";
                        //        dobGeneration();
                        //        LoadCustProf(custProf);
                        //        // Nadeeka

                        //    }
                        //    else
                        //    {
                        //        Int32 cutstVeri = CHNLSVC.General.GetCutomerValidationCode(txtMob.Text);

                        //        Int32 _enteredCode;
                        //    err:
                        //        _enteredCode = Convert.ToInt32(Interaction.InputBox("Pls enter valid verification code", "verification code", "", 400, 100));

                        //        if (cutstVeri == _enteredCode)
                        //        {
                        //            CHNLSVC.General.UpdateCutomerMobile(txtNIC.Text.Trim().ToUpper(), "NIC", out _err);
                        //            if (_eff == -1)
                        //            {
                        //                MessageBox.Show(_err, "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //                return;
                        //            }
                        //            btnCreate_Click(null, null);
                        //        }
                        //        else
                        //        {
                        //            MessageBox.Show("Invalid varification code.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //            if (_attemt == 5)
                        //            {
                        //                return;
                        //            }
                        //            else
                        //            {
                        //                _attemt = _attemt + 1;
                        //                goto err;

                        //            }
                        //        }


                        //    }
                        //}
                        //#endregion




                    }
                    else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                    {
                        MessageBox.Show("Customer is inactivated.Please contact accounts dept.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnCreate.Enabled = false;
                        LoadCustProf(custProf);
                    }
                    else//added on 01/10/2012
                    {
                        //kapila 15/7/2017 - issue of new customer code creation for the same NIC/mobile in dif company
                        txtName.Enabled = true;
                        txtFname.Enabled = true;
                        txtSName.Enabled = true;


                        List<MasterBusinessEntity> _custListtmp = CHNLSVC.Sales.GetCustomerByKeys(null, txtNIC.Text.Trim(), "", "", "", "", 1);
                        if (_custListtmp != null && _custListtmp.Count >= 1 && txtNIC.Text.ToUpper() != "N/A")
                        {
                            _isExsit = true;
                            btnCreate.Text = "Update";
                            LoadCustProfbyList(_custListtmp);
                        }
                        else
                        {
                            _custListtmp = null;
                            _custListtmp = CHNLSVC.Sales.GetCustomerByKeys(null, "", txtMob.Text.Trim(), "", "", "", 2);
                            if (_custListtmp != null && _custListtmp.Count >= 1 && txtMob.Text.ToUpper() != "N/A")
                            {
                                _isExsit = true;
                                btnCreate.Text = "Update";
                                LoadCustProfbyList(_custListtmp);
                            }
                        }

                        if (_isExsit == true)
                        {
                            txtSerNo.Enabled = false;
                            txtSerNo.Text = "";
                            chkisloyalty.Checked = false;
                            chkisloyalty.Enabled = false;
                            string nic = txtNIC.Text.Trim().ToUpper();
                            MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            LoadCustProf(cust_null);
                            txtNIC.Text = nic;
                        }

                        //Check the group level
                        GroupBussinessEntity _grupProf = GetbyNICGrup(txtNIC.Text.Trim().ToUpper());
                        if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                        {
                            LoadCustProfByGrup(_grupProf);
                            _isGroup = true;
                            _isExsit = false;
                            btnCreate.Text = "Create";
                            return;
                        }
                        else
                        {
                            _isGroup = false;
                        }

                        _isExsit = false;
                        btnCreate.Text = "Create";
                        String nic_ = txtNIC.Text.Trim().ToUpper();
                        char[] nicarray = nic_.ToCharArray();

                        string thirdNum = "";
                        if (nic_.Length == 10)     //kapila 14/1/2016
                            thirdNum = (nicarray[2]).ToString();
                        else if (nic_.Length == 12)
                            thirdNum = (nicarray[4]).ToString();

                        if (thirdNum == "5" || thirdNum == "6" || thirdNum == "7" || thirdNum == "8" || thirdNum == "9")
                        {
                            cmbSex.Text = "FEMALE";
                            cmbTitle.Text = "MS.";
                        }
                        else
                        {
                            cmbSex.Text = "MALE";
                            cmbTitle.Text = "MR.";
                        }


                        //---------DOB generation----------------------
                        string threechar = "";
                        if (nic_.Length == 10)
                            threechar = (nicarray[2]).ToString() + (nicarray[3]).ToString() + (nicarray[4]).ToString();
                        else if (nic_.Length == 12)
                            threechar = (nicarray[4]).ToString() + (nicarray[5]).ToString() + (nicarray[6]).ToString();

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
                        Int32 dobYear = 0;
                        if (nic_.Length == 10)
                            dobYear = 1900 + Convert.ToInt32((nicarray[0].ToString())) * 10 + Convert.ToInt32((nicarray[1].ToString()));
                        else if (nic_.Length == 12)
                            dobYear = 1900 + Convert.ToInt32((nicarray[2].ToString())) * 10 + Convert.ToInt32((nicarray[3].ToString()));
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
            BindLoyalatyDetails(txtCusCode.Text.Trim());
        }


        private void txtCusCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCusCode.Text))
            {
                btnCreate.Enabled = true;
                MasterBusinessEntity custProf = GetbyCustCD(txtCusCode.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                {
                    _isExsit = true;
                    btnCreate.Text = "Update";
                    LoadCustProf(custProf);
                }
                else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                {
                    MessageBox.Show("Customer is inactivated.Please contact accounts dept.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnCreate.Enabled = false;
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
                    //Check the group level
                    GroupBussinessEntity _grupProf = GetbyCustCDGrup(txtCusCode.Text.Trim().ToUpper());
                    if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                    {
                        LoadCustProfByGrup(_grupProf);
                        _isGroup = true;
                    }
                    else
                    {
                        _isGroup = false;
                    }

                    _isExsit = false;
                    btnCreate.Text = "Create";
                    if (custProf.Mbe_cd == null)
                    {
                        MessageBox.Show("Invalid customer code.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCusCode.Text = "";
                    }
                }
                //else//added on 01/10/2012
                //{
                //    string nic = txtNIC.Text.Trim().ToUpper();
                //    MasterBusinessEntity cust_null = new MasterBusinessEntity();
                //    LoadCustProf(cust_null);
                //    txtNIC.Text = nic;
                //}
                BindLoyalatyDetails(txtCusCode.Text.Trim());
            }
        }

        private void txtBR_Leave(object sender, EventArgs e)
        {
            string _current_cust = string.Empty;
            // Int32 _cnt = 0;
            if (!string.IsNullOrEmpty(txtBR.Text))
            {
                btnCreate.Enabled = true;
                MasterBusinessEntity custProf = GetbyBrNo(txtBR.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                {
                    _isUpdate = true;  //kapila 15/3/2017 to stop duplicate customer creation
                    _current_cust = custProf.Mbe_cd;

                    txtDL.ReadOnly = true;
                    txtPP.ReadOnly = true;
                    txtNIC.ReadOnly = true;
                    txtBR.ReadOnly = true;
                    txtMob.ReadOnly = true;
                    Int32 _attemt = 0;
                    #region DL
                    if (_current_cust != txtCusCode.Text)
                    {
                        if (_isUpdate == true)
                        {
                            if (MessageBox.Show("Customer already exists for this  BR number, Do you want to recall the existing  customer details ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {

                                _isExsit = true;
                                btnCreate.Text = "Update";
                                LoadCustProf(custProf);
                                // Nadeeka

                            }
                            else
                            {
                                Int32 cutstVeri = CHNLSVC.General.GetCutomerValidationCode(txtMob.Text);

                                Int32 _enteredCode;
                                string _enteredValue = "";  //kapila 13/7/2017
                            err:
                                _enteredValue = Interaction.InputBox("Pls enter valid verification code", "verification code", "", 400, 100).ToString();
                                if (string.IsNullOrEmpty(_enteredValue))
                                {
                                    txtBR.Text = "";
                                    txtBR.Focus();
                                    return;
                                }
                                _enteredCode = Convert.ToInt32(_enteredValue);

                                if (cutstVeri == _enteredCode)
                                {
                                    _eff = CHNLSVC.General.UpdateCutomerMobile(txtBR.Text, "BR", out _err);
                                    if (_eff == -1)
                                    {
                                        MessageBox.Show(_err, "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    //btnCreate_Click(null, null);
                                    //kapila 16/3/2017
                                    txtDL.ReadOnly = false;
                                    txtPP.ReadOnly = false;
                                    txtNIC.ReadOnly = false;
                                    txtBR.ReadOnly = false;
                                    txtMob.ReadOnly = false;
                                }
                                else
                                {
                                    MessageBox.Show("Invalid varification code.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    if (_attemt == 5)
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        _attemt = _attemt + 1;
                                        goto err;

                                    }
                                }


                            }
                        }
                    }
                    #endregion
                }
                else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                {
                    MessageBox.Show("Customer is inactivated.Please contact accounts dept.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnCreate.Enabled = false;
                    LoadCustProf(custProf);
                }
                else
                {
                    if (_isExsit == false)
                    {
                        if (_isUpdate == true)
                        {
                            _isExsit = true;
                            btnCreate.Text = "Update";
                        }
                        else
                        {
                            string BR = txtBR.Text.Trim().ToUpper();
                            MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            LoadCustProf(cust_null);
                            txtBR.Text = BR;
                            //Check the group level
                            GroupBussinessEntity _grupProf = GetbyBrNoGrup(txtBR.Text.Trim().ToUpper());
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
                                LoadCustProfByGrup(_grupProf);
                                _isGroup = true;
                                _isExsit = false;
                                btnCreate.Text = "Create";
                                return;
                            }
                            else
                            {
                                _isGroup = false;
                            }
                            _isExsit = false;
                            btnCreate.Text = "Create";
                        }
                    }

                }
            }
            BindLoyalatyDetails(txtCusCode.Text);
        }

        private void txtPP_Leave(object sender, EventArgs e)
        {
            string _current_cust = string.Empty;
            // Int32 _cnt = 0;
            if (!string.IsNullOrEmpty(txtPP.Text))
            {
                btnCreate.Enabled = true;
                MasterBusinessEntity custProf = GetbyPPno(txtPP.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                {
                    _isUpdate = true;  //kapila 15/3/2017 to stop duplicate customer creation
                    _current_cust = custProf.Mbe_cd;
                    Int32 _attemt = 0;
                    txtDL.ReadOnly = true;
                    txtPP.ReadOnly = true;
                    txtNIC.ReadOnly = true;
                    txtBR.ReadOnly = true;
                    txtMob.ReadOnly = true;
                    #region PP
                    if (_current_cust != txtCusCode.Text)
                    {
                        if (_isUpdate == true)
                        {
                            if (MessageBox.Show("Customer already exists for this  Passport number, Do you want to recall the existing  customer details ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {

                                _isExsit = true;
                                btnCreate.Text = "Update";
                                LoadCustProf(custProf);
                                // Nadeeka

                            }
                            else
                            {
                                Int32 cutstVeri = CHNLSVC.General.GetCutomerValidationCode(txtMob.Text);

                                Int32 _enteredCode;
                                string _enteredValue = "";   //kapila 13/7/2017
                            err:
                                _enteredValue = Interaction.InputBox("Pls enter valid verification code", "verification code", "", 400, 100).ToString();
                                if (string.IsNullOrEmpty(_enteredValue))
                                {
                                    txtPP.Text = "";
                                    txtPP.Focus();
                                    return;
                                }
                                _enteredCode = Convert.ToInt32(_enteredValue);

                                if (cutstVeri == _enteredCode)
                                {
                                    _eff = CHNLSVC.General.UpdateCutomerMobile(txtPP.Text.Trim().ToUpper(), "PP", out _err);
                                    if (_eff == -1)
                                    {
                                        MessageBox.Show(_err, "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    //btnCreate_Click(null, null);
                                    //kapila 16/3/2017
                                    txtDL.ReadOnly = false;
                                    txtPP.ReadOnly = false;
                                    txtNIC.ReadOnly = false;
                                    txtBR.ReadOnly = false;
                                    txtMob.ReadOnly = false;
                                }
                                else
                                {
                                    MessageBox.Show("Invalid varification code.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    if (_attemt == 5)
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        _attemt = _attemt + 1;
                                        goto err;

                                    }
                                }


                            }
                        }
                    }
                    #endregion



                }
                else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                {
                    MessageBox.Show("Customer is inactivated.Please contact accounts dept.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnCreate.Enabled = false;
                    LoadCustProf(custProf);
                }
                else
                {
                    if (_isExsit == true)
                    {
                        if (_isUpdate == true)
                        {
                            btnCreate.Text = "Update";

                        }

                        else
                        {
                            string PP = txtPP.Text.Trim().ToUpper();
                            MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            LoadCustProf(cust_null);
                            txtPP.Text = PP;

                            //Check the group level
                            GroupBussinessEntity _grupProf = GetbyPPnoGrup(txtPP.Text.Trim().ToUpper());
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
                                LoadCustProfByGrup(_grupProf);
                                _isGroup = true;
                                _isExsit = false;
                                btnCreate.Text = "Create";
                                return;
                            }
                            else
                            {
                                _isGroup = false;
                            }
                            _isExsit = false;
                            btnCreate.Text = "Create";
                        }
                    }

                }
            }
            BindLoyalatyDetails(txtCusCode.Text);
        }

        private void txtDL_Leave(object sender, EventArgs e)
        {
            string _current_cust = string.Empty;
            // Int32 _cnt = 0;
            if (!string.IsNullOrEmpty(txtDL.Text))
            {
                btnCreate.Enabled = true;
                MasterBusinessEntity custProf = GetbyDL(txtDL.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                {
                    _isUpdate = true;  //kapila 15/3/2017 to stop duplicate customer creation
                    _current_cust = custProf.Mbe_cd;
                    txtDL.ReadOnly = true;
                    txtPP.ReadOnly = true;
                    txtNIC.ReadOnly = true;
                    txtBR.ReadOnly = true;
                    txtMob.ReadOnly = true;
                    Int32 _attemt = 0;
                    #region DL
                    if (_current_cust != txtCusCode.Text)
                    {
                        if (_isUpdate == true)
                        {
                            if (MessageBox.Show("Customer already exists for this  DL number, Do you want to recall the existing  customer details ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {

                                _isExsit = true;
                                btnCreate.Text = "Update";
                                LoadCustProf(custProf);
                                // Nadeeka

                            }
                            else
                            {
                                Int32 cutstVeri = CHNLSVC.General.GetCutomerValidationCode(txtMob.Text);

                                Int32 _enteredCode;
                                string _enteredValue = "";      //kapila 13/7/2017
                            err:
                                _enteredValue = Interaction.InputBox("Pls enter valid verification code", "verification code", "", 400, 100).ToString();
                                if (string.IsNullOrEmpty(_enteredValue))
                                {
                                    txtDL.Text = "";
                                    txtDL.Focus();
                                    return;
                                }
                                _enteredCode = Convert.ToInt32(_enteredValue);

                                if (cutstVeri == _enteredCode)
                                {
                                    _eff = CHNLSVC.General.UpdateCutomerMobile(txtDL.Text.Trim().ToUpper(), "DL", out _err);
                                    if (_eff == -1)
                                    {
                                        MessageBox.Show(_err, "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    //btnCreate_Click(null, null);
                                    //kapila 16/3/2017
                                    txtDL.ReadOnly = false;
                                    txtPP.ReadOnly = false;
                                    txtNIC.ReadOnly = false;
                                    txtBR.ReadOnly = false;
                                    txtMob.ReadOnly = false;
                                }
                                else
                                {
                                    MessageBox.Show("Invalid varification code.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    if (_attemt == 5)
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        _attemt = _attemt + 1;
                                        goto err;

                                    }
                                }


                            }
                        }
                    }
                    #endregion

                }
                else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                {
                    MessageBox.Show("Customer is inactivated.Please contact accounts dept.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnCreate.Enabled = false;
                    LoadCustProf(custProf);


                }
                else
                {
                    if (_isExsit == true)
                    {


                        if (_isUpdate == true)
                        {
                            _isExsit = true;
                            btnCreate.Text = "Update";
                        }
                        else
                        {
                            string DL = txtDL.Text.Trim().ToUpper();
                            MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            LoadCustProf(cust_null);
                            txtDL.Text = DL;
                            //Check the group level
                            GroupBussinessEntity _grupProf = GetbyDLGrup(txtDL.Text.Trim().ToUpper());
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
                                LoadCustProfByGrup(_grupProf);
                                _isGroup = true;
                                _isExsit = false;
                                btnCreate.Text = "Create";
                                return;
                            }
                            else
                            {
                                _isGroup = false;
                            }
                            _isExsit = false;
                            btnCreate.Text = "Create";
                        }
                    }

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

        #region LoadCustByGroup
        public GroupBussinessEntity GetbyCustCDGrup(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(custCD, null, null, null, null, null);
        }
        public GroupBussinessEntity GetbyNICGrup(string nic)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(null, nic, null, null, null, null);
        }
        public GroupBussinessEntity GetbyDLGrup(string dl)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, dl, null, null, null);
        }
        public GroupBussinessEntity GetbyPPnoGrup(string ppno)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, null, ppno, null, null);
        }
        public GroupBussinessEntity GetbyBrNoGrup(string brNo)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, null, null, brNo, null);
        }
        public GroupBussinessEntity GetbyMobGrup(string mobNo)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, null, null, null, mobNo);
        }
        #endregion LoadCustByGroup

        public void LoadCustProfByGrup(GroupBussinessEntity _cust)
        {

            txtNIC.Text = _cust.Mbg_nic;
            txtPP.Text = _cust.Mbg_pp_no;
            txtBR.Text = _cust.Mbg_br_no;
            txtCusCode.Text = _cust.Mbg_cd;
            txtDL.Text = _cust.Mbg_dl_no;
            txtMob.Text = _cust.Mbg_mob;
            cmbSex.Text = _cust.Mbg_sex;
            txtName.Text = _cust.Mbg_name;
            cmbTitle.Text = _cust.Mbg_tit;
            txtFname.Text = _cust.Mbg_fname;
            txtSName.Text = _cust.Mbg_sname;
            txtInit.Text = _cust.Mbg_ini;

            if (_cust.Mbg_dob.Date > Convert.ToDateTime("01-01-1000").Date)
            {
                dtpDOB.Value = Convert.ToDateTime(_cust.Mbg_dob).Date;// Convert.ToString(cust.Mbe_dob.Date); 
            }
            else
            {

            }
            txtPerAdd1.Text = _cust.Mbg_add1;
            txtPerAdd2.Text = _cust.Mbg_add2;
            txtPerTown.Text = _cust.Mbg_town_cd;
            txtPerPostal.Text = _cust.Mbg_postal_cd;
            txtPerCountry.Text = _cust.Mbg_country_cd;
            txtPerDistrict.Text = _cust.Mbg_distric_cd;
            txtPerProvince.Text = _cust.Mbg_province_cd;
            txtPerPhone.Text = _cust.Mbg_tel;
            txtPerEmail.Text = _cust.Mbg_email;

            txtPreAdd1.Text = _cust.Mbg_add1;
            txtPreAdd2.Text = _cust.Mbg_add2;
            txtPreTown.Text = _cust.Mbg_town_cd;
            txtPrePostal.Text = _cust.Mbg_postal_cd;
            txtPreCountry.Text = _cust.Mbg_country_cd;
            txtPreDistrict.Text = _cust.Mbg_distric_cd;
            txtPreProvince.Text = _cust.Mbg_province_cd;
            txtPrePhone.Text = _cust.Mbg_tel;

            txtWorkAdd1.Text = "";
            txtWorkAdd2.Text = "";
            txtWorkName.Text = "";
            txtWorkDept.Text = "";
            txtWorkDesig.Text = "";
            txtWorkEmail.Text = "";
            txtWorkFax.Text = "";
            txtWorkPhone.Text = "";

            chkVAT.Checked = false;
            chkVatEx.Checked = false;
            chkSVAT.Checked = false;
            txtVatreg.Text = "";
            txtSVATReg.Text = "";




            if (string.IsNullOrEmpty(txtInit.Text))
            {
                txtInit.Enabled = true;
            }
            else
            {
                txtInit.Enabled = false;
            }
            if (string.IsNullOrEmpty(txtFname.Text))
            {
                txtFname.Enabled = true;
            }
            else
            {
                txtFname.Enabled = false;
            }
            if (string.IsNullOrEmpty(txtSName.Text))
            {
                txtSName.Enabled = true;
            }
            else
            {
                txtSName.Enabled = false;
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                txtName.Enabled = true;
            }
            else
            {
                txtName.Enabled = false;
            }
            //------------------------------------------

            txtCusCode.Enabled = false;


        }
        private DataTable _tbl = null;
        private void LoadLanguage()
        {
            _tbl = CHNLSVC.General.get_Language();
            cmbPrefLang.DataSource = _tbl;
            cmbPrefLang.DisplayMember = "mla_desc";
            cmbPrefLang.ValueMember = "mla_cd";
        }

        private void LoadCustProfbyList(List<MasterBusinessEntity> _listCust)
        {
            cmbType.Text = _listCust[0].Mbe_cate;
            txtNIC.Text = _listCust[0].Mbe_nic;
            txtPP.Text = _listCust[0].Mbe_pp_no;
            txtBR.Text = _listCust[0].Mbe_br_no;
            txtCusCode.Text = _listCust[0].Mbe_cd;
            txtDL.Text = _listCust[0].Mbe_dl_no;
            txtMob.Text = _listCust[0].Mbe_mob;
            chkSMS.Checked = _listCust[0].Mbe_agre_send_sms;
            //------------------------------------------
            cmbSex.Text = _listCust[0].Mbe_sex;
            txtName.Text = _listCust[0].Mbe_name;

            String nic_ = txtNIC.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(nic_))
            {
                char[] nicarray = nic_.ToCharArray();
                string thirdNum = "";
                if (nic_.Length == 10)     //kapila 14/1/2016
                    thirdNum = (nicarray[2]).ToString();
                else if (nic_.Length == 12)
                    thirdNum = (nicarray[4]).ToString();
                if (thirdNum == "5" || thirdNum == "6" || thirdNum == "7" || thirdNum == "8" || thirdNum == "9")
                {
                    cmbTitle.Text = "MS.";
                }
                else
                {
                    cmbTitle.Text = "MR.";
                }
            }

            if (_listCust[0].Mbe_dob.Date > Convert.ToDateTime("01-01-1000").Date)
            {
                dtpDOB.Value = Convert.ToDateTime(_listCust[0].Mbe_dob).Date;// Convert.ToString(_listCust[0].Mbe_dob.Date); 
            }
            else
            {

            }
            //-------------------------------------------

            txtPerAdd1.Text = _listCust[0].Mbe_add1;
            txtPerAdd2.Text = _listCust[0].Mbe_add2;
            txtPerTown.Text = _listCust[0].Mbe_town_cd;
            txtPerPostal.Text = _listCust[0].Mbe_postal_cd;
            txtPerCountry.Text = _listCust[0].Mbe_country_cd;
            txtPerDistrict.Text = _listCust[0].Mbe_distric_cd;
            txtPerProvince.Text = _listCust[0].Mbe_province_cd;
            txtPerPhone.Text = _listCust[0].Mbe_tel;
            txtPerEmail.Text = _listCust[0].Mbe_email;

            txtPreAdd1.Text = _listCust[0].Mbe_cr_add1;
            txtPreAdd2.Text = _listCust[0].Mbe_cr_add2;
            txtPreTown.Text = _listCust[0].Mbe_cr_town_cd;
            txtPrePostal.Text = _listCust[0].Mbe_cr_postal_cd;
            txtPreCountry.Text = _listCust[0].Mbe_cr_country_cd;
            txtPreDistrict.Text = _listCust[0].Mbe_cr_distric_cd;
            txtPreProvince.Text = _listCust[0].Mbe_cr_province_cd;
            txtPrePhone.Text = _listCust[0].Mbe_cr_tel;

            txtWorkAdd1.Text = _listCust[0].Mbe_wr_add1;
            txtWorkAdd2.Text = _listCust[0].Mbe_wr_add2;
            txtWorkName.Text = _listCust[0].Mbe_wr_com_name;
            txtWorkDept.Text = _listCust[0].Mbe_wr_dept;
            txtWorkDesig.Text = _listCust[0].Mbe_wr_designation;
            txtWorkEmail.Text = _listCust[0].Mbe_wr_email;
            txtWorkFax.Text = _listCust[0].Mbe_wr_fax;
            txtWorkPhone.Text = _listCust[0].Mbe_wr_tel;

            chkVAT.Checked = _listCust[0].Mbe_is_tax;
            chkVatEx.Checked = _listCust[0].Mbe_tax_ex;
            chkSVAT.Checked = _listCust[0].Mbe_is_svat;
            txtVatreg.Text = _listCust[0].Mbe_tax_no;
            txtSVATReg.Text = _listCust[0].Mbe_svat_no;
            txtInit.Text = _listCust[0].MBE_INI;
            txtFname.Text = _listCust[0].MBE_FNAME;
            txtSName.Text = _listCust[0].MBE_SNAME;
            // Nadeeka 15-12-2014
            chkMail.Checked = _listCust[0].Mbe_agre_send_email;


            if (string.IsNullOrEmpty(_listCust[0].Mbe_cust_lang))
            {
                cmbPrefLang.SelectedValue = "E";

            }
            else
            {
                cmbPrefLang.SelectedValue = _listCust[0].Mbe_cust_lang;
            }

            if (string.IsNullOrEmpty(txtInit.Text))
            {
                txtInit.Enabled = true;
            }
            else
            {
                txtInit.Enabled = false;
            }
            if (string.IsNullOrEmpty(txtFname.Text))
            {
                txtFname.Enabled = true;
            }
            else
            {
                txtFname.Enabled = false;
            }
            if (string.IsNullOrEmpty(txtSName.Text))
            {
                txtSName.Enabled = true;
            }
            else
            {
                txtSName.Enabled = false;
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                txtName.Enabled = true;
            }
            else
            {
                txtName.Enabled = false;
            }
            //------------------------------------------

            txtCusCode.Enabled = false;
            BindLoyalatyDetails(txtCusCode.Text); //Tharanga 2017/07/26

            //if (_listCust[0].Mbe_cd == "" || _listCust[0].Mbe_cd == null)
            //{
            //    txtNicNo.Text = typed_nic;
            //    txtPassportNo.Text = typed_ppno;
            //    txtDLno.Text = typed_dl;
            //    txtBrNo.Text = typed_br;
            //}


        }
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

            String nic_ = txtNIC.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(nic_))
            {
                char[] nicarray = nic_.ToCharArray();
                string thirdNum = "";
                if (nic_.Length == 10)     //kapila 14/1/2016
                    thirdNum = (nicarray[2]).ToString();
                else if (nic_.Length == 12)
                    thirdNum = (nicarray[4]).ToString();
                if (thirdNum == "5" || thirdNum == "6" || thirdNum == "7" || thirdNum == "8" || thirdNum == "9")
                {
                    cmbTitle.Text = "MS.";
                }
                else
                {
                    cmbTitle.Text = "MR.";
                }
            }

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
            txtInit.Text = cust.MBE_INI;
            txtFname.Text = cust.MBE_FNAME;
            txtSName.Text = cust.MBE_SNAME;
            // Nadeeka 15-12-2014
            chkMail.Checked = cust.Mbe_agre_send_email;


            if (string.IsNullOrEmpty(cust.Mbe_cust_lang))
            {
                cmbPrefLang.SelectedValue = "E";

            }
            else
            {
                cmbPrefLang.SelectedValue = cust.Mbe_cust_lang;
            }

            if (string.IsNullOrEmpty(txtInit.Text))
            {
                txtInit.Enabled = true;
            }
            else
            {
                txtInit.Enabled = false;
            }
            if (BaseCls.GlbUserComCode != "AAL")    //kapila 29/7/2017  for service centers
            {
                if (string.IsNullOrEmpty(txtFname.Text))
                {
                    txtFname.Enabled = true;
                }
                else
                {
                    txtFname.Enabled = false;
                }
                if (string.IsNullOrEmpty(txtSName.Text))
                {
                    txtSName.Enabled = true;
                }
                else
                {
                    txtSName.Enabled = false;
                }
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    txtName.Enabled = true;
                }
                else
                {
                    txtName.Enabled = false;
                }
            }
            //------------------------------------------

            txtCusCode.Enabled = false;
            BindLoyalatyDetails(txtCusCode.Text); //Tharanga 2017/07/26
            //if (cust.Mbe_cd == "" || cust.Mbe_cd == null)
            //{
            //    txtNicNo.Text = typed_nic;
            //    txtPassportNo.Text = typed_ppno;
            //    txtDLno.Text = typed_dl;
            //    txtBrNo.Text = typed_br;
            //}

            if (cust != null && (!string.IsNullOrEmpty(cust.Mbe_nationality)))
            {
                LoadNationality(cust.Mbe_nationality);
            }            

        }



        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkisloyalty.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtSerNo.Text))
                    {
                        MessageBox.Show("Please enter Loyalty Membership Number.", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSerNo.Focus();
                        return;
                    }//CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
                    //DataTable odt = CHNLSVC.Sales.GetLoyaltyMemberByCardNo(txtSerNo.Text.Trim());
                    //if (odt.Rows.Count >= 1)
                    //{
                    //    foreach(DataRow r in odt.Rows)
                    //    {
                    //        string custcd=r["SALCM_CUS_CD"].ToString();
                    //        MessageBox.Show("Loyalty card number already available for customer - '" + custcd + "'", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        txtSerNo.Focus();
                    //        return;  
                    //    }

                    //}
                }
                //string _cusCode = "";
                Int32 _effect = 0;
                if (_saleType == "HS")      //kapila 13/10/2016
                {
                    if (txtNIC.Text == "" && txtBR.Text == "" && txtPP.Text == "" && txtDL.Text == "")
                    {
                        MessageBox.Show("Please enter all the required information [NIC/BR/PP/DL]", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNIC.Focus();
                        return;
                    }
                }
                else
                {
                    if (txtNIC.Text == "" && txtBR.Text == "" && txtPP.Text == "" && txtDL.Text == "" && txtMob.Text == "")
                    {
                        MessageBox.Show("Please enter all the required information [NIC/BR/PP/DL/MOB]", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNIC.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Please enter name of customer", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtName.Focus();
                    return;
                }

                if (_scvParam == null)  //kapila 25/5/2017
                {
                    if (string.IsNullOrEmpty(txtPerAdd1.Text))
                    {
                        MessageBox.Show("Please enter customer present address.", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPerAdd1.Focus();
                        return;
                    }
                }
                else     //kapila 25/5/2017
                {
                    if (_scvParam.SP_CUST_ADDR_OPT == 0)
                    {
                        if (string.IsNullOrEmpty(txtPerAdd1.Text))
                        {
                            MessageBox.Show("Please enter customer present address.", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtPerAdd1.Focus();
                            return;
                        }
                    }
                }

                //Akila 2017/11/27
                if (string.IsNullOrEmpty(txtNationality.Text))
                {
                    if (IsControlMandatory("txtNationality"))
                    {
                        MessageBox.Show("Please select the nationality", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtNationality.Focus();
                        return;
                    }
                }

                if (chkVAT.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtVatreg.Text))
                    {
                        MessageBox.Show("Please enter VAT reg. number.", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtVatreg.Focus();
                        return;
                    }
                }

                if (chkSVAT.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtSVATReg.Text))
                    {
                        MessageBox.Show("Please enter SVAT reg. number.", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSVATReg.Focus();
                        return;
                    }
                }

                // Nadeeka 
                if (chkMail.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtPerEmail.Text))
                    {
                        MessageBox.Show("Please enter Email.", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPerEmail.Focus();
                        return;
                    }
                }
                if (_scvParam == null)  //kapila 25/5/2017
                {
                    // Nadeeka 30-07-2015
                    if (string.IsNullOrEmpty(txtPerTown.Text))
                    {
                        MessageBox.Show("Please enter customer permenent town", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPerTown.Focus();
                        return;
                    }
                }
                else
                {
                    if (_scvParam.SP_CUST_ADDR_OPT == 0)
                    {
                        if (string.IsNullOrEmpty(txtPerTown.Text))
                        {
                            MessageBox.Show("Please enter customer permenent town", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtPerTown.Focus();
                            return;
                        }
                    }
                }
                //if (_isExsit == false && _isGroup == false)// Nadeeka 19-08-2015 (Commented 27-08-2015 due to showroom complain mails)
                //{
                //    if (string.IsNullOrEmpty(txtMob.Text))
                //    {
                //        MessageBox.Show("Please enter customer mobile #", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        txtMob.Focus();
                //        return;
                //    }
                //}





                if (chkSVAT.Checked == true)
                {
                    MasterCompany _newCom = new MasterCompany();
                    _newCom = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);

                    if (_newCom.Mc_cd != null)
                    {
                        if (string.IsNullOrEmpty(_newCom.Mc_tax2))
                        {
                            MessageBox.Show("Cannot create SVAT customer under your current login company.", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot find company details.Please re-try.", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }



                //check multiple NIC Add By Chamal 24/04/2014
                List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, txtNIC.Text.Trim(), "", "", "", "", 1);
                if (_custList != null && _custList.Count >= 1 && txtNIC.Text.ToUpper() != "N/A")
                {
                    //string _custNIC = "Duplicate customers found selected NIC!\n";
                    //foreach (var _nicCust in _custList)
                    //{
                    //    _custNIC = _custNIC + _nicCust.Mbe_cd.ToString() + " | " + _nicCust.Mbe_name.ToString() + "\n";
                    //}
                    ////_custNIC = _custNIC + "\nPlease contact accounts department";
                    //MessageBox.Show(_custNIC, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //return;
                }

                //check multiple MOBILE Add By Chamal 24/04/2014
                _custList = null;
                _custList = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, "", txtMob.Text.Trim(), "", "", "", 1);
                if (_custList != null && _custList.Count >= 1 && txtMob.Text.ToUpper() != "N/A")
                {
                    //string _cusMOB = "Duplicate customers found for selected mobile no!\n";
                    //foreach (var _nicCust in _custList)
                    //{
                    //    _cusMOB = _cusMOB + _nicCust.Mbe_cd.ToString() + " | " + _nicCust.Mbe_name.ToString() + " | " + _nicCust.Mbe_nic.ToString() + "\n";
                    //}
                    //_cusMOB = _cusMOB + "\nPlease contact accounts department";
                    //MessageBox.Show(_cusMOB, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //return;
                }

                Collect_Cust();
                Collect_GroupCust();

                if (_isExsit == false)
                {
                    List<BusEntityItem> _lstBusItm = new List<BusEntityItem>();
                    _effect = CHNLSVC.Sales.SaveBusinessEntityDetailWithGroup(_custProfile, _account, _busInfoList, _lstBusItm, out _cusCode, null, _isExsit, _isGroup, _custGroup);
                    // _effect = CHNLSVC.Sales.SaveMyAbansDetails(_myAb, _custProfile, _custGroup, _member, _isExsit);
                    if (BaseCls.GlbUserComCode == "ABL")
                    {
                        Create_myAbance();//Add By Thqaranga 2017/07/24
                    }
                    txtSerNo.Enabled = false;
                    chkisloyalty.Enabled = false;

                }
                else
                {
                    List<BusEntityItem> _lstBusItm = new List<BusEntityItem>();
                    _cusCode = txtCusCode.Text.Trim();
                    _effect = CHNLSVC.Sales.UpdateBusinessEntityProfileWithGroup(_custProfile, BaseCls.GlbUserID, Convert.ToDateTime(DateTime.Today).Date, 0, _busInfoList, null, _lstBusItm, _custGroup);
                }

                //if (_isExsit == false)
                //{
                //    _effect = CHNLSVC.Sales.SaveBusinessEntityDetail(_custProfile, _account, _busInfoList, out _cusCode, null);
                //}
                //else
                //{
                //    _cusCode = txtCusCode.Text.Trim();
                //    _effect = CHNLSVC.Sales.UpdateBusinessEntityProfile(_custProfile, BaseCls.GlbUserID, Convert.ToDateTime(DateTime.Today).Date, 0, _busInfoList, null);
                //}

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
                else
                {
                    if (!string.IsNullOrEmpty(_cusCode))
                    {
                        MessageBox.Show(_cusCode, "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Creation Fail.", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
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
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Collect_GroupCust()
        {
            _custGroup = new GroupBussinessEntity();
            _custGroup.Mbg_cd = txtCusCode.Text.Trim();
            _custGroup.Mbg_name = txtName.Text.Trim();
            _custGroup.Mbg_tit = cmbTitle.Text;
            _custGroup.Mbg_ini = txtInit.Text.Trim();
            _custGroup.Mbg_fname = txtFname.Text.Trim();
            _custGroup.Mbg_sname = txtSName.Text.Trim();
            _custGroup.Mbg_nationality = "SL";
            _custGroup.Mbg_add1 = txtPreAdd1.Text.Trim();
            _custGroup.Mbg_add2 = txtPreAdd2.Text.Trim();
            _custGroup.Mbg_town_cd = txtPerTown.Text.Trim();
            _custGroup.Mbg_distric_cd = txtPerDistrict.Text.Trim();
            _custGroup.Mbg_province_cd = txtPerProvince.Text.Trim();
            _custGroup.Mbg_country_cd = txtPerCountry.Text.Trim();
            _custGroup.Mbg_tel = txtPerPhone.Text.Trim();
            _custGroup.Mbg_fax = "";
            _custGroup.Mbg_postal_cd = txtPerPostal.Text.Trim();
            _custGroup.Mbg_mob = txtMob.Text.Trim();
            _custGroup.Mbg_nic = txtNIC.Text.Trim();
            _custGroup.Mbg_pp_no = txtPP.Text.Trim();
            _custGroup.Mbg_dl_no = txtDL.Text.Trim();
            _custGroup.Mbg_br_no = txtBR.Text.Trim();
            _custGroup.Mbg_email = txtPerEmail.Text.Trim();
            _custGroup.Mbg_contact = "";
            _custGroup.Mbg_act = true;
            _custGroup.Mbg_is_suspend = false;
            _custGroup.Mbg_sex = cmbSex.Text;
            _custGroup.Mbg_dob = Convert.ToDateTime(dtpDOB.Text).Date;
            _custGroup.Mbg_cre_by = BaseCls.GlbUserID;
            _custGroup.Mbg_mod_by = BaseCls.GlbUserID;
            _custGroup.Mbg_nationality = NationalityCd;

        }
        private void Collect_Cust()
        {
            Boolean _isSMS = false;
            Boolean _isSVAT = false;
            Boolean _isVAT = false;
            Boolean _TaxEx = false;
            Boolean _isEmail = false;
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
            if (_isExsit == false && _isGroup == false)
            {
                _custProfile.Mbe_cd = null;
            }
            else
            {
                _custProfile.Mbe_cd = txtCusCode.Text.Trim();
            }
            _custProfile.Mbe_com = BaseCls.GlbUserComCode;
            _custProfile.Mbe_contact = null;
            _custProfile.Mbe_country_cd = txtPerCountry.Text.Trim();
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
            _custProfile.Mbe_cust_com = BaseCls.GlbUserComCode;
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
            _custProfile.Mbe_nationality = NationalityCd;

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
            _custProfile.Mbe_town_cd = txtPerTown.Text.Trim();
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
            _custProfile.MBE_FNAME = txtFname.Text.Trim();
            _custProfile.MBE_SNAME = txtSName.Text.Trim();
            _custProfile.MBE_INI = txtInit.Text.Trim();
            _custProfile.MBE_TIT = cmbTitle.Text.Trim();
            if (chkMail.Checked == true)
            {
                _isEmail = true;
            }
            else
            {
                _isEmail = false;
            }
            // Nadeeka 15-12-2014
            _custProfile.Mbe_agre_send_email = _isEmail;
            _custProfile.Mbe_cust_lang = cmbPrefLang.SelectedValue.ToString();
            _custProfile.Mbe_nationality = NationalityCd;
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
                chkVatEx.Checked = false;
                chkVatEx.Enabled = false;
                chkSVAT.Checked = false;
                chkSVAT.Enabled = false;
                txtSVATReg.Text = "";
                txtVatreg.Text = "";
                txtSVATReg.Enabled = false;
                txtVatreg.Enabled = false;
                chkVatEx.Enabled = true;
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
                _effect = CHNLSVC.Sales.SaveBusinessEntityDetail(_custProfile, _account, _busInfoList, out _cusCode, null);
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

        private void txtInit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFname.Focus();
            }
        }

        private void txtFname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSName.Focus();
            }
        }

        private void txtCusCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNIC_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPerAdd1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPreAdd1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPreAdd2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPerTown_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMob_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMob_DoubleClick(object sender, EventArgs e)
        {

            txtDL.ReadOnly = true;
            txtPP.ReadOnly = true;
            txtNIC.ReadOnly = true;
            txtBR.ReadOnly = true;
            txtMob.ReadOnly = false;
            _isUpdate = true;
        }

        private void txtBR_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMob.Text))
            {
                MessageBox.Show("Please enter customer mobile #", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMob.Focus();
                return;
            }
            txtDL.ReadOnly = true;
            txtPP.ReadOnly = true;
            txtNIC.ReadOnly = true;
            txtBR.ReadOnly = false;
            txtMob.ReadOnly = true;
            _isUpdate = true;
        }

        private void txtNIC_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMob.Text))
            {
                MessageBox.Show("Please enter customer mobile #", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMob.Focus();
                return;
            }
            txtDL.ReadOnly = true;
            txtPP.ReadOnly = true;
            txtNIC.ReadOnly = false; ;
            txtBR.ReadOnly = true;
            txtMob.ReadOnly = true;
            _isUpdate = true;
        }

        private void txtPP_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMob.Text))
            {
                MessageBox.Show("Please enter customer mobile #", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMob.Focus();
                return;
            }
            txtDL.ReadOnly = true;
            txtPP.ReadOnly = false; ;
            txtNIC.ReadOnly = true;
            txtBR.ReadOnly = true;
            txtMob.ReadOnly = true;
            _isUpdate = true;
        }

        private void txtDL_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMob.Text))
            {
                MessageBox.Show("Please enter customer mobile #", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMob.Focus();
                return;
            }
            txtDL.ReadOnly = false; ;
            txtPP.ReadOnly = true;
            txtNIC.ReadOnly = true;
            txtBR.ReadOnly = true;
            txtMob.ReadOnly = true;
            _isUpdate = true;
        }

        private void txtPP_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDL_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBR_TextChanged(object sender, EventArgs e)
        {

        }

        private void Create_myAbance()//tharanga 2017/07/24
        {
            int _effect = 0;
            MyAbans _myAb = new MyAbans();

            _myAb.Myab_seq = 0;
            _myAb.Myab_ser_no = txtSerNo.Text;
            _myAb.Myab_nic = txtNIC.Text;
            _myAb.Myab_tit = cmbTitle.Text.Trim();
            _myAb.Myab_fname = txtFname.Text;
            _myAb.Myab_sname = txtSName.Text;
            _myAb.Myab_stus = "";
            _myAb.Myab_dob = Convert.ToDateTime(dtpDOB.Text).Date;
            _myAb.Myab_email = txtPerEmail.Text;
            _myAb.Myab_mob = txtMob.Text.Trim();
            _myAb.Myab_tel = txtPerPhone.Text.Trim();

            _myAb.Myab_wr_designation = txtWorkDesig.Text.Trim();
            _myAb.Myab_cust_lang = cmbPrefLang.SelectedValue.ToString();
            // _myAb.Myab_add1 = txtPreAdd1.Text.Trim();
            //  _myAb.Myab_add2 = txtPreAdd2.Text.Trim();

            _myAb.Myab_close_sr = BaseCls.GlbUserDefLoca;
            _myAb.Myab_tv_chnl = "";
            _myAb.Myab_radio_chnl = "";
            _myAb.Myab_news_paper = "";
            _myAb.Myab_spouse = "";
            //_myAb.Myab_spouse_dob = Convert.ToDateTime(dtpDOBS.Text).Date;
            _myAb.Myab_name_in_card = txtFname.Text;

            //if (chkBrand.Checked) _decRate = "1";
            //if (chkPrice.Checked) _decRate = _decRate + "2";
            //if (chkDisc.Checked) _decRate = _decRate + "3";
            //if (chkPromo.Checked) _decRate = _decRate + "4";
            //_myAb.Myab_decision_rt = _decRate;

            //if (chkCash.Checked) _method = chkCash.Tag.ToString();
            //if (chkCC.Checked) _method = chkCC.Tag.ToString();
            //if (chkHP.Checked) _method = chkHP.Tag.ToString();
            //_myAb.Myab_pay_method = _method;

            _myAb.Myab_buy_online = false; //optOnline.Checked ? true :
            _myAb.Myab_is_child = false; //optChild.Checked ? true :
            _myAb.Myab_is_buy_ab = false; //optBuy.Checked ? true :
            _myAb.Myab_is_need_card_sp = 1;//optSP.Checked ? 1 :


            //if (!string.IsNullOrEmpty(txtDOB1.Text)) _myAb.Myab_bd_ch1 = Convert.ToDateTime(txtDOB1.Text).Date;
            //if (!string.IsNullOrEmpty(txtDOB2.Text)) _myAb.Myab_bd_ch2 = Convert.ToDateTime(txtDOB2.Text).Date;
            //if (!string.IsNullOrEmpty(txtDOB3.Text)) _myAb.Myab_bd_ch3 = Convert.ToDateTime(txtDOB3.Text).Date;
            //if (!string.IsNullOrEmpty(txtDOB4.Text)) _myAb.Myab_bd_ch4 = Convert.ToDateTime(txtDOB4.Text).Date;

            _myAb.Myab_use_tv = false;
            _myAb.Myab_use_tv_yr = 0;
            _myAb.Myab_use_fr = false;
            _myAb.Myab_use_fr_yr = 0;
            _myAb.Myab_use_wm = false;
            _myAb.Myab_use_wm_yr = 0;
            _myAb.Myab_use_lap = false;
            _myAb.Myab_use_lap_yr = 0;
            _myAb.Myab_use_tab = false;
            _myAb.Myab_use_tab_yr = 0;
            _myAb.Myab_use_dtop = false;
            _myAb.Myab_use_dtop_yr = 0;
            _myAb.Myab_use_sp = false;
            _myAb.Myab_use_sp_yr = 0;
            _myAb.Myab_use_mo = false;
            _myAb.Myab_use_mo_yr = 0;
            _myAb.Myab_use_ck_gas = false;
            _myAb.Myab_use_ck_gas_yr = 0;
            _myAb.Myab_use_ck_dtop = false;
            _myAb.Myab_use_ck_dtop_yr = 0;
            _myAb.Myab_use_mg = false;
            _myAb.Myab_use_mg_yr = 0;
            _myAb.Myab_use_hifi = false;
            _myAb.Myab_use_hifi_yr = 0;
            _myAb.Myab_use_ac = false;
            _myAb.Myab_use_ac_yr = 0;

            _myAb.Myab_buy_tv = 0;
            _myAb.Myab_buy_fr = 0;
            _myAb.Myab_buy_wm = 0;
            _myAb.Myab_buy_pc = 0;
            _myAb.Myab_buy_sp = 0;
            _myAb.Myab_buy_ck = 0;
            _myAb.Myab_buy_ck_dtop = 0;
            _myAb.Myab_buy_mg = 0;
            _myAb.Myab_buy_hifi = 0;
            _myAb.Myab_buy_ac = 0;
            if (chkMail.Checked == true)
            {
                _myAb.myab_is_snd_mail = 1;
            }
            else
            {
                _myAb.myab_is_snd_mail = 0;
            }

            //_myAb.Myab_dt = Convert.ToDateTime(dtpDate.Value).Date;

            _myAb.Myab_com = BaseCls.GlbUserComCode;
            _myAb.Myab_pc = BaseCls.GlbUserDefProf;
            //_myAb.Myab_add3 = txtWorkAdd3.Text;
            //  _myAb.Myab_add4 = txtPerTown.Text;
            _myAb.Myab_oth_rem1 = "";
            _myAb.Myab_oth_rem2 = "";
            _myAb.Myab_need_card = true;

            LoyaltyMemeber _member = new LoyaltyMemeber();
            _member.Salcm_loty_tp = "MYAB";
            _member.Salcm_email = txtPerEmail.Text;
            _member.Salcm_contact = txtMob.Text;
            _member.Salcm_cus_cd = _cusCode;
            _member.Salcm_dis_rt = 0;
            _member.Salcm_val_frm = DateTime.Now.Date;
            string date = "31/12/2999";
            DateTime dt = Convert.ToDateTime(date);

            _member.Salcm_val_to = dt;
            _member.Salcm_cre_by = BaseCls.GlbUserID;
            _member.Salcm_cre_dt = DateTime.Now.Date;
            _member.Salcm_app_by = BaseCls.GlbUserID;
            _member.Salcm_app_dt = DateTime.Now.Date;
            _member.Salcm_bal_pt = 0;
            _member.Salcm_cd_ser = txtSerNo.Text;
            _member.Salcm_cus_spec = "CLASSIC";
            _member.Salcm_no = txtSerNo.Text;
            _member.Salcm_act = 1;









            List<BusEntityItem> _lstBusItm = new List<BusEntityItem>();
            _effect = CHNLSVC.Sales.SaveMyAbansDetailsNew(_myAb, _member, _isExsit, txtSerNo.Text.ToString());
        }

        public void enableloyaltymembership()
        {
            if (BaseCls.GlbUserComCode == "ABL")
            {
                txtSerNo.Enabled = true;
                txtSerNo.ReadOnly = false;
                chkisloyalty.Enabled = true;
                chkisloyalty.Checked = true;

            }

        }
        public void Disableloyaltymembership()
        {

            txtSerNo.Enabled = false;
            chkisloyalty.Enabled = false;
            chkisloyalty.Checked = false;



        }

        private void BindLoyalatyDetails(string _customer)
        {
            try
            {
                List<LoyaltyMemeber> _loyalCus = new List<LoyaltyMemeber>();


                _loyalCus = CHNLSVC.Sales.GetCurrentLoyalByCus(_customer, null);
                foreach (var person in _loyalCus)
                {
                    txtSerNo.Text = person.Salcm_cd_ser;
                    txtSerNo.Enabled = false;
                    chkisloyalty.Checked = true;
                    chkisloyalty.Enabled = false;
                    txtSerNo.ReadOnly = true;
                }
                if (_loyalCus.Count == 0)
                {
                    enableloyaltymembership();
                    txtSerNo.Text = "";
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void chkisloyalty_CheckedChanged(object sender, EventArgs e)
        {
            if (chkisloyalty.Checked == true)
            {
                txtSerNo.Enabled = true;
            }
            else
            {
                txtSerNo.Enabled = false;
            }
        }

        private void txtSerNo_Leave(object sender, EventArgs e)
        {
             //Tharindu 2017-11-21
            try
            {
                //bool valuve = CHNLSVC.Sales.GetLoyalityExistsStatus("MYAB", txtSerNo.Text.Trim()); // type current hardcoded due to req

                //if (valuve)
                //{
                //    MessageBox.Show("Invalid Loyality Number", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtSerNo.Text = "";
                //    txtSerNo.Focus();
                //    return;
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }


            //DataTable odt = CHNLSVC.Sales.GetLoyaltyMemberByCardNo(txtSerNo.Text.Trim());
            //if (odt.Rows.Count >= 1)
            //{
            //    foreach (DataRow r in odt.Rows)
            //    {
            //        string custcd = r["SALCM_CUS_CD"].ToString();
            //        MessageBox.Show("Loyalty card number already available for customer - '" + custcd + "'", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        txtSerNo.Focus();
            //        return;
            //    }

            //}
        }

        private void btnSearchNationality_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                //_CommonSearch.ReturnIndex = 1;
                _CommonSearch.IsReturnFullRow = true;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Nationality);
                DataTable _result = CHNLSVC.CommonSearch.SearchNationality(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtNationality;
                _CommonSearch.ShowDialog();

                string _selectedRow = _CommonSearch.UserSelectedRow;
                if (!string.IsNullOrEmpty(_selectedRow))
                {
                    var _tmpData = _selectedRow.Split(new string[] { "|" }, StringSplitOptions.None).ToList();
                    if (_tmpData != null && _tmpData.Count > 0)
                    {
                        NationalityCd = _tmpData[0];
                        txtNationality.Text = _tmpData[1];
                    }
                }

                txtInit.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtNationality_DoubleClick(object sender, EventArgs e)
        {
            btnSearchNationality_Click(null, null);
        }

        //akila 2017/11/27
        private bool IsControlMandatory(string _controlName)
        {
            bool _isMandatory = false;
            try
            {
                DataTable _syspara = CHNLSVC.Inventory.getMstSysPara(BaseCls.GlbUserComCode, "SCHNL", BaseCls.GlbDefChannel, "CUST_MAND_CTRL", _controlName.ToUpper());
                if (_syspara.Rows.Count > 0)
                {
                    _isMandatory = true;
                }
            }
            catch (Exception ex)
            {
                Cursor = DefaultCursor;
                _isMandatory = false;
                MessageBox.Show("An error occurred !" + Environment.NewLine + ex.Message, "Check Mandatory Controls", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _isMandatory;
        }

        private void LoadNationality(string _code)
        {
            try
            {
                DataTable _result = new DataTable();
                _result = CHNLSVC.CommonSearch.SearchNationality("|", "CODE", _code);
                if (_result.Rows.Count > 0)
                {
                    NationalityCd = _result.Rows[0]["CODE"] == DBNull.Value ? string.Empty : _result.Rows[0]["CODE"].ToString();
                    txtNationality.Text = _result.Rows[0]["DESCRIPTION"] == DBNull.Value ? string.Empty : _result.Rows[0]["DESCRIPTION"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred !" + Environment.NewLine + ex.Message, "Load Nationality", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNationality.Text = string.Empty;
                NationalityCd = string.Empty;
            }
        }

    }
}
