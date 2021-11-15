using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Linq;

namespace FF.WindowsERPClient.Inventory
{
    public partial class TemporaryIssueItems : Base
    {
        private string GblJobNum = string.Empty;
        private Int32 GbljobLineNum = -11;

        private List<Service_TempIssue> oMainList = new List<Service_TempIssue>();
        private List<Service_TempIssue> oRetList = new List<Service_TempIssue>();

        private List<Service_TempIssue> stockReturnItems;

        private string _SerialSearchType = "";

        public TemporaryIssueItems()
        {
            //    GblJobNum = job;
            //  GbljobLineNum = jobLine;
            InitializeComponent();

            dgvItems.AutoGenerateColumns = false;

            Service_Chanal_parameter _Parameters = null;
            _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_Parameters != null)
            {
                //  serial_no.HeaderText = _Parameters.SP_DB_SERIAL;
                // dgvItems.Columns["serial_no"].HeaderText = _Parameters.SP_DB_SERIAL;
                label3.Text = _Parameters.SP_DB_SERIAL;


            }
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

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SubLoc:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes:
                    {
                        paramsText.Append(_SerialSearchType + seperator + BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + txtItemCode.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ServiceJobDetails:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.EMP_ALL:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DefectTypes:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Designation:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town_new:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceEstimate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "2,3,5,6" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                    break;
            }

            return paramsText.ToString();
        }

        private void ServiceWIP_TempIssue_Load(object sender, EventArgs e)
        {

        }

        #region Events

        private void btnView_Click(object sender, EventArgs e)
        {
            if (!chkReturn.Checked)
            {
                getItems();
            }
            else
            {
                getIssued_Items();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string _msg = "";
            if (!chkReturn.Checked)
            {
                if (dgvItems.Rows.Count > 0)
                {
                    
                    if (btnSave.Text == "Issue")
                    {
                        if (MessageBox.Show("Do you want to issue?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please add items to grid", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemCode.Focus();
                    return;
                }

            }
            else
            {
                if (grvRecItems.Rows.Count > 0)
                {
                    Boolean _isfound = false;
                    for (int i = 0; i < grvRecItems.Rows.Count; i++)
                    {
                        if (grvRecItems.Rows[i].Cells["Column2"].Value != null && Convert.ToBoolean(grvRecItems.Rows[i].Cells["Column2"].Value) == true)
                        {
                            _isfound = true;
                            break;
                        }
                        
                    }
                    if(_isfound==false)
                    {
                        MessageBox.Show("Please select an item for return", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        return;
                    }

                    if (MessageBox.Show("Do you want to Return?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please select items to return", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemCode.Focus();
                    return;
                }
            }

            // List<Tuple<Int32, Int32>> _IssueItemList = new List<Tuple<Int32, Int32>>();



            //else
            //{
            //    _ReqInsAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            //    _ReqInsAuto.Aut_cate_tp = "LOC";
            //    _ReqInsAuto.Aut_direction = 0;
            //    _ReqInsAuto.Aut_modify_dt = null;
            //    _ReqInsAuto.Aut_moduleid = "TMPR";
            //    _ReqInsAuto.Aut_number = 0;
            //    _ReqInsAuto.Aut_start_char = "TMPR";
            //    _ReqInsAuto.Aut_year = DateTime.Today.Year;
            //}

            Int32 result = 0;
            string err = string.Empty;

            if (!chkReturn.Checked)
            {
                MasterAutoNumber _ReqInsAuto = new MasterAutoNumber();

                _ReqInsAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _ReqInsAuto.Aut_cate_tp = "LOC";
                _ReqInsAuto.Aut_direction = 1;
                _ReqInsAuto.Aut_modify_dt = null;
                _ReqInsAuto.Aut_moduleid = "TMPI";
                _ReqInsAuto.Aut_number = 0;
                _ReqInsAuto.Aut_start_char = "TMPI";
                _ReqInsAuto.Aut_year = DateTime.Today.Year;


                result = CHNLSVC.CustService.Save_SCV_TempIssue(oMainList, _ReqInsAuto, out err);
                _msg = "Successfully Issued. Temp Issue # : ";
            }
            else
            {
                foreach (DataGridViewRow row in grvRecItems.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        Service_TempIssue _tempIsu = new Service_TempIssue();
                        _tempIsu.STI_COM = BaseCls.GlbUserComCode;
                        _tempIsu.STI_DOC_TP = "TMPI";
                        _tempIsu.STI_ISSUEITMCD = row.Cells["STIISSUEITMCD"].Value.ToString();
                        _tempIsu.STI_ISSUESERIALNO = row.Cells["STIISSUESERIALNO"].Value.ToString();
                        _tempIsu.STI_LOC = BaseCls.GlbUserDefLoca;
                        oRetList.Add(_tempIsu);
                    }
                }



                result = CHNLSVC.CustService.UPD_TMP_ISSUE_RET(oRetList);
                _msg = "Successfully Returned.";
            }

            if (result != -99 && result >= 0)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(_msg + err, "Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                clearAll();
                chkReturn.Checked = false;

            }
            else
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Process Terminated.\n" + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                chkReturn.Checked = false;
                clearAll();
            }
        }

        private void txtItemCode_DoubleClick(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _commonSearch = null;
            _commonSearch = new CommonSearch.CommonSearch();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_commonSearch.SearchParams, null, null);
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

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtItemCode_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCode.Text))
            {
                MasterItem _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCode.Text);

                if (_itemdetail == null || _itemdetail.Mi_cd == null)
                {
                    MessageBox.Show("Please select correct item code.", "Temporary issue item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemCode.Focus();
                    txtItemCode.Clear();
                    return;
                }

            }
        }

        private void txtSerial_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_serial_Click(null, null);
        }

        private void txtSerial_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSerial.Text))
            {
                DataTable _multiItemforSerial = CHNLSVC.Inventory.CheckSerialBySerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text, txtSerial.Text.Trim());
                Int32 _isAvailable = _multiItemforSerial.Rows.Count;

