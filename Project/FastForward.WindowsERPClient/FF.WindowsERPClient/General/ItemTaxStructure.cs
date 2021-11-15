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

namespace FF.WindowsERPClient.General
{
    public partial class ItemTaxStructure : Base
    {
        List<mst_itm_tax_structure_det> _lstTaxDet = new List<mst_itm_tax_structure_det>();
        mst_itm_tax_structure_hdr _taxHdr = new mst_itm_tax_structure_hdr();
        public ItemTaxStructure()
        {
            InitializeComponent();
            BindCombo();
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
              
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
             
                default:
                    break;
            }

            return paramsText.ToString();
        }
        void BindCombo()
        {
           

            DataTable _status = CHNLSVC.Inventory.GetItemStatusMaster("ALL", "ALL");
            if (_status != null && _status.Rows.Count > 0)
            {
                cmbwStatus.DataSource = _status;
                cmbwStatus.DisplayMember = "MIS_DESC";
                cmbwStatus.ValueMember = "MIS_CD";
                cmbwStatus.SelectedIndex = -1;

                            


            }
            DataTable _TaxCodes = CHNLSVC.General.GetTaxCode();
            if (_TaxCodes != null && _TaxCodes.Rows.Count > 0)
            {
                cmbTaxCode.DataSource = _TaxCodes;
                cmbTaxCode.DisplayMember = "mtc_tax_rt_cd";
                cmbTaxCode.ValueMember = "mtc_tax_cd";
                cmbTaxCode.SelectedIndex = -1;
                



            }

        }
        private void btnSearchreCom_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCompany;
                _CommonSearch.ShowDialog();

