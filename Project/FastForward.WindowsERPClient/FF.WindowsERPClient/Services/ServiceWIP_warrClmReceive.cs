using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceWIP_warrClmReceive : Base
    {
        private string GblJobNum = string.Empty;
        private Int32 GbljobLineNum = -11;
        private List<Service_WCN_Detail> oMainList;
        private MasterItem _itemdetail = null;

        public ServiceWIP_warrClmReceive(string job, Int32 jobLine)
        {
            GblJobNum = job;
            GbljobLineNum = jobLine;
            InitializeComponent();

            dgvItems.AutoGenerateColumns = false;
            dgvItems.Size = new Size(759, 427);
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

        private void ServiceWIP_warrClmReceive_Load(object sender, EventArgs e)
        {
            btnClear_Click(null, null);
        }

        private void getDetails()
        {
            oMainList = new List<Service_WCN_Detail>();
            oMainList = CHNLSVC.CustService.GetSupWarntyClaimReveiedItems(GblJobNum, GbljobLineNum);
            foreach (Service_WCN_Detail item in oMainList)
            {
                item.select = false;
            }
            dgvItems.DataSource = oMainList;
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                    break;
            }

            return paramsText.ToString();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            getDetails();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvItems.DataSource = CHNLSVC.CustService.GetSupWarntyClaimReveiedItems("xx", -1);
            chkSelectAll.Checked = false;
            pnlAddNewRecord.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvItems.Rows.Count > 0)
            {
                for (int i = 0; i < dgvItems.Rows.Count; i++)
                {
                    if (dgvItems.Rows[i].Cells["select"].Value != null && dgvItems.Rows[i].Cells["select"].Value.ToString() != string.Empty && Convert.ToBoolean(dgvItems.Rows[i].Cells["select"].Value.ToString()) == true)
                    {
                        Service_WCN_Detail item = oMainList.Find(x => x.SWD_SEQ_NO == Convert.ToInt32(dgvItems.Rows[i].Cells["swd_seq_no"].Value.ToString()) && x.SWD_LINE == Convert.ToInt32(dgvItems.Rows[i].Cells["swd_line"].Value.ToString()));
                        item.select = true;
                    }
                }
                pnlAddNewRecord.Visible = true;
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (dgvItems.Rows.Count > 0)
            {
                if (chkSelectAll.Checked)
                {
                    for (int i = 0; i < dgvItems.Rows.Count; i++)
                    {
                        dgvItems.Rows[i].Cells["select"].Value = true;
                    }
                }
                else
                {
                    for (int i = 0; i < dgvItems.Rows.Count; i++)
                    {
                        dgvItems.Rows[i].Cells["select"].Value = false;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (oMainList !=null && oMainList.FindAll(x => x.select == true).Count > 0)
            {
                if (MessageBox.Show("Do you want to save?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                else
                {
                    string docNum = string.Empty;
                    Int32 result = CHNLSVC.CustService.Save_ServiceWIP_SuppWarrntyClainReceive(oMainList.FindAll(x => x.select == true), BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbDefaultBin, BaseCls.GlbUserSessionID, out docNum);



                    if (result != -99 && result >= 0)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Successfully Saved!\n" + docNum, "Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnClear_Click(null, null);
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Process Terminated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dgvItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvItems.IsCurrentCellDirty)
            {
                dgvItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            pnlAddNewRecord.Visible = false;
        }

        private void btnVouNo_Click(object sender, EventArgs e)
        {
            txtItemCode_DoubleClick(null, null);
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItemCode.Text))
            {
                MessageBox.Show("please enter item code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtItemCode.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtSerial.Text))
            {
                MessageBox.Show("please enter serial.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSerial.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtQty.Text))
            {
                MessageBox.Show("please enter quantity.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQty.Focus();
                return;
            }

            Service_WCN_Detail item = new Service_WCN_Detail();
            item.SWD_JOBNO = GblJobNum;
            item.SWD_JOBLINE = GbljobLineNum;
            item.SWD_SEQ_NO = -1;
            item.SWD_LINE = -1;
            item.SWD_ITMCD = txtItemCode.Text;
            item.SWD_SER1 = txtSerial.Text;
            item.SWD_QTY = Convert.ToDecimal(txtQty.Text);
            item.PartID = txtPartID.Text;
            item.OEM = txtOEM.Text;
            item.CaseID = txtCaseID.Text;
            item.select = true;
            item.SWD_ITM_STUS = oMainList[0].SWD_ITM_STUS;
            item.SWD_ITM_STUSText = oMainList[0].SWD_ITM_STUSText;
            oMainList.Add(item);
            bindData();

            pnlAddNewRecord.Visible = false;
            btnCancel_Click(null, null);
        }

        private void txtItemCode_DoubleClick(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _commonSearch = null;
            _commonSearch = new CommonSearch.CommonSearch();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_commonSearch.SearchParams, null, null);
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtItemCode;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtItemCode.Select();
            }
            catch (Exception ex)
            { txtItemCode.Clear(); this.Cursor = Cursors.Default; }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtItemCode.Text))
            {
                return;
            }
            if (!LoadItemDetail(txtItemCode.Text.Trim()))
            {
                MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtItemCode.Clear();
                txtItemCode.Focus();
                return;
            }
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSerial.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                txtItemCode_DoubleClick(null, null);
            }
        }

        private void txtSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtQty.Enabled == true)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtQty.Focus();
                }
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtPartID.Focus();
                }
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPartID.Focus();
            }
        }

        private void txtPartID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtOEM.Focus();
            }
        }

        private void txtOEM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCaseID.Focus();
            }
        }

        private void txtCaseID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddItem.Focus();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            foreach (Control item in pnlAddNewRecord.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private bool LoadItemDetail(string _item)
        {
            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
            if (_itemdetail != null)
                if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                {
                    if (_itemdetail.Mi_is_ser1 == 0)
                    {
                        txtSerial.Enabled = false;
                        txtSerial.Text = "N/A";
                        txtQty.Clear();
                        txtQty.Enabled = true;
                    }
                    else
                    {
                        txtSerial.Enabled = true;
                        txtSerial.Clear();
                        txtQty.Text = "1";
                        txtQty.Enabled = false;
                    }
                    _isValid = true;
                }

            return _isValid;
        }

        private void bindData()
        {
            dgvItems.DataSource = new List<Service_WCN_Detail>();
            dgvItems.DataSource = oMainList;
        }

        private void btnCloseFrom_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void InventoryAdjectment()
        //{
        //    foreach (Service_WCN_Detail item in oMainList.FindAll(x => x.select == true))
        //    {
        //        List<Service_job_Det> GetJobDetails = CHNLSVC.CustService.GetJobDetails(item.SWD_JOBNO, item.SWD_JOBLINE, BaseCls.GlbUserComCode);
        //        string itemCode = GetJobDetails[0].Jbd_itm_cd;
        //        string ItemStatus = GetJobDetails[0].Jbd_itm_stus;
        //        Int32 serialID = Convert.ToInt32(GetJobDetails[0].Jbd_ser_id);
        //        string serial1 = GetJobDetails[0].Jbd_ser1;
        //        string _serialNo2 = GetJobDetails[0].Jbd_ser2;

        //        #region Stock Adjectment -
        //        List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
        //        ReptPickSerials PickSerials = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, null, itemCode, serialID);
        //        reptPickSerialsList.Add(PickSerials);

        //        #region Header
        //        InventoryHeader inHeader = new InventoryHeader();
        //        DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
        //        foreach (DataRow r in dt_location.Rows)
        //        {
        //            // Get the value of the wanted column and cast it to string
        //            inHeader.Ith_sbu = (string)r["ML_OPE_CD"];
        //            if (System.DBNull.Value != r["ML_CATE_2"])
        //            {
        //                inHeader.Ith_channel = (string)r["ML_CATE_2"];
        //            }
        //            else
        //            {
        //                inHeader.Ith_channel = string.Empty;
        //            }
        //        }
        //        inHeader.Ith_acc_no = "";
        //        inHeader.Ith_anal_1 = "";
        //        inHeader.Ith_anal_2 = "";
        //        inHeader.Ith_anal_3 = "";
        //        inHeader.Ith_anal_4 = "";
        //        inHeader.Ith_anal_5 = "";
        //        inHeader.Ith_anal_6 = 0;
        //        inHeader.Ith_anal_7 = 0;
        //        inHeader.Ith_anal_8 = DateTime.MinValue;
        //        inHeader.Ith_anal_9 = DateTime.MinValue;
        //        inHeader.Ith_anal_10 = false;
        //        inHeader.Ith_anal_11 = false;
        //        inHeader.Ith_anal_12 = false;
        //        inHeader.Ith_bus_entity = "";
        //        inHeader.Ith_cate_tp = "";
        //        inHeader.Ith_com = BaseCls.GlbUserComCode;
        //        inHeader.Ith_com_docno = "";
        //        inHeader.Ith_cre_by = BaseCls.GlbUserID;
        //        inHeader.Ith_cre_when = DateTime.Now;
        //        inHeader.Ith_del_add1 = "";
        //        inHeader.Ith_del_add2 = "";
        //        inHeader.Ith_del_code = "";
        //        inHeader.Ith_del_party = "";
        //        inHeader.Ith_del_town = "";

        //        inHeader.Ith_direct = false;

        //        inHeader.Ith_doc_date = DateTime.Now.Date;
        //        inHeader.Ith_doc_no = string.Empty;
        //        inHeader.Ith_doc_tp = "AOD";
        //        inHeader.Ith_doc_year = DateTime.Now.Date.Year;
        //        inHeader.Ith_entry_no = "";
        //        inHeader.Ith_entry_tp = "";
        //        inHeader.Ith_git_close = true;
        //        inHeader.Ith_git_close_date = DateTime.MinValue;
        //        inHeader.Ith_git_close_doc = string.Empty;
        //        inHeader.Ith_isprinted = false;
        //        inHeader.Ith_is_manual = false;
        //        inHeader.Ith_job_no = string.Empty;
        //        inHeader.Ith_loading_point = string.Empty;
        //        inHeader.Ith_loading_user = string.Empty;
        //        inHeader.Ith_loc = BaseCls.GlbUserDefLoca;
        //        inHeader.Ith_manual_ref = "";
        //        inHeader.Ith_mod_by = BaseCls.GlbUserID;
        //        inHeader.Ith_mod_when = DateTime.Now;
        //        inHeader.Ith_noofcopies = 0;
        //        inHeader.Ith_oth_loc = string.Empty;
        //        inHeader.Ith_oth_docno = "N/A";
        //        inHeader.Ith_remarks = "";

        //        //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013

        //        inHeader.Ith_session_id = BaseCls.GlbUserSessionID;
        //        inHeader.Ith_stus = "A";
        //        inHeader.Ith_sub_tp = "NOR";
        //        inHeader.Ith_vehi_no = string.Empty;
        //        #endregion

        //        #region AutoNumebr
        //        MasterAutoNumber masterAuto = new MasterAutoNumber();
        //        masterAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
        //        masterAuto.Aut_cate_tp = "LOC";
        //        masterAuto.Aut_direction = null;
        //        masterAuto.Aut_modify_dt = null;
        //        masterAuto.Aut_moduleid = "AOD";
        //        masterAuto.Aut_number = 5;//what is Aut_number
        //        masterAuto.Aut_start_char = "AOD";
        //        masterAuto.Aut_year = DateTime.Today.Year;
        //        #endregion AutoNumebr

        //        string documntNo = "";
        //        Int32 result = CHNLSVC.Inventory.ADJMinus(inHeader, reptPickSerialsList, null, masterAuto, out documntNo);

        //        #endregion

        //        #region Stock Adjectment +

        //        int _itemSerializedStatus = 0;
        //        MasterItem msitem = new MasterItem();
        //        msitem = _inventoryDAL.GetItem(ComCode, item.SWD_ITMCD);
        //        if (msitem.Mi_is_ser1 == 1)
        //        {
        //            _itemSerializedStatus = 1;
        //        }
        //        else
        //        {
        //            _itemSerializedStatus = 0;
        //        }
        //        if (msitem.Mi_is_ser1 == -1)
        //        {
        //            _itemSerializedStatus = -1;
        //        }

        //        if (msitem.Mi_is_ser2 == true)
        //        {
        //            _itemSerializedStatus = 2;
        //        }

        //        if (msitem.Mi_is_ser3 == true)
        //        {
        //            _itemSerializedStatus = 3;
        //        }

        //        List<ReptPickSerials> reptPickSerialsListPlus = new List<ReptPickSerials>();

        //        if (_itemSerializedStatus == 1 || _itemSerializedStatus == 2 || _itemSerializedStatus == 3)
        //        {
        //            #region Serialized
        //            string _serialNo1 = serial1;
        //            string _warrantyno = string.Empty;
        //            int _serID = _inventoryDAL.IsExistInSerialMaster("", itemCode, _serialNo1);
        //            InventorySerialMaster _serIDMst = new InventorySerialMaster();
        //            _serIDMst = _inventoryDAL.GetSerialMasterDetailBySerialID(_serID);

        //            DataTable _dtser1 = _inventoryDAL.CheckSerialAvailability("SERIAL1", itemCode, _serialNo1);
        //            if (_dtser1 != null)
        //            {
        //                if (_dtser1.Rows.Count > 0)
        //                {
        //                    return _effects;
        //                }
        //            }
        //            _dtser1.Dispose();

        //            //if ((_InventoryBLL.IsExistInTempPickSerial(ComCode, _userSeqNo.ToString(), itemCode, _serialNo1)) > 0)
        //            //{
        //            //    MessageBox.Show("Serial no 1 is already in use.", "Duplicating . . .", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            //    return;
        //            //}

        //            if (_itemSerializedStatus == 2)
        //            {
        //                DataTable _dtser2 = _inventoryDAL.CheckSerialAvailability("SERIAL2", itemCode, _serialNo2);
        //                if (_dtser2 != null)
        //                {
        //                    if (_dtser2.Rows.Count > 0)
        //                    {
        //                        return _effects;
        //                    }
        //                }
        //                _dtser2.Dispose();

        //                //if ((CHNLSVC.Inventory.IsExistInTempPickSerial(BaseCls.GlbUserComCode, _userSeqNo.ToString(), "SER_2", _serialNo2)) > 0)
        //                //{
        //                //    MessageBox.Show("Serial no 2 is already in use.", "Duplicating . . .", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                //    return;
        //                //}
        //            }

        //            _warrantyno = _serIDMst.Irsm_warr_no;

        //            //Write to the Picked items serial table.
        //            ReptPickSerials _inputReptPickSerials = new ReptPickSerials();
        //            #region Fill Pick Serial Object
        //            _inputReptPickSerials.Tus_usrseq_no = 1;
        //            _inputReptPickSerials.Tus_doc_no = "1";
        //            _inputReptPickSerials.Tus_seq_no = 0;
        //            _inputReptPickSerials.Tus_itm_line = 0;
        //            _inputReptPickSerials.Tus_batch_line = 0;
        //            _inputReptPickSerials.Tus_ser_line = 0;
        //            _inputReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
        //            _inputReptPickSerials.Tus_com = ComCode;
        //            _inputReptPickSerials.Tus_loc = defLocation;
        //            _inputReptPickSerials.Tus_bin = binCode;
        //            _inputReptPickSerials.Tus_itm_cd = itemCode;
        //            _inputReptPickSerials.Tus_itm_stus = ItemStatus;
        //            //_inputReptPickSerials.Tus_unit_cost = _unitCost;
        //            //_inputReptPickSerials.Tus_unit_price = _unitPrice;
        //            _inputReptPickSerials.Tus_qty = 1;
        //            if (_serID > 0)
        //            { _inputReptPickSerials.Tus_ser_id = _serID; }
        //            else
        //            { _inputReptPickSerials.Tus_ser_id = _inventoryDAL.GetSerialID(); }
        //            _inputReptPickSerials.Tus_ser_1 = _serialNo1;
        //            _inputReptPickSerials.Tus_ser_2 = _serialNo2;
        //            //_inputReptPickSerials.Tus_ser_3 = _serialNo3;
        //            if (string.IsNullOrEmpty(_warrantyno)) _warrantyno = String.Format("{0:dd}", DateTime.Now.Date) + String.Format("{0:MM}", DateTime.Now.Date) + String.Format("{0:yy}", DateTime.Now.Date) + "-" + defLocation + "-P01-" + _inputReptPickSerials.Tus_ser_id.ToString();
        //            _inputReptPickSerials.Tus_warr_no = _warrantyno;
        //            _inputReptPickSerials.Tus_itm_desc = msitem.Mi_longdesc;
        //            _inputReptPickSerials.Tus_itm_model = msitem.Mi_model;
        //            _inputReptPickSerials.Tus_itm_brand = msitem.Mi_brand;
        //            //_inputReptPickSerials.Tus_itm_line = 
        //            _inputReptPickSerials.Tus_cre_by = user;
        //            _inputReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
        //            _inputReptPickSerials.Tus_session_id = BaseCls.GlbUserSessionID;
        //            #endregion


        //            #endregion
        //        }
        //        else if (_itemSerializedStatus == 0)
        //        {
        //            #region Non-serialized
        //            int _actualQty = Convert.ToInt32(item.SWD_QTY);
        //            string _warrantyno = string.Empty;

        //            for (int i = 0; i < _actualQty; i++)
        //            {
        //                //Write to the Picked items serials table.
        //                ReptPickSerials _newReptPickSerials = new ReptPickSerials();
        //                #region Fill Pick Serial Object
        //                _newReptPickSerials.Tus_usrseq_no = 1;
        //                _newReptPickSerials.Tus_doc_no = "1";
        //                _newReptPickSerials.Tus_seq_no = 0;
        //                _newReptPickSerials.Tus_itm_line = 0;
        //                _newReptPickSerials.Tus_batch_line = 0;
        //                _newReptPickSerials.Tus_ser_line = 0;
        //                _newReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
        //                _newReptPickSerials.Tus_com = ComCode;
        //                _newReptPickSerials.Tus_loc = defLocation;
        //                //_newReptPickSerials.Tus_bin = _binCode;
        //                _newReptPickSerials.Tus_itm_cd = itemCode;
        //                _newReptPickSerials.Tus_itm_stus = ItemStatus;
        //                //_newReptPickSerials.Tus_unit_cost = _unitCost;
        //                //_newReptPickSerials.Tus_unit_price = _unitPrice;
        //                _newReptPickSerials.Tus_qty = 1;
        //                _newReptPickSerials.Tus_ser_id = _inventoryDAL.GetSerialID();
        //                _newReptPickSerials.Tus_ser_1 = "N/A";
        //                _newReptPickSerials.Tus_ser_2 = "N/A";
        //                _newReptPickSerials.Tus_ser_3 = "N/A";
        //                _newReptPickSerials.Tus_warr_no = _warrantyno;
        //                _newReptPickSerials.Tus_itm_desc = msitem.Mi_longdesc;
        //                _newReptPickSerials.Tus_itm_model = msitem.Mi_model;
        //                _newReptPickSerials.Tus_itm_brand = msitem.Mi_brand;
        //                //_newReptPickSerials.Tus_itm_line = ItemLineNo;
        //                _newReptPickSerials.Tus_cre_by = user;
        //                _newReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
        //                _newReptPickSerials.Tus_session_id = BaseCls.GlbUserSessionID;
        //                #endregion
        //            }
        //            #endregion
        //        }
        //        else if (_itemSerializedStatus == -1) //(Non serialize decimal Item = -1))
        //        {
        //            #region Non-serialized Decimal Allow
        //            decimal _actualQty = Convert.ToDecimal(item.SWD_QTY);

        //            //Write to the Picked items serials table.
        //            ReptPickSerials _decimalReptPickSerials = new ReptPickSerials();
        //            #region Fill Temp Pick Serial List
        //            _decimalReptPickSerials.Tus_usrseq_no = 1;
        //            _decimalReptPickSerials.Tus_doc_no = "1";
        //            _decimalReptPickSerials.Tus_seq_no = 0;
        //            _decimalReptPickSerials.Tus_itm_line = 0;
        //            _decimalReptPickSerials.Tus_batch_line = 0;
        //            _decimalReptPickSerials.Tus_ser_line = 0;
        //            _decimalReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
        //            _decimalReptPickSerials.Tus_com = ComCode;
        //            _decimalReptPickSerials.Tus_loc = defLocation;
        //            _decimalReptPickSerials.Tus_bin = binCode;
        //            _decimalReptPickSerials.Tus_itm_cd = itemCode;
        //            _decimalReptPickSerials.Tus_itm_stus = ItemStatus;
        //            //_decimalReptPickSerials.Tus_unit_cost = _unitCost;
        //            //_decimalReptPickSerials.Tus_unit_price = _unitPrice;
        //            _decimalReptPickSerials.Tus_qty = _actualQty;
        //            //_decimalReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
        //            _decimalReptPickSerials.Tus_ser_1 = "N/A";
        //            _decimalReptPickSerials.Tus_ser_2 = "N/A";
        //            _decimalReptPickSerials.Tus_ser_3 = "N/A";
        //            _decimalReptPickSerials.Tus_itm_desc = msitem.Mi_longdesc;
        //            _decimalReptPickSerials.Tus_itm_model = msitem.Mi_model;
        //            _decimalReptPickSerials.Tus_itm_brand = msitem.Mi_brand;
        //            //_decimalReptPickSerials.Tus_itm_line = ItemLineNo;
        //            _decimalReptPickSerials.Tus_cre_by = user;
        //            _decimalReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
        //            _decimalReptPickSerials.Tus_session_id = BaseCls.GlbUserSessionID;
        //            #endregion
        //            #endregion
        //        }

        //        InventoryHeader inHeaderPlus = new InventoryHeader();
        //        #region Fill InventoryHeader
        //        DataTable dt_locationPlus = _inventoryDAL.Get_location_by_code(ComCode, defLocation);
        //        foreach (DataRow r in dt_locationPlus.Rows)
        //        {
        //            // Get the value of the wanted column and cast it to string
        //            inHeader.Ith_sbu = (string)r["ML_OPE_CD"];
        //            if (System.DBNull.Value != r["ML_CATE_2"])
        //            {
        //                inHeader.Ith_channel = (string)r["ML_CATE_2"];
        //            }
        //            else
        //            {
        //                inHeader.Ith_channel = string.Empty;
        //            }
        //        }
        //        inHeaderPlus.Ith_acc_no = "STOCK_ADJ";
        //        inHeaderPlus.Ith_anal_1 = "";
        //        inHeaderPlus.Ith_anal_2 = "";
        //        inHeaderPlus.Ith_anal_3 = "";
        //        inHeaderPlus.Ith_anal_4 = "";
        //        inHeaderPlus.Ith_anal_5 = "";
        //        inHeaderPlus.Ith_anal_6 = 0;
        //        inHeaderPlus.Ith_anal_7 = 0;
        //        inHeaderPlus.Ith_anal_8 = DateTime.MinValue;
        //        inHeaderPlus.Ith_anal_9 = DateTime.MinValue;
        //        inHeaderPlus.Ith_anal_10 = false;
        //        inHeaderPlus.Ith_anal_11 = false;
        //        inHeaderPlus.Ith_anal_12 = false;
        //        inHeaderPlus.Ith_bus_entity = "";
        //        inHeaderPlus.Ith_cate_tp = "";
        //        inHeaderPlus.Ith_com = ComCode;
        //        inHeaderPlus.Ith_com_docno = "";
        //        inHeaderPlus.Ith_cre_by = user;
        //        inHeaderPlus.Ith_cre_when = DateTime.Now;
        //        inHeaderPlus.Ith_del_add1 = "";
        //        inHeaderPlus.Ith_del_add2 = "";
        //        inHeaderPlus.Ith_del_code = "";
        //        inHeaderPlus.Ith_del_party = "";
        //        inHeaderPlus.Ith_del_town = "";
        //        inHeaderPlus.Ith_direct = true;
        //        inHeaderPlus.Ith_doc_date = DateTime.Today.Date;
        //        inHeaderPlus.Ith_doc_no = string.Empty;
        //        inHeaderPlus.Ith_doc_tp = "ADJ";
        //        inHeaderPlus.Ith_doc_year = DateTime.Today.Date.Year;
        //        inHeaderPlus.Ith_entry_no = "";
        //        inHeaderPlus.Ith_entry_tp = "";
        //        inHeaderPlus.Ith_git_close = true;
        //        inHeaderPlus.Ith_git_close_date = DateTime.MinValue;
        //        inHeaderPlus.Ith_git_close_doc = string.Empty;
        //        inHeaderPlus.Ith_isprinted = false;
        //        inHeaderPlus.Ith_is_manual = false;
        //        inHeaderPlus.Ith_job_no = string.Empty;
        //        inHeaderPlus.Ith_loading_point = string.Empty;
        //        inHeaderPlus.Ith_loading_user = string.Empty;
        //        inHeaderPlus.Ith_loc = defLocation;
        //        inHeaderPlus.Ith_manual_ref = string.Empty;
        //        inHeaderPlus.Ith_mod_by = user;
        //        inHeaderPlus.Ith_mod_when = DateTime.Now;
        //        inHeaderPlus.Ith_noofcopies = 0;
        //        inHeaderPlus.Ith_oth_loc = string.Empty;
        //        inHeaderPlus.Ith_oth_docno = "N/A";
        //        inHeaderPlus.Ith_remarks = "";
        //        //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
        //        inHeaderPlus.Ith_session_id = BaseCls.GlbUserSessionID;
        //        inHeaderPlus.Ith_stus = "A";
        //        inHeaderPlus.Ith_sub_tp = "NOR";
        //        inHeaderPlus.Ith_vehi_no = string.Empty;
        //        #endregion
        //        MasterAutoNumber masterAutoPlus = new MasterAutoNumber();
        //        #region Fill MasterAutoNumber
        //        masterAutoPlus.Aut_cate_cd = defLocation;
        //        masterAutoPlus.Aut_cate_tp = "LOC";
        //        masterAutoPlus.Aut_direction = null;
        //        masterAutoPlus.Aut_modify_dt = null;
        //        masterAutoPlus.Aut_moduleid = "ADJ";
        //        masterAutoPlus.Aut_number = 5;//what is Aut_number
        //        masterAutoPlus.Aut_start_char = "ADJ";
        //        masterAutoPlus.Aut_year = null;
        //        #endregion

        //        //result = _InventoryBLL.ADJPlus(inHeaderPlus, reptPickSerialsListPlus, null, masterAutoPlus, out documntNo);
        //        result = _InventoryBLL.SaveInwardScanSerial(inHeaderPlus, reptPickSerialsListPlus, null);
        //        result = _InventoryBLL.UpdateInventoryAutoNumber(inHeaderPlus, masterAutoPlus, "+", out documntNo);
        //        _inventoryDAL.UpdateMovementDocNo_Other(inHeaderPlus.Ith_seq_no, documntNo);

        //        #endregion
        //    }
        //}
    }
}