using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class SupWarrantyClaimDef : Base
    {
        List<Service_supp_claim_itm> _lst = new List<Service_supp_claim_itm>();
        List<SCV_SUPP_CLAIM_REC> _lstamt = new List<SCV_SUPP_CLAIM_REC>();
        public SupWarrantyClaimDef()
        {
            InitializeComponent();
        }

        private void btn_srch_warr_Click(object sender, EventArgs e)
        {
            txtSupplier_DoubleClick(null, null);
        }
        CommonSearch.CommonSearch _commonSearch = null;
        private void txtSupplier_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor; _commonSearch = new CommonSearch.CommonSearch(); _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(_commonSearch.SearchParams, null, null);
                _commonSearch.dvResult.DataSource = _result; _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtSupplier;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default; _commonSearch.ShowDialog();
                txtSupplier.Select();
            }
            catch (Exception ex) { }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SalesOrder:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "SO" + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.InvoiceWithDate:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.NIC:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    { paramsText.Append(seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + "SEX" + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    { paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + CommonUIDefiniton.PayMode.ADVAN.ToString() + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Group_Sale:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + 1 + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    { paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.AvailableGiftVoucher:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "ITEM" + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.ExchangeINDocument:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator); break; }

                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.AcJobNo:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GetItmByTypeNew:
                    {
                        paramsText.Append("V" + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void txtSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtSupplier_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                load_supplier_det();
                btnSave.Enabled = true;
                dgvSupplierWarClaim.DataSource = null;
                
            }
        }

        private void load_details()
        {
            //List<Service_supp_claim_itm> Servicesuppclaimitm = new List<Service_supp_claim_itm>();
            //Servicesuppclaimitm = CHNLSVC.CustService.getSupClaimItems(BaseCls.GlbUserComCode, txtSupplier.Text, null);
            //if (Servicesuppclaimitm.Count > 0)
            //{
            //    BindingSource _source = new BindingSource();
            //    _source.DataSource = Servicesuppclaimitm;
            //    dgvSupplierWarClaim.AutoGenerateColumns = false;
            //    dgvSupplierWarClaim.DataSource = _source;
            //}
            //else
            //{
            //    dgvSupplierWarClaim.DataSource = null;
            //}
            // Nadeeka 25-06-2015
            _lst = new List<Service_supp_claim_itm>();

            _lst = CHNLSVC.CustService.getSupClaimItems(BaseCls.GlbUserComCode, txtSupplier.Text, null);
            if (_lst.Count > 0)
            {
                BindingSource _source = new BindingSource();
                _source.DataSource = _lst;
                dgvSupplierWarClaim.AutoGenerateColumns = false;
                dgvSupplierWarClaim.DataSource = _source;
            }
            else
            {
                dgvSupplierWarClaim.DataSource = null;
            }
        }



        private void txtSupplier_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSupplier.Text))
            {
                DataTable SUP_DET = CHNLSVC.CustService.GetSupplierDetails(txtSupplier.Text, BaseCls.GlbUserComCode);
                if (SUP_DET.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid supplier code", "Supplier Warranty Claim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSupplier.Focus();
                }
            }
        }

        private void load_supplier_det()
        {
            if (!string.IsNullOrEmpty(txtSupplier.Text))
            {
                DataTable SUP_DET = CHNLSVC.CustService.GetSupplierDetails(txtSupplier.Text, BaseCls.GlbUserComCode);
                if (SUP_DET.Rows.Count > 0)
                {
                    DataRow dr;
                    dr = SUP_DET.Rows[0];
                    lblsupName.Text = dr["MBE_NAME"].ToString();
                    lblsupAddress.Text = dr["MBE_ADD1"].ToString() + dr["MBE_ADD2"].ToString();
                    lblsupEmail.Text = dr["MBE_EMAIL"].ToString();
                }
            }
        }

        private void ddlClaimItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.Text == "Brand")
            {
                // txtJobNo.ReadOnly = false;
                btnBrand.Enabled = true;
                //  txtCate1.ReadOnly = true;
                btnCate1.Enabled = false;
                //   txtCate2.ReadOnly = true;
                btnCate2.Enabled = false;
                txtCate1.Text = "";
                txtCate2.Text = "";
            }
            else if (ddlType.Text == "Cat 1")
            {
                //  txtCate1.ReadOnly = false;
                btnCate1.Enabled = true;
                //  txtCate2.ReadOnly = true;
                btnCate2.Enabled = false;
                //   txtJobNo.ReadOnly = false;
                btnBrand.Enabled = true;
            }
            else if (ddlType.Text == "Cat 2")
            {
                //   txtCate1.ReadOnly = false;
                btnCate1.Enabled = true;
                //  txtCate2.ReadOnly = false;
                btnCate2.Enabled = true;
                //  txtJobNo.ReadOnly = false;
                btnBrand.Enabled = true;
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
                _CommonSearch.obj_TragetTextBox = txtJobNo;
                _CommonSearch.txtSearchbyword.Text = txtJobNo.Text;
                _CommonSearch.ShowDialog();
                txtJobNo.Focus();
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

        private void btnCate1_Click(object sender, EventArgs e)
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

        private void btnCate2_Click(object sender, EventArgs e)
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

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text) || string.IsNullOrEmpty(txtCate1.Text) || string.IsNullOrEmpty(txtCate2.Text))
            {
                MessageBox.Show("Please select the Brand/Category 1/Category 2", "Supplier Warranty Claim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Brand");
            dt.Columns.Add("Cat1");
            dt.Columns.Add("Cat2");
            DataRow _newRow = dt.NewRow();
            _newRow["Brand"] = txtJobNo.Text;
            _newRow["Cat1"] = txtCate1.Text;
            _newRow["Cat2"] = txtCate2.Text;
            dt.Rows.Add(_newRow);

            dvgClaimItemLoad.AutoGenerateColumns = false;
            dvgClaimItemLoad.DataSource = dt;

            foreach (DataGridViewRow gr in dvgClaimItemLoad.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dvgClaimItemLoad.Rows[gr.Index].Cells[0];
                chk.Value = "true";
            }

        }

        private void txtClaimSupplier_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor; _commonSearch = new CommonSearch.CommonSearch(); _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(_commonSearch.SearchParams, null, null);
                _commonSearch.dvResult.DataSource = _result; _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtClaimSupplier;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default; _commonSearch.ShowDialog();
                txtClaimSupplier.Select();
            }
            catch (Exception ex) { }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtClaimSupplier_DoubleClick(null, null);
        }

        private void btnAddtogrid_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtClaimSupplier.Text))
                    lstSupplier.Items.Add(txtClaimSupplier.Text.ToString());


                foreach (ListViewItem Item in lstSupplier.Items)
                {
                    Item.Checked = true;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            // DataTable dt = new DataTable();
            // dt.Clear();
            // dt.Columns.Add("SSP_SUPP");
            // dt.Columns.Add("Brand");
            // dt.Columns.Add("Cat1");
            // dt.Columns.Add("Cat2");
            // dt.Columns.Add("External");
            // dt.Columns.Add("UWarrenty");
            // dt.Columns.Add("OWarrenty");
            // dt.Columns.Add("byItem");
            // dt.Columns.Add("byCash");
            // dt.Columns.Add("ClmForm");
            //DataRow _newRow = dt.NewRow();
            // foreach (DataGridViewRow rowClmItm in dvgClaimItemLoad.Rows)
            // {
            //     string brand = rowClmItm.Cells["ItemBrand"].ToString();
            //     foreach (ListViewItem Item in lstSupplier.Items)
            //     {
            //          _newRow = dt.NewRow();
            //         _newRow["SSP_SUPP"] = txtSupplier.Text;
            //         _newRow["Brand"] = dvgClaimItemLoad.Rows[rowClmItm.Index].Cells["ItemBrand"].Value.ToString(); // rowClmItm.Cells["ItemBrand"].ToString();
            //         _newRow["Cat1"] = rowClmItm.Cells["ItemCate1"].ToString();
            //         _newRow["Cat2"] = rowClmItm.Cells["ItemCate2"].ToString();
            //         if (chkExternlItem.Checked == true) _newRow["External"] = "YES";
            //         else _newRow["External"] = "NO";
            //         if (chkUnderWarrnty.Checked == true) _newRow["UWarrenty"] = "YES";
            //         else _newRow["UWarrenty"] = "NO";
            //         if (chkOvrWrrnty.Checked == true) _newRow["OWarrenty"] = "YES";
            //         else _newRow["OWarrenty"] = "NO";
            //         if (chkItem.Checked == true) _newRow["byItem"] = "YES";
            //         else _newRow["byItem"] = "NO";
            //         if (chkCash.Checked == true) _newRow["byCash"] = "YES";
            //         else _newRow["byCash"] = "NO";
            //         _newRow["ClmForm"] = Item.Text;
            //         dt.Rows.Add(_newRow);
            //     }

            // }
            if (lstSupplier.Items.Count == 0)
            {
                MessageBox.Show("Please select the supplier", "Supplier Warranty Claim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dvgClaimItemLoad.Rows.Count == 0)
            {
                MessageBox.Show("Please select the items(s)", "Supplier Warranty Claim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(txtcredprd.Text))
            {
                txtcredprd.Text = "0";
            }
            foreach (DataGridViewRow rowClmItm in dvgClaimItemLoad.Rows)
            {
                string brand = rowClmItm.Cells["ItemBrand"].ToString();
                foreach (ListViewItem Item in lstSupplier.Items)
                {

                    Service_supp_claim_itm _supclaim = new Service_supp_claim_itm();
                    _supclaim.SSC_BRND = dvgClaimItemLoad.Rows[rowClmItm.Index].Cells["ItemBrand"].Value.ToString();
                    if (chkCash.Checked == true)
                        _supclaim.SSC_CASH_ALW = 1;
                    else
                        _supclaim.SSC_CASH_ALW = 0;
                    _supclaim.SSC_ACT = true;
                    _supclaim.SSC_CAT1 = dvgClaimItemLoad.Rows[rowClmItm.Index].Cells["ItemCate1"].Value.ToString();
                    _supclaim.SSC_CAT2 = dvgClaimItemLoad.Rows[rowClmItm.Index].Cells["ItemCate2"].Value.ToString();
                    _supclaim.SSC_CLAIM_SUPP = Item.Text;
                    if (chkExternlItem.Checked == true)
                        _supclaim.SSC_EXITM_ALW = 1;
                    else
                        _supclaim.SSC_EXITM_ALW = 0;

                    if (chkItem.Checked == true)
                        _supclaim.SSC_ITM_ALW = 1;
                    else
                        _supclaim.SSC_ITM_ALW = 0;

                    if (chkOvrWrrnty.Checked == true)
                        _supclaim.SSC_OVWARR_ALW = 1;
                    else
                        _supclaim.SSC_OVWARR_ALW = 0;

                    _supclaim.SSC_SUPP = txtSupplier.Text;
                    _supclaim.SSC_TP = "S";

                    if (chkUnderWarrnty.Checked == true)
                        _supclaim.SSC_UNWARR_ALW = 1;
                    else
                        _supclaim.SSC_UNWARR_ALW = 0;

                    _supclaim.SSC_CREDIT_PRD = Convert.ToInt32(txtcredprd.Text);
                    _lst.Add(_supclaim);
                }

            }

            BindingSource _source = new BindingSource();
            _source.DataSource = _lst;
            dgvSupplierWarClaim.AutoGenerateColumns = false;
            dgvSupplierWarClaim.DataSource = _source;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvSupplierWarClaim.Rows.Count == 0)
            {
                MessageBox.Show("Cannot save. Details not found !", "Supplier Warranty Claim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            List<Service_supp_claim_itm> _listSupClaim = new List<Service_supp_claim_itm>();

            if (MessageBox.Show("Are you sure ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            foreach (DataGridViewRow dr in dgvSupplierWarClaim.Rows)
            {
                //if (dr.Cells["IsSelect"].Value.ToString() == "1")
                //{

            
                Service_supp_claim_itm supClaim = new Service_supp_claim_itm();
                supClaim.SSC_COM = BaseCls.GlbUserComCode;
                supClaim.SSC_SUPP = dr.Cells["SSC_SUPP"].Value.ToString();
                supClaim.SSC_TP = "S";
                supClaim.SSC_BRND = dr.Cells["SSC_BRND"].Value.ToString();
                supClaim.SSC_CAT1 = dr.Cells["SSC_CAT1"].Value.ToString();
                supClaim.SSC_CAT2 = dr.Cells["SSC_CAT2"].Value.ToString();
                supClaim.SSC_ITM_ALW = Convert.ToInt32(dr.Cells["SSC_ITM_ALW"].Value);
                supClaim.SSC_CASH_ALW = Convert.ToInt32(dr.Cells["SSC_CASH_ALW"].Value);
                supClaim.SSC_EXITM_ALW = Convert.ToInt32(dr.Cells["SSC_EXITM_ALW"].Value);
                supClaim.SSC_UNWARR_ALW = Convert.ToInt32(dr.Cells["SSC_UNWARR_ALW"].Value);
                supClaim.SSC_OVWARR_ALW = Convert.ToInt32(dr.Cells["SSC_OVWARR_ALW"].Value);
                supClaim.SSC_CLAIM_SUPP = dr.Cells["SSC_CLAIM_SUPP"].Value.ToString();
                supClaim.SSC_ACT = true;
                supClaim.SSC_CRE_BY = BaseCls.GlbUserID;
                supClaim.SSC_MOD_BY = BaseCls.GlbUserID;
                supClaim.SSC_CREDIT_PRD = Convert.ToInt32(dr.Cells["creprd"].Value);
                supClaim.SSC_SEQ = Convert.ToInt32(dr.Cells["creprd"].Value);
                supClaim.SSC_ACT = Convert.ToBoolean(dr.Cells["status"].Value);
                _listSupClaim.Add(supClaim);
                //}

            }

            string _msg = "";
            int _eff = CHNLSVC.CustService.Save_Supplier_Claim_Itm(_listSupClaim,_lstamt, out _msg);
            MessageBox.Show("Successfully Saved", "SupWarClaim", MessageBoxButtons.OK, MessageBoxIcon.Information);
            clear();

        }

        private void clear()
        {
            btnSave.Enabled = true;
            SupWarrantyClaimDef formnew = new SupWarrantyClaimDef();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

            btnSave.Enabled = true;
            SupWarrantyClaimDef formnew = new SupWarrantyClaimDef();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void dgvSupplierWarClaim_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnView_Click(object sender, EventArgs e)
        {


            btnSave.Enabled = false;
        }

        private void btnClrItm_Click(object sender, EventArgs e)
        {
            dvgClaimItemLoad.DataSource = null;
        }

        private void btnClrSup_Click(object sender, EventArgs e)
        {
            lstSupplier.Clear();
        }

        private void txtJobNo_DoubleClick(object sender, EventArgs e)
        {
            btnBrand_Click(null, null);
        }

        private void txtCate1_DoubleClick(object sender, EventArgs e)
        {
            btnCate1_Click(null, null);
        }

        private void txtCate2_DoubleClick(object sender, EventArgs e)
        {
            btnCate2_Click(null, null);
        }

        private void txtJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnBrand_Click(null, null);
        }

        private void txtCate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnCate1_Click(null, null);
        }

        private void txtCate2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnCate2_Click(null, null);
        }

        private void txtClaimSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                txtClaimSupplier_DoubleClick(null, null);
        }

        private void btnView_Click_1(object sender, EventArgs e)
        {
            load_details();
           // btnSave.Enabled = false;
        }

        private void btnAddAmt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MessageBox.Show("Please select the charge code", "Supplier Warranty Claim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtAmt.Text))
            {
                MessageBox.Show("Please enter amount", "Supplier Warranty Claim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dtpTo.Value.Date < dtpFrom.Value.Date )
            {
                MessageBox.Show("To date must be higher than from date", "Supplier Warranty Claim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            if (chkCash.Checked==false)
            {
                MessageBox.Show("This definition only need for By Cash option", "Supplier Warranty Claim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (_lstamt != null)
            {
                SCV_SUPP_CLAIM_REC result = _lstamt.Find(x => x.Scc_code == txtCode.Text && x.Scc_fromdate == dtpFrom.Value.Date && x.Scc_todate == dtpTo.Value.Date);
                if (result != null)
                {
                    MessageBox.Show("This record already exist", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
           else
            {
                _lstamt = new List<SCV_SUPP_CLAIM_REC>();
            }

            SCV_SUPP_CLAIM_REC _supitem = new SCV_SUPP_CLAIM_REC();
            _supitem.Scc_amt = Convert.ToDecimal(txtAmt.Text);
            _supitem.Scc_code = txtCode.Text;
            _supitem.Scc_fromdate = dtpFrom.Value.Date;
            _supitem.Scc_todate = dtpTo.Value.Date;
            _supitem.Scc_active = true;
            _lstamt.Add(_supitem);

            BindingSource _source = new BindingSource();
            _source.DataSource = _lstamt;
            gvAmt.AutoGenerateColumns = false;
            gvAmt.DataSource = _source;
        }

        private void gvAmt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //remove item line
                    _lstamt.RemoveAt(e.RowIndex);
                    gvAmt.AutoGenerateColumns = false;
                    gvAmt.DataSource = new List<SCV_SUPP_CLAIM_REC>();
                    gvAmt.DataSource = _lstamt;
                }
            }
        }

        private void dtpFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpTo.Focus();
            }

        }

        private void dtpTo_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtCode.Focus();
            }
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAmt.Focus();
            }

        }

        private void txtAmt_KeyDown(object sender, KeyEventArgs e)
        {  

            if (e.KeyCode == Keys.Enter)
            {
                btnAddAmt.Focus();
            }
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAmt_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByTypeNew);
                DataTable _result = CHNLSVC.CommonSearch.GetItemByTypeNew(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCode;
                _CommonSearch.ShowDialog();
                txtCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCode_DoubleClick(object sender, EventArgs e)
        {

        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtCode_Leave(object sender, EventArgs e)
        {    MasterItem _itemdetail = null;
        if (!string.IsNullOrEmpty(txtCode.Text))
        {
            _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtCode.Text);
            if (_itemdetail == null)
            {
                MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCode.Clear();
                txtCode.Focus();
                return;
            }
            if (_itemdetail.Mi_itm_tp != "V")
            {
                MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCode.Clear();
                txtCode.Focus();
                return;
            }
        }
        }

        private void txtAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) btnAddAmt.Focus ();  //Allow Enter   
        }

        private void dgvSupplierWarClaim_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //remove item line
                    _lst.RemoveAt(e.RowIndex);
                    dgvSupplierWarClaim.AutoGenerateColumns = false;
                    dgvSupplierWarClaim.DataSource = new List<Service_Req_Def>();
                    dgvSupplierWarClaim.DataSource = _lst;
                }
            }
        }

        private void chkCash_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
