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
    public partial class Service_CanibalizeMainUnit : Base
    {
        private string _selectedstatus = ItemStatus.GOD.ToString();
        private string _selectedLoc = string.Empty;
        private bool IsAllowChangeStatus = false;
        private enum ItemStatus
        {
            GOD
        }

     
        Int32 _serId = 0;
        private List<InventorySubSerialMaster> _SubSerialList = null;
        private void SystemWarnningMessage(string _msg, string _title)
        { this.Cursor = Cursors.Default; MessageBox.Show(_msg, _title, MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        public Service_CanibalizeMainUnit()
        {
            InitializeComponent();
            HangGridComboBoxItemStatus();
            txtItem.Focus();
            LoadUserPermission();
            dtDate.Value = DateTime.Now.Date;
           // dgvSelect.Columns["jbd_ser1"].DefaultCellStyle.BackColor = Color.Yellow;
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.MainItem:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.AvailableSerial:
                //    {
                //        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + txtItem.Text.ToUpper() + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.SerialAvb:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + txtItem.Text.ToUpper() + seperator);
                        break;

                    }

                default:
                    break;
            }
            return paramsText.ToString();
        }





        #region data load methods

        private void LoadUserPermission()
        { 
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10104))
                IsAllowChangeStatus = true;
            else
                IsAllowChangeStatus = false;
        }
        private void HangGridComboBoxItemStatus()
        {
             
            DataTable _tbl = CHNLSVC.Inventory.GetAllCompanyStatus(BaseCls.GlbUserComCode);
            gvStatus.AutoGenerateColumns = false;
            gvStatus.DataSource = _tbl;

            List<SystemUserLoc> _ListLoc = CHNLSVC.Security.GetUserLoc(BaseCls.GlbUserID, BaseCls.GlbUserComCode);
            gvLoc.AutoGenerateColumns = false;
            gvLoc.DataSource = _ListLoc;


        }
        /// <summary>
        /// Load the item details
        /// </summary>
        /// 
        private void LoadItemDetails()
        {
            if (txtItem.Text != "")
            {
                FF.BusinessObjects.MasterItem _item = (FF.BusinessObjects.MasterItem)CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.ToUpper());
                if (_item != null && _item.Mi_is_ser1 == -1)
                {
                    MessageBox.Show("Item not allow to Canibalize", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtItem.Text = "";
                    txtItem.Focus();
                    txtItemDesn.Text = "";
                    txtModel.Text = "";
                    txtBrand.Text = "";
                    return;
                }
                if (_item.Mi_is_scansub == true)
                {
                    MessageBox.Show("Item not allow to Canibalize", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtItem.Text = "";
                    txtItem.Focus();
                    txtItemDesn.Text = "";
                    txtModel.Text = "";
                    txtBrand.Text = "";
                    return;
                }
                //if (_item.Mi_itm_tp!="M")
                //{
                //    MessageBox.Show("Item not allow to Canibalize", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    txtItem.Text = "";
                //    txtItem.Focus();
                //    txtItemDesn.Text = "";
                //    txtModel.Text = "";
                //    txtBrand.Text = "";
                //    return;
                //}
                if (_item != null)
                {
                    txtItemDesn.Text = _item.Mi_shortdesc;
                    txtBrand.Text = _item.Mi_brand;
                    txtModel.Text = _item.Mi_model;
                }
                else
                {
                    MessageBox.Show("Invalid Item Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtItem.Text = "";
                    txtItem.Focus();
                    txtItemDesn.Text = "";
                    txtModel.Text = "";
                    txtBrand.Text = "";
                    return;
                }
            }
        }


        private int LoadSubSerial()
        {

            _SubSerialList = CHNLSVC.Inventory.GetAvailablesubSerilsMain(_serId);
            if (_SubSerialList != null)
            {
                foreach (InventorySubSerialMaster _ser in _SubSerialList)
                {
                    _ser.Irsms_loc_chg = BaseCls.GlbUserDefLoca;
                }

                btnSave.Enabled =true;
                dgvSelect.AutoGenerateColumns = false;
                dgvSelect.DataSource = new List<InventorySubSerialMaster>();
                dgvSelect.DataSource = _SubSerialList;
            }
            return _SubSerialList.Count;

        }
        #endregion 
        private void btnsrch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                btnClear_Click(null, null);
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MainItem);
                DataTable _result = CHNLSVC.CommonSearch.GetMainItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem;
                _CommonSearch.ShowDialog();
                txtItem.Select();
                if (txtItem.Text != "")
                    LoadItemDetails();
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

        private void btnsrch_ser_Click(object sender, EventArgs e)
        {
            try
            {
               // btnClear_Click(null, null);

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SerialAvb);
                DataTable _result = CHNLSVC.CommonSearch.GetAvailableSeriaSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSer;
                _CommonSearch.ShowDialog();
                txtSer.Select();
                txtSer.Focus();
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

        private void btnsrch_war_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSer_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSer_Leave(object sender, EventArgs e)
        {
            try
            {
                 
                if (txtSer.Text != "")
                {
                    LoadSerialData();
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
        #region data load methods
        private void LoadSerialData()
        {
            lblFree.Text = "";
            lblRes.Text = "";
            DataTable _dtSer = new DataTable();  
    
            if (txtItem.Text == "")
            {
                _dtSer = CHNLSVC.Inventory.GetSerialDetailsBySerialwithoutItem(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtSer.Text);
                if (_dtSer.Rows.Count > 1)
                {
                    pnlItem.Visible = true;
                    dgvItem.AutoGenerateColumns = false;
                    dgvItem.DataSource = new DataTable();
                    dgvItem.DataSource = _dtSer;
                }
                else if (_dtSer.Rows.Count == 1)
                {
                    txtItem.Text = _dtSer.Rows[0]["ins_itm_cd"].ToString();
             
                        LoadItemDetails();
                }
                else
                {
                    MessageBox.Show("Serial not available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSer.Text = "";
                    txtSer.Focus();
                    return;
                }

            }

            if (txtItem.Text != "")
            {
                MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
               // if (_item.Mi_is_ser1 == 1 && _item.Mi_itm_tp == "M" && _item.Mi_is_scansub == true)
                if (_item.Mi_is_ser1 == 1   && _item.Mi_is_scansub == false)
                {

                    if (!string.IsNullOrEmpty(txtSer.Text))
                    {

                        if (txtItem.Text == "")
                        {


                        }
                        else
                        {
                            _dtSer = CHNLSVC.Inventory.GetSerialDetailsBySerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text, txtSer.Text);
                        }

                        if (_dtSer.Rows.Count > 0)
                        {

                            _serId = Convert.ToInt32(_dtSer.Rows[0]["ins_ser_id"].ToString());
                            lblStus.Text = _dtSer.Rows[0]["ins_itm_stus"].ToString();
                            txtWarr.Text = _dtSer.Rows[0]["ins_warr_no"].ToString();
                            int i =LoadSubSerial();

                            if (i==0)
                            {
                                MessageBox.Show("Main item canibalise setup not available. Please contact Inventory Department.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtSer.Text = "";
                                txtSer.Focus();
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid serial.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSer.Text = "";
                            txtSer.Focus();
                            return;
                        }

                        List<InventoryLocation> _StockBal = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text, lblStus.Text);
                        lblFree.Text = Convert.ToString(_StockBal[0].Inl_free_qty);
                        lblRes.Text = Convert.ToString(_StockBal[0].Inl_res_qty);
                    }

                }
            }
 
        }
        #endregion

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

     
        private void txtSer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtManual.Focus();
            else if (e.KeyCode == Keys.F2)
              btnsrch_ser_Click(null, null);

        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSer.Focus();
            else if (e.KeyCode == Keys.F2 )

            btnsrch_Item_Click(null, null);
        }

        private void txtManual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRem.Focus();

        }

        private void txtRem_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtManual_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to save this ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                btnClear_Click(null, null);
                return;
            }
            bool _allowCurrentTrans = false; 
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty.ToUpper().ToString(), this.GlbModuleName, dtDate, label4, dtDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (dtDate.Value.Date != DateTime.Now.Date)
                    {
                        dtDate.Enabled = true;
                        MessageBox.Show("SelecteSd date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtDate.Focus();
                        return;
                    }
                }
                else
                {
                    dtDate.Enabled = true;
                    MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtDate.Focus();
                    return;
                }
            }

            //decimal _per = 0;
            //foreach (InventorySubSerialMaster item in _SubSerialList)
            //{
            //    _per = _per + item.Irsms_cost_per;
            //}
            //if (_per!=100)
            //{
            //    Cursor.Current = Cursors.Default;
            //    MessageBox.Show("Invalid Cost percentage! " + txtSer.Text, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}


            foreach (InventorySubSerialMaster item in _SubSerialList)
            {
                FF.BusinessObjects.MasterItem _item = (FF.BusinessObjects.MasterItem)CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, item.Irsms_itm_cd);
            if (_item != null && _item.Mi_is_ser1 == 1)
            {
                if (item.Irsms_sub_ser == "N/A")
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("This item is serialize enter,  serial! " + item.Irsms_itm_cd, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }

                 
            }
         

            Cursor.Current = Cursors.WaitCursor;
            List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
            List<InventoryHeader> inHeaderList = new List<InventoryHeader>();
            string documntNo = "";
            Int32 result = -99;
            #region Fill InventoryHeader
            InventoryHeader outHeader = new InventoryHeader();
            DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            foreach (DataRow r in dt_location.Rows)
            {
                // Get the value of the wanted column and cast it to string
                outHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                if (System.DBNull.Value != r["ML_CATE_2"])
                {
                    outHeader.Ith_channel = (string)r["ML_CATE_2"];
                }
                else
                {
                    outHeader.Ith_channel = string.Empty;
                }
            }
            outHeader.Ith_acc_no = "STOCK_ADJ";
            outHeader.Ith_anal_1 = "";
            outHeader.Ith_anal_2 = "";
            outHeader.Ith_anal_3 = "";
            outHeader.Ith_anal_4 = "";
            outHeader.Ith_anal_5 = "";
            outHeader.Ith_anal_6 = 0;
            outHeader.Ith_anal_7 = 0;
            outHeader.Ith_anal_8 = DateTime.MinValue;
            outHeader.Ith_anal_9 = DateTime.MinValue;
            outHeader.Ith_anal_10 = false;
            outHeader.Ith_anal_11 = false;
            outHeader.Ith_anal_12 = false;
            outHeader.Ith_bus_entity = "";
            outHeader.Ith_cate_tp = "CANB";
            outHeader.Ith_com = BaseCls.GlbUserComCode;
            outHeader.Ith_com_docno = "";
            outHeader.Ith_cre_by = BaseCls.GlbUserID;
            outHeader.Ith_cre_when = DateTime.Now;
            outHeader.Ith_del_add1 = "";
            outHeader.Ith_del_add2 = "";
            outHeader.Ith_del_code = "";
            outHeader.Ith_del_party = "";
            outHeader.Ith_del_town = "";
            outHeader.Ith_direct = false;
            outHeader.Ith_doc_date = dtDate.Value.Date;
            outHeader.Ith_doc_no = string.Empty;
            outHeader.Ith_doc_tp = "ADJ";
            outHeader.Ith_doc_year = dtDate.Value.Date.Year;
            outHeader.Ith_entry_no = "";
            outHeader.Ith_entry_tp = "CANB";
            outHeader.Ith_git_close = true;
            outHeader.Ith_git_close_date = DateTime.MinValue;
            outHeader.Ith_git_close_doc = string.Empty;
            outHeader.Ith_isprinted = false;
            outHeader.Ith_is_manual = false;
            outHeader.Ith_job_no = string.Empty;
            outHeader.Ith_loading_point = string.Empty;
            outHeader.Ith_loading_user = string.Empty;
            outHeader.Ith_loc = BaseCls.GlbUserDefLoca;
            outHeader.Ith_manual_ref = txtManual.Text.Trim();
            outHeader.Ith_mod_by = BaseCls.GlbUserID;
            outHeader.Ith_mod_when = DateTime.Now;
            outHeader.Ith_noofcopies = 0;
            outHeader.Ith_oth_loc = string.Empty;
            outHeader.Ith_oth_docno = "N/A";
            outHeader.Ith_remarks = txtRem.Text;
            //outHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
            outHeader.Ith_session_id = BaseCls.GlbUserSessionID;
            outHeader.Ith_stus = "A";
            outHeader.Ith_sub_tp = "NOR";
            outHeader.Ith_vehi_no = string.Empty;
            #endregion
            MasterAutoNumber masterAuto = new MasterAutoNumber();
            #region Fill MasterAutoNumber
            masterAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "ADJ";
            masterAuto.Aut_number = 5;//what is Aut_number
            masterAuto.Aut_start_char = "ADJ";
            masterAuto.Aut_year = null;
            #endregion

            ReptPickSerials _serDet = CHNLSVC.Inventory.GetReservedByserialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, null, txtItem.Text, _serId);
            if (_serDet == null)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Serial not available! " + txtSer.Text, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (InventorySubSerialMaster item in _SubSerialList)
            {

            }

            reptPickSerialsList.Add(_serDet);

            #region Save Adj+ / Adj-

            result = CHNLSVC.CustService.Save_ItemCanibalize(masterAuto, outHeader, reptPickSerialsList, _SubSerialList,"M", out documntNo);


            if (result != -99 && result >= 0)
            {
                Cursor.Current = Cursors.Default;
                if (MessageBox.Show("Successfully Saved! Document No : " + documntNo + "\nDo you want to print this?", "Process Completed  ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {

                }
                btnClear_Click(null, null);
            }
            else
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(documntNo, "Process Terminated ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _SubSerialList = null;
            dgvSelect.AutoGenerateColumns = false;
            dgvSelect.DataSource = new List<InventorySubSerialMaster>();
            dgvSelect.DataSource = _SubSerialList;
            txtRem.Text = "";
            txtManual.Text = "";
            txtItem.Text = "";
            txtSer.Text = "";
            txtItemDesn.Text = "";
            txtWarr.Text = "";
            txtModel.Text = "";
            txtBrand.Text = "";
            lblRes.Text = "";
            lblStus.Text = "";
            lblFree.Text = "";
        }

        private void dgvItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvItem.Rows.Count > 0 && e.RowIndex != -1)
            {
                txtItem.Text = dgvItem.Rows[e.RowIndex].Cells["colItem"].Value.ToString();
                pnlItem.Visible = false;
            }
          
        }

        private void txtItem_Leave(object sender, EventArgs e)
        {
 
            if (txtItem.Text != "")
                LoadItemDetails();
        }
      
        private void btnCloseStatus_Click(object sender, EventArgs e)
        {
            _selectedstatus = ItemStatus.GOD.ToString();
            pnlStatus.Visible = false;
        }

        private void gvStatus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _selectedstatus = string.Empty;
                if (gvStatus.RowCount > 0)
                {
                    if (dgvSelect.RowCount > 0)
                    {
                        Int16  selectedser =Convert.ToInt16(  dgvSelect.SelectedCells[9].Value.ToString());


                        _selectedstatus = Convert.ToString(gvStatus.Rows[e.RowIndex].Cells[0].Value);
                        if (_SubSerialList != null && _SubSerialList.Count > 0)
                        {
                             

                            _SubSerialList.Where(w => w.Irsms_ser_line == selectedser).ToList().ForEach(s => s.Irsms_itm_sts_chg = _selectedstatus);
                            dgvSelect.AutoGenerateColumns = false;
                            dgvSelect.DataSource = new List<ReptPickSerials>();
                            dgvSelect.DataSource = _SubSerialList;




                            // _InvDetailList.ForEach(x => x.Sad_itm_stus = _selectedstatus);
                        }
                        pnlStatus.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void gvStatus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvSelect_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       
        private void dgvSelect_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.RowIndex != -1)
            {
                if (IsAllowChangeStatus)
                {
                    pnlStatus.Visible = true;

                }
                else
                {
                    MessageBox.Show("you don't have permission to Change status " + 10104, "Canibalize", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
                }
            }

            if (e.ColumnIndex == 8 && e.RowIndex != -1)
            {
                if (IsAllowChangeStatus)
                {
                    pnlLoc.Visible = true;

                }
                else
                {
                    MessageBox.Show("you don't have permission to Change status " + 10104, "Canibalize", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
                }
            }
        }

        private void btnCloseLoc_Click(object sender, EventArgs e)
        {
            pnlLoc.Visible = false;
        }

        private void gvLoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _selectedstatus = string.Empty;
                if (gvLoc.RowCount > 0)
                {
                    if (dgvSelect.RowCount > 0)
                    {
                        //String selectedser = dgvSelect.SelectedCells[5].Value.ToString();
                        Int16 selectedser = Convert.ToInt16(dgvSelect.SelectedCells[9].Value.ToString());

                        _selectedLoc = Convert.ToString(gvLoc.Rows[e.RowIndex].Cells[0].Value);
                        if (_SubSerialList != null && _SubSerialList.Count > 0)
                        {


                            _SubSerialList.Where(w => w.Irsms_ser_line == selectedser).ToList().ForEach(s => s.Irsms_loc_chg = _selectedLoc);
                            dgvSelect.AutoGenerateColumns = false;
                            dgvSelect.DataSource = new List<ReptPickSerials>();
                            dgvSelect.DataSource = _SubSerialList;




                            // _InvDetailList.ForEach(x => x.Sad_itm_stus = _selectedstatus);
                        }
                        pnlLoc.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtSer_DoubleClick(object sender, EventArgs e)
        {
            btnsrch_ser_Click(null, null);

        }

        private void txtItem_DoubleClick(object sender, EventArgs e)
        {

            btnsrch_Item_Click(null, null);
        }

        private void dgvSelect_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataTable _dtSer = new DataTable(); if (e.ColumnIndex == 5 && e.RowIndex != -1)
            {

                _dtSer = CHNLSVC.Inventory.GetSerialDetailsBySerialCompany(BaseCls.GlbUserComCode, dgvSelect.SelectedCells[0].Value.ToString(), dgvSelect.SelectedCells[5].Value.ToString());
                if (_dtSer.Rows.Count > 1)
                {
                    Int16 selectedser = Convert.ToInt16(dgvSelect.SelectedCells[9].Value.ToString());
                 
                    if (_SubSerialList != null && _SubSerialList.Count > 0)
                    {
                        _SubSerialList.Where(w => w.Irsms_ser_line == selectedser).ToList().ForEach(s => s.Irsms_sub_ser = "N/A");
                        dgvSelect.AutoGenerateColumns = false;
                        dgvSelect.DataSource = new List<ReptPickSerials>();
                        dgvSelect.DataSource = _SubSerialList;
                        // _InvDetailList.ForEach(x => x.Sad_itm_stus = _selectedstatus);
                    }

                    MessageBox.Show("Serial already Exit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                }

                //if (dgvSelect.SelectedCells[5].Value.ToString() != "N/A")
                //{
                //    if ((_SubSerialList.FindAll(x => (x.Irsms_sub_ser == dgvSelect.SelectedCells[5].Value.ToString()))).Count > 0)
                //    {
                //        //MessageBox.Show("This serial alreay exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //        //return;
                //    }
                //}


             
          }
        }

        private void dgvSelect_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5  )
                e.CellStyle.ForeColor  = Color.Maroon;
        }

        private void dgvSelect_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex != -1)
            {
            if (dgvSelect.SelectedCells[5].Value.ToString() != "N/A")
            {
                if ((_SubSerialList.FindAll(x => (x.Irsms_sub_ser == dgvSelect.SelectedCells[5].Value.ToString()))).Count > 0)
                {
                    MessageBox.Show("This serial alreay exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
            }
        }
        }

        private void dgvSelect_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {

                    string type = dgvSelect.SelectedCells[0].Value.ToString();
                        

                        _SubSerialList.RemoveAll(x => x.Irsms_itm_cd== type  );
                        dgvSelect.AutoGenerateColumns = false;
                        dgvSelect.DataSource = new List<InventorySubSerialMaster>();
                        dgvSelect.DataSource = _SubSerialList;

                     
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
