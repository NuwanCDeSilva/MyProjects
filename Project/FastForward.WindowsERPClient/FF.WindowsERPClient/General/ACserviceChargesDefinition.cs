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
using System.Collections;
using System.Text.RegularExpressions;

namespace FF.WindowsERPClient.General
{
    public partial class ACserviceChargesDefinition : Base
    {
        //sp_save_mst_itm_comtax =NEW
        //sp_get_ITEMS_ONSelect =NEW
        //sp_save_mst_itm_sev =NEW
        //spGetAC_SevCharge_itmes= new
        //pkg_search.sp_get_AC_SevCharge_itmes =new
        DataTable select_ITEMS_List = new DataTable();
        string companyCode = "";
        public ACserviceChargesDefinition()
        {
            InitializeComponent();
            txtCom_C.Text = BaseCls.GlbUserComCode;
            bind_dropdown();
            if (tabControl1.SelectedTab.Name == "tabPage1")
            { btnSave1.Visible = true; btnSave2.Visible = false; }
            else if (tabControl1.SelectedTab.Name == "tabPage2")
            { btnSave1.Visible = false; btnSave2.Visible = true; }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ACserviceChargesDefinition formnew = new ACserviceChargesDefinition();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }
        private void bind_dropdown()
        {
            if (txtCom_C.Text.Trim() == "") return;
            DataTable dt = CHNLSVC.Sales.Get_all_jobTypes(txtCom_C.Text.Trim(), "AC");
            ddlChgTp.DataSource = dt;
            ddlChgTp.DisplayMember = "sit_desc";
            ddlChgTp.ValueMember = "sit_itm_tp";
            List<PriceBookRef> _priceBook = CHNLSVC.Sales.GetPriceBooklist(txtCom_C.Text.Trim());
            DropDownListPriceBook.DataSource = _priceBook;
            DropDownListPriceBook.DisplayMember = "Sapb_desc";
            DropDownListPriceBook.ValueMember = "Sapb_pb";
        }
        private void ddlChgTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string chgTP = ddlChgTp.SelectedValue.ToString();
        }
        private void btnAddPB_Click(object sender, EventArgs e)
        {
            if (CheckBoxPriceBookAll.Checked == true)
            {
                List<PriceBookLevelRef> _PbLevel_All = new List<PriceBookLevelRef>();
                List<PriceBookRef> _priceBook = CHNLSVC.Sales.GetPriceBooklist(BaseCls.GlbUserComCode);
                foreach (PriceBookRef pb in _priceBook)
                { List<PriceBookLevelRef> _PbLevel = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, pb.Sapb_pb, null); _PbLevel_All.AddRange(_PbLevel); grvPriceLevel.DataSource = null; grvPriceLevel.AutoGenerateColumns = false; }
                grvPriceLevel.DataSource = _PbLevel_All;
            }
            else
            {
                List<PriceBookLevelRef> _PbLevel = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, DropDownListPriceBook.SelectedValue.ToString(), null);
                grvPriceLevel.DataSource = null;
                grvPriceLevel.AutoGenerateColumns = false;
                grvPriceLevel.DataSource = _PbLevel;
            }
        }
        private void btnClearPB_Click(object sender, EventArgs e)
        {
            checkBox_PB.Checked = false;
            grvPriceLevel.DataSource = null;
            grvPriceLevel.AutoGenerateColumns = false;
        }
        private void btnAll_pb_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvPriceLevel.Rows)
                { DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0]; chk.Value = true; }
                grvPriceLevel.EndEdit();
            }
            catch (Exception ex) { }
        }
        private void btnNon_pb_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvPriceLevel.Rows)
                { DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0]; chk.Value = false; }
                grvPriceLevel.EndEdit();
            }
            catch (Exception ex) { }
        }
        private void checkBox_PB_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_PB.Checked == true) this.btnAll_pb_Click(null, null);
            else this.btnNon_pb_Click(null, null);
        }
        private void txtCom_C_TextChanged(object sender, EventArgs e)
        {
            bind_dropdown();
        }
        private void txtCom_C_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            { ddlChgTp.Focus(); ddlChgTp.DroppedDown = true; }
        }
        private void DropDownListPriceBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnClearPB_Click(null, null);
            this.btnAddPB_Click(null, null);
        }
        private void btnChgDefnSave_Click(object sender, EventArgs e)
        {
            if (txtCom_C.Text.Trim() == "")
            { MessageBox.Show("Please select company!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (ddlPaidBy.SelectedIndex == -1)
            { MessageBox.Show("Please select the paid by party.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (Convert.ToDateTime(TextBoxFromDate.Text).Date > Convert.ToDateTime(TextBoxToDate.Text).Date)
            { MessageBox.Show("From date should be less than to date!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            try
            {
                Decimal RATE = Convert.ToDecimal(txtVatRate.Text); if (RATE > 100)
                { MessageBox.Show("Rate cannot be greater than 100!", "Rate", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtVatRate.Focus(); return; }
            }
            catch (Exception)
            { MessageBox.Show("Invalid rate!", "Rate", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtVatRate.Focus(); return; }
            if (Convert.ToDecimal(txtVatRate.Text.Trim()) < 0)
            { MessageBox.Show("Rate cannot be minus!", "Rate", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtVatRate.Focus(); return; }
            try
            { Decimal Price = Convert.ToDecimal(txtPrice.Text); }
            catch (Exception)
            { MessageBox.Show("Invalid price!", "Price", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtPrice.Focus(); return; }
            if (Convert.ToDecimal(txtPrice.Text.Trim()) < 0)
            { MessageBox.Show("Price cannot be minus!", "Price", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtPrice.Focus(); return; }
            MasterItem mstItm = new MasterItem();
            mstItm.Mi_act = true;
            mstItm.Mi_cate_1 = ddlChgTp.SelectedValue.ToString();
            mstItm.Mi_cate_2 = ddlPaidBy.SelectedItem.ToString().Trim().ToUpper() == "MANAGER" ? "M" : "C";
            mstItm.Mi_cd = txtChgCode.Text.Trim();
            mstItm.Mi_chg_tp = "CHA";
            mstItm.Mi_cre_by = BaseCls.GlbUserID; ;
            mstItm.Mi_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;
            mstItm.Mi_dim_height = 0;
            mstItm.Mi_dim_length = 1;
            mstItm.Mi_dim_uom = "N/A";
            mstItm.Mi_dim_width = 0;
            mstItm.Mi_fgitm_cd = "N/A";
            mstItm.Mi_hs_cd = "Not Defined";
            mstItm.Mi_is_editlongdesc = true;
            mstItm.Mi_is_editser1 = false;
            mstItm.Mi_is_editser2 = false;
            mstItm.Mi_is_editser3 = false;
            mstItm.Mi_is_editshortdesc = true;
            mstItm.Mi_is_reqcolorext = false;
            mstItm.Mi_is_reqcolorint = false;
            mstItm.Mi_is_ser1 = 0;
            mstItm.Mi_is_ser2 = 0;
            mstItm.Mi_is_ser3 = false;
            mstItm.Mi_itm_stus = "F";
            mstItm.Mi_itm_tp = "V";
            mstItm.Mi_itm_uom = "SET";
            mstItm.Mi_longdesc = txtLongDesc.Text.Trim();
            mstItm.Mi_mod_by = BaseCls.GlbUserID;
            mstItm.Mi_mod_dt = CHNLSVC.Security.GetServerDateTime().Date;
            mstItm.Mi_purcom_cd = txtCom_C.Text.Trim();
            mstItm.Mi_refitm_cd = "N/A";
            mstItm.Mi_session_id = BaseCls.GlbUserSessionID;
            mstItm.Mi_shortdesc = txtShortDesc.Text.Trim();
            mstItm.Mi_std_price = Convert.ToDecimal(txtPrice.Text.Trim());
            mstItm.Mi_uom_warrperiodmain = "MTH";
            mstItm.Mi_warr = false;
            mstItm.Mi_warr_print = false;
            mstItm.Mi_weight_uom = "N/A";
            MasterItemTax itmTax = new MasterItemTax();
            itmTax.Mict_act = true;
            itmTax.Mict_com = txtCom_C.Text.Trim();
            itmTax.Mict_itm_cd = txtChgCode.Text.Trim();
            itmTax.Mict_stus = "NOR";
            itmTax.Mict_tax_cd = "VAT";
            itmTax.Mict_tax_rate = Convert.ToDecimal(txtVatRate.Text.Trim());
            itmTax.Mict_taxrate_cd = "VAT0";
            string PirceBook = "";
            string PB_level = "";
            Int32 count = 0;
            grvPriceLevel.EndEdit();
            foreach (DataGridViewRow dgvr in grvPriceLevel.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                { count = count + 1; PirceBook = dgvr.Cells[1].Value.ToString(); PB_level = dgvr.Cells[2].Value.ToString(); }
            }
            if (count == 0)
            { MessageBox.Show("Please select a price level.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (count > 1)
            { MessageBox.Show("Please select only one price level.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            PriceDetailRef pbDet = new PriceDetailRef();
            pbDet.Sapd_warr_remarks = "N/A";
            pbDet.Sapd_upload_dt = CHNLSVC.Security.GetServerDateTime().Date;
            pbDet.Sapd_update_dt = CHNLSVC.Security.GetServerDateTime().Date;
            pbDet.Sapd_to_date = TextBoxToDate.Value.Date;
            pbDet.Sapd_session_id = BaseCls.GlbUserSessionID;
            pbDet.Sapd_qty_to = 999;
            pbDet.Sapd_qty_from = 1;
            pbDet.Sapd_price_type = 0;
            pbDet.Sapd_price_stus = "A";
            pbDet.Sapd_pbk_lvl_cd = PB_level;
            pbDet.Sapd_pb_tp_cd = PirceBook;
            pbDet.Sapd_no_of_use_times = 0;
            pbDet.Sapd_no_of_times = 9999;
            pbDet.Sapd_model = txtChgCode.Text.Trim();
            pbDet.Sapd_mod_when = mstItm.Mi_cre_dt;
            pbDet.Sapd_mod_by = BaseCls.GlbUserID;
            pbDet.Sapd_margin = 0;
            pbDet.Sapd_lst_cost = 0;
            pbDet.Sapd_itm_price = Convert.ToDecimal(txtPrice.Text.Trim());
            pbDet.Sapd_itm_cd = txtChgCode.Text.Trim();
            pbDet.Sapd_is_cancel = false;
            pbDet.Sapd_is_allow_individual = true;
            pbDet.Sapd_from_date = TextBoxFromDate.Value.Date;
            pbDet.Sapd_erp_ref = "NP";
            pbDet.Sapd_dp_ex_cost = 0;
            pbDet.Sapd_day_attempt = 3;
            pbDet.Sapd_customer_cd = "N/A";
            pbDet.Sapd_cre_when = mstItm.Mi_cre_dt;
            pbDet.Sapd_cre_by = BaseCls.GlbUserID;
            pbDet.Sapd_circular_no = "N/A";
            pbDet.Sapd_cancel_dt = DateTime.MinValue;
            pbDet.Sapd_avg_cost = 0;
            pbDet.Sapd_apply_on = "N/A";
            if (MessageBox.Show("Are you sure to save ?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            Int32 eff = CHNLSVC.Sales.Save_AC_ServiceChargesDefinitions(mstItm, itmTax, pbDet);
            if (eff > 0)
            { MessageBox.Show("Saved Successfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); this.btnClear_Click(null, null); }
            else
            { MessageBox.Show("Not Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private List<string> GetSelectedItemsList()
        {
            GridAll_Items.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in GridAll_Items.Rows)
            { DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell; if (Convert.ToBoolean(chk.Value) == true) list.Add(dgvr.Cells[1].Value.ToString()); }
            return list;
        }
        private void btnSaveItmCharges_Click(object sender, EventArgs e)
        {
            string chargeItmCode = txtChgItmCd.Text.Trim();
            if (txtChgItmCd.Text.Trim() == "")
            { MessageBox.Show("Please enter charge item code.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtChgItmCd.Focus(); return; }
            List<string> selectedItmList = GetSelectedItemsList();
            if (selectedItmList.Count < 1)
            { MessageBox.Show("Please select item code", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            MasterItemService itm_sev = new MasterItemService();
            itm_sev.Misv_act = true;
            itm_sev.Misv_chg_pty = txtPayByCode.Text.Trim();
            itm_sev.Misv_cre_by = BaseCls.GlbUserID;
            itm_sev.Misv_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;
            itm_sev.Misv_itm_cd = "";
            itm_sev.Misv_mod_by = BaseCls.GlbUserID; ;
            itm_sev.Misv_mod_dt = itm_sev.Misv_cre_dt;
            itm_sev.Misv_sevitm_cd = txtChgItmCd.Text.Trim();
            itm_sev.Misv_sevitm_stus = "GOD";
            if (MessageBox.Show("Are you sure to save ?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            Int32 eff = CHNLSVC.Sales.Save_mst_itm_sev(itm_sev, selectedItmList);
            if (eff > 0)
            { MessageBox.Show("Saved Successfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); this.btnClear_Click(null, null); }
            else MessageBox.Show("Not Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.AcServChgCode:
                    { paramsText.Append(companyCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    { paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.ItemBrand:
                    { paramsText.Append(""); break; }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    { paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    { paramsText.Append(txtIcat1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    { paramsText.Append(""); break; }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        private void btnItem_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = TextBoxItem;
            _CommonSearch.ShowDialog();
            TextBoxItem.Focus();
        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBrand.Text) && string.IsNullOrEmpty(txtIcat1.Text) && string.IsNullOrEmpty(txtIcat2.Text) && string.IsNullOrEmpty(txtModel.Text) && string.IsNullOrEmpty(TextBoxItem.Text))
            { MessageBox.Show("Please select a searching value", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            select_ITEMS_List = new DataTable();
            string itemCode = TextBoxItem.Text.Trim().ToUpper() == "" ? "%" : TextBoxItem.Text.Trim().ToUpper();
            string main_category = txtIcat1.Text.Trim() == "" ? "%" : txtIcat1.Text.Trim();
            string sub_category = txtIcat2.Text.Trim() == "" ? "%" : txtIcat2.Text.Trim ();
            string model = txtModel.Text.Trim() == "" ? "%" : txtModel.Text.Trim ();
            string brand = txtBrand.Text.Trim() == "" ? "%" : txtBrand.Text.Trim();
            DataTable dataSource = CHNLSVC.General.Get_ITEMS_ONSelect(main_category, sub_category, model, itemCode, brand, "Code");
            if (dataSource.Rows.Count <= 0) MessageBox.Show("No Items", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DataTable dataSource2 = new DataTable();
            dataSource2.Columns.Add("mi_cd");
            dataSource2.Columns.Add("mi_shortdesc");
            foreach (DataRow drr in dataSource.Rows)
            {
                string itmcd = drr["mi_cd"].ToString();
                string descirption = drr["mi_shortdesc"].ToString();
                var _duplicate = from _dup in select_ITEMS_List.AsEnumerable() where _dup["mi_cd"].ToString() == itmcd select _dup;
                if (_duplicate.Count() == 0)
                { DataRow DR2 = dataSource2.NewRow(); DR2["mi_cd"] = itmcd; DR2["mi_shortdesc"] = descirption; dataSource2.Rows.Add(DR2); }
            }
            if (select_ITEMS_List.Rows.Count == 0) select_ITEMS_List.Merge(dataSource);
            else select_ITEMS_List.Merge(dataSource2);
            GridAll_Items.DataSource = null;
            GridAll_Items.AutoGenerateColumns = false;
            GridAll_Items.DataSource = select_ITEMS_List;
        }
        private void btnSearchChgCd_Click(object sender, EventArgs e)
        {
            if (txtCompany_2.Text.Trim() == "")
            { MessageBox.Show("Please select company!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            companyCode = txtCompany_2.Text.Trim();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AcServChgCode);
            DataTable _result = CHNLSVC.CommonSearch.Get_AC_SevCharge_itmes(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtChgItmCd;
            _CommonSearch.ShowDialog();
            TextBoxItem.Focus();
        }
        private void txtChgItmCd_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = CHNLSVC.General.GetAC_SevCharge_itmes(txtCompany_2.Text.Trim(), txtChgItmCd.Text.Trim());
            if (dt == null)
            { if (txtChgItmCd.Text != "") MessageBox.Show("No details found for this company from this charge item!"); return; }
            if (dt.Rows.Count == 0)
            { if (txtChgItmCd.Text != "") MessageBox.Show("No details found for this company from this charge item!"); return; }
            txtShortDesc_2.Text = dt.Rows[0]["mi_shortdesc"].ToString();
            txtLongDesc_2.Text = dt.Rows[0]["MI_LONGDESC"].ToString();
            txtPayByCode.Text = dt.Rows[0]["MI_CATE_2"].ToString();
            txtPaidBy_2.Text = dt.Rows[0]["MI_CATE_2"].ToString().ToUpper() == "M" ? "Manager" : "Customer";
        }
        private void btnSearchCom_C_Click(object sender, EventArgs e)
        {
            companyCode = txtCom_C.Text.Trim();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCom_C;
            _CommonSearch.ShowDialog();
            txtCom_C.Focus();
        }
        private void btnSearchCom_Click(object sender, EventArgs e)
        {
            companyCode = txtCompany_2.Text.Trim();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCompany_2;
            _CommonSearch.ShowDialog();
            txtCompany_2.Focus();
        }
        private void txtCompany_2_TextChanged(object sender, EventArgs e)
        {
            txtChgItmCd.Text = "";
            txtShortDesc_2.Text = "";
            txtLongDesc_2.Text = "";
            txtPaidBy_2.Text = "";
            txtPayByCode.Text = "";
        }
        private void btnSearchChgCode_Click(object sender, EventArgs e)
        {
            if (txtCom_C.Text.Trim() == "")
            { MessageBox.Show("Please select company!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            companyCode = txtCom_C.Text.Trim();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AcServChgCode);
            DataTable _result = CHNLSVC.CommonSearch.Get_AC_SevCharge_itmes(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtChgCode;
            _CommonSearch.ShowDialog();
            TextBoxItem.Focus();
        }
        private void txtChgCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) txtPrice.Focus();
        }
        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            { txtPrice.Text = txtPrice.Text.Trim(); txtVatRate.Focus(); }
        }
        private void txtVatRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            { txtVatRate.Text = txtVatRate.Text.Trim(); txtShortDesc.Focus(); }
        }
        private void txtShortDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) txtLongDesc.Focus();
        }
        private void txtLongDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            { ddlPaidBy.Focus(); ddlPaidBy.DroppedDown = true; }
        }
        private void ddlPaidBy_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TextBoxFromDate.Focus();
        }
        private void TextBoxFromDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) TextBoxToDate.Focus();
        }
        private void ddlChgTp_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtChgCode.Focus();
        }
        private void TextBoxToDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            { DropDownListPriceBook.Focus(); DropDownListPriceBook.DroppedDown = true; }
        }
        private void DropDownListPriceBook_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.btnAddPB_Click(null, null);
        }
        private void txtPrice_Leave(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txtPrice.Text.Trim(), "[A-Z]+"))
            { txtPrice.Text = ""; MessageBox.Show("Enter valid value", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtPrice.Focus(); return; }
            try { Decimal Price = Convert.ToDecimal(txtPrice.Text); }
            catch (Exception) { MessageBox.Show("Invalid price!", "Price", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtPrice.Focus(); return; }
            if (Convert.ToDecimal(txtPrice.Text.Trim()) < 0) { MessageBox.Show("Price cannot be minus!", "Price", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtPrice.Focus(); return; }
        }
        private void txtVatRate_Leave(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txtVatRate.Text.Trim(), "[A-Z]+"))
            { txtVatRate.Text = ""; MessageBox.Show("Enter valid rate", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtVatRate.Focus(); return; }
            try
            { Decimal RATE = Convert.ToDecimal(txtVatRate.Text); if (RATE > 100) { MessageBox.Show("Rate cannot be greater than 100!", "Rate", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtVatRate.Focus(); return; } }
            catch (Exception)
            { MessageBox.Show("Invalid rate!", "Rate", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtVatRate.Focus(); return; }
            if (Convert.ToDecimal(txtVatRate.Text.Trim()) < 0)
            { MessageBox.Show("Rate cannot be minus!", "Rate", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtVatRate.Focus(); return; }
        }
        private void btnAllItem_Click(object sender, EventArgs e)
        {
            try
            { foreach (DataGridViewRow row in GridAll_Items.Rows) { DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0]; chk.Value = true; } GridAll_Items.EndEdit(); }
            catch (Exception ex) { }
        }
        private void btnNonItem_Click(object sender, EventArgs e)
        {
            try
            { foreach (DataGridViewRow row in GridAll_Items.Rows) { DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0]; chk.Value = false; } GridAll_Items.EndEdit(); }
            catch (Exception ex) { }
        }
        private void btnClearItem_Click(object sender, EventArgs e)
        {
            GridAll_Items.DataSource = null;
            GridAll_Items.AutoGenerateColumns = false;
        }
        private void chkAllItms_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllItms.Checked == true) this.btnAllItem_Click(null, null);
            else this.btnNonItem_Click(null, null);
        }
        private void txtCompany_2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) txtChgItmCd.Focus();
        }
        private void txtCom_C_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) this.btnSearchCom_C_Click(null, null);
            if (e.KeyCode == Keys.Enter) ddlChgTp.Focus();
        }
        private void txtChgCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) this.btnSearchChgCode_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtPrice.Focus();
        }
        private void txtCompany_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) this.btnSearchCom_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtChgItmCd.Focus();
        }
        private void txtChgItmCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) this.btnSearchChgCd_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtShortDesc_2.Focus();
        }
        private void txtCompany_2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnSearchCom_Click(null, null);
        }
        private void txtChgItmCd_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnSearchChgCd_Click(null, null);
        }
        private void txtCom_C_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnSearchCom_C_Click(null, null);
        }
        private void txtChgCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnSearchChgCode_Click(null, null);
        }
        private void TextBoxItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) this.btnItem_Click(null, null);
            if (e.KeyCode == Keys.Enter) btnAddItem.Focus();
        }
        private void TextBoxItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnItem_Click(null, null);
        }
        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            { txtPrice.Text = txtPrice.Text.Trim(); txtPrice.Focus(); }
            if (e.KeyCode == Keys.Enter) txtVatRate.Focus();
        }
        private void txtVatRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            { txtVatRate.Text = txtVatRate.Text.Trim(); txtVatRate.Focus(); }
            if (e.KeyCode == Keys.Enter) txtShortDesc.Focus();
        }
        private void txtChgItmCd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) TextBoxItem.Focus();
        }
        private void btnSave2_Click(object sender, EventArgs e)
        {
            this.btnSaveItmCharges_Click(null, null);
        }
        private void btnSave1_Click(object sender, EventArgs e)
        {
            this.btnChgDefnSave_Click(null, null);
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabPage1")
            { btnSave1.Visible = true; btnSave2.Visible = false; }
            else if (tabControl1.SelectedTab.Name == "tabPage2")
            { btnSave1.Visible = false; btnSave2.Visible = true; }
        }
        private void ddlChgTp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtChgCode.Focus();
        }
        private void txtShortDesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtLongDesc.Focus();
        }
        private void txtLongDesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ddlPaidBy.Focus();
        }
        private void ddlPaidBy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TextBoxFromDate.Focus();
        }
        private void TextBoxFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TextBoxToDate.Focus();
        }
        private void TextBoxToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) DropDownListPriceBook.Focus();
        }
        private void DropDownListPriceBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) CheckBoxPriceBookAll.Focus();
        }
        private void CheckBoxPriceBookAll_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnAddPB.Focus();
        }
        private void txtShortDesc_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtLongDesc_2.Focus();
        }
        private void txtLongDesc_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtPaidBy_2.Focus();
        }
        private void txtPaidBy_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtPayByCode.Focus();
        }
        private void txtPayByCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtBrand.Focus();
        }
         private void btnAddItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSaveItmCharges.Focus();
        }
        private void TextBoxItem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxItem.Text)) return;
            MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, TextBoxItem.Text);
            if (_item == null || string.IsNullOrEmpty(_item.Mi_cd))
            {
                MessageBox.Show("Please check the item code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TextBoxItem.Clear();
                TextBoxItem.Focus();
                return;
            }
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
        private void txtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btn_Srch_Brnd_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtIcat1.Focus();
        }
        private void txtBrand_Leave(object sender, EventArgs e)
        {

        }
        private void txtBrand_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btn_Srch_Brnd_Click(null, null);
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
        private void txtIcat1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btn_Srch_Cat1_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtIcat2.Focus();
        }
        private void txtIcat1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btn_Srch_Cat1_Click(null, null);
        }
        private void txtIcat1_Leave(object sender, EventArgs e)
        {

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
        private void txtIcat2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btn_Srch_Cat2_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtModel.Focus();
        }
        private void txtIcat2_Leave(object sender, EventArgs e)
        {

        }
        private void txtIcat2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btn_Srch_Cat2_Click(null, null);
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
        private void txtModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btn_Srch_Model_Click(null, null);
            if (e.KeyCode == Keys.Enter) TextBoxItem.Focus();
        }
        private void txtModel_Leave(object sender, EventArgs e)
        {

        }
        private void txtModel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btn_Srch_Model_Click(null, null);
        }
    }
}
