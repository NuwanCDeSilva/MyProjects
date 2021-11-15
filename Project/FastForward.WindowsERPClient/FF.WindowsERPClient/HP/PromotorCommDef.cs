using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FF.BusinessObjects;

namespace FF.WindowsERPClient.HP
{
    public partial class PromotorCommDef : Base
    {
        List<MasterItem> Item_List;
        List<HPPrmotorCommDef> Main_List;

        public PromotorCommDef()
        {
            InitializeComponent();
            LoadSchemeCategory();
            LoadSchemeType();
            Item_List = new List<MasterItem>();
            Main_List = new List<HPPrmotorCommDef>();
        }

        private void LoadSchemeCategory()
        {
            DataTable dt = CHNLSVC.Sales.GetSAllchemeCategoryies("%");
            DropDownListSchemeCategory.DataSource = dt;
            DropDownListSchemeCategory.DisplayMember = "HSC_DESC";
            DropDownListSchemeCategory.ValueMember = "HSC_CD";
            DropDownListSchemeCategory.SelectedIndex = -1;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            PromotorCommDef formnew = new PromotorCommDef();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
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
                        paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
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
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {


        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            string _msg = "";
            int _eff = CHNLSVC.Sales.SavePromotorCommDef(Main_List, out _msg);
            if (_eff == 1)
            {
                MessageBox.Show("Successfully Saved", "Promotor Comm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnClear_Click(null, null);
            }
            else
                MessageBox.Show(_msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void chkAllSchm_CheckedChanged(object sender, EventArgs e)
        {
            DropDownListSchemeType.SelectedIndex = -1;
        }

        private void DropDownListSchemeCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnClearScheme_Click(null, null);
            LoadSchemeType();
        }

        private void LoadSchemeType()
        {
            if (DropDownListSchemeCategory.SelectedIndex != -1)
            {
                List<HpSchemeType> _schemeList = CHNLSVC.Sales.GetSchemeTypeByCategory(DropDownListSchemeCategory.SelectedValue.ToString());

                foreach (HpSchemeType sc in _schemeList)
                {
                    string space = "";
                    if ((10 - sc.Hst_cd.Length) > 0)
                    {
                        for (int i = 0; i <= (10 - sc.Hst_cd.Length); i++)
                        {
                            space = space + " ";
                        }
                    }
                    sc.Hst_desc = sc.Hst_cd + space + "--" + sc.Hst_desc;
                }

                DropDownListSchemeType.DataSource = _schemeList;
                DropDownListSchemeType.DisplayMember = "Hst_desc";
                DropDownListSchemeType.ValueMember = "Hst_cd";

                chkAllSchm.Checked = false;
            }
        }

        private void btnAll_Schemes_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvSchemes.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvSchemes.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNon_Schemes_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvSchemes.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvSchemes.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void DropDownListSchemeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnAddSchemes_Click(null, null);
        }

        private void btnAddSchemes_Click(object sender, EventArgs e)
        {
            if (DropDownListSchemeCategory.SelectedIndex == -1)
            {
                return;
            }
            if (chkAllSchm.Checked == true)
            {
                DropDownListSchemeType.SelectedIndex = -1;
            }
            if (chkAllSchm.Checked == true)
            {
                List<HpSchemeType> _schemeList = CHNLSVC.Sales.GetSchemeTypeByCategory(DropDownListSchemeCategory.SelectedValue.ToString());
                DropDownListSchemeType.DataSource = _schemeList;
                DropDownListSchemeType.DisplayMember = "Hst_desc";
                DropDownListSchemeType.ValueMember = "Hst_cd";

                DataTable dt = new DataTable();
                foreach (HpSchemeType schTp in _schemeList)
                {
                    DataTable dt1 = CHNLSVC.Sales.GetSchemes("TYPE", schTp.Hst_cd);
                    dt.Merge(dt1);
                }

                grvSchemes.DataSource = null;
                grvSchemes.AutoGenerateColumns = false;
                grvSchemes.DataSource = dt;
            }
            else
            {
                if (DropDownListSchemeType.SelectedIndex == -1)
                {
                    grvSchemes.DataSource = null;
                    grvSchemes.AutoGenerateColumns = false;

                    return;
                }
                DataTable datasource = CHNLSVC.Sales.GetSchemes("TYPE", DropDownListSchemeType.SelectedValue.ToString());
                //  LoadList(ListBoxSchemes, datasource, "HSD_CD", "HSD_DESC");
                chkAllSchm.Checked = false;

                grvSchemes.DataSource = null;
                grvSchemes.AutoGenerateColumns = false;
                grvSchemes.DataSource = datasource;
            }
        }

        private void btnClearScheme_Click(object sender, EventArgs e)
        {
            checkBox_SCHEM.Checked = false;
            grvSchemes.DataSource = null;
            grvSchemes.AutoGenerateColumns = false;
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

        private void btnMainCat_Click(object sender, EventArgs e)
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

        private void btnCat_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
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
                _CommonSearch.ShowDialog();
                txtItemCD.Select();
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

        private void btnItemAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gr in dataGridViewItem.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridViewItem.Rows[gr.Index].Cells[0];
                chk.Value = "True";
            }
        }

        private void btnItemNone_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gr in dataGridViewItem.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridViewItem.Rows[gr.Index].Cells[0];
                chk.Value = "False";
            }
        }

