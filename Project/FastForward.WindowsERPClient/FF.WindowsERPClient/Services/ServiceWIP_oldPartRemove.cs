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
    public partial class ServiceWIP_oldPartRemove : Base
    {
        private string GblJobNum = string.Empty;
        private string _Serial = string.Empty;
        private Int32 GbljobLineNum = -11;
        private MasterItem _itemdetail = null;
        private List<Service_OldPartRemove> MailList = new List<Service_OldPartRemove>();

        private Service_job_Det oItem;
        private List<Service_job_Det> oItms;
        private Service_JOB_HDR oHeader;
        private MST_ITM_CAT_COMP oCateComp;

        public bool IsOldItemsAdded = false;
        public bool AddOldItemAuto { get; set; }

        public ServiceWIP_oldPartRemove(string job, Int32 jobLine)
        {
            GblJobNum = job;
            GbljobLineNum = jobLine;
            InitializeComponent();
            dgvItems.AutoGenerateColumns = false;

            Service_Chanal_parameter _Parameters = null;
            _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_Parameters != null)
            {
                SOP_OLDITMSER1.HeaderText = _Parameters.SP_DB_SERIAL;
                dgvItems.Columns["SOP_OLDITMSER1"].HeaderText = _Parameters.SP_DB_SERIAL;
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
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemSearchComp:
                    {
                        MasterItem otemp = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, oItem.Jbd_itm_cd);
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + otemp.Mi_cate_1 + seperator);
                        break;
                    }
                    break;
            }

            return paramsText.ToString();
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

        private void btnView_Click(object sender, EventArgs e)
        {
            MailList = new List<Service_OldPartRemove>();
            MailList.AddRange(CHNLSVC.CustService.Get_SCV_Oldparts(GblJobNum, GbljobLineNum, txtItemCode.Text, txtSerial.Text));

            if (MailList.Count > 0)
            {
                foreach (Service_OldPartRemove item in MailList)
                {
                    item.dgvLine = MailList.Max(x => x.dgvLine) + 1;
                }
            }

            bindData();

            if (dgvItems.Rows.Count > 0)
            {
                for (int i = 0; i < dgvItems.Rows.Count; i++)
                {
                    if (dgvItems.Rows[i].Cells["SOP_OLDITMSER1"].Value.ToString() == "N/A")
                    {
                        dgvItems.Rows[i].Cells["SOP_OLDITMQTY"].ReadOnly = false;
                    }
                    if (dgvItems.Rows[i].Cells["SOP_TP"].Value.ToString() == "P")
                    {
                        dgvItems.Rows[i].Cells["SOP_TP_text"].Value = "Peripheral";
                    }
                    else
                    {
                        dgvItems.Rows[i].Cells["SOP_TP_text"].Value = "Old Part";
                    }
                }

            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (MailList.Count > 0)
            {
                if (isAnySelected())
                {
                    if (MessageBox.Show("Do you want to process?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    List<Service_OldPartRemove> SaveList = new List<Service_OldPartRemove>();

                    for (int i = 0; i < dgvItems.Rows.Count; i++)
                    {
                        if (dgvItems.Rows[i].Cells["select"].Value != null && Convert.ToBoolean(dgvItems.Rows[i].Cells["select"].Value) == true)
                        {
                            string item = string.Empty;
                            string status = string.Empty;
                            string Serial = string.Empty;

                            

                            item = dgvItems.Rows[i].Cells["SOP_OLDITMCD"].Value.ToString();
                            status = dgvItems.Rows[i].Cells["SOP_OLDITMSTUS"].Value.ToString();

                            //By Akila 2017/05/09
                            string _newSerial = dgvItems.Rows[i].Cells["colNewSerial"].Value == null ? string.Empty : dgvItems.Rows[i].Cells["colNewSerial"].Value.ToString();
                            Serial = string.IsNullOrEmpty(_newSerial) ? dgvItems.Rows[i].Cells["SOP_OLDITMSER1"].Value.ToString() : _newSerial;
                            //Serial = dgvItems.Rows[i].Cells["SOP_OLDITMSER1"].Value.ToString();

                            SaveList.Add(MailList.Find(x => x.SOP_OLDITMCD == item && x.SOP_OLDITMSTUS == status && x.SOP_OLDITMSER1 == Serial));
                        }
                    }
                    string err;
                    int result = CHNLSVC.CustService.Save_OldParts(SaveList, out err);

                    if (result > 0)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("Record insert successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        IsOldItemsAdded = true;
                        btnClear_Click(null, null);
                        return;
                    }
                    else
                    {
                        IsOldItemsAdded = false;
                        Cursor = Cursors.Default;
                        MessageBox.Show("Process Terminated.\n" + err, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    IsOldItemsAdded = false;
                    MessageBox.Show("Please select items to process.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void btnRefix_Click(object sender, EventArgs e)
        {
            if (MailList.Count > 0)
            {
                if (isAnySelected())
                {
                    if (MessageBox.Show("Do you want to re-fix?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    List<Service_OldPartRemove> SaveList = new List<Service_OldPartRemove>();

                    for (int i = 0; i < dgvItems.Rows.Count; i++)
                    {
                        if (dgvItems.Rows[i].Cells["select"].Value != null && Convert.ToBoolean(dgvItems.Rows[i].Cells["select"].Value) == true)
                        {
                            string item = string.Empty;
                            string status = string.Empty;
                            string Serial = string.Empty;
                            decimal DgvQty = 0;

                            item = dgvItems.Rows[i].Cells["SOP_OLDITMCD"].Value.ToString();
                            status = dgvItems.Rows[i].Cells["SOP_OLDITMSTUS"].Value.ToString();

                            //By Akila 2017/05/09
                            string _newSerial = dgvItems.Rows[i].Cells["colNewSerial"].Value == null ? string.Empty : dgvItems.Rows[i].Cells["colNewSerial"].Value.ToString();
                            Serial = string.IsNullOrEmpty(_newSerial) ? dgvItems.Rows[i].Cells["SOP_OLDITMSER1"].Value.ToString() : _newSerial;
                            //Serial = dgvItems.Rows[i].Cells["SOP_OLDITMSER1"].Value.ToString();

                            DgvQty = Convert.ToDecimal(dgvItems.Rows[i].Cells["SOP_OLDITMQTY"].Value.ToString());

                            Service_OldPartRemove tempItem = MailList.Find(x => x.SOP_OLDITMCD == item && x.SOP_OLDITMSTUS == status && x.SOP_OLDITMSER1 == Serial);

                            List<Service_OldPartRemove> DBList = CHNLSVC.CustService.Get_SCV_Oldparts(GblJobNum, GbljobLineNum, txtItemCode.Text, txtSerial.Text);
                            Service_OldPartRemove editedItem = DBList.Find(x => x.SOP_OLDITMCD == item && x.SOP_OLDITMSTUS == status && x.SOP_OLDITMSER1 == Serial);
                            if (editedItem == null)
                            {
                                return;
                            }
                            tempItem.SOP_OLDITMQTY = editedItem.SOP_OLDITMQTY - DgvQty;
                            SaveList.Add(tempItem);
                        }
                    }
                    string err;
                    int result = CHNLSVC.CustService.Update_SCV_Oldpart_Refix(SaveList, out err);

                    if (result > 0)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("Successfully re-fixed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnClear_Click(null, null);
                        btnView_Click(null, null);
                        return;
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("Process Terminated.\n" + err, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please select items to re-fix", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please add items.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtItemCode.Clear();
            txtSerial.Clear();
            //cmbStatus.SelectedValue = "OLDPT"; Sanjeewa 2017-02-09
            txtQty.Clear();
            dtpDate.Value = DateTime.Today;
            dgvItems.DataSource = new List<Service_OldPartRemove>();
            MailList = new List<Service_OldPartRemove>();

            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;
            lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
            chkSelectAll.Checked = false;
            txtRemark.Clear();
            _Serial = "";
            this.colNewSerial.Visible = false;
            oItem = new Service_job_Det();
            oItms = CHNLSVC.CustService.GetJobDetails(GblJobNum, GbljobLineNum, BaseCls.GlbUserComCode);
            if (oItms.Count > 0)
            {
                oItem = oItms[0];
                _Serial = oItms[0].Jbd_ser1;
                if (oItem.Jbd_sentwcn == 1)
                {
                    MessageBox.Show("Job item has send to warranty claim", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                    btnRefix.Enabled = false;
                    return;
                }
            }
            oHeader = CHNLSVC.CustService.GetServiceJobHeader(GblJobNum, BaseCls.GlbUserComCode);
            // BtnRemove.Visible = true;
        }

        private void txtItemCode_DoubleClick(object sender, EventArgs e)
        {
            if (!chkIsPeri.Checked)
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
                {
                    txtItemCode.Clear(); this.Cursor = Cursors.Default;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                    CHNLSVC.CloseAllChannels();
                }
            }
            else
            {
                CommonSearch.CommonSearch _commonSearch = null;
                _commonSearch = new CommonSearch.CommonSearch();
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    _commonSearch.ReturnIndex = 0;
                    _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemSearchComp);
                    DataTable _result = CHNLSVC.CommonSearch.GET_ITEM_COMP(_commonSearch.SearchParams, null, null);
                    _commonSearch.dvResult.DataSource = _result;
                    _commonSearch.BindUCtrlDDLData(_result);
                    _commonSearch.obj_TragetTextBox = txtItemCode;
                    _commonSearch.IsSearchEnter = true;
                    this.Cursor = Cursors.Default;
                    _commonSearch.ShowDialog();
                    txtItemCode.Select();
                }
                catch (Exception ex)
                {
                    txtItemCode.Clear(); this.Cursor = Cursors.Default;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                    CHNLSVC.CloseAllChannels();
                }
            }
        }

        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            string _cat1 = "";
            string _cat2 = "";
            string _cat3 = "";
            if (!string.IsNullOrEmpty(txtItemCode.Text))
            {
                if (!LoadItemDetail(txtItemCode.Text, out _cat1, out _cat2, out _cat3))
                {
                    MessageBox.Show("Please enter correct item code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemCode.Clear();
                    txtItemCode.Focus();
                    return;
                }
                else
                {
                  
                    MasterItem _chkItm =CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode,txtItemCode.Text);
                    DataTable _odata = CHNLSVC.CustService.get_REF_OLD_PART_CAT(_chkItm.Mi_cate_1, _chkItm.Mi_cate_2, _chkItm.Mi_cate_3);
                    if (_odata.Rows.Count > 0)
                    {
                        if (_odata.Rows[0]["PROCESS_PARTH"].ToString() == "A" || _odata.Rows[0]["PROCESS_PARTH"].ToString() == "B")
                        {
                            txtSerial.Text = _Serial;
                        }
                        else
                        {
                            txtSerial.Text = "N/A";
                        }

                    }
                    else
                    { txtSerial.Text = "N/A"; }

                    //if ((_cat1 == "PH" && _cat2 == "MOB" && _cat3 == "MU") || (_cat1 == "PHACC" && _cat2 == "MOACC" && _cat3 == "MPCB"))
                    //{
                    //    txtSerial.Text = _Serial;
                    //}
                    //else
                    //{
                    //    txtSerial.Text = "";
                    //}
                }
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
                //  cmbStatus.Focus();
                cmbStatus.Focus();
            }
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
                MessageBox.Show("Please enter quantity.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQty.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtSerial.Text))
            {
                MessageBox.Show("Please enter serial.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSerial.Focus();
                return;
            }

            _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCode.Text);

            if (_itemdetail.Mi_is_ser1 == 1 && txtSerial.Text == "N/A")
            {
                MessageBox.Show("Serliaze item.Serial Can't be 'N/A'", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //txtSerial.Focus();
                return;
            }

            if (MailList.Count > 0)
            {
                if (txtSerial.Text == "N/A")
                {
                    if (MailList.FindAll(x => x.SOP_OLDITMCD == txtItemCode.Text && x.SOP_OLDITMSTUS == cmbStatus.SelectedValue).Count > 0)
                    {
                        Service_OldPartRemove itemExsist = new Service_OldPartRemove();
                        itemExsist = MailList.Find(x => x.SOP_OLDITMCD == txtItemCode.Text && x.SOP_OLDITMSTUS == cmbStatus.SelectedValue);
                        itemExsist.SOP_OLDITMQTY = itemExsist.SOP_OLDITMQTY + Convert.ToDecimal(txtQty.Text);
                        bindData();
                        clerEntryLine();
                        return;
                    }
                }
                else
                {
                    if (MailList.FindAll(x => x.SOP_OLDITMCD == txtItemCode.Text && x.SOP_OLDITMSTUS == cmbStatus.SelectedValue && x.SOP_OLDITMSER1 == txtSerial.Text).Count > 0)
                    {
                        Service_OldPartRemove itemExsist = new Service_OldPartRemove();
                        itemExsist = MailList.Find(x => x.SOP_OLDITMCD == txtItemCode.Text && x.SOP_OLDITMSTUS == cmbStatus.SelectedValue && x.SOP_OLDITMSER1 == txtSerial.Text);
                        //itemExsist.SOP_OLDITMQTY = itemExsist.SOP_OLDITMQTY + Convert.ToDecimal(txtQty.Text);
                        //bindData();
                        //clerEntryLine();
                        MessageBox.Show("This serialized item is already in the grid view.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            Service_OldPartRemove item = new Service_OldPartRemove();
            //item.SOP_SEQNO             = "";
            item.dgvLine = MailList.Count > 0 ? MailList.Count + 1 : 1;
            item.SOP_DT = DateTime.Today;
            item.SOP_COM = BaseCls.GlbUserComCode;
            item.SOP_JOBNO = GblJobNum;
            item.SOP_JOBLINE = GbljobLineNum;
            item.SOP_OLDITMCD = txtItemCode.Text;
            item.SOP_OLDITMSTUS = cmbStatus.SelectedValue.ToString();
            item.SOP_OLDITMSTUS_Text = cmbStatus.Text.ToString();
            item.SOP_OLDITMSER1 = txtSerial.Text;
            item.SOP_OLDITMWARR = "";
            //item.SOP_OLDSERID =
            item.SOP_OLDITMQTY = Convert.ToDecimal(txtQty.Text);
            item.SOP_DOC_NO = "";
            item.SOP_IS_SETTLED = 0;
            item.SOP_BASE_DOC = "";
            //item.SOP_DOCLINENO = "";
            //item.SOP_REQUESTNO = "";
            item.SOP_REQWCN = 0;
            item.SOP_SENTWCN = 0;
            item.SOP_RECWNC = 0;
            item.SOP_TAKEWCN = 0;
            item.SOP_CRE_BY = BaseCls.GlbUserID;
            item.SOP_CRE_DT = DateTime.Today;
            //item.SOP_REQWCN_DT = "";
            //item.SOP_REQWCN_SYSDT = "";
            //item.SOP_TAKEWCN_DT = "";
            //item.SOP_TAKEWCN_SYSDT = "";
            //item.SOP_PART_CD = "";
            //item.SOP_OEM_NO = "";
            //item.SOP_CASE_ID = "";
            item.SOP_REFIX = 0;

            item.DESCRIPTION = lblDesc.Text;
            item.PARTNO = lblPartNo.Text;

            if (chkIsPeri.Checked)
            {
                item.SOP_TP = "P";
            }

            MailList.Add(item);

            clerEntryLine();
            bindData();
        }

        private void ServiceWIP_oldPartRemove_Load(object sender, EventArgs e)
        {
            btnClear_Click(null, null);
            fillItemStatus();
            if (AddOldItemAuto)
            {
                Auto_RemoveOldPart();
            }
            // checkItemCategoryWarrnty();
        }

        private void cmbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSerial.Text == "N/A")
                {
                    txtQty.Focus();
                }
                else
                {
                    txtSerial.Focus();
                }
            }
        }

        private void txtSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtQty.Enabled == false)
                {
                    //   btnAddItem.Focus();
                    //btnRemoveItem.Focus();
                    txtRemark.Focus();
                }
                else
                {
                    txtQty.Focus();
                }
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //btnRemoveItem.Focus();
                txtRemark.Focus();
            }
        }

        private void dgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Do you want to delete this item?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) ; ;
                    {
                        string item = string.Empty;
                        string status = string.Empty;
                        string Serial = string.Empty;

                        item = dgvItems.SelectedRows[0].Cells["SOP_OLDITMCD"].Value.ToString();
                        status = dgvItems.SelectedRows[0].Cells["SOP_OLDITMSTUS"].Value.ToString();
                        Serial = dgvItems.SelectedRows[0].Cells["SOP_OLDITMSER1"].Value.ToString();

                        MailList.Remove(MailList.Find(x => x.SOP_OLDITMCD == item && x.SOP_OLDITMSTUS == status && x.SOP_OLDITMSER1 == Serial));
                        bindData();
                    }
                }
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

        private void dgvItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvItems.IsCurrentCellDirty)
            {
                dgvItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItemCode.Text))
                {
                    MessageBox.Show("Please enter item code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemCode.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    MessageBox.Show("Please enter quantity.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtSerial.Text))
                {
                    MessageBox.Show("Please enter serial.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerial.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cmbStatus.Text))
                {
                    MessageBox.Show("Please Item Status.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbStatus.Focus();
                    return;
                }

                if (MessageBox.Show("Do you want to add?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                if (MailList.Count > 0)
                {
                    if (txtSerial.Text == "N/A")
                    {
                        if (MailList.FindAll(x => x.SOP_OLDITMCD == txtItemCode.Text && x.SOP_OLDITMSTUS == cmbStatus.SelectedValue.ToString()).Count > 0)
                        {
                            Service_OldPartRemove itemExsist = new Service_OldPartRemove();
                            itemExsist = MailList.Find(x => x.SOP_OLDITMCD == txtItemCode.Text && x.SOP_OLDITMSTUS == cmbStatus.SelectedValue.ToString());
                            itemExsist.SOP_OLDITMQTY = itemExsist.SOP_OLDITMQTY + Convert.ToDecimal(txtQty.Text);
                            clerEntryLine();
                            bindData();
                            return;
                        }
                    }
                    else
                    {
                        if (MailList.FindAll(x => x.SOP_OLDITMCD == txtItemCode.Text && x.SOP_OLDITMSTUS == cmbStatus.SelectedValue.ToString() && x.SOP_OLDITMSER1 == txtSerial.Text).Count > 0)
                        {
                            Service_OldPartRemove itemExsist = new Service_OldPartRemove();
                            itemExsist = MailList.Find(x => x.SOP_OLDITMCD == txtItemCode.Text && x.SOP_OLDITMSTUS == cmbStatus.SelectedValue.ToString() && x.SOP_OLDITMSER1 == txtSerial.Text);
                            MessageBox.Show("This serialized item is already added.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                Service_OldPartRemove item = new Service_OldPartRemove();
                item.dgvLine = MailList.Count > 0 ? MailList.Count + 1 : 1;
                item.SOP_DT = DateTime.Today;
                item.SOP_COM = BaseCls.GlbUserComCode;
                item.SOP_JOBNO = GblJobNum;
                item.SOP_JOBLINE = GbljobLineNum;
                item.SOP_OLDITMCD = txtItemCode.Text;
                item.SOP_OLDITMSTUS = cmbStatus.SelectedValue.ToString();
                item.SOP_OLDITMSTUS_Text = cmbStatus.Text.ToString();
                item.SOP_OLDITMSER1 = txtSerial.Text;
                item.SOP_OLDITMWARR = "";
                item.SOP_OLDITMQTY = Convert.ToDecimal(txtQty.Text);
                item.SOP_DOC_NO = "";
                item.SOP_IS_SETTLED = 0;
                item.SOP_BASE_DOC = "";
                item.SOP_REQWCN = 0;
                item.SOP_SENTWCN = 0;
                item.SOP_RECWNC = 0;
                item.SOP_TAKEWCN = 0;
                item.SOP_CRE_BY = BaseCls.GlbUserID;
                item.SOP_CRE_DT = DateTime.Today;
                item.SOP_REFIX = 0;
                item.SOP_RMK = txtRemark.Text;
                item.DESCRIPTION = lblDesc.Text;
                item.PARTNO = lblPartNo.Text;
                if (chkIsPeri.Checked)
                {
                    item.SOP_TP = "P";
                }
                else
                {
                    item.SOP_TP = "O";
                }
                if (oCateComp != null && oCateComp.Mcc_supp_warr == 1)
                {
                    item.SOP_ISSUPPWARR = 1;
                }
                else
                {
                    item.SOP_ISSUPPWARR = 0;
                }

                MailList.Add(item);
                clerEntryLine();
                bindData();
                AddOldItemAuto = false;
                toolStrip1.Focus();
                btnSave.Select();
            }
            catch (Exception)
            {
                IsOldItemsAdded = false;
                throw;
            }
        }

        private void dgvItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(itemID_KeyPress);
            if (dgvItems.CurrentCell.ColumnIndex == dgvItems.Columns["SOP_OLDITMQTY"].Index)
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
            if (dgvItems.SelectedRows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dgvItems.SelectedRows[0].Cells["SOP_OLDITMQTY"].Value.ToString()))
                {
                    decimal value = 0;

                    bool successfullyParsed = decimal.TryParse(dgvItems.SelectedRows[0].Cells["SOP_OLDITMQTY"].Value.ToString(), out value);

                    if (successfullyParsed)
                    {
                        string item = string.Empty;
                        string status = string.Empty;
                        string Serial = string.Empty;

                        item = dgvItems.SelectedRows[0].Cells["SOP_OLDITMCD"].Value.ToString();
                        status = dgvItems.SelectedRows[0].Cells["SOP_OLDITMSTUS"].Value.ToString();
                        Serial = dgvItems.SelectedRows[0].Cells["SOP_OLDITMSER1"].Value.ToString();
                        decimal oldQty = Convert.ToDecimal(dgvItems.SelectedRows[0].Cells["SOP_OLDITMQTY"].Value.ToString());

                        List<Service_OldPartRemove> DBList = CHNLSVC.CustService.Get_SCV_Oldparts(GblJobNum, GbljobLineNum, txtItemCode.Text, txtSerial.Text);
                        Service_OldPartRemove editedItem = DBList.Find(x => x.SOP_OLDITMCD == item && x.SOP_OLDITMSTUS == status && x.SOP_OLDITMSER1 == Serial);

                        if (editedItem != null && editedItem.SOP_OLDITMQTY != null)
                        {
                            if (oldQty > editedItem.SOP_OLDITMQTY)
                            {
                                MessageBox.Show("You cant re-fix more than " + editedItem.SOP_OLDITMQTY.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dgvItems.SelectedRows[0].Cells["SOP_OLDITMQTY"].Value = editedItem.SOP_OLDITMQTY;
                                return;
                            }
                            if (oldQty != editedItem.SOP_OLDITMQTY)
                            {
                                dgvItems.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
                                //dgvItems.Rows[e.RowIndex].Cells["select"].Value = true;
                                //dgvItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
                            }
                        }
                    }
                }
            }
            //dgvItems_CurrentCellDirtyStateChanged(null, null);
        }

        private void dgvItems_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            if (anError.Context == DataGridViewDataErrorContexts.Commit)
            {
                //MessageBox.Show("Commit error");
            }
        }

        private void dgvItems_DataError_1(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private bool LoadItemDetail(string _item, out string _cat1, out string _cat2, out string _cat3)
        {
            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;
            lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
            _itemdetail = new MasterItem();
            _cat1 = "";
            _cat2 = "";
            _cat3 = "";

            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
            if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
            {
                _isValid = true;
                _cat1 = _itemdetail.Mi_cate_1;
                _cat2 = _itemdetail.Mi_cate_2;
                _cat3 = _itemdetail.Mi_cate_3;
                string _description = _itemdetail.Mi_longdesc;
                string _model = _itemdetail.Mi_model;
                string _brand = _itemdetail.Mi_brand;
                string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Available" : "Non";

                lblItemDescription.Text = "Description : " + _description;
                lblItemModel.Text = "Model : " + _model;
                lblItemBrand.Text = "Brand : " + _brand;
                lblItemSerialStatus.Text = "Serial Status : " + _serialstatus;

                lblDesc.Text = _description;
                lblPartNo.Text = _itemdetail.Mi_part_no;

                if (_itemdetail.Mi_is_ser1 == 0)
                {
                    txtSerial.Enabled = false;
                    txtSerial.Text = "N/A";
                    txtQty.Enabled = true;
                    txtQty.Clear();
                }
                else
                {
                    txtSerial.Enabled = true;
                    txtSerial.Clear();
                    txtQty.Enabled = false;
                    txtQty.Text = "1";
                }

                if (!chkIsPeri.Checked)
                {
                    List<MST_ITM_CAT_COMP> oTemp = CHNLSVC.CustService.getMasterItemCategoryComByItem(_item);
                    if (oTemp != null && oTemp.Count > 0)
                    {
                        _isValid = false;
                    }
                }
            }

            else _isValid = false;
            return _isValid;
        }

        private void fillItemStatus()
        {
            DataTable _tbl = CacheLayer.Get<DataTable>(CacheLayer.Key.CompanyItemStatus.ToString());
            /*var _s = (from L in _tbl.AsEnumerable()
                      select new
                      {
                          MIS_DESC = L.Field<string>("MIS_DESC"),
                          MIC_CD = L.Field<string>("MIC_CD")
                      }).ToList();    */        

            //Sanjeewa 2017-02-09
            string[] names = new string[3];
            names[0] = "DEF";
            names[1] = "OLDPT";
            names[2] = "EXCDF";

            var _s = (from L in _tbl.AsEnumerable().Where(x => x.Field<string>("MIC_CD") == "DEF" || x.Field<string>("MIC_CD") == "OLDPT" || x.Field<string>("MIC_CD") == "EXCDF" || x.Field<string>("MIC_CD") == "INDF")                      
                      select new
                      {
                          MIS_DESC = L.Field<string>("MIS_DESC"),
                          MIC_CD = L.Field<string>("MIC_CD")
                      }).ToList();

            

            //
            
            var _n = new { MIS_DESC = string.Empty, MIC_CD = string.Empty };

            _s.Insert(0, _n);
            cmbStatus.DataSource = _s;
            cmbStatus.DisplayMember = "MIS_DESC";
            cmbStatus.ValueMember = "MIC_CD";
            //cmbStatus.Text = "OLD_PART";
        }

        private void bindData()
        {
            dgvItems.DataSource = new List<Service_OldPartRemove>();
            if (MailList.Count > 0)
            {
                dgvItems.DataSource = MailList;
                for (int i = 0; i < dgvItems.Rows.Count; i++)
                {
                    if (dgvItems.Rows[i].Cells["SOP_OLDITMSER1"].Value.ToString() == "N/A")
                    {
                        dgvItems.Rows[i].Cells["SOP_OLDITMQTY"].ReadOnly = false;
                    }
                    if (dgvItems.Rows[i].Cells["SOP_TP"].Value.ToString() == "P")
                    {
                        dgvItems.Rows[i].Cells["SOP_TP_text"].Value = "Peripheral";
                    }
                    else
                    {
                        dgvItems.Rows[i].Cells["SOP_TP_text"].Value = "Old Part";
                    }
                }
            }

            txtItemCode.Focus();
        }

        private void clerEntryLine()
        {
            txtItemCode.Clear();
            txtSerial.Clear();
            //cmbStatus.SelectedValue = "OLDPT";
            txtQty.Clear();
            dtpDate.Value = DateTime.Today;

            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;
            lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
            txtRemark.Clear();
        }

        private bool isAnySelected()
        {
            bool status = false;

            if (dgvItems.Rows.Count > 0)
            {
                for (int i = 0; i < dgvItems.Rows.Count; i++)
                {
                    if (dgvItems.SelectedRows[0].Cells["select"].Value != null && Convert.ToBoolean(dgvItems.SelectedRows[0].Cells["select"].Value) == true)
                    {
                        status = true;
                        break;
                    }
                }
            }
            return status;
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRemoveItem.Focus();
            }
        }

        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            txtItemCode_DoubleClick(null, null);
        }

        private void btnCloseFrom_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkItemCategoryWarrnty()
        {
            string outMSg;
            if (!CHNLSVC.CustService.CheckItemCategoriWarrantyStatus(BaseCls.GlbUserComCode, oItem.Jbd_itm_cd, out outMSg, out oCateComp))
            {
                MessageBox.Show(outMSg);
            }
        }

        private void txtSerial_Leave(object sender, EventArgs e)
        {
            if (chkIsPeri.Checked)
            {
                if (!string.IsNullOrEmpty(txtSerial.Text))
                {
                    if (txtSerial.Text.Trim() == "N/A"
                        || txtSerial.Text.Trim() == "NA"
                        || txtSerial.Text.Trim() == "N A"
                        || txtSerial.Text.Trim() == @"N\A"
                        || txtSerial.Text.Trim() == "N-A"
                        || txtSerial.Text.Trim() == "N_A"
                        || txtSerial.Text.Trim() == "N/A")
                    {
                        MessageBox.Show("Please enter a valid serial number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSerial.Clear();
                        txtSerial.Focus();
                        return;
                    }

                    DataTable dtTemp = CHNLSVC.Inventory.CheckSerialAvailability("SERIAL1", txtItemCode.Text, txtSerial.Text.Trim());
                    if (dtTemp != null && dtTemp.Rows.Count > 0)
                    {
                        MessageBox.Show("Please enter a valid serial number.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSerial.Clear();
                        txtSerial.Focus();
                        return;
                    }
                    List<InventoryWarrantySubDetail> oItmwsTemp = CHNLSVC.Inventory.GetSubItemSerials(string.Empty, txtSerial.Text, 0);
                    if (oItmwsTemp != null && oItmwsTemp.Count > 0 && oItmwsTemp.FindAll(x => x.Irsms_act == true).Count > 0)
                    {
                        MessageBox.Show("Please enter a valid serial number.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSerial.Clear();
                        txtSerial.Focus();
                        return;
                    }
                }
            }
        }

        private void ddPanBox1_Click(object sender, EventArgs e)
        {

        }

        private void chkIsPeri_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsPeri.Checked)
            {
                checkItemCategoryWarrnty();
            }
        }

        private void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            txtSerial.Enabled = true;
            txtQty.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MailList.Count > 0)
            {
                if (MessageBox.Show("Do you want to remove?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }


                if (AddOldItemAuto) 
                {
                    int _selactedRowCount = 0;
                    if (dgvItems.Rows.Count > 0)
                    {
                        DataTable _removedOldParts = new DataTable();
                        _removedOldParts = CHNLSVC.CustService.GetPartRemoveByJobline(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum);

                        foreach (DataGridViewRow row in dgvItems.Rows)
                        {
                            if (Convert.ToBoolean(row.Cells["select"].Value))
                            {                                
                                string _newSerial = row.Cells["colNewSerial"].Value == null ? string.Empty : row.Cells["colNewSerial"].Value.ToString();
                                if (string.IsNullOrEmpty(_newSerial))
                                {
                                    MessageBox.Show("Please Enter New Serial #", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                                    dgvItems.Rows[row.Index].Selected = true;
                                    return;
                                }

                                //Add by akila - get removed part details of selected job                            
                                if (_removedOldParts.Rows.Count > 0)
                                {
                                    string _newSerialNo = row.Cells["colNewSerial"].Value == null ? string.Empty : row.Cells["colNewSerial"].Value.ToString();
                                    string _item = row.Cells["SOP_OLDITMCD"].Value == null ? string.Empty : row.Cells["SOP_OLDITMCD"].Value.ToString();

                                    foreach (DataRow _oldPart in _removedOldParts.Rows)
                                    {
                                        string _itemCode = _oldPart["task_ref"] == DBNull.Value ? string.Empty : _oldPart["task_ref"].ToString();
                                        string _serialNo = _oldPart["serial_no"] == DBNull.Value ? string.Empty : _oldPart["serial_no"].ToString();

                                        // prevent removing same item continously;
                                        if ((_item == _itemCode) && (_newSerialNo == _serialNo))
                                        {
                                            MessageBox.Show("This part all ready has been removed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                                            dgvItems.Rows[row.Index].Selected = true;
                                            return;
                                        }
                                    }
                                }
                                _selactedRowCount += 1;
                            }
                        }
                        if (_selactedRowCount > (dgvItems.Rows.Count - _removedOldParts.Rows.Count))
                        {
                            MessageBox.Show("Cannot remove parts. Some of the parts all ready have been removed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Cursor = DefaultCursor;
                            return;
                        }
                    }

                    if (_selactedRowCount < 1)
                    {
                        MessageBox.Show("Please select a part to remove", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Cursor = DefaultCursor;
                        return;
                    }

                    UpdateRemovePartList(); 
                }// Akial 2017/05/09 will be removed selected items only               

                string err;
                int result = CHNLSVC.CustService.Save_OldParts(MailList, out err);

                if (result > 0)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("Record insert successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null, null);
                    btnView_Click(null, null);
                    return;
                }
                else
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("Process Terminated.\n" + err, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please add items.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ddPanBox1_Click_1(object sender, EventArgs e)
        {

        }

        public void Auto_RemoveOldPart()
        {

            try
            {
                List<Service_stockReturn> SelectedParts = new List<Service_stockReturn>();
                SelectedParts = CHNLSVC.CustService.Get_ServiceWIP_ViewStockItems(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum, "", BaseCls.GlbUserDefLoca);

                //if (SelectedParts != null)
                //{
                //    if (SelectedParts.Count > 0)
                //    {
                //        //Add by akila - get removed part details of selected job
                //        DataTable _removedOldParts = new DataTable();
                //        _removedOldParts = CHNLSVC.CustService.GetPartRemoveByJobline(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum);
                //        if (_removedOldParts.Rows.Count > 0)
                //        {
                //            foreach (DataRow _oldPart in _removedOldParts.Rows)
                //            {
                //                string _itemCode = _oldPart["task_ref"] == DBNull.Value ? string.Empty : _oldPart["task_ref"].ToString();
                //                string _serialNo = _oldPart["serial_no"] == DBNull.Value ? string.Empty : _oldPart["serial_no"].ToString();

                //                // remove previously removed item from list. this function add to prevent removing same item continously
                //                SelectedParts.RemoveAll(x => x.ITEM_CODE == _itemCode && x.SERIAL_NO == _serialNo); 
                //            }
                //        }
                //    }
                //}

                MailList = new List<Service_OldPartRemove>();
                if (SelectedParts != null)
                {
                    if (SelectedParts.Count > 0)
                    {
                        foreach (Service_stockReturn _parts in SelectedParts)
                        {
                            Service_OldPartRemove item = new Service_OldPartRemove();
                            item.dgvLine += 1; 
                            item.SOP_DT = DateTime.Today;
                            item.SOP_COM = BaseCls.GlbUserComCode;
                            item.SOP_JOBNO = GblJobNum;
                            item.SOP_JOBLINE = GbljobLineNum;
                            item.SOP_OLDITMCD = _parts.ITEM_CODE;
                            item.SOP_OLDITMSTUS = "OLDPT";
                            item.SOP_OLDITMSTUS_Text = "OLD_PART";
                            item.SOP_OLDITMSER1 = "N/A";
                            item.SOP_OLDITMWARR = "";
                            item.SOP_OLDITMQTY = Convert.ToDecimal(_parts.QTY);
                            item.SOP_DOC_NO = "";
                            item.SOP_IS_SETTLED = 0;
                            item.SOP_BASE_DOC = "";
                            item.SOP_REQWCN = 0;
                            item.SOP_SENTWCN = 0;
                            item.SOP_RECWNC = 0;
                            item.SOP_TAKEWCN = 0;
                            item.SOP_CRE_BY = BaseCls.GlbUserID;
                            item.SOP_CRE_DT = DateTime.Today;
                            item.SOP_REFIX = 0;
                            item.SOP_RMK = "";
                            item.DESCRIPTION = _parts.Desc;
                            item.PARTNO = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _parts.ITEM_CODE).Mi_part_no;
                            item.SOP_TP = "O";
                            item.SOP_ISSUPPWARR = 0;
                            item.SOP_OLDSERID = string.IsNullOrEmpty(_parts.SERIAL_ID) ? 0 : Convert.ToInt32(_parts.SERIAL_ID); //Add by akila
                            MailList.Add(item);
                        }

                        bindData();
                        this.colNewSerial.Visible = true;
                        toolStrip1.Focus();
                        btnSave.Select();
                        IsOldItemsAdded = true;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while processing old item details" + Environment.NewLine + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor = DefaultCursor;
                return;
            }
        }

        private void UpdateRemovePartList()
        {
            try
            {
                if (dgvItems.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvItems.Rows)
                    {
                        if (row.Cells["select"].Value != null && Convert.ToBoolean(row.Cells["select"].Value) == true)
                        {
                           // if (AddOldItemAuto)
                           // {
                                string _newSerial = row.Cells["colNewSerial"].Value == null ? string.Empty : row.Cells["colNewSerial"].Value.ToString();
                                string _item = row.Cells["SOP_OLDITMCD"].Value.ToString();
                                string _serial = row.Cells["SOP_OLDITMSER1"].Value.ToString();
                                if ((!string.IsNullOrEmpty(_newSerial)) && (!string.IsNullOrWhiteSpace(_newSerial)))
                                {
                                    MailList.Where(x => x.SOP_OLDITMCD == _item && x.SOP_OLDITMSER1 == _serial).ToList().ForEach(x => x.SOP_OLDITMSER1 = _newSerial);
                                }
                           // }
                        }
                       // else { if (MailList.Count > 0) { MailList.RemoveAt(row.Index); }}
                    }
                    BindingSource _dgvSource = new BindingSource();
                    _dgvSource.DataSource = MailList;
                    dgvItems.DataSource = _dgvSource;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while updating old item serial" + Environment.NewLine + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor = DefaultCursor;
            }
        }

        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex != -1)
            //{
            //    if (e.ColumnIndex == 1)
            //    {
            //        if (Convert.ToBoolean(dgvItems.Rows[e.RowIndex].Cells["select"].Value) == true)
            //        {
            //            if (MessageBox.Show("Do you want to delete this item?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            //            {
            //                if (AddOldItemAuto)
            //                {
            //                    string _newSerial = dgvItems.Rows[e.RowIndex].Cells["colNewSerial"].Value == null ? string.Empty : dgvItems.Rows[e.RowIndex].Cells["colNewSerial"].Value.ToString();
            //                    if (string.IsNullOrEmpty(_newSerial))
            //                    {
            //                        MessageBox.Show("Please Enter New Serial #", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                        dgvItems.Rows.Cast<DataGridViewRow>().Where(x => x.Index == e.RowIndex).ToList().ForEach(x => x.Cells["select"].Value = false);
            //                        Cursor = DefaultCursor;
            //                        dgvItems.Refresh();
            //                        return;
            //                    }
            //                    else
            //                    {
            //                        //Add by akila - get removed part details of selected job
            //                        DataTable _removedOldParts = new DataTable();
            //                        _removedOldParts = CHNLSVC.CustService.GetPartRemoveByJobline(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum);
            //                        if (_removedOldParts.Rows.Count > 0)
            //                        {
            //                            string _newSerialNo = dgvItems.Rows[e.RowIndex].Cells["colNewSerial"].Value == null ? string.Empty : dgvItems.Rows[e.RowIndex].Cells["colNewSerial"].Value.ToString();
            //                            string _item = dgvItems.Rows[e.RowIndex].Cells["SOP_OLDITMCD"].Value == null ? string.Empty : dgvItems.Rows[e.RowIndex].Cells["SOP_OLDITMCD"].Value.ToString();

            //                            foreach (DataRow _oldPart in _removedOldParts.Rows)
            //                            {
            //                                string _itemCode = _oldPart["task_ref"] == DBNull.Value ? string.Empty : _oldPart["task_ref"].ToString();
            //                                string _serialNo = _oldPart["serial_no"] == DBNull.Value ? string.Empty : _oldPart["serial_no"].ToString();

            //                                // prevent removing same item continously;
            //                                if ((_item == _itemCode) && (_newSerialNo == _serialNo))
            //                                {
            //                                    MessageBox.Show("This part all ready has been removed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                                    dgvItems.Rows.Cast<DataGridViewRow>().Where(x => x.Index == e.RowIndex).ToList().ForEach(x => x.Cells["select"].Value = false);
            //                                    Cursor = DefaultCursor;
            //                                    return;
            //                                }
            //                            }
            //                        }

            //                    }
            //                }
            //            }
            //            else
            //            {
            //                dgvItems.Rows.Cast<DataGridViewRow>().Where(x => x.Index == e.RowIndex).ToList().ForEach(x => x.Cells["select"].Value = false);
            //            }
            //        }
            //    }
            //}
        }
    }
}