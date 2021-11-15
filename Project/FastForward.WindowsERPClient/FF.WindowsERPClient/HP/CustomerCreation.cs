using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FF.BusinessObjects;

namespace FF.WindowsERPClient.HP
{
    public partial class CustomerCreation : Base
    {
        public class BusinessEntityTYPE
        {
            //this object stores all types
            private string typeCD_;

            public string TypeCD_
            {
                get { return typeCD_; }
                set { typeCD_ = value; }
            }

            private string typeDesctipt;

            public string TypeDesctipt
            {
                get { return typeDesctipt; }
                set { typeDesctipt = value; }
            }

            private string isMandatory;

            public string IsMandatory
            {
                get { return isMandatory; }
                set { isMandatory = value; }
            }
        }

        public class BusinessEntityVAL
        {
            //this object stores all the types and the values.(therefore for the same type there can be more than one values)
            private string type_;

            public string Type_
            {
                get { return type_; }
                set { type_ = value; }
            }

            private string val;

            public string Val
            {
                get { return val; }
                set { val = value; }
            }
        }

        #region properties

        private int Temp_discount_seq;
        private List<CashGeneralDicountDef> DiscountList;
        private List<CashGeneralDicountDef> ViewDiscountList;
        private MasterBusinessEntity _custProfile = new MasterBusinessEntity();
        private CustomerAccountRef _account = new CustomerAccountRef();
        private List<MasterBusinessEntityInfo> _busInfoList = new List<MasterBusinessEntityInfo>();
        private List<MasterInvoiceType> SalesType = new List<MasterInvoiceType>();
        private GroupBussinessEntity _custGroup = new GroupBussinessEntity();
        private List<MST_BUSPRIT_LVL> _PriorityLvlList = null;      //kapila 28/1/2015

        private Boolean _isExsit = false;
        private Boolean _isGroup = false;

        public Boolean _isFromOther = false;
        public TextBox obj_TragetTextBox;

        #endregion properties

        public CustomerCreation()
        {
            InitializeComponent();
            DiscountList = new List<CashGeneralDicountDef>();
            ViewDiscountList = new List<CashGeneralDicountDef>();
            _PriorityLvlList = new List<MST_BUSPRIT_LVL>();
        }

