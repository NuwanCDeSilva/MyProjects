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
using System.IO;
using System.Text.RegularExpressions;
using FF.BusinessObjects.General;

namespace FF.WindowsERPClient.General
{
    public partial class SupplierCreation : Base
    {
        private MasterBusinessEntity _supProfile = new MasterBusinessEntity();
        private GroupBussinessEntity _custGroup = new GroupBussinessEntity();
        private Boolean _isExsit = false;
        private Boolean _isGroup = false;
        private Boolean _is_sp_tax = false;
        private CustomerAccountRef _account = new CustomerAccountRef();
        private List<MasterBusinessEntityInfo> _busInfoList = new List<MasterBusinessEntityInfo>();
        private List<BusEntityItem> _busItem = new List<BusEntityItem>();
        private SupplierWiseNBT _SupplierWiseNBT = new SupplierWiseNBT();
        private List<SupplierWiseNBT> _SupplierWiseNBTlist = new List<SupplierWiseNBT>();
        private string tax_rate_cd = string.Empty;
        private Decimal Rate;

        public SupplierCreation()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                string _cusCode = "";
                Int32 _effect = 0;

                if (string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Please enter name of supplier", "Supplier creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtName.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPerAdd1.Text))
                {
                    MessageBox.Show("Please enter supplier present address.", "Supplier creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPerAdd1.Focus();
                    return;
                }


                if (chkTax.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtTaxReg.Text))
                    {
                        MessageBox.Show("Please enter TAX reg. number.", "Supplier creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTaxReg.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtCur.Text))
                {
                    MessageBox.Show("Please enter currency code.", "Supplier creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCur.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cmbType.Text))
                {
                    MessageBox.Show("Please select the type.", "Supplier creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbType.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtPerCountry.Text))
                {
                    MessageBox.Show("Please enter country code.", "Supplier creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPerCountry.Focus();
                    return;
                }

                if (MessageBox.Show("Do you want to save ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                Collect_Supp();
                Collect_GroupCust();
                if (chkSpec.Checked==true)
                {
                    Collect_Supp_NBT();
                }
               


                if (_isExsit == false)
                {
                    _effect = CHNLSVC.Sales.SaveBusinessEntityDetailWithGroup(_supProfile, _account, _busInfoList, _busItem, out _cusCode, null, _isExsit, _isGroup, _custGroup, true, null, "False", null, null, _SupplierWiseNBT);
                }
                else
                {
                    _cusCode = txtCusCode.Text.Trim();
                    _effect = CHNLSVC.Sales.UpdateBusinessEntityProfileWithGroup(_supProfile, BaseCls.GlbUserID, Convert.ToDateTime(DateTime.Today).Date, 0, _busInfoList, null, _busItem, _custGroup, null, _SupplierWiseNBT);
                }



                if (_effect == 1)
                {
                    if (_isExsit == false)
                    {
                        MessageBox.Show("New supplier created. Supplier Code : " + _cusCode, "Supplier creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Exsiting supplier updated.", "Supplier creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
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


                // this.Clear_Data();
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
            //_custGroup.Mbg_tit = cmbTitle.Text;
            //_custGroup.Mbg_ini = txtInit.Text.Trim();
            _custGroup.Mbg_fname = txtName.Text.Trim();
            //_custGroup.Mbg_sname = txtSName.Text.Trim();
            _custGroup.Mbg_nationality = "SL";
            _custGroup.Mbg_add1 = txtPerAdd1.Text.Trim();
            _custGroup.Mbg_add2 = txtPerAdd2.Text.Trim();
            //_custGroup.Mbg_town_cd = txtPerTown.Text.Trim();
            //_custGroup.Mbg_distric_cd = txtPerDistrict.Text.Trim();
            //_custGroup.Mbg_province_cd = txtPerProvince.Text.Trim();
            _custGroup.Mbg_country_cd = txtPerCountry.Text.Trim();
            _custGroup.Mbg_tel = txtPerPhone.Text.Trim();
            _custGroup.Mbg_fax = "";
            // _custGroup.Mbg_postal_cd = txtPerPostal.Text.Trim();
            // _custGroup.Mbg_mob = txtMob.Text.Trim();
            //_custGroup.Mbg_nic = txtNIC.Text.Trim();
            //_custGroup.Mbg_pp_no = txtPP.Text.Trim();
            //_custGroup.Mbg_dl_no = txtDL.Text.Trim();
            //_custGroup.Mbg_br_no = txtBR.Text.Trim();
            _custGroup.Mbg_email = txtPerEmail.Text.Trim();
            _custGroup.Mbg_contact = "";
            _custGroup.Mbg_act = true;
            _custGroup.Mbg_is_suspend = false;
            // _custGroup.Mbg_sex = cmbSex.Text;
            _custGroup.Mbg_dob = DateTime.Now.Date;
            _custGroup.Mbg_cre_by = BaseCls.GlbUserID;
            _custGroup.Mbg_mod_by = BaseCls.GlbUserID;

        }

        private void Collect_Supp()
        {
            Boolean _isSMS = false;
            Boolean _isSVAT = false;
            Boolean _isVAT = false;
            Boolean _TaxEx = false;
            Boolean _isEmail = false;

            if (chkTax.Checked == true)
            {
                _isVAT = true;
            }
            else
            {
                _isVAT = false;
            }

            _supProfile = new MasterBusinessEntity();
            _supProfile.Mbe_acc_cd = null;
            _supProfile.Mbe_act = Convert.ToBoolean(chkAct.Checked);
            _supProfile.Mbe_add1 = txtPerAdd1.Text.Trim();
            _supProfile.Mbe_add2 = txtPerAdd2.Text.Trim();

            _supProfile.Mbe_agre_send_sms = _isSMS;
            _supProfile.Mbe_br_no = "";
            _supProfile.Mbe_cate = txtTaxCat.Text;
            if (_isExsit == false && _isGroup == false)
            {
                _supProfile.Mbe_cd = txtCusCode.Text.Trim();
            }
            else
            {
                _supProfile.Mbe_cd = txtCusCode.Text.Trim();
            }
            _supProfile.Mbe_com = BaseCls.GlbUserComCode;
            _supProfile.Mbe_contact = null;
            _supProfile.Mbe_country_cd = txtPerCountry.Text.Trim();
            _supProfile.Mbe_cr_add1 = txtPerAdd1.Text.Trim();
            _supProfile.Mbe_cr_add2 = txtPerAdd2.Text.Trim();
            _supProfile.Mbe_cr_country_cd = txtPerCountry.Text.Trim();
            _supProfile.Mbe_cr_distric_cd = "";
            _supProfile.Mbe_cr_email = null;
            _supProfile.Mbe_cr_fax = null;
            _supProfile.Mbe_cr_postal_cd = "";
            _supProfile.Mbe_cr_province_cd = "";
            _supProfile.Mbe_cr_tel = txtPerPhone.Text.Trim();
            _supProfile.Mbe_cr_town_cd = "";
            _supProfile.Mbe_cre_by = BaseCls.GlbUserID;
            _supProfile.Mbe_cre_dt = Convert.ToDateTime(DateTime.Today).Date;
            _supProfile.Mbe_cre_pc = BaseCls.GlbUserDefProf;
            _supProfile.Mbe_cust_com = BaseCls.GlbUserComCode;
            _supProfile.Mbe_cust_loc = BaseCls.GlbUserDefLoca;
            _supProfile.Mbe_distric_cd = "";
            _supProfile.Mbe_dl_no = "";
            _supProfile.Mbe_dob = DateTime.Now.Date;
            _supProfile.Mbe_email = txtPerEmail.Text.Trim();
            _supProfile.Mbe_fax = null;
            _supProfile.Mbe_ho_stus = "GOOD";
            _supProfile.Mbe_income_grup = null;
            _supProfile.Mbe_intr_com = false;
            _supProfile.Mbe_is_suspend = false;

            _supProfile.Mbe_is_tax = _isVAT;
            _supProfile.Mbe_mob = "";
            _supProfile.Mbe_name = txtName.Text.Trim();
            _supProfile.Mbe_nic = "";
            _supProfile.Mbe_oth_id_no = txtTin.Text;
            _supProfile.Mbe_oth_id_tp = "TIN";
            _supProfile.Mbe_pc_stus = "GOOD";
            _supProfile.Mbe_postal_cd = "";
            _supProfile.Mbe_pp_no = "";
            _supProfile.Mbe_province_cd = "";
            _supProfile.Mbe_sex = "";
            _supProfile.Mbe_sub_tp = cmbType.Text == "Foreign" ? "F" : "L";
            _supProfile.Mbe_svat_no = "";


            _supProfile.Mbe_tax_ex = _TaxEx;
            _supProfile.Mbe_tax_no = txtTaxReg.Text.Trim();
            _supProfile.Mbe_tel = txtPerPhone.Text.Trim();
            _supProfile.Mbe_town_cd = "";
            _supProfile.Mbe_tp = "S";
            _supProfile.Mbe_wr_add1 = "";
            _supProfile.Mbe_wr_add2 = "";
            _supProfile.Mbe_wr_com_name = "";
            _supProfile.Mbe_wr_country_cd = null;
            _supProfile.Mbe_wr_dept = "";
            _supProfile.Mbe_wr_designation = "";
            _supProfile.Mbe_wr_distric_cd = null;
            _supProfile.Mbe_wr_email = "";
            _supProfile.Mbe_wr_fax = "";
            _supProfile.Mbe_wr_proffesion = null;
            _supProfile.Mbe_wr_province_cd = null;
            _supProfile.Mbe_wr_tel = "";
            _supProfile.Mbe_wr_town_cd = null;
            _supProfile.MBE_FNAME = txtName.Text.Trim();
            _supProfile.MBE_SNAME = "";
            _supProfile.MBE_INI = "";
            _supProfile.MBE_TIT = "";
            _supProfile.MBE_WEB = txtWeb.Text;
            _supProfile.Mbe_acc_cd = txtGL.Text;
            _supProfile.Mbe_cur_cd = txtCur.Text;
            _supProfile.MBE_CR_PERIOD = Convert.ToInt32(txtCredPrd.Text);
            _supProfile.Mbe_fax = txtFax.Text;

            //add by tharanga 2017/09/09
            
            
            // Nadeeka 15-12-2014
            _supProfile.Mbe_agre_send_email = _isEmail;
            // _supProfile.Mbe_cust_lang = cmbPrefLang.SelectedValue.ToString();
        }
        private void Collect_Supp_NBT()
        {
            Boolean _isSMS = false;
            Boolean _isSVAT = false;
            Boolean _isVAT = false;
            Boolean _TaxEx = false;
            Boolean _isEmail = false;

         
            _SupplierWiseNBT = new SupplierWiseNBT();
            _SupplierWiseNBT.MBIT_COM = BaseCls.GlbUserComCode;
            _SupplierWiseNBT.MBIT_CD = txtCusCode.Text.ToString().Trim();
            _SupplierWiseNBT.MBIT_TP = "S";
            _SupplierWiseNBT.MBIT_TAX_CD = txtCode.Text.ToString().Trim();
            _SupplierWiseNBT.MBIT_TAX_RT_CD = tax_rate_cd;
            _SupplierWiseNBT.MBIT_TAX_RT = Convert.ToDecimal(txtRate.Text.ToString());
            _SupplierWiseNBT.MBIT_DIV_RT = Convert.ToDecimal(txtDivRate.Text.ToString());
            _SupplierWiseNBT.MBIT_ACT = true;
            _SupplierWiseNBT.MBIT_CRE_BY = BaseCls.GlbUserID;
            _SupplierWiseNBT.MBIT_CRE_DT=DateTime.Now;
            _SupplierWiseNBT.MBIT_MOD_BY = BaseCls.GlbUserID;
            _SupplierWiseNBT.MBIT_MOD_DT = DateTime.Now;

            _SupplierWiseNBTlist.Add(_SupplierWiseNBT);


          
         
        }
        private void txtCusCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_srch_sup_Click(null, null);
            }
            if (e.KeyCode == Keys.Enter)
                txtName.Focus();
        }

        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtIcat1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtIcat1.Text + seperator + txtIcat2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterTax:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Country:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TaxCodes:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SupplierCommon:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemBrand:
                    {
                        //paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        //break;
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        private void txtCusCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCusCode.Text))
            {

                MasterBusinessEntity custProf = GetbyCustCD(txtCusCode.Text.Trim().ToUpper());
                SupplierWiseNBT _SupplierWiseNBT = GetSupplierWiseNBT(txtCusCode.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                {
                    _isExsit = true;

                    LoadCustProf(custProf);
                }
                if (_SupplierWiseNBT != null)
                {
                    if (_SupplierWiseNBT.MBIT_CD != null)
                    {
                        LoadNBT(_SupplierWiseNBT);

                    }
                }
              
                else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                {
                    MessageBox.Show("Supplier is inactivated.", "supplier Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    LoadCustProf(custProf);
                }
                else
                {
                    if (_isExsit == true)
                    {
                        string cusCD = txtCusCode.Text.Trim().ToUpper();
                        MasterBusinessEntity cust_null = new MasterBusinessEntity();
                        LoadCustProf(custProf);
                        txtCusCode.Text = cusCD;
                    }
                    //Check the group level


                    _isExsit = false;

                }

            }
        }

        public void LoadCustProf(MasterBusinessEntity cust)
        {

            txtName.Text = cust.Mbe_name;
            txtPerAdd1.Text = cust.Mbe_add1;
            txtPerAdd2.Text = cust.Mbe_add2;
            //  txtPerTown.Text = cust.Mbe_town_cd;
            // txtPerPostal.Text = cust.Mbe_postal_cd;
            txtPerCountry.Text = cust.Mbe_country_cd;
            // txtPerDistrict.Text = cust.Mbe_distric_cd;
            // txtPerProvince.Text = cust.Mbe_province_cd;
            txtPerPhone.Text = cust.Mbe_tel;
            txtPerEmail.Text = cust.Mbe_email;
            chkTax.Checked = cust.Mbe_is_tax;
            txtTaxReg.Text = cust.Mbe_tax_no;
            txtCur.Text = cust.Mbe_cur_cd;
            txtGL.Text = cust.Mbe_acc_cd;
            txtWeb.Text = cust.MBE_WEB;
            txtCredPrd.Text = cust.MBE_CR_PERIOD.ToString();
            txtTin.Text = cust.Mbe_oth_id_no;
            if (cust.Mbe_sub_tp == "F")
                cmbType.Text = "Foreign";
            else
                cmbType.Text = "Local";

            txtFax.Text = cust.Mbe_fax;
            txtCur.Text = cust.Mbe_cur_cd;
            txtPerCountry.Text = cust.Mbe_country_cd;
            txtTaxCat.Text = cust.Mbe_cate;
            chkAct.Checked = cust.Mbe_act == true ? true : false;

            DataTable _dtSupItem = CHNLSVC.CommonSearch.LoadSupplierItems(BaseCls.GlbUserComCode, txtCusCode.Text);
            grvSupItms.AutoGenerateColumns = false;
            grvSupItms.DataSource = _dtSupItem;



            //txtPreAdd1.Text = cust.Mbe_cr_add1;
            //txtPreAdd2.Text = cust.Mbe_cr_add2;
            //txtPreTown.Text = cust.Mbe_cr_town_cd;
            //txtPrePostal.Text = cust.Mbe_cr_postal_cd;
            //txtPreCountry.Text = cust.Mbe_cr_country_cd;
            //txtPreDistrict.Text = cust.Mbe_cr_distric_cd;
            //txtPreProvince.Text = cust.Mbe_cr_province_cd;
            //txtPrePhone.Text = cust.Mbe_cr_tel;

            //txtWorkAdd1.Text = cust.Mbe_wr_add1;
            //txtWorkAdd2.Text = cust.Mbe_wr_add2;
            //txtWorkName.Text = cust.Mbe_wr_com_name;
            //txtWorkDept.Text = cust.Mbe_wr_dept;
            //txtWorkDesig.Text = cust.Mbe_wr_designation;
            //txtWorkEmail.Text = cust.Mbe_wr_email;
            //txtWorkFax.Text = cust.Mbe_wr_fax;
            //txtWorkPhone.Text = cust.Mbe_wr_tel;

            //chkVAT.Checked = cust.Mbe_is_tax;
            //chkVatEx.Checked = cust.Mbe_tax_ex;
            //chkSVAT.Checked = cust.Mbe_is_svat;
            //txtVatreg.Text = cust.Mbe_tax_no;
            //txtSVATReg.Text = cust.Mbe_svat_no;
            //txtInit.Text = cust.MBE_INI;
            // txtSName.Text = cust.MBE_SNAME;
            // Nadeeka 15-12-2014
            // chkMail.Checked = cust.Mbe_agre_send_email;

        }
        public void LoadNBT(SupplierWiseNBT _SupplierWiseNBT)
        {

            chkSpec.Checked = true;
            txtCode.Text = _SupplierWiseNBT.MBIT_TAX_CD;
            txtRate.Text = _SupplierWiseNBT.MBIT_TAX_RT.ToString();
            txtDivRate.Text = _SupplierWiseNBT.MBIT_DIV_RT.ToString();

           

        }
        #region LoadCustProfile
        public MasterBusinessEntity GetbyCustCD(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(custCD, null, null, null, null, BaseCls.GlbUserComCode);
        }
        // add by tharanga 2017/09/11
        public SupplierWiseNBT GetSupplierWiseNBT(string custCD)
        {
            return CHNLSVC.Sales.GetSupplierNBT(BaseCls.GlbUserComCode,custCD);
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

        private void txtPerTown_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F2)
            //{
            //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            //    _CommonSearch.ReturnIndex = 0;
            //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
            //    DataTable _result = CHNLSVC.CommonSearch.GetTown(_CommonSearch.SearchParams, null, null);
            //    _CommonSearch.dvResult.DataSource = _result;
            //    _CommonSearch.BindUCtrlDDLData(_result);
            //    _CommonSearch.obj_TragetTextBox = txtPerTown;
            //    _CommonSearch.ShowDialog();
            //    txtPerTown.Select();
            //}
            //else if (e.KeyCode == Keys.Enter)
            //{
            //    txtPerPhone.Focus();
            //}
        }

        private void txtPreTown_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F2)
            //{
            //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            //    _CommonSearch.ReturnIndex = 0;
            //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
            //    DataTable _result = CHNLSVC.CommonSearch.GetTown(_CommonSearch.SearchParams, null, null);
            //    _CommonSearch.dvResult.DataSource = _result;
            //    _CommonSearch.BindUCtrlDDLData(_result);
            //    _CommonSearch.obj_TragetTextBox = txtPreTown;
            //    _CommonSearch.ShowDialog();
            //    txtPreTown.Select();
            //}
            //else if (e.KeyCode == Keys.Enter)
            //{
            //    txtPrePhone.Focus();
            //}
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grvSelItms.Rows)
            {
                BusEntityItem _tmp = new BusEntityItem();
                _tmp.MBII_ACT = 1;
                _tmp.MBII_CD = txtCusCode.Text;
                _tmp.MBII_COM = BaseCls.GlbUserComCode;
                _tmp.MBII_CRE_BY = BaseCls.GlbUserID;
                _tmp.MBII_ITM_CD = row.Cells["CODE"].Value.ToString();
                _tmp.MBII_TP = "S";
                _tmp.MI_SHORTDESC = row.Cells["DESCRIPT"].Value.ToString();
                _tmp.MBII_MOD_BY = BaseCls.GlbUserID;
                _busItem.Add(_tmp);
            }

            // _busItem = _busItem.DefaultView.ToTable(true);

            grvSupItms.AutoGenerateColumns = false;
            grvSupItms.DataSource = new List<BusEntityItem>();
            grvSupItms.DataSource = _busItem;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtIcat1.Text) && string.IsNullOrEmpty(txtIcat2.Text) && string.IsNullOrEmpty(txtItemCode.Text) && string.IsNullOrEmpty(txtBrand.Text) && string.IsNullOrEmpty(txtModel.Text))
            {
                MessageBox.Show("Please enter the criteria", "supplier Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable dt = CHNLSVC.Sales.GetInsuCriteria(BaseCls.GlbUserComCode, "ITEM", txtItemCode.Text.Trim(), txtBrand.Text.Trim(), txtModel.Text.Trim(), txtIcat1.Text.Trim(), txtIcat2.Text.Trim(), null, null, null);
            if (dt.Rows.Count > 0)
            {
                grvSelItms.AutoGenerateColumns = false;
                grvSelItms.DataSource = dt;
            }
            else
                MessageBox.Show("No data found for the selected criteria", "supplier Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btn_Srch_Itm_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtItemCode;
            _CommonSearch.ShowDialog();
            txtItemCode.Focus();
        }

        private void clear()
        {
            SupplierCreation formnew = new SupplierCreation();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear the screen?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                clear();
            }
        }

        private void btn_srch_sup_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierCommon);
            DataTable _result = CHNLSVC.CommonSearch.GetSupplierCommon(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCusCode;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtCusCode.Select();
        }

        private void txtCusCode_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_sup_Click(null, null);
        }

        private void btn_srch_country_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
            DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPerCountry;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtPerCountry.Select();
        }

        private void btn_srch_cur_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
            DataTable _result = CHNLSVC.CommonSearch.GetCurrencyData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCur;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtCur.Select();
        }

        private void btn_srch_tax_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterTax);
            DataTable _result = CHNLSVC.CommonSearch.LoadAllTaxCat(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtTaxCat;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtTaxCat.Select();
        }

