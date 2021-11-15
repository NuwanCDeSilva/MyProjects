using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceWIP_ConsumableItems : Base
    {
        private string GblJobNum = string.Empty;
        private Int32 GbljobLineNum = -11;
        private List<Service_TempIssue> oMainList = new List<Service_TempIssue>();
        private List<Service_TempIssue> stockReturnItems;

        public ServiceWIP_ConsumableItems(string job, Int32 jobLine)
        {
            GblJobNum = job;
            GbljobLineNum = jobLine;
            InitializeComponent();
            dgvItems.AutoGenerateColumns = false;
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
                case CommonUIDefiniton.SearchUserControlType.GetItmByType:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "Z" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ConsumableItms:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                    break;
            }

            return paramsText.ToString();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            dgvItems.Columns["QTYBalance"].Visible = true;
            dgvItems.Columns["RequiredQty"].Visible = true;
            dgvItems.Columns["QtyIssued"].Visible = true;
            dgvItems.Columns["Qty"].HeaderText = "Stock In Hand";
            dgvItems.Columns["Qty"].ReadOnly = true;

            if (!chkReturn.Checked)
            {
                getItems();
                btnSave.Visible = true;
                btnRetun.Visible = false;
            }
            else
            {
                btnRetun.Visible = true;
                btnSave.Visible = false;
                getIssued_Items();
            }
        }

        private void dgvItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(itemID_KeyPress);
            if (dgvItems.CurrentCell.ColumnIndex == dgvItems.Columns["RequiredQty"].Index)
            {
                TextBox itemID = e.Control as TextBox;
                if (itemID != null)
                {
                    itemID.KeyPress += new KeyPressEventHandler(itemID_KeyPress);
                }

                if (dgvItems.CurrentCell.ColumnIndex == dgvItems.Columns["RequiredQty"].Index || dgvItems.CurrentCell.ColumnIndex == dgvItems.Columns["Remark"].Index)
                {
                    dgvItems.Rows[dgvItems.CurrentCell.RowIndex].Cells["select"].Value = true;
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

        private void dgvItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvItems.IsCurrentCellDirty)
            {
                dgvItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void btnVouNo_Click(object sender, EventArgs e)
        {
            txtItemCode_DoubleClick(null, null);
        }

        private void txtItemCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ConsumableItms);
                DataTable _result = CHNLSVC.CommonSearch.SEARCH_CONSUMABLE_ITEMS(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCode;
                _CommonSearch.ShowDialog();
                txtItemCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtItemCode_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                dgvItems.Focus();
            }
        }

        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCode.Text))
            {
                MasterItem _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCode.Text);

                if (_itemdetail == null || _itemdetail.Mi_cd == null)
                {
                    MessageBox.Show("Please select correct item code.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemCode.Focus();
                    txtItemCode.Clear();
                    return;
                }
                else
                {
                    if (!validateItem(txtItemCode.Text))
                    {
                        MessageBox.Show("Selected Item code is not in the stock. Please select correct item code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItemCode.Focus();
                        txtItemCode.Clear();
                        return;
                    }
                }
            }
        }

        private bool validateItem(string itemCode)
        {
            bool status = false;

            List<Service_stockReturn> stockReturnItems = CHNLSVC.CustService.Get_ServiceWIP_ConsumableItems(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, itemCode);
            if (stockReturnItems.Count > 0)
            {
                status = true;
            }
            return status;
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

        public void getItems()
        {
            List<Service_stockReturn> stockReturnItems = new List<Service_stockReturn>();
            stockReturnItems = CHNLSVC.CustService.Get_ServiceWIP_ConsumableItems(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtItemCode.Text);

            List<Service_TempIssue> Service_TempIssueTemp = new List<Service_TempIssue>();
            Service_TempIssueTemp = CHNLSVC.CustService.Get_ServiceWIP_TempIssued_Items(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum, txtItemCode.Text, BaseCls.GlbUserDefProf, "CONSM");

            List<Service_TempIssue> Service_TempIssueTempLoC = new List<Service_TempIssue>();
            Service_TempIssueTempLoC = CHNLSVC.CustService.GET_TEMPISSUE_By_LOC(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum, txtItemCode.Text, BaseCls.GlbUserDefProf, "CONSM");

            foreach (Service_stockReturn item in stockReturnItems)
            {
                if (Service_TempIssueTempLoC.FindAll(x => x.STI_ISSUEITMCD == item.ITEM_CODE).Count > 0)
                {
                    item.QtyIssued = Service_TempIssueTempLoC.FindAll(x => x.STI_ISSUEITMCD == item.ITEM_CODE).Sum(x => x.STI_ISSUEITMQTY);
                    item.QTYBalance = item.QTY - item.QtyIssued;
                }
            }

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
            dgvItems.Columns["DESC"].DataPropertyName = "DESC";

            dgvItems.DataSource = new List<Service_stockReturn>();
            dgvItems.DataSource = stockReturnItems;
        }

        public void getIssued_Items()
        {
            stockReturnItems = new List<Service_TempIssue>();
            stockReturnItems = CHNLSVC.CustService.Get_ServiceWIP_TempIssued_Items(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum, txtItemCode.Text, BaseCls.GlbUserDefProf, "CONSM");

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
            dgvItems.Columns["DESC"].DataPropertyName = "DESC";

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

            dgvItems.Columns["QTYBalance"].Visible = false;
            dgvItems.Columns["RequiredQty"].Visible = false;
            dgvItems.Columns["QtyIssued"].Visible = false;
            dgvItems.Columns["Qty"].HeaderText = "Used Qty";
            dgvItems.Columns["Qty"].ReadOnly = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvItems.Rows.Count > 0)
            {
                if (isAnySelected())
                {
                    if (chkReturn.Checked)
                    {
                        updateConsumableItems();
                    }
                    else
                    {
                        if (MessageBox.Show("Do you want to issue?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }

                        List<Tuple<Int32, Int32>> _IssueItemList = new List<Tuple<Int32, Int32>>();

                        for (int i = 0; i < dgvItems.Rows.Count; i++)
                        {
                            if (dgvItems.Rows[i].Cells["select"].Value != null && Convert.ToBoolean(dgvItems.Rows[i].Cells["select"].Value) == true)
                            {
                                if (dgvItems.Rows[i].Cells["RequiredQty"].Value == null)
                                {
                                    MessageBox.Show("Please enter a qty", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                Service_TempIssue oItem = new Service_TempIssue();
                                //oItem.STI_SEQNO             = "";
                                oItem.STI_LINE = i + 1;
                                //oItem.STI_DOC = "";
                                oItem.STI_DT = dtpDate.Value.Date;
                                oItem.STI_COM = BaseCls.GlbUserComCode;
                                oItem.STI_LOC = BaseCls.GlbUserDefLoca;
                                oItem.STI_DOC_TP = "CONSM";
                                oItem.STI_JOBNO = GblJobNum;
                                oItem.STI_JOBLINENO = GbljobLineNum;
                                oItem.STI_ISSUEITMCD = dgvItems.Rows[i].Cells["item_code"].Value.ToString();
                                oItem.STI_ISSUEITMSTUS = dgvItems.Rows[i].Cells["STATUS_CODE"].Value.ToString();
                                oItem.STI_ISSUESERIALNO = dgvItems.Rows[i].Cells["serial_no"].Value.ToString();
                                oItem.STI_ISSUEITMQTY = Convert.ToDecimal(dgvItems.Rows[i].Cells["RequiredQty"].Value.ToString());
                                oItem.STI_BALQTY = Convert.ToDecimal(dgvItems.Rows[i].Cells["RequiredQty"].Value.ToString());
                                oItem.STI_CROSS_SEQNO = 0;
                                oItem.STI_CROSS_LINE = 0;
                                oItem.STI_ISRECEIVE = 0;
                                oItem.STI_TECHCODE = "";
                                oItem.STI_REFDOCNO = "";
                                oItem.STI_REFDOCLINE = 0;
                                oItem.STI_STUS = "A";
                                oItem.STI_RMK = dgvItems.Rows[i].Cells["Remark"].Value == null ? string.Empty : dgvItems.Rows[i].Cells["Remark"].Value.ToString();
                                oItem.STI_CRE_BY = BaseCls.GlbUserID;
                                oItem.STI_CRE_DT = DateTime.Now;
                                oMainList.Add(oItem);
                            }
                        }
                        MasterAutoNumber _ReqInsAuto = new MasterAutoNumber();

                        _ReqInsAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                        _ReqInsAuto.Aut_cate_tp = "LOC";
                        _ReqInsAuto.Aut_direction = 1;
                        _ReqInsAuto.Aut_modify_dt = null;
                        _ReqInsAuto.Aut_moduleid = "CONSM";
                        _ReqInsAuto.Aut_number = 0;
                        _ReqInsAuto.Aut_start_char = "CONSM";
                        _ReqInsAuto.Aut_year = DateTime.Today.Year;

                        Int32 result = 0;
                        string err;
                        result = CHNLSVC.CustService.Save_SCV_TempIssue(oMainList, _ReqInsAuto, out err);

                        if (result != -99 && result >= 0)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("Successfully Saved!", "Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            btnClear_Click(null, null);
                            btnView_Click(null, null);
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
                    MessageBox.Show("Please Select items", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvItems.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please add items to grid", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtItemCode.Focus();
                return;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            oMainList = new List<Service_TempIssue>();
            dgvItems.DataSource = new List<Service_TempIssue>();
            txtItemCode.Clear();
            btnRetun.Visible = false;
            chkReturn.Checked = false;
            btnView_Click(null, null);
        }

        private void updateConsumableItems()
        {
            if (MessageBox.Show("Do you want to update?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            else
            {
                stockReturnItems = new List<Service_TempIssue>();

                for (int i = 0; i < dgvItems.Rows.Count; i++)
                {
                    if (dgvItems.Rows[i].Cells["select"].Value != null && Convert.ToBoolean(dgvItems.Rows[i].Cells["select"].Value) == true)
                    {
                        Service_TempIssue oItem = new Service_TempIssue();
                        oItem.STI_SEQNO = Convert.ToInt32(dgvItems.Rows[i].Cells["SeqNO"].Value.ToString());
                        oItem.STI_LINE = Convert.ToInt32(dgvItems.Rows[i].Cells["LineNo"].Value.ToString());
                        oItem.STI_RMK = dgvItems.Rows[i].Cells["Remark"].Value == null ? string.Empty : dgvItems.Rows[i].Cells["Remark"].Value.ToString();
                        oItem.STI_ISSUEITMQTY = Convert.ToDecimal(dgvItems.Rows[i].Cells["qty"].Value.ToString());
                        oItem.STI_BALQTY = Convert.ToDecimal(dgvItems.Rows[i].Cells["qty"].Value.ToString());
                        oItem.STI_CRE_BY = BaseCls.GlbUserID;
                        oItem.STI_CRE_DT = DateTime.Now;
                        oItem.STI_COM = BaseCls.GlbUserComCode;
                        oItem.STI_LOC = BaseCls.GlbUserDefLoca;
                        stockReturnItems.Add(oItem);
                    }
                }

                Int32 result = CHNLSVC.CustService.UpdateConsumableItemQty(stockReturnItems);

                if (result != -99 && result >= 0)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Successfully Saved!", "Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnClear_Click(null, null);
                    btnView_Click(null, null);
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Process Terminated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvItems.CurrentCell.Value != null && dgvItems.CurrentCell.Value == "")
            {
                MessageBox.Show("Please enter a qty", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgvItems.CurrentCell.Value = 0;
                return;
            }
            List<Service_stockReturn> stockReturnItems = new List<Service_stockReturn>();
            string itemCode = dgvItems.Rows[e.RowIndex].Cells["item_code"].Value.ToString();
            stockReturnItems = CHNLSVC.CustService.Get_ServiceWIP_ConsumableItems(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, itemCode);
            decimal stockQTY = stockReturnItems[0].QTY;


            List<Service_TempIssue> Service_TempIssueTempLoC = new List<Service_TempIssue>();
            Service_TempIssueTempLoC = CHNLSVC.CustService.GET_TEMPISSUE_By_LOC(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum, itemCode, BaseCls.GlbUserDefProf, "CONSM");

            if (Service_TempIssueTempLoC.Count > 0)
            {
                decimal IssuedQty = Service_TempIssueTempLoC[0].STI_ISSUEITMQTY;
                decimal newItemQty = 0;

                if (chkReturn.Checked)
                {
                    List<Service_TempIssue> IssuedItems = new List<Service_TempIssue>();
                    IssuedItems = CHNLSVC.CustService.Get_ServiceWIP_TempIssued_Items(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum, itemCode, BaseCls.GlbUserDefProf, "CONSM");

                    newItemQty = Convert.ToDecimal(dgvItems.Rows[e.RowIndex].Cells["Qty"].Value.ToString());
                    if (newItemQty > IssuedItems[0].STI_ISSUEITMQTY)
                    {
                        // if (dgvItems.Rows[e.RowIndex].Cells["select"].Value != null && Convert.ToBoolean(dgvItems.Rows[e.RowIndex].Cells["select"].Value) == true)
                        {
                            MessageBox.Show("Please enter valid quantity.\n You have issued only " + (IssuedItems[0].STI_ISSUEITMQTY).ToString("N"), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        dgvItems.Rows[e.RowIndex].Cells["Qty"].Value = (IssuedItems[0].STI_ISSUEITMQTY);
                        return;
                    }
                }
                else
                {
                    if (dgvItems.Rows[e.RowIndex].Cells["RequiredQty"].Value != null && dgvItems.Rows[e.RowIndex].Cells["QTYBalance"].Value != null)
                    {
                        newItemQty = Convert.ToDecimal(dgvItems.Rows[e.RowIndex].Cells["RequiredQty"].Value.ToString());
                        decimal BalanceQty = Convert.ToDecimal(dgvItems.Rows[e.RowIndex].Cells["QTYBalance"].Value.ToString());
                        if (newItemQty > BalanceQty)
                        {
                            dgvItems.Rows[e.RowIndex].Cells["RequiredQty"].Value = 0;
                            MessageBox.Show("Please enter valid quantity.\n You have only " + (BalanceQty).ToString("N"), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
            }
        }

        private void ServiceWIP_ConsumableItems_Load(object sender, EventArgs e)
        {
            btnRetun.Visible = false;

            btnView_Click(null, null);

            dgvItems.CellToolTipTextNeeded += new DataGridViewCellToolTipTextNeededEventHandler(dataGridView1_CellToolTipTextNeeded);
        }

        private void dgvItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == this.dgvItems.Columns["RequiredQty"].Index) && e.Value != null)
            {
                DataGridViewCell cell = this.dgvItems.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (e.Value.Equals("*"))
                {
                    cell.ToolTipText = "very bad";
                }
                else if (e.Value.Equals("**"))
                {
                    cell.ToolTipText = "bad";
                }
                else if (e.Value.Equals("***"))
                {
                    cell.ToolTipText = "good";
                }
                else if (e.Value.Equals("****"))
                {
                    cell.ToolTipText = "very good";
                }
            }
        }

        private void dgvItems_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            //var hucre = dgvItems.CurrentCell;
            //var hucre_loc = dgvItems.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

            //toolTip1.Show("//Info&" + e.ColumnIndex.ToString() + "&" + e.RowIndex.ToString(), dgvItems, hucre_loc.X, hucre_loc.Y);

            if ((e.RowIndex != -1) && (e.ColumnIndex == 1))
            {
                //MessageBox.Show("TEST");
                ToolTip tooltip1 = new ToolTip();
                tooltip1.Show("hello", dgvItems, Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void btnCloseFrom_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvItems_MouseMove(object sender, MouseEventArgs e)
        {
            //    if ((e.RowIndex != -1) && (e.ColumnIndex == 1))
            //    {
            //        //MessageBox.Show("TEST");
            //        ToolTip tooltip1 = new ToolTip();
            //        tooltip1.Show("hello", dataGridView1, Cursor.Position.X, Cursor.Position.Y);
            //    }
        }

        private void dataGridView1_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.ColumnIndex == 11)
            {
                e.ToolTipText = "Please enter amount here";
            }
        }

        private void btnRetun_Click(object sender, EventArgs e)
        {
            if (dgvItems.Rows.Count > 0)
            {
                if (isAnySelected())
                {
                    if (MessageBox.Show("Do you want to return?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        stockReturnItems = new List<Service_TempIssue>();

                        for (int i = 0; i < dgvItems.Rows.Count; i++)
                        {
                            if (dgvItems.Rows[i].Cells["select"].Value != null && Convert.ToBoolean(dgvItems.Rows[i].Cells["select"].Value) == true)
                            {
                                Service_TempIssue oItem = new Service_TempIssue();
                                oItem.STI_SEQNO = Convert.ToInt32(dgvItems.Rows[i].Cells["SeqNO"].Value.ToString());
                                oItem.STI_LINE = Convert.ToInt32(dgvItems.Rows[i].Cells["LineNo"].Value.ToString());
                                oItem.STI_RMK = dgvItems.Rows[i].Cells["Remark"].Value == null ? string.Empty : dgvItems.Rows[i].Cells["Remark"].Value.ToString();
                                oItem.STI_ISSUEITMQTY = Convert.ToDecimal("-" + dgvItems.Rows[i].Cells["qty"].Value.ToString());
                                oItem.STI_BALQTY = Convert.ToDecimal("-" + dgvItems.Rows[i].Cells["qty"].Value.ToString());
                                oItem.STI_CRE_BY = BaseCls.GlbUserID;
                                oItem.STI_CRE_DT = DateTime.Now;
                                oItem.STI_COM = BaseCls.GlbUserComCode;
                                oItem.STI_LOC = BaseCls.GlbUserDefLoca;
                                stockReturnItems.Add(oItem);
                            }
                        }

                        Int32 result = CHNLSVC.CustService.UpdateConsumableItemQty(stockReturnItems);

                        if (result != -99 && result >= 0)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("Successfully returned!", "Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            btnClear_Click(null, null);
                            btnView_Click(null, null);
                        }
                        else
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("Process Terminated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please Select items", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvItems.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please add items to grid", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtItemCode.Focus();
                return;
            }
        }
    }
}