using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using FF.BusinessObjects;
using Microsoft.VisualBasic;

namespace FF.WindowsERPClient.General
{
    public partial class MyAbansEntry : Base
    {


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
        private Int32 _eff = 0;
        //constructor added by Shani 18-02-2013


        public MyAbansEntry(string userID, string UserComCode, string _OrgPC)
        {
            InitializeComponent();
        }
        public MyAbansEntry()
        {
            InitializeComponent();
        }

        private void CustomerCreation_Load(object sender, EventArgs e)
        {
            Clear_Data();
            txtSer.Text = "";
            cmbPrefLang.SelectedIndex = 0;
            LoadLanguage();
            Loadfavourite();
            txtProf.Text = BaseCls.GlbUserDefProf;

        }

        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.CustomerCommonByNIC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
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

            //  chkMail.Checked = false;
            txtCusCode.Text = "";
            txtCusCode.Enabled = true;
            _isExsit = false;
            _isGroup = false;
            //   cmbType.Text = "INDIVIDUAL";
            cmbPrefLang.Text = "ENGLISH";
            cmbSex.Text = "";
            cmbTitle.Text = "";
            txtNIC.Text = "";
            txtBR.Text = "";
            txtPP.Text = "";
            txtDL.Text = "";
            txtMob.Text = "";
            // chkSMS.Checked = false;
            txtName.Text = "";
            //txtPerAdd1.Text = "";
            //txtPerAdd2.Text = "";
            //txtPerTown.Text = "";
            //txtPerDistrict.Text = "";
            //txtPerPostal.Text = "";
            //txtPerProvince.Text = "";
            //txtPerCountry.Text = "";
            txtPerPhone.Text = "";
            txtPerEmail.Text = "";
            //   txtPreAdd1.Text = "";
            //  txtPreAdd2.Text = "";
            txtPreTown.Text = "";
            //txtPreDistrict.Text = "";
            //txtPreProvince.Text = "";
            //txtPrePostal.Text = "";
            //txtPreCountry.Text = "";
            //txtPrePhone.Text = "";
            //txtWorkName.Text = "";
            txtWorkAdd1.Text = "";
            txtWorkAdd2.Text = "";
            //  txtWorkDept.Text = "";
            txtWorkDesig.Text = "";
            //txtWorkPhone.Text = "";
            //txtWorkFax.Text = "";
            //txtWorkEmail.Text = "";
            dtpDOB.Value = Convert.ToDateTime(DateTime.Today).Date;
            dtpDOBS.Value = Convert.ToDateTime(DateTime.Today).Date;
            dtpDate.Value = Convert.ToDateTime(DateTime.Today).Date;


            //chkVAT.Checked = false;
            //chkSVAT.Checked = false;
            //chkVatEx.Checked = false;

            //chkSVAT.Enabled = false;
            //chkVatEx.Enabled = false;
            //chkVatEx.Enabled = true;

            //txtVatreg.Text = "";
            //txtSVATReg.Text = "";
            //txtVatreg.Enabled = false;
            //txtSVATReg.Enabled = false;
            //tbAdd.SelectedTab = tabPage1;
            btnCreate.Enabled = true;
            //  btnCreate.Text = "Create";


            txtTVY.Text = "";
            txtFRY.Text = "";
            txtWMY.Text = "";
            txtLPY.Text = "";
            txtTBY.Text = "";
            txtDTY.Text = "";
            txtSPY.Text = "";
            txtDTCY.Text = "";
            txtOTHY.Text = "";
            txtHIFIY.Text = "";
            txtACY.Text = "";
            txtMOY.Text = "";

            chkSP.Checked = false;
            chkFR.Checked = false;
            chkWM.Checked = false;
            chkDTC.Checked = false;
            chkOTH.Checked = false;
            chkAC.Checked = false;
            chkHIFI.Checked = false;
            chkLP.Checked = false;
            chkTB.Checked = false;
            chkDT.Checked = false;
            chkTV.Checked = false;
            chkMO.Checked = false;

            txtTV.Text = "";
            txtFR.Text = "";
            txtWM.Text = "";
            txtPC.Text = "";
            txtSP.Text = "";
            txtMO.Text = "";
            txtDTC.Text = "";
            txtOTH.Text = "";
            txtHIFI.Text = "";
            txtAC.Text = "";

            txtDOB1.Text = "";
            txtDOB2.Text = "";
            txtDOB3.Text = "";
            txtDOB4.Text = "";

            optBuy.Checked = true;
            optChild.Checked = true;
            optOnline.Checked = true;
            optSP.Checked = true;

            txtSpouse.Text = "";
            txtCloseSR.Text = "";

            // txtProf.Text = "";
            txtOthRem1.Text = "";
            txtOthRem2.Text = "";
            txtWorkAdd3.Text = "";
            //   txtPerTown.Text = "";

            optNeedCard.Checked = true;

            cmbRadio.SelectedIndex = -1;
            cmbPaper.SelectedIndex = -1;
            cmbTV.SelectedIndex = -1;

            //  cmbType.Focus();
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

            DataTable _dt = CHNLSVC.General.getMyAbansBySerial(txtNIC.Text, 1);
            if (_dt.Rows.Count > 0)
            {
                txtSer.Text = _dt.Rows[0]["MYAB_SER_NO"].ToString();
                if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_sp"]) == true) chkSP.Checked = true;
                if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_fr"]) == true) chkFR.Checked = true;
                if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_wm"]) == true) chkWM.Checked = true;
                if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_ck_dtop"]) == true) chkDTC.Checked = true;
                if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_mg"]) == true) chkOTH.Checked = true;
                if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_ac"]) == true) chkAC.Checked = true;
                if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_hifi"]) == true) chkHIFI.Checked = true;
                if (Convert.ToBoolean(_dt.Rows[0]["MYAB_USE_LAP"]) == true) chkLP.Checked = true;
                if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_tab"]) == true) chkTB.Checked = true;
                if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_dtop"]) == true) chkDT.Checked = true;
                if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_tv"]) == true) chkTV.Checked = true;
                if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_mo"]) == true) chkMO.Checked = true;

                if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_TV_YR"].ToString())) txtTVY.Text = _dt.Rows[0]["MYAB_USE_TV_YR"].ToString();
                if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_FR_YR"].ToString())) txtFRY.Text = _dt.Rows[0]["MYAB_USE_FR_YR"].ToString();
                if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_WM_YR"].ToString())) txtWMY.Text = _dt.Rows[0]["MYAB_USE_WM_YR"].ToString();
                if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_LAP_YR"].ToString())) txtLPY.Text = _dt.Rows[0]["MYAB_USE_LAP_YR"].ToString();
                if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_TAB_YR"].ToString())) txtTBY.Text = _dt.Rows[0]["MYAB_USE_TAB_YR"].ToString();
                if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_DTOP_YR"].ToString())) txtDTY.Text = _dt.Rows[0]["MYAB_USE_DTOP_YR"].ToString();
                if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_SP_YR"].ToString())) txtSPY.Text = _dt.Rows[0]["MYAB_USE_SP_YR"].ToString();
                if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_CK_DTOP_YR"].ToString())) txtDTCY.Text = _dt.Rows[0]["MYAB_USE_CK_DTOP_YR"].ToString();
                if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_MG_YR"].ToString())) txtOTHY.Text = _dt.Rows[0]["MYAB_USE_MG_YR"].ToString();
                if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_HIFI_YR"].ToString())) txtHIFIY.Text = _dt.Rows[0]["MYAB_USE_HIFI_YR"].ToString();
                if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_AC_YR"].ToString())) txtACY.Text = _dt.Rows[0]["MYAB_USE_AC_YR"].ToString();
                if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_MO_YR"].ToString())) txtMOY.Text = _dt.Rows[0]["MYAB_USE_MO_YR"].ToString();

                if (Convert.ToBoolean(_dt.Rows[0]["Myab_buy_online"]) == true) optOnline.Checked = true; else optOnlineN.Checked = true;
                if (Convert.ToBoolean(_dt.Rows[0]["Myab_is_child"]) == true) optChild.Checked = true; else optChildN.Checked = true;
                if (Convert.ToBoolean(_dt.Rows[0]["Myab_is_buy_ab"]) == true) optBuy.Checked = true; else optBuyN.Checked = true;
                if (Convert.ToBoolean(_dt.Rows[0]["Myab_is_need_card_sp"]) == true) optSP.Checked = true; else optSPN.Checked = true;

                if (!string.IsNullOrEmpty(_dt.Rows[0]["Myab_bd_ch1"].ToString()) && _dt.Rows[0]["Myab_bd_ch1"].ToString() != "01/Jan/0001 12:00:00 AM") txtDOB1.Text = _dt.Rows[0]["Myab_bd_ch1"].ToString();
                if (!string.IsNullOrEmpty(_dt.Rows[0]["Myab_bd_ch2"].ToString()) && _dt.Rows[0]["Myab_bd_ch2"].ToString() != "01/Jan/0001 12:00:00 AM") txtDOB2.Text = _dt.Rows[0]["Myab_bd_ch2"].ToString();
                if (!string.IsNullOrEmpty(_dt.Rows[0]["Myab_bd_ch3"].ToString()) && _dt.Rows[0]["Myab_bd_ch3"].ToString() != "01/Jan/0001 12:00:00 AM") txtDOB3.Text = _dt.Rows[0]["Myab_bd_ch3"].ToString();
                if (!string.IsNullOrEmpty(_dt.Rows[0]["Myab_bd_ch4"].ToString()) && _dt.Rows[0]["Myab_bd_ch4"].ToString() != "01/Jan/0001 12:00:00 AM") txtDOB4.Text = _dt.Rows[0]["Myab_bd_ch4"].ToString();

                txtCloseSR.Text = _dt.Rows[0]["Myab_close_sr"].ToString();
                cmbTV.Text = _dt.Rows[0]["Myab_tv_chnl"].ToString();
                cmbRadio.Text = _dt.Rows[0]["Myab_radio_chnl"].ToString();
                cmbPaper.Text = _dt.Rows[0]["Myab_news_paper"].ToString();
                txtSpouse.Text = _dt.Rows[0]["Myab_spouse"].ToString();
                dtpDOBS.Value = Convert.ToDateTime(_dt.Rows[0]["Myab_spouse_dob"]);
                txtNameCard.Text = _dt.Rows[0]["Myab_name_in_card"].ToString();

                txtTV.Text = _dt.Rows[0]["Myab_buy_tv"].ToString();
                txtFR.Text = _dt.Rows[0]["Myab_buy_fr"].ToString();
                txtWM.Text = _dt.Rows[0]["Myab_buy_wm"].ToString();
                txtPC.Text = _dt.Rows[0]["Myab_buy_pc"].ToString();
                txtSP.Text = _dt.Rows[0]["Myab_buy_sp"].ToString();
                txtMO.Text = _dt.Rows[0]["Myab_buy_ck"].ToString();
                txtDTC.Text = _dt.Rows[0]["Myab_buy_ck_dtop"].ToString();
                txtOTH.Text = _dt.Rows[0]["Myab_buy_mg"].ToString();
                txtHIFI.Text = _dt.Rows[0]["Myab_buy_hifi"].ToString();
                txtAC.Text = _dt.Rows[0]["Myab_buy_ac"].ToString();

                if (_dt.Rows[0]["Myab_decision_rt"].ToString().Contains("1")) chkBrand.Checked = true;
                if (_dt.Rows[0]["Myab_decision_rt"].ToString().Contains("2")) chkPrice.Checked = true;
                if (_dt.Rows[0]["Myab_decision_rt"].ToString().Contains("3")) chkDisc.Checked = true;
                if (_dt.Rows[0]["Myab_decision_rt"].ToString().Contains("4")) chkPromo.Checked = true;

                if (_dt.Rows[0]["Myab_pay_method"].ToString() == "CASH") chkCash.Checked = true;
                if (_dt.Rows[0]["Myab_pay_method"].ToString() == "CC") chkCC.Checked = true;
                if (_dt.Rows[0]["Myab_pay_method"].ToString() == "HP") chkHP.Checked = true;

                txtProf.Text = _dt.Rows[0]["Myab_pc"].ToString();
                txtWorkAdd3.Text = _dt.Rows[0]["Myab_add3"].ToString();
                //    txtPerTown.Text = _dt.Rows[0]["Myab_add4"].ToString();
                txtOthRem1.Text = _dt.Rows[0]["Myab_oth_rem1"].ToString();
                txtOthRem2.Text = _dt.Rows[0]["Myab_oth_rem2"].ToString();
                if (Convert.ToBoolean(_dt.Rows[0]["Myab_need_card"]) == true) optNeedCard.Checked = true; else optNeedCardN.Checked = true;
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

            }

        }


