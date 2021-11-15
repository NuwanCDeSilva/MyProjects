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

    public partial class MaterialRequisition : FF.WindowsERPClient.Base
    {
        #region Variables
        private MasterItem _itemdetail = null;
        private DataTable _CompanyItemStatus = null;
        List<InventoryRequestItem> _invReqItemList = null;

        bool _isDecimalAllow = false;
        private Boolean _isResvNo = false;
        private Int32 _autoAppr = -1;
        private Int32 _mainAutoAppr = 0;
        private Int32 _autoApproval = 0;
        private Boolean _isFoundSys_Param;
        private string _mainItemCode;
        private Decimal avl_qty, bufer_lvl, recept_count, newbuffer_qty, _deff_qty = 0, Ins_value = 0, currentCost_Loc = 0, TotalGIT=0; //add by tharanga
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

                //kapila 14/8/2017
                _isFoundSys_Param = CHNLSVC.Inventory.Is_Found_Mst_Sys_Para(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "MRNAUTOAPP");
                if (_isFoundSys_Param == true)
                {
                    _autoAppr = 1;
                    _mainAutoAppr = 1;
                    _autoApproval = 1;
                }
                else
                {
                    _isFoundSys_Param = CHNLSVC.Inventory.Is_Found_Mst_Sys_Para(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "QTYINHAND");
                    if (_isFoundSys_Param == false)
                    {
                        _autoAppr = 0;
                        _mainAutoAppr = 0;
                    }
                    else
                    {
                        _autoAppr = 1;
                        _mainAutoAppr = 1;
                    }
                }
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
            List<MasterSubType> _lst = CHNLSVC.General.GetAllSubTypes("MRN");
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
            _invReqItemList = new List<InventoryRequestItem>();
            _autoAppr = _mainAutoAppr;

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
            gvItem.Columns["Itm_Qty"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight;
            BindReceiveCompany();
            ddlRequestSubType.Enabled = true;
            ddlDispCom.Enabled = true;
            load_value();
        }
        public MaterialRequisition()
        {
            InitializeComponent();
            try
            {
                InitVariables();
                LoadCachedObjects();
                InitializeForm();
                pnlBalance.Size = new Size(504, 177);
                pnlError.Size = new Size(504, 177);
                pnlRootBal.Size = new Size(504, 177);
                pnlError.Location = new Point(454, 189);
                pnlRootBal.Location = new Point(454, 189);
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
                case CommonUIDefiniton.SearchUserControlType.PreferLoc:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + _mainItemCode + seperator);
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

                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(ddlDispCom.SelectedValue.ToString() + seperator);
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
                txtDispatchLoc.Focus();

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
            BindUserCompanyItemStatusDDLData(cmbStatus);
            txtReservation.Clear();
            txtQty.Text = FormatToCurrency("0");
            lblAvalQty.Text = FormatToCurrency("0");
            lblFreeQty.Text = FormatToCurrency("0");
            lblbuferqty.Text = FormatToCurrency("0");
            txtItmRemark.Clear();
            //_autoAppr = _mainAutoAppr;
        }
        private void ClearLayer3()
        {
            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;
            lblItemSubStatus.Text = "Serial Status : " + string.Empty;
            txtDispatchLoc.Text = "";
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
                    pnlRootScheduls.Visible = false;
                    btnAddItem.Enabled = true;
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

                //kapila comented on 12/10/2016 req by srimathi
                //if (_itemdetail.Mi_is_discont == 1)
                //{// NADEEKA 23-02-2015
                //    MessageBox.Show("This item " + txtItem.Text.Trim() + " is discontinue", "Discontinue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtItem.Clear();
                //    txtItem.Focus();
                //    return;
                //}
                // add  by tharanga 2017/10/26
                 MasterLocation _loc = CHNLSVC.General.GetAllLocationByLocCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca,1);
                 int month = System.DateTime.Now.Month;
                 int date = int.Parse(DateTime.Today.ToString("dd"));

                List<InventoryLocation> _inventoryLocation = null;
                _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString());

                if (_inventoryLocation != null)
                
                if (_inventoryLocation.Count > 0)
                {
                    lblbuferqty.Text = Convert.ToString(_inventoryLocation.Select(x => x.Inl_bl_qty).Sum());
                }
                else 
                { 
                    lblbuferqty.Text = "0";
                }
                
                if (Convert.ToDecimal(lblbuferqty.Text) <=0)
                {
                 decimal bufer_qty = CHNLSVC.Inventory.get_buffer_qty(txtItem.Text.Trim(), BaseCls.GlbDefChannel, _loc.Ml_buffer_grd, date, month);
                 lblbuferqty.Text = Convert.ToString(bufer_qty);
                }
                
            
                if (CHNLSVC.Inventory.checkReqItemAllow(txtItem.Text.Trim()) == 1)
                {// NADEEKA 23-02-2015
                    if (CHNLSVC.Inventory.checkReqItemAllowCompany(txtItem.Text.Trim(), BaseCls.GlbUserComCode) == 0)
                    {
                        MessageBox.Show("This item  " + txtItem.Text.Trim() + " is not allowed for " + BaseCls.GlbUserComCode + " Company", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItem.Clear();
                        txtItem.Focus();
                        return;
                    }
                }

                //kapila 1/6/2017
                txtDispatchLoc.Text = "";
                DataTable _dtPreLoc = CHNLSVC.Inventory.Get_MRN_Prefer_Loc(BaseCls.GlbUserComCode, txtItem.Text);
                var _manname = _dtPreLoc.AsEnumerable().Where(X => X.Field<Int16>("Is_Default") == 1).ToList();
                if (_manname != null && _manname.Count > 0)
                {
                    txtDispatchLoc.Text = _manname[0].Field<string>("Location");
                }

                //get root balances
                if (chkRootBal.Checked == true)
                {
                    DataTable _dtRootBal = CHNLSVC.Inventory.GetRootBalances(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text);
                    if (_dtRootBal.Rows.Count > 0)
                    {
                        pnlRootBal.Visible = true;
                        grvRootBal.AutoGenerateColumns = false;
                        grvRootBal.DataSource = _dtRootBal;
                        grvRootBal.Visible = true;

                    }
                }

                List<MasterItemComponent> _itemComponentList = CHNLSVC.Inventory.GetItemComponents(txtItem.Text.Trim());
                MasterItem _itemcomponent = null;
                //CHNLSVC.Inventory.GetItemComponentTable 
                // NADEEKA 24-02-2015
                if ((_itemComponentList != null) && (_itemComponentList.Count > 0))
                {
                    foreach (MasterItemComponent _itemCompo in _itemComponentList)
                    {
                        _itemcomponent = new MasterItem();
                        _itemcomponent = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itemCompo.ComponentItem.Mi_cd);
                        if (_itemcomponent != null)
                        {
                            //kapila comented on 12/10/2016 req by srimathi
                            //reuncomented by kapila 15/7/2017
                            if (_itemcomponent.Mi_is_discont == 1)
                            {
                                if (MessageBox.Show("Item code " + _itemCompo.ComponentItem.Mi_cd + " is discontinue. Do you want to continue? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    break;
                                else
                                {
                                    txtItem.Clear();
                                    txtItem.Focus();
                                    break;
                                }
                            }
                            if (CHNLSVC.Inventory.checkReqItemAllow(_itemCompo.ComponentItem.Mi_cd) == 1)
                            {// NADEEKA 23-02-2015
                                if (CHNLSVC.Inventory.checkReqItemAllowCompany(_itemCompo.ComponentItem.Mi_cd, BaseCls.GlbUserComCode) == 0)
                                {
                                    MessageBox.Show("Item code " + _itemCompo.ComponentItem.Mi_cd + " is not allowed for " + BaseCls.GlbUserComCode + " Company. ", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtItem.Clear();
                                    txtItem.Focus();
                                    return;
                                }
                            }

                        }
                    }
                }
                else      //kapila 15/7/2017
                {
                    MasterItem _masItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                    if (_masItem != null)
                    {
                        if (_masItem.Mi_is_discont == 1)
                        {
                            if (MessageBox.Show("Item code " + txtItem.Text + " is discontinue. Do you want to continue ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                txtItem.Clear();
                                txtItem.Focus();
                                return;
                            }
                        }
                    }
                }




                if (string.IsNullOrEmpty(cmbStatus.Text))
                    DisplayAvailableQty(txtItem.Text.Trim(), lblAvalQty, lblFreeQty, string.Empty);
                else
                    DisplayAvailableQty(txtItem.Text.Trim(), lblAvalQty, lblFreeQty, cmbStatus.SelectedValue.ToString());
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
            if (string.IsNullOrEmpty(cmbStatus.Text.Trim())) return;
            try
            {
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
                //kapila
                if (pnlError.Visible == true)
                {
                    pnlRootBal.Visible = false;
                    MessageBox.Show("Cannot add.Please check the following criteria", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    MessageBox.Show("Please enter item code.", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDispatchLoc.Text))
                {
                    MessageBox.Show("Please enter location code.", "Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDispatchLoc.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbStatus.Text))
                {
                    MessageBox.Show("Please select a item status.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbStatus.Focus();
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
                //kapila
                //if (string.IsNullOrEmpty(txtDispatchLoc.Text))
                //{
                //    MessageBox.Show("Please enter dispatch location.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtDispatchLoc.Focus();
                //    return;
                //}
                #region new validation
                string outmsg = string.Empty;
                bool _isSubItemHavenew = (string.IsNullOrEmpty(lblItemSubStatus.Text)) ? false : lblItemSubStatus.Text.Trim() == "Sub Item Status : Available" ? true : false;
                bool insnew;
                bool Check_MRN_Item_exceed = false;
                Boolean sts = CHNLSVC.Inventory.MRN_VALIDATION(txtReservation.Text, txtItem.Text, cmbStatus.SelectedValue.ToString(), BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbDefChannel,
                 Convert.ToDateTime(txtRequestDate.Text), Convert.ToDecimal(txtQty.Text), txtDispatchLoc.Text, out outmsg, out _autoAppr, out _mainAutoAppr, out insnew, _isSubItemHavenew, out Check_MRN_Item_exceed, currentCost_Loc,Ins_value , TotalGIT, out _isResvNo);

                if (!string.IsNullOrEmpty(outmsg))
                {
                    MessageBox.Show(outmsg, "MRN Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (Check_MRN_Item_exceed == true)
                {
                    if (insnew == true)
                    {
                        _mainAutoAppr = 1;
                        //_autoApproval = 1;
                        //_autoAppr = 1;
                    }
                    else
                    { //_autoApproval = 0; _autoAppr = 0; _mainAutoAppr=0;
                        if (MessageBox.Show("Location Insurance value exceeding. Are you sure want to make a request without auto approval? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //_autoApproval = 0; 
                            //_autoAppr = 0; _
                            _mainAutoAppr = 0;
                            //goto L1;
                        }
                        else
                        {
                            _mainAutoAppr = 0;
                            return;
                        }
                        //_autoApproval = 0; 
                        //_autoAppr = 0; 


                    }
                }

                #endregion

                // old validation  cokmmnt by tharanga 2018/07/30 all validation add to ther service side

                #region old validation
                //#region all validation

                //if (!string.IsNullOrEmpty(txtReservation.Text))
                //{ // Nadeeka 28-05-2015
                //    DataTable _dt = CHNLSVC.Inventory.GetReservationDet(BaseCls.GlbUserComCode, txtReservation.Text, txtItem.Text, cmbStatus.SelectedValue.ToString());
                //    if (_dt.Rows.Count == 0)
                //    {
                //        MessageBox.Show("Invalid Reservation #.", "Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        txtReservation.Text = "";
                //        txtReservation.Focus();
                //        return;

                //    }
                //    else
                //        _isResvNo = true;
                //}
                ////kapila 8/8/2017
                //if (!string.IsNullOrEmpty(txtReservation.Text))
                //{
                //    INR_RES_DET _inrRes = CHNLSVC.Inventory.GET_INR_RES_DET_DATA_NEW(new INR_RES_DET()
                //    {
                //        IRD_RES_NO = txtReservation.Text.Trim(),
                //        IRD_ITM_CD = txtItem.Text.Trim(),
                //        IRD_ITM_STUS = cmbStatus.SelectedValue.ToString()
                //    }).FirstOrDefault();
                //    if (_inrRes == null)
                //    {
                //        MessageBox.Show("Reservation balance not available !", "Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        txtReservation.Text = "";
                //        return;
                //    }
                //    if (Convert.ToDecimal(txtQty.Text) > _inrRes.IRD_MRN_AVA_BAL)
                //    {

                //        MessageBox.Show("Reservation balance not available !", "Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        txtReservation.Text = "";
                //        return;
                //    }
                //}
                ////kapila 13/8/2015 check the GIT is exceeded
                //MasterItem _itemMas = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);

                
                //DataTable _dtMax = CHNLSVC.General.GetStockRequest("GIT", BaseCls.GlbUserDefLoca, BaseCls.GlbDefChannel, BaseCls.GlbUserComCode, DateTime.Now.Date, txtItem.Text, _itemMas.Mi_brand, _itemMas.Mi_cate_1, _itemMas.Mi_cate_2, _itemMas.Mi_cate_3, _itemMas.Mi_cate_4, _itemMas.Mi_cate_5);
                //if (_dtMax.Rows.Count > 0)
                //{
                //    DataTable _dtGit = CHNLSVC.General.GetItemGIT(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _dtMax.Rows[0]["mrq_itm_cd"].ToString(), _dtMax.Rows[0]["mrq_brd"].ToString(), _dtMax.Rows[0]["mrq_cat1"].ToString(), _dtMax.Rows[0]["mrq_cat2"].ToString(), _dtMax.Rows[0]["mrq_cat3"].ToString(), _dtMax.Rows[0]["mrq_cat4"].ToString(), _dtMax.Rows[0]["mrq_cat5"].ToString(), Convert.ToDecimal(_dtMax.Rows[0]["mrq_days"]));//, Convert.ToDecimal(_dtMax.Rows[0]["mrq_wsdays"]), Convert.ToDecimal(_dtMax.Rows[0]["mrq_ssdays"]));
                //    if (_dtGit.Rows.Count > 0)
                //    {
                //        if (_autoAppr == 0)
                //        {
                //            if (Convert.ToDecimal(_dtMax.Rows[0]["mrq_qty"]) > 0)
                //            {
                //                if (Convert.ToDecimal(_dtGit.Rows[0]["iti_qty"]) + Convert.ToDecimal(txtQty.Text) > Convert.ToDecimal(_dtMax.Rows[0]["mrq_qty"]))
                //                {
                //                    MessageBox.Show("GIT available.You are exceeding allowable quantity ", "Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                    txtItem.Focus();
                //                    //txtReservation.Text = "";
                //                    //txtReservation.Focus();
                //                    return;
                //                }
                //            }
                //            else
                //            {
                //                MessageBox.Show("GIT available.You are exceeding allowable days ", "Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                txtReservation.Text = "";
                //                txtReservation.Focus();
                //                return;

                //            }
                //        }
                //    }
                //    else
                //    {
                //        //Uncommented by akila 2017/01/20
                //        if ((Convert.ToDecimal(_dtMax.Rows[0]["mrq_qty"]) <= 0) && (Convert.ToDecimal(_dtMax.Rows[0]["mrq_days"]) <= 0))
                //        {
                //            MessageBox.Show("Permission to requesting this item has been blocked by inventory department", "Request", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //            txtReservation.Text = "";
                //            txtReservation.Focus();
                //            return;
                //        }
                //        //if ((Convert.ToDecimal(_dtMax.Rows[0]["mrq_qty"]) <= 0) && (Convert.ToDecimal(_dtMax.Rows[0]["mrq_wsdays"]) <= 0))
                //        //{
                //        //    MessageBox.Show("Permission to requesting this item has been blocked by inventory department", "Request", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //        //    txtReservation.Text = "";
                //        //    txtReservation.Focus();
                //        //    return;
                //        //}
                //        //if ((Convert.ToDecimal(_dtMax.Rows[0]["mrq_qty"]) <= 0) && (Convert.ToDecimal(_dtMax.Rows[0]["mrq_ssdays"]) <= 0))
                //        //{
                //        //    MessageBox.Show("Permission to requesting this item has been blocked by inventory department", "Request", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //        //    txtReservation.Text = "";
                //        //    txtReservation.Focus();
                //        //    return;
                //        //}
                //    }
                //    //Added by dilshan on 23/11/2017----------------- 
                //    //DataTable _dtGitWH = CHNLSVC.General.GetItemGITWH(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _dtMax.Rows[0]["mrq_itm_cd"].ToString(), _dtMax.Rows[0]["mrq_brd"].ToString(), _dtMax.Rows[0]["mrq_cat1"].ToString(), _dtMax.Rows[0]["mrq_cat2"].ToString(), _dtMax.Rows[0]["mrq_cat3"].ToString(), _dtMax.Rows[0]["mrq_cat4"].ToString(), _dtMax.Rows[0]["mrq_cat5"].ToString(), Convert.ToDecimal(_dtMax.Rows[0]["mrq_wsdays"]));//, Convert.ToDecimal(_dtMax.Rows[0]["mrq_wsdays"]), Convert.ToDecimal(_dtMax.Rows[0]["mrq_ssdays"]));
                //    DataTable _dtGitWH = CHNLSVC.General.GetItemGITWH(BaseCls.GlbUserComCode, txtDispatchRequried.Text, _dtMax.Rows[0]["mrq_itm_cd"].ToString(), _dtMax.Rows[0]["mrq_brd"].ToString(), _dtMax.Rows[0]["mrq_cat1"].ToString(), _dtMax.Rows[0]["mrq_cat2"].ToString(), _dtMax.Rows[0]["mrq_cat3"].ToString(), _dtMax.Rows[0]["mrq_cat4"].ToString(), _dtMax.Rows[0]["mrq_cat5"].ToString(), Convert.ToDecimal(_dtMax.Rows[0]["mrq_wsdays"]), BaseCls.GlbUserDefLoca);//, Convert.ToDecimal(_dtMax.Rows[0]["mrq_wsdays"]), Convert.ToDecimal(_dtMax.Rows[0]["mrq_ssdays"]));
                //    if (_dtGitWH.Rows.Count > 0)
                //    {
                //        //if (_autoAppr == 0)
                //        //{
                //            if (Convert.ToDecimal(_dtMax.Rows[0]["mrq_qty"]) > 0)
                //            {
                //                if (Convert.ToDecimal(_dtGitWH.Rows[0]["iti_qty"]) + Convert.ToDecimal(txtQty.Text) > Convert.ToDecimal(_dtMax.Rows[0]["mrq_qty"]))
                //                {
                //                    MessageBox.Show("GIT available.You are exceeding allowable quantity ", "Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                    txtItem.Focus();
                //                    //txtReservation.Text = "";
                //                    //txtReservation.Focus();
                //                    return;
                //                }
                //            }
                //            else
                //            {
                //                MessageBox.Show("GIT available.You are exceeding allowable days ", "Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                txtReservation.Text = "";
                //                txtReservation.Focus();
                //                return;

                //            }
                //        //}
                //    }
                //    else
                //    {
                //        //if ((Convert.ToDecimal(_dtMax.Rows[0]["mrq_qty"]) <= 0) && (Convert.ToDecimal(_dtMax.Rows[0]["mrq_days"]) <= 0))
                //        //{
                //        //    MessageBox.Show("Permission to requesting this item has been blocked by inventory department", "Request", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //        //    txtReservation.Text = "";
                //        //    txtReservation.Focus();
                //        //    return;
                //        //}
                //        if ((Convert.ToDecimal(_dtMax.Rows[0]["mrq_qty"]) <= 0) && (Convert.ToDecimal(_dtMax.Rows[0]["mrq_wsdays"]) <= 0))
                //        {
                //            MessageBox.Show("Permission to requesting this item has been blocked by inventory department", "Request", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //            txtReservation.Text = "";
                //            txtReservation.Focus();
                //            return;
                //        }
                //        //if ((Convert.ToDecimal(_dtMax.Rows[0]["mrq_qty"]) <= 0) && (Convert.ToDecimal(_dtMax.Rows[0]["mrq_ssdays"]) <= 0))
                //        //{
                //        //    MessageBox.Show("Permission to requesting this item has been blocked by inventory department", "Request", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //        //    txtReservation.Text = "";
                //        //    txtReservation.Focus();
                //        //    return;
                //        //}
                //    }
                //    //-------------------------------------

                //}
                ////Get existing items details from the grid.

                _mainItemCode = txtItem.Text.Trim().ToUpper();
                string _itemStatus = cmbStatus.SelectedValue.ToString();
                string _reservationNo = string.IsNullOrEmpty(txtReservation.Text.Trim()) ? string.Empty : txtReservation.Text.Trim();
                decimal _mainItemQty = (string.IsNullOrEmpty(txtQty.Text)) ? 0 : Convert.ToDecimal(txtQty.Text.Trim());
                string _remarksText = string.IsNullOrEmpty(txtItmRemark.Text.Trim()) ? string.Empty : txtItmRemark.Text.Trim();
                bool _isSubItemHave = (string.IsNullOrEmpty(lblItemSubStatus.Text)) ? false : lblItemSubStatus.Text.Trim() == "Sub Item Status : Available" ? true : false;

                ////kapila 21/6/2017
                //if (_isFoundSys_Param == true)
                //{
                //    DataTable _result = null;
                //Outer1:
                //    if (_isSubItemHave)
                //    {
                //        //Get the relevant sub items.
                //        List<MasterItemComponent> _itemComponentList = CHNLSVC.Inventory.GetItemComponents(_mainItemCode);

                //        if (_itemComponentList == null)
                //        {
                //            _isSubItemHave = false;
                //            goto Outer1;
                //        }

                //        if ((_itemComponentList != null) && (_itemComponentList.Count > 0))
                //        {
                //            //Update qty for existing items.
                //            foreach (MasterItemComponent _itemCompo in _itemComponentList)
                //            {
                //                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                //                _CommonSearch.ReturnIndex = 0;
                //                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PreferLoc);
                //                _result = CHNLSVC.CommonSearch.GetPreferLocSearchData(_CommonSearch.SearchParams, null, null);
                //                var _manname = _result.AsEnumerable().Where(X => X.Field<String>("location") == txtDispatchLoc.Text).ToList();
                //                if (_manname == null || _manname.Count == 0)
                //                {
                //                    if (_autoAppr == 0)
                //                    {
                //                        MessageBox.Show("Please select valid dispatch location.", "Dispatching Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                        txtDispatchLoc.Clear();
                //                        txtDispatchLoc.Focus();
                //                        return;
                //                    }
                //                }
                //            }
                //        }
                //    }
                //    else
                //    {
                //        _mainItemCode = txtItem.Text;
                //        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                //        _CommonSearch.ReturnIndex = 0;
                //        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PreferLoc);
                //        _result = CHNLSVC.CommonSearch.GetPreferLocSearchData(_CommonSearch.SearchParams, null, null);
                //        var _manname = _result.AsEnumerable().Where(X => X.Field<String>("location") == txtDispatchLoc.Text).ToList();
                //        if (_manname == null || _manname.Count == 0)
                //        {
                //            if (_autoAppr == 0)
                //            {
                //                MessageBox.Show("Please select valid dispatch location.", "Dispatching Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                txtDispatchLoc.Clear();
                //                txtDispatchLoc.Focus();
                //                return;
                //            }
                //        }
                //    }
                //}

                List<InventoryRequestItem> _temp = _invReqItemList;
                ////This is a temporary collation for newly added items.
                List<InventoryRequestItem> _resultList = null;
                DataTable _dtSysPara = new DataTable();
                //#region MRN Insurance auto app
                //bool Check_MRN_Item_exceed_Ins = false;
                //Check_MRN_Item_exceed_Ins = CHNLSVC.Inventory.Is_Found_Mst_Sys_Para(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "CHKLOCINSU");
                //if (Check_MRN_Item_exceed_Ins == true)
                //{


                List<InventoryRequestItem> _tempnew = new List<InventoryRequestItem>();
                InventoryRequestItem _InventoryRequestItem = new InventoryRequestItem();
                _InventoryRequestItem.Itri_itm_cd = txtItem.Text.ToString();
                _InventoryRequestItem.Itri_qty = Convert.ToInt32(txtQty.Text);
                _tempnew.Add(_InventoryRequestItem);
                //bool ins = CHNLSVC.Inventory.Check_MRN_Item_exceed_Ins(_tempnew, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, DateTime.Now);
                //if (ins == true)
                //{
                //    _mainAutoAppr = 1;
                //    //_autoApproval = 1;
                //    //_autoAppr = 1;
                //}
                //else
                //{ //_autoApproval = 0; _autoAppr = 0; _mainAutoAppr=0;
                //    if (MessageBox.Show("Location Insurance value exceeding. Are you sure want to make a request without auto approval? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //    {
                //        //_autoApproval = 0; 
                //        //_autoAppr = 0; _
                //        _mainAutoAppr = 0;
                //        //goto L1;
                //    }
                //    else
                //    {
                //        _mainAutoAppr = 0;
                //        return;
                //    }
                //    //_autoApproval = 0; 
                //    //_autoAppr = 0; 


                //}


                //}
                //#endregion

                


                //#endregion


                #endregion old validation


            //#region validate stock and advance recipt add by tharanga 2018/03/14
                //if (_autoApproval == 1)     //14/8/2017
                //{
                //   //private  Decimal avl_qty, bufer_lvl, recept_count, newbuffer_qty, _deff_qty = 0;
                //    #region avaliable stock
                //    List<InventoryLocation> _inventoryLocation = null;
                //    //if (string.IsNullOrEmpty(_status))
                //    //    _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item.Trim(), string.Empty);
                //    //else
                //    _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.ToString().Trim(), _itemStatus);
                //    //DataTable dt = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.ToString(), _itemStatus);
                    
                //    if (_inventoryLocation != null)
                //        if (_inventoryLocation.Count > 0)
                //        {
                //            var _aQty = _inventoryLocation.Select(x => x.Inl_qty).Sum();
                //            var _aFree = _inventoryLocation.Select(x => x.Inl_free_qty).Sum();
                //            avl_qty = _aQty;
                //        }
                //    #endregion

                //    #region bufer_lvl
                //    MasterLocation _loc = CHNLSVC.General.GetAllLocationByLocCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, 1);
                //    int month = System.DateTime.Now.Month;
                //    int date = int.Parse(DateTime.Today.ToString("dd"));

                //    bufer_lvl = CHNLSVC.Inventory.get_buffer_qty(txtItem.Text.ToString(), BaseCls.GlbDefChannel, _loc.Ml_buffer_grd, date, month);
                    
                //    #endregion
                //    #region advance recept count
                //     recept_count = CHNLSVC.Financial.get_advance_count_itm_wise(txtItem.Text.ToString().Trim(), BaseCls.GlbUserComCode, _loc.Ml_def_pc);
                //    #endregion
                //     Decimal allow_mrnqty = bufer_lvl - recept_count;
                //     newbuffer_qty = bufer_lvl + recept_count;
                //     _deff_qty = newbuffer_qty - avl_qty;
                //     if (_mainItemQty < _deff_qty)
                //     {
                //         _mainAutoAppr = 1;
                //     }
                //     else
                //     { _mainAutoAppr = 0; }

                //}
                //#endregion
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

                            //kapila 2/6/2017
                            _autoAppr = _mainAutoAppr;
                            if (_autoApproval == 0)     //14/8/2017
                            {
                                _dtSysPara = CHNLSVC.Sales.SP_CHECK_MST_SYS_PARA_MRN(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserDefLoca, _itemCompo.ComponentItem.Mi_cd, Convert.ToDecimal(txtQty.Text));
                                DataRow[] result = _dtSysPara.Select("para_stus = 1");
                                if (result.Length > 0)
                                {
                                    pnlRootBal.Visible = false;
                                    grvError.AutoGenerateColumns = false;
                                    grvError.DataSource = _dtSysPara;
                                    pnlError.Visible = true;
                                    if (MessageBox.Show("As one of the condition is failed item (" + _itemCompo.ComponentItem.Mi_cd + ") cannot be auto approved. Are you sure you want to make a request without auto approval?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        _autoAppr = 0;
                                        goto L1;
                                    }
                                    else
                                        _autoAppr = 0;
                                        return;
                                }
                            }
                        L1:
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
                                _inventoryRequestItem.Itri_app_qty = 0;

                                //kapila 1/6/2017
                                _inventoryRequestItem.Itri_com = BaseCls.GlbUserComCode;
                                _inventoryRequestItem.Itri_loc = txtDispatchLoc.Text;
                                //Add Main item details.
                                _inventoryRequestItem.Itri_mitm_cd = _mainItemCode;
                                _inventoryRequestItem.Itri_mitm_stus = _itemStatus;
                                _inventoryRequestItem.Itri_mqty = _mainItemQty;

                                _inventoryRequestItem.Itri_is_auto_app = _autoAppr;

                                _invReqItemList.Add(_inventoryRequestItem);
                            }
                        }
                    }
                }
                else
                {

                    if (_invReqItemList != null)
                        if (_invReqItemList.Count > 0)
                            _resultList = _temp.Where(x => x.MasterItem.Mi_cd.Equals(txtItem.Text.Trim()) && x.Itri_itm_stus == _itemStatus).ToList();

                    //kapila 2/6/2017
                    _autoAppr = _mainAutoAppr;
                    if (_autoApproval == 0)     //14/8/2017
                    {
                        _dtSysPara = CHNLSVC.Sales.SP_CHECK_MST_SYS_PARA_MRN(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserDefLoca, _mainItemCode, Convert.ToDecimal(txtQty.Text));
                        DataRow[] result = _dtSysPara.Select("para_stus = 1");
                        if (result.Length > 0)
                        {
                            pnlRootBal.Visible = false;
                            grvError.AutoGenerateColumns = false;
                            grvError.DataSource = _dtSysPara;
                            pnlError.Visible = true;
                            if (MessageBox.Show("As one of the condition is failed item (" + _mainItemCode + ") cannot be auto approved. Are you sure you want to make a request without auto approval?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                _autoAppr = 0;
                                goto L2;
                            }
                            else
                                _autoAppr = 0;
                                return;
                        }
                    }

                L2:
                    // If selected sub item exists in the grid,update the qty.
                    if ((_resultList != null) && (_resultList.Count > 0))
                    {
                        //kapila comented on 5/7/2017
                        //if (MessageBox.Show(txtItem.Text + " already added. Do you need to add this qty?", "Duplicate Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //    foreach (InventoryRequestItem _result in _resultList)
                        //        _result.Itri_qty = _result.Itri_qty + _mainItemQty;
                        MessageBox.Show("Already added this item.", "Request", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtItem.Text = "";
                        txtItem.Focus();
                        return;
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

                        _inventoryRequestItem.Itri_itm_stus = _itemStatus;
                        _inventoryRequestItem.Itri_res_no = _reservationNo;
                        _inventoryRequestItem.Itri_note = _remarksText;
                        _inventoryRequestItem.Itri_qty = _mainItemQty;
                        _inventoryRequestItem.Itri_app_qty = 0;

                        //kapila 1/6/2017
                        _inventoryRequestItem.Itri_com = BaseCls.GlbUserComCode;
                        _inventoryRequestItem.Itri_loc = txtDispatchLoc.Text;

                        //Add Main item details.
                        _inventoryRequestItem.Itri_mitm_cd = _mainItemCode;
                        _inventoryRequestItem.Itri_mitm_stus = _itemStatus;
                        _inventoryRequestItem.Itri_mqty = 0;

                        _inventoryRequestItem.Itri_is_auto_app = _autoAppr;

                        _invReqItemList.Add(_inventoryRequestItem);
                    }
                }

                //Clear add new data.
                ClearLayer2();
                ClearLayer3();
                pnlRootBal.Visible = false;

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

                string outmsg=string.Empty;
                Int32 _autoapr=0;
                Int32 _mainautoapr=0;
             
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
            List<InventoryRequestItem> _invRequestItemList = _invReqItemList.Where(x => x.Itri_itm_cd == _item && x.Itri_itm_stus == _status).ToList();
            InventoryRequestItem _inventoryRequestItem = _invRequestItemList[0];


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
                                            _invReqItemList.RemoveAll(x => x.Itri_itm_cd == _KITItem && x.Itri_itm_stus == _status);
                                            gvItem.DataSource = new List<InventoryRequestItem>();
                                        }
                                        else
                                        {
                                            _invReqItemList.RemoveAll(x => x.Itri_mitm_cd == _mainItem && x.Itri_itm_stus == _status);
                                            gvItem.DataSource = new List<InventoryRequestItem>();
                                        }
                                    }

                                //_invReqItemList.RemoveAll(x => x.Itri_mitm_cd == _mainItem && x.Itri_itm_stus == _status);
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
                    masterAuto.Aut_cate_cd = string.IsNullOrEmpty(BaseCls.GlbUserDefLoca) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
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
                    masterAuto.Aut_cate_cd = string.IsNullOrEmpty(BaseCls.GlbUserDefLoca) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
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
                masterAuto.Aut_cate_cd = string.IsNullOrEmpty(BaseCls.GlbUserDefLoca) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
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

                if (txtDispatchRequried.Text.ToString().ToUpper() == BaseCls.GlbUserDefLoca.ToString().ToUpper())
                { MessageBox.Show("Please enter valid dispatch required location.", "Request Type", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDispatchRequried.Focus(); return; }

                //MasterUserLocation Or MasterUserProfitCenter.
                if (string.IsNullOrEmpty(txtRequestDate.Text))
                { MessageBox.Show("Please enter request date.", "Request Date", MessageBoxButtons.OK, MessageBoxIcon.Information); txtRequestDate.Focus(); return; }

                if (string.IsNullOrEmpty(txtRequriedDate.Text))
                { MessageBox.Show("Please enter required date.", "Required Date", MessageBoxButtons.OK, MessageBoxIcon.Information); txtRequriedDate.Focus(); return; }
                if (DateTime.Compare(Convert.ToDateTime(txtRequriedDate.Text.Trim()), Convert.ToDateTime(txtRequestDate.Text.Trim())) < 0)
                { MessageBox.Show("Required date can't be less than request date.", "Required Date", MessageBoxButtons.OK, MessageBoxIcon.Information); txtRequriedDate.Focus(); return; }

                int _count = 1;
                _invReqItemList.ForEach(x => x.Itri_line_no = _count++);
                _invReqItemList.ForEach(X => X.Itri_bqty = X.Itri_qty);
                _invReqItemList.ForEach(X => X.Itri_mitm_stus = X.Itri_itm_stus);
                _invReqItemList.Where(x => string.IsNullOrEmpty(x.Itri_mitm_cd)).ToList().ForEach(y => y.Itri_mitm_cd = y.Itri_itm_cd);

                List<InventoryRequestItem> _inventoryRequestItemList = _invReqItemList;
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

                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                InventoryRequest _inventoryRequest = new InventoryRequest();
                InventoryRequest _inventoryRequest_R = new InventoryRequest();
                //Fill the InventoryRequest header & footer data.
                _inventoryRequest.Itr_com = BaseCls.GlbUserComCode;
                _inventoryRequest.Itr_req_no = GetRequestNo();
                _inventoryRequest.Itr_tp = "MRN";
                _inventoryRequest.Itr_sub_tp = Convert.ToString(ddlRequestSubType.SelectedValue);
                _inventoryRequest.Itr_loc = _masterLocation;
                _inventoryRequest.Itr_ref = string.Empty;
                _inventoryRequest.Itr_dt = Convert.ToDateTime(txtRequestDate.Text);
                _inventoryRequest.Itr_exp_dt = Convert.ToDateTime(txtRequriedDate.Text);

                if (_autoAppr == 1 && _inventoryRequest.InventoryRequestItemList != null)
                {
                    if (string.IsNullOrEmpty(txtDispatchRequried.Text) || txtDispatchRequried.Text == "N/A")
                    {
                       // MessageBox.Show("Please enter dispatch required location.", "Dispatch Location", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDispatchRequried.Focus(); 
                       // return; 
                    }
                    _inventoryRequest.Itr_stus = "F";  //P - Pending , A - Approved. //kapila 3/6/2017 set F coz auto generated REQD number
                  
                }                        
                else
                    _inventoryRequest.Itr_stus = "P";

                _inventoryRequest.Itr_job_no = string.Empty;  //Invoice No.
                _inventoryRequest.Itr_bus_code = string.Empty;  //Customer Code.
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
                _inventoryRequest.Itr_mod_by =BaseCls.GlbUserID;
                _inventoryRequest.Itr_session_id = BaseCls.GlbUserSessionID;
                _inventoryRequest.Itr_issue_com = ddlDispCom.SelectedValue.ToString(); //Edit by Chamal 30/10/2013
                //kapila 7/12/2016
                _inventoryRequest.Temp_is_res_request = _isResvNo;
                _inventoryRequest.Itr_system_module = "MRN";

                //kapila 18/8/2017
                if (_isResvNo == true)
                    _inventoryRequest.TMP_IS_RES_UPDATE = true;

                // _inventoryRequest_R = _inventoryRequest;
                //_inventoryRequest_R.InventoryRequestItemList = _inventoryRequestItemList.Where(x => x.Itri_is_auto_app == 1).ToList();
                _inventoryRequest.InventoryRequestItemList = _inventoryRequestItemList.Where(x => x.Itri_is_auto_app == 0).ToList();
                //kapila 21/6/2017
                List<InventoryRequestItem> _inventoryRequestAutoAppItemList = null;
                _inventoryRequestAutoAppItemList = _inventoryRequestItemList.Where(x => x.Itri_is_auto_app == 1).ToList();


                int rowsAffected = 0;
                string _docNo = string.Empty;
                string _reqdNum = "";

                if (string.IsNullOrEmpty(txtRequest.Text))
                {
                    rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData_SR(_inventoryRequest, _inventoryRequestAutoAppItemList, GenerateMasterAutoNumber(), out _docNo, out _reqdNum, _isFoundSys_Param);
                    MessageBox.Show("Inventory Request Document Successfully saved. " + _docNo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //kapila 10/7/2017
                    if (!string.IsNullOrEmpty(_reqdNum)) MessageBox.Show("Request approved Successfully. " + _reqdNum, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string[] seperator = new string[] { "," };
                    string[] searchParams = _docNo.Split(seperator, StringSplitOptions.None);
                    //if(searchParams.Count>1)
                    foreach (string _printDocNo in searchParams)
                    {
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        _view.GlbReportName = string.Empty;
                        _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SMRNRepPrints.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_MRNRepPrints.rpt" : "MRNRepPrints.rpt";
                        _view.GlbReportDoc = _printDocNo;
                        _view.TopMost = true;
                        _view.Show();
                        _view = null;
                    }
                }
                else
                {
                    rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, null, out _docNo);
                    MessageBox.Show("Inventory Request Document Successfully Updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                InitVariables();
                LoadCachedObjects();
                InitializeForm();
                pnlBalance.Size = new Size(504, 177);
                ddlRequestSubType.Enabled = true;
                ddlRequestSubType.SelectedIndex = 1;
                ddlRequestSubType.Enabled = true;
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
                if (CheckServerDateTime() == false) return;
                //kapila 26/11/2016
                InventoryRequest _reqH = new InventoryRequest();
                _reqH.Itr_com = BaseCls.GlbUserComCode;
                _reqH.Itr_req_no = txtRequest.Text;
                _reqH.Itr_stus = "F";


                List<InventoryRequest> _LstreqH = CHNLSVC.Inventory.GET_INT_REQ_DATA(_reqH);
                if (_LstreqH.Count > 0)
                {
                    MessageBox.Show("Cannot Process! Selected request number has been already approved !", "Request Type", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                DataTable _rootSchedules = new DataTable();
                
                _rootSchedules = GetRootSchedules(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtRequriedDate.Value.Date);
                if (_rootSchedules.Rows.Count < 1)
                {
                    DialogResult _result = MessageBox.Show("Root schedule information not found for selected date. Do you want to continue ?", "Root Schedules", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == System.Windows.Forms.DialogResult.No) { txtRequriedDate.Value = DateTime.Today.Date; return; }
                }
                //add by tharanga chen mrn req date anfd time
                #region MRN time and date validate
                DateTime dt = Convert.ToDateTime(txtRequriedDate.Value.Date.ToString("yyyy-MMM-dd"));
                DataTable _dtSysPara = new DataTable();
                HpSystemParameters _getSystemParameter = new HpSystemParameters();
                DateTime dtnow = CHNLSVC.Security.GetServerDateTime().Date;

                TimeSpan timespan = CHNLSVC.Security.GetServerDateTime().TimeOfDay;
                string output = timespan.ToString("h\\.mm");
                decimal time = Convert.ToDecimal(output);
                bool sheddate = true;
                _getSystemParameter = CHNLSVC.Sales.GetSystemParameter("CHNL", BaseCls.GlbDefChannel, "MRNTIME", DateTime.Now.Date);



                foreach (DataRow dtRow in _rootSchedules.Rows)
                {
                    DateTime ntshed = Convert.ToDateTime(_rootSchedules.Rows[0]["SCHEDUL_DATE"].ToString());
                    if (ntshed == dt)
                    {
                        if (dt > dtnow)
                        {
                            sheddate = true;
                            break;
                        }
                        if (_getSystemParameter.Hsy_cd != null)
                        {
                            //  _getSystemParameter = CHNLSVC.Sales.GetSystemParameter("CHNL", BaseCls.GlbDefChannel, "MRNTIME", DateTime.Now.Date);
                            if (_getSystemParameter.Hsy_val > time)
                            {
                                sheddate = true;
                                break;
                            }
                            else
                            {
                                sheddate = false;
                                MessageBox.Show("MRN cut-off time exceed.  Please select next delivery date. ", "Root Schedules", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                return;
                            }
                        }
                        else
                        {
                            sheddate = true;
                            break;
                        }

                    }
                    else
                    {
                        sheddate = false;
                        MessageBox.Show("Root schedule information not found for selected date ", "Root Schedules", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        return;
                    }
                }
                if (sheddate == false)
                {
                    MessageBox.Show("Root schedule information not found for selected date ", "Root Schedules", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }

                #endregion 
                //DateTime dt = Convert.ToDateTime(txtRequriedDate.Value.Date.ToString("yyyy-MMM-dd"));
                //_rootSchedules = _rootSchedules.AsEnumerable().Where(c => c.Field<DateTime>("FRSH_SHED_DT") == txtRequriedDate.Value.Date).CopyToDataTable();

                if (MessageBox.Show("Do you need to process this MRN?", "Save...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    this.SaveInventoryRequestData();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        #endregion

        #region Rooting for Re-Call the Request No
        private void SetSelectedInventoryRequestData(InventoryRequest _selectedInventoryRequest)
        {
            if (_selectedInventoryRequest != null)
            {
                //Set Header details.
                BindRequestSubTypesDDLData(ddlRequestSubType);
                ddlRequestSubType.SelectedValue = _selectedInventoryRequest.Itr_sub_tp;
                txtDispatchRequried.Text = _selectedInventoryRequest.Itr_issue_from;
                txtRequestDate.Text = _selectedInventoryRequest.Itr_dt.ToString("dd/MMM/yyyy");
                txtRequriedDate.Text = _selectedInventoryRequest.Itr_exp_dt.ToString("dd/MMM/yyyy");
                //txtInvoiceNo.Text = _selectedInventoryRequest.Itr_job_no;
                txtNIC.Text = _selectedInventoryRequest.Itr_collector_id;
                txtCollecterName.Text = _selectedInventoryRequest.Itr_collector_name;
                txtRemark.Text = _selectedInventoryRequest.Itr_note;

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
                if (_selectedInventoryRequest.Itr_stus == "A")
                {
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                }
                if (_selectedInventoryRequest.Itr_stus == "F") //added by Chamal 12-09-2016
                {
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                }
                if (_selectedInventoryRequest.Itr_stus == "C")
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
                InventoryRequest _inputInvReq = new InventoryRequest();
                _inputInvReq.Itr_req_no = txtRequest.Text.Trim();
                InventoryRequest _selectedInventoryRequest = CHNLSVC.Inventory.GetInventoryRequestData(_inputInvReq);

                if (_selectedInventoryRequest != null)
                    if (!string.IsNullOrEmpty(_selectedInventoryRequest.Itr_com))
                    {
                        if (_selectedInventoryRequest.Itr_stus != "P")//add by akila 2017/10/12
                        {
                            btnSave.Enabled = false;
                            btnAddItem.Enabled = false;
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            btnAddItem.Enabled = true;
                        }

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

                InventoryRequest _inputInvReq = new InventoryRequest();
                _inputInvReq.Itr_com = BaseCls.GlbUserComCode;
                _inputInvReq.Itr_loc = BaseCls.GlbUserDefLoca;
                _inputInvReq.Itr_req_no = txtRequest.Text;
                _inputInvReq.Itr_stus = "C";
                _inputInvReq.Itr_mod_by = BaseCls.GlbUserID;
                _inputInvReq.Itr_session_id = BaseCls.GlbUserSessionID;
                _inputInvReq.Itr_cre_by = BaseCls.GlbUserID;

                int result = CHNLSVC.Inventory.UpdateInventoryRequestStatus(_inputInvReq);

                if (result > 0)
                    MessageBox.Show("Inventory Request " + _inputInvReq.Itr_req_no + " successfully Cancel.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Inventory Request " + _inputInvReq.Itr_req_no + " can't be Cancel.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
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
        private bool IsValidDispatchLocation()
        {
            bool status = false;
            txtDispatchLoc.Text = txtDispatchLoc.Text.Trim().ToUpper().ToString();
            MasterLocation _masterLocation = CHNLSVC.General.GetLocationByLocCode(ddlDispCom.SelectedValue.ToString(), txtDispatchLoc.Text.ToString());
            status = (_masterLocation == null) ? false : true;
            return status;
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

        private void txtDispatchLoc_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDispatchLoc.Text)) return;
            try
            {
                if (txtDispatchLoc.Text != "N/A")
                {
                    if (BaseCls.GlbUserDefLoca == txtDispatchLoc.Text)
                    {
                        MessageBox.Show("You cannot enter same location.", "Dispatching Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDispatchLoc.Clear();
                        txtDispatchLoc.Focus();
                        return;
                    }
                    if (IsValidDispatchLocation() == false)
                    {
                        MessageBox.Show("Please check the dispatch location.", "Dispatching Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDispatchLoc.Clear();
                        txtDispatchLoc.Focus();
                        return;
                    }

                    //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    //_CommonSearch.ReturnIndex = 0;
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PreferLoc);
                    //_result = CHNLSVC.CommonSearch.GetPreferLocSearchData(_CommonSearch.SearchParams, null, null);
                    //var _manname = _result.AsEnumerable().Where(X => X.Field<String>("location") == txtDispatchLoc.Text).ToList();
                    //if (_manname == null || _manname.Count == 0)
                    //{
                    //    MessageBox.Show("Please select valid dispatch location.", "Dispatching Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtDispatchLoc.Clear();
                    //    txtDispatchLoc.Focus();
                    //    return;
                    //}
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btn_srch_dispatch_loc_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    MessageBox.Show("Please select the item.", "Dispatching Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }
                _mainItemCode = txtItem.Text;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PreferLoc);
                DataTable _result = CHNLSVC.CommonSearch.GetPreferLocSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDispatchLoc;
                _CommonSearch.ShowDialog();
                txtDispatchLoc.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnCloseErr_Click(object sender, EventArgs e)
        {
            pnlError.Visible = false;
        }

        private void btnClsRtBal_Click(object sender, EventArgs e)
        {
            pnlRootBal.Visible = false;
        }

        private void txtDispatchLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddItem.Focus();
            else if (e.KeyCode == Keys.F2)
                btn_srch_dispatch_loc_Click(null, null);
        }

        private void txtDispatchLoc_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_dispatch_loc_Click(null, null);
        }

        private void txtDispatchLoc_TextChanged(object sender, EventArgs e)
        {

        }

        private DataTable GetRootSchedules(string _comCode, string _locCode, DateTime _schDate)
        {
            DataTable _rootSchedules = new DataTable();

            try
            {
                _rootSchedules = CHNLSVC.Inventory.GetRootSchedules(_comCode, _locCode, _schDate);
            }
            catch (Exception ex)
            { 
                SystemErrorMessage(ex);
                this.Cursor = Cursors.Default; 
                CHNLSVC.CloseAllChannels();
            }

            return _rootSchedules;
        }

        private void btnCloseRootSchedules_Click(object sender, EventArgs e)
        {
            pnlRootScheduls.Visible = false;
        }

        private void lbRootSchedul_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DataTable _rootSchedules = new DataTable();
            _rootSchedules = GetRootSchedules(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, DateTime.Today.Date);
            if (_rootSchedules.Rows.Count > 0)
            {
                BindingSource _source = new BindingSource();
                _source.DataSource = _rootSchedules;
                dgvRootSchedules.DataSource = _source;

                pnlRootScheduls.Visible = true;
                pnlRootScheduls.Focus();
            }
            else 
            {
                MessageBox.Show("Root schedule information not found", "Root Schedules", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void dgvRootSchedules_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (dgvRootSchedules.Rows.Count > 0)
                {
                    string _selectedDate = dgvRootSchedules.Rows[e.RowIndex].Cells["colSchDate"].Value.ToString();
                    if (!string.IsNullOrEmpty(_selectedDate)) { txtRequriedDate.Value = DateTime.Parse(_selectedDate); pnlRootScheduls.Visible = false; }
                }
            }
        }
        private void load_value()
        {

            Ins_value = CHNLSVC.Inventory.GET_INSVALUE_BYLOC(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            currentCost_Loc = CHNLSVC.Inventory.GetLatestCost_Loc(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            if (Ins_value > 0)
            {


                DataTable _git = CHNLSVC.Inventory.Get_GIT_Detailsnew(Convert.ToDateTime(DateTime.Now).Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, null, null, null, null, null, null, null, null, null);
                TotalGIT = _git.AsEnumerable().Sum(x => x.Field<decimal>("Item Total Cost"));
            }
        }
    }
}
