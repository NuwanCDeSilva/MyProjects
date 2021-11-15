using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Threading;
using System.Collections;
using FF.WindowsERPClient.Inventory;
using System.Globalization;
using FF.WindowsERPClient.Reports.Sales;

//Written By kapila on 11/2/2015
namespace FF.WindowsERPClient.Inventory
{
    public partial class ItemComponentSetup : Base
    {
        public ItemComponentSetup()
        {
            InitializeComponent();
            InitializeEnv();
        }

        private void InitializeEnv()
        {
            load_item_component_det();
        }

        private void clearAll()
        {
            txtIcat1.Text = "";
            txtIcat2.Text = "";
            txtIcat3.Text = "";
            txtItemCode.Text = "";
            txtQty.Text = "";
            chkAct.Checked = true;
            chkMan.Checked = true;
            chkWar.Checked = true;

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
            btn_Srch_Cat2.Enabled = true;
            txtIcat2.Text = "";
            txtIcat3.Text = "";
            txtItemCode.Text = "";
        }

        private void btn_Srch_Cat2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIcat1.Text))
            {
                MessageBox.Show("Select Item category 1", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

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
            btn_Srch_Cat3.Enabled = true;
            txtIcat3.Text = "";
            txtItemCode.Text = "";
        }

        private void btn_Srch_Cat3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIcat1.Text))
            {
                MessageBox.Show("Select Item category 1", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtIcat2.Text))
            {
                MessageBox.Show("Select Item category 2", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtIcat3;
            _CommonSearch.txtSearchbyword.Text = txtIcat3.Text;
            _CommonSearch.ShowDialog();
            txtIcat3.Focus();
            txtItemCode.Text = "";
        }

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
                        paramsText.Append(txtIcat1.Text + seperator + txtIcat2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub2.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemCate:
                    {
                        paramsText.Append(txtIcat1.Text + seperator + txtIcat2.Text + seperator + txtIcat3.Text + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void txtIcat1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_Srch_Cat1_Click(null, null);
        }

        private void txtIcat1_DoubleClick(object sender, EventArgs e)
        {
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

        private void txtIcat3_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Cat3_Click(null, null);
        }

        private void txtIcat3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_Srch_Cat3_Click(null, null);
        }

        private void btn_Srch_Itm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIcat1.Text) && string.IsNullOrEmpty(txtIcat2.Text) && string.IsNullOrEmpty(txtIcat3.Text))
            {
                MessageBox.Show("Select Item category", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemCate);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataByCat(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtItemCode;
            _CommonSearch.ShowDialog();
            txtItemCode.Focus();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItemCode.Text))
            {
                MessageBox.Show("Select Item code", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(txtIcat1.Text))
            {
                MessageBox.Show("Select Item category 1", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(txtIcat2.Text) && string.IsNullOrEmpty(txtIcat3.Text))
            {
                MessageBox.Show("Select Item category 2 or category 3", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(txtQty.Text))
            {
                MessageBox.Show("Select enter quantity", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            Int32 _eff = CHNLSVC.CustService.UpdateItemComponent(txtIcat1.Text, txtIcat2.Text, txtIcat3.Text, txtItemCode.Text, Convert.ToInt32(txtQty.Text), Convert.ToInt32(chkWar.Checked), Convert.ToInt32(chkAct.Checked), Convert.ToInt32(chkMan.Checked), BaseCls.GlbUserID);
            if (_eff == 1)
            {
                MessageBox.Show("Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load_item_component_det();
                clearAll();
            }

        }

        private void load_item_component_det()
        {
            DataTable _dt = CHNLSVC.CustService.getItemComponentDet();
            gvRec.AutoGenerateColumns = false;
            gvRec.DataSource = _dt;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
            clearAll();
        }

        private void txtIcat1_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIcat1.Text))
            {
                DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(txtIcat1.Text);
                if (_categoryDet.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid Item category 1", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtIcat1.Text = "";
                    txtIcat1.Focus();
                    return;
                }
            }
            if (string.IsNullOrEmpty(txtIcat1.Text))
            {
                btn_Srch_Cat2.Enabled = false;
                btn_Srch_Cat3.Enabled = false;
            }
            else
            {
                btn_Srch_Cat2.Enabled = true;
                btn_Srch_Cat3.Enabled = true;
            }
        }

        private void txtIcat2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIcat2.Text))
            {
                DataTable _cat2 = CHNLSVC.Sales.GetItemSubCate2(txtIcat1.Text, txtIcat2.Text);
                if (_cat2.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid Item category 2", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtIcat2.Text = "";
                    txtIcat2.Focus();
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtIcat2.Text))
                btn_Srch_Cat3.Enabled = false;
            else
                btn_Srch_Cat3.Enabled = true;
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            if (!IsNumeric(txtQty.Text))
            {
                MessageBox.Show("Invalid quantity", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtQty.Text = "0";
                txtQty.Focus();
                return;
            }
            if (Convert.ToDecimal(txtQty.Text) < 0)
            {
                MessageBox.Show("Invalid quantity", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtQty.Text = "0";
                txtQty.Focus();
                return;
            }
        }

        private void txtIcat3_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIcat3.Text))
            {
                DataTable _cat3 = CHNLSVC.Sales.GetItemSubCate3(txtIcat1.Text, txtIcat2.Text, txtIcat3.Text);
                if (_cat3.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid Item category 3", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtIcat3.Text = "";
                    txtIcat3.Focus();
                    return;
                }
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
                e.Handled = true;
        }

        private void gvRec_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvRec_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIcat1.Text = Convert.ToString(gvRec.Rows[e.RowIndex].Cells["MCC_CAT1"].Value);
            txtIcat2.Text = Convert.ToString(gvRec.Rows[e.RowIndex].Cells["MCC_CAT2"].Value);
            txtIcat3.Text = Convert.ToString(gvRec.Rows[e.RowIndex].Cells["MCC_CAT3"].Value);
            txtQty.Text = Convert.ToString(gvRec.Rows[e.RowIndex].Cells["mcc_qty"].Value);
            chkAct.Checked = Convert.ToBoolean(gvRec.Rows[e.RowIndex].Cells["mcc_act"].Value);
            chkMan.Checked = Convert.ToBoolean(gvRec.Rows[e.RowIndex].Cells["mcc_isser"].Value);
            chkWar.Checked = Convert.ToBoolean(gvRec.Rows[e.RowIndex].Cells["mcc_supp_warr"].Value);
            txtItemCode.Text = Convert.ToString(gvRec.Rows[e.RowIndex].Cells["mcc_itm_cd"].Value);
        }


    }
}


