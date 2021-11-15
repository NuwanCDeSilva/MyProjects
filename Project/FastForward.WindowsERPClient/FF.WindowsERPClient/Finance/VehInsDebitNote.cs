using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;


namespace FF.WindowsERPClient.Finance
{
    public partial class VehInsDebitNote : Base
    {
        string invtype = "";
        DataTable param = new DataTable();


        public VehInsDebitNote()
        {
            InitializeComponent();
            TextBoxLocation.Text = BaseCls.GlbUserDefProf;
        }


        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.VehicalInsuranceRegNo:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ManIssRec:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + TextBoxLocation.Text + seperator + lblAccountNo.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + TextBoxLocation.Text + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.VehicleInsuranceRef:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
            TextBoxLocation.Text = "";
            txtAccountNo.Text = "";
        }

        private void clear()
        {

            lblAccountNo.Text = "";

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            VehicleInsuarance _tempIns = new VehicleInsuarance();
            MasterItem _itemList = new MasterItem();
            _itemList = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);

            _tempIns.Svit_seq = 1;
            _tempIns.Svit_ref_no = "na";
            _tempIns.Svit_com = BaseCls.GlbUserComCode;
            _tempIns.Svit_pc = BaseCls.GlbUserDefProf;
            _tempIns.Svit_dt = Convert.ToDateTime(dtDate.Value).Date;
            _tempIns.Svit_sales_tp = txtSaleTp.Text;
            _tempIns.Svit_ins_com = txtInsCompany.Text.Trim();
            _tempIns.Svit_ins_polc = txtInsPolicy.Text.Trim();
            _tempIns.Svit_ins_val = Convert.ToDecimal(txtTotal.Text);
            _tempIns.Svit_cust_cd = txtCusCode.Text.Trim();
            _tempIns.Svit_cust_title = "Mr.";
            _tempIns.Svit_last_name = txtLastName.Text.Trim();
            _tempIns.Svit_full_name = txtFullName.Text.Trim();
            _tempIns.Svit_initial = "";
            _tempIns.Svit_add01 = txtAdd1.Text.Trim();
            _tempIns.Svit_add02 = txtAdd2.Text.Trim();
            _tempIns.Svit_city = "";
            _tempIns.Svit_district = cmbDistrict.Text;
            _tempIns.Svit_province = txtProvince.Text.Trim();
            //     _tempIns.Svit_contact = txtMobile.Text.Trim();
            _tempIns.Svit_model = txtModal.Text;
            _tempIns.Svit_brd = _itemList.Mi_brand;
            _tempIns.Svit_chassis = txtChassis.Text.Trim();
            _tempIns.Svit_engine = txtEngine.Text.Trim();
            _tempIns.Svit_capacity = "HS";
            _tempIns.Svit_cre_by = BaseCls.GlbUserID;
            _tempIns.Svit_cre_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_mod_by = BaseCls.GlbUserID;
            _tempIns.Svit_mod_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_cvnt_issue = 0;
            _tempIns.Svit_cvnt_no = "";
            _tempIns.Svit_cvnt_days = 0;
            _tempIns.Svit_cvnt_from_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_cvnt_to_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_cvnt_by = "";
            _tempIns.Svit_cvnt_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_ext_issue = false;
            _tempIns.Svit_ext_no = "";
            _tempIns.Svit_ext_days = 0;
            _tempIns.Svit_ext_from_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_ext_to_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_ext_by = "";
            _tempIns.Svit_ext_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_veh_reg_no = lblVeh.Text;
            _tempIns.Svit_reg_by = "";
            _tempIns.Svit_reg_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_polc_stus = true;
            _tempIns.Svit_polc_no = txtPolicyNo.Text;
            _tempIns.Svit_eff_dt = Convert.ToDateTime(dateTimePickerEffectiveDate.Value);
            _tempIns.Svit_expi_dt = Convert.ToDateTime(dateTimePickerExpireNo.Value);
            _tempIns.Svit_net_prem = Convert.ToDecimal(txtNetPremium.Text);
            _tempIns.Svit_srcc_prem = Convert.ToDecimal(txtSRCCPre.Text);
            _tempIns.Svit_oth_val = Convert.ToDecimal(txtOther.Text);
            _tempIns.Svit_tot_val = Convert.ToDecimal(txtTotal.Text);
            _tempIns.Svit_polc_by = "";
            _tempIns.Svit_polc_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_dbt_no = txtDebitNoteNo.Text;
            _tempIns.Svit_dbt_set_stus = false;
            _tempIns.Svit_dbt_by = BaseCls.GlbUserID;
            _tempIns.Svit_dbt_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_veg_ref = "";
            _tempIns.Svit_itm_cd = txtItem.Text.Trim();
            _tempIns.Svit_rec_tp = "VHRNW";
            _tempIns.Svit_inv_no = txtInv.Text;
            _tempIns.Svit_file_no = txtFileNo.Text;