                txtCompany.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbwStatus.Focus();
            }
            else if (e.KeyCode == Keys.F2 )
            {
                btnSearchreCom_Click(null, null);
            }
        }

        private void txtTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) btnAddstatus.Focus();  //Allow Enter   
        }

        private void txtCompany_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddstatus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCompany.Text))
            {
                MessageBox.Show("Enter company", "Item Tax Structure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(cmbwStatus.Text))
            {
                MessageBox.Show("Enter item status ", "Item Tax Structure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(cmbTaxCode.Text))
            {
                MessageBox.Show("Enter Tax code ", "Item Tax Structure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtTax.Text))
            {
                MessageBox.Show("Enter Tax rate  ", " Tax Structure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }



            if (_lstTaxDet != null)
            {
                mst_itm_tax_structure_det result = _lstTaxDet.Find(x => x.Its_com == txtCompany.Text && x.Its_stus == cmbwStatus.Text && x.Its_tax_cd == cmbTaxCode.Text);
                if (result != null)
                {
                    MessageBox.Show("This Tax definition already available", "Item Tax Structure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


            }
            else
            {
                _lstTaxDet = new List<mst_itm_tax_structure_det>();
            }
            mst_itm_tax_structure_det _itm = new mst_itm_tax_structure_det();
            _itm.Its_com = txtCompany.Text;
            _itm.Its_stus = cmbwStatus.SelectedValue.ToString ();
            _itm.Its_tax_cd = cmbTaxCode.SelectedValue.ToString();
            _itm.Its_tax_rate = Convert.ToDecimal(txtTax.Text);
            _itm.Its_act = true;
            _lstTaxDet.Add(_itm);

            gvTax.DataSource = null;
            gvTax.AutoGenerateColumns = false;
            gvTax.DataSource = new List<mst_itm_tax_structure_det>();
            gvTax.DataSource = _lstTaxDet;



            txtCompany.Text = "";
            cmbwStatus.SelectedIndex = -1;
            cmbTaxCode.SelectedIndex = -1;
            txtTax.Text = "";
            txtCompany.Focus();
        }

        private void gvTax_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear_Data();
        }
        private void Clear_Data()
        {
            txtCompany.Text = "";
            cmbwStatus.Text = "";
            cmbTaxCode.Text = "";
            txtTax.Text = "";
            txtStruc.Text = "";
            txtDes.Text = "";
            cmbwStatus.SelectedIndex = -1;
            cmbTaxCode.SelectedIndex = -1;
            gvTax.DataSource = null;
            gvTax.AutoGenerateColumns = false;
            gvTax.DataSource = new List<mst_itm_tax_structure_det>();
            _lstTaxDet = new List<mst_itm_tax_structure_det>();
            _taxHdr = new mst_itm_tax_structure_hdr();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStruc.Text))
            {
                List<mst_itm_tax_structure_hdr> _lstTaxhdr = new List<mst_itm_tax_structure_hdr>();
                _lstTaxhdr = CHNLSVC.General.GetItemTaxStructureHeader(txtStruc.Text);
                if (_lstTaxhdr != null && _lstTaxhdr.Count > 0)
                {
                    _taxHdr.Ish_stuc_seq = _lstTaxhdr[0].Ish_stuc_seq;

                }
            }


          
              if (string.IsNullOrEmpty(txtDes.Text))
              {
                  MessageBox.Show("Enter Description", "Item Tax Structure ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  return;
              }

        
              if (_lstTaxDet.Count == null)
              {
                  MessageBox.Show("Enter tax details", "Item Tax Structure ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  return;
              }
              if (_lstTaxDet.Count == 0)
              {
                  MessageBox.Show("Enter tax details", "Item Tax Structure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  return;
              }


              _taxHdr.Ish_com = BaseCls.GlbUserComCode;
              _taxHdr.Ish_cre_by = BaseCls.GlbUserID;
              _taxHdr.Ish_date = Convert.ToDateTime(DateTime.Today).Date;
              _taxHdr.Ish_mod_by = BaseCls.GlbUserID;
              _taxHdr.Ish_stuc_code = txtStruc.Text;
              _taxHdr.Ish_act = 1;
              _taxHdr.Ish_des = txtDes.Text;


              MasterAutoNumber _taxAuto = new MasterAutoNumber();
             #region Auto Number
             _taxAuto.Aut_cate_cd = BaseCls.GlbUserComCode;
            _taxAuto.Aut_cate_tp = "COM";
            _taxAuto.Aut_moduleid = "TAX";
            _taxAuto.Aut_direction = 0;
            _taxAuto.Aut_start_char = "TAX";
            _taxAuto.Aut_year = DateTime.Today.Year;
            #endregion
            string _err=string.Empty ;

            int row_aff = CHNLSVC.General.SaveItemTaxStructure(_taxHdr, _lstTaxDet, _taxAuto,out _err);
            if (row_aff == 1)
            {
                MessageBox.Show(_err, "Item Tax Structure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear_Data();
            }
        }

        private void btnsrhStuc_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterTax);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchTaxMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtStruc;
            _CommonSearch.txtSearchbyword.Text = txtStruc.Text;
            _CommonSearch.ShowDialog();
            txtStruc.Focus();
        }

        private void txtStruc_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtStruc_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStruc.Text))
            {
                List<mst_itm_tax_structure_hdr> _lstTaxhdr = new List<mst_itm_tax_structure_hdr>();
                _lstTaxhdr = CHNLSVC.General.GetItemTaxStructureHeader(txtStruc.Text);
                if (_lstTaxhdr != null && _lstTaxhdr.Count >0 )
                {
                    txtDes.Text = _lstTaxhdr[0].Ish_des;
          
                _lstTaxDet = CHNLSVC.General.getItemTaxStructure(txtStruc.Text);
                if (_lstTaxDet != null && _lstTaxDet.Count > 0)
                {
                   
                    gvTax.DataSource = null;
                    gvTax.AutoGenerateColumns = false;
                    gvTax.DataSource = new List<mst_itm_cust>();
                    gvTax.DataSource = _lstTaxDet;
                }
                }
                else
                {
                    MessageBox.Show("Invalid Tax structure", "Item Tax Structure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtStruc.Text = "";
                    txtStruc.Focus();
                }
            }
        }

        private void cmbwStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbTaxCode.Focus();
            }
        }

        private void cmbTaxCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtTax.Focus();
            }
        }

        private void txtCompany_DoubleClick(object sender, EventArgs e)
        {
            btnSearchreCom_Click(null, null);
        }

        private void txtStruc_DoubleClick(object sender, EventArgs e)
        {
            btnsrhStuc_Click(null, null);
        }

        private void txtDes_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtStruc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDes.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnsrhStuc_Click(null, null);

            }
        }

        private void cmbwStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtTax_TextChanged(object sender, EventArgs e)
        {

        }

        private void gvTax_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvTax_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (MessageBox.Show("Are you sure you want to delete?", "Item Tax Structure", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string _com = gvTax.SelectedCells[1].Value.ToString();
                            string _stataus = gvTax.SelectedCells[2].Value.ToString();
                            string _taxcode = gvTax.SelectedCells[3].Value.ToString();

                            _lstTaxDet.RemoveAll(x => x.Its_com == _com && x.Its_stus == _stataus && x.Its_tax_cd == _taxcode);
                            gvTax.AutoGenerateColumns = false;
                            gvTax.DataSource = new List<mst_itm_tax_structure_det>();
                            gvTax.DataSource = _lstTaxDet;

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

        private void txtCompany_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCompany.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtCompany.Text.Trim());
                    if (com == null)
                    {
                        MessageBox.Show("Invalid Company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCompany.Text = "";
                        txtCompany.Focus();
                        return;
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtTax_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTax.Text))
            {
                if (Convert.ToDecimal(txtTax.Text) > 100)
                {
                    MessageBox.Show("Rate must be lesser than 100", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTax.Text = "";
                    txtTax.Focus();
                    return;
                }
                if (Convert.ToDecimal(txtTax.Text) < 0)
                {
                    MessageBox.Show("Rate must be higher than 0(zero)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTax.Text = "";
                    txtTax.Focus();
                    return;
                }
            }
        }
    }
}
