using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Text;

namespace FF.WindowsERPClient.Services
{
    public partial class StandbyIssue : Base
    {
        private String gblJobNumber = string.Empty;
        private Int32 gblJobLine = 0;
        private bool _isDecimalAllow = false;
        private MasterItem _itemdetail = null;

        private List<Service_StandBy> oStandByItemsGBL = new List<Service_StandBy>();

        private List<InventoryRequestItem> _invReqItemList = null;

        public StandbyIssue(String jobNum, Int32 JObLine)
        {
            InitializeComponent();
            gblJobNumber = jobNum;
            gblJobLine = JObLine;

            dgvItemsD13.AutoGenerateColumns = false;
            dgvNewItems.AutoGenerateColumns = false;
            dgvReceives.AutoGenerateColumns = false;
        }

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

                case CommonUIDefiniton.SearchUserControlType.SalesInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.ServiceWIPMRN_Loc:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "S" + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear the screen", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
            {
                clearAll();
            }
        }

        private void StandbyIssue_Load(object sender, EventArgs e)
        {
            dgvMRNDetails.AutoGenerateColumns = false;
            this.Text = "Stand by issue   |   Job Number :" + gblJobNumber;
            clearAll();

            DataTable dtTemp = new DataTable();
            dtTemp = CHNLSVC.CustService.Get_service_location(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                txtDispatchRequried.Text = dtTemp.Rows[0]["SLL_LOC"].ToString();
            }
            btnSearch_Click(null, null);
            _invReqItemList = new List<InventoryRequestItem>();

            getSavedItems();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckServerDateTime() == false)
                    return;
                if (string.IsNullOrEmpty(txtDispatchRequried.Text))
                {
                    MessageBox.Show("Please select a dispatch location", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDispatchRequried.Focus();
                    return;
                }
                if (dgvNewItems.Rows.Count > 0)
                {
                    if (MessageBox.Show("Do you need to process this MRN?", "Save...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        this.SaveInventoryRequestData();

                    clearAll();
                }
                else
                {
                    MessageBox.Show("Please add records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            oStandByItemsGBL = CHNLSVC.CustService.GetStandByItems(BaseCls.GlbUserComCode, gblJobNumber, gblJobLine, "%" + txtItem.Text + "%", txtDispatchRequried.Text);
            dgvItemsD13.DataSource = oStandByItemsGBL;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                txtItem.Clear();
            }
        }

        private void dgvItemsD13_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                txtItemAdd.Text = dgvItemsD13.Rows[e.RowIndex].Cells["INL_ITM_CD"].Value.ToString();
                txtModel.Text = dgvItemsD13.Rows[e.RowIndex].Cells["MI_MODEL"].Value.ToString();
                txtBrand.Text = dgvItemsD13.Rows[e.RowIndex].Cells["MI_BRAND"].Value.ToString();
                loadItemDetails(txtItemAdd.Text);
                txtQty.Focus();
            }
        }

        private void clearAll()
        {
            txtItem.Clear();
            txtItemAdd.Clear();
            txtModel.Clear();
            txtQty.Clear();
            chkAll.Checked = true;
            dgvItemsD13.DataSource = new List<Service_stockReturn>();
            dgvNewItems.DataSource = new List<InventoryRequestItem>();
            dgvReceives.DataSource = null;
            lblItemDescription.Text = "";
            lblItemModel.Text = "";
            lblItemBrand.Text = "";
            lblItemSubStatus.Text = "";
            _itemdetail = null;
            _isDecimalAllow = false;
            oStandByItemsGBL = new List<Service_StandBy>();
            _invReqItemList = null;
            txtBrand.Clear();

            btnSearch2_Click(null, null);
            btnSearch_Click(null, null);
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtItemAdd.Text) && !string.IsNullOrEmpty(txtModel.Text))
                {
                    if (oStandByItemsGBL.FindAll(x => x.INL_ITM_CD == txtItemAdd.Text && x.MI_MODEL == txtModel.Text).Count > 0)
                    {
                        Service_StandBy oSelectedItem = oStandByItemsGBL.Find(x => x.INL_ITM_CD == txtItemAdd.Text && x.MI_MODEL == txtModel.Text);
                        if (oSelectedItem.INL_QTY < Convert.ToDecimal(txtQty.Text))
                        {
                            MessageBox.Show("Please enter a valid quantity.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtQty.SelectAll();
                            txtQty.Focus();
                            return;
                        }
                    }
                }
            }
            btnAddItems.Focus();
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtItem_TextChanged(object sender, EventArgs e)
        {
            chkAll.Checked = false;
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }
        }

        private void btnAddItems_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    MessageBox.Show("Please enter required quantity.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtQty.Text.ToString()) <= 0)
                {
                    MessageBox.Show("Please enter valid quantity.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Focus();
                    return;
                }

                string _mainItemCode = txtItemAdd.Text.Trim().ToUpper();
                string _itemStatus = "STDBY";
                decimal _mainItemQty = (string.IsNullOrEmpty(txtQty.Text)) ? 0 : Convert.ToDecimal(txtQty.Text.Trim());
                string _remarksText = string.IsNullOrEmpty(txtItmRemark.Text.Trim()) ? string.Empty : txtItmRemark.Text.Trim();
                bool _isSubItemHave = (string.IsNullOrEmpty(lblItemSubStatus.Text)) ? false : lblItemSubStatus.Text.Trim() == "Sub Item Status : Available" ? true : false;

                List<InventoryRequestItem> _temp = _invReqItemList;
                List<InventoryRequestItem> _resultList = null;
            gn:

                if (_isSubItemHave)
                {
                    //Get the relevant sub items.
                    List<MasterItemComponent> _itemComponentList = CHNLSVC.Inventory.GetItemComponents(_mainItemCode);

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
                                //_inventoryRequestItem.Itri_res_no = _reservationNo;
                                _inventoryRequestItem.Itri_note = _remarksText;
                                _inventoryRequestItem.Itri_qty = _subItemQty;
                                _inventoryRequestItem.Itri_app_qty = _subItemQty;

                                //Add Main item details.
                                _inventoryRequestItem.Itri_mitm_cd = _mainItemCode;
                                _inventoryRequestItem.Itri_mitm_stus = _itemStatus;
                                _inventoryRequestItem.Itri_mqty = _mainItemQty;

                                _inventoryRequestItem.Itri_job_no = gblJobNumber;
                                _inventoryRequestItem.Itri_job_line = gblJobLine;

                                if (_invReqItemList == null)
                                {
                                    _invReqItemList = new List<InventoryRequestItem>();
                                }

                                _invReqItemList.Add(_inventoryRequestItem);
                            }
                        }
                    }
                    else
                    {
                        _isSubItemHave = false;
                        goto gn;
                    }
                }
                else
                {
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
                        //_inventoryRequestItem.Itri_res_no = _reservationNo;
                        _inventoryRequestItem.Itri_note = _remarksText;
                        _inventoryRequestItem.Itri_qty = _mainItemQty;
                        _inventoryRequestItem.Itri_app_qty = _mainItemQty;

                        //Add Main item details.
                        _inventoryRequestItem.Itri_mitm_cd = _mainItemCode;
                        _inventoryRequestItem.Itri_mitm_stus = _itemStatus;
                        _inventoryRequestItem.Itri_mqty = 0;

                        _inventoryRequestItem.Itri_job_no = gblJobNumber;
                        _inventoryRequestItem.Itri_job_line = gblJobLine;

                        if (_invReqItemList == null)
                        {
                            _invReqItemList = new List<InventoryRequestItem>();
                        }

                        _invReqItemList.Add(_inventoryRequestItem);
                    }
                }

                txtItemAdd.Clear();
                txtModel.Clear();
                txtQty.Clear();
                lblItemDescription.Text = "";
                lblItemModel.Text = "";
                lblItemBrand.Text = "";
                lblItemSubStatus.Text = "";
                txtBrand.Clear();

                dgvNewItems.DataSource = new List<InventoryRequestItem>();
                dgvNewItems.DataSource = _invReqItemList;

                if (MessageBox.Show("Do you need to add another item?", "Adding...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtItem.Focus();
                    return;
                }
                else
                {
                    toolStrip1.Focus();
                    btnSave.Select();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadItemDetails(String _item)
        {
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
        }

        private void SaveInventoryRequestData()
        {
            try
            {
                int _count = 1;
                _invReqItemList.ForEach(x => x.Itri_line_no = _count++);
                _invReqItemList.ForEach(X => X.Itri_bqty = X.Itri_qty);
                _invReqItemList.ForEach(X => X.Itri_mitm_stus = X.Itri_itm_stus);
                _invReqItemList.Where(x => string.IsNullOrEmpty(x.Itri_mitm_cd)).ToList().ForEach(y => y.Itri_mitm_cd = y.Itri_itm_cd);

                List<InventoryRequestItem> _inventoryRequestItemList = _invReqItemList;
                if ((_inventoryRequestItemList == null) || (_inventoryRequestItemList.Count == 0))
                { MessageBox.Show("Please add items to List.", "No Item", MessageBoxButtons.OK, MessageBoxIcon.Information); txtItem.Focus(); return; }

                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                InventoryRequest _inventoryRequest = new InventoryRequest();
                _inventoryRequest.Itr_com = BaseCls.GlbUserComCode;
                _inventoryRequest.Itr_req_no = GetRequestNo();
                _inventoryRequest.Itr_tp = "MRN";
                _inventoryRequest.Itr_sub_tp = "SCV";
                _inventoryRequest.Itr_loc = _masterLocation;
                _inventoryRequest.Itr_ref = string.Empty;
                _inventoryRequest.Itr_dt = DateTime.Today.Date;
                _inventoryRequest.Itr_exp_dt = DateTime.Today.Date;

                _inventoryRequest.Itr_stus = "A";  //P - Pending , A - Approved.
                //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10800))
                //{
                //    _inventoryRequest.Itr_stus = "A";  //P - Pending , A - Approved.
                //}
                _inventoryRequest.Itr_job_no = gblJobNumber;
                _inventoryRequest.Itr_bus_code = string.Empty;
                _inventoryRequest.Itr_note = string.Empty;
                _inventoryRequest.Itr_issue_from = txtDispatchRequried.Text;
                _inventoryRequest.Itr_rec_to = _masterLocation;
                _inventoryRequest.Itr_direct = 0;
                _inventoryRequest.Itr_country_cd = string.Empty;
                _inventoryRequest.Itr_town_cd = string.Empty;
                _inventoryRequest.Itr_cur_code = string.Empty;
                _inventoryRequest.Itr_exg_rate = 0;
                _inventoryRequest.Itr_act = 1;
                _inventoryRequest.Itr_cre_by = BaseCls.GlbUserID;
                _inventoryRequest.Itr_session_id = BaseCls.GlbUserSessionID;
                _inventoryRequest.Itr_issue_com = BaseCls.GlbUserComCode;
                _inventoryRequest.Itr_job_line = gblJobLine;

                _inventoryRequest.InventoryRequestItemList = _inventoryRequestItemList;
                int rowsAffected = 0;
                string _docNo = string.Empty;

                if (string.IsNullOrEmpty(txtRequest.Text))
                {
                    rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, GenerateMasterAutoNumber(), out _docNo);
                    if (rowsAffected > 0)
                    {
                        if (MessageBox.Show("Inventory Request Document Successfully saved. " + _docNo + "\nDo you want to print?", "Save...", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                        {
                            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                            BaseCls.GlbReportName = string.Empty;
                            GlbReportName = string.Empty;
                            _view.GlbReportName = string.Empty;
                            _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SMRNRepPrints.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_MRNRepPrints.rpt" : "MRNRepPrints.rpt";
                            _view.GlbReportDoc = _docNo;
                            _view.TopMost = true;
                            _view.Show();
                            _view = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Process Terminated", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, null, out _docNo);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Inventory Request Document Successfully Updated.", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnClear_Click(null, null);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Process Terminated", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                txtRequest.Focus();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                CHNLSVC.CloseChannel();
                return;
            }
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

        private MasterAutoNumber GenerateMasterAutoNumber()
        {
            string moduleText = "MRN";
            MasterAutoNumber masterAuto;
            masterAuto = new MasterAutoNumber();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to close?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnSearch_DispatchRequired_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceWIPMRN_Loc);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_SCV_MRN(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDispatchRequried;
                _CommonSearch.ShowDialog();
                txtDispatchRequried.Select();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void txtDispatchRequried_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_DispatchRequired_Click(null, null);
        }

        private void txtDispatchRequried_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtDispatchRequried_DoubleClick(null, null);
            }
        }

        private void btnSearch2_Click(object sender, EventArgs e)
        {
            List<Service_stockReturn> oItems = CHNLSVC.CustService.GetServiceJobStockItems(BaseCls.GlbUserComCode, gblJobNumber, gblJobLine, "", BaseCls.GlbUserDefLoca);
            foreach (Service_stockReturn item in oItems)
            {
                item.Remark = "STOCK";
            }

            List<Service_stockReturn> oItemsPending = CHNLSVC.CustService.GetStandyPendingADOItems(BaseCls.GlbUserComCode, gblJobNumber, gblJobLine, "", txtDispatchRequried.Text);
            List<Service_TempIssue> oIssedItem = CHNLSVC.CustService.Get_ServiceWIP_TempIssued_Items(BaseCls.GlbUserComCode, gblJobNumber, gblJobLine, string.Empty, BaseCls.GlbUserDefLoca, "STBYI");

            if (oItemsPending != null && oItemsPending.Count > 0)
            {
                //foreach (Service_stockReturn oItemsTemp in oItems)
                //{
                //    oItemsPending.RemoveAll(x => x.SERIAL_ID == oItemsTemp.SERIAL_ID);
                //}
                if (oItemsPending.Count > 0)
                {
                    foreach (Service_stockReturn item in oItemsPending)
                    {
                        item.Remark = "PENDINGS";
                    }

                    oItems.AddRange(oItemsPending);
                }
            }

            if (oIssedItem != null && oIssedItem.Count > 0)
            {
                foreach (Service_TempIssue oIssued in oIssedItem)
                {
                    oItems.RemoveAll(x => x.ITEM_CODE == oIssued.STI_ISSUEITMCD && x.SERIAL_NO == oIssued.STI_ISSUESERIALNO && x.INB_ITM_STUS == oIssued.STI_ISSUEITMSTUS && x.QTY == oIssued.STI_ISSUEITMQTY);
                }
            }

            dgvReceives.DataSource = new List<Service_stockReturn>();
            dgvReceives.DataSource = oItems;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (isAnySelected())
            {
                if (MessageBox.Show("Do you want to issue?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    // Service_TempIssue oItem = new Service_TempIssue();
                    List<Service_TempIssue> oMainList = new List<Service_TempIssue>();

                    List<Tuple<String, String>> oAOD_Serial = new List<Tuple<String, String>>();

                    for (int i = 0; i < dgvReceives.Rows.Count; i++)
                    {
                        if (dgvReceives.Rows[i].Cells["select"].Value != null && Convert.ToBoolean(dgvReceives.Rows[i].Cells["select"].Value) == true)
                        {
                            Service_TempIssue oItem = new Service_TempIssue();
                            //oItem.STI_SEQNO             = "";
                            oItem.STI_LINE = i + 1;
                            //oItem.STI_DOC = "";
                            oItem.STI_DT = DateTime.Today.Date;
                            oItem.STI_COM = BaseCls.GlbUserComCode;
                            oItem.STI_LOC = BaseCls.GlbUserDefLoca;
                            oItem.STI_DOC_TP = "STBYI";
                            oItem.STI_JOBNO = gblJobNumber;
                            oItem.STI_JOBLINENO = gblJobLine;
                            oItem.STI_ISSUEITMCD = dgvReceives.Rows[i].Cells["ITEM_CODE"].Value.ToString();
                            oItem.STI_ISSUEITMSTUS = dgvReceives.Rows[i].Cells["STATUS_CODE"].Value.ToString();
                            oItem.STI_ISSUESERIALNO = dgvReceives.Rows[i].Cells["serial_no"].Value.ToString();
                            if (dgvReceives.Rows[i].Cells["serial_no"].Value.ToString() != "N/A")
                            {
                                oItem.STI_ISSUESERID = Convert.ToInt32(dgvReceives.Rows[i].Cells["SERIAL_ID"].Value.ToString());
                            }
                            oItem.STI_ISSUEITMQTY = Convert.ToDecimal(dgvReceives.Rows[i].Cells["qty"].Value.ToString());
                            oItem.STI_BALQTY = Convert.ToDecimal(dgvReceives.Rows[i].Cells["qty"].Value.ToString());
                            oItem.STI_CROSS_SEQNO = 0;
                            oItem.STI_CROSS_LINE = 0;
                            oItem.STI_ISRECEIVE = 0;
                            oItem.STI_TECHCODE = "";
                            oItem.STI_REFDOCNO = "";
                            oItem.STI_REFDOCLINE = 0;
                            oItem.STI_STUS = "A";
                            //oItem.STI_RMK = dgvReceives.Rows[i].Cells["Remark"].Value == null ? string.Empty : dgvReceives.Rows[i].Cells["Remark"].Value.ToString();
                            oItem.STI_CRE_BY = BaseCls.GlbUserID;
                            oItem.STI_CRE_DT = DateTime.Now;
                            oMainList.Add(oItem);

                            if (dgvReceives.Rows[i].Cells["Remark"].Value.ToString() == "PENDINGS")
                            {
                                oAOD_Serial.Add(new Tuple<string, string>(dgvReceives.Rows[i].Cells["inward_doc"].Value.ToString(), oItem.STI_ISSUESERIALNO));
                            }

                            // BaseCls.GlbDefaultBin
                            //#region AOD IN


                            //string ADONumber = dgvReceives.Rows[i].Cells["inward_doc"].Value.ToString();



                            //List<ReptPickSerials> PickSerialsList = new List<ReptPickSerials>();


                            //if (PickSerialsList == null)
                            //{ MessageBox.Show("No item found!"); return; }
                            //btnSave.Enabled = false;

                            //int _updateDate = CHNLSVC.Inventory.ChangeScanSerialDocDate(invHdr.Ith_oth_com, invHdr.Ith_com, invHdr.Ith_doc_tp, ADONumber, invHdr.Ith_doc_date.Date, BaseCls.GlbUserID);

                            //PickSerialsList.ForEach(x => x.Tus_doc_dt = invHdr.Ith_doc_date.Date);

                            //#endregion

                        }
                    }
                    MasterAutoNumber _ReqInsAuto = new MasterAutoNumber();

                    _ReqInsAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                    _ReqInsAuto.Aut_cate_tp = "LOC";
                    _ReqInsAuto.Aut_direction = 1;
                    _ReqInsAuto.Aut_modify_dt = null;
                    _ReqInsAuto.Aut_moduleid = "STBYI";
                    _ReqInsAuto.Aut_number = 0;
                    _ReqInsAuto.Aut_start_char = "STBYI";
                    _ReqInsAuto.Aut_year = DateTime.Today.Year;

                    InventoryHeader invHdr = new InventoryHeader();
                    invHdr.Ith_loc = BaseCls.GlbUserDefLoca;
                    invHdr.Ith_com = BaseCls.GlbUserComCode;
                    invHdr.Ith_oth_docno = string.Empty;
                    invHdr.Ith_doc_date = DateTime.Now.Date;
                    invHdr.Ith_doc_year = DateTime.Now.Year;
                    invHdr.Ith_doc_tp = "AOD";
                    invHdr.Ith_cate_tp = "SERVICE";
                    invHdr.Ith_sub_tp = "NOR";
                    invHdr.Ith_is_manual = false;
                    invHdr.Ith_stus = "A";
                    invHdr.Ith_cre_by = BaseCls.GlbUserID;
                    invHdr.Ith_mod_by = BaseCls.GlbUserID;
                    invHdr.Ith_direct = true;
                    invHdr.Ith_session_id = BaseCls.GlbUserSessionID;
                    invHdr.Ith_manual_ref = "N/A";
                    invHdr.Ith_remarks = "ADO-IN Service Standby";
                    invHdr.Ith_vehi_no = string.Empty;
                    invHdr.Ith_bus_entity = string.Empty;
                    invHdr.Ith_oth_com = string.Empty;
                    invHdr.Ith_oth_loc = string.Empty;
                    invHdr.Ith_sub_docno = gblJobNumber;
                    invHdr.Ith_job_no = gblJobNumber;
                    invHdr.Ith_isjobbase = true;
                    invHdr.Ith_pc = BaseCls.GlbUserDefProf;

                    string err = string.Empty;
                    int result = CHNLSVC.CustService.Save_SCV_TempIssueWithAOD_IN(oMainList, _ReqInsAuto, oAOD_Serial, out err, invHdr, BaseCls.GlbDefaultBin, gblJobNumber, gblJobLine);

                    if (result != -99 && result >= 0)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Successfully Saved!\n" + err, "Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearAll();
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Process Terminated.\n" + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            else
            {
                MessageBox.Show("Please select items.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private bool isAnySelected()
        {
            bool status = false;

            if (dgvReceives.Rows.Count > 0)
            {
                for (int i = 0; i < dgvReceives.Rows.Count; i++)
                {
                    if (dgvReceives.Rows[i].Cells["select"].Value != null && Convert.ToBoolean(dgvReceives.Rows[i].Cells["select"].Value) == true)
                    {
                        status = true;
                        break;
                    }
                }
            }
            return status;
        }

        private void getSavedItems()
        {
            DataTable dtTemp = CHNLSVC.CustService.GetMRNItemsByJobline(BaseCls.GlbUserComCode, gblJobNumber, gblJobLine);
            dgvMRNDetails.DataSource = dtTemp;
        }

        private void btnClear2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear the screen", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
            {
                clearAll();
            }
        }

        private void dgvReceives_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvReceives.IsCurrentCellDirty)
            {
                dgvReceives.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void txtDispatchRequried_Leave(object sender, EventArgs e)
        {
            List<MasterLocation> loc_list = CHNLSVC.Inventory.getAllLoc_WithSubLoc(BaseCls.GlbUserComCode, txtDispatchRequried.Text.ToUpper());
            if (loc_list == null || loc_list.Count == 0)
            {
                MessageBox.Show("Please enter correct location code.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDispatchRequried.Clear();
                txtDispatchRequried.Focus();
                return;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                btnSearch2_Click(null, null);
                getSavedItems();
            }
        }
    }
}