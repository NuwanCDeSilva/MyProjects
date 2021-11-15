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
using FF.WindowsERPClient.HP;

namespace FF.WindowsERPClient.HP
{
    public partial class GroupCompany : Base
    {
        GroupSale _objGS = new GroupSale();

        public GroupCompany()
        {
            InitializeComponent();

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
                case CommonUIDefiniton.SearchUserControlType.OutsideParty:
                    {
                        paramsText.Append("HP" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        private void txtCompName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddr1.Focus();
            }
        }

        private void txtAddr1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddr2.Focus();
            }
        }

        private void txtAddr2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtTel.Focus();
            }
        }

        private void txtTel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFax.Focus();
            }
        }

        private void txtFax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtemail.Focus();
            }
        }

        private void txtemail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveCompany();
        }

        private void SaveCompany()
        {
            if (txtCompName.Text == "")
            {
                MessageBox.Show("Please enter company name", "Company", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            MasterOutsideParty _outsideParty = new MasterOutsideParty();
            string _CompCode = "";

            _outsideParty.Mbi_cd = txtCode.Text;
            _outsideParty.Mbi_country_cd = "LK";
            _outsideParty.Mbi_desc = (txtCompName.Text).ToUpper();
            _outsideParty.Mbi_tp = "HP";
            _outsideParty.Mbi_id = "0";
            //_outsideParty.Mbi_issub = "";
            _outsideParty.Mbi_add1 = txtAddr1.Text;
            _outsideParty.Mbi_add2 = txtAddr2.Text;
            _outsideParty.Mbi_tel = txtTel.Text;
            _outsideParty.Mbi_fax = txtFax.Text;
            _outsideParty.Mbi_email = txtemail.Text;
            _outsideParty.Mbi_web = "";
            _outsideParty.Mbi_town_cd = "";
            _outsideParty.Mbi_tax1 = "";
            _outsideParty.Mbi_tax2 = "";
            _outsideParty.Mbi_tax3 = "";
            _outsideParty.Mbi_act = true;
            _outsideParty.Mbi_cre_by = BaseCls.GlbUserID;
            _outsideParty.Mbi_cre_when = Convert.ToDateTime(DateTime.Now.Date).Date;
            _outsideParty.Mbi_mod_by = BaseCls.GlbUserID;
            _outsideParty.Mbi_mod_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
            _outsideParty.Mbi_session_id = BaseCls.GlbUserSessionID;

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_cd = null;
            masterAuto.Aut_cate_tp = null;
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "COM";
            masterAuto.Aut_start_char = "COM";
            masterAuto.Aut_year = null;

            int row_aff = CHNLSVC.General.SaveOutsideParty(_outsideParty, masterAuto, out _CompCode);
            if (row_aff != -99 && row_aff >= 0)
            {

                //save company details as customer in busentity
                MasterBusinessEntity _customer = new MasterBusinessEntity();
                _customer.Mbe_com = BaseCls.GlbUserComCode;
                _customer.Mbe_cd = _CompCode;
                _customer.Mbe_tp = "C";
                _customer.Mbe_sub_tp = "C";
                _customer.Mbe_acc_cd = "";
                _customer.Mbe_name = (txtCompName.Text).ToUpper();
                _customer.Mbe_add1 = txtAddr1.Text;
                _customer.Mbe_add2 = txtAddr2.Text;
                _customer.Mbe_country_cd = "";
                _customer.Mbe_province_cd = "";
                _customer.Mbe_distric_cd = "";
                _customer.Mbe_town_cd = "";
                _customer.Mbe_tel = txtTel.Text;
                _customer.Mbe_fax = txtFax.Text;
                _customer.Mbe_mob = "";
                _customer.Mbe_nic = "";
                _customer.Mbe_email = txtemail.Text;
                _customer.Mbe_contact = "";
                _customer.Mbe_act = true;
                _customer.Mbe_tax_no = "";

                int effect = CHNLSVC.Sales.SaveGrpCompAsCustomer(_customer);

                GroupSale _grpSale = new GroupSale();
                _grpSale.setCompanyCode(_CompCode);
                ClearCompany();
                this.Close();
            }
            else
            {
                MessageBox.Show(_CompCode, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearCompany()
        {
            txtCompName.Text = "";
            txtAddr1.Text = "";
            txtAddr2.Text = "";
            txtTel.Text = "";
            txtFax.Text = "";
            txtemail.Text = "";
            txtCode.Text = "";
        }

        private void imgBtn_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OutsideParty);
            DataTable _result = CHNLSVC.CommonSearch.GetOutsidePartySearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCode;
            _CommonSearch.ShowDialog();
            txtCode.Select();
            GetCompanyData(null, null);
            txtCompName.Focus();
        }

        protected void GetCompanyData(object sender, EventArgs e)
        {
            MasterOutsideParty _outsideParty = null;
            if (txtCode.Text == "")
            {
                txtCompName.Text = "";
                txtemail.Text = "";
                txtFax.Text = "";
                txtTel.Text = "";
                txtAddr1.Text = "";
                txtAddr2.Text = "";


                return;
            }
            _outsideParty = CHNLSVC.General.GetOutsideParty(txtCode.Text);
            if (_outsideParty != null)
            {
                txtCompName.Text = _outsideParty.Mbi_desc;
                txtemail.Text = _outsideParty.Mbi_email;
                txtFax.Text = _outsideParty.Mbi_fax;
                txtTel.Text = _outsideParty.Mbi_tel;
                txtAddr1.Text = _outsideParty.Mbi_add1;
                txtAddr2.Text = _outsideParty.Mbi_add2;
            }
            else
            {
                txtCompName.Text = "";
                txtemail.Text = "";
                txtFax.Text = "";
                txtTel.Text = "";
                txtAddr1.Text = "";
                txtAddr2.Text = "";
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearCompany();
            txtCompName.Focus();
        }
    }
}
