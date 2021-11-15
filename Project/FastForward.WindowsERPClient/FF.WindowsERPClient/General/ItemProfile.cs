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
using System.Data.OleDb;
using System.Configuration;

namespace FF.WindowsERPClient.General
{
    public partial class ItemProfile : Base
    {
        MasterItem _item = new MasterItem();
        List<mst_itm_fg_cost> _lstfg_cost = new List<mst_itm_fg_cost>();
        List<mst_itm_com_reorder> _lstreorder = new List<mst_itm_com_reorder>();
        List<MasterCompanyItem> _lstcomItem = new List<MasterCompanyItem>();
        List<BusEntityItem> _lstcusItem = new List<BusEntityItem>();
        List<BusEntityItem> _lstsupItem = new List<BusEntityItem>();
        List<mst_itm_redeem_com> _lstredCom = new List<mst_itm_redeem_com>();
        List<mst_itm_mrn_com> _lstmrn = new List<mst_itm_mrn_com>();
        List<mst_itm_replace> _lstreplace = new List<mst_itm_replace>();
        List<MasterItemComponent> _lstkit = new List<MasterItemComponent>();
        List<MasterItemTaxClaim> _lsttaxClaim = new List<MasterItemTaxClaim>();
        List<mst_itm_sevpd> _lstitmPrd = new List<mst_itm_sevpd>();
        List<MasterItemWarrantyPeriod> _lstitmWrd = new List<MasterItemWarrantyPeriod>();
        List<mst_itm_pc_warr> _lstpcWrd = new List<mst_itm_pc_warr>();
        List<mst_itm_channlwara> _lstchannelWrd = new List<mst_itm_channlwara>();
        List<mst_itm_container> _lstcont = new List<mst_itm_container>();
        List<MasterItemTax> _lstcomItemTax = new List<MasterItemTax>();
        Boolean _isAutoCode = false;
        Boolean _isAddTax = false;

        public ItemProfile()
        {
            InitializeComponent();
            txtTaxComp.Text = BaseCls.GlbUserComCode;
            dtpEffDt.Value = DateTime.Now.Date;
            // Clear();
            BindCombo();

            txtItem.Focus();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_Srch_loc_Click(object sender, EventArgs e)
        {

        }

        private void txtItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSdes.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btn_SrchCode_Click(null, null);

            }

        }

