using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.AbansTours.UserControls;
using FF.BusinessObjects;

namespace FF.AbansTours.DataEnty
{
    public partial class CustomerCreation : BasePage
    {
        private BasePage _basePage;
        private DataTable _tbl = null;

        private MasterBusinessEntity _custProfile = new MasterBusinessEntity();
        private GroupBussinessEntity _custGroup = new GroupBussinessEntity();
        private CustomerAccountRef _account = new CustomerAccountRef();
        private List<MasterBusinessEntityInfo> _busInfoList = new List<MasterBusinessEntityInfo>();

        public TextBox obj_TragetTextBox;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["UserID"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserCompanyCode"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserDefProf"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserDefLoca"])))
                {
                    if (!IsPostBack)
                    {
                        _basePage = new BasePage();
                        LoadLanguage();
                        LoadCustomerType();
                        loadTitles();
                        loadSex();
                        viewBackBtn();
                    }
                }
                else
                {
                    //string gotoURL = "http://" + System.Web.HttpContext.Current.Request.Url.Host + @"/loginNew.aspx";
                    string gotoURL = "login.aspx";
                    Response.Write("<script>window.open('" + gotoURL + "','_parent');</script>");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            _basePage = new BasePage();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(_basePage.GlbUserName + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(_basePage.GlbUserName + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void LoadLanguage()
        {
            _tbl = CHNLSVC.General.get_Language();
            cmbPrefLang.DataSource = _tbl;
            cmbPrefLang.DataTextField = "MLA_DESC";
            cmbPrefLang.DataValueField = "MLA_CD";
            cmbPrefLang.DataBind();
        }

        private void LoadCustomerType()
        {
            List<ComboBoxObject> oList = new List<ComboBoxObject>();
            ComboBoxObject o1 = new ComboBoxObject();
            o1.Text = "INDIVIDUAL";
            o1.Value = "INDIVIDUAL";
            oList.Add(o1);

            ComboBoxObject o2 = new ComboBoxObject();
            o2.Text = "GROUP";
            o2.Value = "GROUP";
            oList.Add(o2);

            ComboBoxObject o3 = new ComboBoxObject();
            o3.Text = "LEASE";
            o3.Value = "LEASE";
            oList.Add(o3);
            cmbType.DataSource = oList;
            cmbType.DataTextField = "Text";
            cmbType.DataValueField = "Value";
            cmbType.DataBind();
        }

        private void loadTitles()
        {
            List<ComboBoxObject> oList = new List<ComboBoxObject>();
            ComboBoxObject o1 = new ComboBoxObject();
            o1.Text = "MR.";
            o1.Value = "MR.";
            oList.Add(o1);

            ComboBoxObject o2 = new ComboBoxObject();
            o2.Text = "MRS.";
            o2.Value = "MRS.";
            oList.Add(o2);

            ComboBoxObject o3 = new ComboBoxObject();
            o3.Text = "MS.";
            o3.Value = "MS.";
            oList.Add(o3);

            ComboBoxObject o4 = new ComboBoxObject();
            o4.Text = "MISS.";
            o4.Value = "MISS.";
            oList.Add(o4);

            ComboBoxObject o5 = new ComboBoxObject();
            o5.Text = "DR.";
            o5.Value = "DR.";
            oList.Add(o5);

            ComboBoxObject o6 = new ComboBoxObject();
            o6.Text = "REV.";
            o6.Value = "REV.";
            oList.Add(o6);

            cmbTitle.DataSource = oList;
            cmbTitle.DataTextField = "Text";
            cmbTitle.DataValueField = "Value";
            cmbTitle.DataBind();
        }

        private void loadSex()
        {
            List<ComboBoxObject> oList = new List<ComboBoxObject>();
            ComboBoxObject o1 = new ComboBoxObject();
            o1.Text = "MALE";
            o1.Value = "MALE";
            oList.Add(o1);

            ComboBoxObject o2 = new ComboBoxObject();
            o2.Text = "FEMALE";
            o2.Value = "FEMALE";
            oList.Add(o2);

            cmbSex.DataSource = oList;
            cmbSex.DataTextField = "Text";
            cmbSex.DataValueField = "Value";
            cmbSex.DataBind();
        }

        private void dobGeneration()
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
            try
            {
                DateTime dob = new DateTime(dobYear, dobMon, bornDate);
                //dtpDOB.Value = dob.Date;
                txtDateOfBirth.Text = dob.Date.ToString("dd-MMM-yyyy");
            }
            catch (Exception ex)
            {
            }
        }

        public bool IsValidNIC(string nic)
        {
            string pattern = @"\d{9}[V|v|x|X]";
            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            bool valid = false;
            if (string.IsNullOrEmpty(nic))
            {
                valid = false;
            }
            else
            {
                valid = check.IsMatch(nic);
            }
            return valid;
        }

        private void DisplayMessages(string message)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
            }
        }

        #region LoadCustProfile

        public MasterBusinessEntity GetbyCustCD(string custCD)
        {
            //return CHNLSVC.Sales.GetCustomerProfileByCom(custCD, null, null, null, null, Session["UserCompanyCode"].ToString());
            return CHNLSVC.Sales.GetCustomerProfileByCom(custCD, null, null, null, null, Session["UserCompanyCode"].ToString());
        }

        public MasterBusinessEntity GetbyNIC(string nic)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, nic, null, null, null, Session["UserCompanyCode"].ToString());
        }

