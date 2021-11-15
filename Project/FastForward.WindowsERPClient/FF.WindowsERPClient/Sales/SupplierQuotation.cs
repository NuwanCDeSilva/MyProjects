using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;
using FF.WindowsERPClient;
using System.Linq;
using System.Configuration;
using System.Data.OleDb;

namespace FF.WindowsERPClient.Sales
{
    public partial class SupplierQuotation : FF.WindowsERPClient.Base
    {
        #region Variables
        private MasterItem _itemdetail = null;
        private DataTable _CompanyItemStatus = null;
        private List<QoutationDetails> _QuotationItemList = new List<QoutationDetails>();
        bool _isDecimalAllow = false;
        private Int32 quoSeq = 0;
        private Int32 _lineNo = 0;
        #endregion

        #region Rooting for Form Initialize
        private void SystemErrorMessage(Exception ex)
        {
            CHNLSVC.CloseChannel();
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        private void button4_Click(object sender, EventArgs e)
        {

        }
        private void LoadCachedObjects()
        {
            _CompanyItemStatus = CacheLayer.Get<DataTable>(CacheLayer.Key.CompanyItemStatus.ToString());
        }

        private void BindUserCompanyItemStatusDDLData(ComboBox ddl)
        {
            DataTable _tbl = _CompanyItemStatus;
            var _s = (from L in _tbl.AsEnumerable()
                      select new
                      {
                          MIS_DESC = L.Field<string>("MIS_DESC"),
                          MIC_CD = L.Field<string>("MIC_CD")
                      }).ToList();
            var _n = new { MIS_DESC = string.Empty, MIC_CD = string.Empty };
            _s.Insert(0, _n);
            ddl.DataSource = _s;
            ddl.DisplayMember = "MIS_DESC";
            ddl.ValueMember = "MIC_CD";
            ddl.Text = "GOOD";
        }

        private void bind_Combo_Types()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("N", "NORMAL");
            PartyTypes.Add("C", "CONSIGN");

            cmbType.DataSource = new BindingSource(PartyTypes, null);
            cmbType.DisplayMember = "Value";
            cmbType.ValueMember = "Key";
        }


        private void InitVariables()
        {

            _QuotationItemList = new List<QoutationDetails>();

            gvItem.AutoGenerateColumns = false;
            gvItem.DataSource = new List<QoutationDetails>();

        }
        private void InitializeForm()
        {
            dtDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MM/yyyy");
            ClearLayer1();
            ClearLayer2();
            ClearLayer3();
            ClearLayer4();
            ClearLayer6();
        }


        private QoutationDetails AssignDataToObject(MasterItem _item)
        {

            QoutationDetails _tempItem = new QoutationDetails();
            _tempItem.Qd_amt = 0;
            _tempItem.Qd_cbatch_line = 0;
            _tempItem.Qd_cdoc_no = "";
            _tempItem.Qd_citm_line = 0;
            _tempItem.Qd_cost_amt = 0;
            _tempItem.Qd_dis_amt = 0;
            _tempItem.Qd_dit_rt = 0;
            _tempItem.Qd_frm_qty = Convert.ToDecimal(txtFromQty.Text);
            _tempItem.Qd_to_qty = Convert.ToDecimal(txtToQty.Text);
            _tempItem.Qd_issue_qty = 0;
            _tempItem.Qd_itm_cd = txtItem.Text;
            _tempItem.Qd_itm_desc = _item.Mi_longdesc;
            string _itemStatus = cmbStatus.SelectedValue.ToString();
            _tempItem.Qd_itm_stus = _itemStatus;
            _tempItem.Qd_itm_tax = 0;
            _tempItem.Qd_line_no = _lineNo;
            _tempItem.Qd_nitm_cd = null;
            _tempItem.Qd_nitm_desc = null;
            _tempItem.Qd_no = "";
            _tempItem.Qd_pb_lvl = "";
            _tempItem.Qd_pb_price = 0;
            _tempItem.Qd_pb_seq = 0;
            _tempItem.Qd_pbook = "";
            _tempItem.Qd_quo_tp = "R";
            _tempItem.Qd_res_no = null;
            _tempItem.Qd_res_qty = 0;
            _tempItem.Qd_resbal_qty = 0;
            _tempItem.Qd_resitm_cd = "";
            _tempItem.Qd_resline_no = 0;
            _tempItem.Qd_resreq_no = null;
            _tempItem.Qd_seq_no = 0;
            _tempItem.Qd_tot_amt = 0;
            _tempItem.Qd_unit_price = Convert.ToDecimal(txtUnitPrice.Text);
            _tempItem.Mi_longdesc = lblItemDescription.Text;
            _tempItem.Mi_model = lblItemModel.Text;


            string _NormalPb = "";
            string _NormalLvl = "";

            MasterCompany _mastercompany = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

            _NormalPb = _mastercompany.Mc_anal7;
            _NormalLvl = _mastercompany.Mc_anal8;

            _tempItem.Qd_uom = _item.Mi_itm_uom;
            _tempItem.Qd_warr_rmk = "";
            _tempItem.Qd_warr_pd = 0;



            return _tempItem;
        }