        private void txtPerTown_Leave(object sender, EventArgs e)
        {

        }

        private void txtPreTown_Leave(object sender, EventArgs e)
        {

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


        }


        private void txtCusCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
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
        }

        private void txtNIC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNIC_Leave(null, null);
                // txtProf.Focus();
            }
        }

        private void txtBR_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtPP_keyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtMob_keyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtDL_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void chkSMS_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cmbTitle_KeyDown(object sender, KeyEventArgs e)
        {

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

        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dtpDOB_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtPerAdd1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtPerAdd2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtPerPhone_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtWorkName_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtWorkAdd1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtWorkAdd2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtWorkDept_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtWorkDesig_KeyDown(object sender, KeyEventArgs e)
        {

        }


        private void txtWorkPhone_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtWorkFax_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtWorkFax_Leave(object sender, EventArgs e)
        {

        }


        private void txtWorkPhone_Leave(object sender, EventArgs e)
        {

        }


        private void txtWorkEmail_Leave(object sender, EventArgs e)
        {

        }

        private void txtWorkEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //if (!string.IsNullOrEmpty(txtWorkEmail.Text))
                //{
                //    Boolean _isValid = IsValidEmail(txtWorkEmail.Text.Trim());

                //    if (_isValid == false)
                //    {
                //        MessageBox.Show("Invalid email address.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //      //  txtWorkEmail.Text = "";
                //      //  txtWorkEmail.Focus();
                //        return;
                //    }
                //}

                // tbAdd.SelectedTab = tabPage4;
                //  chkVAT.Focus();
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
                // tbAdd.SelectedTab = tabPage2;
                // optOnline.Focus();
            }
        }

        private void txtPrePhone_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtPreAdd1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtPreAdd2_KeyDown(object sender, KeyEventArgs e)
        {

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

        }


        private void txtPerAdd1_Leave(object sender, EventArgs e)
        {
            // txtPreAdd1.Text = txtPerAdd1.Text.Trim();
        }

        private void txtPerAdd2_Leave(object sender, EventArgs e)
        {
            // txtPreAdd2.Text = txtPerAdd2.Text.Trim();
        }

        private void txtPrePhone_Leave(object sender, EventArgs e)
        {

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
                btnCreate.Enabled = true;
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
                    //check multiple Add By Chamal 24/04/2014
                    //List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, txtNIC.Text.Trim(), "", "", "", "", 1);
                    //if (_custList != null && _custList.Count > 1 && txtNIC.Text.ToUpper() != "N/A")
                    //{
                    //    string _custNIC = "Duplicate customers found!\n";
                    //    foreach (var _nicCust in _custList)
                    //    {
                    //        _custNIC = _custNIC + _nicCust.Mbe_cd.ToString() + " | " + _nicCust.Mbe_name.ToString() + "\n";
                    //    }
                    //    // _custNIC = _custNIC + "\nPlease contact accounts department";
                    //    MessageBox.Show(_custNIC, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    txtNIC.Text = "";
                    //    txtNIC.Focus();
                    //    return;
                    //}

                    MasterBusinessEntity custProf = GetbyNIC(txtNIC.Text.Trim().ToUpper());
                    //  custProf = CHNLSVC.Sales.GetCustomerProfileByCom(txtNIC.Text, null, null, null, null, BaseCls.GlbUserComCode);
                    if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                    {
                        _isExsit = true;
                        //  btnCreate.Text = "Update";
                        dobGeneration();
                        LoadCustProf(custProf);
                        txtDL.ReadOnly = true;
                        txtPP.ReadOnly = true;
                        txtNIC.ReadOnly = true;
                        txtBR.ReadOnly = true;
                        //  txtMob.ReadOnly = true;

                        Int32 _attemt = 0;





                    }
                    else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                    {
                        MessageBox.Show("Customer is inactivated.Please contact accounts dept.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnCreate.Enabled = false;
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

                        //Check the group level
                        GroupBussinessEntity _grupProf = GetbyNICGrup(txtNIC.Text.Trim().ToUpper());
                        if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                        {
                            LoadCustProfByGrup(_grupProf);
                            _isGroup = true;
                            _isExsit = false;
                            //   btnCreate.Text = "Create";
                            return;
                        }
                        else
                        {
                            _isGroup = false;
                        }

                        _isExsit = false;
                        //   btnCreate.Text = "Create";
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

                    DataTable _dt = CHNLSVC.General.getMyAbansBySerial(txtNIC.Text, 1);
                    if (_dt.Rows.Count > 0)
                    {
                        txtSer.Text = _dt.Rows[0]["MYAB_SER_NO"].ToString();
                        if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_sp"]) == true) chkSP.Checked = true;
                        if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_fr"]) == true) chkFR.Checked = true;
                        if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_wm"]) == true) chkWM.Checked = true;
                        if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_ck_dtop"]) == true) chkDTC.Checked = true;
                        if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_mg"]) == true) chkOTH.Checked = true;
                        if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_ac"]) == true) chkAC.Checked = true;
                        if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_hifi"]) == true) chkHIFI.Checked = true;
                        if (Convert.ToBoolean(_dt.Rows[0]["MYAB_USE_LAP"]) == true) chkLP.Checked = true;
                        if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_tab"]) == true) chkTB.Checked = true;
                        if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_dtop"]) == true) chkDT.Checked = true;
                        if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_tv"]) == true) chkTV.Checked = true;
                        if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_mo"]) == true) chkMO.Checked = true;

                        if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_TV_YR"].ToString())) txtTVY.Text = _dt.Rows[0]["MYAB_USE_TV_YR"].ToString();
                        if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_FR_YR"].ToString())) txtFRY.Text = _dt.Rows[0]["MYAB_USE_FR_YR"].ToString();
                        if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_WM_YR"].ToString())) txtWMY.Text = _dt.Rows[0]["MYAB_USE_WM_YR"].ToString();
                        if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_LAP_YR"].ToString())) txtLPY.Text = _dt.Rows[0]["MYAB_USE_LAP_YR"].ToString();
                        if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_TAB_YR"].ToString())) txtTBY.Text = _dt.Rows[0]["MYAB_USE_TAB_YR"].ToString();
                        if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_DTOP_YR"].ToString())) txtDTY.Text = _dt.Rows[0]["MYAB_USE_DTOP_YR"].ToString();
                        if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_SP_YR"].ToString())) txtSPY.Text = _dt.Rows[0]["MYAB_USE_SP_YR"].ToString();
                        if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_CK_DTOP_YR"].ToString())) txtDTCY.Text = _dt.Rows[0]["MYAB_USE_CK_DTOP_YR"].ToString();
                        if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_MG_YR"].ToString())) txtOTHY.Text = _dt.Rows[0]["MYAB_USE_MG_YR"].ToString();
                        if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_HIFI_YR"].ToString())) txtHIFIY.Text = _dt.Rows[0]["MYAB_USE_HIFI_YR"].ToString();
                        if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_AC_YR"].ToString())) txtACY.Text = _dt.Rows[0]["MYAB_USE_AC_YR"].ToString();
                        if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_MO_YR"].ToString())) txtMOY.Text = _dt.Rows[0]["MYAB_USE_MO_YR"].ToString();

                        if (Convert.ToBoolean(_dt.Rows[0]["Myab_buy_online"]) == true) optOnline.Checked = true; else optOnlineN.Checked = true;
                        if (Convert.ToBoolean(_dt.Rows[0]["Myab_is_child"]) == true) optChild.Checked = true; else optChildN.Checked = true;
                        if (Convert.ToBoolean(_dt.Rows[0]["Myab_is_buy_ab"]) == true) optBuy.Checked = true; else optBuyN.Checked = true;
                        if (Convert.ToBoolean(_dt.Rows[0]["Myab_is_need_card_sp"]) == true) optSP.Checked = true; else optSPN.Checked = true;

                        if (!string.IsNullOrEmpty(_dt.Rows[0]["Myab_bd_ch1"].ToString()) && _dt.Rows[0]["Myab_bd_ch1"].ToString() != "01/Jan/0001 12:00:00 AM") txtDOB1.Text = _dt.Rows[0]["Myab_bd_ch1"].ToString();
                        if (!string.IsNullOrEmpty(_dt.Rows[0]["Myab_bd_ch2"].ToString()) && _dt.Rows[0]["Myab_bd_ch2"].ToString() != "01/Jan/0001 12:00:00 AM") txtDOB2.Text = _dt.Rows[0]["Myab_bd_ch2"].ToString();
                        if (!string.IsNullOrEmpty(_dt.Rows[0]["Myab_bd_ch3"].ToString()) && _dt.Rows[0]["Myab_bd_ch3"].ToString() != "01/Jan/0001 12:00:00 AM") txtDOB3.Text = _dt.Rows[0]["Myab_bd_ch3"].ToString();
                        if (!string.IsNullOrEmpty(_dt.Rows[0]["Myab_bd_ch4"].ToString()) && _dt.Rows[0]["Myab_bd_ch4"].ToString() != "01/Jan/0001 12:00:00 AM") txtDOB4.Text = _dt.Rows[0]["Myab_bd_ch4"].ToString();

                        txtCloseSR.Text = _dt.Rows[0]["Myab_close_sr"].ToString();
                        cmbTV.Text = _dt.Rows[0]["Myab_tv_chnl"].ToString();
                        cmbRadio.Text = _dt.Rows[0]["Myab_radio_chnl"].ToString();
                        cmbPaper.Text = _dt.Rows[0]["Myab_news_paper"].ToString();
                        txtSpouse.Text = _dt.Rows[0]["Myab_spouse"].ToString();
                        dtpDOBS.Value = Convert.ToDateTime(_dt.Rows[0]["Myab_spouse_dob"]);
                        txtNameCard.Text = _dt.Rows[0]["Myab_name_in_card"].ToString();

                        txtTV.Text = _dt.Rows[0]["Myab_buy_tv"].ToString();
                        txtFR.Text = _dt.Rows[0]["Myab_buy_fr"].ToString();
                        txtWM.Text = _dt.Rows[0]["Myab_buy_wm"].ToString();
                        txtPC.Text = _dt.Rows[0]["Myab_buy_pc"].ToString();
                        txtSP.Text = _dt.Rows[0]["Myab_buy_sp"].ToString();
                        txtMO.Text = _dt.Rows[0]["Myab_buy_ck"].ToString();
                        txtDTC.Text = _dt.Rows[0]["Myab_buy_ck_dtop"].ToString();
                        txtOTH.Text = _dt.Rows[0]["Myab_buy_mg"].ToString();
                        txtHIFI.Text = _dt.Rows[0]["Myab_buy_hifi"].ToString();
                        txtAC.Text = _dt.Rows[0]["Myab_buy_ac"].ToString();

                        if (_dt.Rows[0]["Myab_decision_rt"].ToString().Contains("1")) chkBrand.Checked = true;
                        if (_dt.Rows[0]["Myab_decision_rt"].ToString().Contains("2")) chkPrice.Checked = true;
                        if (_dt.Rows[0]["Myab_decision_rt"].ToString().Contains("3")) chkDisc.Checked = true;
                        if (_dt.Rows[0]["Myab_decision_rt"].ToString().Contains("4")) chkPromo.Checked = true;

                        if (_dt.Rows[0]["Myab_pay_method"].ToString() == "CASH") chkCash.Checked = true;
                        if (_dt.Rows[0]["Myab_pay_method"].ToString() == "CC") chkCC.Checked = true;
                        if (_dt.Rows[0]["Myab_pay_method"].ToString() == "HP") chkHP.Checked = true;

                        txtProf.Text = _dt.Rows[0]["Myab_pc"].ToString();
                        txtWorkAdd3.Text = _dt.Rows[0]["Myab_add3"].ToString();
                        //    txtPerTown.Text = _dt.Rows[0]["Myab_add4"].ToString();
                        txtOthRem1.Text = _dt.Rows[0]["Myab_oth_rem1"].ToString();
                        txtOthRem2.Text = _dt.Rows[0]["Myab_oth_rem2"].ToString();
                        if (Convert.ToBoolean(_dt.Rows[0]["Myab_need_card"]) == true) optNeedCard.Checked = true; else optNeedCardN.Checked = true;
                    }
                }
                //cmbTitle.Focus();
            }

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
                    //   btnCreate.Text = "Update";
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
                    //  btnCreate.Text = "Create";
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
                    _current_cust = custProf.Mbe_cd;

                    txtDL.ReadOnly = true;
                    txtPP.ReadOnly = true;
                    txtNIC.ReadOnly = true;
                    txtBR.ReadOnly = true;
                    // txtMob.ReadOnly = true;
                    Int32 _attemt = 0;
                    #region DL
                    if (_current_cust != txtCusCode.Text)
                    {
                        if (_isUpdate == true)
                        {
                            if (MessageBox.Show("Customer already exists for this  BR number, Do you want to recall the existing  customer details ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {

                                _isExsit = true;
                                //    btnCreate.Text = "Update";
                                LoadCustProf(custProf);
                                // Nadeeka

                            }
                            else
                            {
                                Int32 cutstVeri = CHNLSVC.General.GetCutomerValidationCode(txtMob.Text);

                                Int32 _enteredCode;
                            err:
                                _enteredCode = Convert.ToInt32(Interaction.InputBox("Pls enter valid verification code", "verification code", "", 400, 100));

                                if (cutstVeri == _enteredCode)
                                {
                                    _eff = CHNLSVC.General.UpdateCutomerMobile(txtBR.Text, "BR", out _err);
                                    if (_eff == -1)
                                    {
                                        MessageBox.Show(_err, "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    btnCreate_Click(null, null);
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
                            //    btnCreate.Text = "Update";
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
                                //   btnCreate.Text = "Create";
                                return;
                            }
                            else
                            {
                                _isGroup = false;
                            }
                            _isExsit = false;
                            //  btnCreate.Text = "Create";
                        }
                    }

                }
            }
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
                    _current_cust = custProf.Mbe_cd;
                    Int32 _attemt = 0;
                    txtDL.ReadOnly = true;
                    txtPP.ReadOnly = true;
                    txtNIC.ReadOnly = true;
                    txtBR.ReadOnly = true;
                    //  txtMob.ReadOnly = true;
                    #region PP
                    if (_current_cust != txtCusCode.Text)
                    {
                        if (_isUpdate == true)
                        {
                            if (MessageBox.Show("Customer already exists for this  Passport number, Do you want to recall the existing  customer details ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {

                                _isExsit = true;
                                //  btnCreate.Text = "Update";
                                LoadCustProf(custProf);
                                // Nadeeka

                            }
                            else
                            {
                                Int32 cutstVeri = CHNLSVC.General.GetCutomerValidationCode(txtMob.Text);

                                Int32 _enteredCode;
                            err:
                                _enteredCode = Convert.ToInt32(Interaction.InputBox("Pls enter valid verification code", "verification code", "", 400, 100));

                                if (cutstVeri == _enteredCode)
                                {
                                    _eff = CHNLSVC.General.UpdateCutomerMobile(txtPP.Text.Trim().ToUpper(), "PP", out _err);
                                    if (_eff == -1)
                                    {
                                        MessageBox.Show(_err, "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    btnCreate_Click(null, null);
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
                            //  btnCreate.Text = "Update";

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
                                //   btnCreate.Text = "Create";
                                return;
                            }
                            else
                            {
                                _isGroup = false;
                            }
                            _isExsit = false;
                            //  btnCreate.Text = "Create";
                        }
                    }

                }
            }
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
                    _current_cust = custProf.Mbe_cd;
                    txtDL.ReadOnly = true;
                    txtPP.ReadOnly = true;
                    txtNIC.ReadOnly = true;
                    txtBR.ReadOnly = true;
                    //    txtMob.ReadOnly = true;
                    Int32 _attemt = 0;
                    #region DL
                    if (_current_cust != txtCusCode.Text)
                    {
                        if (_isUpdate == true)
                        {
                            if (MessageBox.Show("Customer already exists for this  DL number, Do you want to recall the existing  customer details ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {

                                _isExsit = true;
                                //   btnCreate.Text = "Update";
                                LoadCustProf(custProf);
                                // Nadeeka

                            }
                            else
                            {
                                Int32 cutstVeri = CHNLSVC.General.GetCutomerValidationCode(txtMob.Text);

                                Int32 _enteredCode;
                            err:
                                _enteredCode = Convert.ToInt32(Interaction.InputBox("Pls enter valid verification code", "verification code", "", 400, 100));

                                if (cutstVeri == _enteredCode)
                                {
                                    _eff = CHNLSVC.General.UpdateCutomerMobile(txtDL.Text.Trim().ToUpper(), "DL", out _err);
                                    if (_eff == -1)
                                    {
                                        MessageBox.Show(_err, "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    btnCreate_Click(null, null);
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
                            //    btnCreate.Text = "Update";
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
                                //  btnCreate.Text = "Create";
                                return;
                            }
                            else
                            {
                                _isGroup = false;
                            }
                            _isExsit = false;
                            //  btnCreate.Text = "Create";
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
            //txtPerAdd1.Text = _cust.Mbg_add1;
            //txtPerAdd2.Text = _cust.Mbg_add2;
            //txtPerTown.Text = _cust.Mbg_town_cd;
            //txtPerPostal.Text = _cust.Mbg_postal_cd;
            //txtPerCountry.Text = _cust.Mbg_country_cd;
            //txtPerDistrict.Text = _cust.Mbg_distric_cd;
            //txtPerProvince.Text = _cust.Mbg_province_cd;
            txtPerPhone.Text = _cust.Mbg_tel;
            txtPerEmail.Text = _cust.Mbg_email;

            //txtPreAdd1.Text = _cust.Mbg_add1;
            //txtPreAdd2.Text = _cust.Mbg_add2;
            //txtPreTown.Text = _cust.Mbg_town_cd;
            //txtPrePostal.Text = _cust.Mbg_postal_cd;
            //txtPreCountry.Text = _cust.Mbg_country_cd;
            //txtPreDistrict.Text = _cust.Mbg_distric_cd;
            //txtPreProvince.Text = _cust.Mbg_province_cd;
            //txtPrePhone.Text = _cust.Mbg_tel;

            txtWorkAdd1.Text = "";
            txtWorkAdd2.Text = "";
            //  txtWorkName.Text = "";
            //  txtWorkDept.Text = "";
            txtWorkDesig.Text = "";
            //  txtWorkEmail.Text = "";
            //  txtWorkFax.Text = "";
            //  txtWorkPhone.Text = "";

            // chkVAT.Checked = false;
            // chkVatEx.Checked = false;
            //  chkSVAT.Checked = false;
            //  txtVatreg.Text = "";
            //  txtSVATReg.Text = "";



            //------------------------------------------

            txtCusCode.Enabled = false;


        }
        private DataTable _tbl = null;
        private void Loadfavourite()
        {
            _tbl = CHNLSVC.General.getFavouriteByCat("TV");
            cmbTV.DataSource = _tbl;
            cmbTV.DisplayMember = "fav_desc";
            cmbTV.ValueMember = "fav_cd";
            cmbTV.SelectedIndex = -1;

            _tbl = CHNLSVC.General.getFavouriteByCat("RD");
            cmbRadio.DataSource = _tbl;
            cmbRadio.DisplayMember = "fav_desc";
            cmbRadio.ValueMember = "fav_cd";
            cmbRadio.SelectedIndex = -1;

            _tbl = CHNLSVC.General.getFavouriteByCat("NP");
            cmbPaper.DataSource = _tbl;
            cmbPaper.DisplayMember = "fav_desc";
            cmbPaper.ValueMember = "fav_cd";
            cmbPaper.SelectedIndex = -1;
        }

        private void LoadLanguage()
        {
            _tbl = CHNLSVC.General.get_Language();
            cmbPrefLang.DataSource = _tbl;
            cmbPrefLang.DisplayMember = "mla_desc";
            cmbPrefLang.ValueMember = "mla_cd";
        }
        public void LoadCustProf(MasterBusinessEntity cust)
        {
            //string typed_nic = txtNicNo.Text.Trim();
            //string typed_ppno = txtPassportNo.Text.Trim().ToUpper();
            //string typed_dl = txtDLno.Text.Trim().ToUpper();
            //string typed_br = txtBrNo.Text.Trim().ToUpper();
            //------------------------------------------
            // ddlCustSupType.SelectedValue = cust.Mbe_sub_tp;
            //  cmbType.Text = cust.Mbe_cate;
            txtNIC.Text = cust.Mbe_nic;
            txtPP.Text = cust.Mbe_pp_no;
            txtBR.Text = cust.Mbe_br_no;
            txtCusCode.Text = cust.Mbe_cd;
            txtDL.Text = cust.Mbe_dl_no;
            txtMob.Text = cust.Mbe_mob;
            //   chkSMS.Checked = cust.Mbe_agre_send_sms;
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

            //txtPerAdd1.Text = cust.Mbe_add1;
            //txtPerAdd2.Text = cust.Mbe_add2;
            //txtPerTown.Text = cust.Mbe_town_cd;
            //txtPerPostal.Text = cust.Mbe_postal_cd;
            //txtPerCountry.Text = cust.Mbe_country_cd;
            //txtPerDistrict.Text = cust.Mbe_distric_cd;
            //txtPerProvince.Text = cust.Mbe_province_cd;
            txtPerPhone.Text = cust.Mbe_tel;
            txtPerEmail.Text = cust.Mbe_email;

            //txtPreAdd1.Text = cust.Mbe_cr_add1;
            //txtPreAdd2.Text = cust.Mbe_cr_add2;
            txtPreTown.Text = cust.Mbe_cr_town_cd;
            //txtPrePostal.Text = cust.Mbe_cr_postal_cd;
            //txtPreCountry.Text = cust.Mbe_cr_country_cd;
            //txtPreDistrict.Text = cust.Mbe_cr_distric_cd;
            //txtPreProvince.Text = cust.Mbe_cr_province_cd;
            // txtPrePhone.Text = cust.Mbe_cr_tel;

            txtWorkAdd1.Text = cust.Mbe_add1;
            txtWorkAdd2.Text = cust.Mbe_add2;
            //txtWorkName.Text = cust.Mbe_wr_com_name;
            //txtWorkDept.Text = cust.Mbe_wr_dept;
            txtWorkDesig.Text = cust.Mbe_wr_designation;
            //txtWorkEmail.Text = cust.Mbe_wr_email;
            //txtWorkFax.Text = cust.Mbe_wr_fax;
            //txtWorkPhone.Text = cust.Mbe_wr_tel;

            //chkVAT.Checked = cust.Mbe_is_tax;
            //chkVatEx.Checked = cust.Mbe_tax_ex;
            //chkSVAT.Checked = cust.Mbe_is_svat;
            //txtVatreg.Text = cust.Mbe_tax_no;
            //txtSVATReg.Text = cust.Mbe_svat_no;
            txtInit.Text = cust.MBE_INI;
            txtFname.Text = cust.MBE_FNAME;
            txtSName.Text = cust.MBE_SNAME;
            // Nadeeka 15-12-2014
            //    chkMail.Checked = cust.Mbe_agre_send_email;


            if (string.IsNullOrEmpty(cust.Mbe_cust_lang))
            {
                cmbPrefLang.SelectedValue = "E";

            }
            else
            {
                cmbPrefLang.SelectedValue = cust.Mbe_cust_lang;
            }


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
            try
            {

                string _cusCode = "";
                Int32 _effect = 0;

                if (string.IsNullOrEmpty(txtMob.Text))
                {
                    MessageBox.Show("Please enter mobile number.", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMob.Focus();
                    return;
                }

                //if (string.IsNullOrEmpty(txtSer.Text))
                //{
                //    MessageBox.Show("Please enter serial number.", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtSer.Focus();
                //    return;
                //}

                if (txtNIC.Text == "" && txtMob.Text == "")
                {
                    MessageBox.Show("One of required information not enterd.[NIC/Mobile #]", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNIC.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtFname.Text) && string.IsNullOrEmpty(txtSName.Text))
                {
                    MessageBox.Show("Please enter name of customer", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtName.Focus();
                    return;
                }


                if (string.IsNullOrEmpty(txtWorkAdd1.Text))
                {
                    MessageBox.Show("Please enter customer present address.", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtWorkAdd1.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPreTown.Text))
                {
                    MessageBox.Show("Please enter the town.", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPreTown.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtProf.Text))
                {
                    MessageBox.Show("Please enter the profit center.", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtProf.Focus();
                    return;
                }
                if (optNeedCard.Checked && string.IsNullOrEmpty(txtNameCard.Text))
                {
                    MessageBox.Show("Please enter name printed on card.", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNameCard.Focus();
                    return;
                }

                if (MessageBox.Show("Are you sure ?", "My Abans", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;


                Collect_Cust();
                Collect_GroupCust();

                string _decRate = "";
                string _method = "";

                MyAbans _myAb = new MyAbans();

                _myAb.Myab_seq = 0;
                _myAb.Myab_ser_no = txtSer.Text;
                _myAb.Myab_nic = txtNIC.Text;
                _myAb.Myab_tit = cmbTitle.Text.Trim();
                _myAb.Myab_fname = txtFname.Text;
                _myAb.Myab_sname = txtSName.Text;
                _myAb.Myab_stus = cmbStus.Text;
                _myAb.Myab_dob = Convert.ToDateTime(dtpDOB.Text).Date;
                _myAb.Myab_email = txtPerEmail.Text;
                _myAb.Myab_mob = txtMob.Text.Trim();
                _myAb.Myab_tel = txtPerPhone.Text.Trim();

                _myAb.Myab_wr_designation = txtWorkDesig.Text.Trim();
                _myAb.Myab_cust_lang = cmbPrefLang.SelectedValue.ToString();
                // _myAb.Myab_add1 = txtPreAdd1.Text.Trim();
                //  _myAb.Myab_add2 = txtPreAdd2.Text.Trim();

                _myAb.Myab_close_sr = txtCloseSR.Text;
                _myAb.Myab_tv_chnl = cmbTV.Text;
                _myAb.Myab_radio_chnl = cmbRadio.Text;
                _myAb.Myab_news_paper = cmbPaper.Text;
                _myAb.Myab_spouse = txtSpouse.Text;
                _myAb.Myab_spouse_dob = Convert.ToDateTime(dtpDOBS.Text).Date;
                _myAb.Myab_name_in_card = txtNameCard.Text;

                if (chkBrand.Checked) _decRate = "1";
                if (chkPrice.Checked) _decRate = _decRate + "2";
                if (chkDisc.Checked) _decRate = _decRate + "3";
                if (chkPromo.Checked) _decRate = _decRate + "4";
                _myAb.Myab_decision_rt = _decRate;

                if (chkCash.Checked) _method = chkCash.Tag.ToString();
                if (chkCC.Checked) _method = chkCC.Tag.ToString();
                if (chkHP.Checked) _method = chkHP.Tag.ToString();
                _myAb.Myab_pay_method = _method;

                _myAb.Myab_buy_online = optOnline.Checked ? true : false;
                _myAb.Myab_is_child = optChild.Checked ? true : false;
                _myAb.Myab_is_buy_ab = optBuy.Checked ? true : false;
                _myAb.Myab_is_need_card_sp = optSP.Checked ? 1 : 0;

                if (!string.IsNullOrEmpty(txtDOB1.Text)) _myAb.Myab_bd_ch1 = Convert.ToDateTime(txtDOB1.Text).Date;
                if (!string.IsNullOrEmpty(txtDOB2.Text)) _myAb.Myab_bd_ch2 = Convert.ToDateTime(txtDOB2.Text).Date;
                if (!string.IsNullOrEmpty(txtDOB3.Text)) _myAb.Myab_bd_ch3 = Convert.ToDateTime(txtDOB3.Text).Date;
                if (!string.IsNullOrEmpty(txtDOB4.Text)) _myAb.Myab_bd_ch4 = Convert.ToDateTime(txtDOB4.Text).Date;

                _myAb.Myab_use_tv = chkTV.Checked ? true : false;
                _myAb.Myab_use_tv_yr = !string.IsNullOrEmpty(txtTVY.Text) ? Convert.ToInt32(txtTVY.Text) : Convert.ToInt32(0);
                _myAb.Myab_use_fr = chkFR.Checked ? true : false;
                _myAb.Myab_use_fr_yr = !string.IsNullOrEmpty(txtFRY.Text) ? Convert.ToInt32(txtFRY.Text) : Convert.ToInt32(0);
                _myAb.Myab_use_wm = chkWM.Checked ? true : false;
                _myAb.Myab_use_wm_yr = !string.IsNullOrEmpty(txtWMY.Text) ? Convert.ToInt32(txtWMY.Text) : Convert.ToInt32(0);
                _myAb.Myab_use_lap = chkLP.Checked ? true : false;
                _myAb.Myab_use_lap_yr = !string.IsNullOrEmpty(txtLPY.Text) ? Convert.ToInt32(txtLPY.Text) : Convert.ToInt32(0);
                _myAb.Myab_use_tab = chkTB.Checked ? true : false;
                _myAb.Myab_use_tab_yr = !string.IsNullOrEmpty(txtTBY.Text) ? Convert.ToInt32(txtTBY.Text) : Convert.ToInt32(0);
                _myAb.Myab_use_dtop = chkDT.Checked ? true : false;
                _myAb.Myab_use_dtop_yr = !string.IsNullOrEmpty(txtDTY.Text) ? Convert.ToInt32(txtDTY.Text) : Convert.ToInt32(0);
                _myAb.Myab_use_sp = chkSP.Checked ? true : false;
                _myAb.Myab_use_sp_yr = !string.IsNullOrEmpty(txtSPY.Text) ? Convert.ToInt32(txtSPY.Text) : Convert.ToInt32(0);
                _myAb.Myab_use_mo = chkMO.Checked ? true : false;
                _myAb.Myab_use_mo_yr = !string.IsNullOrEmpty(txtMOY.Text) ? Convert.ToInt32(txtMOY.Text) : Convert.ToInt32(0);
                // _myAb.Myab_use_ck_gas = chkTV.Checked ? true : false;
                //_myAb.Myab_use_ck_gas_yr =
                _myAb.Myab_use_ck_dtop = chkDTC.Checked ? true : false;
                _myAb.Myab_use_ck_dtop_yr = !string.IsNullOrEmpty(txtDTCY.Text) ? Convert.ToInt32(txtDTCY.Text) : Convert.ToInt32(0);
                _myAb.Myab_use_mg = chkOTH.Checked ? true : false;
                _myAb.Myab_use_mg_yr = !string.IsNullOrEmpty(txtOTHY.Text) ? Convert.ToInt32(txtOTHY.Text) : Convert.ToInt32(0);
                _myAb.Myab_use_hifi = chkHIFI.Checked ? true : false;
                _myAb.Myab_use_hifi_yr = !string.IsNullOrEmpty(txtHIFIY.Text) ? Convert.ToInt32(txtHIFIY.Text) : Convert.ToInt32(0);
                _myAb.Myab_use_ac = chkAC.Checked ? true : false;
                _myAb.Myab_use_ac_yr = !string.IsNullOrEmpty(txtACY.Text) ? Convert.ToInt32(txtACY.Text) : Convert.ToInt32(0);

                _myAb.Myab_buy_tv = string.IsNullOrEmpty(txtTV.Text) ? 0 : Convert.ToInt32(txtTV.Text);
                _myAb.Myab_buy_fr = string.IsNullOrEmpty(txtFR.Text) ? 0 : Convert.ToInt32(txtFR.Text);
                _myAb.Myab_buy_wm = string.IsNullOrEmpty(txtWM.Text) ? 0 : Convert.ToInt32(txtWM.Text);
                _myAb.Myab_buy_pc = string.IsNullOrEmpty(txtPC.Text) ? 0 : Convert.ToInt32(txtPC.Text);
                _myAb.Myab_buy_sp = string.IsNullOrEmpty(txtSP.Text) ? 0 : Convert.ToInt32(txtSP.Text);
                _myAb.Myab_buy_ck = string.IsNullOrEmpty(txtMO.Text) ? 0 : Convert.ToInt32(txtMO.Text);
                _myAb.Myab_buy_ck_dtop = string.IsNullOrEmpty(txtDTC.Text) ? 0 : Convert.ToInt32(txtDTC.Text);
                _myAb.Myab_buy_mg = string.IsNullOrEmpty(txtOTH.Text) ? 0 : Convert.ToInt32(txtOTH.Text);
                _myAb.Myab_buy_hifi = string.IsNullOrEmpty(txtHIFI.Text) ? 0 : Convert.ToInt32(txtHIFI.Text);
                _myAb.Myab_buy_ac = string.IsNullOrEmpty(txtAC.Text) ? 0 : Convert.ToInt32(txtAC.Text);

                _myAb.Myab_dt = Convert.ToDateTime(dtpDate.Value).Date;

                _myAb.Myab_com = BaseCls.GlbUserComCode;
                _myAb.Myab_pc = txtProf.Text;
                _myAb.Myab_add3 = txtWorkAdd3.Text;
                //  _myAb.Myab_add4 = txtPerTown.Text;
                _myAb.Myab_oth_rem1 = txtOthRem1.Text;
                _myAb.Myab_oth_rem2 = txtOthRem2.Text;
                _myAb.Myab_need_card = optNeedCard.Checked ? true : false;

                LoyaltyMemeber _member = new LoyaltyMemeber();
                _member.Salcm_loty_tp = "MYAB";
                _member.Salcm_email = txtPerEmail.Text;
                _member.Salcm_contact = txtMob.Text;
                _member.Salcm_cus_cd = txtCusCode.Text;
                _member.Salcm_dis_rt = 0;
                _member.Salcm_val_frm = DateTime.Now.Date;
                _member.Salcm_val_to = DateTime.Now.Date;
                _member.Salcm_cre_by = BaseCls.GlbUserID;
                _member.Salcm_cre_dt = DateTime.Now.Date;
                _member.Salcm_app_by = BaseCls.GlbUserID;
                _member.Salcm_app_dt = DateTime.Now.Date;
                _member.Salcm_bal_pt = 0;
                _member.Salcm_cd_ser = txtSer.Text;
                _member.Salcm_cus_spec = "CLASSIC";
                _member.Salcm_no = txtSer.Text;


                List<BusEntityItem> _lstBusItm = new List<BusEntityItem>();
                _effect = CHNLSVC.Sales.SaveMyAbansDetails(_myAb, _custProfile, _custGroup, _member, _isExsit);

                if (_effect == 1)
                {
                    if (_isExsit == false)
                    {
                        MessageBox.Show("Successfully Saved ", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Successfully updated.", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


                this.Clear_Data();
                txtSer.Text = "";
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
            //_custGroup.Mbg_add1 = txtPreAdd1.Text.Trim();
            //_custGroup.Mbg_add2 = txtPreAdd2.Text.Trim();
            //_custGroup.Mbg_town_cd = txtPerTown.Text.Trim();
            //_custGroup.Mbg_distric_cd = txtPerDistrict.Text.Trim();
            //_custGroup.Mbg_province_cd = txtPerProvince.Text.Trim();
            //_custGroup.Mbg_country_cd = txtPerCountry.Text.Trim();
            _custGroup.Mbg_tel = txtPerPhone.Text.Trim();
            _custGroup.Mbg_fax = "";
            // _custGroup.Mbg_postal_cd = txtPerPostal.Text.Trim();
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
            _custProfile.Mbe_add1 = txtWorkAdd1.Text.Trim();
            _custProfile.Mbe_add2 = txtWorkAdd2.Text.Trim();
            //if (chkSMS.Checked == true)
            //{
            //    _isSMS = true;
            //}
            //else
            //{
            _isSMS = false;
            // }
            _custProfile.Mbe_agre_send_sms = _isSMS;
            _custProfile.Mbe_br_no = txtBR.Text.Trim();
            _custProfile.Mbe_cate = "INDIVIDUAL";
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
            //_custProfile.Mbe_country_cd = txtPerCountry.Text.Trim();
            //_custProfile.Mbe_cr_add1 = txtPreAdd1.Text.Trim();
            //_custProfile.Mbe_cr_add2 = txtPreAdd2.Text.Trim();
            // _custProfile.Mbe_cr_country_cd = txtPreCountry.Text.Trim();
            // _custProfile.Mbe_cr_distric_cd = txtPreDistrict.Text;
            _custProfile.Mbe_cr_email = null;
            _custProfile.Mbe_cr_fax = null;
            //_custProfile.Mbe_cr_postal_cd = txtPrePostal.Text.Trim();
            //_custProfile.Mbe_cr_province_cd = txtPreProvince.Text.Trim();
            //_custProfile.Mbe_cr_tel = txtPrePhone.Text.Trim();
            _custProfile.Mbe_cr_town_cd = txtPreTown.Text.Trim();
            _custProfile.Mbe_cre_by = BaseCls.GlbUserID;
            _custProfile.Mbe_cre_dt = Convert.ToDateTime(DateTime.Today).Date;
            _custProfile.Mbe_cre_pc = BaseCls.GlbUserDefProf;
            _custProfile.Mbe_cust_com = BaseCls.GlbUserComCode;
            _custProfile.Mbe_cust_loc = BaseCls.GlbUserDefLoca;
            //   _custProfile.Mbe_distric_cd = txtPerDistrict.Text;
            _custProfile.Mbe_dl_no = txtDL.Text.Trim();
            _custProfile.Mbe_dob = Convert.ToDateTime(dtpDOB.Text).Date;
            _custProfile.Mbe_email = txtPerEmail.Text.Trim();
            _custProfile.Mbe_fax = null;
            _custProfile.Mbe_ho_stus = "GOOD";
            _custProfile.Mbe_income_grup = null;
            _custProfile.Mbe_intr_com = false;
            _custProfile.Mbe_is_suspend = false;

            //if (chkSVAT.Checked == true)
            //{
            //    _isSVAT = true;
            //}
            //else
            //{
            _isSVAT = false;
            //}

            _custProfile.Mbe_is_svat = _isSVAT;

            //if (chkVAT.Checked == true)
            //{
            //    _isVAT = true;
            //}
            //else
            //{
            _isVAT = false;
            //}
            _custProfile.Mbe_is_tax = _isVAT;
            _custProfile.Mbe_mob = txtMob.Text.Trim();
            _custProfile.Mbe_name = txtFname.Text.Trim() + " " + txtSName.Text.Trim();
            _custProfile.Mbe_nic = txtNIC.Text.Trim();
            _custProfile.Mbe_oth_id_no = null;
            _custProfile.Mbe_oth_id_tp = null;
            _custProfile.Mbe_pc_stus = "GOOD";
            // _custProfile.Mbe_postal_cd = txtPerPostal.Text.Trim();
            _custProfile.Mbe_pp_no = txtPP.Text.Trim();
            //  _custProfile.Mbe_province_cd = txtPerProvince.Text.Trim();
            _custProfile.Mbe_sex = cmbSex.Text;
            _custProfile.Mbe_sub_tp = null;
            //  _custProfile.Mbe_svat_no = txtSVATReg.Text.Trim();

            //if (chkVatEx.Checked == true)
            //{
            //    _TaxEx = true;
            //}
            //else
            //{
            _TaxEx = false;
            //}
            _custProfile.Mbe_tax_ex = _TaxEx;
            //  _custProfile.Mbe_tax_no = txtVatreg.Text.Trim();
            _custProfile.Mbe_tel = txtPerPhone.Text.Trim();
            // _custProfile.Mbe_town_cd = txtPerTown.Text.Trim();
            _custProfile.Mbe_tp = "C";
            _custProfile.Mbe_wr_add1 = txtWorkAdd1.Text.Trim();
            _custProfile.Mbe_wr_add2 = txtWorkAdd2.Text.Trim();
            //  _custProfile.Mbe_wr_com_name = txtWorkName.Text.Trim();
            _custProfile.Mbe_wr_country_cd = null;
            // _custProfile.Mbe_wr_dept = txtWorkDept.Text.Trim();
            _custProfile.Mbe_wr_designation = txtWorkDesig.Text.Trim();
            _custProfile.Mbe_wr_distric_cd = null;
            //  _custProfile.Mbe_wr_email = txtWorkEmail.Text.Trim();
            //  _custProfile.Mbe_wr_fax = txtWorkFax.Text.Trim();
            _custProfile.Mbe_wr_proffesion = null;
            _custProfile.Mbe_wr_province_cd = null;
            //  _custProfile.Mbe_wr_tel = txtWorkPhone.Text.Trim();
            _custProfile.Mbe_wr_town_cd = null;
            _custProfile.MBE_FNAME = txtFname.Text.Trim();
            _custProfile.MBE_SNAME = txtSName.Text.Trim();
            _custProfile.MBE_INI = txtInit.Text.Trim();
            _custProfile.MBE_TIT = cmbTitle.Text.Trim();
            //if (chkMail.Checked == true)
            //{
            //    _isEmail = true;
            //}
            //else
            //{
            _isEmail = false;
            //  }
            // Nadeeka 15-12-2014
            _custProfile.Mbe_agre_send_email = _isEmail;
            _custProfile.Mbe_cust_lang = cmbPrefLang.SelectedValue.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Clear_Data();
            txtSer.Text = "";
            txtCustCode.Text = "";
        }

        private void chkVat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void txtVatreg_KeyDown(object sender, KeyEventArgs e)
        {

        }


        private void chkVAT_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkSVAT_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkSVAT_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtSVATReg_KeyDown(object sender, KeyEventArgs e)
        {


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
                MessageBox.Show("One of required information not enterd.", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNIC.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please enter name of customer", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("New customer created. Customer Code : " + _cusCode, "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Exsiting customer updated.", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (_isFromOther == true)
            {
                HP.AccountCreation _acc = new HP.AccountCreation();
                obj_TragetTextBox.Text = _cusCode;
                this.Close();
            }
            this.Clear_Data();
            txtSer.Text = "";
        }

        private void txtInit_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtFname_KeyDown(object sender, KeyEventArgs e)
        {

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
                MessageBox.Show("Please enter customer mobile #", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMob.Focus();
                return;
            }
            txtDL.ReadOnly = true;
            txtPP.ReadOnly = true;
            txtNIC.ReadOnly = true;
            txtBR.ReadOnly = false;
            //   txtMob.ReadOnly = true;
            _isUpdate = true;
        }

        private void txtNIC_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMob.Text))
            {
                MessageBox.Show("Please enter customer mobile #", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMob.Focus();
                return;
            }
            txtDL.ReadOnly = true;
            txtPP.ReadOnly = true;
            txtNIC.ReadOnly = false; ;
            txtBR.ReadOnly = true;
            //   txtMob.ReadOnly = true;
            _isUpdate = true;
        }

        private void txtPP_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMob.Text))
            {
                MessageBox.Show("Please enter customer mobile #", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMob.Focus();
                return;
            }
            txtDL.ReadOnly = true;
            txtPP.ReadOnly = false; ;
            txtNIC.ReadOnly = true;
            txtBR.ReadOnly = true;
            //txtMob.ReadOnly = true;
            _isUpdate = true;
        }

        private void txtDL_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMob.Text))
            {
                MessageBox.Show("Please enter customer mobile #", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMob.Focus();
                return;
            }
            txtDL.ReadOnly = false; ;
            txtPP.ReadOnly = true;
            txtNIC.ReadOnly = true;
            txtBR.ReadOnly = true;
            // txtMob.ReadOnly = true;
            _isUpdate = true;
        }

        private void txtPP_TextChanged(object sender, EventArgs e)
        {

        }


        private void MyAbansEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtSer_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSer.Text))
            {
                DataTable _dt = CHNLSVC.General.getMyAbansBySerial(txtSer.Text, 0);
                if (_dt.Rows.Count > 0)
                {
                    txtNIC.Text = _dt.Rows[0]["MYAB_NIC"].ToString();
                    MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, string.Empty, txtNIC.Text, string.Empty, "C");
                    LoadCustProf(_masterBusinessCompany);

                    if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_sp"]) == true) chkSP.Checked = true;
                    if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_fr"]) == true) chkFR.Checked = true;
                    if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_wm"]) == true) chkWM.Checked = true;
                    if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_ck_dtop"]) == true) chkDTC.Checked = true;
                    if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_mg"]) == true) chkOTH.Checked = true;
                    if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_ac"]) == true) chkAC.Checked = true;
                    if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_hifi"]) == true) chkHIFI.Checked = true;
                    if (Convert.ToBoolean(_dt.Rows[0]["MYAB_USE_LAP"]) == true) chkLP.Checked = true;
                    if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_tab"]) == true) chkTB.Checked = true;
                    if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_dtop"]) == true) chkDT.Checked = true;
                    if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_tv"]) == true) chkTV.Checked = true;
                    if (Convert.ToBoolean(_dt.Rows[0]["Myab_use_mo"]) == true) chkMO.Checked = true;

                    if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_TV_YR"].ToString())) txtTVY.Text = _dt.Rows[0]["MYAB_USE_TV_YR"].ToString();
                    if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_FR_YR"].ToString())) txtFRY.Text = _dt.Rows[0]["MYAB_USE_FR_YR"].ToString();
                    if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_WM_YR"].ToString())) txtWMY.Text = _dt.Rows[0]["MYAB_USE_WM_YR"].ToString();
                    if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_LAP_YR"].ToString())) txtLPY.Text = _dt.Rows[0]["MYAB_USE_LAP_YR"].ToString();
                    if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_TAB_YR"].ToString())) txtTBY.Text = _dt.Rows[0]["MYAB_USE_TAB_YR"].ToString();
                    if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_DTOP_YR"].ToString())) txtDTY.Text = _dt.Rows[0]["MYAB_USE_DTOP_YR"].ToString();
                    if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_SP_YR"].ToString())) txtSPY.Text = _dt.Rows[0]["MYAB_USE_SP_YR"].ToString();
                    if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_CK_DTOP_YR"].ToString())) txtDTCY.Text = _dt.Rows[0]["MYAB_USE_CK_DTOP_YR"].ToString();
                    if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_MG_YR"].ToString())) txtOTHY.Text = _dt.Rows[0]["MYAB_USE_MG_YR"].ToString();
                    if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_HIFI_YR"].ToString())) txtHIFIY.Text = _dt.Rows[0]["MYAB_USE_HIFI_YR"].ToString();
                    if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_AC_YR"].ToString())) txtACY.Text = _dt.Rows[0]["MYAB_USE_AC_YR"].ToString();
                    if (!string.IsNullOrEmpty(_dt.Rows[0]["MYAB_USE_MO_YR"].ToString())) txtMOY.Text = _dt.Rows[0]["MYAB_USE_MO_YR"].ToString();

                    if (Convert.ToBoolean(_dt.Rows[0]["Myab_buy_online"]) == true) optOnline.Checked = true; else optOnlineN.Checked = true;
                    if (Convert.ToBoolean(_dt.Rows[0]["Myab_is_child"]) == true) optChild.Checked = true; else optChildN.Checked = true;
                    if (Convert.ToBoolean(_dt.Rows[0]["Myab_is_buy_ab"]) == true) optBuy.Checked = true; else optBuyN.Checked = true;
                    if (Convert.ToBoolean(_dt.Rows[0]["Myab_is_need_card_sp"]) == true) optSP.Checked = true; else optSPN.Checked = true;


                    if (!string.IsNullOrEmpty(_dt.Rows[0]["Myab_bd_ch1"].ToString()) && _dt.Rows[0]["Myab_bd_ch1"].ToString() != "01/Jan/0001 12:00:00 AM") txtDOB1.Text = _dt.Rows[0]["Myab_bd_ch1"].ToString();
                    if (!string.IsNullOrEmpty(_dt.Rows[0]["Myab_bd_ch2"].ToString()) && _dt.Rows[0]["Myab_bd_ch2"].ToString() != "01/Jan/0001 12:00:00 AM") txtDOB2.Text = _dt.Rows[0]["Myab_bd_ch2"].ToString();
                    if (!string.IsNullOrEmpty(_dt.Rows[0]["Myab_bd_ch3"].ToString()) && _dt.Rows[0]["Myab_bd_ch3"].ToString() != "01/Jan/0001 12:00:00 AM") txtDOB3.Text = _dt.Rows[0]["Myab_bd_ch3"].ToString();
                    if (!string.IsNullOrEmpty(_dt.Rows[0]["Myab_bd_ch4"].ToString()) && _dt.Rows[0]["Myab_bd_ch4"].ToString() != "01/Jan/0001 12:00:00 AM") txtDOB4.Text = _dt.Rows[0]["Myab_bd_ch4"].ToString();

                    txtCloseSR.Text = _dt.Rows[0]["Myab_close_sr"].ToString();
                    cmbTV.Text = _dt.Rows[0]["Myab_tv_chnl"].ToString();
                    cmbRadio.Text = _dt.Rows[0]["Myab_radio_chnl"].ToString();
                    cmbPaper.Text = _dt.Rows[0]["Myab_news_paper"].ToString();
                    txtSpouse.Text = _dt.Rows[0]["Myab_spouse"].ToString();
                    dtpDOBS.Value = Convert.ToDateTime(_dt.Rows[0]["Myab_spouse_dob"]);
                    txtNameCard.Text = _dt.Rows[0]["Myab_name_in_card"].ToString();

                    txtTV.Text = _dt.Rows[0]["Myab_buy_tv"].ToString();
                    txtFR.Text = _dt.Rows[0]["Myab_buy_fr"].ToString();
                    txtWM.Text = _dt.Rows[0]["Myab_buy_wm"].ToString();
                    txtPC.Text = _dt.Rows[0]["Myab_buy_pc"].ToString();
                    txtSP.Text = _dt.Rows[0]["Myab_buy_sp"].ToString();
                    txtMO.Text = _dt.Rows[0]["Myab_buy_ck"].ToString();
                    txtDTC.Text = _dt.Rows[0]["Myab_buy_ck_dtop"].ToString();
                    txtOTH.Text = _dt.Rows[0]["Myab_buy_mg"].ToString();
                    txtHIFI.Text = _dt.Rows[0]["Myab_buy_hifi"].ToString();
                    txtAC.Text = _dt.Rows[0]["Myab_buy_ac"].ToString();

                    if (_dt.Rows[0]["Myab_decision_rt"].ToString().Contains("1")) chkBrand.Checked = true;
                    if (_dt.Rows[0]["Myab_decision_rt"].ToString().Contains("2")) chkPrice.Checked = true;
                    if (_dt.Rows[0]["Myab_decision_rt"].ToString().Contains("3")) chkDisc.Checked = true;
                    if (_dt.Rows[0]["Myab_decision_rt"].ToString().Contains("4")) chkPromo.Checked = true;

                    if (_dt.Rows[0]["Myab_pay_method"].ToString() == "CASH") chkCash.Checked = true;
                    if (_dt.Rows[0]["Myab_pay_method"].ToString() == "CC") chkCC.Checked = true;
                    if (_dt.Rows[0]["Myab_pay_method"].ToString() == "HP") chkHP.Checked = true;

                    txtProf.Text = _dt.Rows[0]["Myab_pc"].ToString();
                    txtWorkAdd3.Text = _dt.Rows[0]["Myab_add3"].ToString();
                    //txtPerTown.Text = _dt.Rows[0]["Myab_add4"].ToString();
                    txtOthRem1.Text = _dt.Rows[0]["Myab_oth_rem1"].ToString();
                    txtOthRem2.Text = _dt.Rows[0]["Myab_oth_rem2"].ToString();
                    if (Convert.ToBoolean(_dt.Rows[0]["Myab_need_card"]) == true) optNeedCard.Checked = true; else optNeedCardN.Checked = true;

                    _isExsit = true;
                    txtNIC.Focus();
                    //  btnCreate.Text = "Update";
                }
                else
                    Clear_Data();
            }
            else
            {
                Clear_Data();
            }

        }

        private void txtSer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSer_Leave(null, null);
                //  txtNIC.Focus();
            }
        }

        private void txtTVY_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTVY.Text))
                if (txtTVY.Text.Length != 4)
                {
                    MessageBox.Show("Invalid year", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTVY.Text = "";
                    txtTVY.Focus();
                }
        }

        private void txtFRY_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFRY.Text))
                if (txtFRY.Text.Length != 4)
                {
                    MessageBox.Show("Invalid year", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFRY.Text = "";
                    txtFRY.Focus();
                }
        }

        private void txtWMY_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWMY.Text))
                if (txtWMY.Text.Length != 4)
                {
                    MessageBox.Show("Invalid year", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtWMY.Text = "";
                    txtWMY.Focus();
                }
        }

        private void txtLPY_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLPY.Text))
                if (txtLPY.Text.Length != 4)
                {
                    MessageBox.Show("Invalid year", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtLPY.Text = "";
                    txtLPY.Focus();
                }
        }

        private void txtTBY_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTBY.Text))
                if (txtTBY.Text.Length != 4)
                {
                    MessageBox.Show("Invalid year", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTBY.Text = "";
                    txtTBY.Focus();
                }
        }

        private void txtDTY_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDTY.Text))
                if (txtDTY.Text.Length != 4)
                {
                    MessageBox.Show("Invalid year", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDTY.Text = "";
                    txtDTY.Focus();
                }
        }

        private void txtSPY_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSPY.Text))
                if (txtSPY.Text.Length != 4)
                {
                    MessageBox.Show("Invalid year", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSPY.Text = "";
                    txtSPY.Focus();
                }
        }

        private void txtMOY_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMOY.Text))
                if (txtMOY.Text.Length != 4)
                {
                    MessageBox.Show("Invalid year", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMOY.Text = "";
                    txtMOY.Focus();
                }
        }

        private void txtDTCY_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDTCY.Text))
                if (txtDTCY.Text.Length != 4)
                {
                    MessageBox.Show("Invalid year", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDTCY.Text = "";
                    txtDTCY.Focus();
                }
        }

        private void txtHIFIY_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtHIFIY.Text))
                if (txtHIFIY.Text.Length != 4)
                {
                    MessageBox.Show("Invalid year", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtHIFIY.Text = "";
                    txtHIFIY.Focus();
                }
        }

        private void txtACY_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtACY.Text))
                if (txtACY.Text.Length != 4)
                {
                    MessageBox.Show("Invalid year", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtACY.Text = "";
                    txtACY.Focus();
                }
        }

        private void txtOTHY_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOTHY.Text))
                if (txtOTHY.Text.Length != 4)
                {
                    MessageBox.Show("Invalid year", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOTHY.Text = "";
                    txtOTHY.Focus();
                }
        }

        private void chkCC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCC.Checked == true)
            {
                chkCash.Checked = false;
                chkHP.Checked = false;
            }
        }

        private void chkCash_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCash.Checked == true)
            {
                chkCC.Checked = false;
                chkHP.Checked = false;
            }
        }

        private void chkHP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHP.Checked == true)
            {
                chkCash.Checked = false;
                chkCC.Checked = false;
            }
        }

        private void chkTV_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTV.Checked == true)
            {
                txtTVY.Enabled = true;
                txtTVY.Focus();
            }
            else
            {
                txtTVY.Text = "";
                txtTVY.Enabled = false;
            }
        }

        private void chkFR_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFR.Checked == true)
            {
                txtFR.Enabled = true;
                txtFRY.Focus();
            }
            else
            {
                txtFRY.Text = "";
                txtFRY.Enabled = false;
            }
        }

        private void chkWM_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWM.Checked == true)
            {
                txtWMY.Enabled = true;
                txtWMY.Focus();
            }
            else
            {
                txtWMY.Text = "";
                txtWMY.Enabled = false;
            }
        }

        private void chkLP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLP.Checked == true)
            {
                txtLPY.Enabled = true;
                txtLPY.Focus();
            }
            else
            {
                txtLPY.Text = "";
                txtLPY.Enabled = false;
            }
        }

        private void chkTB_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTB.Checked == true)
            {
                txtTBY.Enabled = true;
                txtTBY.Focus();
            }
            else
            {
                txtTBY.Text = "";
                txtTBY.Enabled = false;
            }
        }

        private void chkDT_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDT.Checked == true)
            {
                txtDTY.Enabled = true;
                txtDTY.Focus();
            }
            else
            {
                txtDTY.Text = "";
                txtDTY.Enabled = false;
            }
        }

        private void chkSP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSP.Checked == true)
            {
                txtSPY.Enabled = true;
                txtSPY.Focus();
            }
            else
            {
                txtSPY.Text = "";
                txtSPY.Enabled = false;
            }
        }

        private void chkMO_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMO.Checked == true)
            {
                txtMOY.Enabled = true;
                txtMOY.Focus();
            }
            else
            {
                txtMOY.Text = "";
                txtMOY.Enabled = false;
            }
        }

        private void chkDTC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDTC.Checked == true)
            {
                txtDTCY.Enabled = true;
                txtDTCY.Focus();
            }
            else
            {
                txtDTCY.Text = "";
                txtDTCY.Enabled = false;
            }
        }

        private void chkHIFI_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHIFI.Checked == true)
            {
                txtHIFIY.Enabled = true;
                txtHIFIY.Focus();
            }
            else
            {
                txtHIFIY.Text = "";
                txtHIFIY.Enabled = false;
            }
        }

        private void chkAC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAC.Checked == true)
            {
                txtACY.Enabled = true;
                txtACY.Focus();
            }
            else
            {
                txtACY.Text = "";
                txtACY.Enabled = false;
            }
        }

        private void chkOTH_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOTH.Checked == true)
            {
                txtOTHY.Enabled = true;
                txtOTHY.Focus();
            }
            else
            {
                txtOTHY.Text = "";
                txtOTHY.Enabled = false;
            }
        }

        private void btn_srch_dic_pc_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtProf;
            _CommonSearch.ShowDialog();
            //TextBoxLocation.Select();
            txtProf.Focus();
        }

        private void txtProf_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProf.Text))
            {
                Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, txtProf.Text);
                if (_IsValid == false)
                {
                    MessageBox.Show("Invalid Profit Center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtProf.Text = "";
                    txtProf.Focus();
                    return;
                }
            }
        }

        private void txtPreTown_Leave_1(object sender, EventArgs e)
        {

        }

        private void btnPerTown_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void dtpDate_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtPreTown_KeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void txtTV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                e.Handled = !char.IsNumber(e.KeyChar);
            }
        }

        private void txtFR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                e.Handled = !char.IsNumber(e.KeyChar);
            }
        }

        private void txtWM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                e.Handled = !char.IsNumber(e.KeyChar);
            }
        }

        private void txtPC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                e.Handled = !char.IsNumber(e.KeyChar);
            }
        }

        private void txtSP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                e.Handled = !char.IsNumber(e.KeyChar);
            }
        }

        private void txtMO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                e.Handled = !char.IsNumber(e.KeyChar);
            }
        }

        private void txtDTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                e.Handled = !char.IsNumber(e.KeyChar);
            }
        }

        private void txtHIFI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                e.Handled = !char.IsNumber(e.KeyChar);
            }
        }

        private void txtAC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                e.Handled = !char.IsNumber(e.KeyChar);
            }
        }

        private void txtOTH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                e.Handled = !char.IsNumber(e.KeyChar);
            }
        }

        private void cmbStus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStus.Text == "MARRIED")
            {
                txtSpouse.Enabled = true;
                dtpDOBS.Enabled = true;
            }
            else
            {
                txtSpouse.Enabled = false;
                dtpDOBS.Enabled = false;
            }
        }

        private void btn_srch_cust_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommonByNIC);
            DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommonByNIC(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCustCode;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtCustCode.Select();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                General.CustomerCreation _CusCre = new General.CustomerCreation();
                _CusCre._isFromOther = true;
                _CusCre.obj_TragetTextBox = txtCustCode;
                this.Cursor = Cursors.Default;
                _CusCre.ShowDialog();
                txtCustCode.Select();

            }
            catch (Exception ex)
            { txtNIC.Clear(); this.Cursor = Cursors.Default; MessageBox.Show("Error", "My Abans", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtCustCode_Leave(object sender, EventArgs e)
        {
            MasterBusinessEntity custProf = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustCode.Text, null, null, null, null, BaseCls.GlbUserComCode);
            if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
            {
                _isExsit = true;
                //  btnCreate.Text = "Update";
                if (!string.IsNullOrEmpty(txtNIC.Text))
                    dobGeneration();
                LoadCustProf(custProf);
                txtDL.ReadOnly = true;
                txtPP.ReadOnly = true;
                txtNIC.ReadOnly = true;
                txtBR.ReadOnly = true;
                //  txtMob.ReadOnly = true;

                Int32 _attemt = 0;





            }
            else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
            {
                MessageBox.Show("Customer is inactivated.Please contact accounts dept.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnCreate.Enabled = false;
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

                //Check the group level
                GroupBussinessEntity _grupProf = GetbyNICGrup(txtNIC.Text.Trim().ToUpper());
                if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                {
                    LoadCustProfByGrup(_grupProf);
                    _isGroup = true;
                    _isExsit = false;
                    //   btnCreate.Text = "Create";
                    return;
                }
                else
                {
                    _isGroup = false;
                }

                _isExsit = false;
                //   btnCreate.Text = "Create";
                Int32 bornDate = 0;
                Int32 dobYear = 0;
                Int32 dobMon = 0;
                if (!string.IsNullOrEmpty(txtNIC.Text))
                {
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

                    foreach (var itm in monthDict2)
                    {
                        if (itm.Key == bornMonth)
                        {
                            dobMon = itm.Value;
                        }
                    }

                    if (nic_.Length == 10)
                        dobYear = 1900 + Convert.ToInt32((nicarray[0].ToString())) * 10 + Convert.ToInt32((nicarray[1].ToString()));
                    else if (nic_.Length == 12)
                        dobYear = 1900 + Convert.ToInt32((nicarray[2].ToString())) * 10 + Convert.ToInt32((nicarray[3].ToString()));
                }
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
