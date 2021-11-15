using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceWIP_StockReturn : Base
    {
        private string JobNumber = string.Empty;
        private Int32 jobLineNum = -11;

        public ServiceWIP_StockReturn(string job, Int32 jobLine)
        {
            JobNumber = job;
            jobLineNum = jobLine;
            InitializeComponent();

            dgvItems.AutoGenerateColumns = false;

            Service_Chanal_parameter _Parameters = null;
            _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_Parameters != null)
            {
                serial_no.HeaderText = _Parameters.SP_DB_SERIAL;
                dgvItems.Columns["serial_no"].HeaderText = _Parameters.SP_DB_SERIAL;
                label3.Text = _Parameters.SP_DB_SERIAL;
            }
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
                default:
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
                    break;
            }

            return paramsText.ToString();
        }

        #region Events

        private void ServiceWIP_StockReturn_Load(object sender, EventArgs e)
        {
            fillItemStatus();
            txtItemCode.Focus();
            btnView_Click(null, null);
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                txtItemCode.Clear();
                txtItemCode.Enabled = false;
            }
            else
            {
                txtItemCode.Enabled = true;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            getItems();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItemCode.Text))
            {
                MessageBox.Show("Please enter item code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtItemCode.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtQty.Text))
            {
                MessageBox.Show("Please enter item quantity.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQty.Focus();
                return;
            }

            Service_stockReturn item = new Service_stockReturn();
            //item.
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

        private void txtItemCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvItems.Rows.Count > 0)
            {
                if (isAnySelected())
                {
                    List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();

                    for (int i = 0; i < dgvItems.Rows.Count; i++)
                    {
                        if (dgvItems.Rows[i].Cells["select"].Value != null && Convert.ToBoolean(dgvItems.Rows[i].Cells["select"].Value.ToString()) == true)
                        {
                            //If item is searialized
                            if (dgvItems.Rows[i].Cells["serial_no"].Value.ToString() != "N/A")
                            {
                                List<ReptPickSerials> _list = CHNLSVC.Inventory.Search_by_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, dgvItems.Rows[i].Cells["item_code"].Value.ToString(), BaseCls.GlbDefaultBin, dgvItems.Rows[i].Cells["serial_no"].Value.ToString(), string.Empty);

                                if (_list.Count != 1)
                                {
                                    MessageBox.Show("Multi serial item selected.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                _list[0].Tus_usrseq_no = 1;
                                _list[0].Tus_base_doc_no = "N/A";
                                _list[0].Tus_base_itm_line = 0;

                                reptPickSerialsList.AddRange(_list);
                            }
                            else // if the item is NON-Serialized
                            {
                                ReptPickSerials item = new ReptPickSerials();
                                item.Tus_cre_by = BaseCls.GlbUserID;
                                item.Tus_usrseq_no = 0;
                                item.Tus_cre_by = BaseCls.GlbUserID;
                                item.Tus_base_doc_no = string.Empty;
                                item.Tus_base_itm_line = 0;
                                item.Tus_itm_desc = string.Empty;
                                item.Tus_itm_model = string.Empty;
                                item.Tus_com = BaseCls.GlbUserComCode;
                                item.Tus_loc = BaseCls.GlbUserDefLoca;
                                item.Tus_bin = BaseCls.GlbDefaultBin;
                                item.Tus_itm_cd = dgvItems.Rows[i].Cells["item_code"].Value.ToString();
                                item.Tus_itm_stus = dgvItems.Rows[i].Cells["STATUS_CODE"].Value.ToString();
                                item.Tus_qty = Convert.ToDecimal(dgvItems.Rows[i].Cells["Qty"].Value.ToString());
                                item.Tus_ser_1 = "N/A";
                                item.Tus_ser_2 = "N/A";
                                item.Tus_ser_3 = "N/A";
                                item.Tus_ser_4 = "N/A";
                                item.Tus_ser_id = 0;
                                item.Tus_serial_id = "0";
                                item.Tus_usrseq_no = 1;
                                item.Tus_base_doc_no = "N/A";
                                item.Tus_base_itm_line = 0;
                                item.Tus_job_no = JobNumber;
                                item.Tus_job_line = jobLineNum;
                                reptPickSerialsList.Add(item);
                            }
                        }
                    }

                    #region Header

                    InventoryHeader inHeader = new InventoryHeader();
                    DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                    foreach (DataRow r in dt_location.Rows)
                    {
                        // Get the value of the wanted column and cast it to string
                        inHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                        if (System.DBNull.Value != r["ML_CATE_2"])
                        {
                            inHeader.Ith_channel = (string)r["ML_CATE_2"];
                        }
                        else
                        {
                            inHeader.Ith_channel = string.Empty;
                        }
                    }
                    inHeader.Ith_acc_no = "";
                    inHeader.Ith_anal_1 = "";
                    inHeader.Ith_anal_2 = "";
                    inHeader.Ith_anal_3 = "";
                    inHeader.Ith_anal_4 = "";
                    inHeader.Ith_anal_5 = "";
                    inHeader.Ith_anal_6 = 0;
                    inHeader.Ith_anal_7 = 0;
                    inHeader.Ith_anal_8 = DateTime.MinValue;
                    inHeader.Ith_anal_9 = DateTime.MinValue;
                    inHeader.Ith_anal_10 = false;
                    inHeader.Ith_anal_11 = false;
                    inHeader.Ith_anal_12 = false;
                    inHeader.Ith_bus_entity = "";
                    inHeader.Ith_cate_tp = "SERVICE";
                    inHeader.Ith_com = BaseCls.GlbUserComCode;
                    inHeader.Ith_com_docno = "";
                    inHeader.Ith_cre_by = BaseCls.GlbUserID;
                    inHeader.Ith_cre_when = DateTime.Now;
                    inHeader.Ith_del_add1 = "";
                    inHeader.Ith_del_add2 = "";
                    inHeader.Ith_del_code = "";
                    inHeader.Ith_del_party = "";
                    inHeader.Ith_del_town = "";

                    inHeader.Ith_direct = false;

                    inHeader.Ith_doc_date = dtpDate.Value.Date;
                    inHeader.Ith_doc_no = string.Empty;
                    inHeader.Ith_doc_tp = "AOD";
                    inHeader.Ith_doc_year = dtpDate.Value.Year;
                    inHeader.Ith_entry_tp = "";
                    inHeader.Ith_git_close = false;
                    inHeader.Ith_git_close_date = DateTime.MinValue;
                    inHeader.Ith_git_close_doc = string.Empty;
                    inHeader.Ith_isprinted = false;
                    inHeader.Ith_is_manual = false;
                    inHeader.Ith_job_no = JobNumber;
                    inHeader.Ith_loading_point = string.Empty;
                    inHeader.Ith_loading_user = string.Empty;
                    inHeader.Ith_loc = BaseCls.GlbUserDefLoca;
                    inHeader.Ith_manual_ref = "";
                    inHeader.Ith_mod_by = BaseCls.GlbUserID;
                    inHeader.Ith_mod_when = DateTime.Now;
                    inHeader.Ith_noofcopies = 0;

                    inHeader.Ith_oth_loc = string.Empty;
                    DataTable dt = CHNLSVC.General.get_loc_services(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "S");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        inHeader.Ith_oth_loc = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Plese setup the service locations","Return",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        return;
                    }

                    inHeader.Ith_oth_docno = JobNumber;
                    inHeader.Ith_remarks = "";

                    //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013

                    inHeader.Ith_session_id = BaseCls.GlbUserSessionID;
                    inHeader.Ith_stus = "A";
                    inHeader.Ith_sub_tp = "NOR";
                    inHeader.Ith_vehi_no = string.Empty;
                    inHeader.Ith_sub_docno = JobNumber;
                    inHeader.Ith_entry_no = string.Empty;
                    inHeader.Ith_oth_com = BaseCls.GlbUserComCode;

                    #endregion Header

                    #region AutoNumebr

                    MasterAutoNumber masterAuto = new MasterAutoNumber();
                    masterAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                    masterAuto.Aut_cate_tp = "LOC";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "AOD";
                    masterAuto.Aut_number = 5;//what is Aut_number
                    masterAuto.Aut_start_char = "AOD";
                    masterAuto.Aut_year = DateTime.Today.Year;

                    #endregion AutoNumebr

                    string documntNo = "";
                    Int32 result = CHNLSVC.Inventory.ADJMinus(inHeader, reptPickSerialsList, null, masterAuto, out documntNo);
                    if (result != -99 && result >= 0)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Successfully Saved! Document No : " + documntNo, "Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnClear_Click(null, null);

                        string _repname = string.Empty;
                        string _papersize = string.Empty;
                        BaseCls.GlbReportTp = "OUTWARDWIP";
                        CHNLSVC.General.CheckReportName(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbReportTp, out  _repname, out  BaseCls.ShowComName, out _papersize);

                        if (!(_repname == null || _repname == ""))
                        {
                            //Sanjeewa
                            FF.WindowsERPClient.Reports.Inventory.ReportViewerInventory _views = new FF.WindowsERPClient.Reports.Inventory.ReportViewerInventory();
                            BaseCls.GlbReportName = string.Empty;
                            GlbReportName = string.Empty;
                            _views.GlbReportName = string.Empty;
                            _views.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "Outward_Docs.rpt" : "Outward_Docs.rpt";
                            _views.GlbReportDoc = documntNo;
                            _views.Show();
                            _views = null;
                        }
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(documntNo, "Process Terminated.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select items to request.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please add items to grid.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtItemCode.Clear();
            txtSerial.Clear();
            txtQty.Clear();
            dgvItems.DataSource = new List<Service_stockReturn>();
            txtItemCode.Focus();
            dtpDate.Value = DateTime.Now;
        }

        private void dgvItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvItems.IsCurrentRowDirty)
            {
                dgvItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvItems.SelectedRows.Count > 0 && e.ColumnIndex == 9)
            {
                if (!string.IsNullOrEmpty(dgvItems.SelectedRows[0].Cells["Qty"].Value.ToString()))
                {
                    decimal value = 0;
                    bool successfullyParsed = decimal.TryParse(dgvItems.SelectedRows[0].Cells["Qty"].Value.ToString(), out value);
                    if (successfullyParsed)
                    {
                        List<Service_stockReturn> stockReturnItems = CHNLSVC.CustService.Get_ServiceWIP_StockReturnItems(BaseCls.GlbUserComCode, JobNumber, jobLineNum, string.Empty, BaseCls.GlbUserDefLoca);
                        Service_stockReturn item = stockReturnItems.Find(x => x.ITEM_CODE == dgvItems.SelectedRows[0].Cells["item_code"].Value.ToString());
                        if (Convert.ToDecimal(dgvItems.SelectedRows[0].Cells["Qty"].Value.ToString()) > item.QTY)
                        {
                            MessageBox.Show("Please enter valied quantity", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvItems.SelectedRows[0].Cells["Qty"].Value = item.QTY;
                            return;
                        }
                    }
                }
            }
        }

        private void dgvItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(itemID_KeyPress);
            if (dgvItems.CurrentCell.ColumnIndex == dgvItems.Columns["Qty"].Index)
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
            //if (!char.IsControl(e.KeyChar)
            //    && !char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = true;
            //}
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

        private void dgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dgvItems.Rows[e.RowIndex].Cells["select"].Value = true;
            }
        }

        private void txtSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(null, null);
            }
        }

        private void dgvItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            txtItemCode_DoubleClick(null, null);
        }

        #endregion Events

        #region Methods

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

        public void getItems()
        {
            List<Service_stockReturn> stockReturnItems = new List<Service_stockReturn>();

            stockReturnItems = CHNLSVC.CustService.Get_ServiceWIP_StockReturnItems(BaseCls.GlbUserComCode, JobNumber, jobLineNum, txtItemCode.Text, BaseCls.GlbUserDefLoca);

            if (!string.IsNullOrEmpty(txtSerial.Text))
            {
                stockReturnItems = stockReturnItems.FindAll(x => x.SERIAL_NO == txtSerial.Text);
            }

            dgvItems.DataSource = stockReturnItems;
            modifyGrid();
        }

        private bool validateItem(string itemCode)
        {
            bool status = false;

            List<Service_stockReturn> stockReturnItems = CHNLSVC.CustService.Get_ServiceWIP_StockReturnItems(BaseCls.GlbUserComCode, JobNumber, jobLineNum, itemCode, BaseCls.GlbUserDefLoca);
            if (stockReturnItems.Count > 0)
            {
                status = true;
            }
            return status;
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
                    }
                    else
                    {
                        dgvItems.Rows[i].Cells["Qty"].ReadOnly = true;
                    }
                }
            }
        }

        private bool isAnySelected()
        {
            bool status = false;

            if (dgvItems.Rows.Count > 0)
            {
                for (int i = 0; i < dgvItems.Rows.Count; i++)
                {
                    if (dgvItems.Rows[i].Cells["select"].Value != null && Convert.ToBoolean(dgvItems.Rows[i].Cells["select"].Value.ToString()) == true)
                    {
                        status = true;
                        break;
                    }
                }
            }
            return status;
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

        #endregion Methods

        private void btnCloseFrom_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}