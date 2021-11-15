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
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FF.WindowsERPClient.General
{
    public partial class IssueGiftVoucherAmenment : Base
    {
        private List<GiftVoucherItems> _gvItems = new List<GiftVoucherItems>();
        private List<GiftVoucherItems> _gvItemsIssue = new List<GiftVoucherItems>();
        private GiftVoucherPages _gvPageforItemIssue = new GiftVoucherPages();

        public IssueGiftVoucherAmenment()
        {
            InitializeComponent();
            pnlItemGvIssue.Size = new Size(638, 485);
            pnlCombination.Size = new Size(796, 486);
        }

        private void IssueGiftVoucherAmenment_Load(object sender, EventArgs e)
        {
            Clear_Data();
        }

        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.GetItmByType:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "G" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GiftVoucherByPage:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "A" + seperator + txtGVCode.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GvCategory:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        private void btnSearchGV_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbGvType.Text))
                {
                    MessageBox.Show("Please select type of gift voucher.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbGvType.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                DataTable _result = CHNLSVC.CommonSearch.GetItemByType(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtGVCode;
                _CommonSearch.ShowDialog();
                txtGVCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtGVCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbGvType.Text))
                {
                    MessageBox.Show("Please select type of gift voucher.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbGvType.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                DataTable _result = CHNLSVC.CommonSearch.GetItemByType(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtGVCode;
                _CommonSearch.ShowDialog();
                txtGVCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtGVCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(cmbGvType.Text))
                    {
                        MessageBox.Show("Please select type of gift voucher.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbGvType.Focus();
                        return;
                    }

                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemByType(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtGVCode;
                    _CommonSearch.ShowDialog();
                    txtGVCode.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    cmbGvBook.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtGVCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtGVCode.Text)) return;

                if (string.IsNullOrEmpty(cmbGvType.Text))
                {
                    MessageBox.Show("Please select gift voucher type.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbGvType.Focus();
                    return;
                }

                //updated by akila 2017/11/30
                MasterItem _gvItems = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtGVCode.Text.Trim());
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                //DataTable _result = CHNLSVC.CommonSearch.GetItemByType(SearchParams, "ITEM", txtGVCode.Text.Trim());

                //var _validate = _result.AsEnumerable().Where(x => x.Field<string>("ITEM") == txtGVCode.Text.Trim()).ToList();
                //if (_validate == null || _validate.Count <= 0)
                if (_gvItems == null || string.IsNullOrEmpty(_gvItems.Mi_cd) || _gvItems.Mi_itm_tp != "G")
                {
                    MessageBox.Show("Please select the valid Gift voucher code", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGVCode.Clear();
                    txtGVCode.Focus();
                    return;
                }

                cmbGvBook.DataSource = new DataTable();
                DataTable _book = CHNLSVC.Inventory.GetAvailableGvBooks(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbGvType.Text, "A", txtGVCode.Text.Trim(),null);

                if (_book != null)
                {
                    //var _final = (from _lst in _book
                    //select _lst.Gvp_book).ToList();
                    cmbGvBook.DataSource = _book;
                    cmbGvBook.ValueMember = "GVP_BOOK";
                    cmbGvBook.DisplayMember = "GVP_BOOK";

                    // cmbGvBook.DataSource = _final;
                }
                if (_book == null)
                {
                    MessageBox.Show("Please Select Gift Book No", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbGvType.Focus();
                    return;
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbGvBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                List<GiftVoucherPages> _tmpList = new List<GiftVoucherPages>();
                cmbTopg.DataSource = _tmpList;

                if (!IsNumeric(cmbGvBook.Text))
                {
                    return;
                }

                _tmpList = CHNLSVC.Inventory.GetAvailableGvPages(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbGvType.Text, "A", Convert.ToInt32(cmbGvBook.Text), txtGVCode.Text.Trim());


                if (_tmpList != null)
                {
                    cmbTopg.DataSource = _tmpList;
                    cmbTopg.ValueMember = "gvp_page";
                    cmbTopg.DisplayMember = "gvp_page";

                    AutoCompleteStringCollection _string = new AutoCompleteStringCollection();
                    if (_tmpList != null)
                    {
                        //var _lst = (from List<GiftVoucherPages> _l in _tmpList select _l).ToList();
                        //Parallel.ForEach(_lst, x =>
                        //{
                        //    _string.Add(x.Field<string>("gvp_page"));
                        //});


                        foreach (GiftVoucherPages _tmp in _tmpList)
                        {
                            _string.Add(_tmp.Gvp_page.ToString());
                        }

                        cmbTopg.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        cmbTopg.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmbTopg.AutoCompleteCustomSource = _string;
                    }

                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewPromoDet_Click(object sender, EventArgs e)
        {
            try
            {
                List<GiftVoucherPages> _tmpList = new List<GiftVoucherPages>();
                lblCusCode.Text = "";
                lblCusName.Text = "";
                lblAdd1.Text = "";
                lblAdd2.Text = "";
                lblContact.Text = "";
                lblValidFrom.Text = "";
                txtAmendNoofItems.Text = "0";

                _tmpList = CHNLSVC.Inventory.GetAvailableGvPagesRange(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbGvType.Text, "A", Convert.ToInt32(cmbGvBook.Text), txtGVCode.Text.Trim(), Convert.ToInt32(cmbTopg.Text), Convert.ToInt32(cmbTopg.Text));

                if (_tmpList == null)
                {
                    MessageBox.Show("Cannot find details.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (_tmpList.Count > 1)
                {
                    MessageBox.Show("Multiple records found.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (GiftVoucherPages _gv in _tmpList)
                {
                    if (string.IsNullOrEmpty(_gv.Gvp_cus_cd))
                    {
                        lblCusCode.Text = "CASH";
                    }
                    else
                    {
                        lblCusCode.Text = _gv.Gvp_cus_cd;
                    }
                    lblCusName.Text = _gv.Gvp_cus_name;
                    lblAdd1.Text = _gv.Gvp_cus_add1;
                    lblAdd2.Text = _gv.Gvp_cus_add2;
                    lblContact.Text = _gv.Gvp_cus_mob;
                    lblValidFrom.Text = Convert.ToDateTime(_gv.Gvp_valid_from).ToShortDateString();
                    dtpValidTo.Value = _gv.Gvp_valid_to;
                    txtAmendNoofItems.Text = _gv.Gvp_noof_itm.ToString("n");
                }

                _gvItems = new List<GiftVoucherItems>();
                _gvItems = CHNLSVC.Inventory.GetGiftVoucherAllItems(cmbGvBook.Text, cmbTopg.Text, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);

                dgvGvItems.AutoGenerateColumns = false;
                dgvGvItems.DataSource = new List<GiftVoucherItems>();
                dgvGvItems.DataSource = _gvItems;

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear_Data();
            if (pnlCombination.Visible) ClearGvSetupScreen();
        }

        private void Clear_Data()
        {
            _gvItems = new List<GiftVoucherItems>();
            _gvItemsIssue = new List<GiftVoucherItems>();
            _gvPageforItemIssue = new GiftVoucherPages();

            txtGVCode.Text = "";
            cmbGvBook.DataSource = new DataTable();
            cmbTopg.DataSource = new DataTable();
            lblCusCode.Text = "";
            lblCusName.Text = "";
            lblAdd1.Text = "";
            lblAdd2.Text = "";
            lblContact.Text = "";
            lblValidFrom.Text = "";
            dtpValidTo.Value = Convert.ToDateTime(DateTime.Today).Date;
            dgvGvItems.AutoGenerateColumns = false;
            dgvGvItems.DataSource = new List<GiftVoucherItems>();
            Clear_IssueItems();
            txtGVCode.Focus();
        }

        private void cmbGvBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbTopg.Focus();
            }
        }

        private void cmbTopg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnViewPromoDet.Focus();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;
                Int32 _amendQty = 0;

                if (string.IsNullOrEmpty(txtGVCode.Text))
                {
                    MessageBox.Show("Please select the valid Gift voucher code", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGVCode.Clear();
                    txtGVCode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(lblCusCode.Text))
                {
                    MessageBox.Show("Voucher details not found.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnViewPromoDet.Focus();
                    return;
                }

                if (Convert.ToDateTime(lblValidFrom.Text).Date > Convert.ToDateTime(dtpValidTo.Value).Date)
                {
                    MessageBox.Show("Invalid date range.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpValidTo.Focus();
                    return;
                }

                if (cmbGvType.Text == "ITEM")
                {
                    if (string.IsNullOrEmpty(txtAmendNoofItems.Text))
                    {
                        MessageBox.Show("Pls. enter entitle no of items.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAmendNoofItems.Focus();
                        return;
                    }

                    _amendQty = Convert.ToInt32(txtAmendNoofItems.Text);
                }



                row_aff = (Int32)CHNLSVC.Sales.UpdateGiftVoucherValidDate(Convert.ToDateTime(dtpValidTo.Value).Date, BaseCls.GlbUserID, cmbGvBook.Text, cmbTopg.Text, BaseCls.GlbUserDefProf, txtGVCode.Text, BaseCls.GlbUserComCode, null, _amendQty);


                if (row_aff == 1)
                {
                    MessageBox.Show("Succsufully amanded.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        MessageBox.Show("Fail", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtGVCode.Text))
                {
                    MessageBox.Show("Please select the valid Gift voucher code", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGVCode.Clear();
                    txtGVCode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(lblCusCode.Text))
                {
                    MessageBox.Show("Voucher details not found.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnViewPromoDet.Focus();
                    return;
                }

                if (Convert.ToDateTime(lblValidFrom.Text).Date > Convert.ToDateTime(dtpValidTo.Value).Date)
                {
                    MessageBox.Show("Invalid date range.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpValidTo.Focus();
                    return;
                }


                row_aff = (Int32)CHNLSVC.Sales.UpdateGiftVoucherValidDate(Convert.ToDateTime(dtpValidTo.Value).Date, BaseCls.GlbUserID, cmbGvBook.Text, cmbTopg.Text, BaseCls.GlbUserDefProf, txtGVCode.Text, BaseCls.GlbUserComCode, "C", 0);


                if (row_aff == 1)
                {
                    MessageBox.Show("Succsufully cancelled.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        MessageBox.Show("Fail", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvGvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                string _book = Convert.ToString(dgvGvItems.Rows[e.RowIndex].Cells["col_book"].Value);
                string _page = Convert.ToString(dgvGvItems.Rows[e.RowIndex].Cells["col_pg"].Value);
                string _itm = Convert.ToString(dgvGvItems.Rows[e.RowIndex].Cells["col_Item"].Value);
                Int16 _act = Convert.ToInt16(dgvGvItems.Rows[e.RowIndex].Cells["col_Act"].Value);

                if (_act == 1)
                {
                    if (MessageBox.Show("Do you want to inactive this item for above voucher ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        _act = 0;
                    }
                }
                else if (_act == 0)
                {
                    if (MessageBox.Show("Do you want to active this item for above voucher ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        _act = 1;
                    }
                }

                row_aff = (Int32)CHNLSVC.Sales.UpdateGiftVoucherStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbGvBook.Text, cmbTopg.Text, txtGVCode.Text, _act);


                if (row_aff == 1)
                {
                    MessageBox.Show("Succsufully amanded.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();

                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        MessageBox.Show("Fail", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                    }
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIssueNewItemGv_Click(object sender, EventArgs e)
        {
            if (pnlItemGvIssue.Visible == true)
            {
                pnlItemGvIssue.Visible = false;
                btnCategorySetup.Enabled = true;
            }
            else
            {
                pnlItemGvIssue.Visible = true;
                btnCategorySetup.Enabled = false;
            }
        }

        private void btnSearchItmGv_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                DataTable _result = CHNLSVC.CommonSearch.GetItemByType(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemGvCode;
                _CommonSearch.ShowDialog();
                txtItemGvCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItemGvCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                DataTable _result = CHNLSVC.CommonSearch.GetItemByType(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemGvCode;
                _CommonSearch.ShowDialog();
                txtItemGvCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItemGvCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemByType(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtItemGvCode;
                    _CommonSearch.ShowDialog();
                    txtItemGvCode.Select();
                }
                else
                {
                    cmbItemGvBook.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItemGvCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItemGvCode.Text)) return;

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                DataTable _result = CHNLSVC.CommonSearch.GetItemByType(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("ITEM") == txtItemGvCode.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid Gift voucher code", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemGvCode.Clear();
                    txtItemGvCode.Focus();
                    return;
                }

                cmbItemGvBook.DataSource = new DataTable();
                DataTable _book = CHNLSVC.Inventory.GetAvailableGvBooks(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ITEM", "P", txtItemGvCode.Text.Trim(),null);

                if (_book != null)
                {
                    //var _final = (from _lst in _book
                    //select _lst.Gvp_book).ToList();
                    cmbItemGvBook.DataSource = _book;
                    cmbItemGvBook.ValueMember = "GVP_BOOK";
                    cmbItemGvBook.DisplayMember = "GVP_BOOK";

                    // cmbGvBook.DataSource = _final;
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbItemGvBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                List<GiftVoucherPages> _tmpList = new List<GiftVoucherPages>();
                txtItemGvPage.Text = "";
                lblPreFix.Text = "";
                lblIssueRef.Text = "";
                lblLine.Text = "";
                if (!IsNumeric(cmbItemGvBook.Text))
                {
                    return;
                }

                _tmpList = CHNLSVC.Inventory.GetAvailableGvPages(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ITEM", "P", Convert.ToInt32(cmbItemGvBook.Text), txtItemGvCode.Text.Trim());


                if (_tmpList != null)
                {
                    foreach (GiftVoucherPages tmp in _tmpList)
                    {
                        txtItemGvPage.Text = tmp.Gvp_page.ToString();
                        lblPreFix.Text = tmp.Gvp_gv_prefix.ToString();
                        lblIssueRef.Text = tmp.Gvp_ref.ToString();
                        lblLine.Text = tmp.Gvp_line.ToString();

                        goto L1;
                    }
                }
            L1: Int16 i = 0;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCusCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                    DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCusCode;
                    _CommonSearch.ShowDialog();
                    txtCusCode.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtCusName.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchItmCus_Click(object sender, EventArgs e)
        {
            try
            {

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCusCode;
                _CommonSearch.ShowDialog();
                txtCusCode.Select();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCusCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCusCode;
                _CommonSearch.ShowDialog();
                txtCusCode.Select();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCustomerDetails()
        {
            MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
            if (!string.IsNullOrEmpty(txtCusCode.Text))
            {
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCusCode.Text.Trim(), string.Empty, string.Empty, "C");


                if (_masterBusinessCompany.Mbe_cd != null)
                {

                    if (_masterBusinessCompany.Mbe_cd == "CASH")
                    {
                        txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
                        txtCusName.Text = "";
                        txtCusAdd1.Text = "";
                        txtCusAdd2.Text = "";
                        txtMobile.Text = "";
                        txtCusCode.ReadOnly = false;
                    }
                    else
                    {
                        txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
                        txtCusName.Text = _masterBusinessCompany.Mbe_name;
                        txtCusAdd1.Text = _masterBusinessCompany.Mbe_add1;
                        txtCusAdd2.Text = _masterBusinessCompany.Mbe_add2;
                        txtMobile.Text = _masterBusinessCompany.Mbe_mob;
                        txtCusName.ReadOnly = true;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid customer.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCusCode.Text = "";
                    txtCusName.Text = "";
                    txtCusAdd1.Text = "";
                    txtCusAdd2.Text = "";
                    txtMobile.Text = "";
                    txtCusCode.Focus();
                    return;
                }
            }
            else
            {
                txtCusName.Text = "";
                txtCusAdd1.Text = "";
                txtCusAdd2.Text = "";
                txtMobile.Text = "";

            }
        }

        private void txtCusCode_Leave(object sender, EventArgs e)
        {
            LoadCustomerDetails();
        }

        private void txtCusName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCusAdd1.Focus();
            }
        }

        private void txtCusAdd1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCusAdd2.Focus();
            }
        }

        private void txtCusAdd2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMobile.Focus();
            }
        }

        private void txtMobile_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMobile.Text))
            {
                if (!IsValidMobileOrLandNo(txtMobile.Text.Trim()))
                {
                    MessageBox.Show("Invalid mobile number.", "Mesage", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMobile.Text = "";
                    txtMobile.Focus();
                    return;
                }
            }
        }

        private void btnItemIssueClear_Click(object sender, EventArgs e)
        {
            Clear_IssueItems();
        }

        private void Clear_IssueItems()
        {
            txtItemGvCode.Text = "";
            cmbItemGvBook.DataSource = new DataTable();
            txtItemGvPage.Text = "";
            lblIssueRef.Text = "";
            lblPreFix.Text = "";
            lblLine.Text = "";
            txtCusCode.Text = "";
            txtCusName.Text = "";
            txtCusAdd1.Text = "";
            txtCusAdd2.Text = "";
            txtMobile.Text = "";
            dtpItemValidTo.Value = Convert.ToDateTime(DateTime.Now).Date;

            dgvIssueItemGv.AutoGenerateColumns = false;
            dgvIssueItemGv.DataSource = new List<GiftVoucherItems>();
            txtItemGvCode.Focus();
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtItem;
                    _CommonSearch.ShowDialog();
                    txtItem.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtQty.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem;
                _CommonSearch.ShowDialog();
                txtItem.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItem_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem;
                _CommonSearch.ShowDialog();
                txtItem.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    return;
                }
                else if (IsNumeric(txtQty.Text) == false)
                {
                    MessageBox.Show("Item Qty should be numeric.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Text = "";
                    txtQty.Focus();
                    return;
                }
                else if (Convert.ToInt16(txtQty.Text) < 0)
                {
                    MessageBox.Show("Item Qty cannot be minus.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Text = "";
                    txtQty.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                GiftVoucherItems _gvItemsIssuetmp = new GiftVoucherItems();
                if (string.IsNullOrEmpty(txtQty.Text))
                { MessageBox.Show("Please enter qty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information); txtQty.Text = ""; txtQty.Focus(); return; }

                if (string.IsNullOrEmpty(txtIssueCate.Text) && string.IsNullOrEmpty(txtItem.Text))
                { MessageBox.Show("Please select any of the category or item or both.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                bool _isBoth = false;

                if (!string.IsNullOrEmpty(txtIssueCate.Text) && !string.IsNullOrEmpty(txtItem.Text)) if (MessageBox.Show("Do you need to add both category and item?", "Add", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) _isBoth = true; else _isBoth = false;
                MasterItem _itm = new MasterItem();

                if (_isBoth)
                {
                    if (!string.IsNullOrEmpty(txtIssueCate.Text))
                    {
                        DataTable _tbl = CHNLSVC.General.GetGvCategory(BaseCls.GlbUserComCode, txtIssueCate.Text.Trim());
                        _tbl = _tbl.AsEnumerable().Where(x => x.Field<Int16>("gvctd_stus") == 1).OrderByDescending(x => x.Field<Int16>("gvctd_tp")).ToList().CopyToDataTable();
                        if (_tbl != null && _tbl.Rows.Count > 0)
                            foreach (DataRow r in _tbl.Rows)
                            {
                                string _item = r.Field<string>("gvctd_itm");
                                string _category = txtIssueCate.Text.Trim();
                                Int16 _verb = r.Field<Int16>("gvctd_tp");
                                _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                _gvItemsIssuetmp = new GiftVoucherItems();
                                _gvItemsIssuetmp.Gvi_act = true;
                                _gvItemsIssuetmp.Gvi_bal_qty = Convert.ToInt16(txtQty.Text);
                                _gvItemsIssuetmp.Gvi_book = Convert.ToInt32(cmbItemGvBook.Text);
                                _gvItemsIssuetmp.Gvi_com = BaseCls.GlbUserComCode;
                                _gvItemsIssuetmp.Gvi_itm = _item;
                                _gvItemsIssuetmp.Gvi_line = _gvItemsIssue.Count + 1;
                                _gvItemsIssuetmp.Gvi_page = Convert.ToInt32(txtItemGvPage.Text);
                                _gvItemsIssuetmp.Gvi_page_line = Convert.ToInt16(lblLine.Text);
                                _gvItemsIssuetmp.Gvi_pc = BaseCls.GlbUserDefProf;
                                _gvItemsIssuetmp.Gvi_pre_fix = lblPreFix.Text;
                                _gvItemsIssuetmp.Gvi_qty = Convert.ToInt32(txtQty.Text);
                                _gvItemsIssuetmp.Gvi_ref = lblIssueRef.Text;
                                _gvItemsIssuetmp.Gvi_tp = "CAT";
                                _gvItemsIssuetmp.Gvi_val = _category;
                                _gvItemsIssuetmp.Gvi_verb = _verb == 1 ? true : false;
                                _gvItemsIssuetmp.Verbdescription = _verb == 1 ? "AND" : "OR";
                                _gvItemsIssuetmp.Mi_longdesc = _itm.Mi_longdesc;
                                _gvItemsIssue.Add(_gvItemsIssuetmp);
                            }
                    }

                    if (!string.IsNullOrEmpty(txtItem.Text))
                    {
                        _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                        _gvItemsIssuetmp = new GiftVoucherItems();
                        _gvItemsIssuetmp.Gvi_act = true;
                        _gvItemsIssuetmp.Gvi_bal_qty = Convert.ToInt16(txtQty.Text);
                        _gvItemsIssuetmp.Gvi_book = Convert.ToInt32(cmbItemGvBook.Text);
                        _gvItemsIssuetmp.Gvi_com = BaseCls.GlbUserComCode;
                        _gvItemsIssuetmp.Gvi_itm = txtItem.Text;
                        _gvItemsIssuetmp.Gvi_line = _gvItemsIssue.Count + 1;
                        _gvItemsIssuetmp.Gvi_page = Convert.ToInt32(txtItemGvPage.Text);
                        _gvItemsIssuetmp.Gvi_page_line = Convert.ToInt16(lblLine.Text);
                        _gvItemsIssuetmp.Gvi_pc = BaseCls.GlbUserDefProf;
                        _gvItemsIssuetmp.Gvi_pre_fix = lblPreFix.Text;
                        _gvItemsIssuetmp.Gvi_qty = Convert.ToInt32(txtQty.Text);
                        _gvItemsIssuetmp.Gvi_ref = lblIssueRef.Text;
                        _gvItemsIssuetmp.Gvi_tp = "ITEM";
                        _gvItemsIssuetmp.Gvi_val = txtItem.Text.Trim();
                        _gvItemsIssuetmp.Gvi_verb = true;
                        _gvItemsIssuetmp.Verbdescription = "AND";
                        _gvItemsIssuetmp.Mi_longdesc = _itm.Mi_longdesc;
                        _gvItemsIssue.Add(_gvItemsIssuetmp);
                    }
                }
                else if (!string.IsNullOrEmpty(txtIssueCate.Text))
                {
                    DataTable _tbl = CHNLSVC.General.GetGvCategory(BaseCls.GlbUserComCode, txtIssueCate.Text.Trim());
                    _tbl = _tbl.AsEnumerable().Where(x => x.Field<Int16>("gvctd_stus") == 1).OrderBy(x => x.Field<Int16>("gvctd_tp")).ToList().CopyToDataTable();
                    if (_tbl != null && _tbl.Rows.Count > 0)
                        foreach (DataRow r in _tbl.Rows)
                        {
                            string _item = r.Field<string>("gvctd_itm");
                            string _category = txtIssueCate.Text.Trim();
                            Int16 _verb = r.Field<Int16>("gvctd_tp");
                            _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);

                            _gvItemsIssuetmp = new GiftVoucherItems();
                            _gvItemsIssuetmp.Gvi_act = true;
                            _gvItemsIssuetmp.Gvi_bal_qty = Convert.ToInt16(txtQty.Text);
                            _gvItemsIssuetmp.Gvi_book = Convert.ToInt32(cmbItemGvBook.Text);
                            _gvItemsIssuetmp.Gvi_com = BaseCls.GlbUserComCode;
                            _gvItemsIssuetmp.Gvi_itm = _item;
                            _gvItemsIssuetmp.Gvi_line = _gvItemsIssue.Count + 1;
                            _gvItemsIssuetmp.Gvi_page = Convert.ToInt32(txtItemGvPage.Text);
                            _gvItemsIssuetmp.Gvi_page_line = Convert.ToInt16(lblLine.Text);
                            _gvItemsIssuetmp.Gvi_pc = BaseCls.GlbUserDefProf;
                            _gvItemsIssuetmp.Gvi_pre_fix = lblPreFix.Text;
                            _gvItemsIssuetmp.Gvi_qty = Convert.ToInt32(txtQty.Text);
                            _gvItemsIssuetmp.Gvi_ref = lblIssueRef.Text;
                            _gvItemsIssuetmp.Gvi_tp = "CAT";
                            _gvItemsIssuetmp.Gvi_val = _category;
                            _gvItemsIssuetmp.Gvi_verb = _verb == 1 ? true : false;
                            _gvItemsIssuetmp.Verbdescription = _verb == 1 ? "AND" : "OR";
                            _gvItemsIssuetmp.Mi_longdesc = _itm.Mi_longdesc;
                            _gvItemsIssue.Add(_gvItemsIssuetmp);
                        }
                }
                else if (!string.IsNullOrEmpty(txtItem.Text))
                {
                    _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                    _gvItemsIssuetmp = new GiftVoucherItems();
                    _gvItemsIssuetmp.Gvi_act = true;
                    _gvItemsIssuetmp.Gvi_bal_qty = Convert.ToInt16(txtQty.Text);
                    _gvItemsIssuetmp.Gvi_book = Convert.ToInt32(cmbItemGvBook.Text);
                    _gvItemsIssuetmp.Gvi_com = BaseCls.GlbUserComCode;
                    _gvItemsIssuetmp.Gvi_itm = txtItem.Text;
                    _gvItemsIssuetmp.Gvi_line = _gvItemsIssue.Count + 1;
                    _gvItemsIssuetmp.Gvi_page = Convert.ToInt32(txtItemGvPage.Text);
                    _gvItemsIssuetmp.Gvi_page_line = Convert.ToInt16(lblLine.Text);
                    _gvItemsIssuetmp.Gvi_pc = BaseCls.GlbUserDefProf;
                    _gvItemsIssuetmp.Gvi_pre_fix = lblPreFix.Text;
                    _gvItemsIssuetmp.Gvi_qty = Convert.ToInt32(txtQty.Text);
                    _gvItemsIssuetmp.Gvi_ref = lblIssueRef.Text;
                    _gvItemsIssuetmp.Gvi_tp = "ITEM";
                    _gvItemsIssuetmp.Gvi_val = txtItem.Text.Trim();
                    _gvItemsIssuetmp.Gvi_verb = true;
                    _gvItemsIssuetmp.Verbdescription = "AND";
                    _gvItemsIssuetmp.Mi_longdesc = _itm.Mi_longdesc;
                    _gvItemsIssue.Add(_gvItemsIssuetmp);
                }

                dgvIssueItemGv.AutoGenerateColumns = false;
                dgvIssueItemGv.DataSource = new List<GiftVoucherItems>();
                dgvIssueItemGv.DataSource = _gvItemsIssue;

                txtItem.Text = "";
                txtQty.Text = "";
                txtItem.Focus();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnItemIssueSave_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                _gvPageforItemIssue = new GiftVoucherPages();
                if (string.IsNullOrEmpty(txtItemGvCode.Text))
                {
                    MessageBox.Show("Please select voucher item.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemGvCode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtItemGvPage.Text))
                {
                    MessageBox.Show("Please select voucher page.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemGvPage.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(lblIssueRef.Text))
                {
                    MessageBox.Show("Some details cannot capture.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemGvCode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCusCode.Text))
                {
                    MessageBox.Show("Please enter customer code.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCusCode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCusName.Text))
                {
                    MessageBox.Show("Please enter customer name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCusName.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCusAdd1.Text))
                {
                    MessageBox.Show("Please enter customer address.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCusAdd1.Focus();
                    return;
                }

                if (_gvItemsIssue.Count == 0)
                {
                    MessageBox.Show("Cannot find item details.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtfrom.Text))
                {
                    MessageBox.Show("Please select the from detail", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                _gvPageforItemIssue.Gvp_com = BaseCls.GlbUserComCode;
                _gvPageforItemIssue.Gvp_pc = BaseCls.GlbUserDefProf;
                _gvPageforItemIssue.Gvp_gv_cd = txtItemGvCode.Text;
                _gvPageforItemIssue.Gvp_book = Convert.ToInt32(cmbItemGvBook.Text);
                _gvPageforItemIssue.Gvp_page = Convert.ToInt32(txtItemGvPage.Text);
                _gvPageforItemIssue.Gvp_cus_cd = txtCusCode.Text;
                _gvPageforItemIssue.Gvp_cus_name = txtCusName.Text;
                _gvPageforItemIssue.Gvp_cus_add1 = txtCusAdd1.Text;
                _gvPageforItemIssue.Gvp_cus_add2 = txtCusAdd2.Text;
                _gvPageforItemIssue.Gvp_cus_mob = txtMobile.Text;
                _gvPageforItemIssue.Gvp_valid_from = Convert.ToDateTime(DateTime.Now).Date;
                _gvPageforItemIssue.Gvp_valid_to = Convert.ToDateTime(dtpItemValidTo.Value).Date;
                _gvPageforItemIssue.Gvp_oth_ref = "GV ITEM ISSUE";
                _gvPageforItemIssue.Gvp_issue_by = BaseCls.GlbUserID;
                _gvPageforItemIssue.Gvp_mod_by = BaseCls.GlbUserID;
                _gvPageforItemIssue.Gvp_amt = 0;
                _gvPageforItemIssue.Gvp_from = txtfrom.Text.Trim();
                _gvPageforItemIssue.Gvp_stus = "A";

                row_aff = (Int32)CHNLSVC.Sales.IssueGvItems(_gvPageforItemIssue, _gvItemsIssue);


                if (row_aff == 1)
                {
                   if(MessageBox.Show("Successfully allocated. Do you need to print this now?", "Voucher Amend", MessageBoxButtons.YesNo, MessageBoxIcon.Information)==DialogResult.Yes)
                    {
                        clsInventoryRep obj = new clsInventoryRep();
                        BaseCls.GlbReportProfit = BaseCls.GlbUserDefProf;
                        BaseCls.GlbReportBook = Convert.ToInt16(cmbItemGvBook.SelectedValue);
                        BaseCls.GlbReportFromPage = Convert.ToInt32(txtItemGvPage.Text.Trim());
                        BaseCls.GlbReportToPage = Convert.ToInt32(txtItemGvPage.Text.Trim());
                        obj.GiftVoucherPrintReport();
                    }
                    Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        MessageBox.Show("Fail", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpItemValidTo.Focus();
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnApply.Focus();
            }
        }

        private void txtItem_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItem.Text))
                {
                    MasterItem _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(BaseCls.GlbUserComCode, txtItem.Text, 1);

                    if (_masterItemDetails.Mi_cd == null)
                    {
                        MessageBox.Show("Invalid item.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItem.Text = "";
                        txtItem.Focus();
                        return;
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvIssueItemGv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9 && e.RowIndex != -1)
            {
                if (MessageBox.Show("Do you want to remove selected registration details ?", "Issue gift voucher", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (_gvItemsIssue == null || _gvItemsIssue.Count == 0) return;

                    string _item = dgvIssueItemGv.Rows[e.RowIndex].Cells["gvi_itm"].Value.ToString();
                    Int32 _book = Convert.ToInt32(dgvIssueItemGv.Rows[e.RowIndex].Cells["gvi_book"].Value);
                    Int32 _page = Convert.ToInt32(dgvIssueItemGv.Rows[e.RowIndex].Cells["gvi_page"].Value);



                    List<GiftVoucherItems> _temp = new List<GiftVoucherItems>();
                    _temp = _gvItemsIssue;

                    _temp.RemoveAll(x => x.Gvi_itm == _item && x.Gvi_book == _book && x.Gvi_page == _page);
                    _gvItemsIssue = _temp;


                    dgvIssueItemGv.AutoGenerateColumns = false;
                    dgvIssueItemGv.DataSource = new List<GiftVoucherItems>();
                    dgvIssueItemGv.DataSource = _gvItemsIssue;

                }
            }
        }

        private void txtAmendNoofItems_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAmendNoofItems.Text))
                {
                    return;
                }
                else if (IsNumeric(txtAmendNoofItems.Text) == false)
                {
                    MessageBox.Show("Entitle Qty should be numeric.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAmendNoofItems.Text = "";
                    txtAmendNoofItems.Focus();
                    return;
                }
                else if (Convert.ToInt16(txtAmendNoofItems.Text) < 0)
                {
                    MessageBox.Show("Entitle Qty cannot be minus.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAmendNoofItems.Text = "";
                    txtAmendNoofItems.Focus();
                    return;
                }



                if (Convert.ToInt16(txtAmendNoofItems.Text) > dgvGvItems.Rows.Count)
                {
                    MessageBox.Show("Entile qty cannot exceed selected items.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAmendNoofItems.Text = "";
                    txtAmendNoofItems.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGiftVoucher_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbGvType.Text))
                {
                    MessageBox.Show("Please select type of gift voucher.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbGvType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtGVCode.Text))
                {
                    MessageBox.Show("Please select gift voucher code.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGVCode.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GiftVoucherByPage);
                DataTable _result = CHNLSVC.Inventory.SearchGiftVoucherByPage(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtGiftVoucher;
                _CommonSearch.ShowDialog();
                txtGiftVoucher.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtGiftVoucher_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(cmbGvType.Text))
                    {
                        MessageBox.Show("Please select type of gift voucher.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbGvType.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtGVCode.Text))
                    {
                        MessageBox.Show("Please select gift voucher code.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtGVCode.Focus();
                        return;
                    }

                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GiftVoucherByPage);
                    DataTable _result = CHNLSVC.Inventory.SearchGiftVoucherByPage(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtGiftVoucher;
                    _CommonSearch.ShowDialog();
                    txtGiftVoucher.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    cmbTopg.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtGiftVoucher_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(cmbGvType.Text))
                {
                    MessageBox.Show("Please select type of gift voucher.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbGvType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtGVCode.Text))
                {
                    MessageBox.Show("Please select gift voucher code.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGVCode.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GiftVoucherByPage);
                DataTable _result = CHNLSVC.Inventory.SearchGiftVoucherByPage(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtGiftVoucher;
                _CommonSearch.ShowDialog();
                txtGiftVoucher.Select();


            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtGiftVoucher_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtGiftVoucher.Text))
                {
                    return;
                }

                lblCusCode.Text = "";
                lblCusName.Text = "";
                lblAdd1.Text = "";
                lblAdd2.Text = "";
                lblContact.Text = "";
                lblValidFrom.Text = "";
                
                List<GiftVoucherPages> _tmp = CHNLSVC.Inventory.GetAllGvbyPages(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "A", txtGVCode.Text.Trim(), Convert.ToInt32(txtGiftVoucher.Text));
                

               

                gvMultipleItem.AutoGenerateColumns = false;
                gvMultipleItem.DataSource = new List<GiftVoucherPages>();
                gvMultipleItem.DataSource = _tmp;

                if (_tmp == null)
                {
                    List<GiftVoucherPages> _tmp1 = CHNLSVC.Inventory.GetAllGvbyPages(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "C", txtGVCode.Text.Trim(), Convert.ToInt32(txtGiftVoucher.Text));

                    if (_tmp1 != null)
                    {
                        MessageBox.Show("The entered voucher is already canceled.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtGiftVoucher.Focus();
                        return;
                    }
                    else if (_tmp1 == null)
                    {
                        List<GiftVoucherPages> _tmp2 = CHNLSVC.Inventory.GetAllGvbyPages(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "F", txtGVCode.Text.Trim(), Convert.ToInt32(txtGiftVoucher.Text));
                        if (_tmp2 != null)
                        {
                            MessageBox.Show("The entered voucher is already redeemed.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtGiftVoucher.Focus();
                            return;
                        }
                    }
                  

                    else
                    {
                       


                        MessageBox.Show("Invalid voucher number.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtGiftVoucher.Focus();
                        return;
                    }
                    if (_tmp1 == null) // add by tharanga 2017/09/30
                    {
                        string com = "";
                        List<GiftVoucherPages> _tmpnnew = CHNLSVC.Inventory.GetGiftVoucherPages(null, int.Parse(txtGiftVoucher.Text));

                        //updated by akila 2017/11/30
                        if (_tmpnnew != null && _tmpnnew.Count > 0)
                        {
                            List<GiftVoucherPages> _tmpnnew1 = new List<GiftVoucherPages>();
                            _tmpnnew1 = _tmpnnew.Where(x => x.Gvp_gv_cd == txtGVCode.Text.Trim()).ToList();
                            if (_tmpnnew1 == null && _tmpnnew1.Count == 0)
                            {
                                string _voucher = _tmpnnew.FirstOrDefault().Gvp_gv_cd;
                                MessageBox.Show("Invalid page number. Entered page number belongs to voucher code " + _voucher, "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            else
                            {
                                gvMultipleItem.AutoGenerateColumns = false;
                                gvMultipleItem.DataSource = new List<GiftVoucherPages>();
                                gvMultipleItem.DataSource = _tmpnnew1;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid page number. Voucher details not found", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        //foreach (GiftVoucherPages item in _tmpnnew)
                        //{
                        //    com = item.Gvp_com;
                        //}
                        //MessageBox.Show("This voucher is generated in " + com + " company.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     
                        //return;
                    
                    }
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvMultipleItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvMultipleItem.Rows.Count > 0)
                {
                    string _book = gvMultipleItem.Rows[e.RowIndex].Cells["MuItm_Item"].Value.ToString();
                    string _page = gvMultipleItem.Rows[e.RowIndex].Cells["MuItm_Description"].Value.ToString();
                    string _gv_type = gvMultipleItem.Rows[e.RowIndex].Cells["Gvp_gv_tp"].Value.ToString();
                 
                    cmbGvType.Text = _gv_type;
                    txtGVCode_Leave(null, null);
                    cmbGvBook.Text = "";
                    cmbTopg.Text = "";
                    cmbGvBook.Text = _book;
                    cmbTopg.Text = _page;
                   
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCatItem;
                _CommonSearch.ShowDialog();
                txtCatItem.Focus();
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

        private void btnCatSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GvCategory);
                DataTable _result = CHNLSVC.CommonSearch.SearchGvCategory(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCat;
                _CommonSearch.ShowDialog();
                txtCat.Focus();
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

        private void txtCat_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCatSearch_Click(null, null);
        }

        private void txtCat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnCatSearch_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtCatDesc.Focus();
        }

        private void ClearGvSetupScreen()
        {
            txtCat.Clear();
            txtCatDesc.Clear();
            chkStatus.Checked = true;
            txtItem.Clear();
            radMan.Checked = true;
            cmbLink.SelectedIndex = 0;
            gvMan.DataSource = new DataTable();
            gvOpt.DataSource = new DataTable();
            _manDetail = null;
            _optDetail = null;
        }

        List<MasterGiftVoucherCategoryDetail> _manDetail = null;
        List<MasterGiftVoucherCategoryDetail> _optDetail = null;
        private void Coloring()
        {
            if (gvMan.RowCount > 0)
                foreach (DataGridViewRow _r in gvMan.Rows)
                    if (Convert.ToInt32(_r.Cells["gm_stus"].Value) == 0) { _r.Cells["gm_itm"].Style.BackColor = Color.DarkGray; _r.Cells["gm_desc"].Style.BackColor = Color.DarkGray; }
            if (gvOpt.RowCount > 0)
                foreach (DataGridViewRow _r in gvOpt.Rows)
                    if (Convert.ToInt32(_r.Cells["go_stus"].Value) == 0) { _r.Cells["go_itm"].Style.BackColor = Color.DarkGray; _r.Cells["go_desc"].Style.BackColor = Color.DarkGray; }
        }

        private void txtCat_Leave(object sender, EventArgs e)
        {
            string _code = txtCat.Text.Trim();
            Int64 _seqno = -1;
            ClearGvSetupScreen();
            txtCat.Text = _code;
            if (string.IsNullOrEmpty(txtCat.Text)) return;
            //check availability
            DataTable _tbl = CHNLSVC.General.GetGvCategory(BaseCls.GlbUserComCode, txtCat.Text.Trim());
            if (_tbl == null || _tbl.Rows.Count <= 0) return;

            txtCatDesc.Text = _tbl.Rows[0].Field<string>("gvct_desc");
            Int16 _status = _tbl.Rows[0].Field<Int16>("gvct_stus");
            Int16 _verb = _tbl.Rows[0].Field<Int16>("gvct_verb");
            _seqno = Convert.ToInt64(_tbl.Rows[0].Field<decimal>("gvct_seq"));

            if (_status == 1) { chkStatus.Checked = true; chkStatus.Text = "Active"; } else { chkStatus.Checked = false; chkStatus.Text = "Inactive"; }
            if (_verb == 1) { cmbLink.SelectedIndex = 0; } else { cmbLink.SelectedIndex = 1; }

            var _mandatory = _tbl.AsEnumerable().Where(x => x.Field<Int16>("gvctd_tp") == 1);
            var _optional = _tbl.AsEnumerable().Where(x => x.Field<Int16>("gvctd_tp") == 0);
            DataTable _man = null;
            DataTable _opt = null;
            if (_mandatory != null && _mandatory.Count() > 0)
                _man = _mandatory.CopyToDataTable();
            if (_optional != null && _optional.Count() > 0)
                _opt = _optional.CopyToDataTable();


            gvMan.AutoGenerateColumns = false;
            gvOpt.AutoGenerateColumns = false;

            if (_manDetail == null) _manDetail = new List<MasterGiftVoucherCategoryDetail>();
            if (_optDetail == null) _optDetail = new List<MasterGiftVoucherCategoryDetail>();
            if (_mandatory != null && _mandatory.Count() > 0)
                foreach (DataRow _m in _man.Rows)
                {
                    MasterGiftVoucherCategoryDetail _one = new MasterGiftVoucherCategoryDetail();
                    _one.Gvctd_itm = _m.Field<string>("gvctd_itm");
                    MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _one.Gvctd_itm);
                    _one.Mi_longdesc = _item.Mi_longdesc;
                    _one.Gvctd_seq = Convert.ToInt32(_seqno);
                    _one.Gvctd_stus = _m.Field<Int16>("gvctd_stus") == 1 ? true : false;
                    _one.Gvctd_tp = true;
                    _manDetail.Add(_one);
                }
            if (_optional != null && _optional.Count() > 0)
                foreach (DataRow _o in _opt.Rows)
                {
                    MasterGiftVoucherCategoryDetail _one = new MasterGiftVoucherCategoryDetail();
                    _one.Gvctd_itm = _o.Field<string>("gvctd_itm");
                    MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _one.Gvctd_itm);
                    _one.Mi_longdesc = _item.Mi_longdesc;
                    _one.Gvctd_seq = Convert.ToInt32(_seqno);
                    _one.Gvctd_stus = _o.Field<Int16>("gvctd_stus") == 1 ? true : false;
                    _one.Gvctd_tp = false;
                    _optDetail.Add(_one);
                }

            gvMan.DataSource = _man;
            gvOpt.DataSource = _opt;
            Coloring();
        }

        private void btnCategorySetup_Click(object sender, EventArgs e)
        {
            cmbLink.SelectedIndex = 0;
            if (pnlCombination.Visible) { pnlCombination.Visible = false; btnIssueNewItemGv.Enabled = true; btnCatSetupProcess.Enabled = false; } else { pnlCombination.Visible = true; btnIssueNewItemGv.Enabled = false; btnCatSetupProcess.Enabled = true; }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCatItem.Text)) { MessageBox.Show("Please select the item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtCatItem.Text.Trim());
            if (_item == null || string.IsNullOrEmpty(_item.Mi_cd))
            {
                MessageBox.Show("Selected item is invalid. Please check the item code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_manDetail == null) _manDetail = new List<MasterGiftVoucherCategoryDetail>();
            if (_optDetail == null) _optDetail = new List<MasterGiftVoucherCategoryDetail>();
            MasterGiftVoucherCategoryDetail _one = new MasterGiftVoucherCategoryDetail();

            if (radMan.Checked)
            {
                if (gvMan.RowCount > 0)
                {
                    MessageBox.Show("Item already selected for mandatory section.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //Check for the duplicates
                if (_manDetail != null && _manDetail.Count > 0)
                {
                    var _dups = _manDetail.Where(x => x.Gvctd_itm == txtCatItem.Text.Trim()).ToList();
                    if (_dups != null && _dups.Count > 0)
                    {
                        MessageBox.Show("Select item is already available in the mandatory section. You are not allow for enter same item for mandatory section", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                _one.Gvctd_itm = txtCatItem.Text.Trim();
                _one.Gvctd_seq = -1;
                _one.Gvctd_stus = true;
                _one.Gvctd_tp = true;
                _one.Mi_longdesc = _item.Mi_longdesc;
                _manDetail.Add(_one);
            }

            if (radOpt.Checked)
            {
                //Check for the duplicates
                if (_optDetail != null && _optDetail.Count > 0)
                {
                    var _dups = _optDetail.Where(x => x.Gvctd_itm == txtCatItem.Text.Trim()).ToList();

                    if (_dups != null && _dups.Count > 0)
                    {
                        MessageBox.Show("Selected item is already available in the optional section. You are not allow to enter same item for the optional section.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                _one.Gvctd_itm = txtCatItem.Text.Trim();
                _one.Gvctd_seq = -1;
                _one.Gvctd_stus = true;
                _one.Gvctd_tp = false;
                _one.Mi_longdesc = _item.Mi_longdesc;
                _optDetail.Add(_one);
            }
            gvMan.AutoGenerateColumns = false;
            gvOpt.AutoGenerateColumns = false;

            gvMan.DataSource = new DataTable();
            gvOpt.DataSource = new DataTable();

            gvMan.DataSource = _manDetail;
            gvOpt.DataSource = _optDetail;
            txtCatItem.Clear();
            txtCatItem.Focus();
            Coloring();

        }

        private void gvMan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvMan.RowCount > 0)
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                    if (MessageBox.Show("Do you need to remove this record? ", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string _item = Convert.ToString(gvMan.Rows[e.RowIndex].Cells["gm_itm"].Value);
                        _manDetail.RemoveAll(x => x.Gvctd_itm == _item);

                        gvMan.AutoGenerateColumns = false;
                        gvOpt.AutoGenerateColumns = false;

                        gvMan.DataSource = new DataTable();
                        gvOpt.DataSource = new DataTable();

                        gvMan.DataSource = _manDetail;
                        gvOpt.DataSource = _optDetail;
                        txtCatItem.Clear();
                        txtCatItem.Focus();

                        Coloring();
                    }
        }

        private void gvOpt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvOpt.RowCount > 0)
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                    if (MessageBox.Show("Do you need to remove this record? ", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string _item = Convert.ToString(gvOpt.Rows[e.RowIndex].Cells["go_itm"].Value);
                        _optDetail.RemoveAll(x => x.Gvctd_itm == _item);

                        gvMan.AutoGenerateColumns = false;
                        gvOpt.AutoGenerateColumns = false;

                        gvMan.DataSource = new DataTable();
                        gvOpt.DataSource = new DataTable();

                        gvMan.DataSource = _manDetail;
                        gvOpt.DataSource = _optDetail;
                        txtCatItem.Clear();
                        txtCatItem.Focus();
                        Coloring();
                    }
        }

        private void btngvclose_Click(object sender, EventArgs e)
        {
            pnlCombination.Visible = false;
            btnIssueNewItemGv.Enabled = true;
            btnCatSetupProcess.Enabled = false;
        }

        private void btnCatSetupProcess_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat.Text))
            { MessageBox.Show("Please select the category.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            if (string.IsNullOrEmpty(txtCatDesc.Text))
            { MessageBox.Show("Please select the category description.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            if ((_manDetail == null || _manDetail.Count <= 0) && (_optDetail == null || _optDetail.Count <= 0))
            {
                MessageBox.Show("You have to select one of the mandatory or optional section in order to create a category.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable _tbl = CHNLSVC.General.GetGvCategory(BaseCls.GlbUserComCode, txtCat.Text.Trim());
            Int64 _seqno = -1;
            if (_tbl == null || _tbl.Rows.Count <= 0) _seqno = -1;
            else _seqno = Convert.ToInt64(_tbl.Rows[0].Field<decimal>("gvct_seq"));


            List<MasterGiftVoucherCategoryDetail> _mergone = new List<MasterGiftVoucherCategoryDetail>();
            if (_manDetail != null && _manDetail.Count > 0) _mergone.AddRange(_manDetail);
            if (_optDetail != null && _optDetail.Count > 0) _mergone.AddRange(_optDetail);
            _mergone.ForEach(x => x.Gvctd_seq = Convert.ToInt32(_seqno));

            MasterGiftVoucherCategory _hdr = new MasterGiftVoucherCategory();
            _hdr.Gvct_cat = txtCat.Text.Trim();
            _hdr.Gvct_com = BaseCls.GlbUserComCode;
            _hdr.Gvct_cr_by = BaseCls.GlbUserID;
            _hdr.Gvct_cr_dt = DateTime.Now.Date;
            _hdr.Gvct_desc = txtCatDesc.Text.Trim();
            _hdr.Gvct_md_by = BaseCls.GlbUserID;
            _hdr.Gvct_md_dt = DateTime.Now.Date;
            _hdr.Gvct_seq = Convert.ToInt32(_seqno);
            _hdr.Gvct_session = BaseCls.GlbUserSessionID;
            _hdr.Gvct_stus = chkStatus.Checked;
            _hdr.Gvct_verb = Convert.ToString(cmbLink.Text).Contains("AND") ? true : false;

            try
            {
                string _error = string.Empty;
                int _effect = CHNLSVC.General.SaveGvCategoryCombination(_hdr, _mergone, out _error);

                if (_effect == -1)
                {
                    MessageBox.Show(_error + ". Please contact IT dept.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show("Category " + txtCat.Text + " has been successfully created/updated.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearGvSetupScreen();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void txtCatItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Item_Click(null, null);
        }

        private void txtCatItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Item_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                btnAddItem.Focus();
        }

        private void txtCatDesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCatItem.Focus();
        }

        private void btnSearch_Category_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GvCategory);
                DataTable _result = CHNLSVC.CommonSearch.SearchGvCategory(_CommonSearch.SearchParams, null, null);
                if (_result != null && _result.Rows.Count > 0)
                    _result = _result.AsEnumerable().Where(x => x.Field<Int16>("status") == 1).ToList().CopyToDataTable();
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtIssueCate;
                _CommonSearch.ShowDialog();
                txtIssueCate.Focus();
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
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                DataTable _result = CHNLSVC.CommonSearch.GetItemByType(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textBox1;
                _CommonSearch.ShowDialog();
                txtItemGvCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text)) return;

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                DataTable _result = CHNLSVC.CommonSearch.GetItemByType(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("ITEM") == textBox1.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid Gift voucher code", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                    textBox1.Focus();
                    return;
                }

                cmbItemGvBook.DataSource = new DataTable();
                DataTable _book = CHNLSVC.Inventory.GetAvailableGvBooks(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ITEM", "A", textBox1.Text.Trim(),null);

                if (_book != null)
                {
                    //var _final = (from _lst in _book
                    //select _lst.Gvp_book).ToList();
                    comboBox1.DataSource = _book;
                    comboBox1.ValueMember = "GVP_BOOK";
                    comboBox1.DisplayMember = "GVP_BOOK";

                    // cmbGvBook.DataSource = _final;
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                List<GiftVoucherPages> _tmpList = new List<GiftVoucherPages>();
                txtItemGvPage.Text = "";
                lblPreFix.Text = "";
                lblIssueRef.Text = "";
                lblLine.Text = "";
                if (!IsNumeric(comboBox1.Text))
                {
                    return;
                }
                _tmpList = CHNLSVC.Inventory.GetAvailableGvPages(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ITEM", "A", Convert.ToInt32(comboBox1.Text), textBox1.Text.Trim());
                dataGridView1.AutoGenerateColumns=false;
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = _tmpList;

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrintPrint_Click(object sender, EventArgs e)
        {
            var _select = from DataGridViewRow i in dataGridView1.Rows where Convert.ToBoolean(i.Cells["prt_select"].Value) select i;
            if (_select == null || _select.Count() <= 0) { MessageBox.Show("Please select the gift vouchers.", "Gift Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            foreach (var r in _select)
            {
                Int32 _page = Convert.ToInt32(r.Cells["dataGridViewTextBoxColumn6"].Value);
                clsInventoryRep obj = new clsInventoryRep();
                BaseCls.GlbReportProfit = BaseCls.GlbUserDefProf;
                BaseCls.GlbReportBook = Convert.ToInt16(comboBox1.SelectedValue);
                BaseCls.GlbReportFromPage = Convert.ToInt32(_page);
                BaseCls.GlbReportToPage = Convert.ToInt32(_page);
                obj.GiftVoucherPrintReport();
            }
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = null;
            panel1.Visible = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
panel1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = null;
            textBox1.Clear();
            comboBox1.DataSource = null;
        }




    }
}
