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

namespace FF.WindowsERPClient.Inventory
{
    //Web base system written by Chamal (Original)
    //Windows base system written by Prabhath on 7/01/2013 according to the web

    public partial class FAMaterialRequisition : FF.WindowsERPClient.Base
    {
        #region Variables
        private MasterItem _itemdetail = null;
        private DataTable _CompanyItemStatus = null;
        List<FA_Inventory_Req_Itm> _invReqItemList = null;
        bool _isDecimalAllow = false;
        string _fix_loc = "";
        #endregion

        #region Rooting for Form Initialize
        private void SystemErrorMessage(Exception ex)
        {
            CHNLSVC.CloseChannel();
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        private void button4_Click(object sender, EventArgs e)
        {

        }
        private void LoadCachedObjects()
        {
            _CompanyItemStatus = CacheLayer.Get<DataTable>(CacheLayer.Key.CompanyItemStatus.ToString());
        }

        private void BindReceiveCompany()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ddlDispCom.DataSource = new List<MasterCompany>();
                List<MasterCompany> _list = CHNLSVC.General.GetALLMasterCompaniesData();
                _list.Add(new MasterCompany { Mc_cd = "" });
                var _lst = _list.OrderBy(items => items.Mc_cd).ToList();
                BindingSource _souce = new BindingSource();
                _souce.DataSource = _lst;
                ddlDispCom.DataSource = _souce.DataSource;
                ddlDispCom.DisplayMember = "Mc_cd";
                ddlDispCom.ValueMember = "Mc_cd";
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
            }
            finally { this.Cursor = Cursors.Default; }
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
            List<MasterSubType> _lst = CHNLSVC.General.GetAllSubTypes("FAMRN");
            var _n = new MasterSubType();
            _n.Mstp_cd = string.Empty;
            _n.Mstp_desc = string.Empty;
            _lst.Insert(0, _n);
            ddl.DataSource = _lst;
            ddl.DisplayMember = "MSTP_DESC";
            ddl.ValueMember = "MSTP_CD";
        }
        private void InitVariables()
        {
            gvBalance.AutoGenerateColumns = false;
            gvItem.AutoGenerateColumns = false;
            _invReqItemList = new List<FA_Inventory_Req_Itm>();
        }
        private void InitializeForm()
        {
            txtRequestDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MM/yyyy");
            btnCancel.Enabled = false;
            ClearLayer1();
            ClearLayer2();
            ClearLayer3();
            ClearLayer4();
            ClearLayer5();
            ClearLayer6();
            gvItem.Columns["DISPD_REQUSTED_QTY"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight;
            BindReceiveCompany();
            ddlRequestSubType.Enabled = true;
            ddlDispCom.Enabled = true;
        }
        public FAMaterialRequisition()
        {
            InitializeComponent();
            try
            {
                InitVariables();
                LoadCachedObjects();
                InitializeForm();
                pnlBalance.Size = new Size(504, 177);
                ddlRequestSubType.SelectedIndex = 1;
                BindReceiveCompany();
                ddlRequestSubType.Text = "";
                ddlDispCom.Text = "";
                ddlRequestSubType.Enabled = true;
                ddlDispCom.Enabled = true;
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
                case CommonUIDefiniton.SearchUserControlType.Items_FA:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Status_FA:
                    {
                        paramsText.Append(seperator);
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

                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(ddlDispCom.SelectedValue.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MRN:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + _fix_loc + seperator);
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

        private void btnSearch_BaseDoc_Click(object sender, EventArgs e)
        {
            MessageBox.Show("New feature.. Pending!");
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
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchMRN(_CommonSearch.SearchParams, null, null);
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
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Items_FA);
                DataTable _result = CHNLSVC.CommonSearch.getSearchFAItemData(_CommonSearch.SearchParams, null, null);
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
                txtDispatchRequried.Focus();
        }
        private void txtDispatchRequried_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtBaseDocument.Focus();
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
                txtStatus.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_Item_Click(null, null);
        }
        private void txtStatus_KeyDown(object sender, KeyEventArgs e)
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
        #endregion

        #region Rooting for Clear Screen
        private void ClearLayer1()
        {
           
            txtRequestDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtRequriedDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            BindRequestSubTypesDDLData(ddlRequestSubType);
            txtDispatchRequried.Clear();
            txtBaseDocument.Clear();
            txtRequest.Clear();

        }
        private void ClearLayer2()
        {
            txtItem.Clear();
           // BindUserCompanyItemStatusDDLData(cmbStatus);
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
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you need to clear the screen?", "Clear...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    InitializeForm();
                    InitVariables();
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
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
            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;
            lblItemSubStatus.Text = "Sub Item Status : " + string.Empty;
            _isDecimalAllow = false;
          //  _itemdetail = new MasterItem();
            DataTable _dtItemMaster = null;

            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item)) _dtItemMaster = CHNLSVC.Inventory.GetFAItem( _item);
            if (_dtItemMaster.Rows.Count>0)
                if (!string.IsNullOrEmpty(_dtItemMaster.Rows[0]["item_code"].ToString()))
                {
                    _isValid = true;
                    string _description = _dtItemMaster.Rows[0]["description"].ToString();
                    string _model = _dtItemMaster.Rows[0]["model_no"].ToString();
                    string _brand = _dtItemMaster.Rows[0]["brand_code"].ToString();
                 ////   string _serialstatus = Convert.ToBoolean(_dtItemMaster.Rows[0]["Mi_is_subitem"]) == true ? "Available" : "Non";

                    lblItemDescription.Text = "Description : " + _description;
                    lblItemModel.Text = "Model : " + _model;
                    lblItemBrand.Text = "Brand : " + _brand;
                  ////  lblItemSubStatus.Text = "Sub Item Status : " + _serialstatus;
                  ////  _isDecimalAllow = CHNLSVC.Inventory.IsUOMDecimalAllow(_item);

                }

            return _isValid;
        }
        #endregion

