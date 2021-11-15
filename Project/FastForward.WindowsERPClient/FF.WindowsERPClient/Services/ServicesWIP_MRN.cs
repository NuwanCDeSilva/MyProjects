using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class ServicesWIP_MRN : Base
    {
        private MasterItem _itemdetail = null;
        private bool _isDecimalAllow = false;
        private List<InventoryRequestItem> _invReqItemList = null;
        private string JobNumber = string.Empty;
        private Int32 jobLineNum = -11;
        private string GblMrnNumber = string.Empty;

        private bool mouseIsDown = false;
        private Point firstPoint;

        public ServicesWIP_MRN(string job, Int32 jobLine, string MRN_Number)
        {
            InitializeComponent();
            fillItemStatus();
            pnlBalance.Size = new Size(504, 177);
            gvItem.AutoGenerateColumns = false;
            gvBalance.AutoGenerateColumns = false;
            dgvMRNDetails.AutoGenerateColumns = false;
            JobNumber = job;
            jobLineNum = jobLine;
            GblMrnNumber = MRN_Number;
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
                case CommonUIDefiniton.SearchUserControlType.ServiceEstimate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EstimateItems:
                    {
                        paramsText.Append(txtEstimate.Text + seperator + BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EetimateByJob:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + JobNumber + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        #region Events

        private void ServicesWIP_MRN_Load(object sender, EventArgs e)
        {
            _invReqItemList = new List<InventoryRequestItem>();
            btnClear_Click(null, null);
            if (string.IsNullOrEmpty(GblMrnNumber))
            {
                getSavedItems();
                btnApprove.Visible = false;
                btnCancel.Visible = false;
                btnSave.Visible = true;
                button1.Visible = true;
            }
            else
            {
                txtRequest.Text = GblMrnNumber;
                txtRequest_Leave(null, null);
                btnSearchMRN.Visible = false;
                txtRequest.ReadOnly = true;
                btnMRN.Visible = false;
                btnApprove.Visible = true;
                btnCancel.Visible = true;
                btnSave.Visible = false;
                button1.Visible = false;
            }

            DataTable dtTemp = new DataTable();
            dtTemp = CHNLSVC.CustService.Get_service_location(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Select("SLL_DEF = '1'").Length > 0)
                {
                    DataTable dtTemp1 = dtTemp.Select("SLL_DEF = '1'").CopyToDataTable();
                    txtDispatchRequried.Text = dtTemp1.Rows[0]["SLL_LOC"].ToString();
                }
                else
                {
                    txtDispatchRequried.Text = dtTemp.Rows[0]["SLL_LOC"].ToString();
                }
            }

            Service_Chanal_parameter _Parameters = null;
            _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_Parameters != null)
            {
                if (!string.IsNullOrEmpty(_Parameters.Sp_mrn_def_stus))
                {
                    cmbStatus.SelectedValue = _Parameters.Sp_mrn_def_stus.ToString();
                }
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtItem_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEstimate.Text))
            {
                try
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 1;
                    _CommonSearch.IsSearchEnter = true;
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
            else
            {
                try
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.ReturnIndex = 1;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EstimateItems);
                    DataTable _result = CHNLSVC.CommonSearch.SEARCH_ESTIMATE_ITEMS(_CommonSearch.SearchParams, null, null);
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
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtItem_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtItem_Leave(null, null);
                txtQty.Focus();
            }
        }

        private void txtItem_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtItem.Text))
            {
                return;
            }
            if (!LoadItemDetail(txtItem.Text.Trim()))
            {
                MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtItem.Clear();
                txtItem.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cmbStatus.Text))
                DisplayAvailableQty(txtItem.Text.Trim(), lblAvalQty, lblFreeQty, string.Empty);
            else
                DisplayAvailableQty(txtItem.Text.Trim(), lblAvalQty, lblFreeQty, cmbStatus.SelectedValue.ToString());
            if (_itemdetail.Mi_itm_tp != "V")
                LoadDispatchLocationInventoryBalance(txtItem.Text.Trim());

            if (!String.IsNullOrEmpty(txtEstimate.Text))
            {
                List<Service_Estimate_Item> oEstimateItems = CHNLSVC.CustService.GetServiceEstimateItems(txtEstimate.Text);
                if (oEstimateItems.FindAll(x => x.ESI_ITM_CD == txtItem.Text).Count == 0)
                {
                    MessageBox.Show("Selected item is not in the estimate-" + txtEstimate.Text + ".\nPlease enter valied item.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Clear();
                    return;
                }
            }
        }

        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            txtItem_DoubleClick(null, null);
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
                                        if (_itemdetail.Mi_itm_tp == "K") // Nadeeka 13-08-2015
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

        private void btnAddItem_MouseClick(object sender, MouseEventArgs e)
        {
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
                if (gvItem.Rows.Count > 0)
                {
                    if (MessageBox.Show("Do you need to process this MRN?", "Save...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        this.SaveInventoryRequestData();

                    btnClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Please add records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDispatchRequried.Clear();
            dtpRequestDate.Value = DateTime.Now;
            txtRequest.Clear();
            txtItem.Clear();
            //cmbStatus.SelectedIndex = 0;
            txtQty.Clear();
            txtRemark.Clear();
            lblAvalQty.Text = "";
            lblFreeQty.Text = "";
            lblItemDescription.Text = "Description : ";
            lblItemModel.Text = "Model : ";
            lblItemBrand.Text = "Brand : ";
            lblItemSubStatus.Text = "Sub Item Status : ";
            gvItem.DataSource = new List<InventoryRequestItem>();
            txtItmRemark.Clear();
            dgvMRNDetails.DataSource = CHNLSVC.CustService.GetMRNItemsByJobline("ZZ", "zz", -1);
            pnlMRN.Visible = false;
            txtDispatchRequried.Focus();
            _invReqItemList = new List<InventoryRequestItem>();
            gvItem.AutoGenerateColumns = false;
            gvBalance.AutoGenerateColumns = false;
            dgvMRNDetails.AutoGenerateColumns = false;
        }

        private void txtDispatchRequried_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDispatchRequried.Text))
            {
                DataTable dtLoc = CHNLSVC.CustService.Get_service_location(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                if (dtLoc == null || dtLoc.Rows.Count == 0)
                {
                    MessageBox.Show("Please enter correct location code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDispatchRequried.Clear();
                    txtDispatchRequried.Focus();
                    return;
                }
                else
                {
                    if (dtLoc.Select("SLL_LOC = '" + txtDispatchRequried.Text.Trim() + "'").Length == 0)
                    {
                        MessageBox.Show("Please enter correct location code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDispatchRequried.Clear();
                        txtDispatchRequried.Focus();
                        return;
                    }
                }
            }
        }

        private void txtDispatchRequried_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_DispatchRequired_Click(null, null);
        }

        private void txtDispatchRequried_Leave_1(object sender, EventArgs e)
        {
        }

        private void txtDispatchRequried_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_DispatchRequired_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtItem.Focus();
            }
        }

        private void btnSearch_DispatchRequired_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.IsSearchEnter = true;
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
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void cmbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtQty.Focus();
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtItmRemark.Focus();
            }
        }

        private void txtItmRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
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

        private void btnMRN_Click(object sender, EventArgs e)
        {
            if (pnlMRN.Visible == true)
            {
                pnlMRN.Visible = false;
            }
            else
            {
                getSavedItems();
                pnlMRN.Visible = true;
            }
        }

        private void btnHide_Click_1(object sender, EventArgs e)
        {
            pnlMRN.Visible = false;
        }

        private void label5_MouseDown(object sender, MouseEventArgs e)
        {
            firstPoint = e.Location;
            mouseIsDown = true;
        }

        private void label5_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                int xDiff = firstPoint.X - e.Location.X;
                int yDiff = firstPoint.Y - e.Location.Y;

                int x = pnlMRN.Location.X - xDiff;
                int y = pnlMRN.Location.Y - yDiff;
                pnlMRN.Location = new Point(x, y);
            }
        }

        private void label5_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }

        private void dgvMRNDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (dgvMRNDetails.SelectedRows[0].Cells["itri_itm_stus"].Value.ToString() == "TEMP SAVE" ||
                    dgvMRNDetails.SelectedRows[0].Cells["itri_itm_stus"].Value.ToString() == "PENDING" ||
                    dgvMRNDetails.SelectedRows[0].Cells["itri_itm_stus"].Value.ToString() == "SAVE")
                {

                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10810))
                    {
                        MessageBox.Show("You don't have the permission for this function.\nPermission Code :- 10810", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    if (MessageBox.Show("Do you want to cancel this MRN. \n MRN No :- " + dgvMRNDetails.SelectedRows[0].Cells[1].Value.ToString(), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        Int32 result = CHNLSVC.CustService.Update_ReqHeaderStatus("C", BaseCls.GlbUserID, BaseCls.GlbUserComCode, dgvMRNDetails.SelectedRows[0].Cells[1].Value.ToString());

                        if (result > 0)
                        {

                            Service_Message oMessage = new Service_Message();
                            oMessage.Sm_com = BaseCls.GlbUserComCode;
                            oMessage.Sm_jobno = JobNumber;
                            oMessage.Sm_joboline = jobLineNum;
                            oMessage.Sm_jobstage = 0;
                            oMessage.Sm_ref_num = string.Empty;
                            oMessage.Sm_status = 0;
                            oMessage.Sm_msg_tmlt_id = 12;

                            string err;
                            CHNLSVC.CustService.SaveServiceMsg(oMessage, out err);

                            MessageBox.Show("MRN canceled successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            getSavedItems();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Process terminated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You cant cancel this MRN in this status.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void txtEstimate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtEstimate_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtItem.Focus();
            }
        }

        private void txtEstimate_DoubleClick(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EetimateByJob);
            DataTable _result = CHNLSVC.CommonSearch.Get_Service_Estimates_ByJob(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtEstimate;
            _CommonSearch.ShowDialog();
            txtEstimate.Select();
        }

        private void txtEstimate_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEstimate.Text))
            {
                Service_Estimate_Header oheader = CHNLSVC.CustService.GetServiceEstimateHeader(txtEstimate.Text, BaseCls.GlbUserComCode);
                if (oheader == null || oheader.ESH_ESTNO == null || oheader.EST_STUS != "F")
                {
                    MessageBox.Show("Please enter correct Estimate number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEstimate.Clear();
                    txtEstimate.Focus();
                    return;
                }
                else
                {
                    List<Service_Estimate_Item> oEstItems = CHNLSVC.CustService.GetServiceEstimateItems(txtEstimate.Text);
                    if (oEstItems!=null)
                    {
                        foreach (Service_Estimate_Item item in oEstItems)
                        {
                            List<Service_job_Det> _jobItems = CHNLSVC.CustService.GetJobDetails(JobNumber, jobLineNum, BaseCls.GlbUserComCode);
                            string _jobItemCode = _jobItems[0].Jbd_itm_cd.ToString();

                            if (txtItem.Text.ToString().ToUpper() == _jobItemCode.ToString().ToUpper())
                            {
                                MessageBox.Show("Can not request job item in request!", "Job Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtItem.Focus();
                                return;
                            }

                            _invReqItemList.Clear();

                            InventoryRequestItem _inventoryRequestItem = new InventoryRequestItem();
                            MasterItem _itemDet = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, item.ESI_ITM_CD);

                            if (!(_itemDet.Mi_itm_tp == "V" || _itemDet.Mi_itm_tp == "Z"))
                            {
                                _inventoryRequestItem.Itri_itm_cd = item.ESI_ITM_CD;
                                _inventoryRequestItem.Mi_longdesc = _itemDet.Mi_longdesc;
                                _inventoryRequestItem.Mi_model = _itemDet.Mi_model;
                                _inventoryRequestItem.Mi_brand = _itemDet.Mi_brand;
                                _inventoryRequestItem.Itri_itm_stus = item.ESI_ITM_STUS;
                                //_inventoryRequestItem.Itri_res_no = _reservationNo;
                                _inventoryRequestItem.Itri_note = "";
                                _inventoryRequestItem.Itri_qty = item.ESI_QTY;
                                _inventoryRequestItem.Itri_app_qty = item.ESI_QTY;

                                //Add Main item details.
                                _inventoryRequestItem.Itri_mitm_cd = item.ESI_ITM_CD;
                                _inventoryRequestItem.Itri_mitm_stus = item.ESI_ITM_STUS;
                                _inventoryRequestItem.Itri_mqty = 0;

                                _inventoryRequestItem.Itri_job_no = JobNumber;
                                _inventoryRequestItem.Itri_job_line = jobLineNum;

                                _invReqItemList.Add(_inventoryRequestItem);
                            }
                        }

                        ClearLayer2();
                        ClearLayer3();

                        //Bind the updated list to grid.
                        gvItem.DataSource = new List<InventoryRequestItem>();
                        gvItem.DataSource = _invReqItemList;

                        toolStrip1.Focus();
                        btnSave.Select();
                        return;
                    }
                    
                }

            }
        }

        private void btnSearchEstimate_Click(object sender, EventArgs e)
        {
            txtEstimate_DoubleClick(null, null);
        }

        private void btnCloseFrom_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearchMRN_Click(object sender, EventArgs e)
        {
            txtMRNNum_DoubleClick(null, null);
        }

        private void txtMRNNum_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.IsSearchEnter = true;
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

        private void txtMRNNum_Leave(object sender, EventArgs e)
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
                        this.SetSelectedInventoryRequestData(_selectedInventoryRequest);
                        return;
                    }

                MessageBox.Show("Request no is invalid", "Request No", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtMRNNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtMRNNum_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnSearchMRN.Focus();
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

        private void txtRequest_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.IsSearchEnter = true;
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
                        this.SetSelectedInventoryRequestData(_selectedInventoryRequest);
                        return;
                    }

                MessageBox.Show("Request no is invalid", "Request No", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtRequest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtRequest_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnSearchMRN.Focus();
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to approve this MRN", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                //Int32 result = CHNLSVC.CustService.Update_ReqHeaderStatus("A", BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtRequest.Text.Trim());
                apptoryRequestData();
              //  if (result > 0)
                //{
                //    MessageBox.Show("MRN approved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    getSavedItems();
                //    this.Close();
                //    return;
                //}
                //else
                //{
                //    MessageBox.Show("Process terminated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(dgvMRNDetails.SelectedRows[0].Cells[1].Value.ToString())) return;
                Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                BaseCls.GlbReportName = string.Empty;
                GlbReportName = string.Empty;
                _view.GlbReportName = string.Empty;
                _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SMRNRepPrints.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_MRNRepPrints.rpt" : "MRNRepPrints.rpt";
                _view.GlbReportDoc = dgvMRNDetails.SelectedRows[0].Cells[1].Value.ToString();
                _view.Show();
                _view = null;
            }
            catch (Exception ex)
            {
                //this.Cursor = Cursors.Default; SystemErrorMessage(ex); 
                MessageBox.Show("Please select MRN number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void gvItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtItem.Text))
            {
                if (!CHNLSVC.Inventory.IsUOMDecimalAllow(txtItem.Text.Trim()) && txtQty.Text.Contains('.'))
                {
                    MessageBox.Show("Item is not allowed decimal quntities.", "MRN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Clear();
                    txtQty.Focus();
                }
            }
        }

        #endregion Events

        #region Methods

        private void SystemErrorMessage(Exception ex)
        {
            CHNLSVC.CloseChannel();
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        private void fillItemStatus()
        {
            DataTable _tbl = CacheLayer.Get<DataTable>(CacheLayer.Key.CompanyItemStatus.ToString());
            var _s = (from L in _tbl.AsEnumerable()
                      select new
                      {
                          MIS_DESC = L.Field<string>("MIS_DESC"),
                          MIC_CD = L.Field<string>("MIC_CD")
                      }).ToList();
            var _n = new { MIS_DESC = string.Empty, MIC_CD = string.Empty };
            _s.Insert(0, _n);
            cmbStatus.DataSource = _s;
            cmbStatus.DisplayMember = "MIS_DESC";
            cmbStatus.ValueMember = "MIC_CD";
            cmbStatus.Text = "GOOD";
        }

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
                if (!string.IsNullOrEmpty(_itemdetail.Mi_cd) && _itemdetail.Mi_itm_tp != "V")
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
                    lblumo.Text = _itemdetail.Mi_itm_uom.ToString();
                    _isDecimalAllow = CHNLSVC.Inventory.IsUOMDecimalAllow(_item);


                }

            return _isValid;
        }

        private void DisplayAvailableQty(string _item, Label _avalQty, Label _freeQty, string _status)
        {
            List<InventoryLocation> _inventoryLocation = null;
            if (string.IsNullOrEmpty(_status))
                _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, txtDispatchRequried.Text, _item.Trim(), string.Empty);
            else
                _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, txtDispatchRequried.Text, _item.Trim(), _status);

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

        private void LoadDispatchLocationInventoryBalance(string _item)
        {
            List<InventoryLocation> _lst = CHNLSVC.Inventory.GetInventoryBalanceSCMnSCM2(BaseCls.GlbUserComCode, "", _item, string.Empty);
            if (_lst != null)
                if (_lst.Count > 0)
                {
                    //pnlBalance.Visible = true;
                    gvBalance.DataSource = new DataTable();
                    gvBalance.DataSource = _lst;
                    gvBalance.Focus();
                }
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

                //get job item details_____________
                List<Service_job_Det> _jobItems = CHNLSVC.CustService.GetJobDetails(JobNumber, jobLineNum, BaseCls.GlbUserComCode);
                string _jobItemCode = _jobItems[0].Jbd_itm_cd.ToString();

                if (txtItem.Text.ToString().ToUpper() == _jobItemCode.ToString().ToUpper())
                {
                    MessageBox.Show("Can not request job item in request!", "Job Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }


                //Get existing items details from the grid.

                string _mainItemCode = txtItem.Text.Trim().ToUpper();
                string _itemStatus = cmbStatus.SelectedValue.ToString();
                // string _reservationNo = string.IsNullOrEmpty(txtReservation.Text.Trim()) ? string.Empty : txtReservation.Text.Trim();
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
                                //_inventoryRequestItem.Itri_res_no = _reservationNo;
                                _inventoryRequestItem.Itri_note = _remarksText;
                                _inventoryRequestItem.Itri_qty = _subItemQty;
                                _inventoryRequestItem.Itri_app_qty = _subItemQty;

                                //Add Main item details.
                                _inventoryRequestItem.Itri_mitm_cd = _mainItemCode;
                                _inventoryRequestItem.Itri_mitm_stus = _itemStatus;
                                _inventoryRequestItem.Itri_mqty = _mainItemQty;

                                _inventoryRequestItem.Itri_job_no = JobNumber;
                                _inventoryRequestItem.Itri_job_line = jobLineNum;
                                _inventoryRequestItem.Mi_itm_uom = lblumo.Text.ToString();
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

                        _inventoryRequestItem.Itri_itm_stus = _itemStatus;
                        //_inventoryRequestItem.Itri_res_no = _reservationNo;
                        _inventoryRequestItem.Itri_note = _remarksText;
                        _inventoryRequestItem.Itri_qty = _mainItemQty;
                        _inventoryRequestItem.Itri_app_qty = _mainItemQty;

                        //Add Main item details.
                        _inventoryRequestItem.Itri_mitm_cd = _mainItemCode;
                        _inventoryRequestItem.Itri_mitm_stus = _itemStatus;
                        _inventoryRequestItem.Itri_mqty = 0;

                        _inventoryRequestItem.Itri_job_no = JobNumber;
                        _inventoryRequestItem.Itri_job_line = jobLineNum;

                        _inventoryRequestItem.Mi_itm_uom = lblumo.Text.ToString();

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
                    toolStrip1.Focus();
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

        private void ClearLayer2()
        {
            txtItem.Clear();
            //txtReservation.Clear();
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

            //txtReservation.Text = _inventoryRequestItem.Itri_res_no;
            txtQty.Text = _inventoryRequestItem.Itri_qty.ToString();
            txtItmRemark.Text = _inventoryRequestItem.Itri_note;

            _invReqItemList.RemoveAll(x => x.Itri_mitm_cd == _mainItem && x.Itri_itm_stus == _status);
            gvItem.DataSource = new List<InventoryRequestItem>();
            gvItem.DataSource = _invReqItemList;
            txtItem.Focus();
            if (BaseCls.GlbUserComCode=="AEC")
            {
                button1.Visible = true;
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


                // 29-01-2015 Nadeeka (Blocked to mrn for supplier warranty claim jobs)
                List<Service_job_Det> _jobdetList = CHNLSVC.CustService.getSupplierClaimRequestMRN(BaseCls.GlbUserComCode, JobNumber, jobLineNum);
              
             
                if (_jobdetList!=null && _jobdetList.Count > 0)
                {
                    MessageBox.Show("Can't raise MRN, Supplier warranty requested for the job # : " + JobNumber, "Supplier warranty claim", MessageBoxButtons.OK, MessageBoxIcon.Information); txtItem.Focus(); return; 
   
                }


                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                InventoryRequest _inventoryRequest = new InventoryRequest();
                //Fill the InventoryRequest header & footer data.
                _inventoryRequest.Itr_com = BaseCls.GlbUserComCode;
                _inventoryRequest.Itr_req_no = GetRequestNo();
                _inventoryRequest.Itr_tp = "MRN";
                _inventoryRequest.Itr_sub_tp = "SCV";
                _inventoryRequest.Itr_loc = _masterLocation;
                _inventoryRequest.Itr_ref = string.Empty;
                _inventoryRequest.Itr_dt = Convert.ToDateTime(dtpRequestDate.Text);
                _inventoryRequest.Itr_exp_dt = Convert.ToDateTime(txtRequriedDate.Text);

                _inventoryRequest.Itr_stus = "S";  //P - Pending , A - Approved.
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10800))
                {
                    _inventoryRequest.Itr_stus = "A";  //P - Pending , A - Approved.
                }
                _inventoryRequest.Itr_job_no = JobNumber;  //Invoice No.
                _inventoryRequest.Itr_bus_code = string.Empty;  //Customer Code.
                _inventoryRequest.Itr_note = txtRemark.Text;
                _inventoryRequest.Itr_issue_from = txtDispatchRequried.Text;
                _inventoryRequest.Itr_rec_to = _masterLocation;
                _inventoryRequest.Itr_direct = 0;
                _inventoryRequest.Itr_country_cd = string.Empty;  //Country Code.
                _inventoryRequest.Itr_town_cd = string.Empty;     //Town Code.
                _inventoryRequest.Itr_cur_code = string.Empty;    //Currency Code.
                _inventoryRequest.Itr_exg_rate = 0;              //Exchange rate.
                //_inventoryRequest.Itr_collector_id = string.IsNullOrEmpty(txtNIC.Text) ? string.Empty : txtNIC.Text.Trim();
                //_inventoryRequest.Itr_collector_name = string.IsNullOrEmpty(txtCollecterName.Text) ? string.Empty : txtCollecterName.Text.Trim();
                _inventoryRequest.Itr_act = 1;
                _inventoryRequest.Itr_cre_by = BaseCls.GlbUserID;
                _inventoryRequest.Itr_session_id = BaseCls.GlbUserSessionID;
                _inventoryRequest.Itr_issue_com = BaseCls.GlbUserComCode;
                _inventoryRequest.Itr_job_line = jobLineNum;
                //Added By Udaya 07/Nov/2017
                _inventoryRequest.TMP_SEND_MAIL = true;//control MRN approve mail send
                _inventoryRequest.Itr_gran_app_by = BaseCls.GlbUserID;
                _inventoryRequest.Itr_mod_dt = DateTime.Now;

                _inventoryRequest.InventoryRequestItemList = _inventoryRequestItemList;
                int rowsAffected = 0;
                string _docNo = string.Empty;

                if (string.IsNullOrEmpty(txtRequest.Text))
                {
                    rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, GenerateMasterAutoNumber(), out _docNo);
                    if (rowsAffected > 0)
                    {
                        if (MessageBox.Show("Inventory Request Document Successfully saved. " + _docNo + "Do you want to print?", "Save...", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                        {
                            if (BaseCls.GlbDefSubChannel == "MCS")
                            { BaseCls.GlbReportDirectPrint = 1; }
                            else
                            { BaseCls.GlbReportDirectPrint = 0; }

                            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                            BaseCls.GlbReportName = string.Empty;
                            GlbReportName = string.Empty;
                            _view.GlbReportName = string.Empty;
                            _view.GlbReportName = "MRNRepPrints.rpt";
                            _view.GlbReportDoc = _docNo;
                            BaseCls.GlbReportJobNo = JobNumber; //kapila 20/6/2015

                            Reports.Service.clsServiceRep objSvc = new Reports.Service.clsServiceRep();
                            if (BaseCls.GlbReportDirectPrint == 1)
                            {
                                _view.MRN_print();
                                Reports.Sales.ReportViewer _viewsale = new Reports.Sales.ReportViewer();
                                _view._mrnReport1.PrintOptions.PrinterName = _viewsale.GetDefaultPrinter();
                                MessageBox.Show("Please check whether printer load the MRN documents and press ok!\n", "Load Documents.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _view._mrnReport1.PrintToPrinter(1, false, 0, 0);
                            }
                            else
                            {
                                _view.TopMost = true;
                                _view.Show();
                                _view = null;
                            }
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

        private void getSavedItems()
        {
            DataTable dtTemp = CHNLSVC.CustService.GetMRNItemsByJobline(BaseCls.GlbUserComCode, JobNumber, jobLineNum);
            dgvMRNDetails.DataSource = dtTemp;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (m.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = m.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }
            base.WndProc(ref m);
        }

        private void SetSelectedInventoryRequestData(InventoryRequest _selectedInventoryRequest)
        {
            if (_selectedInventoryRequest != null)
            {
                //Set Header details.
                //////BindRequestSubTypesDDLData(ddlRequestSubType);
                //////ddlRequestSubType.SelectedValue = _selectedInventoryRequest.Itr_sub_tp;
                //////txtDispatchRequried.Text = _selectedInventoryRequest.Itr_issue_from;
                //////txtRequestDate.Text = _selectedInventoryRequest.Itr_dt.ToString("dd/MMM/yyyy");
                //////txtRequriedDate.Text = _selectedInventoryRequest.Itr_exp_dt.ToString("dd/MMM/yyyy");
                ////////txtInvoiceNo.Text = _selectedInventoryRequest.Itr_job_no;
                //////txtNIC.Text = _selectedInventoryRequest.Itr_collector_id;
                //////txtCollecterName.Text = _selectedInventoryRequest.Itr_collector_name;
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
                if (_selectedInventoryRequest.Itr_stus == "C")
                {
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                }


                //btnSave.Enabled = (_selectedInventoryRequest.Itr_stus.ToUpper().Equals("P")) ? true : false;
                //btnCancel.Enabled = (_selectedInventoryRequest.Itr_stus.ToUpper().Equals("P")) ? true : false;
            }
        }

        private void CancelSelectedRequest()
        {
            try
            {
                if (string.IsNullOrEmpty(txtRequest.Text))
                {
                    MessageBox.Show("Please select request before cancel.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (DateTime.Compare(Convert.ToDateTime(dtpRequestDate.Text.Trim()), DateTime.Now.Date) != 0)
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

                int result = CHNLSVC.Inventory.UpdateInventoryRequestStatus(_inputInvReq);
                result = CHNLSVC.CustService.Update_ReqHeaderStatus("C", BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtRequest.Text);

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

        #endregion Methods

        private void txtEstimate_TextChanged(object sender, EventArgs e)
        {

        }
        private void apptoryRequestData()
        {

            try
            {
                int _count = 1;
                _invReqItemList.ForEach(x => x.Itri_line_no = _count++);
                _invReqItemList.ForEach(X => X.Itri_app_qty = X.Itri_qty);
                _invReqItemList.ForEach(X => X.Itri_mitm_stus = X.Itri_itm_stus);
                _invReqItemList.ForEach(X => X.Itri_job_no = JobNumber);
                _invReqItemList.ForEach(X => X.Itri_job_line = jobLineNum);
                //_invReqItemList.Where(x => string.IsNullOrEmpty(x.Itri_mitm_cd)).ToList().
                //    ForEach(y => 
                //    { y.Itri_mitm_cd = y.Itri_itm_cd;
                //        y.Itri_job_no = JobNumber;
                //        y.Itri_job_line = jobLineNum;
                //    } );
                _invReqItemList.Where(x => string.IsNullOrEmpty(x.Itri_mitm_cd)).ToList().ForEach(y => y.Itri_mitm_cd = y.Itri_itm_cd);

                List<InventoryRequestItem> _inventoryRequestItemList = _invReqItemList;
                if ((_inventoryRequestItemList == null) || (_inventoryRequestItemList.Count == 0))
                { MessageBox.Show("Please add items to List.", "No Item", MessageBoxButtons.OK, MessageBoxIcon.Information); txtItem.Focus(); return; }


                // 29-01-2015 Nadeeka (Blocked to mrn for supplier warranty claim jobs)
                List<Service_job_Det> _jobdetList = CHNLSVC.CustService.getSupplierClaimRequestMRN(BaseCls.GlbUserComCode, JobNumber, jobLineNum);


                if (_jobdetList != null && _jobdetList.Count > 0)
                {
                    MessageBox.Show("Can't raise MRN, Supplier warranty requested for the job # : " + JobNumber, "Supplier warranty claim", MessageBoxButtons.OK, MessageBoxIcon.Information); txtItem.Focus(); return;

                }


                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                InventoryRequest _inventoryRequest = new InventoryRequest();
                //foreach (DataGridViewRow row in gvItem.Rows)
                //{
                // invHdrDoShedule.sid_seq_no = Convert.ToInt32(row.Cells["Sad_seq_no"].Value.ToString());
                    _inventoryRequest.Itr_com = BaseCls.GlbUserComCode;
                    _inventoryRequest.Itr_req_no = GetRequestNo();
                 
                    _inventoryRequest.Itr_tp = "MRN";
                    _inventoryRequest.Itr_sub_tp = "SCV";
                    _inventoryRequest.Itr_loc = _masterLocation;
                    _inventoryRequest.Itr_ref = string.Empty;
                    _inventoryRequest.Itr_dt = Convert.ToDateTime(dtpRequestDate.Text);
                    _inventoryRequest.Itr_exp_dt = Convert.ToDateTime(txtRequriedDate.Text);


                    _inventoryRequest.Itr_stus = "A";  //P - Pending , A - Approved.

                    _inventoryRequest.Itr_job_no = JobNumber;  //Invoice No.
                    _inventoryRequest.Itr_bus_code = string.Empty;  //Customer Code.
                    _inventoryRequest.Itr_note = txtRemark.Text;
                    _inventoryRequest.Itr_issue_from = txtDispatchRequried.Text;
                    _inventoryRequest.Itr_rec_to = _masterLocation;
                    _inventoryRequest.Itr_direct = 0;
                    _inventoryRequest.Itr_country_cd = string.Empty;  //Country Code.
                    _inventoryRequest.Itr_town_cd = string.Empty;     //Town Code.
                    _inventoryRequest.Itr_cur_code = string.Empty;    //Currency Code.
                    _inventoryRequest.Itr_exg_rate = 0;              //Exchange rate.
                    //_inventoryRequest.Itr_collector_id = string.IsNullOrEmpty(txtNIC.Text) ? string.Empty : txtNIC.Text.Trim();
                    //_inventoryRequest.Itr_collector_name = string.IsNullOrEmpty(txtCollecterName.Text) ? string.Empty : txtCollecterName.Text.Trim();
                    _inventoryRequest.Itr_act = 1;
                    _inventoryRequest.Itr_cre_by = BaseCls.GlbUserID;
                    _inventoryRequest.Itr_session_id = BaseCls.GlbUserSessionID;
                    _inventoryRequest.Itr_issue_com = BaseCls.GlbUserComCode;
                    _inventoryRequest.Itr_job_line = jobLineNum;
                    //Added By Udaya 07/Nov/2017
                    _inventoryRequest.TMP_SEND_MAIL = true;//control MRN approve mail send
                    _inventoryRequest.Itr_gran_app_by = BaseCls.GlbUserID;
                    _inventoryRequest.Itr_mod_dt = DateTime.Now;
                //}
                _inventoryRequest.InventoryRequestItemList = _inventoryRequestItemList;
           
                string _docNo = string.Empty;

                
                  //  rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, GenerateMasterAutoNumber(), out _docNo);
                Int32 result = CHNLSVC.CustService.Update_Approve_MRN("A", BaseCls.GlbUserID, BaseCls.GlbUserComCode,txtRequest.Text.Trim(),_inventoryRequest);
                if (result > 0)
                    {
                        MessageBox.Show("MRN approved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getSavedItems();
                        this.Close();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Process Terminated", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
               
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                CHNLSVC.CloseChannel();
                return;
            }
        }
    }
}