        private bool ValidateSave()
        {
            decimal val;
            if (cmbEntityType.Text != "SUPPLIER")
            {
                if (txtNIC.Text == "" && txtBR.Text == "" && txtPP.Text == "" && txtDL.Text == "" && txtMob.Text == "")
                {
                    MessageBox.Show("One of required information not entered.\nEG. NIC Number, BR Number, PP Number, Mobile Number", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNIC.Focus();
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please enter name of customer", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return false;
            }
            if (cmbType.SelectedItem == null || cmbType.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Please select customer type", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbType.Focus();
                return false;
            }
            //if (cmbTitle.SelectedItem == null || cmbTitle.SelectedItem.ToString() == "")
            //{
            //    MessageBox.Show("Please select customer title", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cmbTitle.Focus();
            //    return false;
            //}
            if (cmbSex.SelectedItem == null || cmbSex.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Please select customer gender", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbSex.Focus();
                return false;
            }
            if (txtPerAdd1.Text == "" && txtPreAdd2.Text == "")
            {
                MessageBox.Show("Please enter customer address", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbSex.Focus();
                return false;
            }
            if (!decimal.TryParse(txtCredLimit.Text, out val))
            {
                MessageBox.Show("Please enter credit limit in number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCredLimit.Focus();
                return false;
            }
            //check segmentation
            grvCustSegmentation.EndEdit();
            if (cmbTitle.SelectedItem.ToString() != "  ")
            {
                foreach (DataGridViewRow gr in grvCustSegmentation.Rows)
                {
                    string isMand = gr.Cells[0].Value.ToString();
                    string type = gr.Cells[1].Value.ToString();
                    if (isMand.ToUpper() == "TRUE")
                    {
                        if (gr.Cells[2].Value == null)
                        {
                            MessageBox.Show("Please select customer segmentation " + type + " value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                }
            }
            if (txtPerEmail.Text != "")
            {
                if (!IsValidEmail(txtPerEmail.Text))
                {
                    MessageBox.Show("Permanent email address invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPerEmail.Focus();
                    return false;
                }
            }
            if (txtWorkEmail.Text != "")
            {
                if (!IsValidEmail(txtWorkEmail.Text))
                {
                    MessageBox.Show("Working email address invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtWorkEmail.Focus();
                    return false;
                }
            }

            if (chkSMS.Checked && txtMob.Text == "")
            {
                MessageBox.Show("Mobile number compulsory when agree to send SMS", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMob.Focus();
                return false;
            }

            //NOT VALIDATING
            //CITY/COUNTRY/DISTRICT
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //save customer details
                string _cusCode = "";
                Int32 _effect = 0;

                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10099))
                {
                    MessageBox.Show("Sorry, You have no permission to save the customer!\n( Advice: Required permission code :10099 )");
                    return;
                }

                //NEED TO VALIDATE SEGMANTATION MANDATORY VALUES
                if (ValidateSave())
                {
                    try
                    {
                        //Save main details

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

                        // Nadeeka 30-07-2015
                        if (string.IsNullOrEmpty(txtPerTown.Text))
                        {
                            MessageBox.Show("Please enter town", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtPerTown.Focus();
                            return;
                        }
                        if (string.IsNullOrEmpty(txtMob.Text))
                        {
                            MessageBox.Show("Please enter mobile #", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtMob.Focus();
                            return;
                        }

                        Collect_Cust();
                        Collect_GroupCust();
                        DateTime _date = CHNLSVC.Security.GetServerDateTime();
                        //save customer segmentation
                        foreach (DataGridViewRow gvr in this.grvCustSegmentation.Rows)
                        {
                            MasterBusinessEntityInfo bisInfo = new MasterBusinessEntityInfo();
                            //bisInfo.Mbei_cd
                            bisInfo.Mbei_com = BaseCls.GlbUserComCode;
                            string type = gvr.Cells[1].Value.ToString();
                            bisInfo.Mbei_tp = type;
                            if (gvr.Cells[2].Value != null)
                                bisInfo.Mbei_val = gvr.Cells[2].Value.ToString();
                            else
                                bisInfo.Mbei_val = string.Empty;
                            if (!string.IsNullOrEmpty(bisInfo.Mbei_val))
                            {
                                _busInfoList.Add(bisInfo);
                            }
                        }
                        //ACCOUNT
                        _account.Saca_com_cd = BaseCls.GlbUserComCode;
                        try
                        {
                            if (txtCredLimit.Text.Trim() != string.Empty)
                            {
                                _account.Saca_crdt_lmt = Convert.ToDecimal(txtCredLimit.Text.Trim());
                                _custProfile.Mbe_sub_tp = "D";
                            }
                            else
                            {
                                _account.Saca_crdt_lmt = 0;
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Invalid credit limit!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        _account.Saca_cre_by = BaseCls.GlbUserID;
                        _account.Saca_cre_when = DateTime.Now.Date;
                        // _account.Saca_cust_cd = _invoiceHeader.Sah_cus_cd;
                        _account.Saca_mod_by = BaseCls.GlbUserID;
                        _account.Saca_mod_when = DateTime.Now.Date;
                        _account.Saca_ord_bal = 0;
                        _account.Saca_session_id = BaseCls.GlbUserSessionID;

                        List<MasterBusinessEntitySalesType> _salesTypes = new List<MasterBusinessEntitySalesType>();
                        foreach (MasterInvoiceType sal in SalesType)
                        {
                            MasterBusinessEntitySalesType _type = new MasterBusinessEntitySalesType();
                            _type.Mbsa_sa_tp = sal.Srtp_cd;
                            _type.Mbsa_cre_by = BaseCls.GlbUserID;
                            _type.Mbsa_cre_dt = _date;
                            _type.Mbsa_mod_by = BaseCls.GlbUserID;
                            _type.Mbsa_mod_dt = _date;
                            _type.Mbsa_act = true;
                            _type.Mbsa_com = BaseCls.GlbUserComCode;
                            _type.Mbsa_tp = _custProfile.Mbe_tp;
                            _type.Mbsa_valid_frm_dt = sal.Srtp_valid_from_dt;
                            _type.Mbsa_valid_to_dt = sal.Srtp_valid_to_dt;
                            _salesTypes.Add(_type);
                        }

                        // _effect = CHNLSVC.Sales.SaveBusinessEntityDetail(_custProfile, _account, _busInfoList, out _cusCode, _salesTypes);
                        List<BusEntityItem> _lstBusItm = new List<BusEntityItem>();
                        _effect = CHNLSVC.Sales.SaveBusinessEntityDetailWithGroup(_custProfile, _account, _busInfoList,_lstBusItm, out _cusCode, _salesTypes, _isExsit, _isGroup, _custGroup);

                        if (_effect == 1)
                        {
                            if (chkByRequest.Checked)
                            {
                                CHNLSVC.Sales.UpdateCreditCustomerRequest(BaseCls.GlbUserComCode, Convert.ToString(cmbRequest.SelectedValue), _cusCode);
                            }

                            MessageBox.Show("New customer created. Customer Code : " + _cusCode, "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //save discount
                            List<CashGeneralDicountDef> saveDiscountList = new List<CashGeneralDicountDef>();
                            foreach (CashGeneralDicountDef disc in DiscountList)
                            {
                                saveDiscountList.Add(disc);
                            }
                            if (DiscountList.Count > 0)
                            {
                                Int32 effect = 0;
                                foreach (CashGeneralDicountDef disc in saveDiscountList)
                                {
                                    disc.Sgdd_cust_cd = _cusCode;
                                    if (disc.Sgdd_pc == "All")
                                    {
                                        List<string> pclist = new List<string>();
                                        pclist = CHNLSVC.Sales.GetAllProfCenters(disc.Sgdd_com);
                                        foreach (string pc in pclist)
                                        {
                                            List<string> pclist_ = new List<string>();
                                            pclist_.Add(pc);
                                            effect = CHNLSVC.Sales.SaveBusinessEntityDiscount(disc, pclist_);
                                        }
                                    }
                                    else
                                    {
                                        List<string> pclist = new List<string>();
                                        pclist.Add(disc.Sgdd_pc);
                                        effect = CHNLSVC.Sales.SaveBusinessEntityDiscount(disc, pclist);
                                    }
                                }
                                if (effect < 1)
                                {
                                    MessageBox.Show("No entries gone to discount table", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else
                                {
                                    MessageBox.Show("Discounts inserted successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                        if (_isFromOther == true)
                        {
                            HP.AccountCreation _acc = new HP.AccountCreation();
                            obj_TragetTextBox.Text = _cusCode;
                            this.Close();
                        }
                        Clear_Data();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CHNLSVC.CloseChannel();
                        return;
                    }
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10100))
                {
                    MessageBox.Show("Sorry, You have no permission to update the customer!\n( Advice: Required permission code :10100 )");
                    return;
                }

                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                if (ValidateSave())
                {
                    try
                    {
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

                        // Nadeeka 30-07-2015
                        if (string.IsNullOrEmpty(txtPerTown.Text))
                        {
                            MessageBox.Show("Please enter customer permanent town", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtPerTown.Focus();
                            return;
                        }
                        if (string.IsNullOrEmpty(txtMob.Text))
                        {
                            MessageBox.Show("Please enter customer mobile #", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtMob.Focus();
                            return;
                        }

                        Collect_Cust();
                        Collect_GroupCust();
                        _custProfile.Mbe_cd = txtCusCode.Text.Trim().ToUpper();

                        //save customer segmentation
                        foreach (DataGridViewRow gvr in this.grvCustSegmentation.Rows)
                        {
                            MasterBusinessEntityInfo bisInfo = new MasterBusinessEntityInfo();
                            //bisInfo.Mbei_cd
                            bisInfo.Mbei_com = BaseCls.GlbUserComCode;
                            string type = gvr.Cells[1].Value.ToString();
                            bisInfo.Mbei_tp = type;
                            if (gvr.Cells[2].Value != null)
                                bisInfo.Mbei_val = gvr.Cells[2].Value.ToString();
                            else
                                bisInfo.Mbei_val = string.Empty;
                            if (!string.IsNullOrEmpty(bisInfo.Mbei_val))
                            {
                                _busInfoList.Add(bisInfo);
                            }
                        }

                        if (Convert.ToDecimal(txtCredLimit.Text) > 0)
                        {
                            _custProfile.Mbe_tp = "C";
                            _custProfile.Mbe_sub_tp = "D";
                        }
                        else
                        {
                            _custProfile.Mbe_tp = "C";
                        }

                        _account = SetCustomerCreditAccount();

                        List<MasterBusinessEntitySalesType> _salesTypes = new List<MasterBusinessEntitySalesType>();
                        foreach (MasterInvoiceType sal in SalesType)
                        {
                            MasterBusinessEntitySalesType _type = new MasterBusinessEntitySalesType();
                            _type.Mbsa_sa_tp = sal.Srtp_cd;
                            _type.Mbsa_cd = txtCusCode.Text.Trim();
                            _type.Mbsa_cre_by = BaseCls.GlbUserID;
                            _type.Mbsa_cre_dt = _date;
                            _type.Mbsa_mod_by = BaseCls.GlbUserID;
                            _type.Mbsa_mod_dt = _date;
                            _type.Mbsa_act = true;
                            _type.Mbsa_com = BaseCls.GlbUserComCode;
                            _type.Mbsa_tp = _custProfile.Mbe_tp;
                            _type.Mbsa_valid_frm_dt = sal.Srtp_valid_from_dt;
                            _type.Mbsa_valid_to_dt = sal.Srtp_valid_to_dt;
                            _salesTypes.Add(_type);
                        }
                        Int32 effect = 0;

                        //if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10086))
                        //{
                        //    effect = CHNLSVC.Sales.UpdateBusinessEntityProfileWithPermission(_custProfile, BaseCls.GlbUserID, _date, Convert.ToDecimal(txtCredLimit.Text.Trim()), _busInfoList, _salesTypes);
                        //}
                        //else
                        //{
                        //effect = CHNLSVC.Sales.UpdateBusinessEntityProfile(_custProfile, BaseCls.GlbUserID, _date, Convert.ToDecimal(txtCredLimit.Text.Trim()), _busInfoList, _salesTypes);
                        if (chkUpdateGrupLvl.Checked == true)
                        {
                            if (MessageBox.Show("You are going to update with group level customer details.Pls. confirm ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                            {
                                effect = CHNLSVC.Sales.UpdateBusinessEntityProfileWithGroupWithPermission(_custProfile, BaseCls.GlbUserID, _date, Convert.ToDecimal(txtCredLimit.Text.Trim()), _busInfoList, _salesTypes, _custGroup);
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            List<BusEntityItem> _lstBusItm = new List<BusEntityItem>();
                            effect = CHNLSVC.Sales.UpdateBusinessEntityProfileWithGroup(_custProfile, BaseCls.GlbUserID, _date, Convert.ToDecimal(txtCredLimit.Text.Trim()), _busInfoList, _salesTypes, _lstBusItm, _custGroup, null, null, _account);
                        }
                        //}

                        if (effect > 0)
                        {
                            if (chkByRequest.Checked)
                            {
                                CHNLSVC.Sales.UpdateCreditCustomerRequest(BaseCls.GlbUserComCode, Convert.ToString(cmbRequest.SelectedValue), txtCusCode.Text.Trim());
                            }

                            MessageBox.Show("Record update successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            List<CashGeneralDicountDef> saveDiscountList = new List<CashGeneralDicountDef>();
                            foreach (CashGeneralDicountDef disc in DiscountList)
                            {
                                saveDiscountList.Add(disc);
                            }
                            if (DiscountList.Count > 0)
                            {
                                Int32 effect_ = 0;

                                foreach (CashGeneralDicountDef disc in saveDiscountList)
                                {
                                    disc.Sgdd_cust_cd = _custProfile.Mbe_cd;//custCD;
                                    if (disc.Sgdd_pc == "All")
                                    {
                                        List<string> pclist = new List<string>();
                                        pclist = CHNLSVC.Sales.GetAllProfCenters(disc.Sgdd_com);
                                        foreach (string pc in pclist)
                                        {
                                            List<string> pclist_ = new List<string>();
                                            pclist_.Add(pc);
                                            effect_ = CHNLSVC.Sales.SaveBusinessEntityDiscount(disc, pclist_);
                                        }
                                    }
                                    else
                                    {
                                        List<string> pclist = new List<string>();
                                        pclist.Add(disc.Sgdd_pc);
                                        effect_ = CHNLSVC.Sales.SaveBusinessEntityDiscount(disc, pclist);
                                    }
                                }
                                if (effect_ < 1)
                                {
                                    MessageBox.Show("No entries gone to discount table", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    MessageBox.Show("Discounts inserted successfully.", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            Clear_Data();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Record update unsuccessful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Clear_Data();
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CHNLSVC.CloseChannel();
                        return;
                    }
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
            _custGroup.Mbg_act = chkActive.Checked;
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
            Int32 _cheIssue = 0;
            _custProfile = new MasterBusinessEntity();
            _custProfile.Mbe_acc_cd = null;
            _custProfile.Mbe_cd = txtCusCode.Text.Trim();
            _custProfile.Mbe_act = chkActive.Checked;
            _custProfile.Mbe_tel = txtPerPhone.Text.ToUpper();
            _custProfile.Mbe_add1 = txtPerAdd1.Text.Trim().ToUpper();
            _custProfile.Mbe_add2 = txtPerAdd2.Text.Trim().ToUpper();
            _custProfile.Mbe_town_cd = txtPerTown.Text.ToUpper();
            _custProfile.Mbe_country_cd = txtPerCountry.Text;
            _custProfile.Mbe_distric_cd = txtPerDistrict.Text;
            _custProfile.Mbe_province_cd = txtPerProvince.Text;
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
            _custProfile.Mbe_com = BaseCls.GlbUserComCode;
            _custProfile.Mbe_contact = null;
            _custProfile.Mbe_country_cd = null;
            _custProfile.Mbe_cr_add1 = txtPreAdd1.Text.Trim();
            _custProfile.Mbe_cr_add2 = txtPreAdd2.Text.Trim();
            _custProfile.Mbe_cr_country_cd = txtPreCountry.Text.Trim();
            _custProfile.Mbe_cr_distric_cd = txtPreDistrict.Text;
            _custProfile.Mbe_cr_email = null;
            _custProfile.Mbe_cr_fax = null;
            _custProfile.Mbe_cr_postal_cd = txtPrePostal.Text.Trim().ToUpper();
            _custProfile.Mbe_cr_province_cd = txtPreProvince.Text.Trim();
            _custProfile.Mbe_cr_tel = txtPrePhone.Text.Trim().ToUpper();
            _custProfile.Mbe_cr_town_cd = txtPreTown.Text.Trim().ToUpper();
            _custProfile.Mbe_cre_by = BaseCls.GlbUserID;
            _custProfile.Mbe_cre_dt = Convert.ToDateTime(DateTime.Today).Date;
            _custProfile.Mbe_cre_pc = BaseCls.GlbUserDefProf;
            _custProfile.Mbe_cust_com = BaseCls.GlbUserComCode.ToUpper();
            _custProfile.Mbe_cust_loc = BaseCls.GlbUserDefLoca.ToUpper();
            _custProfile.Mbe_distric_cd = txtPerDistrict.Text;
            _custProfile.Mbe_dl_no = txtDL.Text.Trim().ToUpper();
            _custProfile.Mbe_dob = Convert.ToDateTime(dtpDOB.Text).Date;
            _custProfile.Mbe_email = txtPerEmail.Text.Trim();
            _custProfile.Mbe_fax = null;
            _custProfile.Mbe_ho_stus = "GOOD";
            _custProfile.Mbe_income_grup = null;
            _custProfile.Mbe_intr_com = false;
            _custProfile.Mbe_is_suspend = false;
            _custProfile.Mbe_cust_com = BaseCls.GlbUserComCode;
            _custProfile.Mbe_cust_loc = BaseCls.GlbUserDefProf;
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
            _custProfile.Mbe_name = txtName.Text.Trim().ToUpper();
            _custProfile.Mbe_nic = txtNIC.Text.Trim().ToUpper();
            _custProfile.Mbe_oth_id_no = null;
            _custProfile.Mbe_oth_id_tp = null;
            _custProfile.Mbe_pc_stus = "GOOD";
            _custProfile.Mbe_postal_cd = txtPerPostal.Text.Trim().ToUpper();
            _custProfile.Mbe_pp_no = txtPP.Text.Trim();
            _custProfile.Mbe_province_cd = txtPerProvince.Text.Trim().ToUpper();
            _custProfile.Mbe_sex = cmbSex.Text;
            _custProfile.Mbe_sub_tp = null;
            _custProfile.Mbe_svat_no = txtSVATReg.Text.Trim().ToUpper();

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
            if (chkChqPrint.Checked == true)
            {
                _cheIssue = 1;
            }
            else
            {
                _cheIssue = 0;
            }
            _custProfile.Mbe_agre_send_email = _isEmail;
            _custProfile.MBE_CHQ_ISS = _cheIssue;
            _custProfile.Mbe_cust_lang = cmbPrefLang.SelectedValue.ToString();

            //kapila 5/1/2016
            _custProfile.Mbe_alw_dcn = Convert.ToInt32(chkDCN.Checked);
            _custProfile.Mbe_ins_man = Convert.ToInt32(chkInsMan.Checked);
            _custProfile.Mbe_min_dp_per = Convert.ToDecimal(txtMinDP.Text);

        }
        private void dobGeneration()
        {// NADEEKA 18-12-2014
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

            }
            catch (Exception ex)
            {
            }
        }



        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Clear_Data();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region common search

        private void btnCompany_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtComCode;
                _CommonSearch.txtSearchbyword.Text = txtComCode.Text;
                _CommonSearch.ShowDialog();
                txtComCode.Focus();
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

        private void btnPC_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC;
                _CommonSearch.txtSearchbyword.Text = txtPC.Text;
                _CommonSearch.ShowDialog();
                txtPC.Focus();
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

        private void btnPriceBook_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPB;
                _CommonSearch.txtSearchbyword.Text = txtPB.Text;
                _CommonSearch.ShowDialog();
                txtPB.Focus();
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

        private void btnPriceLevel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPBLvl;
                _CommonSearch.txtSearchbyword.Text = txtPBLvl.Text;
                _CommonSearch.ShowDialog();
                txtPBLvl.Focus();
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

        private void btnBrand_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBrand;
                _CommonSearch.txtSearchbyword.Text = txtBrand.Text;
                _CommonSearch.ShowDialog();
                txtBrand.Focus();
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

        private void btnMainCategory_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate1;
                _CommonSearch.txtSearchbyword.Text = txtCate1.Text;
                _CommonSearch.ShowDialog();
                txtCate1.Focus();
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

        private void btnCategory_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate2;
                _CommonSearch.txtSearchbyword.Text = txtCate2.Text;
                _CommonSearch.ShowDialog();
                txtCate2.Focus();
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

        private void btnSubCategory_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate3;
                _CommonSearch.txtSearchbyword.Text = txtCate3.Text;
                _CommonSearch.ShowDialog();
                txtCate3.Focus();
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

        private void btnItem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCD;
                _CommonSearch.txtSearchbyword.Text = txtItemCD.Text;
                _CommonSearch.ShowDialog();
                txtItemCD.Focus();
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
                _CommonSearch.obj_TragetTextBox = txtPerTown;
                _CommonSearch.ShowDialog();
                txtPerTown.Select();
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

        private void btnPreTown_Click(object sender, EventArgs e)
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

        private void btnCusCode_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                if (cmbEntityType.Text == "SUPPLIER")
                {


                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierCommon);
                    DataTable _result = CHNLSVC.CommonSearch.GetSupplierCommon(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCusCode;
                    _CommonSearch.ShowDialog();
                    txtCusCode.Select();
                }
                else
                {
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                    DataTable _resultSup = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _resultSup;
                    _CommonSearch.BindUCtrlDDLData(_resultSup);
                    _CommonSearch.obj_TragetTextBox = txtCusCode;
                    _CommonSearch.ShowDialog();
                    txtCusCode.Select();
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

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPB.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(txtComCode.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SupplierCommon:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + seperator + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_Type: //Akila 2017/10/26
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        #endregion common search

        private void LoadSalesTypes()
        {
            DataTable datasource = CHNLSVC.General.GetSalesTypes("", null, null);
            ComboBoxDraw(datasource, cmbSalesTp, "srtp_cd", "srtp_desc");
        }

        private void ComboBoxDraw(DataTable table, ComboBox combo, string code, string desc)
        {
            combo.DataSource = table;
            combo.DisplayMember = desc;
            combo.ValueMember = code;

            // Enable the owner draw on the ComboBox.
            combo.DrawMode = DrawMode.OwnerDrawVariable;
            // Handle the DrawItem event to draw the items.
            combo.DrawItem += delegate(object cmb, DrawItemEventArgs args)
            {
                // Draw the default background
                args.DrawBackground();

                // The ComboBox is bound to a DataTable,
                // so the items are DataRowView objects.
                DataRowView drv = (DataRowView)combo.Items[args.Index];

                // Retrieve the value of each column.
                string id = drv[code].ToString();
                string name = drv[desc].ToString();

                // Get the bounds for the first column
                Rectangle r1 = args.Bounds;
                r1.Width /= 2;

                // Draw the text on the first column
                using (SolidBrush sb = new SolidBrush(args.ForeColor))
                {
                    args.Graphics.DrawString(id, args.Font, sb, r1);
                }

                // Draw a line to isolate the columns
                using (Pen p = new Pen(Color.Black))
                {
                    args.Graphics.DrawLine(p, r1.Right - 5, 0, r1.Right - 5, r1.Bottom);
                }

                // Get the bounds for the second column
                Rectangle r2 = args.Bounds;
                r2.X = args.Bounds.Width / 2;
                r2.Width /= 2;

                // Draw the text on the second column
                using (SolidBrush sb = new SolidBrush(args.ForeColor))
                {
                    args.Graphics.DrawString(name, args.Font, sb, r2);
                }
            };
        }

        private void CustomerCreation_Load(object sender, EventArgs e)
        {
            try
            {
                LoadSalesTypes();
                LoadCustomerSegmantation();
                cmbType.SelectedIndex = 0;
                cmbTitle.SelectedIndex = 0;
                cmbSex.SelectedIndex = 0;
                cmbSHStatus.SelectedIndex = 0;
                cmbHOStatus.SelectedIndex = 0;
                cmbPrefLang.SelectedIndex = 0;
                cmbType.Focus();
                BindSalesTypes();
                LoadLanguage();
                LoadEntityType();
                BindHeirachy();
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

        private void BindSalesTypes()
        {
           /* cmbSalesType.DataSource = CHNLSVC.General.GetSalesTypes("", null, null);
            cmbSalesType.DisplayMember = "srtp_desc";
            cmbSalesType.ValueMember = "srtp_cd";*/
        }

        private void BindHeirachy()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("LOC", "Location");
            PartyTypes.Add("SCHNL", "Sub Channel");

            DropDownListPartyTypes.DataSource = new BindingSource(PartyTypes, null);
            DropDownListPartyTypes.DisplayMember = "Value";
            DropDownListPartyTypes.ValueMember = "Key";
        }

        private void btnAddCredit_Click(object sender, EventArgs e)
        {
            try
            {
                AddToCredList();
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

        protected void AddToCredList()
        {
            if (txtPC.Text.Trim() == "" || txtPC.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter profit center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtComCode.Text.Trim() == "" || txtComCode.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter company !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtPB.Text.Trim() == "" || txtPB.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Price Book!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtPBLvl.Text.Trim() == "" || txtPBLvl.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Price Book Level!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CashGeneralDicountDef cd = new CashGeneralDicountDef();
            cd.Sgdd_com = BaseCls.GlbUserComCode;

            if (chkAllPC.Checked)
            {
                cd.Sgdd_pc = "All";
            }
            else
            {
                cd.Sgdd_pc = txtPC.Text.Trim();
            }

            cd.Sgdd_sale_tp = cmbSalesTp.SelectedValue.ToString();
            cd.Sgdd_pb = txtPB.Text.Trim();
            cd.Sgdd_pb_lvl = txtPBLvl.Text.Trim();
            cd.Sgdd_alw_pro = chkAllowProm.Checked;
            cd.Sgdd_alw_ser = chkAllowSer.Checked;
            if (txtBrand.Text.Trim().ToUpper() == "N/A")
            {
                cd.Sgdd_brand = string.Empty;
            }
            else
            {
                cd.Sgdd_brand = txtBrand.Text.Trim(); //ddlBrand.SelectedValue;
            }

            if (txtCate1.Text.Trim().ToUpper() == "N/A")
            {
                cd.Sgdd_cate1 = string.Empty;
            }
            else
            {
                cd.Sgdd_cate1 = txtCate1.Text.Trim();//ddlCate1.SelectedValue;
            }

            if (txtCate2.Text.Trim().ToUpper() == "N/A")
            {
                cd.Sgdd_cate2 = string.Empty;
            }
            else
            {
                cd.Sgdd_cate2 = txtCate2.Text.Trim();//ddlCate2.SelectedValue;
            }

            if (txtCate3.Text.Trim().ToUpper() == "N/A")
            {
                cd.Sgdd_cate3 = string.Empty;
            }
            else
            {
                cd.Sgdd_cate3 = txtCate3.Text.Trim();//ddlCate3.SelectedValue;
            }

            cd.Sgdd_cre_by = BaseCls.GlbUserID;
            cd.Sgdd_cre_dt = DateTime.Now.Date;
            cd.Sgdd_itm = txtItemCD.Text.Trim();
            cd.Sgdd_stus = true;

            cd.Sgdd_from_dt = dateTimePickerFromDT.Value;
            cd.Sgdd_to_dt = dateTimePickerToDT.Value;

            //cd.Sgdd_cust_cd
            try
            {
                Convert.ToDecimal(txtDiscount.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid discount rate/value!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (chkIsRate.Checked)
            {
                if (Convert.ToDecimal(txtDiscount.Text.Trim()) > 100)
                {
                    MessageBox.Show("Discount rate cannot be grater than 100!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                cd.Sgdd_disc_rt = Convert.ToDecimal(txtDiscount.Text.Trim());
            }
            else
            {
                cd.Sgdd_disc_val = Convert.ToDecimal(txtDiscount.Text.Trim());
            }
            try
            {
                if (txtNoOfcredTimes.Text.Trim() == "")
                {
                    cd.Sgdd_no_of_times = 9999;//TODO: get default number
                }
                else
                {
                    cd.Sgdd_no_of_times = Convert.ToInt32(txtNoOfcredTimes.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No. of times for credit is invalid!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            grvCreditLimDet.AutoGenerateColumns = false;
            Temp_discount_seq = Temp_discount_seq + 1;
            cd.Sgdd_seq = Temp_discount_seq;
            DiscountList.Add(cd);

            BindingSource source = new BindingSource();
            source.DataSource = DiscountList;
            grvCreditLimDet.DataSource = source;

            txtComCode.Text = "";
            txtPC.Text = "";
            txtPB.Text = "";
            txtPBLvl.Text = "";
            txtBrand.Text = "";
            txtCate1.Text = "";
            txtCate2.Text = "";
            txtCate3.Text = "";
            txtItemCD.Text = "";
            txtDiscount.Text = "0";
            txtNoOfcredTimes.Text = "";
            chkAllPC.Checked = false;
            chkIsRate.Checked = false;
        }

        private void LoadCustomerSegmantation()
        {
            grvCustSegmentation.AutoGenerateColumns = false;
            DataTable dt = new DataTable();
            dt = CHNLSVC.Sales.GetBusinessEntityTypes("C");
            List<BusinessEntityTYPE> bindtypeList = new List<BusinessEntityTYPE>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    // Get the value of the wanted column and cast it to string
                    string TP = Convert.ToString(r["RBT_TP"]);
                    string DESC = Convert.ToString(r["RBT_DESC"]); //rbv_val
                    Boolean isMandetory = Convert.ToBoolean(r["RBT_MAD"]);
                    BusinessEntityTYPE bizTP = new BusinessEntityTYPE();
                    if (isMandetory)
                        bizTP.IsMandatory = "True";
                    else
                        bizTP.IsMandatory = "False";
                    bizTP.TypeCD_ = TP;
                    bizTP.TypeDesctipt = DESC;
                    bindtypeList.Add(bizTP);
                }
            }
            grvCustSegmentation.DataSource = bindtypeList;
        }

        private void grvCustSegmentation_DataSourceChanged(object sender, EventArgs e)
        {
            //foreach(DataGridViewRow grv in grvCustSegmentation.Rows){
            //    DataGridViewComboBoxCell _combo = (DataGridViewComboBoxCell)grv.Cells[2];
            //    string typeName = grv.Cells[1].Value.ToString();
            //    DataGridViewCheckBoxCell _check = (DataGridViewCheckBoxCell)grv.Cells[0];
            //    string isMand = _check.Value.ToString();
            //    if (isMand.ToUpper() == "TRUE")
            //    {
            //        Color myColor = Color.PaleVioletRed;
            //        grv.Cells[0].Style.BackColor = myColor;
            //    }
            //    else
            //    {
            //        grv.Cells[0].Style.BackColor = grvCustSegmentation.DefaultCellStyle.BackColor;
            //    }

            //    DataTable dtVal = new DataTable();
            //    dtVal = CHNLSVC.Sales.GetBusinessEntityAllValues("C", typeName);

            //    List<string> ddlBindList = new List<string>();

            //    if (dtVal.Rows.Count > 0)
            //    {
            //        ddlBindList.Add(string.Empty);
            //        foreach (DataRow r in dtVal.Rows)
            //        {
            //            ddlBindList.Add(Convert.ToString(r["RBV_VAL"]));

            //        }
            //    }
            //    BindingSource _source = new BindingSource();
            //    _source.DataSource = ddlBindList;
            //    _combo.DataSource = _source;

            // }
        }

        private void grvCustSegmentation_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                DataGridViewComboBoxCell _combo = (DataGridViewComboBoxCell)grvCustSegmentation.Rows[e.RowIndex].Cells[2];
                string typeName = grvCustSegmentation.Rows[e.RowIndex].Cells[1].Value.ToString();
                DataGridViewCheckBoxCell _check = (DataGridViewCheckBoxCell)grvCustSegmentation.Rows[e.RowIndex].Cells[0];
                string isMand = _check.Value.ToString();
                if (isMand.ToUpper() == "TRUE")
                {
                    Color myColor = Color.PaleVioletRed;
                    grvCustSegmentation.Rows[e.RowIndex].Cells[0].Style.BackColor = myColor;
                    grvCustSegmentation.Rows[e.RowIndex].Cells[0].Style.SelectionBackColor = Color.Transparent;
                }
                else
                {
                    grvCustSegmentation.Rows[e.RowIndex].Cells[0].Style.BackColor = grvCustSegmentation.DefaultCellStyle.BackColor;
                    grvCustSegmentation.Rows[e.RowIndex].Cells[0].Style.SelectionBackColor = grvCustSegmentation.DefaultCellStyle.SelectionBackColor;
                }

                DataTable dtVal = new DataTable();
                dtVal = CHNLSVC.Sales.GetBusinessEntityAllValues("C", typeName);

                List<string> ddlBindList = new List<string>();
                _combo.Items.Clear();
                if (dtVal.Rows.Count > 0)
                {
                    foreach (DataRow r in dtVal.Rows)
                    {
                        _combo.Items.Add(Convert.ToString(r["RBV_VAL"]));
                    }
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

        private void txtCusCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (chkByRequest.Checked) return;

                if (!string.IsNullOrEmpty(txtCusCode.Text))
                {
                    MasterBusinessEntity custProf = GetbyCustCD(txtCusCode.Text.Trim().ToUpper());
                    if (custProf.Mbe_cd != null)
                    {
                        btnSave.Enabled = false;
                        btnUpdate.Enabled = true;
                        LoadCustProf(custProf);

                        if (!string.IsNullOrEmpty(txtNIC.Text))
                        {
                            Boolean _isValid = IsValidNIC(txtNIC.Text.Trim());
                            if (_isValid == true)
                            {
                                txtNIC.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        string cusCD = txtCusCode.Text.Trim().ToUpper();
                        MasterBusinessEntity cust_null = new MasterBusinessEntity();
                        //LoadCustProf(cust_null);
                        txtCusCode.Text = cusCD;
                        GroupBussinessEntity _grupProf = GetbyCustCDGrup(txtCusCode.Text.Trim().ToUpper());
                        if (_grupProf.Mbg_cd != null)
                        {
                            LoadCustProfByGrup(_grupProf);
                            _isGroup = true;
                            _isExsit = false;
                        }
                        else
                        {
                            _isGroup = false;
                        }
                        btnSave.Enabled = true;
                        btnUpdate.Enabled = false;
                    }
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

        public void LoadCustProf(MasterBusinessEntity cust)
        {
            //string typed_nic = txtNicNo.Text.Trim();
            //string typed_ppno = txtPassportNo.Text.Trim().ToUpper();
            //string typed_dl = txtDLno.Text.Trim().ToUpper();
            //string typed_br = txtBrNo.Text.Trim().ToUpper();
            //------------------------------------------
            // ddlCustSupType.SelectedValue = cust.Mbe_sub_tp;
            cmbType.Text = cust.Mbe_cate;
            txtNIC.Text = cust.Mbe_nic.ToUpper();
            txtPP.Text = cust.Mbe_pp_no.ToUpper();
            txtBR.Text = cust.Mbe_br_no.ToUpper();
            txtCusCode.Text = cust.Mbe_cd.ToUpper();
            txtDL.Text = cust.Mbe_dl_no.ToUpper();
            txtMob.Text = cust.Mbe_mob.ToUpper();
            chkSMS.Checked = cust.Mbe_agre_send_sms;
            chkActive.Checked = cust.Mbe_act;
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

            txtPerAdd1.Text = cust.Mbe_add1.ToUpper();
            txtPerAdd2.Text = cust.Mbe_add2.ToUpper();
            txtPerTown.Text = cust.Mbe_town_cd.ToUpper();
            txtPerPostal.Text = cust.Mbe_postal_cd.ToUpper();
            txtPerCountry.Text = cust.Mbe_country_cd.ToUpper();
            txtPerDistrict.Text = cust.Mbe_distric_cd.ToUpper();
            txtPerProvince.Text = cust.Mbe_province_cd.ToUpper();
            txtPerPhone.Text = cust.Mbe_tel.ToUpper();
            txtPerEmail.Text = cust.Mbe_email;

            txtPreAdd1.Text = cust.Mbe_cr_add1.ToUpper();
            txtPreAdd2.Text = cust.Mbe_cr_add2.ToUpper();
            txtPreTown.Text = cust.Mbe_cr_town_cd.ToUpper();
            txtPrePostal.Text = cust.Mbe_cr_postal_cd.ToUpper();
            txtPreCountry.Text = cust.Mbe_cr_country_cd.ToUpper();
            txtPreDistrict.Text = cust.Mbe_cr_distric_cd.ToUpper();
            txtPreProvince.Text = cust.Mbe_cr_province_cd.ToUpper();
            txtPrePhone.Text = cust.Mbe_cr_tel;

            txtWorkAdd1.Text = cust.Mbe_wr_add1.ToUpper();
            txtWorkAdd2.Text = cust.Mbe_wr_add2.ToUpper();
            txtWorkName.Text = cust.Mbe_wr_com_name.ToUpper();
            txtWorkDept.Text = cust.Mbe_wr_dept.ToUpper();
            txtWorkDesig.Text = cust.Mbe_wr_designation.ToUpper();
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
            // Added By Nadeeka 15-12-2014
            if (cust.MBE_CHQ_ISS == 1)
            {
                chkChqPrint.Checked = true;
            }
            else
            {
                chkChqPrint.Checked = false;
            }
            if (string.IsNullOrEmpty(cust.Mbe_cust_lang))
            {
                cmbPrefLang.SelectedValue = "E";

            }
            else
            {
                cmbPrefLang.SelectedValue = cust.Mbe_cust_lang;
            }
            cmbEntityType.SelectedValue = cust.Mbe_tp;
            //------------------------------------------

            txtCusCode.Enabled = false;

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

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10086))
            {
                txtInit.Enabled = true;
                txtFname.Enabled = true;
                txtSName.Enabled = true;
                txtName.Enabled = true;
            }
            else
            {
                txtInit.Enabled = false;
                txtFname.Enabled = false;
                txtSName.Enabled = false;
                txtName.Enabled = false;
            }

            //kapila 5/1/2016
            if (cust.Mbe_alw_dcn == 1)
                chkDCN.Checked = true;
            else
                chkDCN.Checked = false;

            if (cust.Mbe_ins_man == 1)
                chkInsMan.Checked = true;
            else
                chkInsMan.Checked = false;

            txtMinDP.Text = cust.Mbe_min_dp_per.ToString();

            //TODO
            //load discounts
            DataTable _dt = CHNLSVC.Sales.GetCustomerAllowInvoiceTypeNew(BaseCls.GlbUserComCode, cust.Mbe_cd);

            if (_dt.Rows.Count > 0)
            {
                SalesType = new List<MasterInvoiceType>();
                BindingSource _newSource = new BindingSource();
                foreach (DataRow _row in _dt.Rows)
                {
                    MasterInvoiceType _invType = new MasterInvoiceType();
                    _invType.Srtp_cd = _row["Srtp_cd"].ToString();
                    _invType.Srtp_desc = _row["SRTP_DESC"].ToString();
                    _invType.Srtp_valid_from_dt =_row["MBSA_VALID_FRM_DT"] == DBNull.Value ? DateTime.MinValue: Convert.ToDateTime(_row["MBSA_VALID_FRM_DT"]).Date;
                    _invType.Srtp_valid_to_dt = _row["MBSA_VALID_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(_row["MBSA_VALID_TO_DT"]).Date;
                    SalesType.Add(_invType);
                }
                grvSalesType.AutoGenerateColumns = false;
                _newSource.DataSource = SalesType;
                grvSalesType.DataSource = _newSource;
                txtInvoiceType.Text = null;
            }
            else
            {
                grvSalesType.DataSource = new List<MasterInvoiceType>();
            }

            //load segmentation
            List<MasterBusinessEntityInfo> _segmentation = CHNLSVC.Sales.GetCustomerSegmentation(cust.Mbe_cd);
            if (_segmentation != null && _segmentation.Count > 0)
            {
                foreach (DataGridViewRow gr in grvCustSegmentation.Rows)
                {
                    string item = gr.Cells[1].Value.ToString();
                    gr.Cells[2].Value = (from _res in _segmentation
                                         where _res.Mbei_tp == item && _res.Mbei_com == BaseCls.GlbUserComCode
                                         select _res.Mbei_val).SingleOrDefault<string>();
                }
            }

            //load credit limit
            CustomerAccountRef _cust = CHNLSVC.Sales.GetCustomerAccount(BaseCls.GlbUserComCode, cust.Mbe_cd);
            if (_cust != null)
            {
                txtCredLimit.Text = _cust.Saca_crdt_lmt.ToString();
            }

            //load priority levels   ---kapila 28/1/2015
            _PriorityLvlList = CHNLSVC.CustService.GetCustomerPriorityLevel(cust.Mbe_cd, BaseCls.GlbUserComCode);
            grvParty.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = _PriorityLvlList;
            grvParty.DataSource = _source;
        }

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

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10086))
            {
                txtInit.Enabled = true;
                txtFname.Enabled = true;
                txtSName.Enabled = true;
                txtName.Enabled = true;
            }
            else
            {
                txtInit.Enabled = false;
                txtFname.Enabled = false;
                txtSName.Enabled = false;
                txtName.Enabled = false;
            }

            txtCusCode.Enabled = false;

        }

        #region LoadCustProfile

        public MasterBusinessEntity GetbyCustCD(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerAllProfileByCom(custCD, null, null, null, null, BaseCls.GlbUserComCode, cmbEntityType.SelectedValue.ToString());
        }

        public MasterBusinessEntity GetbyNIC(string nic)
        {
            return CHNLSVC.Sales.GetCustomerAllProfileByCom(null, nic, null, null, null, BaseCls.GlbUserComCode, cmbEntityType.SelectedValue.ToString());
        }

        public MasterBusinessEntity GetbyDL(string dl)
        {
            return CHNLSVC.Sales.GetCustomerAllProfileByCom(null, null, dl, null, null, BaseCls.GlbUserComCode, cmbEntityType.SelectedValue.ToString());
        }

        public MasterBusinessEntity GetbyPPno(string ppno)
        {
            return CHNLSVC.Sales.GetCustomerAllProfileByCom(null, null, null, ppno, null, BaseCls.GlbUserComCode, cmbEntityType.SelectedValue.ToString());
        }

        public MasterBusinessEntity GetbyBrNo(string brNo)
        {
            return CHNLSVC.Sales.GetCustomerAllProfileByCom(null, null, null, null, brNo, BaseCls.GlbUserComCode, cmbEntityType.SelectedValue.ToString());
        }

        #endregion LoadCustProfile

        private void Clear_Data()
        {
            chkSalesAll.Checked = false;
            txtInvoiceType.Text = null;
            txtRPc.Text = null;
            pnlDateRange.Visible = false;
            _custProfile = new MasterBusinessEntity();
            _account = new CustomerAccountRef();
            _busInfoList = new List<MasterBusinessEntityInfo>();
            _custGroup = new GroupBussinessEntity();
            _PriorityLvlList = new List<MST_BUSPRIT_LVL>();
            SalesType = new List<MasterInvoiceType>();

            _isGroup = false;
            txtCusCode.Text = "";
            txtCusCode.Enabled = true;
            txtNIC.Enabled = true;
            chkMail.Checked = false;
            chkChqPrint.Checked = false;
            cmbType.Text = "INDIVIDUAL";
            cmbPrefLang.Text = "ENGLISH";
            cmbEntityType.Text = "CUSTOMER";
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
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            cmbType.Focus();

            //clear
            txtCredLimit.Text = "0";
            txtComCode.Text = "";
            txtPC.Text = "";
            txtBrand.Text = "";
            txtCate1.Text = "";
            txtCate2.Text = "";
            txtCate3.Text = "";
            txtItemCD.Text = "";
            txtDiscount.Text = "0";
            chkAllPC.Checked = false;
            chkIsRate.Checked = false;
            chkAllowSer.Checked = false;
            chkAllowProm.Checked = false;

            grvCreditLimDet.DataSource = null;
            grvCustSegmentation.DataSource = null;

            cmbType.SelectedIndex = 0;
            cmbTitle.SelectedIndex = 0;
            cmbSex.SelectedIndex = 0;
            cmbSHStatus.SelectedIndex = 0;
            cmbHOStatus.SelectedIndex = 0;
            cmbEntityType.SelectedIndex = 0;

            LoadCustomerSegmantation();
            LoadSalesTypes();
            grvSalesType.DataSource = null;
            chkSalesAll.Checked = false;

            pnlMain.Enabled = true;
            pnlDuplicate.Visible = false;
            lblErrMsg.Text = "";
            grvDuplicate.DataSource = null;

            cmbRequest.DataSource = null;
            chkByRequest.Checked = false;
            chkActive.Checked = false;
            chkUpdateGrupLvl.Checked = false;
            txtInit.Text = "";
            txtFname.Text = "";
            txtSName.Text = "";

            chkDCN.Checked = false;
            chkInsMan.Checked = false;
            txtMinDP.Text = "0";

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10086))
            {
                chkUpdateGrupLvl.Enabled = true;
            }
            else
            {
                chkUpdateGrupLvl.Enabled = false;
            }
        }

        private void txtNIC_Leave(object sender, EventArgs e)
        {
            if (chkByRequest.Checked) return;
            try
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
                        //check multiple
                        //List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, txtNIC.Text, "", "", "", "", 1);
                        List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, txtNIC.Text, string.Empty, "C");

                        if (_custList != null && _custList.Count > 1 && txtNIC.Text.ToUpper() != "N/A")
                        {
                            pnlMain.Enabled = false;
                            pnlDuplicate.Visible = true;
                            lblErrMsg.Text = "Entered NIC number has multiple duplicate records";
                            grvDuplicate.AutoGenerateColumns = false;
                            BindingSource _source = new BindingSource();
                            _source.DataSource = _custList;
                            grvDuplicate.DataSource = _source;
                            txtNIC.Text = "";
                            return;
                        }

                        MasterBusinessEntity custProf = GetbyNIC(txtNIC.Text.Trim().ToUpper());
                        if (custProf.Mbe_cd != null)
                        {
                            btnSave.Enabled = false;
                            btnUpdate.Enabled = true;
                            dobGeneration();
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
                            GroupBussinessEntity _grupProf = GetbyNICGrup(txtNIC.Text.Trim().ToUpper());
                            if (_grupProf.Mbg_cd != null)
                            {

                                LoadCustProfByGrup(_grupProf);
                                _isGroup = true;
                                _isExsit = false;
                                btnSave.Enabled = true;
                                btnUpdate.Enabled = false;
                                return;
                            }
                            else
                            {
                                _isGroup = false;
                            }

                            _isExsit = false;
                            btnSave.Enabled = true;
                            btnUpdate.Enabled = false;
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
                                cmbTitle.Text = "Ms.";
                            }
                            else
                            {
                                cmbSex.Text = "MALE";
                                cmbTitle.Text = "Mr.";
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

        private void txtBR_Leave(object sender, EventArgs e)
        {
            if (chkByRequest.Checked) return;
            try
            {
                if (!string.IsNullOrEmpty(txtBR.Text))
                {
                    //check multiple
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, "", "", txtBR.Text, "", "", 3);

                    if (_custList != null && _custList.Count > 1 && txtBR.Text.ToUpper() != "N/A")
                    {
                        pnlMain.Enabled = false;
                        pnlDuplicate.Visible = true;
                        lblErrMsg.Text = "Entered Business Registration number has multiple duplicate records";
                        grvDuplicate.AutoGenerateColumns = false;
                        BindingSource _source = new BindingSource();
                        _source.DataSource = _custList;
                        grvDuplicate.DataSource = _source;
                        txtBR.Text = "";
                        return;
                    }

                    MasterBusinessEntity custProf = GetbyBrNo(txtBR.Text.Trim().ToUpper());
                    if (custProf.Mbe_cd != null)
                    {
                        btnSave.Enabled = false;
                        btnUpdate.Enabled = true;
                        LoadCustProf(custProf);
                    }
                    else
                    {
                        if (_isExsit)
                        {
                            string BR = txtBR.Text.Trim().ToUpper();
                            MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            LoadCustProf(cust_null);
                            txtCusCode.Text = BR;
                        }
                        GroupBussinessEntity _grupProf = GetbyBrNoGrup(txtBR.Text.Trim().ToUpper());
                        if (_grupProf.Mbg_cd != null)
                        {
                            LoadCustProfByGrup(_grupProf);
                            _isGroup = true;
                            _isExsit = false;
                            return;
                        }
                        else
                        {
                            _isGroup = false;
                        }
                        _isExsit = false;
                        btnSave.Enabled = true;
                        btnUpdate.Enabled = false;
                    }
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

        private void txtPP_Leave(object sender, EventArgs e)
        {
            if (chkByRequest.Checked) return;
            try
            {
                if (!string.IsNullOrEmpty(txtPP.Text))
                {
                    //check multiple
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, "", "", "", txtPP.Text, "", 5);

                    if (_custList != null && _custList.Count > 1 && txtPP.Text.ToUpper() != "N/A")
                    {
                        pnlMain.Enabled = false;
                        pnlDuplicate.Visible = true;
                        lblErrMsg.Text = "Entered Passport number has multiple duplicate records";
                        grvDuplicate.AutoGenerateColumns = false;
                        BindingSource _source = new BindingSource();
                        _source.DataSource = _custList;
                        grvDuplicate.DataSource = _source;
                        txtPP.Text = "";
                        return;
                    }

                    MasterBusinessEntity custProf = GetbyPPno(txtPP.Text.Trim().ToUpper());
                    if (custProf.Mbe_cd != null)
                    {
                        btnSave.Enabled = false;
                        btnUpdate.Enabled = true;
                        LoadCustProf(custProf);
                    }
                    else
                    {
                        if (_isExsit)
                        {
                            string PP = txtPP.Text.Trim().ToUpper();
                            MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            LoadCustProf(cust_null);
                            txtPP.Text = PP;
                        }
                        GroupBussinessEntity _grupProf = GetbyPPnoGrup(txtPP.Text.Trim().ToUpper());
                        if (_grupProf.Mbg_cd != null)
                        {
                            LoadCustProfByGrup(_grupProf);
                            _isGroup = true;
                            _isExsit = false;
                            return;
                        }
                        else
                        {
                            _isGroup = false;
                        }
                        _isExsit = false;
                        btnSave.Enabled = true;
                        btnUpdate.Enabled = false;
                    }
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

        private void txtMob_Leave(object sender, EventArgs e)
        {
            if (chkByRequest.Checked) return;
            try
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

                    //check multiple

                    //List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, "", txtMob.Text, "", "", "", 2);
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtMob.Text, "C");

                    if (_custList != null && _custList.Count > 1 && txtMob.Text.ToUpper() != "N/A")
                    {
                        pnlMain.Enabled = false;
                        pnlDuplicate.Visible = true;
                        lblErrMsg.Text = "Entered Mobile number has multiple duplicate records";
                        grvDuplicate.AutoGenerateColumns = false;
                        BindingSource _source = new BindingSource();
                        _source.DataSource = _custList;
                        grvDuplicate.DataSource = _source;
                        txtMob.Text = "";
                        return;
                    }
                    else if (_custList != null && _custList.Count == 1 && txtMob.Text.ToUpper() != "N/A")
                    {
                        btnSave.Enabled = false;
                        btnUpdate.Enabled = true;
                        LoadCustProf(_custList[0]);
                    }
                    else
                    {
                        GroupBussinessEntity _grupProf = GetbyMobGrup(txtMob.Text.Trim().ToUpper());
                        if (_grupProf.Mbg_cd != null)
                        {
                            LoadCustProfByGrup(_grupProf);
                            _isGroup = true;
                            _isExsit = false;
                            return;
                        }
                        else
                        {
                            _isGroup = false;
                        }
                    }
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

        private void txtDL_Leave(object sender, EventArgs e)
        {
            if (chkByRequest.Checked) return;
            try
            {
                if (!string.IsNullOrEmpty(txtDL.Text))
                {
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, "", "", "", "", txtDL.Text, 4);

                    if (_custList != null && _custList.Count > 1 && txtDL.Text.ToUpper() != "N/A")
                    {
                        pnlMain.Enabled = false;
                        pnlDuplicate.Visible = true;
                        lblErrMsg.Text = "Entered Mobile number has multiple duplicate records";
                        grvDuplicate.AutoGenerateColumns = false;
                        BindingSource _source = new BindingSource();
                        _source.DataSource = _custList;
                        grvDuplicate.DataSource = _source;
                        txtDL.Text = "";
                        return;
                    }

                    MasterBusinessEntity custProf = GetbyDL(txtDL.Text.Trim().ToUpper());
                    if (custProf.Mbe_cd != null)
                    {
                        btnSave.Enabled = false;
                        btnUpdate.Enabled = true;
                        LoadCustProf(custProf);
                    }
                    else
                    {
                        string DL = txtDL.Text.Trim().ToUpper();
                        MasterBusinessEntity cust_null = new MasterBusinessEntity();
                        LoadCustProf(cust_null);
                        txtDL.Text = DL;
                        GroupBussinessEntity _grupProf = GetbyDLGrup(txtDL.Text.Trim().ToUpper());
                        if (_grupProf.Mbg_cd != null)
                        {
                            LoadCustProfByGrup(_grupProf);
                            _isGroup = true;
                            _isExsit = false;
                            return;
                        }
                        else
                        {
                            _isGroup = false;
                        }
                        btnSave.Enabled = true;
                        btnUpdate.Enabled = false;
                    }
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

        private void LoadPremenentTownDetails()
        {
            txtPerDistrict.Text = "";
            txtPerProvince.Text = "";
            txtPerPostal.Text = "";
            txtPerCountry.Text = "";

            if (!string.IsNullOrEmpty(txtPerTown.Text))
            {
                DataTable dt = new DataTable();

                dt = CHNLSVC.General.Get_DetBy_town(txtPerTown.Text.Trim().ToUpper());
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

        private void LoadPresentTownDetails()
        {
            txtPreDistrict.Text = "";
            txtPreProvince.Text = "";
            txtPrePostal.Text = "";
            txtPreCountry.Text = "";

            if (!string.IsNullOrEmpty(txtPreTown.Text))
            {
                DataTable dt = new DataTable();

                dt = CHNLSVC.General.Get_DetBy_town(txtPreTown.Text.Trim().ToUpper());
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

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (m.Msg == 0x0100 && (int)m.WParam == 13)
            {
                this.ProcessTabKey(true);
            }
            return base.ProcessKeyPreview(ref m);
        }

        private void txtPerTown_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPerTown.Text != "")
                {
                    LoadPremenentTownDetails();
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

        private void txtPreTown_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPreTown.Text != "")
                {
                    LoadPresentTownDetails();
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

        private void chkAllPC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllPC.Checked)
            {
                txtPC.Enabled = false;
                txtPC.Text = "All";
            }
            else
            {
                txtPC.Enabled = true;
                txtPC.Text = "";
            }
        }

        #region txtbox key down

        private void txtCusCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnCusCode_Click(null, null);
        }

        private void txtPerTown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPerTown_Click(null, null);
        }

        private void txtPreTown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPreTown_Click(null, null);
        }

        private void txtComCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnCompany_Click(null, null);
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPC_Click(null, null);
        }

        private void txtPB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPriceBook_Click(null, null);
        }

        private void txtPBLvl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPriceLevel_Click(null, null);
        }

        private void txtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnBrand_Click(null, null);
        }

        private void txtCate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnMainCategory_Click(null, null);
        }

        private void txtCate2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnCategory_Click(null, null);
        }

        private void txtCate3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSubCategory_Click(null, null);
        }

        private void txtItemCD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnItem_Click(null, null);
        }

        #endregion txtbox key down

        #region txtbox mouse double click

        private void txtCusCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCusCode_Click(null, null);
        }

        private void txtPerTown_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPerTown_Click(null, null);
        }

        private void txtPreTown_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPreTown_Click(null, null);
        }

        private void txtComCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCompany_Click(null, null);
        }

        private void txtPC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPC_Click(null, null);
        }

        private void txtPB_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPriceBook_Click(null, null);
        }

        private void txtPBLvl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPriceLevel_Click(null, null);
        }

        private void txtBrand_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnBrand_Click(null, null);
        }

        private void txtCate1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnMainCategory_Click(null, null);
        }

        private void txtCate2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCategory_Click(null, null);
        }

        private void txtCate3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSubCategory_Click(null, null);
        }

        private void txtItemCD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnItem_Click(null, null);
        }

        #endregion txtbox mouse double click

        private void grvCreditLimDet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DiscountList.RemoveAll((x) => x.Sgdd_seq == Convert.ToInt32(grvCreditLimDet.Rows[e.RowIndex].Cells[9].Value));

                        BindingSource source = new BindingSource();
                        source.DataSource = DiscountList;
                        grvCreditLimDet.DataSource = source;
                    }
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void chkSalesAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSalesAll.Checked)
            {
                btnSearchInvType.Enabled = false;
            }
            else
            {
                btnSearchInvType.Enabled = true;
            }

            /*try
            {
                if (chkSalesAll.Checked)
                {
                    DataTable _dt = CHNLSVC.General.GetSalesTypes("", null, null);
                    foreach (DataRow dr in _dt.Rows)
                    {
                        MasterInvoiceType _invoType = new MasterInvoiceType();
                        _invoType.Srtp_cd = dr["SRTP_CD"].ToString();
                        _invoType.Srtp_desc = dr["SRTP_DESC"].ToString();
                        SalesType.Add(_invoType);
                    }
                    grvSalesType.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = SalesType;
                    grvSalesType.DataSource = _source;
                }
                else
                {
                    SalesType = new List<MasterInvoiceType>();
                    grvSalesType.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = SalesType;
                    grvSalesType.DataSource = _source;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }*/
        }

        private void btnAddSt_Click(object sender, EventArgs e)
        {
            //Add by akila - 2017/10/26
            if (string.IsNullOrEmpty(txtInvoiceType.Text.Trim()) && !chkSalesAll.Checked)
            {
                MessageBox.Show("Please select the invoice type !", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                pnlDateRange.Visible = true;
            }
            //try
            //{
                

            //    //string code = (cmbSalesType.SelectedValue != null) ? cmbSalesType.SelectedValue.ToString() : "";
            //    //DataTable _dt = CHNLSVC.General.GetSalesTypes("", "SRTP_CD", code);
            //    //if (_dt.Rows.Count > 0)
            //    //{
            //    //    MasterInvoiceType _duplicate = SalesType.Find(x => x.Srtp_cd == _dt.Rows[0]["Srtp_cd"].ToString());
            //    //    if (_duplicate == null)
            //    //    {
            //    //        MasterInvoiceType _invType = new MasterInvoiceType();
            //    //        _invType.Srtp_cd = _dt.Rows[0]["Srtp_cd"].ToString();
            //    //        _invType.Srtp_desc = _dt.Rows[0]["SRTP_DESC"].ToString();
            //    //        SalesType.Add(_invType);

            //    //        grvSalesType.AutoGenerateColumns = false;
            //    //        BindingSource _source = new BindingSource();
            //    //        _source.DataSource = SalesType;
            //    //        grvSalesType.DataSource = _source;
            //    //    }
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}
        }

        private void grvSalesType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (SalesType != null && SalesType.Count > 0)
                        {
                            SalesType.RemoveAt(e.RowIndex);

                            grvSalesType.AutoGenerateColumns = false;
                            BindingSource _source = new BindingSource();
                            _source.DataSource = SalesType;
                            grvSalesType.DataSource = _source;
                        }
                        
                    }
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

        private void btnPopUpClose_Click(object sender, EventArgs e)
        {
            lblErrMsg.Text = "";
            grvDuplicate.AutoGenerateColumns = false;
            grvDuplicate.DataSource = null;
            pnlMain.Enabled = true; pnlDuplicate.Visible = false;
        }

        private DataTable _tbl = null;

        private void LoadRequest(int _status)
        {
            if (string.IsNullOrEmpty(txtRPc.Text))
            { MessageBox.Show("Please select the profit center", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            _tbl = CHNLSVC.Sales.GetCreditCustomerRequest(BaseCls.GlbUserComCode, txtRPc.Text.Trim(), _status);
            if (_tbl != null && _tbl.Rows.Count > 0)
            {
                _tbl.Rows.InsertAt(_tbl.NewRow(), 0);
            }
            cmbRequest.DataSource = _tbl;
            cmbRequest.DisplayMember = "mbq_reqno";
            cmbRequest.ValueMember = "mbq_reqno";
        }
        private void LoadLanguage()
        {
            _tbl = CHNLSVC.General.get_Language();
            cmbPrefLang.DataSource = _tbl;
            cmbPrefLang.DisplayMember = "mla_desc";
            cmbPrefLang.ValueMember = "mla_cd";
        }
        private void LoadEntityType()
        {
            _tbl = CHNLSVC.General.get_Buss_ent_type();
            cmbEntityType.DataSource = _tbl;
            cmbEntityType.DisplayMember = "rbe_desc";
            cmbEntityType.ValueMember = "rbe_cd";
        }

        private void chkByRequest_Leave(object sender, EventArgs e)
        {
            //Load Pending Requests
           // LoadRequest(0);
            _tbl = new DataTable();
            _tbl = CHNLSVC.Sales.GetCreditCustomerRequest(BaseCls.GlbUserComCode, txtRPc.Text.Trim(), 0);
            if (_tbl != null && _tbl.Rows.Count > 0)
            {
                _tbl.Rows.InsertAt(_tbl.NewRow(), 0);
            }
            cmbRequest.DataSource = _tbl;
            cmbRequest.DisplayMember = "mbq_reqno";
            cmbRequest.ValueMember = "mbq_reqno";
 
        }

        public void LoadCustReqProf(DataRow cust)
        {
            if (cust.Field<string>("mbq_cd") == null) txtCusCode.Text = string.Empty; else txtCusCode.Text = cust.Field<string>("mbq_cd").ToUpper();
            cmbType.Text = cust.Field<string>("Mbq_cate");
            if (cust.Field<string>("Mbq_nic") == null) txtNIC.Text = string.Empty; else txtNIC.Text = cust.Field<string>("Mbq_nic").ToUpper();
            if (cust.Field<string>("Mbq_pp_no") == null) txtPP.Text = string.Empty; else txtPP.Text = cust.Field<string>("Mbq_pp_no").ToUpper();
            if (cust.Field<string>("Mbq_br_no") == null) txtBR.Text = string.Empty; else txtBR.Text = cust.Field<string>("Mbq_br_no").ToUpper();
            if (cust.Field<string>("Mbq_dl_no") == null) txtDL.Text = string.Empty; else txtDL.Text = cust.Field<string>("Mbq_dl_no").ToUpper();
            if (cust.Field<string>("Mbq_mob") == null) txtMob.Text = string.Empty; else txtMob.Text = cust.Field<string>("Mbq_mob").ToUpper();

            //------------------------------------------
            cmbSex.Text = cust.Field<string>("Mbq_sex");
            if (cust.Field<string>("Mbq_name") == null) txtName.Text = string.Empty; else txtName.Text = cust.Field<string>("Mbq_name").ToUpper();

            if (cust.Field<DateTime>("Mbq_dob").Date > Convert.ToDateTime("01-01-1000").Date)
            {
                dtpDOB.Value = Convert.ToDateTime(cust.Field<DateTime>("Mbq_dob")).Date;
            }

            if (cust.Field<string>("Mbq_add1") == null) txtPerAdd1.Text = string.Empty; else txtPerAdd1.Text = cust.Field<string>("Mbq_add1").ToUpper();
            if (cust.Field<string>("Mbq_add2") == null) txtPerAdd2.Text = string.Empty; else txtPerAdd2.Text = cust.Field<string>("Mbq_add2").ToUpper();
            if (cust.Field<string>("Mbq_town_cd") == null) txtPerTown.Text = string.Empty; else txtPerTown.Text = cust.Field<string>("Mbq_town_cd").ToUpper();
            if (cust.Field<string>("Mbq_postal_cd") == null) txtPerPostal.Text = string.Empty; else txtPerPostal.Text = cust.Field<string>("Mbq_postal_cd").ToUpper();
            if (cust.Field<string>("Mbq_country_cd") == null) txtPerCountry.Text = string.Empty; else txtPerCountry.Text = cust.Field<string>("Mbq_country_cd").ToUpper();
            if (cust.Field<string>("Mbq_distric_cd") == null) txtPerDistrict.Text = string.Empty; else txtPerDistrict.Text = cust.Field<string>("Mbq_distric_cd").ToUpper();
            if (cust.Field<string>("Mbq_province_cd") == null) txtPerProvince.Text = string.Empty; else txtPerProvince.Text = cust.Field<string>("Mbq_province_cd").ToUpper();
            if (cust.Field<string>("Mbq_tel") == null) txtPerPhone.Text = string.Empty; else txtPerPhone.Text = cust.Field<string>("Mbq_tel").ToUpper();
            if (cust.Field<string>("Mbq_email") == null) txtPerEmail.Text = string.Empty; else txtPerEmail.Text = cust.Field<string>("Mbq_email").ToUpper();

            if (cust.Field<string>("Mbq_cr_add1") == null) txtPreAdd1.Text = string.Empty; else txtPreAdd1.Text = cust.Field<string>("Mbq_cr_add1").ToUpper();
            if (cust.Field<string>("Mbq_cr_add2") == null) txtPreAdd2.Text = string.Empty; else txtPreAdd2.Text = cust.Field<string>("Mbq_cr_add2").ToUpper();
            if (cust.Field<string>("Mbq_cr_town_cd") == null) txtPreTown.Text = string.Empty; else txtPreTown.Text = cust.Field<string>("Mbq_cr_town_cd").ToUpper();
            if (cust.Field<string>("Mbq_cr_postal_cd") == null) txtPrePostal.Text = string.Empty; else txtPrePostal.Text = cust.Field<string>("Mbq_cr_postal_cd").ToUpper();
            if (cust.Field<string>("Mbq_cr_country_cd") == null) txtPreCountry.Text = string.Empty; else txtPreCountry.Text = cust.Field<string>("Mbq_cr_country_cd").ToUpper();
            if (cust.Field<string>("Mbq_cr_distric_cd") == null) txtPreDistrict.Text = string.Empty; else txtPreDistrict.Text = cust.Field<string>("Mbq_cr_distric_cd").ToUpper();
            if (cust.Field<string>("Mbq_cr_province_cd") == null) txtPreProvince.Text = string.Empty; else txtPreProvince.Text = cust.Field<string>("Mbq_cr_province_cd").ToUpper();
            if (cust.Field<string>("Mbq_cr_tel") == null) txtPrePhone.Text = string.Empty; else txtPrePhone.Text = cust.Field<string>("Mbq_cr_tel");

            if (cust.Field<string>("Mbq_wr_add1") == null) txtWorkAdd1.Text = string.Empty; else txtWorkAdd1.Text = cust.Field<string>("Mbq_wr_add1").ToUpper();
            if (cust.Field<string>("Mbq_wr_add2") == null) txtWorkAdd2.Text = string.Empty; else txtWorkAdd2.Text = cust.Field<string>("Mbq_wr_add2").ToUpper();
            if (cust.Field<string>("Mbq_wr_com_name") == null) txtWorkName.Text = string.Empty; else txtWorkName.Text = cust.Field<string>("Mbq_wr_com_name").ToUpper();
            if (cust.Field<string>("Mbq_wr_dept") == null) txtWorkDept.Text = string.Empty; else txtWorkDept.Text = cust.Field<string>("Mbq_wr_dept").ToUpper();
            if (cust.Field<string>("Mbq_wr_designation") == null) txtWorkDesig.Text = string.Empty; else txtWorkDesig.Text = cust.Field<string>("Mbq_wr_designation").ToUpper();
            if (cust.Field<string>("Mbq_wr_email") == null) txtWorkEmail.Text = string.Empty; else txtWorkEmail.Text = cust.Field<string>("Mbq_wr_email");
            if (cust.Field<string>("Mbq_wr_fax") == null) txtWorkFax.Text = string.Empty; else txtWorkFax.Text = cust.Field<string>("Mbq_wr_fax");
            if (cust.Field<string>("Mbq_wr_tel") == null) txtWorkPhone.Text = string.Empty; else txtWorkPhone.Text = cust.Field<string>("Mbq_wr_tel");
            if (cust.Field<Int16>("Mbq_is_tax") == 0) chkVAT.Checked = false; else chkVAT.Checked = Convert.ToBoolean(cust.Field<Int16>("Mbq_is_tax"));
            if (cust.Field<Int16>("Mbq_tax_ex") == 0) chkVatEx.Checked = false; else chkVatEx.Checked = Convert.ToBoolean(cust.Field<Int16>("Mbq_tax_ex"));
            if (cust.Field<Int16>("Mbq_is_svat") == 0) chkSVAT.Checked = false; else chkSVAT.Checked = Convert.ToBoolean(cust.Field<Int16>("Mbq_is_svat"));
            if (cust.Field<string>("Mbq_tax_no") == null) txtVatreg.Text = string.Empty; else txtVatreg.Text = cust.Field<string>("Mbq_tax_no");
            if (cust.Field<string>("Mbq_svat_no") == null) txtSVATReg.Text = string.Empty; else txtSVATReg.Text = cust.Field<string>("Mbq_svat_no");
            //chkVAT.Checked = cust.Field<bool>("Mbq_is_tax");
            //chkVatEx.Checked = cust.Field<bool>("Mbq_tax_ex");
            //chkSVAT.Checked = cust.Field<bool>("Mbq_is_svat");
        }

        private Int16 _recallstatus = -1;

        private void LoadRequestCustomerCommonCriteriaFromOriginal()
        {
            List<MasterBusinessEntity> _custListNic = null;
            List<MasterBusinessEntity> _custListBr = null;
            List<MasterBusinessEntity> _custListPp = null;
            List<MasterBusinessEntity> _custListMob = null;
            List<MasterBusinessEntity> _custListDl = null;

            if (!string.IsNullOrEmpty(txtNIC.Text) && (txtNIC.Text.ToUpper() != "N/A"))
                _custListNic = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, txtNIC.Text, "", "", "", "", 1);
            if (!string.IsNullOrEmpty(txtBR.Text) && (txtBR.Text.ToUpper() != "N/A"))
                _custListBr = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, "", "", txtBR.Text, "", "", 3);
            if (!string.IsNullOrEmpty(txtPP.Text) && (txtPP.Text.ToUpper() != "N/A"))
                _custListPp = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, "", "", "", txtPP.Text, "", 5);
            if (!string.IsNullOrEmpty(txtMob.Text) && (txtMob.Text.ToUpper() != "N/A"))
                _custListMob = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, "", txtMob.Text, "", "", "", 2);
            if (!string.IsNullOrEmpty(txtDL.Text) && (txtDL.Text.ToUpper() != "N/A"))
                _custListDl = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, "", "", "", "", txtDL.Text, 4);

            List<MasterBusinessEntity> _custList = new List<MasterBusinessEntity>();
            if (_custListNic != null && _custListNic.Count > 0)
                _custList.AddRange(_custListNic);
            if (_custListBr != null && _custListBr.Count > 0)
                _custList.AddRange(_custListBr);
            if (_custListPp != null && _custListPp.Count > 0)
                _custList.AddRange(_custListPp);
            if (_custListMob != null && _custListMob.Count > 0)
                _custList.AddRange(_custListMob);
            if (_custListDl != null && _custListDl.Count > 0)
                _custList.AddRange(_custListDl);

            if (_custList != null && _custList.Count > 1)
            {
                pnlMain.Enabled = false;
                pnlDuplicate.Visible = true;
                lblErrMsg.Text = "There are some registered customers available for the request customer criteria. If you need, you can match one from the below list and update.";
                grvDuplicate.AutoGenerateColumns = false;
                BindingSource _source = new BindingSource();
                _source.DataSource = _custList;
                grvDuplicate.DataSource = _source;
                return;
            }
        }

        private void cmbRequest_Leave(object sender, EventArgs e)
        {       
            _recallstatus = -1;
            if (string.IsNullOrEmpty(Convert.ToString(cmbRequest.SelectedValue))) { btnSave.Enabled = true; Clear_Data(); return; }
            if (_tbl != null && _tbl.Rows.Count > 0)
            {
                var _existRec = _tbl.AsEnumerable().Where(x => x.Field<string>("mbq_reqno") == Convert.ToString(cmbRequest.SelectedValue)).ToList();
                if (_existRec != null && _existRec.Count > 0)
                {
                    txtCusCode.Clear();
                    _recallstatus = _existRec[0].Field<Int16>("mbq_reqstus");
                    if (_recallstatus == 1) btnSave.Enabled = false; else btnSave.Enabled = true;
                    DataRow _r = _existRec.CopyToDataTable().Rows[0];
                    LoadCustReqProf(_r);
                    LoadRequestCustomerCommonCriteriaFromOriginal();
                }
            }
        }

        private void chkByRequest_CheckedChanged(object sender, EventArgs e)
        {
            if (chkByRequest.Checked) { txtRPc.ReadOnly = false; btnSearchPc.Enabled = true; cmbRequest.Enabled = true; txtCusCode.ReadOnly = true; btnCusCode.Enabled = false; } else { txtRPc.ReadOnly = true; btnSearchPc.Enabled = false; cmbRequest.Enabled = false; txtCusCode.ReadOnly = false; btnCusCode.Enabled = true; }
            btnSave.Enabled = true; btnUpdate.Enabled = false;
        }

        private void grvDuplicate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (chkByRequest.Checked)
            {
                if (e.ColumnIndex == 0)
                {
                    string _customercode = Convert.ToString(grvDuplicate.Rows[e.RowIndex].Cells["Column14"].Value);
                    txtCusCode.Text = _customercode;
                    pnlMain.Enabled = true; pnlDuplicate.Visible = false;
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;
                }
            }
        }

        private void btnSearchPc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRPc;
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

        private void txtRPc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearchPc_Click(null, null);
        }

        private void txtRPc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearchPc_Click(null, null);
        }

        private void txtRPc_Leave(object sender, EventArgs e)
        {
            LoadRequest(0);
        }

        private void groupBox10_Enter(object sender, EventArgs e)
        {

        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void txtCusCode_TextChanged(object sender, EventArgs e)
        {

        }



        private void txtRPc_TextChanged(object sender, EventArgs e)
        {

        }



        private void txtMob_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDL_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {

            cmbType.Focus();
        }

        private void txtNIC_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBR_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnHierachySearch_Click(object sender, EventArgs e)
        {
            try
            {
                Base _basePage = new Base();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                DataTable dt = new DataTable();
                DataTable _result = new DataTable();

                if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "LOC")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    _result = _basePage.CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                }


                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtHierchCode;
                _CommonSearch.ShowDialog();
                txtHierchCode.Focus();
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

        private void txtHierchCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnHierachySearch_Click(null, null);
        }

        private void txtHierchCode_DoubleClick(object sender, EventArgs e)
        {
            btnHierachySearch_Click(null, null);
        }

        private void btn_srch_prio_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServicePriority);
            DataTable _result = CHNLSVC.CommonSearch.SearchScvPriority(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPriority;
            _CommonSearch.ShowDialog();
            txtPriority.Focus();
        }

        private void txtPriority_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_prio_Click(null, null);
        }

        private void txtPriority_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_prio_Click(null, null);
        }

        private void DropDownListPartyTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtHierchCode.Text = "";
        }

        private void grvParty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _PriorityLvlList.RemoveAt(e.RowIndex);
                        grvParty.AutoGenerateColumns = false;
                        BindingSource _source = new BindingSource();
                        _source.DataSource = _PriorityLvlList;
                        grvParty.DataSource = _source;
                    }
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

        private void btnAddPartys_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPriority.Text))
            {
                MessageBox.Show("Select the priority level !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtHierchCode.Text))
            {
                MessageBox.Show("Select the location/Sub channel !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Base _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = new DataTable();
            if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                if (txtHierchCode.Text != "")
                {
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                }
                if (_result == null || _result.Rows.Count <= 0)
                {
                    MessageBox.Show("Invalid search term, no result found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                List<MasterLocation> loc_list = CHNLSVC.Inventory.getAllLoc_WithSubLoc(BaseCls.GlbUserComCode, txtHierchCode.Text);
                if (loc_list == null)
                {
                    MessageBox.Show("Invalid Location Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            foreach (DataGridViewRow row in grvParty.Rows)
            {
                if (row.Cells[1].Value.ToString().Equals(DropDownListPartyTypes.SelectedValue.ToString()) && row.Cells[2].Value.ToString().Equals(txtHierchCode.Text) && row.Cells[3].Value.ToString().Equals(txtPriority.Text))
                    return;
            }

            MST_BUSPRIT_LVL _PrtLevel = new MST_BUSPRIT_LVL();
            _PrtLevel.Mbl_act = Convert.ToInt32(chkAct.Checked);
            _PrtLevel.Mbl_cd = txtCusCode.Text;
            _PrtLevel.Mbl_com = BaseCls.GlbUserComCode;
            _PrtLevel.Mbl_cre_by = BaseCls.GlbUserID;
            _PrtLevel.Mbl_mod_by = BaseCls.GlbUserID;
            _PrtLevel.Mbl_prit_cd = txtPriority.Text;
            _PrtLevel.Mbl_pty_cd = txtHierchCode.Text;
            _PrtLevel.Mbl_pty_tp = DropDownListPartyTypes.SelectedValue.ToString();
            _PriorityLvlList.Add(_PrtLevel);

            BindingSource _source = new BindingSource();
            _source.DataSource = _PriorityLvlList;
            grvParty.DataSource = _source;

            txtPriority.Text = "";
            txtHierchCode.Text = "";

        }

        private void btnPrioClose_Click(object sender, EventArgs e)
        {
            pnlPriority.Visible = false;
        }

        private void btnPriority_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10098))
            {
                MessageBox.Show("Sorry, You have no permission to set the priority level!\n( Advice: Required permission code :10098 )");
                return;
            }
            pnlPriority.Visible = true;
        }

        private void btnUpdProty_Click(object sender, EventArgs e)
        {
            if (_PriorityLvlList.Count == 0)
            {
                MessageBox.Show("Select the priority level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            Int32 _eff = CHNLSVC.Sales.SaveCustomerPriorityLevel(_PriorityLvlList,txtCusCode.Text);
            MessageBox.Show("Successfully completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void txtPerTown_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearchInvType_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchSalesType);
                DataTable _result = CHNLSVC.General.SearchSalesTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvoiceType;
                _CommonSearch.ShowDialog();
                txtInvoiceType.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("An error occurred while searching invoice types. " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtInvoiceType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchInvType_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnAddSt.Focus();
                btnAddSt_Click(null, null);
            }

        }

        private void txtInvoiceType_DoubleClick(object sender, EventArgs e)
        {
            btnSearchInvType_Click(null, null);
        }

        private void AddInvoiceType()
        {
            try
            {

                //string code = (cmbSalesType.SelectedValue != null) ? cmbSalesType.SelectedValue.ToString() : "";
                string code = txtInvoiceType.Text;
                DataTable _dt = new DataTable();

                if (chkSalesAll.Checked)
                {
                    _dt = CHNLSVC.General.GetSalesTypes("", "SRTP_CD", null);
                }
                else
                {
                    _dt = CHNLSVC.General.GetSalesTypes("", "SRTP_CD", code);
                }
                
                if (_dt.Rows.Count > 0)
                {
                    List<DataRow> _tmpDataRows = new List<DataRow>();
                    if (chkSalesAll.Checked)
                    {
                        _tmpDataRows = _dt.Rows.Cast<DataRow>().ToList();
                    }
                    else
                    {
                        _tmpDataRows = _dt.Rows.Cast<DataRow>().Where(x => x.Field<string>("SRTP_CD").ToString().Equals(code)).ToList();
                    }
                   

                    if (_tmpDataRows != null && _tmpDataRows.Count > 0)
                    {
                        foreach (DataRow _row in _tmpDataRows)
                        {
                            MasterInvoiceType _duplicate = SalesType.Find(x => x.Srtp_cd == _row["Srtp_cd"].ToString());
                            if (_duplicate == null)
                            {
                                MasterInvoiceType _invType = new MasterInvoiceType();
                                _invType.Srtp_cd = _row["Srtp_cd"].ToString();
                                _invType.Srtp_desc = _row["SRTP_DESC"].ToString();
                                _invType.Srtp_valid_from_dt = dtpTpValidFrom.Value.Date;
                                _invType.Srtp_valid_to_dt = dtpTpValidTo.Value.Date;
                                SalesType.Add(_invType);

                                grvSalesType.AutoGenerateColumns = false;
                                BindingSource _source = new BindingSource();
                                _source.DataSource = SalesType;
                                grvSalesType.DataSource = _source;
                                txtInvoiceType.Text = null;
                            }
                        }
                    }                                        
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

        private void btnAddDate_Click(object sender, EventArgs e)
        {
            if (dtpTpValidFrom.Value.Date > dtpTpValidTo.Value.Date)
            {
                MessageBox.Show("From date is invalid !", "Valid date range", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (dtpTpValidTo.Value.Date < dtpTpValidFrom.Value.Date)
            {
                MessageBox.Show("To date is invalid !", "Valid date range", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                pnlDateRange.Visible = false;                
                AddInvoiceType();
                chkSalesAll.Checked = false;
            }
            
        }

        private CustomerAccountRef SetCustomerCreditAccount()
        {
            CustomerAccountRef _returnCsutAccount = new CustomerAccountRef();
            try
            {
                decimal _creditLimit = 0;
                decimal.TryParse(txtCredLimit.Text, out _creditLimit);

                CustomerAccountRef _custAccount = CHNLSVC.Sales.GetCustomerAccount(BaseCls.GlbUserComCode, txtCusCode.Text.Trim());
                if (_custAccount != null && !string.IsNullOrEmpty(_custAccount.Saca_cust_cd))
                {
                    _returnCsutAccount = _custAccount;
                    _returnCsutAccount.Saca_crdt_lmt = _creditLimit;
                }
                else
                {
                    _returnCsutAccount.Saca_com_cd = BaseCls.GlbUserComCode;
                    _returnCsutAccount.Saca_crdt_lmt = _creditLimit;
                    _returnCsutAccount.Saca_cre_by = BaseCls.GlbUserID;
                    _returnCsutAccount.Saca_cre_when = DateTime.Today.Date;
                    _returnCsutAccount.Saca_cust_cd = txtCusCode.Text.Trim();
                    _returnCsutAccount.Saca_mod_by = BaseCls.GlbUserID;
                    _returnCsutAccount.Saca_mod_when = DateTime.Today.Date;
                    _returnCsutAccount.Saca_session_id = BaseCls.GlbUserSessionID;
                }                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                _returnCsutAccount = null;
            }

            return _returnCsutAccount;
        }

        private void grvCustSegmentation_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}