        private void btnItemClear_Click(object sender, EventArgs e)
        {
            dataGridViewItem.DataSource = null;
        }

        private void rdoInsuRate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoInsuAmt.Checked)
            {
                TextBoxInsuRate.Enabled = false;
                TextBoxInsuAmt.Enabled = true;
                TextBoxInsuRate.Text = "0.00";
                TextBoxInsuAmt.Text = "0.00";//
            }
            else
            {
                TextBoxInsuRate.Enabled = true;
                TextBoxInsuAmt.Enabled = false;
                TextBoxInsuRate.Text = "0.00";
                TextBoxInsuAmt.Text = "0.00";//
            }
        }

        private void btnSrchCirc_Click(object sender, EventArgs e)
        {


            pnlComm.Enabled = false;
            pnlItem.Enabled = false;
            pnlSch.Enabled = false;
            txtCirc.ReadOnly = true;
        }

        private void checkBox_SCHEM_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_SCHEM.Checked == true)
            {
                this.btnAll_Schemes_Click(null, null);
            }
            else
            {
                this.btnNon_Schemes_Click(null, null);
            }
        }

        private void btnAddCat_Click(object sender, EventArgs e)
        {
            try
            {
                string selection = "ITEM";

                //ItemBrandCat_List = new List<CashCommissionDetailRef>();
                DataTable dt = CHNLSVC.Sales.GetBrandsCatsItems_new(selection, txtBrand.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), null, txtItemCD.Text.Trim(), null, null, null,txtModel.Text);

                foreach (DataRow dr in dt.Rows)
                {
                    string code = dr["code"].ToString();
                    MasterItem obj = new MasterItem(); //for display purpose
                    obj.Mi_cd = code;
                    obj.Mi_shortdesc = dr["descript"].ToString();

                    var _duplicate = from _dup in Item_List
                                     where _dup.Mi_cd == obj.Mi_cd
                                     select _dup;
                    if (_duplicate.Count() == 0)
                    {
                        Item_List.Add(obj);
                    }

                }

                BindingSource source = new BindingSource();
                source.DataSource = Item_List;
                dataGridViewItem.AutoGenerateColumns = false;
                dataGridViewItem.DataSource = source;

                foreach (DataGridViewRow gr in dataGridViewItem.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridViewItem.Rows[gr.Index].Cells[0];
                    chk.Value = "true";
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCirc.Text))
            {
                MessageBox.Show("Please enter the circular number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (rdoInsuRate.Checked == true && Convert.ToDecimal(TextBoxInsuRate.Text) == 0)
            {
                MessageBox.Show("Please enter the commission rate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxInsuRate.Focus();
                return;
            }
            if (rdoInsuAmt.Checked == true && Convert.ToDecimal(TextBoxInsuAmt.Text) == 0)
            {
                MessageBox.Show("Please enter the commission amounts", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxInsuAmt.Focus();
                return;
            }

            if (Convert.ToDateTime(TextBoxFromDate.Value.Date) > Convert.ToDateTime(TextBoxToDate.Value.Date))
            {
                MessageBox.Show("Cannot add. Invalid date !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (grvSchemes.Rows.Count == 0)
            {
                MessageBox.Show("Please select the schemes.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataGridViewRow dgvr in grvSchemes.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    foreach (DataGridViewRow dr in dataGridViewItem.Rows)
                    {
                        DataGridViewCheckBoxCell chk1 = dr.Cells[0] as DataGridViewCheckBoxCell;
                        if (Convert.ToBoolean(chk1.Value) == true)
                        {
                            List<HPPrmotorCommDef> tem = (from _res in Main_List
                                                          where _res.Hpcm_circ == txtCirc.Text && _res.Hpcm_from_dt == Convert.ToDateTime(TextBoxFromDate.Text) && _res.Hpcm_itm_cd == dr.Cells[1].Value.ToString() && _res.Hpcm_sch_cd== dgvr.Cells[1].Value.ToString() 
                                                          select _res).ToList<HPPrmotorCommDef>();
                            if (tem == null || tem.Count == 0)
                            {
                                HPPrmotorCommDef _promot = new HPPrmotorCommDef();
                                _promot.Hpcm_circ = txtCirc.Text;
                                _promot.Hpcm_com = BaseCls.GlbUserComCode;
                                _promot.Hpcm_comm_amt = Convert.ToDecimal(TextBoxInsuAmt.Text);
                                _promot.Hpcm_comm_rt = Convert.ToDecimal(TextBoxInsuRate.Text);
                                _promot.Hpcm_cre_by = BaseCls.GlbUserID;
                                _promot.Hpcm_from_dt = Convert.ToDateTime(TextBoxFromDate.Text);
                                _promot.Hpcm_itm_cd = dr.Cells[1].Value.ToString();
                                _promot.Hpcm_mod_by = BaseCls.GlbUserID;
                                _promot.Hpcm_sch_cd = dgvr.Cells[1].Value.ToString();
                                _promot.Hpcm_to_dt = Convert.ToDateTime(TextBoxToDate.Text);
                                Main_List.Add(_promot);
                            }
                        }
                    }
                }

            }

            BindingSource source = new BindingSource();
            source.DataSource = Main_List;
            grvReceptDet.AutoGenerateColumns = false;
            grvReceptDet.DataSource = source;

        }

        private void pnlItem_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_Srch_Model_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void TextBoxInsuRate_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxInsuRate.Text))
            {
                if (!IsNumeric(TextBoxInsuRate.Text))
                {
                    MessageBox.Show("Amount should be numeric.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextBoxInsuRate.Text = "0.00";
                    TextBoxInsuRate.Focus();
                    return;
                }
            }
            if (Convert.ToDecimal(TextBoxInsuRate.Text) < 0 || Convert.ToDecimal(TextBoxInsuRate.Text) > 100)
            {
                MessageBox.Show("Invalid commission rate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TextBoxInsuRate.Text = "0.00";
                TextBoxInsuRate.Focus();
                return;
            }

        }

        private void TextBoxInsuAmt_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxInsuAmt.Text))
            {
                if (!IsNumeric(TextBoxInsuAmt.Text))
                {
                    MessageBox.Show("Amount should be numeric.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextBoxInsuAmt.Text = "0.00";
                    TextBoxInsuAmt.Focus();
                    return;
                }
            }
        }

        private void txtCirc_Leave(object sender, EventArgs e)
        {
            //TODO - check circular no exist
        }

        private void TextBoxInsuRate_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void TextBoxInsuRate_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void TextBoxInsuAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }


    }
}