        private void txtModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBrand.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btn_SrchModel_Click(null, null);
            }
        }

        private void txtSdes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtLdes.Focus();
            }
        }

        private void txtLdes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtModel.Focus();
            }
        }

        private void txtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPartNo.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btn_SrchBrand_Click(null, null);
            }

        }

        private void txtPartNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPackCode.Focus();
            }

        }

        private void txtPackCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUOM.Focus();
            }

        }

        private void txtUOM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //  dtExpDate.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {

            }
        }

        private void dtExpDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbActive.Focus();
            }

        }

        private void cmbActive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMainCat.Focus();
            }

        }





        private void btnSave_Click(object sender, EventArgs e)
        {



            if (_isAutoCode == false)
            {

                if (string.IsNullOrEmpty(txtItem.Text)) { MessageBox.Show("Enter Item Code", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            }

            string _err;
            int row_aff = 0;
            if (tbAdd.SelectedTab == tbAdd.TabPages[5])
            {
                if (_isAddTax == false)
                {
                    MessageBox.Show("Please add tax", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (MessageBox.Show("Are You Sure ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No) return;

                row_aff = CHNLSVC.General.SaveItemComTax(_lstcomItemTax, out _err);
                if (row_aff == 1)
                {
                    MessageBox.Show(_err, "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();

                }
                else
                {
                    MessageBox.Show(_err, "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtSdes.Text)) { MessageBox.Show("Enter Short Description", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtLdes.Text)) { MessageBox.Show("Enter Long Description", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtModel.Text)) { MessageBox.Show("Enter Model", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtBrand.Text)) { MessageBox.Show("Enter Brand", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtPartNo.Text)) { MessageBox.Show("Enter Part No", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtPackCode.Text)) { MessageBox.Show("Enter Paking Code", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtUOM.Text)) { MessageBox.Show("Enter UOM", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtMainCat.Text)) { MessageBox.Show("Enter Main Category", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtCat1.Text)) { MessageBox.Show("Enter Sub Category 1", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtCat2.Text)) { MessageBox.Show("Enter Sub Category 2", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtCat3.Text)) { MessageBox.Show("Enter Sub Category 3", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtCat4.Text)) { MessageBox.Show("Enter Sub Category 4", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(cmbItemType.Text)) { MessageBox.Show("Enter Item Type", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtCapacity.Text)) { MessageBox.Show("Enter Capacity", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtColorExt.Text)) { MessageBox.Show("Enter External Color", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtColorInt.Text)) { MessageBox.Show("Enter Internal Color", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(cmbPayType.Text)) { MessageBox.Show("Enter Pay Type ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                // if (string.IsNullOrEmpty(txtPrefix.Text)) { MessageBox.Show("Enter Prefix", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(cmbStatus.Text)) { MessageBox.Show("Enter Status", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(cmbSerilize.Text)) { MessageBox.Show("Enter Serialize Status", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(cmbWarranty.Text)) { MessageBox.Show("Enter Warranty Status", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(cmbChassis.Text)) { MessageBox.Show("Enter Chassis Status", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(cmbScanSub.Text)) { MessageBox.Show("Enter Suba Serial Status", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(cmbhpSalesAccept.Text)) { MessageBox.Show("Enter Hp Accepted Status", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(cmbIsRegister.Text)) { MessageBox.Show("Enter Registration Status", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtPurComp.Text)) { MessageBox.Show("Enter Purchase Company", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(ttxHsCode.Text)) { MessageBox.Show("Enter HS Code", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtCountry.Text)) { MessageBox.Show("Enter Country", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtTaxStucture.Text)) { MessageBox.Show("Enter Tax structure", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                CollectMaster();


                row_aff = CHNLSVC.General.SaveItemMaster(_item, _lstchannelWrd, _lstpcWrd, _lstitmWrd, _lstitmPrd, _lsttaxClaim, _lstcont, _lstkit, _lstreplace, _lstmrn, _lstredCom, _lstsupItem, _lstcusItem, _lstcomItem, _lstreorder, _lstfg_cost, null, _isAutoCode, BaseCls.GlbUserComCode, out _err);
                if (row_aff == 1)
                {
                    MessageBox.Show(_err, "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();

                }
                else
                {
                    MessageBox.Show(_err, "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
        #region CollectoLsit
        private void CollectMaster()
        {
            _item.Mi_cd = txtItem.Text.ToString().Trim();
            _item.Mi_shortdesc = txtSdes.Text;
            _item.Mi_longdesc = txtLdes.Text;
            _item.Mi_cate_1 = txtMainCat.Text;
            _item.Mi_cate_2 = txtCat1.Text;
            _item.Mi_cate_3 = txtCat2.Text;
            _item.Mi_cate_4 = txtCat3.Text;
            _item.Mi_cate_5 = txtCat4.Text;
            _item.Mi_brand = txtBrand.Text;
            _item.Mi_model = txtModel.Text;
            _item.Mi_part_no = txtPartNo.Text;
            _item.Mi_color_int = txtColorInt.Text;
            _item.Mi_is_reqcolorint = chkColorInt.Checked;
            _item.Mi_color_ext = txtColorExt.Text;
            _item.Mi_is_reqcolorext = chkColorExt.Checked;
            _item.Mi_itm_tp = cmbItemType.SelectedValue.ToString();
            _item.Mi_is_stockmaintain = chkStcokMain.Checked == true ? "1" : "0";
            _item.Mi_hs_cd = ttxHsCode.Text;
            _item.Mi_itm_uom = txtUOM.Text;
            _item.Mi_dim_uom = txtduom.Text;
            if (string.IsNullOrEmpty(txtbreath.Text))
            {
                txtbreath.Text = "0";
            }
            _item.Mi_dim_length = Convert.ToDecimal(txtbreath.Text);
            if (string.IsNullOrEmpty(txtwidth.Text))
            {
                txtwidth.Text = "0";
            }
            _item.Mi_dim_width = Convert.ToDecimal(txtwidth.Text);
            if (string.IsNullOrEmpty(txthight.Text))
            {
                txthight.Text = "0";
            }
            _item.Mi_dim_height = Convert.ToDecimal(txthight.Text);
            _item.Mi_weight_uom = txtwuom.Text;
            if (string.IsNullOrEmpty(txtgross.Text))
            {
                txtgross.Text = "0";
            }
            _item.Mi_gross_weight = Convert.ToDecimal(txtgross.Text);
            if (string.IsNullOrEmpty(txtnet.Text))
            {
                txtnet.Text = "0";
            }
            _item.Mi_net_weight = Convert.ToDecimal(txtnet.Text);

            if (string.IsNullOrEmpty(txtAmount.Text))
            {
                txtAmount.Text = "0";
            }

            if (string.IsNullOrEmpty(txttrimLeft.Text))
            {
                txttrimLeft.Text = "0";
            }

            if (string.IsNullOrEmpty(txttrimRight.Text))
            {
                txttrimRight.Text = "0";
            }


            _item.Mi_image_path = txtImagePath.Text;
            //_item.Mi_is_ser1 = cmbSerilize.SelectedItem.ToString() == "YES" ? 1 : 0;
            //if (BaseCls.GlbUserComCode == "ABL" && BaseCls.GlbDefChannel == "ABT" && (BaseCls.GlbDefSubChannel == "HUG" || BaseCls.GlbDefSubChannel == "ABS" || BaseCls.GlbDefSubChannel == "SKE" || BaseCls.GlbDefSubChannel == "TFS"))    //kapila 17/2/2017
            //{
                if (cmbSerilize.SelectedItem.ToString() == "DECIMAL")
                    _item.Mi_is_ser1 = -1;
                else if (cmbSerilize.SelectedItem.ToString() == "YES")
                    _item.Mi_is_ser1 = 1;
                else
                    _item.Mi_is_ser1 = 0;
            //}
            //else
            //{
            //    _item.Mi_is_ser1 = cmbSerilize.SelectedItem.ToString() == "YES" ? 1 : 0;
            //}
            _item.Mi_is_ser2 = cmbChassis.SelectedIndex;// == "YES" ? true : false;
            //_item.Mi_is_ser3
            _item.Mi_warr = cmbWarranty.SelectedItem.ToString() == "YES" ? true : false;
            _item.Mi_warr_print = chkwarrprint.Checked;
            _item.Mi_hp_allow = cmbhpSalesAccept.SelectedItem.ToString() == "YES" ? true : false;
            _item.Mi_insu_allow = cmbIsRegister.SelectedItem.ToString() == "YES" ? true : false;
            _item.Mi_country_cd = txtCountry.Text;
            _item.Mi_purcom_cd = txtPurComp.Text;
            _item.Mi_itm_stus = cmbStatus.SelectedValue.ToString();
            _item.Mi_fgitm_cd = txtFgood.Text;
            _item.Mi_itmtot_cost = Convert.ToInt32(txtAmount.Text);
            _item.Mi_chg_tp = cmbPayType.SelectedItem.ToString();
            _item.Mi_is_scansub = cmbScanSub.SelectedItem.ToString() == "YES" ? true : false;
            _item.Mi_is_barcodetrim = chkTrim.Checked;
            _item.Mi_ltrim_val = Convert.ToInt32(txttrimLeft.Text);
            _item.Mi_rtrim_val = Convert.ToInt32(txttrimRight.Text);
            //_item.Mi_is_editshortdesc
            //_item.Mi_is_editlongdesc
            _item.Mi_ser_prefix = txtPrefix.Text;
            _item.Mi_refitm_cd = "N/A";
            _item.Mi_uom_warrperiodmain = "N/A";
            _item.Mi_uom_warrperiodsub1 = "N/A";
            _item.Mi_uom_warrperiodsub2 = "N/A";
            //_item.Mi_is_editser1
            //_item.Mi_is_editser2
            //_item.Mi_is_editser3
            //_item.Mi_std_cost
            //_item.Mi_std_price
            _item.Mi_act = cmbActive.SelectedItem.ToString() == "YES" ? true : false;
            _item.Mi_cre_by = BaseCls.GlbUserID;
            _item.Mi_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _item.Mi_mod_by = BaseCls.GlbUserID;
            _item.Mi_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _item.Mi_session_id = BaseCls.GlbUserSessionID;
            _item.Mi_anal1 = txtTaxStucture.Text;
            _item.Mi_size = txtSize.Text;
            //_item.Mi_anal3
            // _item.Mi_anal4 = txtTaxStucture.Text;
            //_item.Mi_anal5
            //_item.Mi_anal6
            //_item.Mi_is_subitm = chkIssub.Checked;
            _item.Mi_need_reg = chkProRegis.Checked;
            _item.Mi_need_insu = chkProInsurance.Checked;
            _item.Mi_need_freesev = chkFreeSer.Checked == true ? 1 : 0;
            //_item.Mi_comm_israte
            //_item.Mi_comm_val
            //_item.Mi_fac_base
            //_item.Mi_fac_val
            //_item.Mi_is_cond
            _item.Mi_packing_cd = txtPackCode.Text;
            _item.Mi_part_cd = txtPartNo.Text;
            _item.Mi_is_discont = chkDiscont.Checked == true ? 1 : 0;
            _item.Mi_is_sup_wara = chkMaintSupp.Checked == true ? 1 : 0;
            _item.MI_CHK_CUST = chkcust.Checked == true ? 1 : 0;
            _item.MI_IS_EXP_DT = chkIsExpired.Checked == true ? 1 : 0;
            _item.Mi_capacity = Convert.ToDecimal(txtCapacity.Text);
            _item.Mi_add_itm_des = chkAdditem.Checked;
            _item.Mi_edit_alt_ser = chkAlterSer.Checked;
            _item.Mi_ser_rq_cus = chkSerReq.Checked;
            _item.Mi_main_supp = chkMaintSupp.Checked;
            _item.Mi_app_itm_cond = chkAppCond.Checked;



        }

        # endregion

        private void txtMainCat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCat1.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {

            }
        }

        private void txtCat1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCat2.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {

            }
        }

        private void txtCat2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCat3.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {

            }
        }

        private void txtCat3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCat4.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {

            }
        }

        private void txtCat4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWaraUOM.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {

            }
        }

        private void cmbItemType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtTaxStucture.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {

            }
        }

        private void cmbTaxCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCapacity.Focus();
            }
        }

        private void txtCapacity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtColorExt.Focus();
            }
        }

        private void txtColorExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtColorInt.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {

            }
        }

        private void txtColorInt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbPayType.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {

            }

        }

        private void cmbPayType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPrefix.Focus();
            }
        }

        private void txtPrefix_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbStatus.Focus();
            }

        }

        private void cmbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbSerilize.Focus();
            }
        }

        private void cmbSerilize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbWarranty.Focus();
            }
        }

        private void cmbWarranty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbChassis.Focus();
            }
        }

        private void cmbChassis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbScanSub.Focus();
            }
        }

        private void cmbScanSub_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbhpSalesAccept.Focus();
            }

        }

        private void cmbhpSalesAccept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbIsRegister.Focus();
            }
        }

        private void cmbIsRegister_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPurComp.Focus();
            }
        }

        private void txtPurComp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ttxHsCode.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnSrhPurCom_Click(null, null);
            }
        }

        private void ttxHsCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCountry.Focus();
            }
        }

        private void txtCountry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnSrchCounty_Click(null, null);
            }
        }

        private void txttrimLeft_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txttrimRight.Focus();
            }
        }

        private void txttrimRight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFgood.Focus();
            }
        }

        private void txtFgood_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCostElement.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnSearchFG_Click(null, null);
            }
        }

        private void txtCostElement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAmount.Focus();
            }
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddstatus.Focus();
            }
        }

        private void txtCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRoLevel.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnSearchreCom_Click(null, null);
            }
        }

        private void txtRoLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRoQty.Focus();
            }
        }

        private void txtRoQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbClassification.Focus();
            }
        }

        private void cmbClassification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddReorder.Focus();
            }
        }

        private void txtDuration_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDuration_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                cmbItemType.Focus();
            }
        }

        private void cmbPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDuration.Focus();
            }
        }

        private void cmbItemType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txttrimRight_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbClassification_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddstatus_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtFgood.Text))
            {
                MessageBox.Show("Enter finish good", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtCostElement.Text))
            {
                MessageBox.Show("Enter cost elemet ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtAmount.Text))
            {
                MessageBox.Show("Enter cost amount ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_lstfg_cost != null)
            {
                mst_itm_fg_cost result = _lstfg_cost.Find(x => x.Ifc_fg_item_code == txtFgood.Text);
                if (result != null)
                {
                    MessageBox.Show("This finish good already exist", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


            }
            else
            {
                _lstfg_cost = new List<mst_itm_fg_cost>();
            }
            mst_itm_fg_cost _itm = new mst_itm_fg_cost();
            _itm.Ifc_item_code = txtItem.Text;
            _itm.Ifc_fg_item_code = txtFgood.Text;
            _itm.Ifc_cost_type = txtCostElement.Text;
            _itm.Ifc_cost_amount = Convert.ToDecimal(txtAmount.Text);
            _itm.Ifc_currency_code = "LKR";
            _itm.Ifc_create_by = BaseCls.GlbUserID;
            _itm.Ifc_last_modify_by = BaseCls.GlbUserID;
            _lstfg_cost.Add(_itm);

            gvStatus.DataSource = null;
            gvStatus.AutoGenerateColumns = false;
            gvStatus.DataSource = new List<mst_itm_fg_cost>();
            gvStatus.DataSource = _lstfg_cost;



            txtFgood.Text = "";
            txtCostElement.Text = "";
            txtAmount.Text = "";


        }

        private void btnAddReorder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCompany.Text))
            {
                MessageBox.Show("Enter company ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtRoQty.Text))
            {
                MessageBox.Show("Enter reorder Qty ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtRoLevel.Text))
            {
                MessageBox.Show("Enter reorder level ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_lstreorder != null)
            {
                mst_itm_com_reorder result = _lstreorder.Find(x => x.Icr_com_code == txtCompany.Text);
                if (result != null)
                {
                    MessageBox.Show("This company already exist", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }

            else
            {
                _lstreorder = new List<mst_itm_com_reorder>();
            }

            mst_itm_com_reorder _itm = new mst_itm_com_reorder();
            _itm.Icr_com_code = txtCompany.Text;
            _itm.Icr_itm_code = txtItem.Text;
            _itm.Icr_itm_sts = "GOD";
            _itm.Icr_re_order_qty = Convert.ToDecimal(txtRoQty.Text);
            _itm.Icr_re_order_lvl = Convert.ToDecimal(txtRoLevel.Text);
            _itm.Icr_class = cmbClassification.Text;

            _itm.Icr_created_by = BaseCls.GlbUserID;
            _itm.Icr_last_modify_by = BaseCls.GlbUserID;

            _lstreorder.Add(_itm);

            gvReorder.DataSource = null;
            gvReorder.AutoGenerateColumns = false;
            gvReorder.DataSource = new List<mst_itm_com_reorder>();
            gvReorder.DataSource = _lstreorder;

            txtCompany.Text = "";

            txtRoQty.Text = "";
            txtRoLevel.Text = "";
            cmbClassification.SelectedIndex = -1;

        }

        private void txtFgood_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnsearchComItem_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtItemCom.Text))
            {
                MessageBox.Show("Enter reorder level ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(cmbitemStatus.Text))
            {
                MessageBox.Show("Select item status ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(cmbFoc.Text))
            {
                MessageBox.Show("Select FOC allow or not ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(cmbAgecType.Text))
            {
                MessageBox.Show("Select agency type ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_lstcomItem != null)
            {
                MasterCompanyItem result = _lstcomItem.Find(x => x.Mci_com == txtItemCom.Text);
                if (result != null)
                {
                    MessageBox.Show("This company already exist", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            else { _lstcomItem = new List<MasterCompanyItem>(); }
            MasterCompanyItem _itm = new MasterCompanyItem();
            _itm.Mci_itm_cd = txtItem.Text;
            _itm.Mci_com = txtItemCom.Text;

            if (cmbitemStatus.SelectedItem.ToString() == "YES")
            {
                _itm.Mci_act = true;
            }
            else
            {
                _itm.Mci_act = false;
            }
            if (cmbFoc.SelectedItem.ToString() == "Allow")
            {
                _itm.Mci_isfoc = true;
            }
            else
            {
                _itm.Mci_isfoc = false;
            }

            _itm.Mci_age_type = cmbAgecType.SelectedItem.ToString();
            _itm.Mci_comDes = txtitemDes.Text;


            _lstcomItem.Add(_itm);

            gvComItem.DataSource = null;
            gvComItem.AutoGenerateColumns = false;
            gvComItem.DataSource = new List<MasterCompanyItem>();
            gvComItem.DataSource = _lstcomItem;



            txtItemCom.Text = "";
            cmbitemStatus.SelectedIndex = -1;
            cmbFoc.SelectedIndex = -1;
            cmbAgecType.SelectedIndex = -1;
            txtitemDes.Text = "";


        }

        private void btnsearchCustomer_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtCuscom.Text))
            {
                MessageBox.Show("Enter Customer company", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtCust.Text))
            {
                MessageBox.Show("Enter Customer ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(cmbCustStatus.Text))
            {
                MessageBox.Show("Select Status ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (_lstcusItem != null)
            {
                BusEntityItem result = _lstcusItem.Find(x => x.MBII_COM == txtCuscom.Text && x.MBII_CD == txtCust.Text);
                if (result != null)
                {
                    MessageBox.Show("This customer already exist", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                _lstcusItem = new List<BusEntityItem>();
            }
            BusEntityItem _itm = new BusEntityItem();
            _itm.MBII_ITM_CD = txtItem.Text;
            _itm.MBII_COM = txtCuscom.Text;
            _itm.MBII_CD = txtCust.Text;
            _itm.MBII_ACT = cmbCustStatus.SelectedItem.ToString() == "YES" ? 1 : 0;
            _itm.MBII_CUSTNAME = txtCustName.Text;
            _itm.MBII_CRE_BY = BaseCls.GlbUserID;
            _itm.MBII_MOD_BY = BaseCls.GlbUserID;
            _itm.MBII_TP = "C";
            _lstcusItem.Add(_itm);

            gvCustomer.DataSource = null;
            gvCustomer.AutoGenerateColumns = false;
            gvCustomer.DataSource = new List<BusEntityItem>();
            gvCustomer.DataSource = _lstcusItem;
            txtCustName.Text = "";
            txtCuscom.Text = "";

            txtCust.Text = "";
            cmbCustStatus.SelectedIndex = -1;

        }

        private void btnSearchgvSupplier_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSupCom.Text))
            {
                MessageBox.Show("Enter supplier company ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtSupp.Text))
            {
                MessageBox.Show("Enter supplier ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_lstsupItem != null)
            {
                BusEntityItem result = _lstsupItem.Find(x => x.MBII_CD == txtSupp.Text && x.MBII_COM == txtSupCom.Text);
                if (result != null)
                {
                    MessageBox.Show("This supplier already exist", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                _lstsupItem = new List<BusEntityItem>();
            }

            BusEntityItem _itm = new BusEntityItem();
            _itm.MBII_CD = txtSupp.Text;
            _itm.MBII_ITM_CD = txtItem.Text;
            _itm.MBII_COM = txtSupCom.Text;
            _itm.MBII_ACT = cmbsupStatus.SelectedItem.ToString() == "YES" ? 1 : 0;
            // _itm.Msi_curr_code = "LKR";
            //   _itm.Msi_price_quoted = 0;
            _itm.MBII_CUSTNAME = txtSupName.Text;
            _itm.MBII_TP = "S";
            _itm.MBII_CRE_BY = BaseCls.GlbUserID;
            _itm.MBII_MOD_BY = BaseCls.GlbUserID;
            _itm.MBII_WARR_PERI = Convert.ToInt32(cmbwarsPrd.SelectedValue.ToString());
            _itm.MBII_WARR_RMK = txtsuppwarrremrk.Text.Trim();
            _lstsupItem.Add(_itm);

            gvSupplier.DataSource = null;
            gvSupplier.AutoGenerateColumns = false;
            gvSupplier.DataSource = new List<BusEntityItem>();
            gvSupplier.DataSource = _lstsupItem;


            txtSupCom.Text = "";
            txtSupp.Text = "";
            txtSupName.Text = "";
            cmbsupStatus.SelectedIndex = -1;


        }

        private void btnsearchRedeemCom_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReCom.Text))
            {
                MessageBox.Show("Enter company ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(cmbReStatus.Text))
            {
                MessageBox.Show("Select Status ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_lstredCom != null)
            {
                mst_itm_redeem_com result = _lstredCom.Find(x => x.Red_com_code == txtReCom.Text);
                if (result != null)
                {
                    MessageBox.Show("This company already exist", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                _lstredCom = new List<mst_itm_redeem_com>();
            }

            mst_itm_redeem_com _itm = new mst_itm_redeem_com();
            _itm.Red_item_code = txtItem.Text;
            _itm.Red_com_code = txtReCom.Text;
            _itm.Red_com_des = txtReDes.Text;
            _itm.Red_active = cmbReStatus.SelectedItem.ToString() == "YES" ? 1 : 0;
            _itm.Red_create_by = BaseCls.GlbUserID;

            _lstredCom.Add(_itm);

            gvRedeemCom.DataSource = null;
            gvRedeemCom.AutoGenerateColumns = false;
            gvRedeemCom.DataSource = new List<mst_itm_redeem_com>();
            gvRedeemCom.DataSource = _lstredCom;


            txtReCom.Text = "";
            txtReDes.Text = "";
            cmbReStatus.SelectedIndex = -1;
        }

        private void btnSearchgvMRN_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMrnCom.Text))
            {
                MessageBox.Show("Select MRN Company ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(cmbmrnStatus.Text))
            {
                MessageBox.Show("Select Status ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }



            if (_lstmrn != null)
            {
                mst_itm_mrn_com result = _lstmrn.Find(x => x.Imc_com == txtMrnCom.Text);
                if (result != null)
                {
                    MessageBox.Show("This company already exist", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            else
            {
                _lstmrn = new List<mst_itm_mrn_com>();
            }

            mst_itm_mrn_com _itm = new mst_itm_mrn_com();
            _itm.Imc_itemcode = txtItem.Text;
            _itm.Imc_com = txtMrnCom.Text;
            _itm.Imc_comdes = txtMrndes.Text;
            _itm.Imc_active = cmbmrnStatus.SelectedItem.ToString() == "YES" ? 1 : 0;
            _itm.Imc_create_by = BaseCls.GlbUserID;
            _itm.Imc_modified_by = BaseCls.GlbUserID;
            _lstmrn.Add(_itm);

            gvMRN.DataSource = null;
            gvMRN.AutoGenerateColumns = false;
            gvMRN.DataSource = new List<mst_itm_mrn_com>();
            gvMRN.DataSource = _lstmrn;


            txtMrnCom.Text = "";
            txtMrndes.Text = "";
            cmbmrnStatus.SelectedIndex = -1;




        }

        private void btnAddRepalced_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtrepItem.Text))
            {
                MessageBox.Show("Select replaced item ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(cmbrepStatus.Text))
            {
                MessageBox.Show("Select status ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_lstreplace != null)
            {
                mst_itm_replace result = _lstreplace.Find(x => x.Rpl_replaceditem == txtrepItem.Text);
                if (result != null)
                {
                    MessageBox.Show("This item already exist", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                _lstreplace = new List<mst_itm_replace>();
            }

            mst_itm_replace _itm = new mst_itm_replace();
            _itm.Rpl_item = txtItem.Text;
            _itm.Rpl_replaceditem = txtrepItem.Text;
            _itm.Rpl_itemdes = txtrepDes.Text;
            _itm.Rpl_active = cmbrepStatus.SelectedItem.ToString() == "YES" ? 1 : 0;
            _itm.Rpl_cre_by = BaseCls.GlbUserID;
            _itm.Rpl_mod_by = BaseCls.GlbUserID;
            _lstreplace.Add(_itm);

            gvRepalced.DataSource = null;
            gvRepalced.AutoGenerateColumns = false;
            gvRepalced.DataSource = new List<mst_itm_replace>();
            gvRepalced.DataSource = _lstreplace;


            txtrepItem.Text = "";
            txtrepDes.Text = "";
            cmbrepStatus.SelectedIndex = -1;



        }

        private void btnAddKit_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtkitItem.Text))
            {
                MessageBox.Show("Select item components ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtkitcost.Text))
            {
                MessageBox.Show("Select item cost percentage ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(cmbKitItemType.Text))
            {
                MessageBox.Show("Select item type ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(cmbScan.Text))
            {
                MessageBox.Show("Select scan or not ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtunits.Text))
            {
                MessageBox.Show("Select units ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(cmbKitActive.Text))
            {
                MessageBox.Show("Select Status", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (_lstkit != null)
            {


                MasterItemComponent result = _lstkit.Find(x => x.Micp_comp_itm_cd == txtkitItem.Text);
                if (result != null)
                {
                    MessageBox.Show("This item components already exist", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                _lstkit = new List<MasterItemComponent>();
            }

            MasterItemComponent _itm = new MasterItemComponent();
            _itm.Micp_itm_cd = txtItem.Text;
            _itm.Micp_comp_itm_cd = txtkitItem.Text;
            _itm.Micp_cost_percentage = Convert.ToDecimal(txtkitcost.Text);
            _itm.Micp_itm_tp = cmbKitItemType.SelectedItem.ToString();
            _itm.Micp_must_scan = cmbScan.SelectedItem.ToString() == "YES" ? true : false;
            _itm.Micp_qty = Convert.ToInt32(txtunits.Text);
            _itm.Micp_act = cmbKitActive.SelectedItem.ToString() == "YES" ? true : false;
            _itm.Micp_cate = cmbkitCate.SelectedItem.ToString();

            _lstkit.Add(_itm);

            gvKit.DataSource = null;
            gvKit.AutoGenerateColumns = false;
            gvKit.DataSource = new List<MasterItemComponent>();
            gvKit.DataSource = _lstkit;

            txtkitItem.Text = "";

            txtkitcost.Text = "";
            cmbKitItemType.SelectedIndex = -1;
            cmbScan.SelectedIndex = -1;
            txtunits.Text = "";
            cmbKitActive.SelectedIndex = -1;



        }

        private void panel22_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddTaxClaim_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtclaim.Text))
            {
                MessageBox.Show("Enter claimable percentage", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtclaimcom.Text))
            {
                MessageBox.Show("Select company", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(cmbclaimcate.Text))
            {
                MessageBox.Show("Select tax category", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txttaxRate.Text))
            {
                MessageBox.Show("Select Rate", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_lsttaxClaim != null)
            {
                MasterItemTaxClaim result = _lsttaxClaim.Find(x => x.Mic_com == txtclaimcom.Text && x.Mic_tax_rt == Convert.ToDecimal(txttaxRate.Text));
                if (result != null)
                {
                    MessageBox.Show("This record already exist", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                _lsttaxClaim = new List<MasterItemTaxClaim>();
            }

            MasterItemTaxClaim _itm = new MasterItemTaxClaim();
            _itm.Mic_claim = Convert.ToDecimal(txtclaim.Text);
            _itm.Mic_com = txtclaimcom.Text;
            _itm.Mic_itm_cd = txtItem.Text;
            _itm.Mic_stus = true;
            _itm.Mic_tax_cd = cmbclaimcate.SelectedValue.ToString();
            _itm.Mic_tax_rt = Convert.ToDecimal(txttaxRate.Text);
            _itm.Mic_cre_by = BaseCls.GlbUserID;

            _lsttaxClaim.Add(_itm);

            gvTaxClaim.DataSource = null;
            gvTaxClaim.AutoGenerateColumns = false;
            gvTaxClaim.DataSource = new List<MasterItemTaxClaim>();
            gvTaxClaim.DataSource = _lsttaxClaim;

            txtclaim.Text = "";
            txtclaimcom.Text = "";
            cmbclaimcate.SelectedIndex = -1;
            txttaxRate.Text = "";


        }

        private void btnAddservice_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbserSts.Text))
            {
                MessageBox.Show("Select status", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(cmbserTerm.Text))
            {
                MessageBox.Show("Select Term", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (string.IsNullOrEmpty(txtserfrom.Text))
            {
                MessageBox.Show("Select From", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtserto.Text))
            {
                MessageBox.Show("Select To", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtseruom.Text))
            {
                MessageBox.Show("Select UOM", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_lstitmPrd != null)
            {
                mst_itm_sevpd result = _lstitmPrd.Find(x => x.Msp_itm_stus == cmbserSts.SelectedValue.ToString() && x.Msp_term == Convert.ToInt32(cmbserTerm.SelectedValue.ToString()));
                if (result != null)
                {
                    MessageBox.Show("This record already exist", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            else
            {
                _lstitmPrd = new List<mst_itm_sevpd>();
            }
            mst_itm_sevpd _itm = new mst_itm_sevpd();
            _itm.Msp_itm_stus = cmbserSts.SelectedValue.ToString();
            _itm.Msp_itm_cd = txtItem.Text;
            _itm.Msp_term = Convert.ToInt32(cmbserTerm.SelectedItem.ToString());
            _itm.Msp_pd_from = Convert.ToDecimal(txtserfrom.Text);
            _itm.Msp_pd_to = Convert.ToDecimal(txtserto.Text);
            _itm.Msp_pd_uom = txtseruom.Text;
            _itm.Msp_pdalt_from = Convert.ToDecimal(txtserdfrom.Text);
            _itm.Msp_pdalt_to = Convert.ToDecimal(txtserdto.Text);
            _itm.Msp_pdalt_uom = txtserduom.Text;
            _itm.Msp_isfree = cmbserisfree.SelectedItem.ToString() == "YES" ? 1 : 0;

            _lstitmPrd.Add(_itm);

            gvservice.DataSource = null;
            gvservice.AutoGenerateColumns = false;
            gvservice.DataSource = new List<mst_itm_sevpd>();
            gvservice.DataSource = _lstitmPrd;

            cmbserSts.SelectedIndex = -1;
            cmbserTerm.SelectedIndex = -1;
            txtserfrom.Text = "";
            txtserto.Text = "";
            txtseruom.Text = "";
            txtserdfrom.Text = "";
            txtserdto.Text = "";
            txtserduom.Text = "";
            cmbserisfree.SelectedIndex = -1;
        }

        private void btnAddWarranty_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbwStatus.Text))
            {
                MessageBox.Show("Select item status", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtWaraUOM.Text))
            {
                MessageBox.Show("Select UOM", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(cmbwPeriod.Text) && string.IsNullOrEmpty(cmbwarsPrd.Text))
            {
                MessageBox.Show("Enter warranty period", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_lstitmWrd != null)
            {

                MasterItemWarrantyPeriod result = _lstitmWrd.Find(x => x.Mwp_itm_stus == cmbwStatus.SelectedValue.ToString());
                if (result != null)
                {
                    MessageBox.Show("This record already exist", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                _lstitmWrd = new List<MasterItemWarrantyPeriod>();
            }

            MasterItemWarrantyPeriod _itm = new MasterItemWarrantyPeriod();
            _itm.Mwp_itm_cd = txtItem.Text;
            _itm.Mwp_itm_stus = cmbwStatus.SelectedValue.ToString();
            _itm.Mwp_val = Convert.ToInt32(cmbwPeriod.SelectedValue.ToString());
            _itm.Mwp_sup_warranty_prd = Convert.ToInt32(cmbwarsPrd.SelectedValue.ToString());
            _itm.Mwp_warr_tp = txtWaraUOM.Text;
            //   _itm.Mwp_sup_warr_prd_alt = Convert.ToInt32(cmbwarsdur.Text);
            _itm.Mwp_rmk = txtWarRem.Text;
            _itm.Mwp_effect_dt = dtpEffectiveDate.Value.Date;

            _itm.Mwp_sup_wara_rem = txtwarsRem.Text;
            _itm.Mwp_cre_by = BaseCls.GlbUserID;
            _itm.Mwp_mod_by = BaseCls.GlbUserID;

            _lstitmWrd.Add(_itm);

            gvWarranty.DataSource = null;
            gvWarranty.AutoGenerateColumns = false;
            gvWarranty.DataSource = new List<MasterItemWarrantyPeriod>();
            gvWarranty.DataSource = _lstitmWrd;


            cmbwStatus.SelectedIndex = -1;
            cmbwarsPrd.SelectedIndex = -1;
            cmbwPeriod.SelectedIndex = -1;
            txtWarRem.Text = "";
            txtWaraUOM.Text = "";
            txtwarsRem.Text = "";
        }

        private void btnAddPcwarranty_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtpcCom.Text))
            {
                MessageBox.Show("Enter company", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtpc.Text))
            {
                MessageBox.Show("Enter pc", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(cmbPcstatus.Text))
            {
                MessageBox.Show("Enter item status ", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(cmbpcPrd.Text))
            {
                MessageBox.Show("Enter warranty period", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtppcfrom.Value.Date > dtppcto.Value.Date)
            {
                MessageBox.Show("From date must be lesst than to date", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_lstpcWrd != null)
            {
                mst_itm_pc_warr result = _lstpcWrd.Find(x => x.Pc_com == txtpcCom.Text && x.Pc_code == txtpc.Text && x.Pc_item_stus == cmbPcstatus.SelectedValue.ToString());
                if (result != null)
                {
                    MessageBox.Show("This record already exist", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                _lstpcWrd = new List<mst_itm_pc_warr>();
            }

            mst_itm_pc_warr _itm = new mst_itm_pc_warr();
            _itm.Pc_com = txtpcCom.Text;
            _itm.Pc_code = txtpc.Text;
            _itm.Pc_item_code = txtItem.Text;
            _itm.Pc_item_stus = cmbPcstatus.SelectedValue.ToString();
            _itm.Pc_wara_period = Convert.ToInt32(cmbpcPrd.SelectedValue.ToString());
            _itm.Pc_wara_rmk = txtpcRem.Text;
            _itm.Pc_valid_from = dtppcfrom.Value.Date;

            _itm.Pc_valid_to = dtppcto.Value.Date;
            _itm.Pc_create_by = BaseCls.GlbUserID;


            _lstpcWrd.Add(_itm);

            gvPcwarranty.DataSource = null;
            gvPcwarranty.AutoGenerateColumns = false;
            gvPcwarranty.DataSource = new List<mst_itm_pc_warr>();
            gvPcwarranty.DataSource = _lstpcWrd;

            txtpcCom.Text = "";
            txtpc.Text = "";
            cmbPcstatus.SelectedIndex = -1;
            cmbpcPrd.SelectedIndex = -1;
            txtpcRem.Text = "";





        }

        private void btnAddcannelWara_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtChaCom.Text))
            {
                MessageBox.Show("Enter Company", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtChan.Text))
            {
                MessageBox.Show("Enter channel", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(cmbchanStatus.Text))
            {
                MessageBox.Show("Enter item status", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtChanPrd.Text))
            {
                MessageBox.Show("Enter warranty period", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_lstchannelWrd != null)
            {
                mst_itm_channlwara result = _lstchannelWrd.Find(x => x.Cw_com == txtChaCom.Text && x.Cw_channel == txtChan.Text && x.Cw_item_status == cmbchanStatus.SelectedValue.ToString());
                if (result != null)
                {
                    MessageBox.Show("This record already exist", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            { _lstchannelWrd = new List<mst_itm_channlwara>(); }


            mst_itm_channlwara _itm = new mst_itm_channlwara();

            _itm.Cw_item_code = txtItem.Text;
            _itm.Cw_channel = txtChan.Text;
            _itm.Cw_item_status = cmbchanStatus.SelectedValue.ToString();
            _itm.Cw_warranty_prd = Convert.ToInt32(txtChanPrd.SelectedValue.ToString());
            _itm.Cw_active = 1;
            _itm.Cw_com = txtChaCom.Text;
            _itm.Cw_create_by = BaseCls.GlbUserID;
            _itm.Cw_modify_by = BaseCls.GlbUserID;
            _lstchannelWrd.Add(_itm);

            gvcannelWara.DataSource = null;
            gvcannelWara.AutoGenerateColumns = false;
            gvcannelWara.DataSource = new List<mst_itm_channlwara>();
            gvcannelWara.DataSource = _lstchannelWrd;

            txtChan.Text = "";
            cmbchanStatus.SelectedIndex = -1;
            txtChanPrd.Text = "";
            cmbwarsdur.SelectedIndex = -1;

        }

        private void btnAddCont_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(cmbContType.Text))
            {
                MessageBox.Show("Select container Type", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtContUnits.Text))
            {
                MessageBox.Show("Enter no of units", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_lstcont != null)
            {
                mst_itm_container result = _lstcont.Find(x => x.Ic_container_type_code == cmbContType.SelectedValue.ToString());
                if (result != null)
                {
                    MessageBox.Show("This record already exist", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }

            else
            {
                _lstcont = new List<mst_itm_container>();
            }
            mst_itm_container _itm = new mst_itm_container();
            _itm.Ic_item_code = txtItem.Text;
            _itm.Ic_container_type_code = cmbContType.SelectedValue.ToString();
            _itm.Ic_no_of_unit_per_one_item = Convert.ToInt32(txtContUnits.Text);
            _itm.Ic_create_by = BaseCls.GlbUserID;
            _itm.Ic_last_modify_by = BaseCls.GlbUserID;
            _lstcont.Add(_itm);


            gvCont.DataSource = null;
            gvCont.AutoGenerateColumns = false;
            gvCont.DataSource = new List<mst_itm_container>();
            gvCont.DataSource = _lstcont;


            cmbContType.SelectedIndex = -1;
            txtContUnits.Text = "";


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to clear?", "Item Master", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Clear();
            }

        }

        void BindCombo()
        {
            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            if (string.IsNullOrEmpty(_masterComp.Mc_anal23) == false)
            {
                //if (_masterComp.Mc_anal23 == "Y")
                //if (BaseCls.GlbUserComCode == "ABL" && (BaseCls.GlbDefChannel == "ABT" || BaseCls.GlbDefChannel == "SKE" || BaseCls.GlbDefChannel == "TFS"))    //kapila 17/2/2017
                if (BaseCls.GlbUserComCode == "ABL" && BaseCls.GlbDefChannel == "ABT" && (BaseCls.GlbDefSubChannel == "ABS" || BaseCls.GlbDefSubChannel == "SKE" || BaseCls.GlbDefSubChannel == "TFS"))    //kapila 17/2/2017
                {
                    _isAutoCode = true;
                }
            }

            DataTable param = new DataTable();
            DataRow dr;
            param.Clear();

            param.Columns.Add("code", typeof(string));
            param.Columns.Add("des", typeof(string));


            dr = param.NewRow();
            dr["code"] = "F";
            dr["des"] = "Finish Good";
            param.Rows.Add(dr);
            dr = param.NewRow();
            dr["code"] = "R";
            dr["des"] = "Row Material";
            param.Rows.Add(dr);


            cmbStatus.DataSource = param;
            cmbStatus.DisplayMember = "des";
            cmbStatus.ValueMember = "code";
            cmbStatus.SelectedIndex = -1;


            DataTable param2 = new DataTable();
            DataRow dr2;

            param2.Columns.Add("code", typeof(string));
            param2.Columns.Add("des", typeof(string));
            param2.Clear();

            dr2 = param2.NewRow();
            dr2["code"] = "VAT_C";
            dr2["des"] = "Tax Claimable";
            param2.Rows.Add(dr2);

            dr2 = param2.NewRow();
            dr2["code"] = "VAT_UC";
            dr2["des"] = "Tax Unclaimable";
            param2.Rows.Add(dr2);


            cmbclaimcate.DataSource = param2;
            cmbclaimcate.DisplayMember = "des";
            cmbclaimcate.ValueMember = "code";
            cmbclaimcate.SelectedIndex = -1;







            DataTable param3 = new DataTable();
            DataRow dr3;

            param3.Columns.Add("code", typeof(string));
            param3.Columns.Add("des", typeof(string));
            param3.Clear();

            dr3 = param3.NewRow();
            dr3["code"] = "A";
            dr3["des"] = "Allow";
            param3.Rows.Add(dr3);

            dr3 = param3.NewRow();
            dr3["code"] = "N";
            dr3["des"] = "Not Allow";
            param3.Rows.Add(dr3);


            cmbFoc.DataSource = param3;
            cmbFoc.DisplayMember = "des";
            cmbFoc.ValueMember = "code";
            cmbFoc.SelectedIndex = -1;





            DataTable dtcnt = CHNLSVC.General.GetContainerType();
            if (dtcnt != null && dtcnt.Rows.Count > 0)
            {
                cmbContType.DataSource = dtcnt;
                cmbContType.DisplayMember = "MCT_DESC";
                cmbContType.ValueMember = "MCT_TP";
                cmbContType.SelectedIndex = -1;


            }
            DataTable dtwar = CHNLSVC.General.GetWarrantyPeriod();
            if (dtwar != null && dtwar.Rows.Count > 0)
            {
                cmbwarsPrd.DataSource = dtwar;
                cmbwarsPrd.DisplayMember = "wp_des";
                cmbwarsPrd.ValueMember = "wp_period";
                cmbwarsPrd.SelectedIndex = -1;

                cmbwPeriod.DataSource = dtwar;
                cmbwPeriod.DisplayMember = "wp_des";
                cmbwPeriod.ValueMember = "wp_period";
                cmbwPeriod.SelectedIndex = -1;

                cmbwdur.DataSource = dtwar;
                cmbwdur.DisplayMember = "wp_des_al";
                cmbwdur.ValueMember = "wp_warr_prd_alt";
                cmbwdur.SelectedIndex = -1;

                cmbpcPrd.DataSource = dtwar;
                cmbpcPrd.DisplayMember = "wp_des";
                cmbpcPrd.ValueMember = "wp_period";
                cmbpcPrd.SelectedIndex = -1;

                txtChanPrd.DataSource = dtwar;
                txtChanPrd.DisplayMember = "wp_des";
                txtChanPrd.ValueMember = "wp_period";
                txtChanPrd.SelectedIndex = -1;
                //add by tharanga 2017/11/13
                cmbsupwarr.DataSource = dtwar;
                cmbsupwarr.DisplayMember = "wp_des";
                cmbsupwarr.ValueMember = "wp_period";
                cmbsupwarr.SelectedIndex = -1;

                loadDefault();

            }

            DataTable dtpl = CHNLSVC.General.GetItemTpAll();
            if (dtpl != null && dtpl.Rows.Count > 0)
            {
                cmbItemType.DataSource = dtpl;
                cmbItemType.DisplayMember = "mstp_desc";
                cmbItemType.ValueMember = "mstp_cd";
                cmbItemType.SelectedIndex = 0;

            }

            DataTable _status = CHNLSVC.Inventory.GetItemStatusMaster("ALL", "ALL");
            if (_status != null && _status.Rows.Count > 0)
            {
                cmbwStatus.DataSource = _status;
                cmbwStatus.DisplayMember = "MIS_DESC";
                cmbwStatus.ValueMember = "MIS_CD";
                cmbwStatus.SelectedIndex = -1;


                cmbPcstatus.DataSource = _status;
                cmbPcstatus.DisplayMember = "MIS_DESC";
                cmbPcstatus.ValueMember = "MIS_CD";
                cmbPcstatus.SelectedIndex = -1;

                cmbchanStatus.DataSource = _status;
                cmbchanStatus.DisplayMember = "MIS_DESC";
                cmbchanStatus.ValueMember = "MIS_CD";
                cmbchanStatus.SelectedIndex = -1;


                cmbchanStatus.DataSource = _status;
                cmbchanStatus.DisplayMember = "MIS_DESC";
                cmbchanStatus.ValueMember = "MIS_CD";
                cmbchanStatus.SelectedIndex = -1;


                cmbserSts.DataSource = _status;
                cmbserSts.DisplayMember = "MIS_DESC";
                cmbserSts.ValueMember = "MIS_CD";
                cmbserSts.SelectedIndex = -1;




            }


        }

        void Clear()
        {
            //txtWaraUOM.Text = "";
            //txtItem.Text = "";
            //txtSdes.Text = "";
            //txtLdes.Text = "";
            //txtModel.Text = "";
            //txtBrand.Text = "";
            //txtPartNo.Text = "";
            //txtPackCode.Text = "";
            //txtUOM.Text = "";
            //txtMainCat.Text = "";
            //txtCat1.Text = "";
            //txtCat2.Text = "";
            //txtCat3.Text = "";
            //txtCat4.Text = "";
            //cmbItemType.Text = "";

            //txtCapacity.Text = "";
            //txtColorExt.Text = "";
            //txtColorInt.Text = "";
            //cmbPayType.Text = "";
            //txtPrefix.Text = "";
            //cmbStatus.Text = "";
            //cmbSerilize.Text = "";
            //cmbWarranty.Text = "";
            //cmbChassis.Text = "";
            //cmbScanSub.Text = "";
            //cmbhpSalesAccept.Text = "";
            //cmbIsRegister.Text = "";
            //txtPurComp.Text = "";
            //ttxHsCode.Text = "";
            //txtCountry.Text = "";




            //_lstfg_cost = new List<mst_itm_fg_cost>();
            //gvStatus.DataSource = null;
            //gvStatus.AutoGenerateColumns = false;
            //gvStatus.DataSource = new List<mst_itm_fg_cost>();
            //gvStatus.DataSource = _lstfg_cost;


            //_lstreorder = new List<mst_itm_com_reorder>();
            //gvReorder.DataSource = null;
            //gvReorder.AutoGenerateColumns = false;
            //gvReorder.DataSource = new List<mst_itm_com_reorder>();
            //gvReorder.DataSource = _lstreorder;

            //_lstcomItem = new List<mst_com_itm>();

            //gvComItem.DataSource = null;
            //gvComItem.AutoGenerateColumns = false;
            //gvComItem.DataSource = new List<mst_com_itm>();
            //gvComItem.DataSource = _lstcomItem;

            //_lstcusItem = new List<BusEntityItem>();

            //gvCustomer.DataSource = null;
            //gvCustomer.AutoGenerateColumns = false;
            //gvCustomer.DataSource = new List<BusEntityItem>();
            //gvCustomer.DataSource = _lstcusItem;

            //_lstsupItem = new List<BusEntityItem>();
            //gvSupplier.DataSource = null;
            //gvSupplier.AutoGenerateColumns = false;
            //gvSupplier.DataSource = new List<BusEntityItem>();
            //gvSupplier.DataSource = _lstsupItem;

            //_lstredCom = new List<mst_itm_redeem_com>();
            //gvRedeemCom.DataSource = null;
            //gvRedeemCom.AutoGenerateColumns = false;
            //gvRedeemCom.DataSource = new List<mst_itm_redeem_com>();
            //gvRedeemCom.DataSource = _lstredCom;

            //_lstmrn = new List<mst_itm_mrn_com>();

            //gvMRN.DataSource = null;
            //gvMRN.AutoGenerateColumns = false;
            //gvMRN.DataSource = new List<mst_itm_mrn_com>();
            //gvMRN.DataSource = _lstmrn;

            //_lstreplace = new List<mst_itm_replace>();
            //gvRepalced.DataSource = null;
            //gvRepalced.AutoGenerateColumns = false;
            //gvRepalced.DataSource = new List<mst_itm_replace>();
            //gvRepalced.DataSource = _lstreplace;

            //_lstkit = new List<MasterItemComponent>();

            //gvKit.DataSource = null;
            //gvKit.AutoGenerateColumns = false;
            //gvKit.DataSource = new List<MasterItemComponent>();
            //gvKit.DataSource = _lstkit;

            //_lsttaxClaim = new List<MasterItemTaxClaim>();
            //gvTaxClaim.DataSource = null;
            //gvTaxClaim.AutoGenerateColumns = false;
            //gvTaxClaim.DataSource = new List<MasterItemTaxClaim>();
            //gvTaxClaim.DataSource = _lsttaxClaim;

            //_lstitmPrd = new List<mst_itm_sevpd>();
            //gvservice.DataSource = null;
            //gvservice.AutoGenerateColumns = false;
            //gvservice.DataSource = new List<mst_itm_sevpd>();
            //gvservice.DataSource = _lstitmPrd;


            //_lstitmWrd = new List<MasterItemWarrantyPeriod>();
            //gvWarranty.DataSource = null;
            //gvWarranty.AutoGenerateColumns = false;
            //gvWarranty.DataSource = new List<MasterItemWarrantyPeriod>();
            //gvWarranty.DataSource = _lstitmWrd;

            //_lstpcWrd = new List<mst_itm_pc_warr>();
            //gvPcwarranty.DataSource = null;
            //gvPcwarranty.AutoGenerateColumns = false;
            //gvPcwarranty.DataSource = new List<mst_itm_pc_warr>();
            //gvPcwarranty.DataSource = _lstpcWrd;


            //_lstchannelWrd = new List<mst_itm_channlwara>();
            //gvcannelWara.DataSource = null;
            //gvcannelWara.AutoGenerateColumns = false;
            //gvcannelWara.DataSource = new List<mst_itm_channlwara>();
            //gvcannelWara.DataSource = _lstchannelWrd;


            //_lstcont = new List<mst_itm_container>();
            //gvCont.DataSource = null;
            //gvCont.AutoGenerateColumns = false;
            //gvCont.DataSource = new List<mst_itm_container>();
            //gvCont.DataSource = _lstcont;





            //_lstfg_cost = new List<mst_itm_fg_cost>();


            //_lstreorder = new List<mst_itm_com_reorder>();


            //_lstcomItem = new List<mst_com_itm>();


            //_lstcusItem = new List<BusEntityItem>();



            //_lstsupItem = new List<BusEntityItem>();


            //_lstredCom = new List<mst_itm_redeem_com>();


            //_lstmrn = new List<mst_itm_mrn_com>();



            //_lstreplace = new List<mst_itm_replace>();


            //_lstkit = new List<MasterItemComponent>();



            //_lsttaxClaim = new List<MasterItemTaxClaim>();



            //_lstitmPrd = new List<mst_itm_sevpd>();



            //_lstitmWrd = new List<MasterItemWarrantyPeriod>();


            //_lstpcWrd = new List<mst_itm_pc_warr>();



            //_lstchannelWrd = new List<mst_itm_channlwara>();



            //_lstcont = new List<mst_itm_container>();

            ItemProfile _x = new ItemProfile();
            _x.MdiParent = this.MdiParent;
            _x.Location = this.Location;
            _x.GlbModuleName = this.GlbModuleName;
            _x.Show();
            this.Close();



            BindCombo();


            loadDefault();










        }

        private void btn_SrchCode_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtItem;
            _CommonSearch.ShowDialog();
            txtItem.Select();
        }


        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.TaxrateCodes:
                    {
                        paramsText.Append(txtTaxCode.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterItem:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat1:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat1.ToString() + seperator + "" + seperator + "CAT_Main" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat2:
                    {

                        paramsText.Append(txtMainCat.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat2.ToString() + seperator + "" + seperator + "CAT_Sub1" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat3:
                    {
                        paramsText.Append(txtMainCat.Text + seperator + txtCat1.Text + seperator + CommonUIDefiniton.SearchUserControlType.masterCat3.ToString() + seperator + "" + seperator + "CAT_Sub2" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat4:
                    {
                        paramsText.Append(txtMainCat.Text + seperator + txtCat1.Text + seperator + txtCat2.Text + seperator + CommonUIDefiniton.SearchUserControlType.masterCat4.ToString() + seperator + "CAT_Sub3" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat5:
                    {
                        paramsText.Append(txtMainCat.Text + seperator + txtCat1.Text + seperator + txtCat2.Text + seperator + txtCat3.Text + seperator + "CAT_Sub4" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TaxCode:
                    {

                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemBrand:
                    {

                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ModelMaster:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(txtCuscom.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterUOM:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterColor:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterTax:
                    {
                        paramsText.Append("");
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append(txtChaCom.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + null + seperator + null + seperator + null + seperator + null + seperator + null + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btn_SrchModel_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
            DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(_CommonSearch.SearchParams, null, null);

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtModel; //txtBox;
            _CommonSearch.ShowDialog();
            txtModel.Focus();
        }

        private void btn_SrchBrand_Click(object sender, EventArgs e)
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

        private void btn_Srch_mainCat_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtMainCat;
            _CommonSearch.txtSearchbyword.Text = txtMainCat.Text;
            _CommonSearch.ShowDialog();
            txtMainCat.Focus();
        }

        private void btn_Srch_cat1_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCat1;
            _CommonSearch.txtSearchbyword.Text = txtCat1.Text;
            _CommonSearch.ShowDialog();
            txtCat1.Focus();
        }

        private void btn_Srch_cat2_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCat2;
            _CommonSearch.txtSearchbyword.Text = txtCat2.Text;
            _CommonSearch.ShowDialog();
            txtCat2.Focus();
        }

        private void btn_Srch_cat3_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCat3;
            _CommonSearch.txtSearchbyword.Text = txtCat3.Text;
            _CommonSearch.ShowDialog();
            txtCat3.Focus();
        }

        private void btn_Srch_cat4_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat5);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCat4;
            _CommonSearch.txtSearchbyword.Text = txtCat4.Text;
            _CommonSearch.ShowDialog();
            txtCat4.Focus();
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

        private void btnSearchitemCom_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCom;
                _CommonSearch.ShowDialog();

                txtItemCom.Focus();
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

        private void btnSearchCusitem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCuscom;
                _CommonSearch.ShowDialog();

                txtCuscom.Focus();
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

        private void btnSearchSupp_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSupp;
                _CommonSearch.ShowDialog();
                txtSupp.Select();


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

        private void btnSearchRedeem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtReCom;
                _CommonSearch.ShowDialog();

                txtReCom.Focus();
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

        private void btnSearchmrn_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMrnCom;
                _CommonSearch.ShowDialog();

                txtMrnCom.Focus();
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

        private void txtSupp_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSupp_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSupp.Text))
                {
                    if (!CHNLSVC.Inventory.IsValidSupplier(BaseCls.GlbUserComCode, txtSupp.Text, 1, "S"))
                    {
                        MessageBox.Show("Invalid supplier code.", "Supplier Warranty Claim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSupp.Text = "";
                        txtSupp.Focus();
                        return;
                    }
                    else
                    {
                        MasterBusinessEntity _supDet = new MasterBusinessEntity();
                        _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtSupp.Text, null, null, "S");

                        if (_supDet.Mbe_cd == null)
                        {
                            MessageBox.Show("Invalid supplier code.", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSupp.Text = "";
                            txtSupName.Text = "";

                            return;
                        }
                        else
                        {
                            txtSupName.Text = _supDet.Mbe_name;

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

        private void btnCusSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtCust;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtCust.Select();
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

        private void txtCust_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCust.Text))
            {
                MasterBusinessEntity _masterBusinessCompany = null;
                _masterBusinessCompany = new MasterBusinessEntity();
                if (!string.IsNullOrEmpty(txtCust.Text))
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCust.Text, null, null, null, null, txtCuscom.Text);

                if (_masterBusinessCompany.Mbe_cd != null)
                {
                    if (_masterBusinessCompany.Mbe_act == false)
                    {
                        MessageBox.Show("This customer already inactive. Please contact accounts dept", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return;
                    }

                    else
                    {
                        txtCustName.Text = _masterBusinessCompany.Mbe_name;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Customer", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
            }











        }

        private void txtCompany_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCust_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtItemCom_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtItemCom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtItemCom.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtItemCom.Text.Trim());
                    if (com == null)
                    {
                        MessageBox.Show("Invalid Company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        txtitemDes.Text = com.Mc_desc;
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

        private void txtCuscom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCuscom.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtCuscom.Text.Trim());
                    if (com == null)
                    {
                        MessageBox.Show("Invalid Company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void txtReCom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtReCom.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtReCom.Text.Trim());
                    if (com == null)
                    {
                        MessageBox.Show("Invalid Company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        txtReDes.Text = com.Mc_desc;
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

        private void txtMrnCom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtMrnCom.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtMrnCom.Text.Trim());
                    if (com == null)
                    {
                        MessageBox.Show("Invalid Company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        txtMrndes.Text = com.Mc_desc;
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

        private void txtpcCom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtpcCom.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtpcCom.Text.Trim());
                    if (com == null)
                    {
                        MessageBox.Show("Invalid Company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void txtChanCom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtChaCom.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtChaCom.Text.Trim());
                    if (com == null)
                    {
                        MessageBox.Show("Invalid Company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnSearchpcWara_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtpcCom;
                _CommonSearch.ShowDialog();

                txtpcCom.Focus();
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

        private void tnChacom_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChaCom;
                _CommonSearch.ShowDialog();

                txtChaCom.Focus();
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

        private void txtModel_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearchFG_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFgood;
                _CommonSearch.ShowDialog();
                txtFgood.Select();








            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSerchRep_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.obj_TragetTextBox = txtrepItem;
                _CommonSearch.ShowDialog();
                txtrepItem.Select();
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Supplier Warranty Claim", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchKit_Click(object sender, EventArgs e)
        {
            try
            {
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                //_CommonSearch.ReturnIndex = 1;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                //DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(_CommonSearch.SearchParams, null, null);

                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = txtkitItem;
                //_CommonSearch.ShowDialog();
                //txtkitItem.Select();

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtkitItem;
                _CommonSearch.ShowDialog();
                txtkitItem.Select();
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Supplier Warranty Claim", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnsrhBaseUOM_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtUOM;
            _CommonSearch.txtSearchbyword.Text = txtUOM.Text;
            _CommonSearch.ShowDialog();
            txtUOM.Focus();
        }

        private void btnshcextcolor_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterColor);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtColorExt;
            _CommonSearch.txtSearchbyword.Text = txtColorExt.Text;
            _CommonSearch.ShowDialog();
            txtColorExt.Focus();
        }

        private void btnsrhintcolor_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterColor);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtColorInt;
            _CommonSearch.txtSearchbyword.Text = txtColorInt.Text;
            _CommonSearch.ShowDialog();
            txtColorInt.Focus();
        }

        private void btnsrhdimenuom_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtduom;
            _CommonSearch.txtSearchbyword.Text = txtduom.Text;
            _CommonSearch.ShowDialog();
            txtduom.Focus();
        }

        private void btnSrhWeightUOM_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtwuom;
            _CommonSearch.txtSearchbyword.Text = txtwuom.Text;
            _CommonSearch.ShowDialog();
            txtwuom.Focus();
        }

        void Load_items()
        {
            MasterItem _item = new MasterItem();
            _item = CHNLSVC.General.GetItemMaster(txtItem.Text);
            if (_item != null)
            {
                txtTaxItem.Text = txtItem.Text;
                txtSdes.Text = _item.Mi_shortdesc;
                txtLdes.Text = _item.Mi_longdesc;
                txtMainCat.Text = _item.Mi_cate_1;
                txtCat1.Text = _item.Mi_cate_2;
                txtCat2.Text = _item.Mi_cate_3;
                txtCat3.Text = _item.Mi_cate_4;
                txtCat4.Text = _item.Mi_cate_5;
                txtBrand.Text = _item.Mi_brand;
                txtModel.Text = _item.Mi_model;
                txtPartNo.Text = _item.Mi_part_no;
                txtColorInt.Text = _item.Mi_color_int;
                chkColorInt.Checked = _item.Mi_is_reqcolorint;
                txtColorExt.Text = _item.Mi_color_ext;
                chkColorExt.Checked = _item.Mi_is_reqcolorext;
                cmbItemType.SelectedValue = _item.Mi_itm_tp;
                chkStcokMain.Checked = _item.Mi_is_stockmaintain == "1" ? true : false;
                ttxHsCode.Text = _item.Mi_hs_cd;
                txtUOM.Text = _item.Mi_itm_uom;
                txtduom.Text = _item.Mi_dim_uom;
                txtbreath.Text = Convert.ToString(_item.Mi_dim_length);
                txtwidth.Text = Convert.ToString(_item.Mi_dim_width);
                txthight.Text = Convert.ToString(_item.Mi_dim_height);
                txtwuom.Text = _item.Mi_weight_uom;
                txtgross.Text = Convert.ToString(_item.Mi_gross_weight);
                txtnet.Text = Convert.ToString(_item.Mi_net_weight);
                txtImagePath.Text = _item.Mi_image_path;
                cmbSerilize.SelectedItem = _item.Mi_is_ser1 == 1 ? "YES" : "NO";
                cmbChassis.SelectedIndex = _item.Mi_is_ser2;///== true ? "YES" : "NO";
                if (_item.Mi_is_ser1 == -1)
                {
                    cmbSerilize.SelectedItem = _item.Mi_is_ser1 == -1 ? "DECIMAL" : "NO";
                } 
                //_item.Mi_is_ser3
                cmbWarranty.SelectedItem = _item.Mi_warr == true ? "YES" : "NO";
                chkwarrprint.Checked = _item.Mi_warr_print;
                cmbhpSalesAccept.SelectedItem = _item.Mi_hp_allow == true ? "YES" : "NO";
                cmbIsRegister.SelectedItem = _item.Mi_insu_allow == true ? "YES" : "NO";
                txtCountry.Text = _item.Mi_country_cd;
                txtPurComp.Text = _item.Mi_purcom_cd;
                cmbStatus.SelectedValue = _item.Mi_itm_stus;
                txtFgood.Text = _item.Mi_fgitm_cd;
                txtAmount.Text = Convert.ToString(_item.Mi_itmtot_cost);
                cmbPayType.SelectedItem = _item.Mi_chg_tp;
                cmbScanSub.SelectedItem = _item.Mi_is_scansub == true ? "YES" : "NO";
                chkTrim.Checked = _item.Mi_is_barcodetrim;
                txttrimLeft.Text = Convert.ToString(_item.Mi_ltrim_val);
                txttrimRight.Text = Convert.ToString(_item.Mi_rtrim_val);
                //_item.Mi_is_editshortdesc
                //_item.Mi_is_editlongdesc
                txtPrefix.Text = _item.Mi_ser_prefix;
                txtTaxStucture.Text = _item.Mi_anal1;
                txtSize.Text = _item.Mi_size;
                //_item.Mi_uom_warrperiodmain
                //_item.Mi_uom_warrperiodsub1
                //_item.Mi_uom_warrperiodsub2
                //_item.Mi_is_editser1
                //_item.Mi_is_editser2
                //_item.Mi_is_editser3
                //_item.Mi_std_cost
                //_item.Mi_std_price
                cmbActive.SelectedItem = _item.Mi_act == true ? "YES" : "NO";

                //_item.Mi_session_id
                //_item.Mi_anal1
                //_item.Mi_anal2
                //_item.Mi_anal3
                //_item.Mi_anal4
                //_item.Mi_anal5
                //_item.Mi_anal6
                //_item.Mi_is_subitm = chkIssub.Checked;
                chkProRegis.Checked = _item.Mi_need_reg;
                chkProInsurance.Checked = _item.Mi_need_insu;
                chkFreeSer.Checked = _item.Mi_need_freesev == 1 ? true : false;
                //_item.Mi_comm_israte
                //_item.Mi_comm_val
                //_item.Mi_fac_base
                //_item.Mi_fac_val
                //_item.Mi_is_cond
                txtPackCode.Text = _item.Mi_packing_cd;
                //  txtPartNo.Text=   _item.Mi_part_cd ;
                chkDiscont.Checked = _item.Mi_is_discont == 1 ? true : false;
                chkMaintSupp.Checked = _item.Mi_is_sup_wara == 1 ? true : false; ;
                chkcust.Checked = _item.MI_CHK_CUST == 1 ? true : false;

                txtCapacity.Text = Convert.ToString(_item.Mi_capacity);
                //if(Convert.ToDateTime(_item.Mi_is_exp_dt).Date !=Convert.ToDateTime("01/Jan/0001 12:00:00 AM").Date )
                //{
                //    dtExpDate.Value = Convert.ToDateTime(_item.Mi_is_exp_dt).Date;
                //}


                chkIsExpired.Checked = _item.MI_IS_EXP_DT == 1 ? true : false; ;

                chkAdditem.Checked = _item.Mi_add_itm_des;
                chkAlterSer.Checked = _item.Mi_edit_alt_ser;
                chkSerReq.Checked = _item.Mi_ser_rq_cus;
                chkMaintSupp.Checked = _item.Mi_main_supp;
                chkAppCond.Checked = _item.Mi_app_itm_cond;




                //_lstfg_cost = new List<mst_itm_fg_cost>();
                _lstfg_cost = CHNLSVC.General.GetFinishGood(txtItem.Text);
                gvStatus.DataSource = null;
                gvStatus.AutoGenerateColumns = false;
                gvStatus.DataSource = new List<mst_itm_fg_cost>();
                gvStatus.DataSource = _lstfg_cost;


                //  _lstreorder = new List<mst_itm_com_reorder>();
                _lstreorder = CHNLSVC.General.GetReOrder(txtItem.Text);
                gvReorder.DataSource = null;
                gvReorder.AutoGenerateColumns = false;
                gvReorder.DataSource = new List<mst_itm_com_reorder>();
                gvReorder.DataSource = _lstreorder;

                //  _lstcomItem = new List<mst_com_itm>();
                _lstcomItem = CHNLSVC.General.GetComItem(txtItem.Text);
                gvComItem.DataSource = null;
                gvComItem.AutoGenerateColumns = false;
                gvComItem.DataSource = new List<MasterCompanyItem>();
                gvComItem.DataSource = _lstcomItem;

                //    _lstcusItem = new List<BusEntityItem>();
                _lstcusItem = CHNLSVC.General.GetBuninessEntityItem(txtItem.Text, "C");
                gvCustomer.DataSource = null;
                gvCustomer.AutoGenerateColumns = false;
                gvCustomer.DataSource = new List<BusEntityItem>();
                gvCustomer.DataSource = _lstcusItem;

                // _lstsupItem = new List<BusEntityItem>();
                _lstsupItem = CHNLSVC.General.GetBuninessEntityItem(txtItem.Text, "S");
                gvSupplier.DataSource = null;
                gvSupplier.AutoGenerateColumns = false;
                gvSupplier.DataSource = new List<BusEntityItem>();
                gvSupplier.DataSource = _lstsupItem;

                // _lstredCom = new List<mst_itm_redeem_com>();
                _lstredCom = CHNLSVC.General.GetRedeem(txtItem.Text);
                gvRedeemCom.DataSource = null;
                gvRedeemCom.AutoGenerateColumns = false;
                gvRedeemCom.DataSource = new List<mst_itm_redeem_com>();
                gvRedeemCom.DataSource = _lstredCom;

                //     _lstmrn = new List<mst_itm_mrn_com>();
                _lstmrn = CHNLSVC.General.getItemMRN(txtItem.Text);
                gvMRN.DataSource = null;
                gvMRN.AutoGenerateColumns = false;
                gvMRN.DataSource = new List<mst_itm_mrn_com>();
                gvMRN.DataSource = _lstmrn;

                //   _lstreplace = new List<mst_itm_replace>();
                _lstreplace = CHNLSVC.General.getReplaceItem(txtItem.Text);
                gvRepalced.DataSource = null;
                gvRepalced.AutoGenerateColumns = false;
                gvRepalced.DataSource = new List<mst_itm_replace>();
                gvRepalced.DataSource = _lstreplace;

                //   _lstkit = new List<MasterItemComponent>();
                _lstkit = CHNLSVC.General.getitemComponent(txtItem.Text);
                gvKit.DataSource = null;
                gvKit.AutoGenerateColumns = false;
                gvKit.DataSource = new List<MasterItemComponent>();
                gvKit.DataSource = _lstkit;

                //   _lsttaxClaim = new List<MasterItemTaxClaim>();
                _lsttaxClaim = CHNLSVC.General.getitemTaxClaim(txtItem.Text);
                gvTaxClaim.DataSource = null;
                gvTaxClaim.AutoGenerateColumns = false;
                gvTaxClaim.DataSource = new List<MasterItemTaxClaim>();
                gvTaxClaim.DataSource = _lsttaxClaim;

                //   _lstitmPrd = new List<mst_itm_sevpd>();
                _lstitmPrd = CHNLSVC.General.getServiceSchedule(txtItem.Text);
                gvservice.DataSource = null;
                gvservice.AutoGenerateColumns = false;
                gvservice.DataSource = new List<mst_itm_sevpd>();
                gvservice.DataSource = _lstitmPrd;


                //  _lstitmWrd = new List<MasterItemWarrantyPeriod>();
                _lstitmWrd = CHNLSVC.General.getitemWarranty(txtItem.Text);
                gvWarranty.DataSource = null;
                gvWarranty.AutoGenerateColumns = false;
                gvWarranty.DataSource = new List<MasterItemWarrantyPeriod>();
                gvWarranty.DataSource = _lstitmWrd;

                //  _lstpcWrd = new List<mst_itm_pc_warr>();
                _lstpcWrd = CHNLSVC.General.getPcWarrant(txtItem.Text);
                gvPcwarranty.DataSource = null;
                gvPcwarranty.AutoGenerateColumns = false;
                gvPcwarranty.DataSource = new List<mst_itm_pc_warr>();
                gvPcwarranty.DataSource = _lstpcWrd;


                //   _lstchannelWrd = new List<mst_itm_channlwara>();
                _lstchannelWrd = CHNLSVC.General.getChannelWarranty(txtItem.Text);
                gvcannelWara.DataSource = null;
                gvcannelWara.AutoGenerateColumns = false;
                gvcannelWara.DataSource = new List<mst_itm_channlwara>();
                gvcannelWara.DataSource = _lstchannelWrd;


                //   _lstcont = new List<mst_itm_container>();
                _lstcont = CHNLSVC.General.getRContainerItem(txtItem.Text);
                gvCont.DataSource = null;
                gvCont.AutoGenerateColumns = false;
                gvCont.DataSource = new List<mst_itm_container>();
                gvCont.DataSource = _lstcont;

                //kapila 28/2/2017
                _lstcomItemTax = CHNLSVC.Inventory.GetItemTax(null, txtItem.Text, null, null, null);
                gvItmTaxs.DataSource = null;
                gvItmTaxs.AutoGenerateColumns = false;
                gvItmTaxs.DataSource = new List<MasterItemTax>();
                gvItmTaxs.DataSource = _lstcomItemTax;

            }

        }

        private void txtItem_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItem.Text))
            {
                if (chkSave.Checked == false)
                {
                    Load_items();
                }
            }
        }

        private void gvStatus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvStatus_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Item Master", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvStatus.SelectedCells[0].Value.ToString();


                        _lstfg_cost.RemoveAll(x => x.Ifc_fg_item_code == type);
                        gvStatus.AutoGenerateColumns = false;
                        gvStatus.DataSource = new List<mst_itm_fg_cost>();
                        gvStatus.DataSource = _lstfg_cost;

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

        private void btnsrhwarauom_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtWaraUOM;
            _CommonSearch.txtSearchbyword.Text = txtWaraUOM.Text;
            _CommonSearch.ShowDialog();
            txtWaraUOM.Focus();
        }

        private void btnsrhwaraDuuom_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDuration;
            _CommonSearch.txtSearchbyword.Text = txtDuration.Text;
            _CommonSearch.ShowDialog();
            txtDuration.Focus();
        }

        private void txtTaxStucture_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCapacity.Focus();
            }
        }

        private void txtTaxStucture_TextChanged(object sender, EventArgs e)
        {

        }

        private void gvReorder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Item Master", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvReorder.SelectedCells[0].Value.ToString();


                        _lstreorder.RemoveAll(x => x.Icr_com_code == type);
                        gvReorder.AutoGenerateColumns = false;
                        gvReorder.DataSource = new List<mst_itm_com_reorder>();
                        gvReorder.DataSource = _lstreorder;

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

        private void gvComItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Item Master", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvComItem.SelectedCells[0].Value.ToString();


                        _lstcomItem.RemoveAll(x => x.Mci_com == type);
                        gvComItem.AutoGenerateColumns = false;
                        gvComItem.DataSource = new List<MasterCompanyItem>();
                        gvComItem.DataSource = _lstcomItem;

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

        private void gvCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Item Master", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvCustomer.SelectedCells[0].Value.ToString();
                        string cust = gvCustomer.SelectedCells[1].Value.ToString();

                        _lstcusItem.RemoveAll(x => x.MBII_COM == type && x.MBII_CD == cust);
                        gvCustomer.AutoGenerateColumns = false;
                        gvCustomer.DataSource = new List<BusEntityItem>();
                        gvCustomer.DataSource = _lstcusItem;

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

        private void gvSupplier_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Item Master", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvSupplier.SelectedCells[1].Value.ToString();
                        string com = gvSupplier.SelectedCells[0].Value.ToString();

                        _lstsupItem.RemoveAll(x => x.MBII_CD == type && x.MBII_COM == com);
                        gvSupplier.AutoGenerateColumns = false;
                        gvSupplier.DataSource = new List<BusEntityItem>();
                        gvSupplier.DataSource = _lstsupItem;

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

        private void gvRedeemCom_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Item Master", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvRedeemCom.SelectedCells[0].Value.ToString();


                        _lstredCom.RemoveAll(x => x.Red_com_code == type);
                        gvRedeemCom.AutoGenerateColumns = false;
                        gvRedeemCom.DataSource = new List<mst_itm_redeem_com>();
                        gvRedeemCom.DataSource = _lstredCom;

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

        private void gvMRN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Item Master", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvMRN.SelectedCells[0].Value.ToString();


                        _lstmrn.RemoveAll(x => x.Imc_com == type);
                        gvMRN.AutoGenerateColumns = false;
                        gvMRN.DataSource = new List<mst_itm_mrn_com>();
                        gvMRN.DataSource = _lstmrn;

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

        private void gvRepalced_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Item Master", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvRepalced.SelectedCells[0].Value.ToString();


                        _lstreplace.RemoveAll(x => x.Rpl_replaceditem == type);
                        gvRepalced.AutoGenerateColumns = false;
                        gvRepalced.DataSource = new List<mst_itm_replace>();
                        gvRepalced.DataSource = _lstreplace;

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

        private void gvKit_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Item Master", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvKit.SelectedCells[0].Value.ToString();


                        _lstkit.RemoveAll(x => x.Micp_comp_itm_cd == type);
                        gvKit.AutoGenerateColumns = false;
                        gvKit.DataSource = new List<MasterItemComponent>();
                        gvKit.DataSource = _lstkit;

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

        private void gvCont_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Item Master", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvCont.SelectedCells[0].Value.ToString();


                        _lstcont.RemoveAll(x => x.Ic_container_type_code == type);
                        gvCont.AutoGenerateColumns = false;
                        gvCont.DataSource = new List<mst_itm_container>();
                        gvCont.DataSource = _lstcont;

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

        private void gvTaxClaim_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Item Master", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvTaxClaim.SelectedCells[0].Value.ToString();
                        string taxcat = gvTaxClaim.SelectedCells[1].Value.ToString();
                        decimal taxrate = Convert.ToDecimal(gvTaxClaim.SelectedCells[2].Value.ToString());

                        _lsttaxClaim.RemoveAll(x => x.Mic_com == type && x.Mic_tax_cd == taxcat && x.Mic_tax_rt == taxrate);
                        gvTaxClaim.AutoGenerateColumns = false;
                        gvTaxClaim.DataSource = new List<MasterItemTaxClaim>();
                        gvTaxClaim.DataSource = _lsttaxClaim;

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

        private void gvservice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Item Master", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvservice.SelectedCells[0].Value.ToString();
                        Int32 term = Convert.ToInt32(gvservice.SelectedCells[1].Value.ToString());

                        _lstitmPrd.RemoveAll(x => x.Msp_itm_stus == type && x.Msp_term == term);
                        gvservice.AutoGenerateColumns = false;
                        gvservice.DataSource = new List<mst_itm_sevpd>();
                        gvservice.DataSource = _lstitmPrd;

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

        private void gvWarranty_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Item Master", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvWarranty.SelectedCells[0].Value.ToString();
                        Int32 prd = Convert.ToInt32(gvWarranty.SelectedCells[2].Value.ToString());

                        _lstitmWrd.RemoveAll(x => x.Mwp_itm_stus == type && x.Mwp_val == prd);
                        gvWarranty.AutoGenerateColumns = false;
                        gvWarranty.DataSource = new List<MasterItemWarrantyPeriod>();
                        gvWarranty.DataSource = _lstitmWrd;

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

        private void gvPcwarranty_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Item Master", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type = gvPcwarranty.SelectedCells[0].Value.ToString();
                        string pc = gvPcwarranty.SelectedCells[1].Value.ToString();
                        string sts = gvPcwarranty.SelectedCells[2].Value.ToString();
                        Int32 prd = Convert.ToInt32(gvPcwarranty.SelectedCells[3].Value.ToString());

                        _lstpcWrd.RemoveAll(x => x.Pc_com == type && x.Pc_code == pc && x.Pc_item_stus == sts && x.Pc_wara_period == prd);
                        gvPcwarranty.AutoGenerateColumns = false;
                        gvPcwarranty.DataSource = new List<mst_itm_pc_warr>();
                        gvPcwarranty.DataSource = _lstpcWrd;

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

        private void gvcannelWara_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Item Master", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string com = gvcannelWara.SelectedCells[0].Value.ToString();
                        string chan = gvcannelWara.SelectedCells[1].Value.ToString();
                        string sts = gvcannelWara.SelectedCells[2].Value.ToString();
                        Int32 prd = Convert.ToInt32(gvcannelWara.SelectedCells[3].Value.ToString());

                        _lstchannelWrd.RemoveAll(x => x.Cw_channel == chan && x.Cw_item_status == sts && x.Cw_warranty_prd == prd && x.Cw_com == com);
                        gvcannelWara.AutoGenerateColumns = false;
                        gvcannelWara.DataSource = new List<mst_itm_channlwara>();
                        gvcannelWara.DataSource = _lstchannelWrd;

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

        private void gvReorder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvComItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvRedeemCom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvMRN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvRepalced_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvKit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvTaxClaim_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvservice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvCont_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvWarranty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvPcwarranty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvcannelWara_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtItemCom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbFoc.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnSearchitemCom_Click(null, null);
            }

        }

        private void txtitemDes_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cmbFoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbAgecType.Focus();
            }
        }

        private void cmbAgecType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddRepalced.Focus();
            }
        }

        private void cmbitemStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCuscom.Focus();
            }
        }

        private void txtCuscom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCust.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnSearchCusitem_Click(null, null);

            }

        }

        private void txtCust_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbCustStatus.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnCusSearch_Click(null, null);

            }
        }



        private void cmbCustStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSupp.Focus();
            }
        }

        private void txtSupp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbsupStatus.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnSearchSupp_Click(null, null);
            }
        }



        private void cmbsupStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtReCom.Focus();
            }
        }

        private void txtReCom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbReStatus.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnSearchRedeem_Click(null, null);
            }
        }

        private void cmbReStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMrnCom.Focus();
            }
        }

        private void txtMrnCom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbmrnStatus.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnSearchmrn_Click(null, null);
            }

        }

        private void cmbmrnStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtrepItem.Focus();
            }
        }

        private void txtrepItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbrepStatus.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnSerchRep_Click(null, null);
            }
        }

        private void txtrepDes_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cmbrepStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtkitItem.Focus();
            }
        }

        private void txtkitItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbkitCate.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnSearchKit_Click(null, null);

            }

        }

        private void cmbkitCate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbKitItemType.Focus();
            }
        }

        private void cmbKitItemType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtkitcost.Focus();
            }
        }

        private void txtkitcost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbScan.Focus();
            }

        }

        private void cmbScan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtunits.Focus();
            }
        }

        private void txtunits_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbKitActive.Focus();
            }
        }

        private void cmbKitActive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddKit.Focus();
            }
        }

        private void txtgross_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtnet.Focus();
            }

        }

        private void txtnet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtwuom.Focus();
            }
        }

        private void txtwuom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txthight.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnSrhWeightUOM_Click(null, null);
            }

        }

        private void txthight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtwidth.Focus();
            }

        }

        private void txtwidth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtbreath.Focus();
            }
        }

        private void txtbreath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtduom.Focus();
            }

        }

        private void txtduom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtclaimcom.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnsrhdimenuom_Click(null, null);

            }
        }

        private void txtclaimcom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbclaimcate.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnsrhTaxCom_Click(null, null);

            }
        }

        private void cmbclaimcate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txttaxRate.Focus();
            }
        }

        private void txttaxRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtclaim.Focus();
            }
        }

        private void txtclaim_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddTaxClaim.Focus();
            }
        }

        private void cmbserSts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbserTerm.Focus();
            }

        }

        private void cmbserTerm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtserfrom.Focus();
            }
        }

        private void txtserfrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtserto.Focus();
            }
        }

        private void txtserto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtseruom.Focus();
            }
        }

        private void txtseruom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtserdfrom.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnsrhserUOM_Click(null, null);
            }
        }

        private void txtserdfrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtserdto.Focus();
            }

        }

        private void txtserdto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtserduom.Focus();
            }

        }

        private void txtserduom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbserisfree.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnsrhseraUOM_Click(null, null);
            }
        }

        private void cmbserisfree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddservice.Focus();
            }
        }

        private void cmbContType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtContUnits.Focus();
            }
        }

        private void txtContUnits_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddCont.Focus();
            }
        }

        private void cmbwStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWaraUOM.Focus();
            }
        }

        private void cmbwPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWarRem.Focus();
            }

        }

        private void cmbwdur_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWarRem.Focus();
            }

        }

        private void txtWarRem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbwarsPrd.Focus();
            }

        }

        private void cmbwarsPrd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtwarsRem.Focus();
            }

        }

        private void cmbwarsdur_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtwarsRem.Focus();
            }
        }

        private void txtwarsRem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpEffectiveDate.Focus();
            }
        }

        private void dtpEffectiveDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddWarranty.Focus();
            }
        }

        private void txtpcCom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpc.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnSearchpcWara_Click(null, null);

            }
        }

        private void txtpc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbPcstatus.Focus();
            }


            if (e.KeyCode == Keys.F2)
            {
                btnSearchpc_Click(null, null);

            }
        }

        private void cmbPcstatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbpcPrd.Focus();
            }
        }

        private void cmbpcPrd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpcRem.Focus();
            }
        }

        private void txtpcRem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtppcfrom.Focus();
            }

        }

        private void dtppcfrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtppcto.Focus();
            }
        }

        private void dtppcto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddPcwarranty.Focus();
            }

        }

        private void txtChan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbchanStatus.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnsrhchannel_Click(null, null);
            }
        }

        private void cmbchanStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtChanPrd.Focus();
            }

        }

        private void txtChanPrd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddcannelWara.Focus();
            }

        }



        private void txtWaraUOM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbwPeriod.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnsrhwarauom_Click(null, null);
            }

        }

        private void btnsrcTax_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterTax);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchTaxMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtTaxStucture;
            _CommonSearch.txtSearchbyword.Text = txtTaxStucture.Text;
            _CommonSearch.ShowDialog();
            txtTaxStucture.Focus();
        }

        private void txtFgood_Leave(object sender, EventArgs e)
        {
            MasterItem _itemdetail = null;
            if (!string.IsNullOrEmpty(txtFgood.Text))
            {

                _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtFgood.Text);
                if (_itemdetail == null)
                {
                    MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFgood.Clear();
                    txtFgood.Focus();
                    return;
                }

                if (_itemdetail.Mi_itm_tp == "R")
                {
                    MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFgood.Clear();
                    txtFgood.Focus();
                    return;
                }
                if (!string.IsNullOrEmpty(txtFgood.Text))
                {
                    if (txtFgood.Text == txtItem.Text)
                    {
                        MessageBox.Show("Finish good item and can not be same as item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtrepItem.Clear();
                        txtrepItem.Focus();
                        return;
                    }
                }
            }
        }

        private void txtMainCat_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMainCat.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtMainCat.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid Category", "Invalid Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMainCat.Clear();
                txtMainCat.Focus();
                return;
            }

        }

        private void txtCat1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat1.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat1.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid Category", "Invalid Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCat1.Clear();
                txtCat1.Focus();
                return;
            }
        }

        private void txtCat2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat2.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat2.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid Category", "Invalid Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCat2.Clear();
                txtCat2.Focus();
                return;
            }
        }

        private void txtCat3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat3.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat3.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid Category", "Invalid Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCat3.Clear();
                txtCat3.Focus();
                return;
            }
        }

        private void txtCat4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat4.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat5);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat4.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid Category", "Invalid Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCat4.Clear();
                txtCat4.Focus();
                return;
            }
        }

        private void txtTaxStucture_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTaxStucture.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchTaxMaster(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtTaxStucture.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid Structure", "Invalid Structure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTaxStucture.Clear();
                txtTaxStucture.Focus();
                return;
            }
        }

        private void txtColorExt_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtColorExt.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtColorExt.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid Color", "Invalid Color", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtColorExt.Clear();
                txtColorExt.Focus();
                return;
            }
        }

        private void txtColorInt_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtColorInt.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtColorInt.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid Color", "Invalid Color", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtColorInt.Clear();
                txtColorInt.Focus();
                return;
            }
        }

        private void txtModel_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtModel.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            //  _CommonSearch.ReturnIndex = 0;
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
            //   _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
            //   DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(_CommonSearch.SearchParams, null, null);

            //    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtModel.Text).ToList();
            //  if (_validate == null || _validate.Count <= 0)
            List<MasterItemModel> _lstmodeltem = new List<MasterItemModel>();
            _lstmodeltem = CHNLSVC.General.GetItemModel(txtModel.Text);
            if (_lstmodeltem == null)
            {
                MessageBox.Show("Invalid Model", "Invalid Model", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtModel.Clear();
                txtModel.Focus();
                return;
            }
            else
            {

                List<MasterItemModel> _lstmodel = new List<MasterItemModel>();
                _lstmodeltem = CHNLSVC.General.GetItemModel(txtModel.Text);
                if (_lstmodeltem != null)
                {
                    // _lstmodel = _lstmodeltem.Where(x => x.Mm_cd == txtModel.Text).ToList();   //kapila 6/7/2016
                    txtMainCat.Text = _lstmodeltem[0].Mm_cat1;
                    txtCat1.Text = _lstmodeltem[0].Mm_cat2;
                    txtCat2.Text = _lstmodeltem[0].Mm_cat3;
                    txtCat3.Text = _lstmodeltem[0].Mm_cat4;
                    txtCat4.Text = _lstmodeltem[0].Mm_cat5;
                }

            }
        }

        private void txtBrand_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBrand.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtBrand.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid Brand", "Invalid Brand", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBrand.Clear();
                txtBrand.Focus();
                return;
            }
        }

        private void txtUOM_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUOM.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtUOM.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid UOM", "Invalid UOM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUOM.Clear();
                txtUOM.Focus();
                return;
            }

        }

        private void txtCountry_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCountry.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCountry.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid Country", "Invalid Country", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCountry.Clear();
                txtCountry.Focus();
                return;
            }
        }

        private void txtrepItem_Leave(object sender, EventArgs e)
        {
            MasterItem _itemdetail = null;
            if (!string.IsNullOrEmpty(txtrepItem.Text))
            {

                _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtrepItem.Text);
                if (_itemdetail == null)
                {
                    MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtrepItem.Clear();
                    txtrepItem.Focus();
                    return;
                }

                if (_itemdetail.Mi_itm_tp == "R")
                {
                    MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtrepItem.Clear();
                    txtrepItem.Focus();
                    return;
                }

                if (txtrepItem.Text == txtItem.Text)
                {
                    MessageBox.Show("Replace item and can be same as item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtrepItem.Clear();
                    txtrepItem.Focus();
                    return;
                }

                txtrepDes.Text = _itemdetail.Mi_shortdesc;
            }

        }

        private void txtwuom_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtwuom.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtwuom.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid UOM", "Invalid UOM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtwuom.Clear();
                txtwuom.Focus();
                return;
            }
        }

        private void txtduom_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtduom.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtduom.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid UOM", "Invalid UOM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtduom.Clear();
                txtduom.Focus();
                return;
            }
        }

        private void txtseruom_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtseruom.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtseruom.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid UOM", "Invalid UOM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtseruom.Clear();
                txtseruom.Focus();
                return;
            }
        }

        private void txtserduom_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtserduom.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtserduom.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid UOM", "Invalid UOM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtserduom.Clear();
                txtserduom.Focus();
                return;
            }
        }

        private void txtWaraUOM_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtWaraUOM.Text))
            {
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(_CommonSearch.SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtWaraUOM.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                MessageBox.Show("Invalid UOM", "Invalid UOM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtWaraUOM.Clear();
                txtWaraUOM.Focus();
                return;
            }

        }

        private void txtMainCat_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCat1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCat2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCat3_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCat4_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBrand_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtColorExt_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSrhPurCom_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPurComp;
                _CommonSearch.ShowDialog();

                txtPurComp.Focus();
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

        private void txtPurComp_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPurComp_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPurComp.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtPurComp.Text.Trim());
                    if (com == null)
                    {
                        MessageBox.Show("Invalid Company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void txtCuscom_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtReCom_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMrnCom_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtrepItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtkitItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtkitItem_Leave(object sender, EventArgs e)
        {
            MasterItem _itemdetail = null;
            if (!string.IsNullOrEmpty(txtkitItem.Text))
            {

                _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtkitItem.Text);
                if (_itemdetail == null)
                {
                    MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtkitItem.Clear();
                    txtkitItem.Focus();
                    return;
                }

                if (_itemdetail.Mi_itm_tp == "R")
                {
                    MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtkitItem.Clear();
                    txtkitItem.Focus();
                    return;
                }

                if (txtkitItem.Text == txtItem.Text)
                {
                    MessageBox.Show("KIT/SKU item and can be same as item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtkitItem.Clear();
                    txtkitItem.Focus();
                    return;
                }
            }
        }

        private void txtwuom_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUOM_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtduom_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtclaimcom_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtclaimcom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtclaimcom.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtclaimcom.Text.Trim());
                    if (com == null)
                    {
                        MessageBox.Show("Invalid Company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void txtseruom_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtserduom_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtWaraUOM_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtpcCom_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtChaCom_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnsrhchannel_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtChaCom.Text == "")
                {
                    MessageBox.Show("Select the Company", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChan;
                _CommonSearch.ShowDialog();
                txtChan.Select();



            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Item Master Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtChaCom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtChan.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                tnChacom_Click(null, null);

            }
        }

        private void txtChan_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) btnAddstatus.Focus();  //Allow Enter   
        }

        private void txtRoLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) btnAddReorder.Focus();  //Allow Enter   
        }

        private void txtRoQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) btnAddReorder.Focus();  //Allow Enter   
        }

        private void txtRoLevel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtkitcost_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtkitcost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) btnAddKit.Focus();  //Allow Enter   
        }

        private void txtunits_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtunits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) btnAddKit.Focus();  //Allow Enter   
        }

        private void txtgross_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtgross_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) txtnet.Focus();  //Allow Enter   

        }

        private void txtnet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) txthight.Focus();  //Allow Enter   

        }

        private void txthight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) txtwidth.Focus();  //Allow Enter   
        }

        private void txtwidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) txtbreath.Focus();  //Allow Enter   
        }

        private void txtbreath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) txtduom.Focus();  //Allow Enter   
        }

        private void txtContUnits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) btnAddCont.Focus();  //Allow Enter   
        }

        private void txttaxRate_TextChanged(object sender, EventArgs e)
        {

        }

        private void txttaxRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) txtclaim.Focus();  //Allow Enter   
        }

        private void txtclaim_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) btnAddTaxClaim.Focus();  //Allow Enter   
        }

        private void txtserfrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) txtserto.Focus();  //Allow Enter   
        }

        private void txtserto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) txtseruom.Focus();  //Allow Enter   
        }

        private void txtserdfrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) txtserdto.Focus();  //Allow Enter   
        }

        private void txtserdto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) txtserduom.Focus();  //Allow Enter   
        }

        private void txtclaim_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSrchCounty_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterContry);
                DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCountry;
                _CommonSearch.ShowDialog();

                txtCountry.Focus();
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

        private void txtCountry_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMainCat_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_mainCat_Click(null, null);
        }

        private void txtCat1_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_cat1_Click(null, null);
        }

        private void txtCat2_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_cat2_Click(null, null);
        }

        private void txtCat3_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_cat3_Click(null, null);
        }

        private void txtCat4_DockChanged(object sender, EventArgs e)
        {

        }

        private void txtTaxStucture_DoubleClick(object sender, EventArgs e)
        {
            btnsrcTax_Click(null, null);
        }

        private void txtBrand_DoubleClick(object sender, EventArgs e)
        {
            btn_SrchBrand_Click(null, null);
        }

        private void txtModel_DoubleClick(object sender, EventArgs e)
        {
            btn_SrchModel_Click(null, null);
        }

        private void txtItem_DoubleClick(object sender, EventArgs e)
        {
            btn_SrchCode_Click(null, null);
        }

        private void txtUOM_DoubleClick(object sender, EventArgs e)
        {
            btnsrhBaseUOM_Click(null, null);

        }

        private void txtColorExt_DoubleClick(object sender, EventArgs e)
        {
            btnshcextcolor_Click(null, null);
        }

        private void txtColorInt_DoubleClick(object sender, EventArgs e)
        {
            btnsrhintcolor_Click(null, null);
        }

        private void txtPurComp_DoubleClick(object sender, EventArgs e)
        {
            btnSrhPurCom_Click(null, null);
        }

        private void txtCountry_DoubleClick(object sender, EventArgs e)
        {
            btnSrchCounty_Click(null, null);
        }

        private void txtFgood_DoubleClick(object sender, EventArgs e)
        {
            btnSearchFG_Click(null, null);
        }

        private void txtCompany_DoubleClick(object sender, EventArgs e)
        {
            btnSearchreCom_Click(null, null);
        }

        private void txtItemCom_DoubleClick(object sender, EventArgs e)
        {
            btnSearchitemCom_Click(null, null);
        }

        private void txtCuscom_DoubleClick(object sender, EventArgs e)
        {
            btnSearchCusitem_Click(null, null);
        }

        private void txtCust_DoubleClick(object sender, EventArgs e)
        {
            btnCusSearch_Click(null, null);
        }

        private void txtSupp_DoubleClick(object sender, EventArgs e)
        {
            btnSearchSupp_Click(null, null);
        }

        private void txtReCom_DoubleClick(object sender, EventArgs e)
        {
            btnSearchRedeem_Click(null, null);
        }

        private void txtMrnCom_DoubleClick(object sender, EventArgs e)
        {
            btnSearchmrn_Click(null, null);
        }

        private void txtrepItem_DoubleClick(object sender, EventArgs e)
        {
            btnSerchRep_Click(null, null);
        }

        private void txtkitItem_DoubleClick(object sender, EventArgs e)
        {
            btnSearchKit_Click(null, null);
        }

        private void txtwuom_DoubleClick(object sender, EventArgs e)
        {
            btnSrhWeightUOM_Click(null, null);
        }

        private void txtduom_DoubleClick(object sender, EventArgs e)
        {
            btnsrhdimenuom_Click(null, null);
        }

        private void txtclaimcom_DoubleClick(object sender, EventArgs e)
        {
            btnsrhTaxCom_Click(null, null);
        }

        private void txtseruom_DoubleClick(object sender, EventArgs e)
        {
            btnsrhserUOM_Click(null, null);
        }

        private void txtserduom_DoubleClick(object sender, EventArgs e)
        {
            btnsrhseraUOM_Click(null, null);
        }

        private void txtWaraUOM_DoubleClick(object sender, EventArgs e)
        {
            btnsrhwarauom_Click(null, null);
        }

        private void txtpcCom_DoubleClick(object sender, EventArgs e)
        {
            btnSearchpcWara_Click(null, null);
        }

        private void txtChaCom_DoubleClick(object sender, EventArgs e)
        {
            tnChacom_Click(null, null);
        }

        private void txtChan_DoubleClick(object sender, EventArgs e)
        {
            btnsrhchannel_Click(null, null);
        }

        private void txtCat4_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_cat4_Click(null, null);
        }

        private void c(object sender, EventArgs e)
        {

        }

        private void btnsrhTaxCom_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtclaimcom;
                _CommonSearch.ShowDialog();

                txtclaimcom.Focus();
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

        private void btnsrhserUOM_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtseruom;
            _CommonSearch.txtSearchbyword.Text = txtseruom.Text;
            _CommonSearch.ShowDialog();
            txtseruom.Focus();
        }

        private void btnsrhseraUOM_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtserduom;
            _CommonSearch.txtSearchbyword.Text = txtserduom.Text;
            _CommonSearch.ShowDialog();
            txtserduom.Focus();
        }

        private void btnSearchpc_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtpc;
            _CommonSearch.txtSearchbyword.Text = txtpc.Text;
            _CommonSearch.ShowDialog();
            txtpc.Select();
        }

        private void txtpc_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtpc_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtpc.Text))
            {
                DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "PC", txtpc.Text);
                if (LocDes.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid Profit center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                    txtpc.Focus();
                }
            }

        }

        private void txtpc_DoubleClick(object sender, EventArgs e)
        {
            btnSearchpc_Click(null, null);
        }

        private void btnsrhSupCom_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSupCom;
                _CommonSearch.ShowDialog();

                txtCuscom.Focus();
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

        private void txtSupCom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSupCom.Text.Trim() != "")
                {
                    //TODO: LOAD COMPANY DESCRIPTION
                    MasterCompany com = CHNLSVC.General.GetCompByCode(txtSupCom.Text.Trim());
                    if (com == null)
                    {
                        MessageBox.Show("Invalid Company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void txtSupCom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSupp.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnsrhSupCom_Click(null, null);

            }

        }

        private void txtSupCom_DoubleClick(object sender, EventArgs e)
        {
            btnsrhSupCom_Click(null, null);
        }

        private void txtSupCom_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {

        }

        private void loadDefault()
        {  // cmbItemType.SelectedIndex=0;
            cmbActive.SelectedIndex = 0;
            cmbPayType.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            cmbSerilize.SelectedIndex = 1;
            cmbWarranty.SelectedIndex = 1;
            cmbChassis.SelectedIndex = 0;
            cmbScanSub.SelectedIndex = 1;
            cmbhpSalesAccept.SelectedIndex = 1;
            cmbIsRegister.SelectedIndex = 1;
            txtColorExt.Text = "N/A";
            txtCountry.Text = "SL";
            txtColorInt.Text = "N/A";
            txtPurComp.Text = BaseCls.GlbUserComCode;
            txtItemCom.Text = BaseCls.GlbUserComCode;
            txtPrefix.Text = "0";
            cmbFoc.SelectedIndex = 1;
            cmbitemStatus.SelectedIndex = 0;
            cmbwStatus.SelectedIndex = 0;
            cmbwPeriod.SelectedIndex = 0;
            cmbwarsPrd.SelectedIndex = 0;
            txtWarRem.Text = "NO WARRANTY";
            txtWaraUOM.Text = "MTH";
            txtUOM.Text = "NOS";
            txtCapacity.Text = "0";
            ttxHsCode.Text = "N/A";
            cmbwStatus.Text = "GOOD LP";
            txtTaxStucture.Text = "ABL-TAX-16-00000";
            txtPackCode.Text = "N/A";
            cmbAgecType.Text = "N/A";
            txtSupCom.Text = BaseCls.GlbUserComCode;
        }

        private void btn_srch_tax_comp_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtTaxComp;
                _CommonSearch.ShowDialog();

                txtTaxComp.Focus();
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

        private void btn_srch_tax_itmstus_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
            DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtTaxItmStus;
            _CommonSearch.ShowDialog();
            txtTaxItmStus.Focus();
        }

        private void btn_srch_tax_txcode_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TaxCode);
            DataTable _result = CHNLSVC.CommonSearch.SearchMasterTaxCodes(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtTaxCode;
            _CommonSearch.ShowDialog();
            txtTaxCode.Focus();
        }

        private void btnAddTax_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTaxComp.Text))
            {
                MessageBox.Show("Select Company", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtTaxItem.Text))
            {
                MessageBox.Show("Select Item", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtTaxCode.Text))
            {
                MessageBox.Show("Select tax code", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtTaxRateCode.Text))
            {
                MessageBox.Show("Select tax rate code", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MasterItemTax result = new MasterItemTax();
            if (_lstcomItemTax != null)
            {

                if (chkApplyAll.Checked == false)
                {
                    result = _lstcomItemTax.Find(x => x.Mict_com == txtTaxComp.Text && x.Mict_itm_cd == txtTaxItem.Text && x.Mict_stus == txtTaxItmStus.Text && x.Mict_tax_cd == txtTaxCode.Text);
                    if (result != null)
                    {
                        MessageBox.Show("This record already exist", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

            }
            else
            {
                _lstcomItemTax = new List<MasterItemTax>();
            }

            if (chkApplyAll.Checked)
            {
                Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();
                status_list.Add("Any", "Any");
                foreach (string Key in status_list.Keys)
                {
                    result = _lstcomItemTax.Find(x => x.Mict_com == txtTaxComp.Text && x.Mict_itm_cd == txtTaxItem.Text && x.Mict_stus == txtTaxItmStus.Text && x.Mict_tax_cd == txtTaxCode.Text);
                    if (result == null)
                    {
                        MasterItemTax _itm = new MasterItemTax();
                        _itm.Mict_act = chkTaxAct.Checked == true ? true : false;
                        _itm.Mict_com = txtTaxComp.Text;
                        _itm.Mict_effct_dt = DateTime.Now.Date;
                        _itm.Mict_itm_cd = txtTaxItem.Text;
                        _itm.Mict_stus = Key;
                        _itm.Mict_tax_cd = txtTaxCode.Text;
                        _itm.Mict_tax_rate = Convert.ToDecimal(txtTaxS_Rate.Text);
                        _itm.Mict_taxrate_cd = txtTaxRateCode.Text;
                        _itm.Mict_effct_dt = dtpEffDt.Value.Date;
                        _lstcomItemTax.Add(_itm);
                        _isAddTax = true;
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtTaxItmStus.Text))
                {
                    MessageBox.Show("Select Item status", "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                result = _lstcomItemTax.Find(x => x.Mict_com == txtTaxComp.Text && x.Mict_itm_cd == txtTaxItem.Text && x.Mict_stus == txtTaxItmStus.Text && x.Mict_tax_cd == txtTaxCode.Text);
                if (result == null)
                {
                    MasterItemTax _itm = new MasterItemTax();
                    _itm.Mict_act = chkTaxAct.Checked == true ? true : false;
                    _itm.Mict_com = txtTaxComp.Text;
                    _itm.Mict_effct_dt = DateTime.Now.Date;
                    _itm.Mict_itm_cd = txtTaxItem.Text;
                    _itm.Mict_stus = txtTaxItmStus.Text;
                    _itm.Mict_tax_cd = txtTaxCode.Text;
                    _itm.Mict_tax_rate = Convert.ToDecimal(txtTaxS_Rate.Text);
                    _itm.Mict_taxrate_cd = txtTaxRateCode.Text;
                    _itm.Mict_effct_dt = dtpEffDt.Value.Date;
                    _lstcomItemTax.Add(_itm);
                    _isAddTax = true;
                }
            }
            gvItmTaxs.DataSource = null;
            gvItmTaxs.AutoGenerateColumns = false;
            gvItmTaxs.DataSource = new List<MasterItemTax>();
            gvItmTaxs.DataSource = _lstcomItemTax;

            txtTaxComp.Enabled = true;
            txtTaxItmStus.Enabled = true;
            txtTaxRateCode.Enabled = true;
            txtTaxS_Rate.Enabled = true;
            txtTaxCode.Enabled = true;
            dtpEffDt.Enabled = true;
            chkApplyAll.Enabled = true;

            txtTaxItmStus.Text = "";
            txtTaxCode.Text = "";
            txtTaxS_Rate.Text = "";
            txtTaxRateCode.Text = "";
        }

        private void btn_srch_tax_rtcd_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TaxrateCodes);
            DataTable _result = CHNLSVC.CommonSearch.SearchMasterTaxRateCodes(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtTaxRateCode;
            _CommonSearch.ShowDialog();
            txtTaxRateCode.Focus();
        }

        private void txtTaxRateCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTaxRateCode.Text))
            {
                DataTable _dt = CHNLSVC.General.GetTaxRateCode(txtTaxCode.Text, txtTaxRateCode.Text);
                if (_dt.Rows.Count > 0)
                    txtTaxS_Rate.Text = _dt.Rows[0]["mtc_rt"].ToString();
                else
                {
                    MessageBox.Show("Invalid Tax Rate code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTaxRateCode.Text = "";
                    txtTaxS_Rate.Text = "";
                    txtTaxRateCode.Focus();
                }
            }
        }

        private void gvItmTaxs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure you want to update?", "Item Master", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string com = gvItmTaxs.SelectedCells[0].Value.ToString();
                    string status = gvItmTaxs.SelectedCells[1].Value.ToString();
                    string taxcode = gvItmTaxs.SelectedCells[2].Value.ToString();
                    string taxratecode = gvItmTaxs.SelectedCells[3].Value.ToString();
                    decimal rate = Convert.ToDecimal(gvItmTaxs.SelectedCells[4].Value);
                    Boolean _act = Convert.ToBoolean(gvItmTaxs.SelectedCells[5].Value);
                    DateTime _effdate = Convert.ToDateTime(gvItmTaxs.SelectedCells[6].Value);

                    _lstcomItemTax.RemoveAll(x => x.Mict_com == com && x.Mict_stus == status && x.Mict_tax_cd == taxcode && x.Mict_taxrate_cd == txtTaxRateCode.Text && x.Mict_itm_cd == txtTaxItem.Text);
                    gvItmTaxs.AutoGenerateColumns = false;
                    gvItmTaxs.DataSource = new List<MasterItemTax>();
                    gvItmTaxs.DataSource = _lstcomItemTax;

                    txtTaxComp.Text = com;
                    txtTaxItmStus.Text = status;
                    txtTaxRateCode.Text = taxratecode;
                    txtTaxS_Rate.Text = rate.ToString();
                    txtTaxCode.Text = taxcode;
                    dtpEffDt.Value = Convert.ToDateTime(_effdate);
                    chkTaxAct.Checked = _act;

                    txtTaxComp.Enabled = false;
                    txtTaxItmStus.Enabled = false;
                    txtTaxRateCode.Enabled = false;
                    txtTaxS_Rate.Enabled = false;
                    txtTaxCode.Enabled = false;
                    dtpEffDt.Enabled = false;
                    chkApplyAll.Enabled = false;

                }
            }
        }

        private void txtTaxComp_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTaxComp.Text))
            {
                DataTable _dt = CHNLSVC.General.GetCompanyByCode(txtTaxComp.Text);
                if (_dt.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid Company code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTaxComp.Text = "";
                    txtTaxComp.Focus();
                }
            }
        }

        private void txtTaxItmStus_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTaxItmStus.Text))
            {
                if (!CHNLSVC.Inventory.IsValidItemStatus(txtTaxItmStus.Text))
                {
                    MessageBox.Show("Invalid item status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtTaxItmStus.Text = "";
                    txtTaxItmStus.Focus();
                    return;
                }
            }
        }

        private void txtTaxCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTaxCode.Text))
            {
                DataTable _dt = CHNLSVC.General.GetTaxRateCode(txtTaxCode.Text, null);
                if (_dt.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid tax code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtTaxCode.Text = "";
                    txtTaxCode.Focus();
                    return;
                }
            }
        }

        private void txtTaxComp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_tax_comp_Click(null, null);

        }

        private void chkApplyAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkApplyAll.Checked)
            {
                btn_srch_tax_itmstus.Enabled = false;
                txtTaxItmStus.Text = "";
                txtTaxItmStus.Enabled = false;
            }
            else
            {
                btn_srch_tax_itmstus.Enabled = true;
                txtTaxItmStus.Enabled = true;
            }
        }

        private void txtTaxItmStus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_tax_itmstus_Click(null, null);
        }

        private void txtTaxCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_tax_txcode_Click(null, null);
        }

        private void txtTaxRateCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_tax_rtcd_Click(null, null);
        }

        private void gvItmTaxs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvItmTaxs_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvItmTaxs_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (gvItmTaxs.CurrentCell is DataGridViewCheckBoxCell)
                gvItmTaxs.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void btnSearchFile_spv_Click(object sender, EventArgs e)
        {
            txtFileName_pv.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            txtFileName_pv.Text = openFileDialog1.FileName;
        }

        private void btnUploadFile_spv_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;

            if (string.IsNullOrEmpty(txtFileName_pv.Text))
            {
                MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFileName_pv.Clear();
                txtFileName_pv.Focus();
                return;
            }

            System.IO.FileInfo fileObj = new System.IO.FileInfo(txtFileName_pv.Text);

            if (fileObj.Exists == false)
            {
                MessageBox.Show("Selected file does not exist at the following path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            string Extension = fileObj.Extension;

            string conStr = "";
            DataTable _dtCat = null;
            txtErrList.Text = "";
            txtErrList.Visible = false;

            if (Extension.ToUpper() == ".XLS")
            {

                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                         .ConnectionString;
            }
            else if (Extension.ToUpper() == ".XLSX")
            {
                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                          .ConnectionString;

            }

            conStr = String.Format(conStr, txtFileName_pv.Text, "NO");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable _dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(_dt);
            connExcel.Close();

            MasterItem _masterItem = new MasterItem();
            StringBuilder _errorLst = new StringBuilder();
            List<MasterColor> _lstclr = new List<MasterColor>();
            List<MasterItemModel> _lstmodeltem = new List<MasterItemModel>();
            if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            if (_dt.Rows.Count > 0)
            {
                foreach (DataRow _dr in _dt.Rows)
                {
                    if (_dr[0].ToString() == "Item Code") { continue; }
                    if (string.IsNullOrEmpty(_dr[0].ToString())) { continue; }

                    _masterItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _dr[0].ToString().Trim());
                    if (_masterItem != null)
                        txtErrList.Text = "Item already exist " + _dr[0].ToString().Trim();

                    _dtCat = CHNLSVC.General.GetMainCategoryDetail(_dr[3].ToString().Trim());
                    if (_dtCat.Rows.Count == 0)
                        txtErrList.Text = "Main Category " + _dr[3].ToString().Trim() + " not found";

                    MasterItemSubCate _mstItmSb = CHNLSVC.Sales.GetItemSubCate(_dr[4].ToString().Trim());
                    if (_mstItmSb == null)
                        txtErrList.Text = "Category 2 " + _dr[4].ToString().Trim() + " not found";

                    _dtCat = CHNLSVC.Sales.GetItemSubCate3(_dr[3].ToString().Trim(), _dr[4].ToString().Trim(), _dr[5].ToString().Trim());
                    if (_dtCat.Rows.Count == 0)
                        txtErrList.Text = "Category 3 " + _dr[3].ToString().Trim() + " " + _dr[4].ToString().Trim() + " " + _dr[5].ToString().Trim() + " not found";

                    REF_ITM_CATE4 _itmCate4 = CHNLSVC.Sales.GetItemCate4(_dr[6].ToString().Trim());
                    if (_itmCate4 == null)
                        txtErrList.Text = "Category 4 " + _dr[6].ToString().Trim().Trim() + " not found";

                    REF_ITM_CATE5 _itmCate5 = CHNLSVC.Sales.GetItemCate5(_dr[7].ToString().Trim());
                    if (_itmCate5 == null)
                        txtErrList.Text = "Category 5 " + _dr[7].ToString().Trim() + " not found";

                    _dtCat = CHNLSVC.General.GetMstItmUOM(_dr[14].ToString().Trim());
                    if (_dtCat.Rows.Count == 0)
                        txtErrList.Text = "Item UOM " + _dr[14].ToString().Trim() + " not found";

                    _lstclr = CHNLSVC.General.GetItemColorByCode(_dr[11].ToString().Trim());
                    if (_lstclr ==null)
                        txtErrList.Text = "Color code " + _dr[11].ToString().Trim() + " not found";

                    if (_dr[3].ToString().Trim().Length > 20)
                        txtErrList.Text = "Category 1 length is exceeded " + _dr[3].ToString().Trim();
                    if (_dr[4].ToString().Trim().Length > 20)
                        txtErrList.Text = "Category 2 length is exceeded " + _dr[4].ToString().Trim();
                    if (_dr[5].ToString().Trim().Length > 20)
                        txtErrList.Text = "Category 3 length is exceeded " + _dr[5].ToString().Trim();
                    if (_dr[6].ToString().Trim().Length > 20)
                        txtErrList.Text = "Category 4 length is exceeded " + _dr[6].ToString().Trim();
                    if (_dr[7].ToString().Trim().Length > 20)
                        txtErrList.Text = "Category 5 length is exceeded " + _dr[7].ToString().Trim();

                    if (string.IsNullOrEmpty(_dr[17].ToString()))
                        txtErrList.Text = "HS Code cannot be blank. Item code - " + _dr[0].ToString().Trim();
                    if (string.IsNullOrEmpty(_dr[18].ToString()))
                        txtErrList.Text = "Packing code cannot be blank. Item code - " + _dr[0].ToString().Trim();
                    if (string.IsNullOrEmpty(_dr[18].ToString()))
                        txtErrList.Text = "Purchase company cannot be blank. Item code - " + _dr[0].ToString().Trim();
                    if (string.IsNullOrEmpty(_dr[20].ToString()))
                        txtErrList.Text = "Item status cannot be blank. Item code - " + _dr[0].ToString().Trim();
                    if (string.IsNullOrEmpty(_dr[22].ToString()))
                        txtErrList.Text = "Tax structure cannot be blank. Item code - " + _dr[0].ToString().Trim();
                    if (string.IsNullOrEmpty(_dr[23].ToString()))
                        txtErrList.Text = "Is expiry date cannot be blank. Item code - " + _dr[0].ToString().Trim();
                  
                    _lstmodeltem = CHNLSVC.General.GetItemModel(_dr[9].ToString().Trim());
                    if (_lstmodeltem==null  || _lstmodeltem.Count <=0)
                    {
                        txtErrList.Text = "Item Model not setup. Model - " + _dr[9].ToString().Trim();
                    }
                    if (!string.IsNullOrEmpty(txtErrList.Text)) break;
                }
            }

            if (!string.IsNullOrEmpty(txtErrList.Text))
            {
                MessageBox.Show("Process halted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtErrList.Visible = true;
                return;
            }

            List<MasterItem> _lstMasterItem = new List<MasterItem>();
            List<MasterCompanyItem> _lstcom_Item = new List<MasterCompanyItem>();
            foreach (DataRow _dr in _dt.Rows)
            {
                if (_dr[0].ToString() == "Item Code") { continue; }
                if (string.IsNullOrEmpty(_dr[0].ToString())) { continue; }

                MasterItem _masterItem1 = new MasterItem();
                _masterItem1.Mi_cd = _dr[0].ToString().Trim();
                _masterItem1.Mi_shortdesc = _dr[1].ToString().Trim();
                _masterItem1.Mi_longdesc = _dr[2].ToString().Trim();
                _masterItem1.Mi_cate_1 = _dr[3].ToString().Trim();
                _masterItem1.Mi_cate_2 = _dr[4].ToString().Trim();
                _masterItem1.Mi_cate_3 = _dr[5].ToString().Trim();
                _masterItem1.Mi_cate_4 = _dr[6].ToString().Trim();
                _masterItem1.Mi_cate_5 = _dr[7].ToString().Trim();
                _masterItem1.Mi_brand = _dr[8].ToString().Trim();
                _masterItem1.Mi_model = _dr[9].ToString().Trim();
                _masterItem1.Mi_part_no = _dr[10].ToString().Trim();
                _masterItem1.Mi_color_int = _dr[11].ToString().Trim();
                _masterItem1.Mi_color_ext = _dr[12].ToString().Trim();
                _masterItem1.Mi_itm_tp = _dr[13].ToString().Trim();
                _masterItem1.Mi_itm_uom = _dr[14].ToString().Trim();
                _masterItem1.Mi_country_cd = _dr[15].ToString().Trim();
                _masterItem1.Mi_size = _dr[16].ToString().Trim();
                _masterItem1.Mi_hs_cd = _dr[17].ToString().Trim();
                _masterItem1.Mi_packing_cd = _dr[18].ToString().Trim();
                _masterItem1.Mi_purcom_cd = _dr[19].ToString().Trim();
                _masterItem1.Mi_itm_stus = _dr[20].ToString().Trim();
                _masterItem1.Mi_is_ser1 = Convert.ToInt32(_dr[21]);
                _masterItem1.Mi_anal1 = _dr[22].ToString().Trim();
                _masterItem1.Mi_is_exp_dt = Convert.ToInt32(_dr[23]);

                _masterItem1.Mi_act = true;
                _masterItem1.Mi_cre_by = BaseCls.GlbUserID;
                _masterItem1.Mi_mod_by = BaseCls.GlbUserID;

                _lstMasterItem.Add(_masterItem1);

                MasterCompanyItem _itm_com = new MasterCompanyItem();
                _itm_com.Mci_itm_cd = _dr[0].ToString().Trim();
                _itm_com.Mci_com = BaseCls.GlbUserComCode;
                _itm_com.Mci_act = true;
                _itm_com.Mci_isfoc = false;
                _itm_com.Mci_age_type = "N/A";
                _lstcom_Item.Add(_itm_com);
            }

           
            string _err = "";
            int row_aff = CHNLSVC.General.Save_Item_Master(_lstMasterItem,_lstcom_Item, out _err);
            if (row_aff == 1)
            {
                MessageBox.Show(_err, "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();

            }
            else
            {
                MessageBox.Show(_err, "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void lstPDItems_Click(object sender, EventArgs e)
        {

        }

        private void btn_srch_file_Click(object sender, EventArgs e)
        {
            txtTaxFile.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            txtTaxFile.Text = openFileDialog1.FileName;
        }

        private void btn_up_tax_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;

            if (string.IsNullOrEmpty(txtTaxFile.Text))
            {
                MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTaxFile.Clear();
                txtTaxFile.Focus();
                return;
            }

            System.IO.FileInfo fileObj = new System.IO.FileInfo(txtTaxFile.Text);

            if (fileObj.Exists == false)
            {
                MessageBox.Show("Selected file does not exist at the following path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            string Extension = fileObj.Extension;

            string conStr = "";
            DataTable _dtCat = null;
            lstMsg.Clear();

            if (Extension.ToUpper() == ".XLS")
            {

                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                         .ConnectionString;
            }
            else if (Extension.ToUpper() == ".XLSX")
            {
                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                          .ConnectionString;

            }

            conStr = String.Format(conStr, txtTaxFile.Text, "NO");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable _dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(_dt);
            connExcel.Close();

            MasterItem _masterItem = new MasterItem();
            StringBuilder _errorLst = new StringBuilder();
            MasterItem _mstItm = new MasterItem();
            if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            if (_dt.Rows.Count > 0)
            {
                foreach (DataRow _dr in _dt.Rows)
                {
                    //comp/item/status/tax cd/rate cd/rate/eff date
                    if (_dr[0].ToString() == "Company") { continue; }

                    _dtCat = CHNLSVC.General.GetCompanyByCode(_dr[0].ToString().Trim());
                    if (_dtCat.Rows.Count == 0)
                        lstMsg.Items.Add("Invalid company code " + _dr[0].ToString().Trim());

                    _mstItm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _dr[1].ToString().Trim());
                    if (_mstItm == null)
                        lstMsg.Items.Add("Invalid item code " + _dr[1].ToString().Trim());

                    if (!CHNLSVC.Inventory.IsValidItemStatus(_dr[2].ToString().Trim()))
                        lstMsg.Items.Add("Invalid item status " + _dr[2].ToString().Trim());

                    _dtCat = CHNLSVC.General.GetTaxRateCode(_dr[3].ToString().Trim(), null);
                    if (_dtCat.Rows.Count == 0)
                        lstMsg.Items.Add("Invalid tax code " + _dr[3].ToString().Trim());

                    _dtCat = CHNLSVC.General.GetTaxRateCode(_dr[3].ToString().Trim(), _dr[4].ToString().Trim());
                    if (_dtCat.Rows.Count == 0)
                        lstMsg.Items.Add("Invalid tax rate code " + _dr[4].ToString().Trim());


                }
            }

            if (lstMsg.Items.Count > 0)
            {
                MessageBox.Show("Process halted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lstMsg.Visible = true;
                return;
            }

            MasterItemTax result = new MasterItemTax();
            List<MasterItemTax> _tmplstcomItemTax = new List<MasterItemTax>();

            if (_lstcomItemTax == null)
            {
                _lstcomItemTax = new List<MasterItemTax>();
            }

            foreach (DataRow _dr in _dt.Rows)
            {
                if (_dr[0].ToString() == "Company") { continue; }

                _tmplstcomItemTax = CHNLSVC.Inventory.GetItemTax(null, _dr[1].ToString().Trim(), null, null, null);

                //comp/item/status/tax cd/rate cd/rate/eff date
                result = _tmplstcomItemTax.Find(x => x.Mict_com == _dr[0].ToString().Trim() && x.Mict_itm_cd == _dr[1].ToString().Trim() && x.Mict_stus == _dr[2].ToString().Trim() && x.Mict_tax_cd == _dr[3].ToString().Trim());
                if (result == null)
                {
                    MasterItemTax _itm = new MasterItemTax();
                    _itm.Mict_act = true;
                    _itm.Mict_com = _dr[0].ToString().Trim();
                    _itm.Mict_effct_dt = DateTime.Now.Date;
                    _itm.Mict_itm_cd = _dr[1].ToString().Trim();
                    _itm.Mict_stus = _dr[2].ToString().Trim();
                    _itm.Mict_tax_cd = _dr[3].ToString().Trim();
                    _itm.Mict_tax_rate = Convert.ToDecimal(_dr[5]);
                    _itm.Mict_taxrate_cd = _dr[4].ToString().Trim();
                    _itm.Mict_effct_dt = Convert.ToDateTime(_dr[6].ToString().Trim());
                    _lstcomItemTax.Add(_itm);
                    _isAddTax = true;
                }
            }
            string _err = "";
            int row_aff = CHNLSVC.General.SaveItemComTax(_lstcomItemTax, out _err);
            if (row_aff == 1)
            {
                MessageBox.Show(_err, "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();

            }
            else
            {
                MessageBox.Show(_err, "Item Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label146_Click(object sender, EventArgs e)
        {

        }

        private void cmbsupwarr_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    cmbwarsPrd.Focus();
            //}
        }

    }



}