        public SupplierQuotation()
        {
            InitializeComponent();
            try
            {
                InitVariables();
                LoadCachedObjects();
                InitializeForm();
                BindUserCompanyItemStatusDDLData(cmbStatus);
                bind_Combo_Types();

                cmbType.SelectedIndex = 1;

                cmbType.Text = "";

                cmbType.Enabled = true;

            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Common Searching Area

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SupplierQuo:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.SalesInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }


                case CommonUIDefiniton.SearchUserControlType.MRN:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnSearch_DispatchRequired_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtManualRef;
                _CommonSearch.ShowDialog();
                txtManualRef.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void txtDispatchRequried_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_DispatchRequired_Click(null, null);
        }

        private void btnSearch_BaseDoc_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
            DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSup;
            _CommonSearch.ShowDialog();
            load_sup();

        }
        private void txtBaseDocument_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void btnSearch_Request_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierQuo);
                DataTable _result = CHNLSVC.CommonSearch.GetSupplierQuotation(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRequest;
                _CommonSearch.ShowDialog();
                txtRequest.Select();
                txtRequest_Leave(null, null);
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void txtRequest_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Request_Click(null, null);
        }

        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtItem.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void txtItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Item_Click(null, null);
        }

        #endregion

        #region Rooting for Key Navigation
        private void txtRequestDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtExpDate.Focus();
        }
        private void txtRequriedDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbType.Focus();
        }
        private void ddlRequestSubType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtManualRef.Focus();
        }
        private void txtDispatchRequried_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSup.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_DispatchRequired_Click(null, null);
        }
        private void txtBaseDocument_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRequest.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_BaseDoc_Click(null, null);
        }
        private void txtRequest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtItem.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_Request_Click(null, null);
        }
        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbStatus.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_Item_Click(null, null);
        }
        private void cmbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtFromQty.Focus();
        }
        private void txtReservation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtToQty.Focus();
        }
        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtUnitPrice.Focus();
        }
        private void txtItmRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddItem.Focus();
        }

        private void txtRequestBy_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void txtNIC_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void txtCollecterName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRemark.Focus();
        }
        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnApprove.Select();
        }
        #endregion

        #region Rooting for Clear Screen
        private void ClearLayer1()
        {

            dtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            dtExpDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtManualRef.Clear();
            txtSup.Clear();
            txtRequest.Clear();

        }
        private void ClearLayer2()
        {
            txtItem.Clear();
            txtFromQty.Clear();
            txtToQty.Clear();
            txtUnitPrice.Text = FormatToCurrency("0");
            btnAddItem.Enabled = true;
            btnUploadFile_spv.Enabled = true;

        }
        private void ClearLayer3()
        {
            lblItemDescription.Text = string.Empty;
            lblItemModel.Text = string.Empty;
            lblItemBrand.Text = string.Empty;
            lblSup.Text = "";
        }
        private void ClearLayer4()
        {
            gvItem.DataSource = new List<RequestApprovalDetail>();
        }

        private void ClearLayer6()
        {

            txtRemark.Clear();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you need to clear the screen?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    InitializeForm();
                    InitVariables();
                    btn_Save.Enabled = true;
                    btnApprove.Enabled = true;
                    return;
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Load Item Detail
        private bool LoadItemDetail(string _item)
        {
            lblItemDescription.Text = string.Empty;
            lblItemModel.Text = string.Empty;
            lblItemBrand.Text = string.Empty;
            _isDecimalAllow = false;

            _itemdetail = new MasterItem();

            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
            if (_itemdetail != null)
                if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                {
                    _isValid = true;
                    string _description = _itemdetail.Mi_longdesc;
                    string _model = _itemdetail.Mi_model;
                    string _brand = _itemdetail.Mi_brand;
                    string _serialstatus = _itemdetail.Mi_is_subitem == true ? "Available" : "Non";

                    lblItemDescription.Text = _description;
                    lblItemModel.Text = _model;
                    lblItemBrand.Text = _brand;
                    _isDecimalAllow = CHNLSVC.Inventory.IsUOMDecimalAllow(_item);

                }

            return _isValid;
        }
        #endregion



        #region Rooting for Item Code Validation
        private void CheckItemCode(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtItem.Text.Trim())) return;

            try
            {


                if (string.IsNullOrEmpty(txtManualRef.Text))
                {
                    txtManualRef.Text = "N/A";
                }
                if (!LoadItemDetail(txtItem.Text.Trim()))
                {
                    MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Clear();
                    txtItem.Focus();
                    return;
                }

            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Show Invnetory Balance


        private void BindInventoryRequestItemsGridData()
        {
            try
            {
                if (string.IsNullOrEmpty(txtUnitPrice.Text))
                {
                    MessageBox.Show("Please enter unit price.", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    MessageBox.Show("Please enter item code.", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtUnitPrice.Text) <= 0)
                {
                    MessageBox.Show("Please enter valid unit price.", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUnitPrice.Focus();
                    return;
                }


                if (string.IsNullOrEmpty(cmbStatus.Text))
                {
                    MessageBox.Show("Please select a item status.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbStatus.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtToQty.Text))
                {
                    MessageBox.Show("Please enter to quantity.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtToQty.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtFromQty.Text))
                {
                    MessageBox.Show("Please enter to quantity.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFromQty.Focus();
                    return;
                }

                if (IsNumeric(txtFromQty.Text.Trim()) == false)
                {
                    MessageBox.Show("Please enter valid quantity.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFromQty.Focus();
                    return;
                }
                if (IsNumeric(txtToQty.Text.Trim()) == false)
                {
                    MessageBox.Show("Please enter valid quantity.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtToQty.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtToQty.Text.ToString()) <= 0)
                {
                    MessageBox.Show("Please enter valid quantity.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtToQty.Focus();
                    return;
                }
                if (Convert.ToDecimal(txtFromQty.Text.ToString()) <= 0)
                {
                    MessageBox.Show("Please enter valid quantity.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFromQty.Focus();
                    return;
                }


                //Get existing items details from the grid.

                string _mainItemCode = txtItem.Text.Trim().ToUpper();
                string _itemStatus = cmbStatus.SelectedValue.ToString();
                string _reservationNo = string.IsNullOrEmpty(txtFromQty.Text.Trim()) ? string.Empty : txtFromQty.Text.Trim();
                decimal _mainItemQty = (string.IsNullOrEmpty(txtToQty.Text)) ? 0 : Convert.ToDecimal(txtToQty.Text.Trim());
                string _remarksText = string.IsNullOrEmpty(txtUnitPrice.Text.Trim()) ? string.Empty : txtUnitPrice.Text.Trim();
                //  bool _isSubItemHave = (string.IsNullOrEmpty(lblItemSubStatus.Text)) ? false : lblItemSubStatus.Text.Trim() == "Sub Item Status : Available" ? true : false;

                bool _isDuplicateItem = false;
                Int32 _duplicateComLine = 0;
                Int32 _duplicateItmLine = 0;

                MasterItem _itm = new MasterItem();
                _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());

                if (_QuotationItemList == null || _QuotationItemList.Count <= 0)
                //No Records
                {
                    _isDuplicateItem = false;
                    _lineNo += 1;

                    _QuotationItemList.Add(AssignDataToObject(_itm));
                }
                else
                //Having some records
                {
                    var _similerItem = from _list in _QuotationItemList
                                       where _list.Qd_itm_cd == txtItem.Text && _list.Qd_itm_stus == cmbStatus.Text && _list.Qd_frm_qty == Convert.ToDecimal(txtFromQty.Text) && _list.Qd_to_qty == Convert.ToDecimal(txtToQty.Text)
                                       select _list;

                    if (_similerItem.Count() > 0)
                    //Similar item available
                    {
                        _isDuplicateItem = true;
                        foreach (var _similerList in _similerItem)
                        {
                            _duplicateComLine = _similerList.Qd_citm_line;
                            _duplicateItmLine = _similerList.Qd_line_no;

                            _similerList.Qd_unit_price = Convert.ToDecimal(txtUnitPrice.Text);

                        }
                    }
                    else
                    //No similar item found
                    {
                        _isDuplicateItem = false;
                        _lineNo += 1;
                        _QuotationItemList.Add(AssignDataToObject(_itm));
                    }

                }

                gvItem.AutoGenerateColumns = false;
                gvItem.DataSource = new List<QoutationDetails>();
                gvItem.DataSource = _QuotationItemList;
                btnUploadFile_spv.Enabled = false;

                ClearLayer2();

                if (MessageBox.Show("Do you need to add another item?", "Adding...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtItem.Focus();
                    return;
                }
                else
                {
                    btn_Save.Select();
                    return;
                }

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }
        #endregion

        #region Rooting for Add an Item
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text.Trim())) { MessageBox.Show("Please select the item.", "Item Code", MessageBoxButtons.OK, MessageBoxIcon.Information); txtItem.Clear(); return; }

                BindInventoryRequestItemsGridData();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Item Grid Events

        private void gvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvItem.Rows.Count > 0)
                {
                    Int32 _rowIndex = e.RowIndex;
                    if (_rowIndex != -1)
                    {
                        if (e.ColumnIndex == 0)
                        {

                            if (MessageBox.Show("Do you need to remove this item", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                string _item = Convert.ToString(gvItem.Rows[_rowIndex].Cells["qd_itm_cd"].Value);
                                _QuotationItemList.RemoveAll(x => x.Qd_itm_cd == _item);
                                gvItem.DataSource = new List<InventoryRequestItem>();

                                gvItem.DataSource = _QuotationItemList;
                                return;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Re-Call the Request No

        private void txtRequest_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRequest.Text))
                load_save_Quotation();
        }
        #endregion


        #region Rooting for Check Location
        public bool IsValid(string rate)
        {
            string[] pin = rate.Split('.');
            if (pin.Length > 1)
                return true;
            else
                return false;
        }

        #endregion

        #region Rooting for Qty Events
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                IsDecimalAllow(_isDecimalAllow, sender, e);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion


        private void MaterialRequisition_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
        }

        private void ddlRequestSubType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void txtQty_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtToQty.Text))
            {
                if (IsNumeric(txtToQty.Text))
                {
                    if (_isDecimalAllow == false) txtToQty.Text = decimal.Truncate(Convert.ToDecimal(txtToQty.Text.ToString())).ToString();
                }
                else
                {
                    txtToQty.Clear();
                    txtToQty.Focus();
                }
            }

        }

        private void load_save_Quotation()
        {
            try
            {
                QuotationHeader _saveHdr = new QuotationHeader();

                _saveHdr = CHNLSVC.Sales.Get_Quotation_HDR(txtRequest.Text);
                btnAddItem.Enabled = true;
                if (_saveHdr != null)
                {
                    quoSeq = _saveHdr.Qh_seq_no;
                    dtDate.Text = _saveHdr.Qh_dt.ToShortDateString();
                    txtManualRef.Text = _saveHdr.Qh_ref;
                    txtSup.Text = _saveHdr.Qh_party_cd;

                    txtRemark.Text = _saveHdr.Qh_remarks;

                    btn_Save.Enabled = false;
                    btnApprove.Enabled = false;

                    if (_saveHdr.Qh_stus == "A")
                        lblStus.Text = "Active";

                    if (_saveHdr.Qh_stus == "P")
                    {
                        lblStus.Text = "Pending";
                        btnApprove.Enabled = true;
                    }
                    if (_saveHdr.Qh_stus == "C")
                        lblStus.Text = "Cancelled";

                    List<QoutationDetails> _recallList = new List<QoutationDetails>();
                    _recallList = CHNLSVC.Financial.GetSupQoutation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtRequest.Text);
                    _QuotationItemList = _recallList.ToList();
                    gvItem.AutoGenerateColumns = false;
                    gvItem.DataSource = new List<QoutationDetails>();
                    gvItem.DataSource = _recallList;

                    btnAddItem.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Please select the valid quotation number", "Invalid Quotation Number ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();
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

        private void load_sup()
        {
            List<MasterBusinessEntity> _custList = new List<MasterBusinessEntity>();
            if (!string.IsNullOrEmpty(txtSup.Text))
            {
                _custList = CHNLSVC.Sales.GetCustomerDetailList(BaseCls.GlbUserComCode, txtSup.Text, string.Empty, string.Empty, "S");
                if (_custList == null || _custList.Count == 0)
                {
                    MessageBox.Show("Please select a valid supplier", "Invalid Supplier", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSup.Text = "";
                    lblSup.Text = "";
                    txtSup.Focus();
                }
                else
                {
                    lblSup.Text = _custList[0].Mbe_name.ToString();
                }
            }
        }

        private void Clear_Data()
        {
            _itemdetail = new MasterItem();

            _QuotationItemList = new List<QoutationDetails>();

            gvItem.AutoGenerateColumns = false;
            gvItem.DataSource = new List<QoutationDetails>();
            btnAddItem.Enabled = true;

            LoadCachedObjects();

            txtManualRef.Text = "";
            txtRequest.Text = "";
            txtSup.Text = "";
            lblSup.Text = "";

            txtItem.Text = "";
            txtRemark.Text = "";
            lblStus.Text = "";

            quoSeq = 0;
            _lineNo = 0;

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;

                if (string.IsNullOrEmpty(txtSup.Text))
                {
                    MessageBox.Show("Please select the supplier", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (_QuotationItemList == null || _QuotationItemList.Count <= 0)
                {
                    MessageBox.Show("Please select relavant items", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //collect quo header
                QuotationHeader _saveHdr = new QuotationHeader();
                _saveHdr.Qh_seq_no = quoSeq;
                _saveHdr.Qh_add1 = "";
                _saveHdr.Qh_add2 = "";
                _saveHdr.Qh_com = BaseCls.GlbUserComCode;
                _saveHdr.Qh_cre_by = BaseCls.GlbUserID;
                _saveHdr.Qh_cur_cd = "LKR";
                _saveHdr.Qh_del_cusadd1 = "";
                _saveHdr.Qh_del_cusadd2 = "";
                _saveHdr.Qh_del_cuscd = "";
                _saveHdr.Qh_del_cusfax = "";
                _saveHdr.Qh_del_cusid = "";
                _saveHdr.Qh_del_cusname = "";
                _saveHdr.Qh_del_custel = "";
                _saveHdr.Qh_del_cusvatreg = null;
                _saveHdr.Qh_dt = Convert.ToDateTime(dtDate.Value.Date);
                _saveHdr.Qh_ex_dt = Convert.ToDateTime(dtExpDate.Value.Date);
                _saveHdr.Qh_ex_rt = 1;
                _saveHdr.Qh_frm_dt = Convert.ToDateTime(dtDate.Value.Date);
                _saveHdr.Qh_is_tax = false;
                _saveHdr.Qh_jobno = "";
                _saveHdr.Qh_mobi = "";
                _saveHdr.Qh_mod_by = BaseCls.GlbUserID;
                _saveHdr.Qh_no = "";
                _saveHdr.Qh_party_cd = txtSup.Text;
                _saveHdr.Qh_party_name = lblSup.Text;
                _saveHdr.Qh_pc = BaseCls.GlbUserDefProf;
                _saveHdr.Qh_ref = txtManualRef.Text;
                _saveHdr.Qh_remarks = txtRemark.Text;
                _saveHdr.Qh_sales_ex = "";
                _saveHdr.Qh_session_id = BaseCls.GlbUserSessionID;
                _saveHdr.Qh_stus = "P";
                _saveHdr.Qh_sub_tp = Convert.ToString(cmbType.SelectedValue);
                _saveHdr.Qh_tel = "";
                _saveHdr.Qh_tp = "S";
                //  _saveHdr.Qh_anal_1 = _MasterProfitCenter.Mpc_man;
                //_saveHdr.Qh_anal_2 = txtPaymentTerm.Text.Trim();
                //_saveHdr.Qh_anal_3 = cmbInvType.Text;

                if (quoSeq == 0)
                {
                    _saveHdr.Qh_no = null;
                }

                _saveHdr.Qh_anal_5 = 0;

                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                masterAuto.Aut_cate_tp = "PC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "QUA";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "QUA";
                masterAuto.Aut_year = null;

                string QTNum;

                List<QuotationSerial> _QuSerLst = new List<QuotationSerial>();
                row_aff = (Int32)CHNLSVC.Sales.Quotation_save(_saveHdr, _QuotationItemList, masterAuto, _QuSerLst,null,null,null, false, null, null, out QTNum);



                if (row_aff >= 1)
                {
                    MessageBox.Show("Successfully created " + QTNum, "supplier Quotation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //MessageBox.Show("Successfully created.Quotation No: " + QTNum, "supplier Quotation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    //BaseCls.GlbReportName = string.Empty;
                    //_view.GlbReportName = string.Empty;
                    //BaseCls.GlbReportDoc = QTNum;
                    //BaseCls.GlbReportComp = BaseCls.GlbUserComCode;
                    //BaseCls.GlbReportName = "Quotation_RepPrint.rpt";
                    //_view.GlbReportName = "QUOTATION";
                    //_view.Show();
                    //_view = null;

                    Clear_Data();

                }
                else
                {
                    if (!string.IsNullOrEmpty(QTNum))
                    {
                        MessageBox.Show(QTNum, "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Creation Fail.", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtSup_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSup.Text))
                load_sup();
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10112))
            {
                MessageBox.Show("Sorry, You have no permission for approve!\n( Advice: Required permission code :10112)", "Supplier Quotation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(txtRequest.Text))
            {
                MessageBox.Show("Please Save the Supplier Quotation before approval", "Supplier Quotation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Are you sure ?", "Approve", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            Int32 _eff = CHNLSVC.Sales.Update_Quotation_HDR_status(txtRequest.Text, "A");
            if (_eff == 1)
            {
                MessageBox.Show("Successfully Approved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear_Data();
            }
        }

        private void txtSup_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_BaseDoc_Click(null, null);
        }

        private void txtRequest_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_Request_Click(null, null);
        }

        private void txtUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtFromQty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnSearchFile_spv_Click(object sender, EventArgs e)
        {
            txtFileName.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            txtFileName.Text = openFileDialog1.FileName;
        }

        private void btnUploadFile_spv_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            Boolean _isVatClaim = false;
            string _suppTaxCate = "";
            Int32 _upLine = 0;
            Int32 _upDelLineNo = 0;

            Int32 _locaCount = 0;
            bool _isDecimalAllow = false;
            try
            {

                    # region validation

                    if (string.IsNullOrEmpty(txtFileName.Text))
                    {
                        MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFileName.Clear();
                        txtFileName.Focus();
                        return;
                    }

                    System.IO.FileInfo fileObj = new System.IO.FileInfo(txtFileName.Text);

                    if (fileObj.Exists == false)
                    {
                        MessageBox.Show("Selected file does not exist at the following path.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                    }
                    #endregion

                    #region open excel
                    string Extension = fileObj.Extension;

                    string conStr = "";

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


                    conStr = String.Format(conStr, txtFileName.Text, "NO");
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

                    #endregion

                    _QuotationItemList = new List<QoutationDetails>();

                    string _item = "";
                    string _itemDesc = "";
                    decimal _frmqty = 0;
                    decimal _toqty = 0;
                    string _stus = "";
                    decimal _uprice = 0;
                    decimal _drate = 0;
                    decimal _tax = 0;


                    StringBuilder _errorLst = new StringBuilder();
                    if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                    if (_dt.Rows.Count > 0)
                    {
                        if (MessageBox.Show("Are you sure ?", "Purchase order", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No) return;

                        foreach (DataRow _dr in _dt.Rows)
                        {
                            _upLine = Convert.ToInt32(_dr[0]);
                            _item = _dr[1].ToString().Trim();
                            _stus = _dr[2].ToString().Trim();
                            _frmqty = Convert.ToDecimal(_dr[3]);
                            _toqty = Convert.ToDecimal(_dr[4]);
                            _uprice = Convert.ToDecimal(_dr[5]);


                            #region item validation
                            if (string.IsNullOrEmpty(_stus))
                            {
                                MessageBox.Show("Please select item status.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                            if (string.IsNullOrEmpty(_item))
                            {
                                MessageBox.Show("Please enter purchasing item.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }



                            if (string.IsNullOrEmpty(_uprice.ToString()))
                            {
                                MessageBox.Show("Please enter item unit price.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtUnitPrice.Focus();
                                return;
                            }
       
                            _isDecimalAllow = CHNLSVC.Inventory.IsUOMDecimalAllow(_item);
                            if (_isDecimalAllow == false) _frmqty = decimal.Truncate(Convert.ToDecimal(_frmqty));
                            if (_isDecimalAllow == false) _toqty = decimal.Truncate(Convert.ToDecimal(_toqty));

                            #endregion

                            MasterItem _tmpItem = new MasterItem();

                            _tmpItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                            decimal _taxForActual = 0;

                            if (_tmpItem != null)
                            {
                               // decimal _amt = _uprice * _qty;

                                // Add items ______________________
                                MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                _lineNo = _lineNo + 1;
                                QoutationDetails _tempItem = new QoutationDetails();
                                _tempItem.Qd_amt = 0;
                                _tempItem.Qd_cbatch_line = 0;
                                _tempItem.Qd_cdoc_no = "";
                                _tempItem.Qd_citm_line = 0;
                                _tempItem.Qd_cost_amt = 0;
                                _tempItem.Qd_dis_amt = 0;
                                _tempItem.Qd_dit_rt = 0;
                                _tempItem.Qd_frm_qty = _frmqty;
                                _tempItem.Qd_to_qty = _toqty;
                                _tempItem.Qd_issue_qty = 0;
                                _tempItem.Qd_itm_cd = _item;
                                _tempItem.Qd_itm_desc = _itm.Mi_longdesc;
                             //   string _itemStatus = cmbStatus.SelectedValue.ToString();
                                _tempItem.Qd_itm_stus = _stus;
                                _tempItem.Qd_itm_tax = 0;
                                _tempItem.Qd_line_no = _lineNo;
                                _tempItem.Qd_nitm_cd = null;
                                _tempItem.Qd_nitm_desc = null;
                                _tempItem.Qd_no = "";
                                _tempItem.Qd_pb_lvl = "";
                                _tempItem.Qd_pb_price = 0;
                                _tempItem.Qd_pb_seq = 0;
                                _tempItem.Qd_pbook = "";
                                _tempItem.Qd_quo_tp = "R";
                                _tempItem.Qd_res_no = null;
                                _tempItem.Qd_res_qty = 0;
                                _tempItem.Qd_resbal_qty = 0;
                                _tempItem.Qd_resitm_cd = "";
                                _tempItem.Qd_resline_no = 0;
                                _tempItem.Qd_resreq_no = null;
                                _tempItem.Qd_seq_no = 0;
                                _tempItem.Qd_tot_amt = 0;
                                _tempItem.Qd_unit_price = Convert.ToDecimal(_uprice);
                                _tempItem.Mi_longdesc = _itm.Mi_longdesc;
                                _tempItem.Mi_model = _itm.Mi_model;


                                string _NormalPb = "";
                                string _NormalLvl = "";

                                MasterCompany _mastercompany = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

                                _NormalPb = _mastercompany.Mc_anal7;
                                _NormalLvl = _mastercompany.Mc_anal8;

                                _tempItem.Qd_uom = _itm.Mi_itm_uom;
                                _tempItem.Qd_warr_rmk = "";
                                _tempItem.Qd_warr_pd = 0;

                                _QuotationItemList.Add(_tempItem);
                            }

                        }

                        gvItem.AutoGenerateColumns = false;
                        gvItem.DataSource = new List<QoutationDetails>();
                        gvItem.DataSource = _QuotationItemList;

                        btnAddItem.Enabled = false;

                        MessageBox.Show("Done", "Supplier Quotation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    }
}
