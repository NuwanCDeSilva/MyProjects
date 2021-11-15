using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;

namespace FF.WindowsERPClient.General
{
    public partial class CreditCustomerRequest : FF.WindowsERPClient.Base
    {
        private Boolean _isExsit = false;
        public CreditCustomerRequest()
        {
            InitializeComponent();
            this.Cursor = Cursors.WaitCursor;
            try
            { LoadRequest(-1); RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.ARQT034, CHNLSVC.Security.GetServerDateTime().Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC); }
            catch (Exception EX) { MessageBox.Show(EX.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            finally { this.Cursor = Cursors.Default; }
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
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator);
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
                default:
                    break;
            }

            return paramsText.ToString();
        }

        public MasterBusinessEntity GetbyNIC(string nic)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, nic, null, null, null, BaseCls.GlbUserComCode);
        }
        public void LoadCustProf(MasterBusinessEntity cust)
        {
            cmbType.Text = cust.Mbe_cate;
            txtNIC.Text = cust.Mbe_nic.ToUpper();
            txtPP.Text = cust.Mbe_pp_no.ToUpper();
            txtBR.Text = cust.Mbe_br_no.ToUpper();
            txtDL.Text = cust.Mbe_dl_no.ToUpper();
            txtMob.Text = cust.Mbe_mob.ToUpper();
            //------------------------------------------
            cmbSex.Text = cust.Mbe_sex;
            txtName.Text = cust.Mbe_name;

            if (cust.Mbe_dob.Date > Convert.ToDateTime("01-01-1000").Date)
            {
                dtpDOB.Value = Convert.ToDateTime(cust.Mbe_dob).Date;
            }

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

            txtCusCode.ReadOnly = true;
        }
        private void txtNIC_Leave(object sender, EventArgs e)
        {
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
                        List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, txtNIC.Text, "", "", "", "", 1);

                        if (_custList != null && _custList.Count > 1 && txtNIC.Text.ToUpper() != "N/A")
                        {
                            grvDuplicate.AutoGenerateColumns = false;
                            BindingSource _source = new BindingSource();
                            _source.DataSource = _custList;
                            grvDuplicate.DataSource = _source;
                            return;
                        }


                        MasterBusinessEntity custProf = GetbyNIC(txtNIC.Text.Trim().ToUpper());
                        if (custProf.Mbe_cd != null)
                        {
                            btnSave.Enabled = false;
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
                            btnSave.Enabled = true;
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
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }
        public MasterBusinessEntity GetbyBrNo(string brNo)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, null, null, null, brNo, BaseCls.GlbUserComCode);
        }
        private void txtBR_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBR.Text))
                {
                    //check multiple
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, "", "", txtBR.Text, "", "", 3);

                    if (_custList != null && _custList.Count > 1 && txtBR.Text.ToUpper() != "N/A")
                    {
                        grvDuplicate.AutoGenerateColumns = false;
                        BindingSource _source = new BindingSource();
                        _source.DataSource = _custList;
                        grvDuplicate.DataSource = _source;
                        return;
                    }



                    MasterBusinessEntity custProf = GetbyBrNo(txtBR.Text.Trim().ToUpper());
                    if (custProf.Mbe_cd != null)
                    {

                        btnSave.Enabled = false;
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
                        _isExsit = false;
                        btnSave.Enabled = true;
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
        public MasterBusinessEntity GetbyPPno(string ppno)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, null, null, ppno, null, BaseCls.GlbUserComCode);
        }
        private void txtPP_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPP.Text))
                {
                    //check multiple
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, "", "", "", txtPP.Text, "", 5);

                    if (_custList != null && _custList.Count > 1 && txtPP.Text.ToUpper() != "N/A")
                    {
                        grvDuplicate.AutoGenerateColumns = false;
                        BindingSource _source = new BindingSource();
                        _source.DataSource = _custList;
                        grvDuplicate.DataSource = _source;
                        return;
                    }


                    MasterBusinessEntity custProf = GetbyPPno(txtPP.Text.Trim().ToUpper());
                    if (custProf.Mbe_cd != null)
                    {

                        btnSave.Enabled = false;
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
                        _isExsit = false;
                        btnSave.Enabled = true;
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

                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, "", txtMob.Text, "", "", "", 2);

                    if (_custList != null && _custList.Count > 1 && txtMob.Text.ToUpper() != "N/A")
                    {
                        grvDuplicate.AutoGenerateColumns = false;
                        BindingSource _source = new BindingSource();
                        _source.DataSource = _custList;
                        grvDuplicate.DataSource = _source;
                        return;
                    }
                    else if (_custList != null && _custList.Count == 1 && txtMob.Text.ToUpper() != "N/A")
                    {
                        btnSave.Enabled = false;
                        LoadCustProf(_custList[0]);

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
        public MasterBusinessEntity GetbyDL(string dl)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(null, null, dl, null, null, BaseCls.GlbUserComCode);
        }
        private void txtDL_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDL.Text))
                {
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, "", "", "", "", txtDL.Text, 4);

                    if (_custList != null && _custList.Count > 1 && txtDL.Text.ToUpper() != "N/A")
                    {
                        grvDuplicate.AutoGenerateColumns = false;
                        BindingSource _source = new BindingSource();
                        _source.DataSource = _custList;
                        grvDuplicate.DataSource = _source;
                        return;
                    }

                    MasterBusinessEntity custProf = GetbyDL(txtDL.Text.Trim().ToUpper());
                    if (custProf.Mbe_cd != null)
                    {
                        btnSave.Enabled = false;
                        LoadCustProf(custProf);
                    }
                    else
                    {
                        string DL = txtDL.Text.Trim().ToUpper();
                        MasterBusinessEntity cust_null = new MasterBusinessEntity();
                        LoadCustProf(cust_null);
                        txtDL.Text = DL;
                        btnSave.Enabled = true;
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

        private void txtPerTown_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPerTown_Click(null, null);
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

        private void txtPerTown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPerTown_Click(null, null);
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

        private void txtPreTown_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPreTown_Click(null, null);
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

        private void txtPreTown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPreTown_Click(null, null);
        }
        DataTable _tbl = null;
        private void LoadRequest(int _status)
        {
            _tbl = CHNLSVC.Sales.GetCreditCustomerRequest(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _status);
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
        Int16 _recallstatus = -1;
        private void cmbRequest_Leave(object sender, EventArgs e)
        {
            try
            {
                _recallstatus = -1;
                lblstatus.Text = string.Empty;
                if (string.IsNullOrEmpty(Convert.ToString(cmbRequest.SelectedValue))) { btnSave.Enabled = true; Clear_Data(); return; }
                if (_tbl != null && _tbl.Rows.Count > 0)
                {
                    var _existRec = _tbl.AsEnumerable().Where(x => x.Field<string>("mbq_reqno") == Convert.ToString(cmbRequest.SelectedValue)).ToList();
                    if (_existRec != null && _existRec.Count > 0)
                    {
                        _recallstatus = _existRec[0].Field<Int16>("mbq_reqstus");
                        if (_recallstatus == 1) btnSave.Enabled = false; else btnSave.Enabled = true;
                        if (_recallstatus == 1) lblstatus.Text = "Approved"; else lblstatus.Text = "Pending";
                        DataRow _r = _existRec.CopyToDataTable().Rows[0];
                        LoadCustReqProf(_r);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally { CHNLSVC.CloseAllChannels(); }
        }

        private bool ValidateSave()
        {
            if (txtNIC.Text == "" && txtBR.Text == "" && txtPP.Text == "" && txtDL.Text == "" && txtMob.Text == "")
            {
                MessageBox.Show("One of required information not entered.\nEG. NIC Number, BR Number, PP Number, Mobile Number", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNIC.Focus();
                return false;
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

            return true;
        }
        MasterBusinessEntity _custProfile = null;
        private void Clear_Data()
        {
            _custProfile = new MasterBusinessEntity(); txtCusCode.Text = "";
            txtCusCode.Enabled = true; cmbType.Text = "INDIVIDUAL"; cmbSex.Text = "";
            cmbTitle.Text = ""; txtNIC.Text = ""; txtBR.Text = ""; txtPP.Text = ""; txtDL.Text = "";
            txtMob.Text = ""; txtName.Text = ""; txtPerAdd1.Text = ""; txtPerAdd2.Text = "";
            txtPerTown.Text = ""; txtPerDistrict.Text = ""; txtPerPostal.Text = "";
            txtPerProvince.Text = ""; txtPerCountry.Text = ""; txtPerPhone.Text = "";
            txtPerEmail.Text = ""; txtPreAdd1.Text = ""; txtPreAdd2.Text = "";
            txtPreTown.Text = ""; txtPreDistrict.Text = ""; txtPreProvince.Text = "";
            txtPrePostal.Text = ""; txtPreCountry.Text = ""; txtPrePhone.Text = "";
            txtWorkName.Text = ""; txtWorkAdd1.Text = ""; txtWorkAdd2.Text = "";
            txtWorkDept.Text = ""; txtWorkDesig.Text = ""; txtWorkPhone.Text = "";
            txtWorkFax.Text = ""; txtWorkEmail.Text = ""; dtpDOB.Value = Convert.ToDateTime(DateTime.Today).Date;
            chkVAT.Checked = false; chkSVAT.Checked = false; chkVatEx.Checked = false; txtVatreg.Text = "";
            txtSVATReg.Text = ""; txtVatreg.Enabled = false; txtSVATReg.Enabled = false;
            tbAdd.SelectedTab = tabPage1; btnSave.Enabled = true; cmbType.Focus(); cmbType.SelectedIndex = 0;
            cmbTitle.SelectedIndex = 0; cmbSex.SelectedIndex = 0; grvDuplicate.DataSource = null; _recallstatus = -1;
            lblstatus.Text = string.Empty;
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
            _custProfile.Mbe_tel = txtPerPhone.Text.ToUpper();
            _custProfile.Mbe_add1 = txtPerAdd1.Text.Trim().ToUpper();
            _custProfile.Mbe_add2 = txtPerAdd2.Text.Trim().ToUpper();
            _custProfile.Mbe_town_cd = txtPerTown.Text.ToUpper();
            _custProfile.Mbe_country_cd = txtPerCountry.Text;
            _custProfile.Mbe_distric_cd = txtPerDistrict.Text;
            _custProfile.Mbe_province_cd = txtPerProvince.Text;
            _isSMS = false;

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
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //save customer details
                string _cusCode = "";
                Int32 _effect = 0;

                //NEED TO VALIDATE SEGMANTATION MANDATORY VALUES
                if (ValidateSave())
                {
                    try
                    {
                        int _status = 0;

     
                        //Save main details
                        Collect_Cust();

                        DateTime _date = CHNLSVC.Security.GetServerDateTime();
                        #region fill RequestApprovalHeader
                        RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                        ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                        ra_hdr.Grah_app_dt = Convert.ToDateTime(DateTime.Now.Date);
                        ra_hdr.Grah_app_lvl = BaseCls.GlbReqUserPermissionLevel;
                        ra_hdr.Grah_app_stus = "P";
                        ra_hdr.Grah_app_tp = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT034.ToString();
                        ra_hdr.Grah_com = BaseCls.GlbUserComCode;
                        ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
                        ra_hdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now.Date);
                        ra_hdr.Grah_fuc_cd = string.Empty;
                        ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;
                        ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                        ra_hdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now.Date);
                        ra_hdr.Grah_oth_loc = "0";
                        ra_hdr.Grah_ref = string.Empty;
                        ra_hdr.Grah_remaks = "Credit Customer Request";
                        #endregion
                        if (_recallstatus != -1)
                        {
                            ra_hdr.Grah_fuc_cd = Convert.ToString(cmbRequest.SelectedValue);
                            ra_hdr.Grah_ref = Convert.ToString(cmbRequest.SelectedValue);
                        }


                        _effect = CHNLSVC.Sales.SaveCreditCustomerRequest(_custProfile, BaseCls.GlbUserDefProf, _status, out _cusCode, ra_hdr);

                        if (_effect == 1)
                        {
                            MessageBox.Show("New customer created. Customer Code : " + _cusCode, "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        Clear_Data();
                        LoadRequest(-1);
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult _RES = MessageBox.Show("Do you need to exit from the credit customer request?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (_RES == System.Windows.Forms.DialogResult.Yes)
                this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear_Data();
            LoadRequest(-1);
        }

        private void chkVAT_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVAT.Checked == true)
            {
                txtVatreg.Text = "";
                txtVatreg.Enabled = true;
            }
            else
            {
                txtVatreg.Text = "";
                txtVatreg.Enabled = false;
            }
        }

        private void chkSVAT_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSVAT.Checked == true)
            {
                txtSVATReg.Text = "";
                txtSVATReg.Enabled = true;
            }
            else
            {
                txtSVATReg.Text = "";
                txtSVATReg.Enabled = false;
            }
        }

    }
}