            Int32 _eff = CHNLSVC.Sales.SaveVehInsuarance(_tempIns);
            MessageBox.Show("Record updated successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // clear();
        }





        private void btnProfitCenter_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxLocation;
                _CommonSearch.ShowDialog();
                TextBoxLocation.Select();

                clear();
                txtAccountNo.Text = "";

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

        private void TextBoxLocation_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxLocation.Text))
            {
                Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, TextBoxLocation.Text);
                if (_IsValid == false)
                {
                    MessageBox.Show("Invalid Profit Center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    TextBoxLocation.Focus();
                    return;
                }
            }
            clear();
            txtAccountNo.Text = "";
        }

        private void TextBoxLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnProfitCenter_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtAccountNo.Focus();
        }

        private void ImgBtnAccountNo_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
            DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAccountNo;
            _CommonSearch.ShowDialog();
            txtAccountNo.Select();

            Load_Acc_Det();

        }

        private void Load_det()
        {
            List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("IssInsCovNot", "");

            //load details
            if (_vehins != null)
            {

                invtype = _vehins[0].Svit_sales_tp;
                txtInsCompany.Text = _vehins[0].Svit_ins_com;
                txtInsPolicy.Text = _vehins[0].Svit_ins_polc;
                lblVeh.Text = _vehins[0].Svit_veh_reg_no;

                txtCusCode.Text = _vehins[0].Svit_cust_cd;
                cmbCustomerTitle.SelectedItem = _vehins[0].Svit_cust_title;
                txtLastName.Text = _vehins[0].Svit_last_name;
                txtFullName.Text = _vehins[0].Svit_full_name;
                txtInitials.Text = _vehins[0].Svit_initial;
                txtAdd1.Text = _vehins[0].Svit_add01;
                txtAdd2.Text = _vehins[0].Svit_add02;
                txtCity.Text = _vehins[0].Svit_city;
                cmbDistrict.Text = _vehins[0].Svit_district;
                txtProvince.Text = _vehins[0].Svit_province;
                txtContact.Text = _vehins[0].Svit_contact;
                //TextBoxMake.Text=_vehins.m
                txtModal.Text = _vehins[0].Svit_model;
                txtCapacity.Text = _vehins[0].Svit_capacity;
                txtEngine.Text = _vehins[0].Svit_engine;
                txtChassis.Text = _vehins[0].Svit_chassis;
                txtInv.Text = _vehins[0].Svit_inv_no;
                txtSaleTp.Text = _vehins[0].Svit_sales_tp;
                txtItem.Text = _vehins[0].Svit_itm_cd;


                List<InvoiceItem> list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_vehins[0].Svit_inv_no);
                List<InvoiceItem> _select = (from _lst in list
                                             where _lst.Sad_unit_rt > 0
                                             select _lst).ToList();

                list = new List<InvoiceItem>();
                list = _select;
                DataTable dt = CHNLSVC.General.GetHpSch(_vehins[0].Svit_inv_no);
                if (dt.Rows.Count > 0)
                {

                    txtSchemeCode.Text = dt.Rows[0]["HPA_SCH_CD"].ToString();
                    txtAccount.Text = dt.Rows[0]["HPA_ACC_NO"].ToString();
                }
                else
                {
                    txtSchemeCode.Text = "";
                    txtAccount.Text = "";
                }
                txtSalesPrice.Text = list[0].Sad_unit_amt.ToString();

                string _cusNo = "";


            }
        }

        private void txtAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                ImgBtnAccountNo_Click(null, null);
        }




        private void txtAccountNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13)
                return;
            if (!string.IsNullOrEmpty(txtAccountNo.Text))
            {
                // Load_Account_Det();
            }
        }

        private void txtAccountNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAccountNo.Text))
            {
                Load_Acc_Det();
            }
        }

        private void Load_Acc_Det()
        {
            List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, TextBoxLocation.Text, txtAccountNo.Text);
            InvoiceHeader _hdrs = null;
            if (_invoice != null)
                if (_invoice.Count > 0) _hdrs = CHNLSVC.Sales.GetInvoiceHeaderDetails(_invoice[0].Sah_inv_no);

            List<VehicleInsuarance> _insurance = CHNLSVC.Sales.GetVehicalInsuranceByInvoice(_invoice[0].Sah_inv_no);
            if (_insurance != null)
            {
                foreach (VehicleInsuarance _tmp in _insurance)
                {
                    txtSaleTp.Text = _tmp.Svit_sales_tp;
                    txtInsCompany.Text = _tmp.Svit_ins_com;
                    txtInsPolicy.Text = _tmp.Svit_ins_polc;
                    lblVeh.Text = _tmp.Svit_veh_reg_no;

                    txtCusCode.Text = _tmp.Svit_cust_cd;
                    cmbCustomerTitle.SelectedItem = _tmp.Svit_cust_title;
                    txtLastName.Text = _tmp.Svit_last_name;
                    txtFullName.Text = _tmp.Svit_full_name;
                    txtInitials.Text = _tmp.Svit_initial;
                    txtAdd1.Text = _tmp.Svit_add01;
                    txtAdd2.Text = _tmp.Svit_add02;
                    txtCity.Text = _tmp.Svit_city;
                    cmbDistrict.Text = _tmp.Svit_district;
                    txtProvince.Text = _tmp.Svit_province;
                    txtContact.Text = _tmp.Svit_contact;
                    //TextBoxMake.Text=_vehins.m
                    txtModal.Text = _tmp.Svit_model;
                    txtCapacity.Text = _tmp.Svit_capacity;
                    txtEngine.Text = _tmp.Svit_engine;
                    txtChassis.Text = _tmp.Svit_chassis;
                    txtCapacity.Text = _tmp.Svit_capacity;
                    txtEngine.Text = _tmp.Svit_engine;
                    txtChassis.Text = _tmp.Svit_chassis;
                    txtInv.Text = _tmp.Svit_inv_no;
                    txtSaleTp.Text = _tmp.Svit_sales_tp;
                    txtItem.Text = _tmp.Svit_itm_cd;
                    dateTimePickerEffectiveDate.Value = Convert.ToDateTime(_tmp.Svit_eff_dt);
                    dateTimePickerExpireNo.Value = Convert.ToDateTime(_tmp.Svit_expi_dt);

                    txtFileNo.Text = _tmp.Svit_file_no;
                    txtDebitNoteNo.Text = _tmp.Svit_dbt_no;
                    txtNetPremium.Text = _tmp.Svit_net_prem.ToString();
                    txtSRCCPre.Text = _tmp.Svit_srcc_prem.ToString();
                    txtOther.Text = _tmp.Svit_oth_val.ToString();
                    txtTotal.Text = _tmp.Svit_tot_val.ToString();
                    txtPolicyNo.Text = _tmp.Svit_polc_no.ToString();

                    DataTable dt = CHNLSVC.General.GetHpSch(_invoice[0].Sah_inv_no);
                    if (dt.Rows.Count > 0)
                    {

                        txtSchemeCode.Text = dt.Rows[0]["HPA_SCH_CD"].ToString();
                        txtAccount.Text = dt.Rows[0]["HPA_ACC_NO"].ToString();
                        txtSalesPrice.Text = dt.Rows[0]["hpa_cash_val"].ToString();
                    }

                }

                DataTable _dtI = CHNLSVC.Financial.Get_veh_ins_renewal_hdr(BaseCls.GlbUserComCode, TextBoxLocation.Text, txtAccountNo.Text);
                if (_dtI.Rows.Count > 0)
                {
                    txtTotal.Text = _dtI.Rows[0]["virnh_amt"].ToString();
                  //  txtOther.Text = _dtI.Rows[0]["virnh_amt"].ToString();
                }
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {



        }

        private void btn_srchVehNo_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VehicalInsuranceRegNo);
            DataTable _result = CHNLSVC.CommonSearch.GetvehicalInsuranceRegistrationNUmber(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtVehicleNo;
            _CommonSearch.ShowDialog();
            txtVehicleNo.Select();

            load_accbyVehNo();
        }

        private void load_accbyVehNo()
        {
            DataTable _dt = CHNLSVC.Sales.GetInsuranceOnEngine(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "Veh", txtVehicleNo.Text);
            if (_dt.Rows.Count > 0)
            {
                txtAccountNo.Text = _dt.Rows[0]["hpa_acc_no"].ToString();
                TextBoxLocation.Text = _dt.Rows[0]["hpa_pc"].ToString();
            }
            else
            {
                txtAccountNo.Text = "";
                TextBoxLocation.Text = "";
            }

            txtAccountNo_Leave(null, null);

        }
        private void txtNetPremium_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalcTotalPremium();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void CalcTotalPremium()
        {
            try
            {
                decimal net = Convert.ToDecimal(txtNetPremium.Text);
                decimal srcc = Convert.ToDecimal(txtSRCCPre.Text);
                decimal total = Convert.ToDecimal(txtTotal.Text);

                txtOther.Text = (total - (net + srcc)).ToString();
            }
            catch (Exception) { }
        }

        private void txtSRCCPre_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalcTotalPremium();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtVehicleNo_Leave(object sender, EventArgs e)
        {

        }



    }
}