        public MasterBusinessEntity GetbyDL(string dl)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, null, dl, null, null, Session["UserCompanyCode"].ToString());
        }

        public MasterBusinessEntity GetbyPPno(string ppno)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, null, null, ppno, null, Session["UserCompanyCode"].ToString());
        }

        public MasterBusinessEntity GetbyBrNo(string brNo)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, null, null, null, brNo, Session["UserCompanyCode"].ToString());
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

        public void LoadCustProf(MasterBusinessEntity cust)
        {
            if (cust.Mbe_cate != null && !string.IsNullOrEmpty(cust.Mbe_cate))
            {
                cmbType.Text = cust.Mbe_cate;
            }
            txtNIC.Text = cust.Mbe_nic;
            txtPP.Text = cust.Mbe_pp_no;
            txtBR.Text = cust.Mbe_br_no;
            txtCusCode.Text = cust.Mbe_cd;
            txtDL.Text = cust.Mbe_dl_no;
            txtMob.Text = cust.Mbe_mob;
            chkSMS.Checked = cust.Mbe_agre_send_sms;
            if (cust.Mbe_sex != null && !string.IsNullOrEmpty(cust.Mbe_sex))
            {
                cmbSex.Text = cust.Mbe_sex;
            }
            txtName.Text = cust.Mbe_name;

            String nic_ = txtNIC.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(nic_))
            {
                char[] nicarray = nic_.ToCharArray();
                string thirdNum = (nicarray[2]).ToString();
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
                txtDateOfBirth.Text = cust.Mbe_dob.ToString("dd-MMM-yyyy");
            }
            else
            {
                txtDateOfBirth.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            }

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

            //txtCusCode.Enabled = false;
        }

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
                txtDateOfBirth.Text = _cust.Mbg_dob.ToString("dd-MMM-yyyy");
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
            //txtCusCode.Enabled = false;
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
            _custGroup.Mbg_dob = Convert.ToDateTime(txtDateOfBirth.Text).Date;
            _custGroup.Mbg_cre_by = Session["UserID"].ToString();
            _custGroup.Mbg_mod_by = Session["UserID"].ToString();
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
            _custProfile.Mbe_add2 = txtPerAdd2.Text.Trim();
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

            if (_isExsit.Value == "0" && _isGroup.Value == "0")
            {
                _custProfile.Mbe_cd = null;
            }
            else
            {
                _custProfile.Mbe_cd = txtCusCode.Text.Trim();
            }
            _custProfile.Mbe_com = Session["UserCompanyCode"].ToString();
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
            _custProfile.Mbe_cre_by = Session["UserID"].ToString();
            _custProfile.Mbe_cre_dt = Convert.ToDateTime(DateTime.Today).Date;
            _custProfile.Mbe_cre_pc = Session["UserDefProf"].ToString();
            _custProfile.Mbe_cust_com = Session["UserCompanyCode"].ToString();
            _custProfile.Mbe_cust_loc = Session["UserDefLoca"].ToString();
            _custProfile.Mbe_distric_cd = txtPerDistrict.Text;
            _custProfile.Mbe_dl_no = txtDL.Text.Trim();
            _custProfile.Mbe_dob = Convert.ToDateTime(txtDateOfBirth.Text).Date;
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
            _custProfile.Mbe_agre_send_email = _isEmail;
            _custProfile.Mbe_cust_lang = cmbPrefLang.SelectedValue.ToString();
        }

        private void Clear_Data()
        {
            _custProfile = new MasterBusinessEntity();
            _account = new CustomerAccountRef();
            _busInfoList = new List<MasterBusinessEntityInfo>();
            _custGroup = new GroupBussinessEntity();

            txtInit.Text = "";
            txtFname.Text = "";
            txtSName.Text = "";

            chkMail.Checked = false;
            txtCusCode.Text = "";
            txtCusCode.Enabled = true;
            _isExsit.Value = "0";
            _isGroup.Value = "0";
            cmbType.Text = "INDIVIDUAL";
            cmbPrefLang.SelectedIndex = 0;
            cmbSex.SelectedIndex = 0;
            cmbTitle.SelectedIndex = 0;
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
            //dtpDOB.Value = Convert.ToDateTime(DateTime.Today).Date;
            txtDateOfBirth.Text = DateTime.Now.ToString("dd-MMM-yyyy");

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
            btnCreate.Enabled = true;
            btnCreate.Text = "Create";
            // cmbType.Focus();
        }

        private void RedirectToBackPage()
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["RedirectPage"])))
            {
                Session["isComingBack"] = "1";
                Response.Redirect(Session["RedirectPage"].ToString() + "?htenus=customer");
            }
        }

        private void updateSesstion(String CustomtomerCode)
        {
            Session["newCustomer"] = CustomtomerCode;
            if (Session["GEN_CUST_ENQ"] != null &&
                  !String.IsNullOrEmpty(Convert.ToString(Session["RedirectPage"])))
            {
                GEN_CUST_ENQ oItem = (GEN_CUST_ENQ)Session["GEN_CUST_ENQ"];
                oItem.GCE_CUS_CD = CustomtomerCode;
            }
        }

        private void viewBackBtn()
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["RedirectPage"])))
            {
                btnBack.Visible = true;
            }
            else
            {
                btnBack.Visible = false;
            }
        }

        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

            System.Text.RegularExpressions.Match match = Regex.Match(email.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }

        protected void btnCustomer_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetCustomerCommon(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtCusCode.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            _custProfile = new MasterBusinessEntity();
            _account = new CustomerAccountRef();
            _busInfoList = new List<MasterBusinessEntityInfo>();
            _custGroup = new GroupBussinessEntity();

            txtInit.Text = "";
            txtFname.Text = "";
            txtSName.Text = "";

            chkMail.Checked = false;
            txtCusCode.Text = "";
            txtCusCode.Enabled = true;
            _isExsit.Value = "0";
            _isGroup.Value = "0";
            cmbType.SelectedIndex = 0;
            cmbPrefLang.SelectedIndex = 0;
            cmbSex.SelectedIndex = 0;
            cmbTitle.SelectedIndex = 0;
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
            // dtpDOB.Value = Convert.ToDateTime(DateTime.Today).Date;
            txtDateOfBirth.Text = "";

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
            btnCreate.Enabled = true;
            btnCreate.Text = "Create";
            //   cmbType.Focus();
        }

        protected void btnTownPerment_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetTown(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtPerTown.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void btnTownPresent_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetTown(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtPreTown.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void txtPerPhone_TextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtPerPhone.Text))
            //{
            //    Boolean _isvalid = IsValidMobileOrLandNo(txtPerPhone.Text.Trim());

            //    if (_isvalid == false)
            //    {
            //        DisplayMessages("Invalid phone number.");
            //        txtPerPhone.Text = "";
            //        txtPerPhone.Focus();
            //        return;
            //    }
            //}
        }

        protected void txtCusCode_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCusCode.Text))
            {
                btnCreate.Enabled = true;
                MasterBusinessEntity custProf = GetbyCustCD(txtCusCode.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                {
                    _isExsit.Value = "1";
                    btnCreate.Text = "Update";
                    LoadCustProf(custProf);
                }
                else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                {
                    DisplayMessages("Customer is inactivated.Please contact accounts dept.");
                    btnCreate.Enabled = false;
                    LoadCustProf(custProf);
                }
                else
                {
                    if (_isExsit.Value == "1")
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
                        _isGroup.Value = "1";
                    }
                    else
                    {
                        _isGroup.Value = "0";
                    }

                    _isExsit.Value = "0";
                    btnCreate.Text = "Create";
                }
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                string _cusCode = "";
                Int32 _effect = 0;

                if (txtNIC.Text == "" && txtBR.Text == "" && txtPP.Text == "" && txtDL.Text == "" && txtMob.Text == "")
                {
                    DisplayMessages("One of required information not entered.[NIC/BR/PP/DL/MOB]");
                    txtNIC.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtName.Text))
                {
                    DisplayMessages("Please enter name of customer");
                    txtName.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPerAdd1.Text))
                {
                    DisplayMessages("Please enter customer present address.");
                    txtPerAdd1.Focus();
                    return;
                }

                if (chkVAT.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtVatreg.Text))
                    {
                        DisplayMessages("Please enter VAT reg. number.");
                        txtVatreg.Focus();
                        return;
                    }
                }

                if (chkSVAT.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtSVATReg.Text))
                    {
                        DisplayMessages("Please enter SVAT reg. number.");
                        txtSVATReg.Focus();
                        return;
                    }
                }

                if (chkMail.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtPerEmail.Text))
                    {
                        DisplayMessages("Please enter Email.");
                        txtPerEmail.Focus();
                        return;
                    }
                }

                if (chkSVAT.Checked == true)
                {
                    MasterCompany _newCom = new MasterCompany();
                    _newCom = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());

                    if (_newCom.Mc_cd != null)
                    {
                        if (string.IsNullOrEmpty(_newCom.Mc_tax2))
                        {
                            DisplayMessages("Cannot create SVAT customer under your current login company.");
                            return;
                        }
                    }
                    else
                    {
                        DisplayMessages("Cannot find company details.Please re-try.");
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(txtPerEmail.Text))
                {
                    if (!IsValidEmail(txtPerEmail.Text))
                    {
                        DisplayMessages("Please enter a valid email address.");
                        txtPerEmail.Focus();
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(txtWorkEmail.Text))
                {
                    if (!IsValidEmail(txtWorkEmail.Text))
                    {
                        DisplayMessages("Please enter a valid work place email address.");
                        txtWorkEmail.Focus();
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(txtWorkPhone.Text))
                {
                    Boolean _isValid = IsValidMobileOrLandNo(txtWorkPhone.Text.Trim());

                    if (_isValid == false)
                    {
                        DisplayMessages("Invalid work phone number.");
                        txtWorkPhone.Text = "";
                        txtWorkPhone.Focus();
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(txtPerPhone.Text))
                {
                    Boolean _isvalid = IsValidMobileOrLandNo(txtPerPhone.Text.Trim());

                    if (_isvalid == false)
                    {
                        DisplayMessages("Invalid phone number.");
                        txtPerPhone.Text = "";
                        txtPerPhone.Focus();
                        return;
                    }

                }
                if (!string.IsNullOrEmpty(txtWorkPhone.Text))
                {
                    Boolean _isValid = IsValidMobileOrLandNo(txtWorkPhone.Text.Trim());

                    if (_isValid == false)
                    {
                        DisplayMessages("Invalid work phone number.");
                        txtWorkPhone.Text = "";
                        txtWorkPhone.Focus();
                        return;
                    }
                }

                Collect_Cust();
                Collect_GroupCust();

                bool isExsit = (_isExsit.Value == "1") ? true : false;
                bool isGroup = (_isGroup.Value == "1") ? true : false;
                GroupBussinessEntity _groupCus = new GroupBussinessEntity();

                List<BusEntityItem> busItemList = new List<BusEntityItem>();

                //if (_isExsit.Value == false)
                if (btnCreate.Text.ToUpper() != "Update".ToUpper())
                {
                    _effect = CHNLSVC.Sales.SaveBusinessEntityDetailWithGroup(_custProfile, _account, _busInfoList, busItemList, out _cusCode, null, isExsit, isGroup, _custGroup);
                }
                else
                {
                    _cusCode = txtCusCode.Text.Trim();
                    _effect = CHNLSVC.Sales.UpdateBusinessEntityProfileWithGroup(_custProfile, Session["UserID"].ToString(), Convert.ToDateTime(DateTime.Today).Date, 0, _busInfoList, null, busItemList, _custGroup);
                }

                updateSesstion(_cusCode);
                //if (_isExsit.Value == false)
                //{
                //    _effect = CHNLSVC.Sales.SaveBusinessEntityDetail(_custProfile, _account, _busInfoList, out _cusCode, null);
                //}
                //else
                //{
                //    _cusCode = txtCusCode.Text.Trim();
                //    _effect = CHNLSVC.Sales.UpdateBusinessEntityProfile(_custProfile, _basePage.GlbUserID, Convert.ToDateTime(DateTime.Today).Date, 0, _busInfoList, null);
                //}

                if (_effect == 1)
                {
                    if (_isExsit.Value == "0")
                    {
                        DisplayMessages("New customer created. Customer Code : " + _cusCode);
                    }
                    else
                    {
                        DisplayMessages("Existing customer updated.");
                    }
                    RedirectToBackPage();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_cusCode))
                    {
                        DisplayMessages("Error.:" + _cusCode);
                        return;
                    }
                    else
                    {
                        DisplayMessages("Creation Fail.");
                        return;
                    }
                }

                this.Clear_Data();
            }
            catch (Exception err)
            {
                DisplayMessages(err.Message);
            }
        }

        protected void txtNIC_TextChanged(object sender, EventArgs e)
        {
            cal.Visible = true;
            txtClearingDateExtender.Enabled = true;
            if (!string.IsNullOrEmpty(txtNIC.Text))
            {
                btnCreate.Enabled = true;
                Boolean _isValid = IsValidNIC(txtNIC.Text.Trim());

                if (_isValid == false)
                {
                    DisplayMessages("Invalid NIC.");
                    txtNIC.Text = "";
                    txtNIC.Focus();
                    return;
                }
                else
                {
                    txtClearingDateExtender.Enabled = false;
                    cal.Visible = false;
                    //check multiple Add By Chamal 24/04/2014
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(Session["UserCompanyCode"].ToString(), txtNIC.Text.Trim(), "", "", "", "", 1);
                    if (_custList != null && _custList.Count > 1 && txtNIC.Text.ToUpper() != "N/A")
                    {
                        string _custNIC = "Duplicate customers found!";
                        //foreach (var _nicCust in _custList)
                        //{
                        //    _custNIC = _custNIC + _nicCust.Mbe_cd.ToString() + " | " + _nicCust.Mbe_name.ToString() + "\n";
                        //}
                        DisplayMessages(_custNIC);
                        txtNIC.Text = "";
                        txtNIC.Focus();
                        return;
                    }

                    MasterBusinessEntity custProf = GetbyNIC(txtNIC.Text.Trim().ToUpper());
                    if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                    {
                        _isExsit.Value = "1";
                        btnCreate.Text = "Update";
                        dobGeneration();
                        LoadCustProf(custProf);
                    }
                    else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                    {
                        DisplayMessages("Customer is inactivated.Please contact accounts dept.");
                        btnCreate.Enabled = false;
                        LoadCustProf(custProf);
                    }
                    else//added on 01/10/2012
                    {
                        if (_isExsit.Value == "1")
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
                            _isGroup.Value = "1";
                            _isExsit.Value = "0";
                            btnCreate.Text = "Create";
                            return;
                        }
                        else
                        {
                            _isGroup.Value = "0";
                        }

                        _isExsit.Value = "0";
                        btnCreate.Text = "Create";
                        String nic_ = txtNIC.Text.Trim().ToUpper();
                        char[] nicarray = nic_.ToCharArray();
                        string thirdNum = (nicarray[2]).ToString();
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
                            //dtpDOB.Value = dob.Date;
                            //dob.ToString("dd/MM/yyyy");
                            txtDateOfBirth.Text = dob.Date.ToString("dd-MMM-yyyy");
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            //txtNIC.Focus();
        }

        protected void txtBR_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBR.Text))
            {
                _isExsit.Value = "0";
                btnCreate.Enabled = true;
                MasterBusinessEntity custProf = GetbyBrNo(txtBR.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                {
                    _isExsit.Value = "1";
                    btnCreate.Text = "Update";
                    LoadCustProf(custProf);
                }
                else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                {
                    DisplayMessages("Customer is inactivated.Please contact accounts dept.");
                    btnCreate.Enabled = false;
                    LoadCustProf(custProf);
                }
                else
                {
                    if (_isExsit.Value == "0")
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
                            _isGroup.Value = "1";
                            _isExsit.Value = "0";
                            btnCreate.Text = "Create";
                            return;
                        }
                        else
                        {
                            _isGroup.Value = "0";
                        }
                        _isExsit.Value = "0";
                        btnCreate.Text = "Create";
                    }
                }
            }
        }

        protected void txtPP_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPP.Text))
            {
                btnCreate.Enabled = true;
                MasterBusinessEntity custProf = GetbyPPno(txtPP.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                {
                    _isExsit.Value = "1";
                    btnCreate.Text = "Update";
                    LoadCustProf(custProf);
                }
                else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                {
                    DisplayMessages("Customer is inactivated.Please contact accounts dept.");
                    btnCreate.Enabled = false;
                    LoadCustProf(custProf);
                }
                else
                {
                    if (_isExsit.Value == "1")
                    {
                        string PP = txtPP.Text.Trim().ToUpper();
                        MasterBusinessEntity cust_null = new MasterBusinessEntity();
                        LoadCustProf(cust_null);
                        txtPP.Text = PP;
                    }
                    //Check the group level
                    GroupBussinessEntity _grupProf = GetbyPPnoGrup(txtPP.Text.Trim().ToUpper());
                    if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                    {
                        LoadCustProfByGrup(_grupProf);
                        _isGroup.Value = "1";
                        _isExsit.Value = "0";
                        btnCreate.Text = "Create";
                        return;
                    }
                    else
                    {
                        _isGroup.Value = "0";
                    }
                    _isExsit.Value = "0";
                    btnCreate.Text = "Create";
                }
            }
        }

        protected void txtMob_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMob.Text))
            {
                btnCreate.Enabled = true;
                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                Boolean _isValid = IsValidMobileOrLandNo(txtMob.Text.Trim());

                if (_isValid == false)
                {
                    DisplayMessages("Invalid mobile number.");
                    txtMob.Text = "";
                    txtMob.Focus();
                    return;
                }

                //List<MasterBusinessEntity> _fk = CHNLSVC.Sales.GetCustomerDetailList(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, txtMob.Text, "C");
                List<MasterBusinessEntity> _fk = CHNLSVC.Sales.GetActiveCustomerDetailList(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, txtMob.Text, "C");
                if (_fk != null && _fk.Count > 0)
                {
                    if (_fk.Count > 1)
                    {
                        DisplayMessages("There are " + _fk.Count + " number of customers available for the selected mobile.");
                        txtMob.Text = "";
                        txtMob.Focus();
                        return;
                    }
                }

                //_masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, txtMob.Text, "C");
                _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, txtMob.Text, "C");
                if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_act == true)
                {
                    _isExsit.Value = "1";
                    btnCreate.Text = "Update";
                    LoadCustProf(_masterBusinessCompany);
                }
                else if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_act == false)
                {
                    DisplayMessages("Customer is inactivated.Please contact accounts dept.");
                    btnCreate.Enabled = false;
                    LoadCustProf(_masterBusinessCompany);
                }
                else
                {
                    if (_isExsit.Value == "1")
                    {
                        string Mob = txtMob.Text.Trim().ToUpper();
                        MasterBusinessEntity cust_null = new MasterBusinessEntity();
                        LoadCustProf(cust_null);
                        txtMob.Text = Mob;
                    }
                    //Check the group level
                    GroupBussinessEntity _grupProf = GetbyMobGrup(txtMob.Text.Trim().ToUpper());
                    if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                    {
                        LoadCustProfByGrup(_grupProf);
                        _isGroup.Value = "1";
                        _isExsit.Value = "0";
                        btnCreate.Text = "Create";
                        return;
                    }
                    else
                    {
                        _isGroup.Value = "0";
                    }
                    _isExsit.Value = "0";
                    btnCreate.Text = "Create";
                }
            }
        }

        protected void txtDL_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDL.Text))
            {
                btnCreate.Enabled = true;
                MasterBusinessEntity custProf = GetbyDL(txtDL.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                {
                    _isExsit.Value = "1";
                    btnCreate.Text = "Update";
                    LoadCustProf(custProf);
                }
                else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                {
                    DisplayMessages("Customer is inactivated.Please contact accounts dept.");
                    btnCreate.Enabled = false;
                    LoadCustProf(custProf);
                }
                else
                {
                    if (_isExsit.Value == "1")
                    {
                        string DL = txtDL.Text.Trim().ToUpper();
                        MasterBusinessEntity cust_null = new MasterBusinessEntity();
                        LoadCustProf(cust_null);
                        txtDL.Text = DL;
                    }
                    //Check the group level
                    GroupBussinessEntity _grupProf = GetbyDLGrup(txtDL.Text.Trim().ToUpper());
                    if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                    {
                        LoadCustProfByGrup(_grupProf);
                        _isGroup.Value = "1";
                        _isExsit.Value = "0";
                        btnCreate.Text = "Create";
                        return;
                    }
                    else
                    {
                        _isGroup.Value = "0";
                    }
                    _isExsit.Value = "0";
                    btnCreate.Text = "Create";
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            RedirectToBackPage();
        }

        protected void txtWorkPhone_TextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtWorkPhone.Text))
            //{
            //    Boolean _isValid = IsValidMobileOrLandNo(txtWorkPhone.Text.Trim());

            //    if (_isValid == false)
            //    {
            //        DisplayMessages("Invalid work phone number.");
            //        txtWorkPhone.Text = "";
            //        txtWorkPhone.Focus();
            //        return;
            //    }
            //}
        }

        protected void txtPerTown_TextChanged(object sender, EventArgs e)
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
                        DisplayMessages("Invalid town.");
                        txtPerTown.Text = "";
                        txtPerTown.Focus();
                    }
                }
                else
                {
                    DisplayMessages("Invalid town.");
                    txtPerTown.Text = "";
                    txtPerTown.Focus();
                }
            }
        }

        protected void txtPreTown_TextChanged(object sender, EventArgs e)
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
                        DisplayMessages("Invalid town.");
                        txtPreTown.Text = "";
                        txtPreTown.Focus();
                    }
                }
                else
                {
                    DisplayMessages("Invalid town.");
                    txtPreTown.Text = "";
                    txtPreTown.Focus();
                }
            }
        }
    }
}