                if (_isAvailable <= 0)
                {
                    MessageBox.Show("Selected serial no is invalid or not available in your location.\nPlease check your inventory.", "Invalid Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtSerial.Clear(); return;
                }
            }

        }

        private void txtSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(null, null);
            }
            if(e.KeyCode==Keys.F2)
                btn_srch_serial_Click(null, null);
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

        private void dgvItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvItems.IsCurrentCellDirty)
            {
                if (dgvItems.IsCurrentCellDirty)
                {
                    dgvItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        private void dgvItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void dgvItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(itemID_KeyPress);
            if (dgvItems.CurrentCell.ColumnIndex == dgvItems.Columns["qty"].Index)
            {
                TextBox itemID = e.Control as TextBox;
                if (itemID != null)
                {
                    itemID.KeyPress += new KeyPressEventHandler(itemID_KeyPress);
                }
            }
        }

        private void itemID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void dgvItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != 0 || e.ColumnIndex != 0)
            {
                if (dgvItems.Rows[e.RowIndex].Cells["qty"].Value != null && !String.IsNullOrEmpty(dgvItems.Rows[e.RowIndex].Cells["qty"].Value.ToString()))
                {
                    String item_code = string.Empty;
                    String item_staus = string.Empty;
                    String STATUS_CODE = string.Empty;
                    String serial_no = string.Empty;
                    String SERIAL_ID = string.Empty;
                    decimal qty = 0;

                    item_code = dgvItems.Rows[e.RowIndex].Cells["item_code"].Value.ToString();
                    item_staus = dgvItems.Rows[e.RowIndex].Cells["item_staus"].Value.ToString();
                    STATUS_CODE = dgvItems.Rows[e.RowIndex].Cells["STATUS_CODE"].Value.ToString();
                    serial_no = dgvItems.Rows[e.RowIndex].Cells["serial_no"].Value.ToString();
                    SERIAL_ID = dgvItems.Rows[e.RowIndex].Cells["SERIAL_ID"].Value.ToString();

                    List<Service_stockReturn> stockReturnItems1 = new List<Service_stockReturn>();
                    List<Service_TempIssue> stockReturnItems2 = new List<Service_TempIssue>();
                    decimal oldQty = 0;
                    decimal IssuedQty = 0;
                    stockReturnItems1 = CHNLSVC.CustService.Get_ServiceWIP_StockReturnItems(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum, txtItemCode.Text, BaseCls.GlbUserDefProf);
                    stockReturnItems2 = CHNLSVC.CustService.Get_ServiceWIP_TempIssued_Items(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum, txtItemCode.Text, BaseCls.GlbUserDefProf, "TMPI");

                    if (!chkReturn.Checked)
                    {
                        if (serial_no != "N/A")
                        {
                            //Service_stockReturn item1 = stockReturnItems1.Find(x => x.ITEM_CODE == item_code && x.STATUS_CODE == STATUS_CODE && x.SERIAL_ID == SERIAL_ID);
                            //oldQty = item1.QTY;

                            //Service_TempIssue item2 = stockReturnItems2.Find(x => x.STI_ISSUEITMCD == item_code && x.STI_ISSUEITMSTUS == STATUS_CODE && x.STI_ISSUESERID == Convert.ToInt32(SERIAL_ID));
                            //if (item2 != null)
                            //{
                            //    IssuedQty = item2.STI_ISSUEITMQTY;

                            //    decimal NewQty = Convert.ToDecimal(dgvItems.Rows[e.RowIndex].Cells["qty"].Value.ToString());
                            //    if (oldQty - IssuedQty < NewQty)
                            //    {
                            //        MessageBox.Show("You have temporary issued " + IssuedQty.ToString("N2") + " from this item. \nYou have only " + (oldQty - IssuedQty).ToString("N2") + ".", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //        dgvItems.Rows[e.RowIndex].Cells["qty"].Value = (oldQty - IssuedQty);
                            //        return;
                            //    }
                            //}
                        }
                        else
                        {
                            Service_stockReturn item1 = stockReturnItems1.Find(x => x.ITEM_CODE == item_code && x.STATUS_CODE == STATUS_CODE);
                            oldQty = item1.QTY;

                            Service_TempIssue item2 = stockReturnItems2.Find(x => x.STI_ISSUEITMCD == item_code && x.STI_ISSUEITMSTUS == STATUS_CODE);
                            if (item2 != null)
                            {
                                IssuedQty = item2.STI_ISSUEITMQTY;

                                decimal NewQty = Convert.ToDecimal(dgvItems.Rows[e.RowIndex].Cells["qty"].Value.ToString());
                                if (oldQty - IssuedQty < NewQty)
                                {
                                    MessageBox.Show("You have temporary issued " + IssuedQty.ToString("N2") + " from this item. \nYou have only " + (oldQty - IssuedQty).ToString("N2") + ".", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    dgvItems.Rows[e.RowIndex].Cells["qty"].Value = (oldQty - IssuedQty);
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        Service_TempIssue item2 = stockReturnItems2.Find(x => x.STI_ISSUEITMCD == item_code && x.STI_ISSUEITMSTUS == STATUS_CODE && x.STI_ISSUESERID == Convert.ToInt32(SERIAL_ID));
                        if (item2 != null)
                        {
                            IssuedQty = item2.STI_ISSUEITMQTY;

                            decimal NewQty = Convert.ToDecimal(dgvItems.Rows[e.RowIndex].Cells["qty"].Value.ToString());
                            if (IssuedQty < NewQty)
                            {
                                MessageBox.Show("You have temporary issued " + IssuedQty.ToString("N2") + " from this item.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dgvItems.Rows[e.RowIndex].Cells["qty"].Value = (IssuedQty);
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void chkReturn_CheckedChanged(object sender, EventArgs e)
        {
            clearAll();
            btnSave.Visible = true;
            if (chkReturn.Checked)
            {
                btnSave.Text = "Return";
                btnView.Visible = true;
                grvRecItems.Visible = true;
                dgvItems.Visible = false;
                chkSelAll.Visible = true;
            }
            else
            {
                btnSave.Text = "Issue";
                btnView.Visible = false;
                dgvItems.Visible = true;
                grvRecItems.Visible = false;
                chkSelAll.Visible = false;
            }
        }

        private void dgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    oMainList.RemoveAt(e.RowIndex);
                    dgvItems.AutoGenerateColumns = false;
                    dgvItems.DataSource = new List<Service_TempIssue>();
                    dgvItems.DataSource = oMainList;
                }
            }
        }

        private void dgvItems_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!chkReturn.Checked)
            {
                //if (e.RowIndex != -1)
                //{
                //    if (Convert.ToBoolean(dgvItems.Rows[e.RowIndex].Cells["Column1"].Value) == true)
                //    {
                //        String item_code = string.Empty;
                //        String item_staus = string.Empty;
                //        String STATUS_CODE = string.Empty;
                //        String serial_no = string.Empty;
                //        String SERIAL_ID = string.Empty;

                //        item_code = dgvItems.Rows[e.RowIndex].Cells["item_code"].Value.ToString();
                //        item_staus = dgvItems.Rows[e.RowIndex].Cells["item_staus"].Value.ToString();
                //        STATUS_CODE = dgvItems.Rows[e.RowIndex].Cells["STATUS_CODE"].Value.ToString();
                //        serial_no = dgvItems.Rows[e.RowIndex].Cells["serial_no"].Value.ToString();
                //        SERIAL_ID = dgvItems.Rows[e.RowIndex].Cells["SERIAL_ID"].Value.ToString();

                //        List<Service_TempIssue> stockReturnItems2 = new List<Service_TempIssue>();
                //        stockReturnItems2 = CHNLSVC.CustService.Get_ServiceWIP_TempIssued_Items(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum, item_code, BaseCls.GlbUserDefProf, "TMPI");

                //        if (stockReturnItems2.FindAll(x => x.STI_ISSUESERIALNO == serial_no).Count > 0)
                //        {
                //            MessageBox.Show("This item is already issued.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            dgvItems.Rows[e.RowIndex].Cells["select"].Value = false;
                //            return;
                //        }
                //    }
                //}
            }
        }

        private void chkReturnItems_CheckedChanged(object sender, EventArgs e)
        {

        }

        #endregion Events

        #region methods

        private bool validateItem(string itemCode)
        {
            bool status = false;

            List<Service_stockReturn> stockReturnItems = CHNLSVC.CustService.Get_ServiceWIP_StockReturnItems(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum, itemCode, BaseCls.GlbUserDefProf);
            if (stockReturnItems.Count > 0)
            {
                status = true;
            }
            return status;
        }

        public void getItems()
        {
            List<Service_stockReturn> stockReturnItems = new List<Service_stockReturn>();
            stockReturnItems = CHNLSVC.CustService.Get_ServiceWIP_StockReturnItems(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum, txtItemCode.Text, BaseCls.GlbUserDefProf);
            if (!string.IsNullOrEmpty(txtSerial.Text))
            {
                stockReturnItems = stockReturnItems.FindAll(x => x.SERIAL_NO.Contains(txtSerial.Text));
            }

            List<Service_TempIssue> TempIssue = new List<Service_TempIssue>();
            TempIssue = CHNLSVC.CustService.Get_ServiceWIP_TempIssued_Items(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum, txtItemCode.Text, BaseCls.GlbUserDefProf, "TMPI");
            if (!string.IsNullOrEmpty(txtSerial.Text))
            {
                TempIssue = TempIssue.FindAll(x => x.STI_ISSUESERIALNO == txtSerial.Text);
            }

            foreach (Service_stockReturn item in stockReturnItems)
            {
                if (TempIssue.FindAll(x => x.STI_ISSUEITMCD == item.ITEM_CODE).Count > 0)
                {
                    item.QtyIssued = TempIssue.FindAll(x => x.STI_ISSUEITMCD == item.ITEM_CODE).Sum(x => x.STI_ISSUEITMQTY);
                    item.QTYBalance = item.QTY - item.QtyIssued;
                }
            }

            dgvItems.Columns["QtyIssued"].Visible = true;
            dgvItems.Columns["QTYBalance"].Visible = true;

            if (dgvItems.Columns.Contains("SeqNO"))
            {
                dgvItems.Columns.Remove("SeqNO");
                dgvItems.Columns.Remove("LineNo");
            }

            dgvItems.Columns["inward_doc"].DataPropertyName = "INWARD_DOC";
            dgvItems.Columns["item_code"].DataPropertyName = "ITEM_CODE";
            dgvItems.Columns["item_staus"].DataPropertyName = "ITEM_STAUS";
            dgvItems.Columns["STATUS_CODE"].DataPropertyName = "STATUS_CODE";
            dgvItems.Columns["serial_no"].DataPropertyName = "serial_no";
            dgvItems.Columns["SERIAL_ID"].DataPropertyName = "SERIAL_ID";
            dgvItems.Columns["job_no"].DataPropertyName = "job_no";
            dgvItems.Columns["job_line"].DataPropertyName = "job_line";
            dgvItems.Columns["Qty"].DataPropertyName = "Qty";
            dgvItems.Columns["Remark"].DataPropertyName = "Remark";

            dgvItems.DataSource = new List<Service_stockReturn>();
            dgvItems.AutoGenerateColumns = false;
            dgvItems.DataSource = stockReturnItems;
            modifyGrid();
        }

        public void getIssued_Items()
        {
            stockReturnItems = new List<Service_TempIssue>();
            stockReturnItems = CHNLSVC.CustService.Get_ServiceWIP_TempIssued_Items(BaseCls.GlbUserComCode, null, -999, txtItemCode.Text, BaseCls.GlbUserDefProf, "TMPI");
            //if (!string.IsNullOrEmpty(txtSerial.Text))
            //{
            //    stockReturnItems = stockReturnItems.FindAll(x => x.STI_ISSUESERIALNO == txtSerial.Text);
            //}

            //stockReturnItems.RemoveAll(x => x.STI_DT.Date != dtpDate.Value.Date);

            //dgvItems.Columns["QtyIssued"].Visible = false;
            //dgvItems.Columns["QTYBalance"].Visible = false;

            //dgvItems.Columns["inward_doc"].DataPropertyName = null;
            //dgvItems.Columns["item_code"].DataPropertyName = "STI_ISSUEITMCD";
            //dgvItems.Columns["item_staus"].DataPropertyName = "STI_ISSUEITMSTUS_Text";
            //dgvItems.Columns["STATUS_CODE"].DataPropertyName = "STI_ISSUEITMSTUS";
            //dgvItems.Columns["serial_no"].DataPropertyName = "STI_ISSUESERIALNO";
            //dgvItems.Columns["SERIAL_ID"].DataPropertyName = "STI_ISSUESERID";
            //dgvItems.Columns["job_no"].DataPropertyName = "STI_JOBNO";
            //dgvItems.Columns["job_line"].DataPropertyName = "STI_JOBLINENO";
            //dgvItems.Columns["Qty"].DataPropertyName = "STI_ISSUEITMQTY";
            //dgvItems.Columns["Remark"].DataPropertyName = "STI_RMK";

            //if (!dgvItems.Columns.Contains("SeqNO"))
            //{
            //    dgvItems.Columns.Add("SeqNO", "SeqNO");
            //    dgvItems.Columns.Add("LineNo", "LineNo");

            //    dgvItems.Columns["SeqNO"].Visible = false;
            //    dgvItems.Columns["LineNo"].Visible = false;

            //    dgvItems.Columns["SeqNO"].DataPropertyName = "STI_SEQNO";
            //    dgvItems.Columns["LineNo"].DataPropertyName = "STI_LINE";
            //}

            grvRecItems.AutoGenerateColumns = false;
            grvRecItems.DataSource = stockReturnItems;
            // modifyGrid();
        }

        public void getReturnd_Items()
        {
            stockReturnItems = new List<Service_TempIssue>();

            stockReturnItems = CHNLSVC.CustService.GET_TEMPISSUE_RETURNED_ITMS(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum, txtItemCode.Text, BaseCls.GlbUserDefProf);

            if (!string.IsNullOrEmpty(txtSerial.Text))
            {
                stockReturnItems = stockReturnItems.FindAll(x => x.STI_ISSUESERIALNO == txtSerial.Text);
            }

            stockReturnItems.RemoveAll(x => x.STI_DT != dtpDate.Value.Date);

            dgvItems.Columns["QtyIssued"].Visible = false;
            dgvItems.Columns["QTYBalance"].Visible = false;

            dgvItems.Columns["inward_doc"].DataPropertyName = null;
            dgvItems.Columns["item_code"].DataPropertyName = "STI_ISSUEITMCD";
            dgvItems.Columns["item_staus"].DataPropertyName = "STI_ISSUEITMSTUS_Text";
            dgvItems.Columns["STATUS_CODE"].DataPropertyName = "STI_ISSUEITMSTUS";
            dgvItems.Columns["serial_no"].DataPropertyName = "STI_ISSUESERIALNO";
            dgvItems.Columns["SERIAL_ID"].DataPropertyName = "STI_ISSUESERID";
            dgvItems.Columns["job_no"].DataPropertyName = "STI_JOBNO";
            dgvItems.Columns["job_line"].DataPropertyName = "STI_JOBLINENO";
            dgvItems.Columns["Qty"].DataPropertyName = "STI_ISSUEITMQTY";
            dgvItems.Columns["Remark"].DataPropertyName = "STI_RMK";

            if (!dgvItems.Columns.Contains("SeqNO"))
            {
                dgvItems.Columns.Add("SeqNO", "SeqNO");
                dgvItems.Columns.Add("LineNo", "LineNo");

                dgvItems.Columns["SeqNO"].Visible = false;
                dgvItems.Columns["LineNo"].Visible = false;

                dgvItems.Columns["SeqNO"].DataPropertyName = "STI_SEQNO";
                dgvItems.Columns["LineNo"].DataPropertyName = "STI_LINE";
            }

            dgvItems.DataSource = stockReturnItems;
            modifyGrid();
        }

        private void modifyGrid()
        {
            if (dgvItems.Rows.Count > 0)
            {
                for (int i = 0; i < dgvItems.Rows.Count; i++)
                {
                    if (dgvItems.Rows[i].Cells["serial_no"].Value.ToString() == "N/A")
                    {
                        dgvItems.Rows[i].Cells["Qty"].ReadOnly = false;
                        dgvItems.Rows[i].DefaultCellStyle.BackColor = Color.Thistle;
                    }
                    else
                    {
                        dgvItems.Rows[i].Cells["Qty"].ReadOnly = true;
                    }
                }
                dgvItems.Columns[0].Frozen = true;
            }
        }

        private bool isAnySelected()
        {
            bool status = false;

            if (dgvItems.Rows.Count > 0)
            {
                for (int i = 0; i < dgvItems.Rows.Count; i++)
                {
                    if (dgvItems.Rows[i].Cells["select"].Value != null && Convert.ToBoolean(dgvItems.Rows[i].Cells["select"].Value) == true)
                    {
                        status = true;
                        break;
                    }
                }
            }
            return status;
        }

        #endregion methods

        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            txtItemCode_DoubleClick(null, null);
        }

        private void btnCloseFrom_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearAll()
        {
            oMainList = new List<Service_TempIssue>();
            oRetList = new List<Service_TempIssue>();
            txtItemCode.Clear();
            txtSerial.Clear();
            dtpDate.Value = DateTime.Today;
            dgvItems.AutoGenerateColumns = false;
            grvRecItems.AutoGenerateColumns = false;
            dgvItems.DataSource = new List<Service_TempIssue>();
            grvRecItems.DataSource = new List<Service_TempIssue>();
            stockReturnItems = new List<Service_TempIssue>();
            chkSelectAll.Checked = false;
            txtSubLoc.Text = "";
            txtRem.Text = "";
            txtStus.Text = "";

            //chkReturn.Checked = false;
        }

        private void btnVisit_Click(object sender, EventArgs e)
        {

        }

        private void dgvVisits_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {

        }



        private void btnHide_Click(object sender, EventArgs e)
        {
            // pnlVistiSelect.Visible = false;
        }

        private void btn_srch_serial_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            if (string.IsNullOrEmpty(txtItemCode.Text))
            {
                _SerialSearchType = "SER1_WOITEM";
                _CommonSearch.ReturnIndex = 7;
            }
            else
            {
                _SerialSearchType = "SER1_WITEM";
                _CommonSearch.ReturnIndex = 6;
            }


            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
            DataTable _result = CHNLSVC.CommonSearch.Search_inr_ser_infor(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSerId;
            _CommonSearch.ShowDialog();
            txtSerId.Select();
            txtSerId_Leave(null, null);



        }

        private void txtSerId_Leave(object sender, EventArgs e)
        {
            DataTable inr_ser1 = CHNLSVC.Inventory.GetInventorySerialbyId(txtSerId.Text.ToString(), BaseCls.GlbUserDefLoca);
            if (inr_ser1.Rows.Count > 0)
            {
                txtSerial.Text = inr_ser1.Rows[0]["INS_SER_1"].ToString();
                txtItemCode.Text = inr_ser1.Rows[0]["INS_ITM_CD"].ToString();
                txtStus.Text = inr_ser1.Rows[0]["INS_ITM_STUS"].ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(chkReturn.Checked)
            {
                MessageBox.Show("Please deselect Issue Item to add items", "Delivery Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
         if(string.IsNullOrEmpty( txtItemCode.Text)   )
         {
             MessageBox.Show("Please select the item", "Delivery Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
             txtItemCode.Focus();
             return;
         }
         if (string.IsNullOrEmpty(txtSerial.Text))
         {
             MessageBox.Show("Please select the serial", "Delivery Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
             txtSerial.Focus();
             return;
         }
         if (string.IsNullOrEmpty(txtSubLoc.Text))
         {
             MessageBox.Show("Please select the sub location code", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
             return;
         }
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.Cells[3].Value.ToString().Equals(txtSerial.Text))
                {
                    MessageBox.Show("Already added this serial number", "Delivery Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    return;
                }
            }

            DataTable _dt = CHNLSVC.CustService.GET_TEMPISSUE_By_SER(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca,txtItemCode.Text,txtSerial.Text, "TMPI");

            if (_dt.Rows.Count>0)
            {
                if (MessageBox.Show("This serial is already temporarily issued. Do you want to return ?", "Temporary Issues", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                return;
            }
            Service_TempIssue _tempIsu = new Service_TempIssue();
            _tempIsu.STI_BALQTY = 1;
            _tempIsu.STI_COM = BaseCls.GlbUserComCode;
            _tempIsu.STI_CRE_BY = BaseCls.GlbUserID;
            _tempIsu.STI_DOC_TP = "TMPI";
            _tempIsu.STI_DT = Convert.ToDateTime(dtpDate.Value).Date;
            _tempIsu.STI_ISRECEIVE = 0;
            _tempIsu.STI_ISSUEITMCD = txtItemCode.Text;
            _tempIsu.STI_ISSUEITMQTY = 1;
            _tempIsu.STI_ISSUEITMSTUS = txtStus.Text;
            _tempIsu.STI_ISSUESERIALNO = txtSerial.Text;
            _tempIsu.STI_ISSUESERID = Convert.ToInt32(txtSerId.Text);
            _tempIsu.STI_LOC = BaseCls.GlbUserDefLoca;
            _tempIsu.STI_RMK = txtRem.Text;
            _tempIsu.STI_STUS = "A";
            _tempIsu.STI_JOBNO = "-";
            _tempIsu.STI_SUB_LOC = txtSubLoc.Text;
            oMainList.Add(_tempIsu);

            dgvItems.AutoGenerateColumns = false;
            dgvItems.DataSource = new List<Service_TempIssue>();
            dgvItems.DataSource = oMainList;

        }

        private void btn_srch_sub_loc_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SubLoc);
            DataTable _result = CHNLSVC.CommonSearch.SearchSubLocationData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSubLoc;
            _CommonSearch.ShowDialog();
            txtSubLoc.Select();
        }

        private void grvRecItems_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }

        private void grvRecItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grvRecItems.IsCurrentCellDirty)
            {
                grvRecItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void txtSubLoc_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_sub_loc_Click(null, null);
        }

        private void txtSubLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F2)
                btn_srch_sub_loc_Click(null, null);
        }

        private void txtSubLoc_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSubLoc.Text))
            {
                DataTable _dt = CHNLSVC.Sales.getSubLocationByCode(BaseCls.GlbUserComCode, null, txtSubLoc.Text);
                if (_dt == null || _dt.Rows.Count <= 0)
                {
                    MessageBox.Show("Invalid Sub location", "Delivery Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSubLoc.Clear();
                    txtSubLoc.Focus();
                    return;
                }
            }
        }

        private void chkSelAll_CheckedChanged(object sender, EventArgs e)
        {
            if (grvRecItems.Rows.Count > 0)
            {
                if (chkSelAll.Checked)
                {
                    for (int i = 0; i < grvRecItems.Rows.Count; i++)
                    {
                        grvRecItems.Rows[i].Cells["Column2"].Value = true;
                    }
                }
                else
                {
                    for (int i = 0; i < grvRecItems.Rows.Count; i++)
                    {
                        grvRecItems.Rows[i].Cells["Column2"].Value = false;
                    }
                }
            }
        }
    }
}