        private void txtPerPhone_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPerPhone.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtPerPhone.Text.Trim());
                if (_isValid == false)
                {
                    MessageBox.Show("Invalid phone number.", "Supplier Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPerPhone.Focus();
                    return;
                }
            }
        }

        private void txtPerEmail_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPerEmail.Text))
            {
                if (!IsValidEmail(txtPerEmail.Text))
                {
                    MessageBox.Show("Email address invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPerEmail.Focus();
                }
            }
        }

        private void txtCur_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCur.Text))
            {
                List<MasterCurrency> _Currency = CHNLSVC.General.GetAllCurrency(txtCur.Text);
                if (_Currency == null)
                {
                    MessageBox.Show("Invalid currency code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCur.Focus();
                }
            }
        }

        private void btn_Srch_Cat2_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtIcat2;
            _CommonSearch.txtSearchbyword.Text = txtIcat2.Text;
            _CommonSearch.ShowDialog();
            txtIcat2.Focus();
        }

        private void grvSupItms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _busItem.RemoveAt(e.RowIndex);

                    grvSupItms.AutoGenerateColumns = false;
                    grvSupItms.DataSource = new List<BusEntityItem>();
                    grvSupItms.DataSource = _busItem;
                }
            }
        }

        private void chkSpec_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSpec.Checked == true)
                pnlSpec.Enabled = true;
            else
                pnlSpec.Enabled = false;
        }

        private void btn_Srch_Cat1_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtIcat1;
            _CommonSearch.txtSearchbyword.Text = txtIcat1.Text;
            _CommonSearch.ShowDialog();
            txtIcat1.Focus();
        }

        private void btn_Srch_Brnd_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtBrand;
            _CommonSearch.txtSearchbyword.Text = txtBrand.Text;
            _CommonSearch.ShowDialog();
            txtBrand.Focus();
        }

        private void btn_Srch_Model_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
            DataTable _result = CHNLSVC.CommonSearch.GetAllModels(_CommonSearch.SearchParams, null, null);

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtModel; //txtBox;
            _CommonSearch.ShowDialog();
            txtModel.Focus();
        }

        private void txtIcat1_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Cat1_Click(null, null);
        }

        private void txtIcat1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_Srch_Cat1_Click(null, null);
        }

        private void txtIcat2_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Cat2_Click(null, null);
        }

        private void txtIcat2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_Srch_Cat2_Click(null, null);
        }

        private void txtItemCode_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Itm_Click(null, null);
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_Srch_Itm_Click(null, null);
        }

        private void txtBrand_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Brnd_Click(null, null);
        }

        private void txtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_Srch_Brnd_Click(null, null);
        }

        private void txtModel_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Model_Click(null, null);
        }

        private void txtModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            btn_Srch_Model_Click(null, null);
        }

        private void btn_srch_taxes_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.IsReturnFullRow = true;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TaxCodes);
            DataTable _result = CHNLSVC.CommonSearch.LoadAllTaxCodes(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCode;
            string _tmpString = _CommonSearch.UserSelectedRow;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtCode.Select();
            if (string.IsNullOrEmpty(tax_rate_cd))
            {
                List<string> tax_rat_cd = new List<string>();
                tax_rat_cd = _CommonSearch.UserSelectedRow.Split(new string[] { "|" }, StringSplitOptions.None).ToList();

                if ((tax_rat_cd != null) && (tax_rat_cd.Count > 0)) 
                {
                    tax_rate_cd = Convert.ToString(tax_rat_cd[1]);
                    txtRate.Text = Convert.ToString(tax_rat_cd[3]); 
                }

            }
            
        }

        private void txtCode_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_taxes_Click(null, null);
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F2)
                btn_srch_taxes_Click(null, null);
        }

        private void SupplierCreation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtCode_Leave(object sender, EventArgs e)
        {


           


        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