        #region Rooting for Check Inventory Balance and Display
        private void DisplayAvailableQty(string _item, Label _avalQty, Label _freeQty, string _status)
        {
            List<InventoryLocation> _inventoryLocation = null;
            if (string.IsNullOrEmpty(_status))
                _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, _fix_loc, _item.Trim(), string.Empty);
            else
                _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, _fix_loc, _item.Trim(), _status);

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
           
            if (string.IsNullOrEmpty(txtItem.Text.Trim())) return;

            try
            { 
               
                if (string.IsNullOrEmpty(ddlRequestSubType.Text))
                {
                    MessageBox.Show("Please select the document sub type.", "Sub Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ddlRequestSubType.Focus();
                    txtItem.Clear();
                    return;
                }
                if (string.IsNullOrEmpty(txtDispatchRequried.Text))
                {
                    txtDispatchRequried.Text = "N/A";// Nadeeka 26-02-2015 (Requested by Chamal)
                    //MessageBox.Show("Please select the dispatch location.", "Dispatch Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //txtDispatchRequried.Focus();
                    //txtItem.Clear();
                    //return;
                }
                if (!LoadItemDetail(txtItem.Text.Trim()))
                {
                    MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Clear();
                    txtItem.Focus();
                    return;
                }

                ////if (_itemdetail.Mi_is_discont == 1)
                ////{// NADEEKA 23-02-2015
                ////    MessageBox.Show("This item " + txtItem.Text.Trim() + " is discontinue", "Discontinue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////    txtItem.Clear();
                ////    txtItem.Focus();
                ////    return;
                ////}


                ////if (CHNLSVC.Inventory.checkReqItemAllow(txtItem.Text.Trim()) == 1)
                ////{// NADEEKA 23-02-2015
                ////    if (CHNLSVC.Inventory.checkReqItemAllowCompany(txtItem.Text.Trim(), BaseCls.GlbUserComCode) == 0)
                ////    {
                ////        MessageBox.Show("This item  " + txtItem.Text.Trim() + " is not allowed for " + BaseCls.GlbUserComCode + " Company", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////        txtItem.Clear();
                ////        txtItem.Focus();
                ////        return;
                ////    }
                ////}

               //// List<MasterItemComponent> _itemComponentList = CHNLSVC.Inventory.GetItemComponents(txtItem.Text.Trim());
               //// MasterItem _itemcomponent = null;
               //////CHNLSVC.Inventory.GetItemComponentTable 
               //// // NADEEKA 24-02-2015
               //// if ((_itemComponentList != null) && (_itemComponentList.Count > 0))
               //// {
               ////     foreach (MasterItemComponent _itemCompo in _itemComponentList)
               ////     {
               ////         _itemcomponent = new MasterItem();
               ////         _itemcomponent = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itemCompo.ComponentItem.Mi_cd);
               ////         if (_itemcomponent != null)
               ////         { 
               ////             if (_itemcomponent.Mi_is_discont == 1)
               ////             {
               ////                 MessageBox.Show("Item code "+ _itemCompo.ComponentItem.Mi_cd+" is discontinue "  , "Discontinue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
               ////                 txtItem.Clear();
               ////                 txtItem.Focus();
               ////                 break;
               ////             }
               ////                 if (CHNLSVC.Inventory.checkReqItemAllow(_itemCompo.ComponentItem.Mi_cd) == 1)
               ////                 {// NADEEKA 23-02-2015
               ////                     if (CHNLSVC.Inventory.checkReqItemAllowCompany(_itemCompo.ComponentItem.Mi_cd, BaseCls.GlbUserComCode) == 0)
               ////                     {
               ////                         MessageBox.Show("Item code "+_itemCompo.ComponentItem.Mi_cd+" is not allowed for " + BaseCls.GlbUserComCode + " Company. ", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
               ////                         txtItem.Clear();
               ////                         txtItem.Focus();
               ////                         return;
               ////                     }
               ////                 }
                            
               ////         }
               ////     }
               //// }




               //// if (string.IsNullOrEmpty(txtStatus.Text))
               ////     DisplayAvailableQty(txtItem.Text.Trim(), lblAvalQty, lblFreeQty, string.Empty);
               //// else
               ////     DisplayAvailableQty(txtItem.Text.Trim(), lblAvalQty, lblFreeQty, txtStatus.Text);
               //// if (_itemdetail.Mi_itm_tp != "V")
               ////     LoadDispatchLocationInventoryBalance(txtItem.Text.Trim());
               //// else
               ////     txtReservation.Focus();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Check Item Status
        private void CheckItemStatus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStatus.Text.Trim())) return;
            try
            {
                if (string.IsNullOrEmpty(txtStatus.Text))
                    DisplayAvailableQty(txtItem.Text.Trim(), lblAvalQty, lblFreeQty, string.Empty);
                else
                    DisplayAvailableQty(txtItem.Text.Trim(), lblAvalQty, lblFreeQty, txtStatus.Text);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Load Dispatch Location Invnetory Balance
        private void LoadDispatchLocationInventoryBalance(string _item)
        {
            List<InventoryLocation> _lst = CHNLSVC.Inventory.GetInventoryBalanceSCMnSCM2(BaseCls.GlbUserComCode, txtDispatchRequried.Text, _item, string.Empty);
            if (_lst != null)
                if (_lst.Count > 0)
                {
                    //pnlBalance.Visible = true;
                    gvBalance.DataSource = new DataTable();
                    gvBalance.DataSource = _lst;
                    gvBalance.Focus();
                }
        }
        #endregion

        #region Rooting for Show Invnetory Balance
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
                            txtStatus.Text = Convert.ToString(_cmbstatus[0].Field<string>("MIS_DESC"));
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
                {
                    Int32 _rowIndex = gvBalance.SelectedRows[0].Index;
                    SelectItemStatusfromBalance(_rowIndex);
                }
                if (e.KeyCode == Keys.Escape)
                {
                    pnlBalance.Visible = false;
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void BindInventoryRequestItemsGridData()
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    MessageBox.Show("Please enter item code.", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }


                if (string.IsNullOrEmpty(txtStatus.Text))
                {
                    MessageBox.Show("Please select a item status.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtStatus.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    MessageBox.Show("Please enter required quantity.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Focus();
                    return;
                }

                if (IsNumeric(txtQty.Text.Trim()) == false)
                {
                    MessageBox.Show("Please enter valid quantity.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtQty.Text.ToString()) <= 0)
                {
                    MessageBox.Show("Please enter valid quantity.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(txtReservation.Text))
                { // Nadeeka 28-05-2015
                    DataTable _dt = CHNLSVC.Inventory.GetReservationDet(BaseCls.GlbUserComCode, txtReservation.Text, txtItem.Text,  txtStatus.Text);
                    if (_dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid Reservation #.", "Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtReservation.Text = "";
                        txtReservation.Focus();
                        return;

                    }
                }
                
                //Get existing items details from the grid.

                string _mainItemCode = txtItem.Text.Trim().ToUpper();
                string _itemStatus = txtStatus.Text;
                string _reservationNo = string.IsNullOrEmpty(txtReservation.Text.Trim()) ? "N/A" : txtReservation.Text.Trim();
                decimal _mainItemQty = (string.IsNullOrEmpty(txtQty.Text)) ? 0 : Convert.ToDecimal(txtQty.Text.Trim());
                string _remarksText = string.IsNullOrEmpty(txtItmRemark.Text.Trim()) ? "N/A" : txtItmRemark.Text.Trim();
                bool _isSubItemHave = (string.IsNullOrEmpty(lblItemSubStatus.Text)) ? false : lblItemSubStatus.Text.Trim() == "Sub Item Status : Available" ? true : false;

                List<FA_Inventory_Req_Itm> _temp = _invReqItemList;
                //This is a temporary collation for newly added items.
                List<FA_Inventory_Req_Itm> _resultList = null;

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
                                    _resultList = _temp.Where(x => x.MasterItem.Mi_cd.Equals(_itemCompo.ComponentItem.Mi_cd) && x.Dispd_item_status == _itemStatus).ToList();

                            // If selected sub item exists in the grid,update the qty.
                            if ((_resultList != null) && (_resultList.Count > 0))
                                foreach (FA_Inventory_Req_Itm _result in _resultList)
                                    _result.Dispd_requsted_qty = _result.Dispd_requsted_qty + (_mainItemQty * _itemCompo.Micp_qty);
                            else
                            {
                                // If selected sub item does not exists in the grid add it.
                                FA_Inventory_Req_Itm _inventoryRequestItem = new FA_Inventory_Req_Itm();
                                decimal _subItemQty = _mainItemQty * _itemCompo.Micp_qty;

                                MasterItem _componentItem = new MasterItem();
                                _componentItem.Mi_cd = _itemCompo.ComponentItem.Mi_cd;
                                _inventoryRequestItem.Dispd_item_code = _itemCompo.ComponentItem.Mi_cd;
                                _componentItem.Mi_longdesc = _itemCompo.ComponentItem.Mi_longdesc;
                                _inventoryRequestItem.Dispd_item_desc = _itemCompo.ComponentItem.Mi_longdesc;
                                _componentItem.Mi_model = _itemCompo.ComponentItem.Mi_model;
                                _inventoryRequestItem.Dispd_item_model = _itemCompo.ComponentItem.Mi_model;
                                _componentItem.Mi_brand = _itemCompo.ComponentItem.Mi_brand;
                                _inventoryRequestItem.Dispd_item_brand = _itemCompo.ComponentItem.Mi_brand;
                                _inventoryRequestItem.MasterItem = _componentItem;
                                

                                _inventoryRequestItem.Dispd_item_status = _itemStatus;
                                _inventoryRequestItem.Dispd_res_no = _reservationNo;
                                _inventoryRequestItem.Dispd_remarks = _remarksText;
                                _inventoryRequestItem.Dispd_requsted_qty = _subItemQty;
                                _inventoryRequestItem.Dispd_approved_qty = 0;
                                _inventoryRequestItem.Dispd_uom = "N/A";

                                //Add Main item details.
                                //_inventoryRequestItem.Dispd_item_code = _mainItemCode;
                                //_inventoryRequestItem.Itri_mitm_stus = _itemStatus;
                                //_inventoryRequestItem.Itri_mqty = _mainItemQty;

                                _invReqItemList.Add(_inventoryRequestItem);
                            }
                        }
                    }
                }
                else
                {

                    if (_invReqItemList != null)
                        if (_invReqItemList.Count > 0)
                            _resultList = _temp.Where(x => x.MasterItem.Mi_cd.Equals(txtItem.Text.Trim()) && x.Dispd_item_status == _itemStatus).ToList();

                    // If selected sub item exists in the grid,update the qty.
                    if ((_resultList != null) && (_resultList.Count > 0))
                    {
                        if (MessageBox.Show(txtItem.Text + " already added. Do you need to add this qty?", "Duplicate Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            foreach (FA_Inventory_Req_Itm _result in _resultList)
                                _result.Dispd_requsted_qty = _result.Dispd_requsted_qty + _mainItemQty;
                    }
                    else
                    {

                        //Add new item to existing list.
                        FA_Inventory_Req_Itm _inventoryRequestItem = new FA_Inventory_Req_Itm();

                        MasterItem _masterItem = new MasterItem();
                        _masterItem.Mi_cd = _mainItemCode;
                        _masterItem.Mi_longdesc = _itemdetail.Mi_longdesc;
                        _masterItem.Mi_model = _itemdetail.Mi_model;
                        _masterItem.Mi_brand = _itemdetail.Mi_brand;
                        _inventoryRequestItem.MasterItem = _masterItem;

                        _inventoryRequestItem.Dispd_item_code = _mainItemCode;
                        _inventoryRequestItem.Dispd_item_desc = _itemdetail.Mi_longdesc;
                        _inventoryRequestItem.Dispd_item_model = _itemdetail.Mi_model;
                        _inventoryRequestItem.Dispd_item_brand = _itemdetail.Mi_brand;

                        _inventoryRequestItem.Dispd_line_no = 0;
                        _inventoryRequestItem.Dispd_date =Convert.ToDateTime(txtRequestDate.Text);
                        _inventoryRequestItem.Dispd_uom = "N/A";
                        _inventoryRequestItem.Dispd_approved_item = _mainItemCode;
                        _inventoryRequestItem.Dispd_shop_qty = 0;
                        _inventoryRequestItem.Dispd_forward_sales =
                        _inventoryRequestItem.Dispd_buffer_limit = 0;
                        _inventoryRequestItem.Dispd_sr_request_qty = _mainItemQty;
                        _inventoryRequestItem.Dispd_aod_qty = 0;
                        _inventoryRequestItem.Dispd_res_req_no = "N/A";
                        _inventoryRequestItem.Dispd_res_line_no = 0;
                        _inventoryRequestItem.Dispd_res_item_code = _mainItemCode;
                        _inventoryRequestItem.Dispd_res_qty = 0;
                        _inventoryRequestItem.Dispd_res_balqty = _mainItemQty;
                        _inventoryRequestItem.Dispd_chk_status = false;
                        _inventoryRequestItem.Disp_pro_in_qty = 0;
                        _inventoryRequestItem.Disp_mrn_bal_qty = 0;
                        _inventoryRequestItem.Disp_cost_applicable = false;
                        _inventoryRequestItem.Disp_alternate_item = "N/A";
                        _inventoryRequestItem.Disp_pro_out_qty = 0;
                        _inventoryRequestItem.Disp_kit_item_code = "N/A";
                        _inventoryRequestItem.Dispd_can_qty = 0;
                        _inventoryRequestItem.Dispd_bal_qty = _mainItemQty;
                        _inventoryRequestItem.Disp_rtn_qty = 0;
                        _inventoryRequestItem.Disp_any_status = 0;
                        _inventoryRequestItem.Disp_so_res = 0;
                        _inventoryRequestItem.Disp_base_kline = 0;
                        _inventoryRequestItem.Disp_base_line = 0;
                        _inventoryRequestItem.Disp_line = 0;
                        _inventoryRequestItem.Dispf_line = 0;

                        _inventoryRequestItem.Dispd_item_status = _itemStatus;
                        _inventoryRequestItem.Dispd_res_no = _reservationNo;
                        _inventoryRequestItem.Dispd_remarks = _remarksText;
                        _inventoryRequestItem.Dispd_approved_qty = 0;

                        //Add Main item details.
                        //_inventoryRequestItem.Dispd_item_code = _mainItemCode;
                        //_inventoryRequestItem.Itri_mitm_stus = _itemStatus;
                        //_inventoryRequestItem.Itri_mqty = 0;

                        _invReqItemList.Add(_inventoryRequestItem);
                    }
                }

                //Clear add new data.
                ClearLayer2();
                ClearLayer3();

                //Bind the updated list to grid.
                gvItem.DataSource = new List<InventoryRequestItem>();
                gvItem.DataSource = _invReqItemList;

                if (MessageBox.Show("Do you need to add another item?", "Adding...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtItem.Focus();
                    return;
                }
                else
                {
                    btnSave.Select();
                    return;
                }

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
        private void LoadEditData(int rowIndex)
        {
            //Get the selected item from list.

            string _mainItem = Convert.ToString(gvItem.Rows[rowIndex].Cells["Itm_MainItem"].Value);
            string _item = Convert.ToString(gvItem.Rows[rowIndex].Cells["Itm_Item"].Value);
            string _status = Convert.ToString(gvItem.Rows[rowIndex].Cells["Itm_Status"].Value);

            Int32 _line = Convert.ToInt32(gvItem.Rows[rowIndex].Cells["Itm_No"].Value);
            List<FA_Inventory_Req_Itm> _invRequestItemList = _invReqItemList.Where(x => x.Dispd_item_code == _item && x.Dispd_item_status == _status).ToList();
            FA_Inventory_Req_Itm _inventoryRequestItem = _invRequestItemList[0];


            //Set selected data.
            txtItem.Text = _mainItem;

            txtStatus.Text = _status;

            txtReservation.Text = _inventoryRequestItem.Dispd_res_no;
            txtQty.Text = _inventoryRequestItem.Dispd_requsted_qty.ToString();
            txtItmRemark.Text = _inventoryRequestItem.Dispd_remarks;

            _invReqItemList.RemoveAll(x => x.Dispd_item_code == _mainItem && x.Dispd_item_status == _status);
            gvItem.DataSource = new List<InventoryRequestItem>();
            gvItem.DataSource = _invReqItemList;
            txtItem.Focus();

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

                                
                                   if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _mainItem);
                                   if (_itemdetail != null)
                                   if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                                   {
                                       if (_itemdetail.Mi_itm_tp == "K") // Nadeeka 07-08-2015
                                       {
                                           string _KITItem = Convert.ToString(gvItem.Rows[_rowIndex].Cells["Itm_Item"].Value);
                                           _invReqItemList.RemoveAll(x => x.Dispd_item_code == _KITItem && x.Dispd_item_status == _status);
                                           gvItem.DataSource = new List<InventoryRequestItem>();
                                       }
                                       else
                                       {
                                           _invReqItemList.RemoveAll(x => x.Dispd_item_code == _mainItem && x.Dispd_item_status == _status);
                                           gvItem.DataSource = new List<InventoryRequestItem>();
                                       }
                                   }

                                   //_invReqItemList.RemoveAll(x => x.Dispd_item_code == _mainItem && x.Dispd_item_status == _status);
                                   //gvItem.DataSource = new List<InventoryRequestItem>();
                                gvItem.DataSource = _invReqItemList;
                                return;
                            }
                        }

                        if (e.ColumnIndex == 1)
                        {
                            if (MessageBox.Show("Do you need to edit this item", "Edit...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                LoadEditData(_rowIndex);
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

        #region Save MRN No
        private MasterAutoNumber GenerateMasterAutoNumber()
        {
            string moduleText = "MRN";
            //ADDED BY SACHITH
            //2012/12/15
            MasterAutoNumber masterAuto;
            if (moduleText == "MRN")
            {
                if (ddlRequestSubType.SelectedValue.Equals("PRQ"))
                {
                    masterAuto = new MasterAutoNumber();
                    masterAuto.Aut_cate_tp = "LOC";
                    masterAuto.Aut_cate_cd = string.IsNullOrEmpty(_fix_loc) ? BaseCls.GlbUserDefProf : _fix_loc;
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "PRQ";
                    masterAuto.Aut_number = 0;
                    masterAuto.Aut_start_char = "PRQ";
                    masterAuto.Aut_year = null;
                }
                else
                {
                    masterAuto = new MasterAutoNumber();
                    masterAuto.Aut_cate_tp = "LOC";
                    masterAuto.Aut_cate_cd = string.IsNullOrEmpty(_fix_loc) ? BaseCls.GlbUserDefProf : _fix_loc;
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = moduleText;
                    masterAuto.Aut_number = 0;
                    masterAuto.Aut_start_char = moduleText;
                    masterAuto.Aut_year = null;
                }
            }
            else
            {
                masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_tp = "LOC";
                masterAuto.Aut_cate_cd = string.IsNullOrEmpty(_fix_loc) ? BaseCls.GlbUserDefProf : _fix_loc;
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = moduleText;
                masterAuto.Aut_number = 0;
                masterAuto.Aut_start_char = moduleText;
                masterAuto.Aut_year = null;
            }
            //END
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
                //UI validation.
                if (string.IsNullOrEmpty(ddlRequestSubType.SelectedValue.ToString()))
                { MessageBox.Show("Please select request type.", "Request Type", MessageBoxButtons.OK, MessageBoxIcon.Information); ddlRequestSubType.Focus(); return; }

                if (string.IsNullOrEmpty(txtDispatchRequried.Text))
                {
                    txtDispatchRequried.Text = "N/A";
                }//  Nadeeka 26-02-2015 (Requested by Chamal)
             //   { MessageBox.Show("Please enter dispatch required location.", "Dispatch Location", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDispatchRequried.Focus(); return; }

                if (txtDispatchRequried.Text.ToString().ToUpper() == _fix_loc.ToString().ToUpper())
                { MessageBox.Show("Please enter valid dispatch required location.", "Request Type", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDispatchRequried.Focus(); return; }

                //MasterUserLocation Or MasterUserProfitCenter.
                if (string.IsNullOrEmpty(txtRequestDate.Text))
                { MessageBox.Show("Please enter request date.", "Request Date", MessageBoxButtons.OK, MessageBoxIcon.Information); txtRequestDate.Focus(); return; }

                if (string.IsNullOrEmpty(txtRequriedDate.Text))
                { MessageBox.Show("Please enter required date.", "Required Date", MessageBoxButtons.OK, MessageBoxIcon.Information); txtRequriedDate.Focus(); return; }
                if (DateTime.Compare(Convert.ToDateTime(txtRequriedDate.Text.Trim()), Convert.ToDateTime(txtRequestDate.Text.Trim())) < 0)
                { MessageBox.Show("Required date can't be less than request date.", "Required Date", MessageBoxButtons.OK, MessageBoxIcon.Information); txtRequriedDate.Focus(); return; }

                int _count = 1;
                _invReqItemList.ForEach(x => x.Dispd_line_no = _count++);
                _invReqItemList.ForEach(X => X.Dispd_res_balqty = X.Dispd_requsted_qty);
                _invReqItemList.ForEach(X => X.Dispd_item_status = X.Dispd_item_status);
                _invReqItemList.Where(x => string.IsNullOrEmpty(x.Dispd_item_code)).ToList().ForEach(y => y.Dispd_item_code = y.Dispd_item_code);

                List<FA_Inventory_Req_Itm> _inventoryRequestItemList = _invReqItemList;
                if ((_inventoryRequestItemList == null) || (_inventoryRequestItemList.Count == 0))
                { MessageBox.Show("Please add items to List.", "No Item", MessageBoxButtons.OK, MessageBoxIcon.Information); txtItem.Focus(); return; }
                if (string.IsNullOrEmpty(txtNIC.Text))
                    txtNIC.Text = "-";
                else
                {
                    if (txtNIC.Text != "-")
                        if (IsValidNIC(txtNIC.Text.ToString()) == false)
                        { MessageBox.Show("Please enter valid NIC No.", "NIC", MessageBoxButtons.OK, MessageBoxIcon.Information); txtNIC.Focus(); return; }
                }

                string _masterLocation = (string.IsNullOrEmpty(_fix_loc)) ? BaseCls.GlbUserDefProf : _fix_loc;
                FA_Inventory_Req_Hdr _inventoryRequest = new FA_Inventory_Req_Hdr();
                //Fill the FA_Inventory_Req_Hdr header & footer data.

                _inventoryRequest.Disp_reqt_seq = "";
                _inventoryRequest.Disp_ref_no = "";
                _inventoryRequest.Disp_location = _masterLocation;
                _inventoryRequest.Disp_date = Convert.ToDateTime(txtRequestDate.Text);
                _inventoryRequest.Disp_authorized_by1 = "";
                _inventoryRequest.Disp_authorized_by2 = "";
                _inventoryRequest.Disp_authorized_by3 = "";
                _inventoryRequest.Disp_created_by = BaseCls.GlbUserID;
                // _inventoryRequest.Disp_approved_date =
                // _inventoryRequest.Disp_created_date =
                _inventoryRequest.Disp_comments = "";
                _inventoryRequest.Disp_status = "SAVE";
                _inventoryRequest.Disp_req_by = "";
                _inventoryRequest.Disp_company = BaseCls.GlbUserComCode;
                _inventoryRequest.Disp_type= Convert.ToString(ddlRequestSubType.SelectedValue);
                _inventoryRequest.Disp_prefer_location = txtDispatchRequried.Text;
                _inventoryRequest.Disp_request_type = "NOR";
                _inventoryRequest.Disp_last_modify_by = BaseCls.GlbUserID;
                //_inventoryRequest.Disp_last_modify_when =
                //_inventoryRequest.Disp_production_stage =
                //_inventoryRequest.Disp_production_job =
                //_inventoryRequest.Disp_sub_type =
                _inventoryRequest.Main_mrn_no = "N/A";
                _inventoryRequest.Pro_mrn_sub_type = "MAIN";
                //_inventoryRequest.Disp_request_date =
               _inventoryRequest.Disp_aodno ="N/A";
               _inventoryRequest.Disp_del_by ="N/A";
                _inventoryRequest.Dips_town ="N/A";
                _inventoryRequest.Disp_skip_app = false;
                _inventoryRequest.Disp_pda_status = 0;
                _inventoryRequest.Disp_collector_id = string.IsNullOrEmpty(txtNIC.Text) ? "N/A" : txtNIC.Text.Trim();
                _inventoryRequest.Disp_collector_name = string.IsNullOrEmpty(txtCollecterName.Text) ? "N/A" : txtCollecterName.Text.Trim();
                _inventoryRequest.Disp_cust_code = "";
                //_inventoryRequest.Disp_temp_loca =
                //_inventoryRequest.Disp_sbu =
                //_inventoryRequest.Disp_doc_ref_no =
                //_inventoryRequest.Disp_skd_req = 

                //_inventoryRequest.Itr_com = 
                //_inventoryRequest.Itr_req_no = GetRequestNo();
                //_inventoryRequest.Itr_tp = "MRN";
                //_inventoryRequest.Itr_sub_tp = Convert.ToString(ddlRequestSubType.SelectedValue);
                //_inventoryRequest.Itr_loc = 
                //_inventoryRequest.Itr_ref = string.Empty;
                //_inventoryRequest.Itr_dt = 
                //_inventoryRequest.Itr_exp_dt = Convert.ToDateTime(txtRequriedDate.Text);
                //_inventoryRequest.Itr_stus = "P";  //P - Pending , A - Approved. 
                //_inventoryRequest.Itr_job_no = string.Empty;  //Invoice No.
                //_inventoryRequest.Itr_bus_code = string.Empty;  //Customer Code.
                //_inventoryRequest.Itr_note = txtRemark.Text;
                //_inventoryRequest.Itr_issue_from = 
                //_inventoryRequest.Itr_rec_to = _masterLocation;
                //_inventoryRequest.Itr_direct = 0;
                //_inventoryRequest.Itr_country_cd = string.Empty;  //Country Code.
                //_inventoryRequest.Itr_town_cd = string.Empty;     //Town Code.
                //_inventoryRequest.Itr_cur_code = string.Empty;    //Currency Code.
                //_inventoryRequest.Itr_exg_rate = 0;              //Exchange rate.
                //_inventoryRequest.Itr_collector_id = 
                //_inventoryRequest.Itr_collector_name = 
                //_inventoryRequest.Itr_act = 1;
                //_inventoryRequest.Itr_cre_by = 
                //_inventoryRequest.Itr_session_id = BaseCls.GlbUserSessionID;
                //_inventoryRequest.Itr_issue_com = ddlDispCom.SelectedValue.ToString(); //Edit by Chamal 30/10/2013

           //     _inventoryRequest.InventoryRequestItemList = _inventoryRequestItemList;
                int rowsAffected = 0;
                string _docNo = string.Empty;

                if (string.IsNullOrEmpty(txtRequest.Text))
                {
                    rowsAffected = CHNLSVC.Inventory.SaveFARequest(_inventoryRequest,_inventoryRequestItemList,"MRN","MRN", out _docNo);
                    MessageBox.Show("Inventory Request Document Successfully saved. " + _docNo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    //BaseCls.GlbReportName = string.Empty;
                    //GlbReportName = string.Empty;
                    //_view.GlbReportName = string.Empty;
                    //_view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SMRNRepPrints.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_MRNRepPrints.rpt" : "MRNRepPrints.rpt";
                    //_view.GlbReportDoc = _docNo;
                    //_view.TopMost = true;
                    //_view.Show();
                    //_view = null;
                }
                else
                {
                   // rowsAffected = CHNLSVC.Inventory.SaveFARequest(_inventoryRequest,  out _docNo);
                    MessageBox.Show("Inventory Request Document Successfully Updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                InitVariables();
                LoadCachedObjects();
                InitializeForm();
                pnlBalance.Size = new Size(504, 177);
                ddlRequestSubType.SelectedIndex = 1;
                txtRequest.Focus();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                CHNLSVC.CloseChannel();
                return;
            }
        }
        protected void btnMRNSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckServerDateTime() == false)return;
                if (MessageBox.Show("Do you need to process ?", "Save...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    this.SaveInventoryRequestData();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Re-Call the Request No
        private void SetSelectedInventoryRequestData(FA_Inventory_Req_Hdr _selectedInventoryRequest)
        {
            if (_selectedInventoryRequest != null)
            {
                //Set Header details.
                BindRequestSubTypesDDLData(ddlRequestSubType);
                ddlRequestSubType.SelectedValue = _selectedInventoryRequest.Disp_type;
                txtDispatchRequried.Text = _selectedInventoryRequest.Disp_prefer_location;
                txtRequestDate.Text = _selectedInventoryRequest.Disp_date.ToString("dd/MMM/yyyy");
                txtRequriedDate.Text = _selectedInventoryRequest.Disp_request_date.ToString("dd/MMM/yyyy");
                //txtInvoiceNo.Text = _selectedInventoryRequest.Itr_job_no;
                txtNIC.Text = _selectedInventoryRequest.Disp_collector_id;
                txtCollecterName.Text = _selectedInventoryRequest.Disp_collector_name;
                txtRemark.Text = _selectedInventoryRequest.Disp_comments;

                //Set Item details.
                if (_selectedInventoryRequest.InventoryRequestItemList != null)
                {
                    gvItem.DataSource = _selectedInventoryRequest.InventoryRequestItemList;
                    _invReqItemList = _selectedInventoryRequest.InventoryRequestItemList;
                }
                else
                {
                    MessageBox.Show("There are no pending items", "Pending Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                //Set relevant buttons according to the MRN status.
                if (_selectedInventoryRequest.Disp_status == "APPROVED")
                {
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                }
                if (_selectedInventoryRequest.Disp_status == "CANCEL")
                {
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                }

                
                //btnSave.Enabled = (_selectedInventoryRequest.Itr_stus.ToUpper().Equals("P")) ? true : false;
                //btnCancel.Enabled = (_selectedInventoryRequest.Itr_stus.ToUpper().Equals("P")) ? true : false;
            }
        }
        private void txtRequest_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRequest.Text)) return;
            try
            {
                FA_Inventory_Req_Hdr _inputInvReq = new FA_Inventory_Req_Hdr();
                _inputInvReq.Disp_reqt_seq = txtRequest.Text.Trim();
                FA_Inventory_Req_Hdr _selectedInventoryRequest = CHNLSVC.Inventory.GetInventoryFARequestData(_inputInvReq);

                if (_selectedInventoryRequest != null)
                    if (!string.IsNullOrEmpty(_selectedInventoryRequest.Disp_company))
                    {
                        this.SetSelectedInventoryRequestData(_selectedInventoryRequest);
                        return;
                    }

                MessageBox.Show("Request no is invalid", "Request No", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Cancel MRN
        private void CancelSelectedRequest()
        {
            try
            {
                if (string.IsNullOrEmpty(txtRequest.Text))
                {
                    MessageBox.Show("Please select request before cancel.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (DateTime.Compare(Convert.ToDateTime(txtRequestDate.Text.Trim()), DateTime.Now.Date) != 0)
                {
                    MessageBox.Show("Request date should be current date in order to Cancel.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                FA_Inventory_Req_Hdr _inputInvReq = new FA_Inventory_Req_Hdr();
                _inputInvReq.Disp_company = BaseCls.GlbUserComCode;
                _inputInvReq.Disp_location = _fix_loc;
                _inputInvReq.Disp_reqt_seq = txtRequest.Text;
                _inputInvReq.Disp_status = "CANCEL";
                _inputInvReq.Disp_last_modify_by = BaseCls.GlbUserID;
              //  _inputInvReq.Itr_session_id = BaseCls.GlbUserSessionID;

                int result = 0;// CHNLSVC.Inventory.UpdateInventoryRequestStatus(_inputInvReq);

                if (result > 0)
                    MessageBox.Show("Inventory Request " + _inputInvReq.Disp_reqt_seq + " successfully Cancel.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Inventory Request " + _inputInvReq.Disp_reqt_seq + " can't be Cancel.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckServerDateTime() == false) return;
                if (MessageBox.Show("Do you need to cancel this MRN?", "Canceling...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CancelSelectedRequest();
                    btnClear_Click(null, null);
                    return;
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
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

        private bool IsValidLocation()
        {
            bool status = false;
            txtDispatchRequried.Text = txtDispatchRequried.Text.Trim().ToUpper().ToString();
            MasterLocation _masterLocation = CHNLSVC.General.GetLocationByLocCode(ddlDispCom.SelectedValue.ToString(), txtDispatchRequried.Text.ToString());
            status = (_masterLocation == null) ? false : true;
            return status;
        }
        private void txtDispatchRequried_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDispatchRequried.Text)) return;
            try
            {
                if (txtDispatchRequried.Text != "N/A")
                {
                    if (IsValidLocation() == false)
                    {
                        MessageBox.Show("Please check the dispatch location.", "Dispatching Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDispatchRequried.Clear();
                        txtDispatchRequried.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
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
        #endregion

        private void MaterialRequisition_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
        }

        private void ddlRequestSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlRequestSubType.Enabled = false;
            if (ddlRequestSubType.SelectedValue.ToString() == "NOR")
            {
                ddlDispCom.Enabled = false;
                ddlDispCom.SelectedValue = BaseCls.GlbUserComCode;
                txtDispatchRequried.Focus();
                txtDispatchRequried.Select();
            }
            else if (ddlRequestSubType.SelectedValue.ToString() == "PRQ")
            {
                ddlDispCom.Enabled = true;
                ddlDispCom.Text = "";
                ddlDispCom.Focus();
            }
        }

        private void ddlDispCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRequestSubType.SelectedValue.ToString() == "PRQ")
            {
                if (ddlDispCom.SelectedValue.ToString() == BaseCls.GlbUserComCode)
                {
                    ddlDispCom.Text = "";
                }
                txtDispatchRequried.Clear();
                txtDispatchRequried.Focus(); 
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

        private void txtItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void gvItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_srch_stus_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Status_FA);
                DataTable _result = CHNLSVC.CommonSearch.getSearchFAItemStatusData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtStatus;
                _CommonSearch.ShowDialog();
                txtStatus.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void FAMaterialRequisition_Load(object sender, EventArgs e)
        {
            panel3.Enabled = true;
            MasterLocation _mstLoc = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            if (string.IsNullOrEmpty(_mstLoc.ML_FX_LOC))
            {
                MessageBox.Show("Fixed Asset location is not setup. Please contact inventory department.", "MRN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panel3.Enabled = false;
                return;
            }
            _fix_loc = _mstLoc.ML_FX_LOC;
        }

    }
}
