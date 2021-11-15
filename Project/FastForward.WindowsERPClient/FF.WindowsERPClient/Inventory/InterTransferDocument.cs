using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Linq;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

//Web base system written by Chamal (Original)
//Windows base system written by Prabhath on 7/01/2013 according to the web

namespace FF.WindowsERPClient.Inventory
{
    public partial class InterTransferDocument : FF.WindowsERPClient.Base
    {
        #region Variables
        public InterTransferDocument()
        {
            InitializeComponent();
            InitVariables();
            LoadCachedObjects();
            InitializeForm(true);
            pnlBalance.Size = new Size(504, 177);
            ddlRequestSubType.SelectedIndex = 3;
            CheckPermission();

        }
        private MasterItem _itemdetail = null;
        private DataTable _CompanyItemStatus = null;
        List<InventoryRequestItem> _invReqItemList = null;
        bool _isDecimalAllow = false;
        bool _isShowInventoryBalance = false;
        bool _isApprovedUser = false;
        string _recallloc = string.Empty;
        public string Recallocation = "";
        public string _issue_from="";
        bool approveAll = true;
        bool IsRecalled = false;
        int maxlineNo = 0;
        Int32 _line = 0;
        int appQyt;
        #endregion

        #region Rooting for Form Initialize
        private void SystemErrorMessage(Exception ex)
        {
            CHNLSVC.CloseChannel();
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you need to close " + this.Text + "?", "Closing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }

        }
        private void LoadCachedObjects()
        {
            _CompanyItemStatus = CacheLayer.Get<DataTable>(CacheLayer.Key.CompanyItemStatus.ToString());
        }
        private void CheckPermission()
        {// Nadeeka 26-02-2015
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11061))
            {
                chkReqType.Visible = true;

            }
            else
            {
                chkReqType.Visible = false;
            }
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
        private void BindRequestSubTypesDDLData(ComboBox ddl)
        {
            ddl.DataSource = new List<MasterSubType>();
            List<MasterSubType> _lst = CHNLSVC.General.GetAllSubTypes("INTR");
            var _n = new MasterSubType();
            _n.Mstp_cd = string.Empty;
            _n.Mstp_desc = string.Empty;
            _lst.Insert(0, _n);
            ddl.DataSource = _lst;
            ddl.DisplayMember = "MSTP_DESC";
            ddl.ValueMember = "MSTP_CD";
        }
        private void BindCompany()
        {
            List<MasterCompany> _lst = CHNLSVC.General.GetALLMasterCompaniesData();
            var _n = new MasterCompany();
            _n.Mc_cd = string.Empty;
            _n.Mc_cd = string.Empty;
            _lst.Insert(0, _n);
            ddlCompany.DataSource = _lst;
            ddlCompany.DisplayMember = "Mc_desc";
            ddlCompany.ValueMember = "Mc_cd";
            ddlCompany.SelectedValue = BaseCls.GlbUserComCode;
        }
        private void BackDatePermission()
        {
            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, txtRequestDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
        }
        private void InitVariables()
        {
            gvBalance.AutoGenerateColumns = false;
            gvItem.AutoGenerateColumns = false;
            gvInvoice.AutoGenerateColumns = false;
            _invReqItemList = new List<InventoryRequestItem>();
        }
        private void InitializeForm(bool _isLoadSubType)
        {
            txtRequestDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MM/yyyy");
            ClearLayer1(_isLoadSubType);
            ClearLayer2();
            ClearLayer3();
            ClearLayer4();
            ClearLayer5();
            ClearLayer6();
            ClearLayer7();
            gvItem.Columns["Itm_Qty"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight;
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "INV3"))
            {
                _isApprovedUser = true;
                AutoApprove = true;
                btnApproved.Enabled = true;
                btnSave.Enabled = true;
                btnAddItem.Enabled = true;
                btnBulkCancel.Enabled = true;
            }
            else
            {
                _isApprovedUser = false;
                AutoApprove = false;
                btnApproved.Enabled = false;
                btnSave.Enabled = true;
                btnAddItem.Enabled = true;
                btnBulkCancel.Enabled = false;
            }
            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "INV7"))
                _isShowInventoryBalance = true;
            else
                _isShowInventoryBalance = false;
            lblDisplayOnly.Size = new Size(198, 39);
            if (_isLoadSubType) ddlRequestSubType.SelectedIndex = 3;
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

                case CommonUIDefiniton.SearchUserControlType.InterTransferInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(Convert.ToString(ddlCompany.SelectedValue) + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InterTransferRequest:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InterTransferReceipt:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + ddlRequestSubType.SelectedValue.ToString().ToUpper() + seperator + "INTR" + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnSearch_DispatchRequired_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Convert.ToString(ddlCompany.Text)))
            {
                MessageBox.Show("Please select the company.", "Company", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDispatchRequried;
                _CommonSearch.ShowDialog();
                txtDispatchRequried.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void txtDispatchRequried_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_DispatchRequired_Click(null, null);
        }

        private void btnSearch_Request_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InterTransferRequest);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchInterTransferRequest(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRequest;
                _CommonSearch.ShowDialog();
                txtRequest.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
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
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem;
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

        private void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Invoice_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                gvInvoice.Focus();

        }
        private void txtInvoice_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Invoice_Click(null, null);
        }
        private void btnSearch_Invoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlRequestSubType.SelectedValue.ToString() == "SALES")
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InterTransferInvoice);
                    DataTable _result = CHNLSVC.CommonSearch.GetInvoiceforInterTransferSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtInvoice;
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.ShowDialog();
                    txtInvoice.Select();
                }
                else if (ddlRequestSubType.SelectedValue.ToString() == "ADVAN")
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InterTransferReceipt);
                    DataTable _result = CHNLSVC.CommonSearch.GetReceiptsByType(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtInvoice;
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.ShowDialog();
                    txtInvoice.Select();
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Key Navigation
        private void txtRequestDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRequriedDate.Focus();
        }
        private void txtRequriedDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ddlRequestSubType.Focus();
        }
        private void ddlRequestSubType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ddlCompany.Focus();
        }
        private void txtDispatchRequried_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_DispatchRequired_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtRequest.Focus();
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
                txtReservation.Focus();
        }
        private void txtReservation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtQty.Focus();
        }
        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtItmRemark.Focus();
        }
        private void txtItmRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddItem.Focus();
        }

        private void txtRequestBy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNIC.Focus();
        }
        private void txtNIC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCollecterName.Focus();
        }
        private void txtCollecterName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRemark.Focus();
        }
        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Select();
        }
        private void ddlCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDispatchRequried.Focus();
        }
        #endregion

        #region Rooting for Clear Screen
        private void ClearLayer1(bool _isReloadSubType)
        {
            chkReqType.Checked = false;
            txtRequestDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtRequriedDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            if (_isReloadSubType) BindRequestSubTypesDDLData(ddlRequestSubType);
            BindRequestSubTypesDDLData(ddlSearchReason);
            BindCompany();
            txtDispatchRequried.Clear();
            txtRequest.Clear();

        }
        private void ClearLayer2()
        {
            txtItem.Clear();
            BindUserCompanyItemStatusDDLData(cmbStatus);
            txtReservation.Clear();
            txtQty.Text = FormatToCurrency("0");
            lblAvalQty.Text = FormatToCurrency("0");
            lblFreeQty.Text = FormatToCurrency("0");
            txtItmRemark.Clear();
        }
        private void ClearLayer3()
        {
            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;
            lblItemSubStatus.Text = "Serial Status : " + string.Empty;
        }
        private void ClearLayer4()
        {
            gvItem.DataSource = new List<RequestApprovalDetail>();
        }
        private void ClearLayer5()
        {
            gvBalance.DataSource = new List<InventoryLocation>();
        }
        private void ClearLayer6()
        {
            txtRequestBy.Text = CHNLSVC.Security.GetUserByUserID(BaseCls.GlbUserID).Se_usr_name; ;
            txtNIC.Clear();
            txtCollecterName.Clear();
            txtRemark.Clear();
        }
        private void ClearLayer7()
        {
            dgvPromo.AutoGenerateColumns = false;
            dgvPromo.DataSource = new DataTable();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you need to clear the screen?", "Clear...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    InitializeForm(true);
                    InitVariables();
                    BackDatePermission();
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
            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;
            lblItemSubStatus.Text = "Sub Item Status : " + string.Empty;
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

                    lblItemDescription.Text = "Description : " + _description;
                    lblItemModel.Text = "Model : " + _model;
                    lblItemBrand.Text = "Brand : " + _brand;
                    lblItemSubStatus.Text = "Sub Item Status : " + _serialstatus;
                    _isDecimalAllow = CHNLSVC.Inventory.IsUOMDecimalAllow(_item);

                }

            return _isValid;
        }
        #endregion

        #region Rooting for Check Inventory Balance and Display
        private void DisplayAvailableQty(string _item, Label _avalQty, Label _freeQty, string _status)
        {

            List<InventoryLocation> _inventoryLocation = null;
            if (string.IsNullOrEmpty(_status))
                _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item.Trim(), string.Empty);
            else
                _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item.Trim(), _status);

            if (_inventoryLocation != null)
                if (_inventoryLocation.Count > 0)
                {
                    var _aQty = _inventoryLocation.Select(x => x.Inl_qty).Sum();
                    var _aFree = _inventoryLocation.Select(x => x.Inl_free_qty).Sum();
                    _avalQty.Text = FormatToQty(Convert.ToString(_aQty));
                    _freeQty.Text = FormatToQty(Convert.ToString(_aFree));
                }
                else { _avalQty.Text = FormatToQty("0"); _freeQty.Text = FormatToQty("0"); }
            else { _avalQty.Text = FormatToQty("0"); _freeQty.Text = FormatToQty("0"); }
        }
        #endregion

        #region Rooting for Item Code Validation
        private void CheckItemCode(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtDispatchRequried.Text)) 
            //{ 
            //    MessageBox.Show("Please select the dispatch location.", "Dispatch Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtItem.Text = "";
            //    return; 
            //}


            if (string.IsNullOrEmpty(txtItem.Text.Trim())) return;
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text)) return;
                Regex r = new Regex("^[a-zA-Z0-9]*$");

                //if (!Regex.IsMatch(txtRequest.Text, "^[a-zA-Z0-9]*$"))
                //{
                //    MessageBox.Show("Please enter valid Item Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtItem.Text = "";
                //    return;
                //}

                if (string.IsNullOrEmpty(ddlRequestSubType.Text)) { MessageBox.Show("Please select the document sub type.", "Sub Type", MessageBoxButtons.OK, MessageBoxIcon.Information); ddlRequestSubType.Focus(); return; }
                if (!chkReqType.Checked)
                {
                    if (string.IsNullOrEmpty(txtDispatchRequried.Text)) { MessageBox.Show("Please select the dispatch location.", "Dispatch Location", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDispatchRequried.Focus(); return; }
                }
                if (!LoadItemDetail(txtItem.Text.Trim()))
                {
                    MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Clear();
                    txtItem.Focus();
                    return;
                }

                //check sys para
                if (string.IsNullOrEmpty(txtInvoice.Text.Trim()))
                {
                    HpSystemParameters _SystemPara = new HpSystemParameters();
                    _SystemPara = CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "CHKCOMMRN", DateTime.Now.Date);
                    if (_SystemPara != null && _SystemPara.Hsy_cd == "CHKCOMMRN")
                    {
                        DataTable _comItm = new DataTable();
                        _comItm = CHNLSVC.Inventory.GetCompeleteItemfromAssambleItem(txtItem.Text.Trim());

                        if (_comItm.Rows.Count > 0)
                        {
                            MessageBox.Show("Cannot request individual item. Please select complete item code", "Complete Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtItem.Clear();
                            txtItem.Focus();
                            return;

                            ////updated by Akila 2017/09/30
                            //var _comItmes = _comItm.Rows.Cast<DataRow>().Where(x => x.Field<string>("Type").ToString() == "C").ToList();
                            //if (_comItmes != null && _comItmes.Count > 0)
                            //{
                            //    MessageBox.Show("Cannot request individual item. Please select complete item code", "Complete Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    txtItem.Clear();
                            //    txtItem.Focus();
                            //    return;
                            //}                       
                        }
                    }
                }
               

                if (ddlRequestSubType.SelectedValue.ToString() != "SALES")
                    if (string.IsNullOrEmpty(cmbStatus.Text))
                        DisplayAvailableQty(txtItem.Text.Trim(), lblAvalQty, lblFreeQty, string.Empty);
                    else
                        DisplayAvailableQty(txtItem.Text.Trim(), lblAvalQty, lblFreeQty, cmbStatus.SelectedValue.ToString());

                if (ddlRequestSubType.SelectedValue.ToString() != "SALES")
                    if (_itemdetail.Mi_itm_tp != "V")
                        LoadDispatchLocationInventoryBalance(txtItem.Text.Trim());
                    else
                        txtReservation.Focus();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Check Item Status
        private void CheckItemStatus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbStatus.Text)) return;
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text.Trim())) { MessageBox.Show("Please select the item.", "Item Code", MessageBoxButtons.OK, MessageBoxIcon.Information); txtItem.Focus(); return; }
                if (string.IsNullOrEmpty(cmbStatus.Text))
                    DisplayAvailableQty(txtItem.Text.Trim(), lblAvalQty, lblFreeQty, string.Empty);
                else
                    DisplayAvailableQty(txtItem.Text.Trim(), lblAvalQty, lblFreeQty, cmbStatus.SelectedValue.ToString());
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Load Dispatch Location Invnetory Balance
        private void LoadDispatchLocationInventoryBalance(string _item)
        {
            if (_isShowInventoryBalance == false) return;
            List<InventoryLocation> _lst = CHNLSVC.Inventory.GetInventoryBalanceSCMnSCM2(BaseCls.GlbUserComCode, txtDispatchRequried.Text, _item, string.Empty);
            if (_lst != null)
                if (_lst.Count > 0)
                {
                    pnlBalance.Visible = true;
                    gvBalance.DataSource = new DataTable();
                    gvBalance.DataSource = _lst;
                    gvBalance.Focus();
                }
        }
        #endregion

        #region Rooting for Inventory Balance
        private void btnpnlBalanceClose_Click(object sender, EventArgs e)
        {
            pnlBalance.Visible = false;
        }
        private void SelectItemStatusfromBalance(Int32 e)
        {
            if (gvBalance.RowCount > 0)
            {
                Int32 _rowIndex = e;
                bool _isNotCompatible = false;
                if (_rowIndex != -1)
                {
                    string _status = Convert.ToString(gvBalance.Rows[_rowIndex].Cells["bal_Status"].Value);
                    var _cmbstatus = _CompanyItemStatus.AsEnumerable().Where(x => x.Field<string>("MIC_CD") == _status).ToList();
                    if (_cmbstatus != null)
                        if (_cmbstatus.Count > 0)
                        {
                            cmbStatus.Text = Convert.ToString(_cmbstatus[0].Field<string>("MIS_DESC"));
                            pnlBalance.Visible = false;
                            txtQty.Focus();
                            return;
                        }
                        else
                            _isNotCompatible = true;
                    else
                        _isNotCompatible = true;


                    if (_isNotCompatible)
                    {
                        MessageBox.Show("Selected item status does not allow for the company", "Item Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

        }
        private void gvBalance_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SelectItemStatusfromBalance(e.RowIndex);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void gvBalance_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    SelectItemStatusfromBalance(gvBalance.CurrentRow.Index);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Add an Item
        private void BindInventoryRequestItemsGridData()
        {
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    txtItem.Focus();
                    MessageBox.Show("Please enter item code.", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(cmbStatus.Text))
                {
                    cmbStatus.Focus();
                    MessageBox.Show("Please select a item status.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    txtQty.Focus();
                    MessageBox.Show("Please enter required quantity.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (IsNumeric(txtQty.Text.Trim()) == false)
                {
                    txtQty.Focus();
                    MessageBox.Show("Please enter valid quantity.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //if (Convert.ToDecimal(txtQty.Text.ToString()) <= 0)
                //{
                //    txtQty.Focus();
                //    MessageBox.Show("Please enter valid quantity.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

               
                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "INV3"))
                {
                    if (Convert.ToDecimal(txtQty.Text.ToString()) < 0)
                    {
                        txtQty.Focus();
                        MessageBox.Show("Please enter valid quantity.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                   
                }
                else if (Convert.ToDecimal(txtQty.Text.ToString()) <= 0)
                {
                    txtQty.Focus();
                    MessageBox.Show("Please enter valid quantity.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                }

                //kapila 13/8/2015 check the GIT is exceeded
                MasterItem _itemMas = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);

                DataTable _dtMax = CHNLSVC.General.GetStockRequest("GIT", BaseCls.GlbUserDefLoca, BaseCls.GlbDefChannel, BaseCls.GlbUserComCode, DateTime.Now.Date, txtItem.Text, _itemMas.Mi_brand, _itemMas.Mi_cate_1, _itemMas.Mi_cate_2, _itemMas.Mi_cate_3, _itemMas.Mi_cate_4, _itemMas.Mi_cate_5);
                if (_dtMax.Rows.Count > 0)
                {
                    DataTable _dtGit = CHNLSVC.General.GetItemGIT(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _dtMax.Rows[0]["mrq_itm_cd"].ToString(), _dtMax.Rows[0]["mrq_brd"].ToString(), _dtMax.Rows[0]["mrq_cat1"].ToString(), _dtMax.Rows[0]["mrq_cat2"].ToString(), _dtMax.Rows[0]["mrq_cat3"].ToString(), _dtMax.Rows[0]["mrq_cat4"].ToString(), _dtMax.Rows[0]["mrq_cat5"].ToString(), Convert.ToDecimal(_dtMax.Rows[0]["mrq_days"]));//, Convert.ToDecimal(_dtMax.Rows[0]["mrq_wsdays"]), Convert.ToDecimal(_dtMax.Rows[0]["mrq_ssdays"]));
                    if (_dtGit.Rows.Count > 0)
                        if (Convert.ToDecimal(_dtMax.Rows[0]["mrq_qty"]) > 0)
                        {
                            if (Convert.ToDecimal(_dtGit.Rows[0]["iti_qty"]) + Convert.ToDecimal(txtQty.Text) > Convert.ToDecimal(_dtMax.Rows[0]["mrq_qty"]))
                            {
                                MessageBox.Show("GIT available.You are exceeding allowable quantity", "Inter-Transfer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("GIT available.You are exceeding allowable days ", "Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtReservation.Text = "";
                            txtReservation.Focus();
                            return;

                        }
                    DataTable _dtGitSS = CHNLSVC.General.GetItemGITWH(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _dtMax.Rows[0]["mrq_itm_cd"].ToString(), _dtMax.Rows[0]["mrq_brd"].ToString(), _dtMax.Rows[0]["mrq_cat1"].ToString(), _dtMax.Rows[0]["mrq_cat2"].ToString(), _dtMax.Rows[0]["mrq_cat3"].ToString(), _dtMax.Rows[0]["mrq_cat4"].ToString(), _dtMax.Rows[0]["mrq_cat5"].ToString(), Convert.ToDecimal(_dtMax.Rows[0]["mrq_ssdays"]), BaseCls.GlbUserDefLoca);//, Convert.ToDecimal(_dtMax.Rows[0]["mrq_wsdays"]), Convert.ToDecimal(_dtMax.Rows[0]["mrq_ssdays"]));
                    if (_dtGitSS.Rows.Count > 0)
                        if (Convert.ToDecimal(_dtMax.Rows[0]["mrq_qty"]) > 0)
                        {
                            if (Convert.ToDecimal(_dtGitSS.Rows[0]["iti_qty"]) + Convert.ToDecimal(txtQty.Text) > Convert.ToDecimal(_dtMax.Rows[0]["mrq_qty"]))
                            {
                                MessageBox.Show("GIT available.You are exceeding allowable quantity", "Inter-Transfer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("GIT available.You are exceeding allowable days ", "Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtReservation.Text = "";
                            txtReservation.Focus();
                            return;

                        }
                }
                //Get existing items details from the grid.

                string _mainItemCode = txtItem.Text.Trim().ToUpper();
                string _itemStatus = cmbStatus.SelectedValue.ToString();
                string _reservationNo = string.IsNullOrEmpty(txtReservation.Text.Trim()) ? string.Empty : txtReservation.Text.Trim();
                decimal _mainItemQty = (string.IsNullOrEmpty(txtQty.Text)) ? 0 : Convert.ToDecimal(txtQty.Text.Trim());
                string _remarksText = string.IsNullOrEmpty(txtItmRemark.Text.Trim()) ? string.Empty : txtItmRemark.Text.Trim();
                bool _isSubItemHave = (string.IsNullOrEmpty(lblItemSubStatus.Text)) ? false : lblItemSubStatus.Text.Trim() == "Sub Item Status : Available" ? true : false;

                List<InventoryRequestItem> _temp = _invReqItemList;
                //This is a temporary collation for newly added items.
                List<InventoryRequestItem> _resultList = null;

                //Check whether that Master Item have sub Items.
            Outer:// Nadeeka 07-08-2015
                if (_isSubItemHave)
                {
                    //Get the relevant sub items.
                    List<MasterItemComponent> _itemComponentList = CHNLSVC.Inventory.GetItemComponents(_mainItemCode);
                    if (_itemComponentList == null)
                    {
                        _isSubItemHave = false;
                        goto Outer;
                    }
                    if ((_itemComponentList != null) && (_itemComponentList.Count > 0))
                    {
                        //Update qty for existing items.
                        foreach (MasterItemComponent _itemCompo in _itemComponentList)
                        {
                            if (_invReqItemList != null)
                                if (_invReqItemList.Count > 0)
                                    _resultList = _temp.Where(x => x.MasterItem.Mi_cd.Equals(_itemCompo.ComponentItem.Mi_cd) && x.Itri_itm_stus == _itemStatus).ToList();

                            // If selected sub item exists in the grid,update the qty.
                            if ((_resultList != null) && (_resultList.Count > 0))
                                foreach (InventoryRequestItem _result in _resultList)
                                    _result.Itri_qty = _result.Itri_qty + (_mainItemQty * _itemCompo.Micp_qty);
                            else
                            {
                                // If selected sub item does not exists in the grid add it.
                                InventoryRequestItem _inventoryRequestItem = new InventoryRequestItem();
                                decimal _subItemQty = _mainItemQty * _itemCompo.Micp_qty;

                                MasterItem _componentItem = new MasterItem();
                                _componentItem.Mi_cd = _itemCompo.ComponentItem.Mi_cd;
                                _inventoryRequestItem.Itri_itm_cd = _itemCompo.ComponentItem.Mi_cd;
                                _componentItem.Mi_longdesc = _itemCompo.ComponentItem.Mi_longdesc;
                                _inventoryRequestItem.Mi_longdesc = _itemCompo.ComponentItem.Mi_longdesc;
                                _componentItem.Mi_model = _itemCompo.ComponentItem.Mi_model;
                                _inventoryRequestItem.Mi_model = _itemCompo.ComponentItem.Mi_model;
                                _componentItem.Mi_brand = _itemCompo.ComponentItem.Mi_brand;
                                _inventoryRequestItem.Mi_brand = _itemCompo.ComponentItem.Mi_brand;
                                _inventoryRequestItem.MasterItem = _componentItem;

                                _inventoryRequestItem.Itri_itm_stus = _itemStatus;
                                _inventoryRequestItem.Itri_res_no = _reservationNo;
                                _inventoryRequestItem.Itri_note = _remarksText;
                                _inventoryRequestItem.Itri_qty = _subItemQty;
                              
                                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "INV3"))
                                {
                                    _inventoryRequestItem.Itri_qty = _subItemQty;
                                    _inventoryRequestItem.Itri_app_qty = _subItemQty;
                                    _inventoryRequestItem.Itri_bqty = _subItemQty;
                                
                                }
                                        
                                _inventoryRequestItem.Itri_app_qty = _mainItemQty; //tharanga
                                //if (_)
                                //{

                                if (_line <= 0)
                                {
                                    maxlineNo++;
                                    _inventoryRequestItem.Itri_line_no = maxlineNo;
                                }
                                else
                                {
                                    _inventoryRequestItem.Itri_line_no = _line;
                                }

                                //Add Main item details.
                                _inventoryRequestItem.Itri_mitm_cd = _mainItemCode;
                                _inventoryRequestItem.Itri_mitm_stus = _itemStatus;
                                _inventoryRequestItem.Itri_mqty = _mainItemQty;

                                ////Add location
                                _inventoryRequestItem.Itri_loc = IsRecalled ? Recallocation : BaseCls.GlbUserDefLoca;

                                _line = 0;
                                _invReqItemList.Add(_inventoryRequestItem);
                            }
                        }
                    }
                }
                else
                {
                    #region Check Consignment Items :: Chamal 07-May-2014
                    if (_invReqItemList != null)
                    {
                        if (_invReqItemList.Count > 0)
                        {
                            if (_itemStatus == "CONS")
                            {
                                _resultList = _temp.Where(x => x.Itri_itm_stus != _itemStatus).ToList();
                                if ((_resultList != null) && (_resultList.Count > 0))
                                {
                                    MessageBox.Show("Please create separate entry for consignment items", "Consignment Items Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            else
                            {
                                _resultList = _temp.Where(x => x.Itri_itm_stus == "CONS").ToList();
                                if ((_resultList != null) && (_resultList.Count > 0))
                                {
                                    MessageBox.Show("Please create separate entry for consignment items", "Consignment Items Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                    }
                    #endregion

                    // If selected sub item exists in the grid,update the qty.
                    if ((_resultList != null) && (_resultList.Count > 0))
                    {
                        if (MessageBox.Show(txtItem.Text + " already added. Do you need to add this qty?", "Duplicate Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            foreach (InventoryRequestItem _result in _resultList)
                                _result.Itri_qty = _result.Itri_qty + _mainItemQty;
                    }


                    if (_invReqItemList != null)
                        if (_invReqItemList.Count > 0)
                            _resultList = _temp.Where(x => x.MasterItem.Mi_cd.Equals(txtItem.Text.Trim()) && x.Itri_itm_stus == _itemStatus).ToList();

  

                    // If selected sub item exists in the grid,update the qty.
                    if ((_resultList != null) && (_resultList.Count > 0))
                    {
                        if (MessageBox.Show(txtItem.Text + " already added. Do you need to add this qty?", "Duplicate Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            foreach (InventoryRequestItem _result in _resultList)
                                _result.Itri_qty = _result.Itri_qty + _mainItemQty;
                    }
                    else
                    {
                        //Add new item to existing list.
                        InventoryRequestItem _inventoryRequestItem = new InventoryRequestItem();

                        MasterItem _masterItem = new MasterItem();
                        _masterItem.Mi_cd = _mainItemCode;
                        _inventoryRequestItem.Itri_itm_cd = _mainItemCode;
                        _masterItem.Mi_longdesc = _itemdetail.Mi_longdesc;
                        _inventoryRequestItem.Mi_longdesc = _itemdetail.Mi_longdesc;
                        _masterItem.Mi_model = _itemdetail.Mi_model;
                        _inventoryRequestItem.Mi_model = _itemdetail.Mi_model;
                        _masterItem.Mi_brand = _itemdetail.Mi_brand;
                        _inventoryRequestItem.Mi_brand = _itemdetail.Mi_brand;
                        _inventoryRequestItem.MasterItem = _masterItem;

                        if (_line <= 0)
                        {
                           maxlineNo++;
                           _inventoryRequestItem.Itri_line_no = maxlineNo;
                        }
                        else
                        {
                            _inventoryRequestItem.Itri_line_no = _line;
                        }
                        

                        if (!string.IsNullOrEmpty(txtQty.Text))
                        {
                            
                             _inventoryRequestItem.Itri_app_qty = Convert.ToDecimal(txtQty.Text.ToString());
                             _inventoryRequestItem.Itri_bqty = Convert.ToDecimal(txtQty.Text.ToString());
                        }

                      
                        _inventoryRequestItem.Itri_itm_stus = _itemStatus;
                        _inventoryRequestItem.Itri_res_no = _reservationNo;
                        _inventoryRequestItem.Itri_note = _remarksText;
                        _inventoryRequestItem.Itri_qty = _mainItemQty;
                        _inventoryRequestItem.Itri_app_qty = _mainItemQty; //Tharanga _inventoryRequestItem.Itri_app_qty =0

                        //Add Main item details.
                        _inventoryRequestItem.Itri_mitm_cd = _mainItemCode;
                        _inventoryRequestItem.Itri_mitm_stus = _itemStatus;
                        _inventoryRequestItem.Itri_mqty = 0;
                        _inventoryRequestItem.Itri_bqty = _mainItemQty; //Tharanda add this line

                        //Add location
                        _inventoryRequestItem.Itri_loc = BaseCls.GlbUserDefLoca.ToString();
                        _line = 0;
                        _invReqItemList.Add(_inventoryRequestItem);
                    }
                }

                //Clear add new data.
                ClearLayer2();
                ClearLayer3();

                //Bind the updated list to grid.
                gvItem.DataSource = new List<InventoryRequestItem>();
                gvItem.DataSource = _invReqItemList;

                pnlBalance.Visible = false;

                if (ddlRequestSubType.SelectedValue.ToString() != "SALES")
                    if (MessageBox.Show("Do you need to add another item?", "Adding...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        txtItem.Focus();
                    else
                        btnSave.Select();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtReservation.Text))
                { // Nadeeka 28-05-2015
                    DataTable _dt = CHNLSVC.Inventory.GetReservationDet(BaseCls.GlbUserComCode, txtReservation.Text, txtItem.Text, cmbStatus.SelectedValue.ToString());
                    if (_dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid Reservation #.", "Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtReservation.Text = "";
                        txtReservation.Focus();
                        return;

                    }
                    
                }
                
                if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "SALES")
                {
                    if (string.IsNullOrEmpty(txtInvoice.Text))
                    {
                        MessageBox.Show("Please select the invoice no first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    BindInventoryRequestItemsGridData();
                }
                else if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "ADVAN")
                {
                    if (string.IsNullOrEmpty(txtInvoice.Text))
                    {
                        MessageBox.Show("Please select the advance receipt no first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    // check receipt items.....
                    List<ReceiptItemDetails> _recItems = new List<ReceiptItemDetails>();
                    _recItems = CHNLSVC.Sales.GetAdvanReceiptItems(txtInvoice.Text.Trim());
                    Boolean _isRecItem = false;
                    if (_recItems != null)
                    {
                        foreach (ReceiptItemDetails _rItm in _recItems)
                        {
                            if (_rItm.Sari_item == txtItem.Text.Trim())
                            {
                                _isRecItem = true;
                                goto LD01;
                            }
                        }
                    }
                    else
                    {
                        _isRecItem = true;
                    }

                LD01:
                    if (_isRecItem == false)
                    {
                        MessageBox.Show("Request item not in advance receipt.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    BindInventoryRequestItemsGridData();
                }
                else
                {
                    BindInventoryRequestItemsGridData();
                }


            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Item Grid Events
        private void LoadEditData(int rowIndex)
        {
            try
            {
                //Get the selected item from list.

                string _mainItem = Convert.ToString(gvItem.Rows[rowIndex].Cells["Itm_MainItem"].Value);
                string _item = Convert.ToString(gvItem.Rows[rowIndex].Cells["Itm_Item"].Value);
                string _status = Convert.ToString(gvItem.Rows[rowIndex].Cells["Itm_Status"].Value);

                _line = Convert.ToInt32(gvItem.Rows[rowIndex].Cells["Itm_No"].Value);

                List<InventoryRequestItem> _invRequestItemList = _invReqItemList.Where(x => x.Itri_itm_cd == _item && x.Itri_itm_stus == _status).ToList();
                InventoryRequestItem _inventoryRequestItem = _invRequestItemList[0];
               // appQyt = Convert.ToInt32(gvItem.Rows[rowIndex].Cells["Itri_qty"].Value);

                //Set selected data.
                txtItem.Text = _mainItem;

                cmbStatus.SelectedValue = _status;

                txtReservation.Text = _inventoryRequestItem.Itri_res_no;
                txtQty.Text = _inventoryRequestItem.Itri_qty.ToString();
                txtItmRemark.Text = _inventoryRequestItem.Itri_note;

                _invReqItemList.RemoveAll(x => x.Itri_mitm_cd == _mainItem && x.Itri_itm_stus == _status);
                gvItem.DataSource = new List<InventoryRequestItem>();
                gvItem.DataSource = _invReqItemList;
                txtItem.Focus();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }

        }
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

                            if (MessageBox.Show("Do you need to remove this item", "Remove...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                string _mainItem = Convert.ToString(gvItem.Rows[_rowIndex].Cells["Itm_MainItem"].Value);
                                string _item = Convert.ToString(gvItem.Rows[_rowIndex].Cells["Itm_Item"].Value);
                                string _status = Convert.ToString(gvItem.Rows[_rowIndex].Cells["Itm_Status"].Value);
                                
                               
                                _invReqItemList.RemoveAll(x => x.Itri_mitm_cd == _mainItem && x.Itri_itm_stus == _status);
                                gvItem.DataSource = new List<InventoryRequestItem>();
                                gvItem.DataSource = _invReqItemList;
                            }
                        }

                        if (e.ColumnIndex == 1)
                        {
                            if (MessageBox.Show("Do you need to edit this item", "Edit...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                approveAll = false;
                                LoadEditData(_rowIndex);
                            }
                        }
                        if (e.ColumnIndex == 6)
                        {

                        }

                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { 
                        //this.Cursor = Cursors.Default;
                        CHNLSVC.CloseAllChannels(); 
                    }
        }
        #endregion

        #region Rooting for Re-Call Request
        private void LoadReceiptDetails()
        {
            RecieptHeader _ReceiptHeader = null;
            ClearCustomerDetail();
            _ReceiptHeader = CHNLSVC.Sales.GetReceiptHeaderByType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoice.Text.Trim(), ddlRequestSubType.SelectedValue.ToString().ToUpper());
            if (_ReceiptHeader != null)
            {
                if (_ReceiptHeader.Sar_act == false)
                {
                    MessageBox.Show("Selected advance receipt is already cancelled.", "Invalid Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvoice.Clear();
                    return;
                }

                if (_ReceiptHeader.Sar_used_amt > 0)
                {
                    MessageBox.Show("Selected advance receipt is already used or refunded. Cannot use for request.", "Invalid Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvoice.Clear();
                    return;
                }

                lblCusCode.Text = _ReceiptHeader.Sar_debtor_cd;
                lblCusName.Text = _ReceiptHeader.Sar_debtor_name;
                lblCusAddress.Text = _ReceiptHeader.Sar_debtor_add_1 + " " + _ReceiptHeader.Sar_debtor_add_2;
            }
            else
            {
                MessageBox.Show("Please select the valid receipt no", "Invalid Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInvoice.Clear();
                return;
            }
        }
        private void SetSelectedInventoryRequestData(InventoryRequest _selectedInventoryRequest)
        {
            if (_selectedInventoryRequest != null)
            {
                ddlRequestSubType.SelectedValue = _selectedInventoryRequest.Itr_sub_tp;
                txtRequest.Text = _selectedInventoryRequest.Itr_req_no;
                ddlCompany.SelectedValue = _selectedInventoryRequest.Itr_issue_com; //add chamal 17-08-2012
                txtDispatchRequried.Text = _selectedInventoryRequest.Itr_issue_from;
                txtRequestDate.Text = _selectedInventoryRequest.Itr_dt.ToString("dd/MMM/yyyy");
                txtRequriedDate.Text = _selectedInventoryRequest.Itr_exp_dt.ToString("dd/MMM/yyyy");
                txtInvoice.Text = _selectedInventoryRequest.Itr_job_no;

                txtNIC.Text = _selectedInventoryRequest.Itr_collector_id;
                txtCollecterName.Text = _selectedInventoryRequest.Itr_collector_name;
                txtRemark.Text = _selectedInventoryRequest.Itr_note;

                //Set Item details.
                if (_selectedInventoryRequest.InventoryRequestItemList != null)
                {
                    BindingSource _source = new BindingSource();
                    _selectedInventoryRequest.InventoryRequestItemList.ForEach(X => X.Itri_mitm_cd = X.Itri_itm_cd);
                    _selectedInventoryRequest.InventoryRequestItemList.ForEach(X => X.Itri_mitm_stus = X.Itri_itm_stus);
                    _invReqItemList = _selectedInventoryRequest.InventoryRequestItemList;
                    _source.DataSource = _selectedInventoryRequest.InventoryRequestItemList;
                    gvItem.DataSource = _source;
                }
                else
                {
                    MessageBox.Show("There is no pending items", "Pending Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    InitializeForm(true);
                    InitVariables();
                    return;
                }

                if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "SALES")
                {
                    txtInvoice.Text = _selectedInventoryRequest.Itr_job_no;
                    txtInvoice_Leave(null, null);
                }
                else if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "ADVAN")
                {

                    txtInvoice.Text = _selectedInventoryRequest.Itr_job_no;
                    LoadReceiptDetails();
                }

                if (_selectedInventoryRequest.Itr_stus.ToUpper() == "A" || _selectedInventoryRequest.Itr_stus.ToUpper() == "F" || _selectedInventoryRequest.Itr_stus.ToUpper() == "C")
                {
                    btnApproved.Enabled = true; //tharanga change to true
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                }
                else if (_selectedInventoryRequest.Itr_stus.ToUpper() == "P")
                {
                    btnApproved.Enabled = false;
                    btnSave.Enabled = true;
                    string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                    if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "INV3"))
                    {
                        btnApproved.Enabled = true;
                        btnSave.Enabled = false;
                        btnAddItem.Enabled = false;
                    }
                }

                DataTable _tbl = CHNLSVC.Inventory.GetUserNameByUserID(_selectedInventoryRequest.Itr_cre_by);
                if (_tbl != null && _tbl.Rows.Count > 0)
                {
                    string _name = _tbl.Rows[0].Field<string>("se_usr_name");
                    txtRequestBy.Text = _name;
                }
                else txtRequestBy.Text = string.Empty;

                if (_selectedInventoryRequest.Itr_stus == "A")
                {// Nadeeka 13-06-2015
                    if (!string.IsNullOrEmpty(_selectedInventoryRequest.Itr_gran_app_by))
                    {
                        DataTable _tbl0 = CHNLSVC.Inventory.GetUserNameByUserID(_selectedInventoryRequest.Itr_gran_app_by);
                        //DataTable _tbl0 = CHNLSVC.Inventory.GetUserNameByUserID(_selectedInventoryRequest.Itr_mod_by);
                        if (_tbl0 != null && _tbl0.Rows.Count > 0)
                        {
                            string _name = _tbl0.Rows[0].Field<string>("se_usr_name");
                            txtApprovedBy.Text = _name;
                        }
                    }
                    else
                    {
                        DataTable _tbl0 = CHNLSVC.Inventory.GetUserNameByUserID(_selectedInventoryRequest.Itr_mod_by);
                        if (_tbl0 != null && _tbl0.Rows.Count > 0)
                        {
                            string _name = _tbl0.Rows[0].Field<string>("se_usr_name");
                            txtApprovedBy.Text = _name;
                        }
                    }

                }
                else txtApprovedBy.Text = string.Empty;


            }
        }
        private void txtRequest_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRequest.Text)) return;
            Regex r = new Regex("^[a-zA-Z0-9]*$");

            //if (!Regex.IsMatch(txtRequest.Text, "^[a-zA-Z0-9]*$"))
            //{
            //    MessageBox.Show("Please enter valid Requst Number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtRequest.Text = "";
            //    return;
            //}
            try
            {
                _line = 0;
                _recallloc = string.Empty;
                IsRecalled = false;
                InventoryRequest _inputInvReq = new InventoryRequest();
                _inputInvReq.Itr_req_no = txtRequest.Text.Trim();
                InventoryRequest _selectedInventoryRequest = CHNLSVC.Inventory.GetInventoryRequestData(_inputInvReq);
                maxlineNo =int.Parse( _selectedInventoryRequest.InventoryRequestItemList.Count.ToString());
                 _issue_from = _selectedInventoryRequest.Itr_issue_from;//._selectedInventoryRequest.Itr_issue_from;
                 txtDispatchRequried.Text = _issue_from;
                _inputInvReq.Itr_rec_to = _selectedInventoryRequest.Itr_issue_from;
               // _inputInvReq.Itr_loc = _selectedInventoryRequest.Itr_loc;
                if (_selectedInventoryRequest != null)
                    if (!string.IsNullOrEmpty(_selectedInventoryRequest.Itr_com))
                    {
                        _recallloc = _selectedInventoryRequest.Itr_loc;
                        Recallocation = _selectedInventoryRequest.Itr_loc;
                        IsRecalled = true;

                        if (_selectedInventoryRequest.Itr_stus == "A") 
                            btnPrint.Enabled = true; 
                        else
                            btnPrint.Enabled = false;
                        this.SetSelectedInventoryRequestData(_selectedInventoryRequest);
                        if ((_selectedInventoryRequest.Itr_stus == "A" || _selectedInventoryRequest.Itr_stus == "P") && _isApprovedUser)
                        {
                            btnCancel.Enabled = true;
                            btnAddItem.Enabled = true;
                        }
                        else btnCancel.Enabled = false;

                        return;
                    }

                MessageBox.Show("Request no is invalid", "Request No", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void LoadRequestData()
        {
            string req_no = txtRequest.Text.Trim();
            InventoryRequest _inputInvReq = new InventoryRequest();
            _inputInvReq.Itr_req_no = req_no;
            InventoryRequest _selectedInventoryRequest = CHNLSVC.Inventory.GetInventoryRequestData(_inputInvReq);
            this.SetSelectedInventoryRequestData(_selectedInventoryRequest);
            //updPnlMRNDataEntry.Update();
        }

        #endregion

        #region Just a Comment
        //private string _lastQty = "0";
        //private void gvItem_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        //{
        //    if (gvItem.Rows.Count > 0)
        //    {
        //        Int32 _rowIndex = e.RowIndex;
        //        _lastQty = Convert.ToString(gvItem.Rows[_rowIndex].Cells["Itm_Qty"].Value);
        //    }
        //}

        //private void gvItem_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (gvItem.Rows.Count > 0)
        //    {
        //        Int32 _rowIndex = e.RowIndex;
        //        string _qty = Convert.ToString(gvItem.Rows[_rowIndex].Cells["Itm_Qty"].Value);
        //        if (IsNumeric(_qty) == false)
        //        {
        //            MessageBox.Show("Please enter valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            //gvItem.DataSource = new List<InventoryRequestItem>();
        //            //gvItem.DataSource = _invReqItemList;
        //            gvItem.Rows[_rowIndex].Cells["Itm_Qty"].Value = _lastQty;
        //            gvItem.Refresh();
        //            return;
        //        }

        //        string _item = Convert.ToString(gvItem.Rows[e.RowIndex].Cells["Itm_Item"].Value);
        //        bool _isAllowDecimal = CHNLSVC.Inventory.IsUOMDecimalAllow(_item);
        //        if (!_isAllowDecimal)
        //        {
        //            if (IsValid(_qty))
        //            {
        //                MessageBox.Show("Not allow decimal values for this item", "Decimal Not Allow", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                gvItem.Rows[_rowIndex].Cells["Itm_Qty"].Value = _lastQty;
        //                gvItem.Refresh();
        //                return;
        //            }
        //        }


        //    }
        //}

        //private void gvItem_DataError(object sender, DataGridViewDataErrorEventArgs e)
        //{
        //    if (gvItem.Rows.Count > 0)
        //    {



        //        MessageBox.Show("Please enter valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //gvItem.DataSource = new List<InventoryRequestItem>();
        //        //gvItem.DataSource = _invReqItemList;
        //        gvItem.Rows[e.RowIndex].Cells["Itm_Qty"].Value = _lastQty;
        //        gvItem.Refresh();
        //        return;
        //    }
        //}

        //GetLocationByLocCode
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
        bool AutoApprove = false;
        private bool IsValidLocation()
        {
            bool status = false;
            txtDispatchRequried.Text = txtDispatchRequried.Text.Trim().ToUpper().ToString();
            MasterLocation _masterLocationDisp = CHNLSVC.General.GetLocationByLocCode(Convert.ToString(ddlCompany.SelectedValue), txtDispatchRequried.Text.ToString());
            MasterLocation _masterLocationFrm = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            status = (_masterLocationDisp == null) ? false : true;
            if (_masterLocationDisp != null)
                if (!string.IsNullOrEmpty(_masterLocationDisp.Ml_com_cd))
                {
                    string _fromcompany = BaseCls.GlbUserComCode;
                    string _fromlocation = BaseCls.GlbUserDefLoca;
                    string _tocompany = Convert.ToString(ddlCompany.SelectedValue);
                    string _toLocation = txtDispatchRequried.Text.Trim();
                    //string _tocategory = _masterLocation.Ml_cate_3;
                    string _fromChnl = _masterLocationFrm.Ml_cate_3;
                    string _tochnl = _masterLocationDisp.Ml_cate_3;

                    //DataTable _tfrm = CHNLSVC.Inventory.GetLocationChannel(_fromcompany, _fromlocation);
                    //DataTable _tto = CHNLSVC.Inventory.GetLocationChannel(_tocompany, _toLocation);
                    //if (_tfrm != null && _tfrm.Rows.Count > 0)
                    //    _fromChnl = _tfrm.Rows[0].Field<string>("mli_val");
                    //if (_tto != null && _tto.Rows.Count > 0)
                    //    _tochnl = _tto.Rows[0].Field<string>("mli_val");

                    //DataTable _permission = CHNLSVC.Inventory.GetToLocationPermission(_fromcompany, _fromlocation, _tocompany, _tocategory, "INTRS_AUTOAPP");
                    //if (!AutoApprove)
                    //    if (_permission != null || _permission.Rows.Count > 0) AutoApprove = false; else AutoApprove = true;
                    //if (_isApprovedUser) AutoApprove = true;


                    if (string.IsNullOrEmpty(_fromChnl) || string.IsNullOrEmpty(_tochnl))
                    { MessageBox.Show("There is no channel define for the " + (string.IsNullOrEmpty(_fromChnl) ? "your location " + _fromlocation : "selected location " + _toLocation) + ". Please contact it dept.", "Channel Not Define", MessageBoxButtons.OK, MessageBoxIcon.Information); status = false; return status; }
                    DataTable _tbs = CHNLSVC.Inventory.GetChannelPermission("INTR", Convert.ToString(ddlRequestSubType.SelectedValue), _fromChnl, _tochnl);
                    if (_tbs != null && _tbs.Rows.Count > 0)
                    {
                        AutoApprove = true;
                    }
                    else
                    {
                        string _subtype = ddlRequestSubType.SelectedValue.ToString();
                        List<MasterSubType> _lst = CHNLSVC.General.GetAllSubTypes("INTR");
                        var _isApprove = _lst.Where(x => x.Mstp_cd.Equals(_subtype) && x.Mstp_isapp).ToList();
                        if (_isApprove != null && _isApprove.Count > 0) AutoApprove = true;
                        else AutoApprove = false;
                    }
                    if (_isApprovedUser) AutoApprove = true;
                }
            return status;
        }
        private void txtDispatchRequried_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDispatchRequried.Text)) return;
            if (txtDispatchRequried.Text.Trim() == BaseCls.GlbUserDefLoca)
            { MessageBox.Show("You can not enter same location which you already logged.", "Invalid Location", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDispatchRequried.Clear(); return; }
            try
            {
                if (!chkReqType.Checked)
                {
                    if (IsValidLocation() == false)
                    {
                        MessageBox.Show("Invalid location code or permission not allow for the selected location.", "Dispatching Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDispatchRequried.Clear();
                        txtDispatchRequried.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; txtDispatchRequried.Clear(); SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
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

        #region Rooting for Printing
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRequest.Text)) return;
                Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SMRNRepPrints.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_MRNRepPrints.rpt" : "MRNRepPrints.rpt";
                _view.GlbReportDoc = txtRequest.Text.Trim();
                _view.Show();
                _view = null;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Sub Type
        private void ddlRequestSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ddlRequestSubType.SelectedValue.ToString())) return;
                //lblCaption
                string _subtype = ddlRequestSubType.SelectedValue.ToString();
                InitializeForm(false);
                InitVariables();
                BindingSource _source = new BindingSource();
                _source.DataSource = new List<InvoiceItem>();
                gvInvoice.DataSource = _source;
                List<MasterSubType> _lst = CHNLSVC.General.GetAllSubTypes("INTR");
                var _isApprove = _lst.Where(x => x.Mstp_cd.Equals(_subtype) && x.Mstp_isapp).ToList();
                if (_isApprove != null && _isApprove.Count > 0) AutoApprove = true;
                else AutoApprove = false;

                switch (_subtype)
                {
                    case "ADVAN":
                        lblCaption.Text = "Receipt";
                        pnlInvoice.Enabled = true;
                        pnlInvoiceItem.Visible = false;
                        lblDisplayOnly.Visible = false;
                        this.Height = 617 - 105;
                        pnlItemAdd.Enabled = true;
                        lblCusCode.Text = string.Empty;
                        lblCusName.Text = string.Empty;
                        lblCusAddress.Text = string.Empty;
                        txtInvoice.Clear();
                        break;
                    case "DISP":
                        lblCaption.Text = string.Empty;
                        pnlInvoice.Enabled = false;
                        pnlInvoiceItem.Visible = false;
                        lblDisplayOnly.Visible = true;
                        lblDisplayOnly.Text = "Only Display";
                        this.Height = 617 - 105;
                        pnlItemAdd.Enabled = true;
                        lblCusCode.Text = string.Empty;
                        lblCusName.Text = string.Empty;
                        lblCusAddress.Text = string.Empty;
                        txtInvoice.Clear();
                        break;
                    case "SALES":
                        lblCaption.Text = "Invoice";
                        pnlInvoice.Enabled = true;
                        pnlInvoiceItem.Visible = true;
                        lblDisplayOnly.Visible = false;
                        this.Height = 617;
                        pnlItemAdd.Enabled = false;
                        lblCusCode.Text = string.Empty;
                        lblCusName.Text = string.Empty;
                        lblCusAddress.Text = string.Empty;
                        txtInvoice.Clear();
                        break;
                    case "AGETR":
                        lblCaption.Text = string.Empty;
                        pnlInvoice.Enabled = false;
                        pnlInvoiceItem.Visible = false;
                        lblDisplayOnly.Visible = true;
                        lblDisplayOnly.Text = "Age Item Transfer";
                        this.Height = 617 - 105;
                        pnlItemAdd.Enabled = true;
                        lblCusCode.Text = string.Empty;
                        lblCusName.Text = string.Empty;
                        lblCusAddress.Text = string.Empty;
                        txtInvoice.Clear();
                        break;
                    case "EXCHG":
                        lblCaption.Text = string.Empty;
                        pnlInvoice.Enabled = false;
                        pnlInvoiceItem.Visible = false;
                        lblDisplayOnly.Visible = true;
                        lblDisplayOnly.Text = "Product Exchange";
                        this.Height = 617 - 105;
                        pnlItemAdd.Enabled = true;
                        lblCusCode.Text = string.Empty;
                        lblCusName.Text = string.Empty;
                        lblCusAddress.Text = string.Empty;
                        txtInvoice.Clear();
                        break;
                    case "RVRRT":
                        lblCaption.Text = string.Empty;
                        pnlInvoice.Enabled = false;
                        pnlInvoiceItem.Visible = false;
                        lblDisplayOnly.Visible = true;
                        lblDisplayOnly.Text = "Reverse and Re-report";
                        this.Height = 617 - 105;
                        pnlItemAdd.Enabled = true;
                        lblCusCode.Text = string.Empty;
                        lblCusName.Text = string.Empty;
                        lblCusAddress.Text = string.Empty;
                        txtInvoice.Clear();
                        break;
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Customer Detail Clear
        private void ClearCustomerDetail()
        {
            lblCusCode.Text = string.Empty;
            lblCusName.Text = string.Empty;
            lblCusAddress.Text = string.Empty;

            BindingSource _source = new BindingSource();
            gvInvoice.DataSource = _source;
        }
        #endregion

        #region Rooting for Collect Invoice Detail
        private void txtInvoice_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInvoice.Text)) return;

                DataTable _chk = CHNLSVC.Sales.CheckTheDocument(BaseCls.GlbUserComCode, txtInvoice.Text.Trim());
                if (_chk != null && _chk.Rows.Count > 0)
                {
                    string _refDocument = _chk.Rows[0].Field<string>("itr_req_no");
                    if (ddlRequestSubType.SelectedValue.ToString() == "SALES")
                        MessageBox.Show("This invoice is already picked for a inter-transfer. You are not allow to raise another request for the pending reference.", "Picked Document", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (ddlRequestSubType.SelectedValue.ToString() == "ADVAN")
                        MessageBox.Show("This advance receipt is already picked for a inter-transfer. You are not allow to raise another request for the pending reference.", "Picked Document", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvoice.Text = "";
                    txtInvoice.Focus();
                    return;
                }

                if (ddlRequestSubType.SelectedValue.ToString() == "ADVAN")
                {
                    DataTable _adv = CHNLSVC.Sales.CheckAdvanForIntr(BaseCls.GlbUserComCode, txtInvoice.Text.Trim());
                    if (_adv != null && _adv.Rows.Count > 0)
                    {
                        MessageBox.Show("This advance receipt is already picked for a inter-transfer. You are not allow to raise another request for the pending reference.", "Picked Document", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInvoice.Text = "";
                        txtInvoice.Focus();
                        return;
                    }

                    //kapila 30/8/2016 - block if advance receipt > 3 months
                    HpSystemParameters _SystemPara = new HpSystemParameters();
                    decimal _maxDays = 0;
                    _SystemPara = CHNLSVC.Sales.GetSystemParameter("COM", "ALL", "INTTRANS", DateTime.Now.Date);

                    if (_SystemPara.Hsy_cd != null)
                    {
                        _maxDays = _SystemPara.Hsy_val;

                        RecieptHeader _ReceiptHeader = null;
                        ClearCustomerDetail();
                        _ReceiptHeader = CHNLSVC.Sales.GetReceiptHeaderByType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoice.Text.Trim(), ddlRequestSubType.SelectedValue.ToString().ToUpper());
                        if (_ReceiptHeader != null)
                        {
                            if( Convert.ToInt32( (DateTime.Now.Date - _ReceiptHeader.Sar_receipt_date).TotalDays) > _maxDays)
                            {
                                MessageBox.Show("This advance receipt is expired for a inter-transfer. You are not allow to raise request for the reference.", "Picked Document", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtInvoice.Text = "";
                                txtInvoice.Focus();
                                return;
                            }
                        }
                    }



                }


                if (ddlRequestSubType.SelectedValue.ToString() == "SALES")
                {
                    InvoiceHeader _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoice.Text.Trim());
                    if (string.IsNullOrEmpty(_hdr.Sah_com))
                    {
                        MessageBox.Show("Please check the invoice no", "Invalid Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (_hdr.Sah_pc != BaseCls.GlbUserDefProf)
                    {
                        MessageBox.Show("You are going to select " + _hdr.Sah_pc + " invoice.", "Invalid Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    List<InvoiceItem> _lst = CHNLSVC.Sales.GetInterTransferInvoice(txtInvoice.Text.Trim());
                    if (_lst == null || _lst.Count <= 0)
                    {
                        MessageBox.Show("There is no pending item for request inter-transfer.", "Invoice Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    ClearCustomerDetail();

                    lblCusCode.Text = _hdr.Sah_cus_cd;
                    lblCusName.Text = _hdr.Sah_cus_name;
                    lblCusAddress.Text = _hdr.Sah_cus_add1 + " " + _hdr.Sah_cus_add2;

                    BindingSource _source = new BindingSource();
                    _lst.ForEach(x => x.Sad_qty = x.Sad_qty - x.Sad_do_qty);
                    _source.DataSource = _lst;
                    gvInvoice.DataSource = _source;
                }

                if (ddlRequestSubType.SelectedValue.ToString() == "ADVAN")
                {
                    LoadReceiptDetails();
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }

        }
        #endregion

        #region Rooting for Invoice Grid Event
        private void gvInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvInvoice.RowCount > 0)
                {
                    int _rowIndex = e.RowIndex;
                    if (_rowIndex != -1)
                    {

                        if (gvInvoice.Columns[e.ColumnIndex].Name == "inv_select")
                        {
                            string _item = Convert.ToString(gvInvoice.Rows[_rowIndex].Cells["inv_Item"].Value);
                            string _description = Convert.ToString(gvInvoice.Rows[_rowIndex].Cells["inv_Description"].Value);
                            string _brand = string.Empty;
                            string _model = Convert.ToString(gvInvoice.Rows[_rowIndex].Cells["inv_Model"].Value);
                            string _status = Convert.ToString(gvInvoice.Rows[_rowIndex].Cells["inv_status"].Value);
                            decimal _qty = Convert.ToDecimal(gvInvoice.Rows[_rowIndex].Cells["inv_Qty"].Value);
                            string _reservation = string.Empty;
                            string _remarks = string.Empty;

                            if (_qty == 1)
                            {
                                decimal _alreadyAvailables = _invReqItemList.Where(x => x.Itri_itm_cd == _item && x.Itri_itm_stus == _status).Sum(y => y.Itri_qty);
                                if (_alreadyAvailables != 0)
                                {
                                    decimal _currentAdded = _alreadyAvailables;
                                    decimal _wannaAdd = _qty;
                                    if (_currentAdded + _wannaAdd > _qty)
                                    {
                                        MessageBox.Show("You are not allow to enter more than invoice qty", "Invalid Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }

                                txtItem.Text = _item;
                                CheckItemCode(null, null);
                                cmbStatus.SelectedValue = _status;
                                txtQty.Text = Convert.ToString(_qty);
                                btnAddItem.Focus();
                                BindInventoryRequestItemsGridData();
                                return;
                            }

                            string _cashGiven = Interaction.InputBox("Please enter required qty.", "Required Qty", Convert.ToString(_qty), 100, 0);
                            if (string.IsNullOrEmpty(_cashGiven))
                            {
                                MessageBox.Show("You are not selected any qty amount. Item adding terminated", "No Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (IsNumeric(_cashGiven) == false)
                            {
                                MessageBox.Show("Please select the valid qty.", "Invalid Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (Convert.ToDecimal(_cashGiven) > _qty)
                            {
                                MessageBox.Show("You are not allow to enter more than invoice qty", "Invalid Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }




                            decimal _alreadyAvailable = _invReqItemList.Where(x => x.Itri_itm_cd == _item && x.Itri_itm_stus == _status).Sum(y => y.Itri_qty);
                            if (_alreadyAvailable != 0)
                            {
                                decimal _currentAdded = _alreadyAvailable;
                                decimal _wannaAdd = Convert.ToDecimal(_cashGiven);
                                if (_currentAdded + _wannaAdd > _qty)
                                {
                                    MessageBox.Show("You are not allow to enter more than invoice qty", "Invalid Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }

                            txtItem.Text = _item;
                            CheckItemCode(null, null);
                            cmbStatus.SelectedValue = _status;
                            txtQty.Text = Convert.ToString(_cashGiven);
                            btnAddItem.Focus();
                            BindInventoryRequestItemsGridData();
                        }
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Save Inter-Transfer
        private MasterAutoNumber GenerateMasterAutoNumber()
        {
            string moduleText = "INTR";

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_cate_cd = string.IsNullOrEmpty(BaseCls.GlbUserDefLoca) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = moduleText;
            masterAuto.Aut_number = 0;
            masterAuto.Aut_start_char = moduleText;
            masterAuto.Aut_year = null;

            return masterAuto;
        }
        private string GetRequestNo()
        {
            string _reqNo = string.Empty;

            if (string.IsNullOrEmpty(txtRequest.Text))
                _reqNo = "DOC-" + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();
            else
                _reqNo = txtRequest.Text;

            return _reqNo;
        }
        private void SaveInventoryRequestData()
        {
            try
            {
                Regex r = new Regex("^[a-zA-Z0-9]*$");

                if (!Regex.IsMatch(txtRequest.Text, "^[a-zA-Z0-9]*$"))
                {
                    MessageBox.Show("Please enter valid center code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                if (CheckServerDateTime() == false) return;
                //UI validation.
                if (string.IsNullOrEmpty(ddlRequestSubType.SelectedValue.ToString()))
                { this.Cursor = Cursors.Default; MessageBox.Show("Please select request type.", "Request Type", MessageBoxButtons.OK, MessageBoxIcon.Information); ddlRequestSubType.Focus(); return; }
                if (!chkReqType.Checked)
                {
                    if (string.IsNullOrEmpty(txtDispatchRequried.Text))
                    { this.Cursor = Cursors.Default; MessageBox.Show("Please enter dispatch required location.", "Dispatch Location", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDispatchRequried.Focus(); return; }
                }
                else
                {
                    txtDispatchRequried.Text = "N/A";
                }
                if (txtDispatchRequried.Text.ToString().ToUpper() == BaseCls.GlbUserDefLoca.ToString().ToUpper())
                { this.Cursor = Cursors.Default; MessageBox.Show("Please enter valid dispatch required location.", "Request Type", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDispatchRequried.Focus(); return; }

                //MasterUserLocation Or MasterUserProfitCenter.
                if (string.IsNullOrEmpty(txtRequestDate.Text))
                { this.Cursor = Cursors.Default; MessageBox.Show("Please enter request date.", "Request Date", MessageBoxButtons.OK, MessageBoxIcon.Information); txtRequestDate.Focus(); return; }

                if (string.IsNullOrEmpty(txtRequriedDate.Text))
                { this.Cursor = Cursors.Default; MessageBox.Show("Please enter required date.", "Required Date", MessageBoxButtons.OK, MessageBoxIcon.Information); txtRequestDate.Focus(); return; }

                if (DateTime.Compare(Convert.ToDateTime(txtRequriedDate.Text.Trim()), Convert.ToDateTime(txtRequestDate.Text.Trim())) < 0)
                { this.Cursor = Cursors.Default; MessageBox.Show("Required date can't be less than request date.", "Required Date", MessageBoxButtons.OK, MessageBoxIcon.Information); txtRequestDate.Focus(); return; }



                if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "SALES")
                {
                    if (string.IsNullOrEmpty(txtInvoice.Text))
                    {
                        this.Cursor = Cursors.Default; MessageBox.Show("Please enter Invoice No.", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information); txtInvoice.Focus();
                        return;
                    }

                    //check invoice item qty
                    decimal _curQty = 0;
                    decimal _invQty = 0;
                    decimal _reqQty = 0;
                    foreach (InventoryRequestItem _tmpItm in _invReqItemList)
                    {
                        _curQty = _tmpItm.Itri_qty;

                        DataTable dt = CHNLSVC.Sales.GetInvItemQty(txtInvoice.Text.Trim(), _tmpItm.Itri_itm_cd);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow drow in dt.Rows)
                            {
                                _invQty = _invQty + Convert.ToDecimal(drow["inv_qty"]);
                            }
                        }

                        DataTable dt1 = CHNLSVC.Sales.GetReqItemQty("INTR", "SALES", txtInvoice.Text.Trim(), _tmpItm.Itri_itm_cd);

                        if (dt1.Rows.Count > 0)
                        {
                            foreach (DataRow drow1 in dt1.Rows)
                            {
                                _reqQty = _reqQty + Convert.ToDecimal(drow1["reqQty"]);
                            }
                        }

                        if (_invQty < (_reqQty + _curQty))
                        {
                            MessageBox.Show("You are not allow to request more than invoice qty.", "Invalid Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                }
                else if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "ADVAN")
                {
                    if (string.IsNullOrEmpty(txtInvoice.Text))
                    { this.Cursor = Cursors.Default; MessageBox.Show("Please enter Receipt No.", "Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information); txtInvoice.Focus(); return; }
                }

                int _count = 1;
                _invReqItemList.ForEach(x => x.Itri_line_no = _count++);
                _invReqItemList.ForEach(X => X.Itri_bqty = X.Itri_qty);
                List<InventoryRequestItem> _inventoryRequestItemList = _invReqItemList;
                if ((_inventoryRequestItemList == null) || (_inventoryRequestItemList.Count == 0))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please add items to List.", "No Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtNIC.Text))
                { txtNIC.Text = string.Empty; }
                else
                {
                    if (IsValidNIC(txtNIC.Text.ToString()) == false)
                    { this.Cursor = Cursors.Default; MessageBox.Show("Please enter valid NIC No.", "NIC", MessageBoxButtons.OK, MessageBoxIcon.Information); txtNIC.Focus(); return; }
                }

                string _masterLocation = IsRecalled ? _recallloc : (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                InventoryRequest _inventoryRequest = new InventoryRequest();

                //Fill the InventoryRequest header & footer data.
                _inventoryRequest.Itr_com = BaseCls.GlbUserComCode;
                _inventoryRequest.Itr_req_no = GetRequestNo();
                //_inventoryRequest.Itr_tp = ddlRequestType.SelectedValue;
                _inventoryRequest.Itr_tp = "INTR";
                _inventoryRequest.Itr_sub_tp = Convert.ToString(ddlRequestSubType.SelectedValue);
                _inventoryRequest.Itr_loc = BaseCls.GlbUserDefLoca;
                _inventoryRequest.Itr_ref = string.Empty;
                _inventoryRequest.Itr_dt = Convert.ToDateTime(txtRequestDate.Text);
                _inventoryRequest.Itr_exp_dt = Convert.ToDateTime(txtRequriedDate.Text);
                if (chkReqType.Checked == true && chkReqType.Visible == true)
                {
                    _inventoryRequest.Itr_stus = "P";// Nadeeka 27-02-2015  tharanga change tp "P"
                }
                else
                {
                    _inventoryRequest.Itr_stus = AutoApprove == false ? "P" : "A";  //P - Pending , A - Approved, C - Cancel
                }
                if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "SALES")
                {
                    _inventoryRequest.Itr_job_no = txtInvoice.Text;  //Invoice No.
                    _inventoryRequest.Itr_bus_code = lblCusCode.Text;  //Customer Code.
                }
                else if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "ADVAN")
                {
                    _inventoryRequest.Itr_job_no = txtInvoice.Text;  //Invoice No.
                    _inventoryRequest.Itr_bus_code = lblCusCode.Text;  //Customer Code.
                }
                else if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "DISP")
                {
                    _inventoryRequest.Itr_job_no = "DISP";
                }
                _inventoryRequest.Itr_note = txtRemark.Text;
                _inventoryRequest.Itr_issue_from = txtDispatchRequried.Text;
                _inventoryRequest.Itr_rec_to = _masterLocation;
                _inventoryRequest.Itr_direct = 0;
                _inventoryRequest.Itr_country_cd = string.Empty;  //Country Code.
                _inventoryRequest.Itr_town_cd = string.Empty;     //Town Code.
                _inventoryRequest.Itr_cur_code = string.Empty;    //Currency Code.
                _inventoryRequest.Itr_exg_rate = 0;              //Exchange rate.
                _inventoryRequest.Itr_collector_id = string.IsNullOrEmpty(txtNIC.Text) ? string.Empty : txtNIC.Text.Trim();
                _inventoryRequest.Itr_collector_name = string.IsNullOrEmpty(txtCollecterName.Text) ? string.Empty : txtCollecterName.Text.Trim();
                _inventoryRequest.Itr_act = 1;
                _inventoryRequest.Itr_cre_by = BaseCls.GlbUserID;
                _inventoryRequest.Itr_session_id = BaseCls.GlbUserSessionID;
                _inventoryRequest.Itr_issue_com = ddlCompany.SelectedValue.ToString();

                _inventoryRequest.InventoryRequestItemList = _inventoryRequestItemList;
                if (AutoApprove) _inventoryRequest.InventoryRequestItemList.ForEach(x => x.Itri_app_qty = x.Itri_qty);
                int rowsAffected = 0;
                string _docNo = string.Empty;

                if (string.IsNullOrEmpty(txtRequest.Text))
                {
                    rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, GenerateMasterAutoNumber(), out _docNo);
                    this.Cursor = Cursors.Default;
                    if (rowsAffected != -1)
                    {
                        MessageBox.Show("Inventory Request Document Successfully saved. " + _docNo, "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (AutoApprove || (chkReqType.Checked == true && chkReqType.Visible == true))
                        {
                            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                            BaseCls.GlbReportName = string.Empty;
                            GlbReportName = string.Empty;
                            _view.GlbReportName = string.Empty;
                            _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SMRNRepPrints.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_MRNRepPrints.rpt" : "MRNRepPrints.rpt";
                            _view.GlbReportDoc = _docNo;
                            _view.Show();
                            _view = null;
                        }
                    }
                    else
                        MessageBox.Show(_docNo, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, null, out _docNo);
                    this.Cursor = Cursors.Default;
                    if (rowsAffected != -1)
                        MessageBox.Show("Inventory Request Document Successfully Updated.", "Update...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show(_docNo, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                InitVariables();
                LoadCachedObjects();
                InitializeForm(true);
                pnlBalance.Size = new Size(504, 177);
                ddlRequestSubType.SelectedIndex = 3;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
            finally { this.Cursor = Cursors.Default; }
        }
        private bool IsBackDateOk()
        {
            bool _isOK = true;
            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, txtRequestDate, lblBackDateInfor, txtRequestDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (txtRequestDate.Value.Date != DateTime.Now.Date)
                    {
                        txtRequestDate.Enabled = true;
                        MessageBox.Show("Back date not allow for selected date for the location " + BaseCls.GlbUserDefLoca + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRequestDate.Focus();
                        _isOK = false;
                        return _isOK;
                    }
                }
                else
                {
                    txtRequestDate.Enabled = true;
                    MessageBox.Show("Back date not allow for selected date for the location " + BaseCls.GlbUserDefLoca + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRequestDate.Focus();
                    _isOK = false;
                    return _isOK;
                }
            }
            return _isOK;
        }
        protected void btnMRNSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Added darshana due to stock verification process
                //if (BaseCls.GlbUserComCode == "ABL")
                //{
                //    MessageBox.Show("Sorry. Inter transfers are temporary unavailable till stock verification over.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                //else if (BaseCls.GlbUserComCode == "LRP")
                //{
                //    MessageBox.Show("Sorry. Inter transfers are temporary unavailable till stock verification over.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                #region Back Date
                if (IsBackDateOk() == false) return;
                #endregion

                if (CheckServerDateTime() == false) return;
                if (MessageBox.Show("Do you need to process this inter-transfer request?", "Save...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    this.SaveInventoryRequestData();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Approve The Document
        private void ApprovedSelectedRequest()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtRequest.Text)) { this.Cursor = Cursors.Default; MessageBox.Show("Please select request before Approved.", "Approve...", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                InventoryRequest _inputInvReq = new InventoryRequest();
                _inputInvReq.Itr_req_no = txtRequest.Text.ToString().Trim();
                _inputInvReq.Itr_com = BaseCls.GlbUserComCode;
                _inputInvReq.Itr_loc = IsRecalled ? _recallloc : BaseCls.GlbUserDefLoca;
                _inputInvReq.Itr_req_no = txtRequest.Text.Trim();
                _inputInvReq.Itr_stus = "A";
                _inputInvReq.Itr_mod_by = BaseCls.GlbUserID;
                _inputInvReq.Itr_session_id = BaseCls.GlbUserSessionID;
                _inputInvReq.Itr_gran_app_by = txtApprovedBy.Text;
                _inputInvReq.Itr_issue_from = _issue_from;
                

                if (approveAll)
                {
                  foreach(InventoryRequestItem _tmpItem in _invReqItemList)
                  {
                      _invReqItemList.Where(x => x.Itri_itm_cd == _tmpItem.Itri_itm_cd).ToList().ForEach(x => { x.Itri_app_qty = _tmpItem.Itri_qty; x.Itri_bqty = _tmpItem.Itri_qty; });
                     // _invReqItemList.Where(x => x.Itri_itm_cd == _tmpItem.Itri_itm_cd).ToList().ForEach(x => { x.Itri_com = _tmpItem.Itri_com; });
                      
                  }
                }

                int result = CHNLSVC.Inventory.UpdateInventoryRequestStatus(_inputInvReq, _invReqItemList);
                if (result > 0)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Inventory Request " + _inputInvReq.Itr_req_no + " Successfully Approved.", "Approve...", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    _view.GlbReportName = string.Empty;
                    _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SMRNRepPrints.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_MRNRepPrints.rpt" : "MRNRepPrints.rpt";
                    _view.GlbReportDoc = txtRequest.Text;
                    _view.Show();
                    _view = null;
                }
                else { this.Cursor = Cursors.Default; MessageBox.Show("Inventory Request " + _inputInvReq.Itr_req_no + " can't be Approved.", "Approve...", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                InitVariables();
                LoadCachedObjects();
                InitializeForm(true);
                pnlBalance.Size = new Size(504, 177);
                ddlRequestSubType.SelectedIndex = 3;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }
        private void btnApproved_Click(object sender, EventArgs e)
        {
            try
            {
                //Added darshana due to stock verification process
                //if (BaseCls.GlbUserComCode == "ABL")
                //{
                //    MessageBox.Show("Sorry. Inter transfers are temporary unavailable till stock verification over.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                //else if (BaseCls.GlbUserComCode == "LRP")
                //{
                //    MessageBox.Show("Sorry. Inter transfers are temporary unavailable till stock verification over.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                if (CheckServerDateTime() == false) return;
                if (string.IsNullOrEmpty(txtApprovedBy.Text))
                {
                    MessageBox.Show("Please enter the approval person name", "Approval", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Do you need to approve this inter-transfer request?", "Approve...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                   
                    this.ApprovedSelectedRequest();
                    //this.SaveInventoryRequestData();
                    btnPrint.Enabled = true;
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Cancel Document
        private void CancelSelectedRequest()
        {
            try
            {
                if (string.IsNullOrEmpty(txtRequest.Text))
                { MessageBox.Show("Please select request before cancel.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                if (DateTime.Compare(Convert.ToDateTime(txtRequestDate.Text.Trim()), DateTime.Now.Date) != 0)
                { MessageBox.Show("Request date should be current date in order to Cancel.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                InventoryRequest _inputInvReq = new InventoryRequest();
                _inputInvReq.Itr_com = BaseCls.GlbUserComCode;
                _inputInvReq.Itr_loc = IsRecalled ? _recallloc : BaseCls.GlbUserDefLoca;
                _inputInvReq.Itr_req_no = txtRequest.Text;
                _inputInvReq.Itr_stus = "C";
                _inputInvReq.Itr_mod_by = BaseCls.GlbUserID;
                _inputInvReq.Itr_session_id = BaseCls.GlbUserSessionID;

                int result = CHNLSVC.Inventory.UpdateInventoryRequestStatus(_inputInvReq);

                if (result > 0)
                {
                    MessageBox.Show("Inventory Request " + _inputInvReq.Itr_req_no + " successfully Cancel.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InitVariables();
                    LoadCachedObjects();
                    InitializeForm(true);
                    pnlBalance.Size = new Size(504, 177);
                    ddlRequestSubType.SelectedIndex = 3;
                }
                else

                    MessageBox.Show("Inventory Request " + _inputInvReq.Itr_req_no + " can't be Cancel.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information); ;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //Added darshana due to stock verification process
                //if (BaseCls.GlbUserComCode == "ABL")
                //{
                //    MessageBox.Show("Sorry. Inter transfers are temporary unavailable till stock verification over.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                //else if (BaseCls.GlbUserComCode == "LRP")
                //{
                //    MessageBox.Show("Sorry. Inter transfers are temporary unavailable till stock verification over.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                if (CheckServerDateTime() == false) return;
                if (MessageBox.Show("Do you need to cancel this request?", "Canceling...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CancelSelectedRequest();
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRequest.Text))
                { MessageBox.Show("Please select the request no", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information); txtRequest.Clear(); txtRequest.Focus(); return; }

                Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                BaseCls.GlbReportName = string.Empty;
                GlbReportName = string.Empty;
                _view.GlbReportName = string.Empty;
                _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SMRNRepPrints.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_MRNRepPrints.rpt" : "MRNRepPrints.rpt";
                _view.GlbReportDoc = txtRequest.Text.Trim();
                _view.Show();
                _view = null;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }


        }

        private void InterTransferDocument_Load(object sender, EventArgs e)
        {
            try
            {
                BackDatePermission();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void InterTransferDocument_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
        }

        private void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDispatchRequried.Clear();
        }

        private void btnUserLocation_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLoc;
                _CommonSearch.ShowDialog();
                txtLoc.Select();


                //if (!string.IsNullOrEmpty(txtLoc.Text))
                //{
                //    if (CheckLocation(BaseCls.GlbUserComCode, txtLoc.Text.ToString()) == false)
                //    {
                //        MessageBox.Show("Selected location " + txtLoc.Text.ToString() + " is invalid or inactivated!", "Invalid Location", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return;
                //    }

                //}
                //CHNLSVC.CloseAllChannels();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtLoc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtLoc;
                    _CommonSearch.ShowDialog();
                    txtLoc.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    cmbSearchStatus.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtLoc_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLoc;
                _CommonSearch.ShowDialog();
                txtLoc.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                txtLoc.Focus();
            }
        }

        private void cmbSearchStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnGetInvoices.Focus();
            }
        }

        private void txtLoc_Leave(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(txtLoc.Text)) return;
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                //DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);

                //var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtLoc.Text.Trim()).ToList();
                //if (_validate == null || _validate.Count <= 0)
                //{
                //    MessageBox.Show("Invalid / none permission location.", "Location Validating", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtLoc.Clear();
                //    txtLoc.Focus();
                //    return;
                //}

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGetInvoices_Click(object sender, EventArgs e)
        {
            try
            {
                string _status = "";
                string _subTp = "";
                if (!string.IsNullOrEmpty(cmbSearchStatus.Text))
                {
                    if (cmbSearchStatus.Text == "APPROVED")
                    {
                        _status = "A";
                    }
                    else if (cmbSearchStatus.Text == "PENDING")
                    {
                        _status = "P";
                    }
                }
                else
                {
                    _status = "";
                }


                if (chkAll.Checked == true)
                {
                    _subTp = "";
                }
                else if (string.IsNullOrEmpty(ddlSearchReason.SelectedValue.ToString()))
                {
                    MessageBox.Show("Please select reason.", "Location Validating", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    _subTp = Convert.ToString(ddlSearchReason.SelectedValue);
                }

                Cursor.Current = Cursors.WaitCursor;

                DataTable _tmpReq = new DataTable();
                DataTable _newReq = new DataTable();
                DataRow dr;

                _newReq.Columns.Add("ITR_REQ_NO", typeof(string));
                _newReq.Columns.Add("ITR_LOC", typeof(string));
                _newReq.Columns.Add("STATUS", typeof(string));
                _newReq.Columns.Add("ITR_DT", typeof(DateTime));
                _newReq.Columns.Add("MSTP_DESC", typeof(string));

                _tmpReq = CHNLSVC.Inventory.Get_InterTrans_Req(BaseCls.GlbUserComCode, txtLoc.Text.Trim(), "INTR", _status, dtpFrom.Value.Date, dtpTo.Value.Date, _subTp);

                foreach (DataRow drow in _tmpReq.Rows)
                {
                    DataTable _tempReq = CHNLSVC.Sales.GetinvBatch(drow["ITR_REQ_NO"].ToString());

                    if (_tempReq.Rows.Count == 0)
                    {
                        dr = _newReq.NewRow();
                        dr["ITR_REQ_NO"] = drow["ITR_REQ_NO"].ToString();
                        dr["ITR_LOC"] = drow["ITR_LOC"].ToString();
                        dr["STATUS"] = drow["STATUS"].ToString();
                        dr["ITR_DT"] = drow["ITR_DT"];
                        dr["MSTP_DESC"] = drow["MSTP_DESC"].ToString();
                        _newReq.Rows.Add(dr);
                    }


                }

                dgvPromo.AutoGenerateColumns = false;
                dgvPromo.DataSource = new DataTable();
                dgvPromo.DataSource = _newReq;

                Cursor.Current = Cursors.Default;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPromo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                ch1 = (DataGridViewCheckBoxCell)dgvPromo.Rows[dgvPromo.CurrentRow.Index].Cells[0];

                if (ch1.Value == null)
                    ch1.Value = false;
                switch (ch1.Value.ToString())
                {
                    case "False":
                        {
                            ch1.Value = true;
                            break;
                        }
                    case "True":
                        {
                            ch1.Value = false;
                            break;
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

        private void btnSelectAllPromo_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvPromo.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chk.Value) == false)
                {
                    chk.Value = true;
                }

            }
        }

        private void btnUnSelectAllPromo_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvPromo.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chk.Value) == true)
                {
                    chk.Value = false;
                }

            }
        }

        private void btnBulkClear_Click(object sender, EventArgs e)
        {
            txtLoc.Text = "";
            cmbSearchStatus.Text = "";
            dgvPromo.AutoGenerateColumns = false;
            dgvPromo.DataSource = new DataTable();
        }

        private void btnCancelBulk_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean _appPromo = false;
                InventoryRequest _inputInvReq = new InventoryRequest();
                List<InventoryRequest> _ReqCancelDet = new List<InventoryRequest>();

                foreach (DataGridViewRow row in dgvPromo.Rows)
                {
                    DataGridViewCheckBoxCell chk = row.Cells["col_p_Get"] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        _appPromo = true;
                        goto L4;
                    }
                }
            L4:

                if (_appPromo == false)
                {
                    MessageBox.Show("No any request is select for cancel.", "Canceling....", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show("Do you want to cancel selected requests ?", "Scheme Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                foreach (DataGridViewRow row in dgvPromo.Rows)
                {
                    DataGridViewCheckBoxCell chk = row.Cells["col_p_Get"] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        _inputInvReq = new InventoryRequest();

                        _inputInvReq.Itr_com = BaseCls.GlbUserComCode;
                        _inputInvReq.Itr_loc = row.Cells["ITR_LOC"].Value.ToString();
                        _inputInvReq.Itr_req_no = row.Cells["ITR_REQ_NO"].Value.ToString();
                        _inputInvReq.Itr_stus = "C";
                        _inputInvReq.Itr_mod_by = BaseCls.GlbUserID;
                        _inputInvReq.Itr_session_id = BaseCls.GlbUserSessionID;
                        _ReqCancelDet.Add(_inputInvReq);
                    }
                }

                int result = CHNLSVC.Inventory.UpdateInventoryRequestStatusBulk(_ReqCancelDet);

                if (result > 0)
                {
                    MessageBox.Show("Requests are successfully Cancel.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InitVariables();
                    LoadCachedObjects();
                    InitializeForm(true);
                    pnlBalance.Size = new Size(504, 177);
                    ddlRequestSubType.SelectedIndex = 3;
                }
                else

                    MessageBox.Show("Requests are can't be Cancel.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information); ;


            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBulkCancel_Click(object sender, EventArgs e)
        {
            //Added darshana due to stock verification process
            //if (BaseCls.GlbUserComCode == "ABL")
            //{
            //    MessageBox.Show("Sorry. Inter transfers are temporary unavailable till stock verification over.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //else if (BaseCls.GlbUserComCode == "LRP")
            //{
            //    MessageBox.Show("Sorry. Inter transfers are temporary unavailable till stock verification over.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            if (pnlBulkCancel.Visible == true)
            {
                pnlBulkCancel.Visible = false;
            }
            else
            {
                pnlBulkCancel.Visible = true;
            }
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtQty.Text))
            {
                if (IsNumeric(txtQty.Text))
                {
                    if (_isDecimalAllow == false) txtQty.Text = decimal.Truncate(Convert.ToDecimal(txtQty.Text.ToString())).ToString();
                }
                else
                {
                    txtQty.Clear();
                    txtQty.Focus();
                }
            }

        }

        private void chkReqType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReqType.Checked == true)
            {
                txtDispatchRequried.Enabled = false;
            }
            else
            {
                txtDispatchRequried.Enabled = true;
            }

        }

        private void txtRequest_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

      




    }
}
