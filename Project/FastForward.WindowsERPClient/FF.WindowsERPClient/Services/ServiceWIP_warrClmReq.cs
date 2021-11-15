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
    public partial class ServiceWIP_warrClmReq : Base
    {
        private string GblJobNum = string.Empty;
        private Int32 GbljobLineNum = -11;
        private List<Service_SupplierWarrantyClaim> oMainList = new List<Service_SupplierWarrantyClaim>();
        private MasterItem _itemdetail = null;
        private Service_job_Det oItem;
        private List<Service_job_Det> oItms;
        private Service_JOB_HDR oHeader;
        private List<Service_OldPartRemove> MailList = new List<Service_OldPartRemove>();
        private MST_ITM_CAT_COMP oCateComp;
        private DataTable DtTemp;
        private String _job = string.Empty;
        private Int32 _jobline = 0;
        private Int32 _jobSeq = 0;
        private Int32 _sentwcn = 0;
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

        public ServiceWIP_warrClmReq(string job, Int32 jobLine)
        {
            GblJobNum = job;
            GbljobLineNum = jobLine;
            InitializeComponent();
            dgvItems.AutoGenerateColumns = false;
            dgvRequestedItems.AutoGenerateColumns = false;

            dgvItems.Size = new Size(759, 427);
            dgvRequestedItems.Size = new Size(759, 427);
            dgvRequestedItems.Location = new Point(0, 29);
            dgvRequestedItems.Visible = false;

            Service_Chanal_parameter _Parameters = null;
            _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_Parameters != null)
            {
                Serial.HeaderText = _Parameters.SP_DB_SERIAL;
                dgvItems.Columns["Serial"].HeaderText = _Parameters.SP_DB_SERIAL;

                dataGridViewTextBoxColumn7.HeaderText = _Parameters.SP_DB_SERIAL;
                dgvRequestedItems.Columns["dataGridViewTextBoxColumn7"].HeaderText = _Parameters.SP_DB_SERIAL;
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

        private void ServiceWIP_warrClmReq_Load(object sender, EventArgs e)
        {
            btnClear_Click(null, null);
            pnlPeriphals.Visible = false;
            btnView_Click(null, null);
            fillItemStatus();
        }

        #region Events

        private void btnView_Click(object sender, EventArgs e)
        {
            dgvRequestedItems.Visible = false;
            DtTemp = CHNLSVC.CustService.GetSupplierWarrantyClaimItems(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum);
            dgvItems.DataSource = DtTemp;
            modifyGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Int32 result = -1;

            oMainList = new List<Service_SupplierWarrantyClaim>();

            if (dgvItems.Rows.Count > 0)
            {
                if (isAnySelected())
                {
                    if (!NotValidate())
                    {
                        for (int i = 0; i < dgvItems.Rows.Count; i++)
                        {
                            if (dgvItems.Rows[i].Cells["select"].Value != null && Convert.ToBoolean(dgvItems.Rows[i].Cells["select"].Value.ToString()) == true)
                            {
                                Service_SupplierWarrantyClaim item = new Service_SupplierWarrantyClaim();
                                item.SEQ = Convert.ToInt32(dgvItems.Rows[i].Cells["SEQ"].Value.ToString());
                                item.JOB = dgvItems.Rows[i].Cells["JOB"].Value.ToString();
                                item.JOBLINE = Convert.ToInt32(dgvItems.Rows[i].Cells["JOBLINE"].Value.ToString());
                                item.ITEM = dgvItems.Rows[i].Cells["ITEM"].Value.ToString();


                                MasterItemBlock _block = new MasterItemBlock();
                                _block = CHNLSVC.Inventory.GetBlockedItmByCatTp(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, item.ITEM, 7, "P");
                                if (_block != null && !string.IsNullOrEmpty(_block.Mib_itm))
                                {

                                    MessageBox.Show("Item : " + item.ITEM + " is not allow to claim from supplier.", "Blocked Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                _block = CHNLSVC.Inventory.GetBlockedItmByCatTp(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, item.ITEM, 7, "L");
                                if (_block != null && !string.IsNullOrEmpty(_block.Mib_itm))
                                {

                                    MessageBox.Show("Item : " + item.ITEM + " is not allow to claim from supplier.", "Blocked Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                item.ItemStatus = dgvItems.Rows[i].Cells["ItemStatus"].Value.ToString();
                                item.Serial = dgvItems.Rows[i].Cells["Serial"].Value.ToString();
                                item.QTY = Convert.ToDecimal(dgvItems.Rows[i].Cells["QTY"].Value.ToString());
                                item.FROMTABLE = dgvItems.Rows[i].Cells["FROMTABLE"].Value.ToString();
                                item.PartID = dgvItems.Rows[i].Cells["PartID"].Value != null ? dgvItems.Rows[i].Cells["PartID"].Value.ToString() : string.Empty;
                                item.OEM = dgvItems.Rows[i].Cells["OEM"].Value != null ? dgvItems.Rows[i].Cells["OEM"].Value.ToString() : string.Empty;
                                item.CaseID = dgvItems.Rows[i].Cells["CaseID"].Value != null ? dgvItems.Rows[i].Cells["CaseID"].Value.ToString() : string.Empty;
                                item.JBD_ISEXTERNALITM = dgvItems.Rows[i].Cells["JBD_ISEXTERNALITM"].Value != null ? Convert.ToInt32(dgvItems.Rows[i].Cells["JBD_ISEXTERNALITM"].Value.ToString()) : 0;
                                item.JBD_SUPP_CD = dgvItems.Rows[i].Cells["JBD_SUPP_CD"].Value != null ? dgvItems.Rows[i].Cells["JBD_SUPP_CD"].Value.ToString() : string.Empty;
                                item.MI_CATE_1 = dgvItems.Rows[i].Cells["mi_cate_1"].Value != null ? dgvItems.Rows[i].Cells["mi_cate_1"].Value.ToString() : string.Empty;
                                item.MI_CATE_2 = dgvItems.Rows[i].Cells["mi_cate_2"].Value != null ? dgvItems.Rows[i].Cells["mi_cate_2"].Value.ToString() : string.Empty;
                                item.MI_CATE_3 = dgvItems.Rows[i].Cells["mi_cate_3"].Value != null ? dgvItems.Rows[i].Cells["mi_cate_3"].Value.ToString() : string.Empty;
                                item.MI_BRAND = dgvItems.Rows[i].Cells["mi_brand"].Value != null ? dgvItems.Rows[i].Cells["mi_brand"].Value.ToString() : string.Empty;

                                item.JBD_WARR_STUS = dgvItems.Rows[i].Cells["JBD_WARR_STUS"].Value != null ? Convert.ToInt32(dgvItems.Rows[i].Cells["JBD_WARR_STUS"].Value.ToString()) : 0;

                                item.Date = dtpDate.Value.Date;
                                item.JobStage = Convert.ToDecimal("5.1");
                                oMainList.Add(item);
                            }
                        }
                        String ouMsg;
                        result = CHNLSVC.CustService.Service_SupplierClaimRequest(oMainList, BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserSessionID, out ouMsg);

                        if (result != -99 && result >= 0)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("Successfully Saved!", "Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnClear_Click(null, null);
                        }
                        else
                        {
                            if (result == -5)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("Please setup the supplier.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (result == -6)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show(ouMsg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (result == -8)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("Selected supplier cannot process external items.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (result == -9)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("Selected supplier cannot process over warranty items.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("Process Terminated.\n" + ouMsg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select items.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dtpDate.Value = DateTime.Today;
            dgvItems.DataSource = CHNLSVC.CustService.GetSupplierWarrantyClaimItems("XX", "XX", -1);
            oMainList = new List<Service_SupplierWarrantyClaim>();
            dgvRequestedItems.Visible = false;
            chkSelectAll.Checked = false;
            pnlPeriphals.Visible = false;

            oItem = new Service_job_Det();
            oItms = CHNLSVC.CustService.GetJobDetails(GblJobNum, GbljobLineNum, BaseCls.GlbUserComCode);
            if (oItms.Count > 0)
            {
                oItem = oItms[0];
            }
            oHeader = CHNLSVC.CustService.GetServiceJobHeader(GblJobNum, BaseCls.GlbUserComCode);
            _job = "";
            _jobline = 0;
            _jobSeq = 0;
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
            if (dgvItems.CurrentCell.ColumnIndex == dgvItems.Columns["PartID"].Index || dgvItems.CurrentCell.ColumnIndex == dgvItems.Columns["OEM"].Index || dgvItems.CurrentCell.ColumnIndex == dgvItems.Columns["CaseID"].Index)
            {
                dgvItems.Rows[dgvItems.CurrentCell.RowIndex].Cells["select"].Value = true;
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

        private void dgvItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void dgvItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvItems.IsCurrentCellDirty)
            {
                dgvItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvItems_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            modifyGrid();
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            _job = "";
            _jobline = 0;
            _jobSeq = 0;
            DataTable DtTemp = CHNLSVC.CustService.GetSupplierWarrantyClaimRequestedItems(BaseCls.GlbUserComCode, GblJobNum, GbljobLineNum);
            dgvRequestedItems.DataSource = DtTemp;
            dgvRequestedItems.Visible = true;
        }

        private void btnCloseFrom_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            pnlPeriphals.Visible = false;
        }

        private void txtItemCode_DoubleClick(object sender, EventArgs e)
        {
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtItemCode_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnRemoveItem.Focus();
            }
        }

        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCode.Text))
            {
                if (!LoadItemDetail(txtItemCode.Text))
                {
                    MessageBox.Show("Please enter correct item code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemCode.Clear();
                    txtItemCode.Focus();
                    return;
                }
            }
        }

        private void btnSearchItem_Click(object sender, EventArgs e)
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

        private void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            //txtSerial.Enabled = true;
            txtQty.Enabled = true;
        }

        private void btnAddperipharal_Click(object sender, EventArgs e)
        {
            string outMSg;
            if (!CHNLSVC.CustService.CheckItemCategoriWarrantyStatus(BaseCls.GlbUserComCode, oItem.Jbd_itm_cd, out outMSg, out oCateComp))
            {
                MessageBox.Show(outMSg);
            }

            pnlPeriphals.Visible = true;
        }

        private void txtSerial_Leave(object sender, EventArgs e)
        {

        }

        private void txtSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtQty.Enabled == false)
                {
                    btnRemoveItem.Focus();
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
                btnRemoveItem.Focus();
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

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRemoveItem.Focus();
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
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


            if (MessageBox.Show("Do you want to add?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            //if (MailList.Count > 0)
            //{
            //    if (txtSerial.Text == "N/A")
            //    {
            //        if (MailList.FindAll(x => x.SOP_OLDITMCD == txtItemCode.Text && x.SOP_OLDITMSTUS == cmbStatus.SelectedValue.ToString()).Count > 0)
            //        {
            //            Service_OldPartRemove itemExsist = new Service_OldPartRemove();
            //            itemExsist = MailList.Find(x => x.SOP_OLDITMCD == txtItemCode.Text && x.SOP_OLDITMSTUS == cmbStatus.SelectedValue.ToString());
            //            itemExsist.SOP_OLDITMQTY = itemExsist.SOP_OLDITMQTY + Convert.ToDecimal(txtQty.Text);
            //            clerEntryLine();
            //            // bindData();
            //            return;
            //        }
            //    }
            //    //else
            //    //{
            //    //    if (MailList.FindAll(x => x.SOP_OLDITMCD == txtItemCode.Text && x.SOP_OLDITMSTUS == cmbStatus.SelectedValue.ToString() && x.SOP_OLDITMSER1 == txtSerial.Text).Count > 0)
            //    //    {
            //    //        Service_OldPartRemove itemExsist = new Service_OldPartRemove();
            //    //        itemExsist = MailList.Find(x => x.SOP_OLDITMCD == txtItemCode.Text && x.SOP_OLDITMSTUS == cmbStatus.SelectedValue.ToString() && x.SOP_OLDITMSER1 == txtSerial.Text);
            //    //        MessageBox.Show("This serialized item is already added.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    //        return;
            //    //    }
            //    //}
            //}

            Service_OldPartRemove item = new Service_OldPartRemove();
            item.dgvLine = MailList.Count > 0 ? MailList.Count + 1 : 1;
            item.SOP_DT = DateTime.Today;
            item.SOP_COM = BaseCls.GlbUserComCode;
            item.SOP_JOBNO = GblJobNum;
            item.SOP_JOBLINE = GbljobLineNum;
            item.SOP_OLDITMCD = txtItemCode.Text;
            item.SOP_OLDITMSTUS = cmbStatus.SelectedValue.ToString();
            item.SOP_OLDITMSTUS_Text = cmbStatus.Text.ToString();
            item.SOP_OLDITMSER1 = string.Empty;
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
            //item.SOP_RMK = txtRemark.Text;
            item.DESCRIPTION = lblDesc.Text;
            item.PARTNO = lblPartNo.Text;

            item.SOP_TP = "P";

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
            listAddtoTable(item);
            //bindData();

            //toolStrip1.Focus();
            //btnSave.Select();
        }

        #endregion Events

        #region Methods

        private void modifyGrid()
        {
            if (dgvItems.Rows.Count > 0)
            {
                for (int i = 0; i < dgvItems.Rows.Count; i++)
                {
                    if (dgvItems.Rows[i].Cells["Serial"].Value.ToString() == "N/A")
                    {
                        dgvItems.Rows[i].Cells["QTY"].ReadOnly = false;
                    }
                    else
                    {
                        dgvItems.Rows[i].Cells["QTY"].ReadOnly = true;
                    }

                    //if (dgvItems.Rows[i].Cells["REQWCN"].Value != null && dgvItems.Rows[i].Cells["REQWCN"].Value != "" && Convert.ToInt32(dgvItems.Rows[i].Cells["REQWCN"].Value.ToString()) > 0)
                    //{
                    //}
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

        private bool NotValidate()
        {
            bool status = false;

            if (dgvItems.Rows.Count > 0)
            {
                for (int i = 0; i < dgvItems.Rows.Count; i++)
                {
                    if (dgvItems.Rows[i].Cells["select"].Value != null && Convert.ToBoolean(dgvItems.Rows[i].Cells["select"].Value) == true && Convert.ToInt32(dgvItems.Rows[i].Cells["REQWCN"].Value.ToString()) > 0)
                    {
                        if (dgvItems.Rows[i].Cells["CaseID"].Value == null || dgvItems.Rows[i].Cells["CaseID"].Value.ToString() == string.Empty)
                        {
                            MessageBox.Show("Please enter case ID for " + dgvItems.Rows[i].Cells["ITEM"].Value.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            status = true;
                            break;
                        }
                    }
                }
            }
            return status;
        }

        private bool LoadItemDetail(string _item)
        {
            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;
            lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
            _itemdetail = new MasterItem();

            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
            if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
            {
                _isValid = true;
                string _description = _itemdetail.Mi_longdesc;
                string _model = _itemdetail.Mi_model;
                string _brand = _itemdetail.Mi_brand;
                string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Available" : "Non";

                lblItemDescription.Text = "Description : " + _description;
                lblItemModel.Text = "Model : " + _model;
                lblItemBrand.Text = "Brand : " + _brand;
                lblItemSerialStatus.Text = "Serial Status : " + _serialstatus;

                //lblDesc.Text = _description;
                //lblPartNo.Text = _itemdetail.Mi_part_no;

                //if (_itemdetail.Mi_is_ser1 == 0)
                //{
                //    txtSerial.Enabled = false;
                //    txtSerial.Text = "N/A";
                //    txtQty.Enabled = true;
                //    txtQty.Clear();
                //}
                //else
                //{
                //    txtSerial.Enabled = true;
                //    txtSerial.Clear();
                //    txtQty.Enabled = false;
                //    txtQty.Text = "1";
                //}
            }
            else _isValid = false;
            return _isValid;
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
            cmbStatus.Text = "OLD_PART";
        }

        private void clerEntryLine()
        {
            txtItemCode.Clear();
            //txtSerial.Clear();
            cmbStatus.SelectedValue = "OLDPT";
            txtQty.Clear();
            dtpDate.Value = DateTime.Today;

            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;
            lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
            //txtRemark.Clear();
        }

        #endregion Methods

        private void listAddtoTable(Service_OldPartRemove oItemOldpart)
        {

            DataRow drTemp = DtTemp.NewRow();
            drTemp["SEQ"] = -1;
            drTemp["JOB"] = GblJobNum;
            drTemp["JOBLINE"] = GbljobLineNum;
            drTemp["ITEM"] = oItemOldpart.SOP_OLDITMCD;
            drTemp["ITEMSTATUS"] = oItemOldpart.SOP_OLDITMSTUS;
            drTemp["ITEMSTATUSTEXT"] = oItemOldpart.SOP_OLDITMSTUS_Text;
            drTemp["SERIAL"] = oItemOldpart.SOP_OLDITMSER1;
            drTemp["QTY"] = oItemOldpart.SOP_OLDITMQTY;
            drTemp["JBD_WARR_STUS"] = oItemOldpart.SOP_ISSUPPWARR;
            drTemp["JBD_ISEXTERNALITM"] = 0;
            drTemp["FROMTABLE"] = "PERIPHARAL";
            drTemp["JBD_SUPP_CD"] = oItem.Jbd_supp_cd;
            drTemp["MI_CATE_1"] = _itemdetail.Mi_cate_1;
            drTemp["MI_CATE_2"] = _itemdetail.Mi_cate_2;
            drTemp["MI_CATE_3"] = _itemdetail.Mi_cate_3;
            drTemp["MI_BRAND"] = _itemdetail.Mi_brand;
            drTemp["REQWCN"] = 1;
            DtTemp.Rows.Add(drTemp);
            dgvItems.DataSource = DtTemp;
            modifyGrid();
        }

        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvRequestedItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
      
        private void dgvRequestedItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRequestedItems.Rows.Count > 0 && e.RowIndex != -1)
            {

                _job = dgvRequestedItems.Rows[e.RowIndex].Cells["colJob"].Value.ToString();
                 _jobline = Convert.ToInt32( dgvRequestedItems.Rows[e.RowIndex].Cells["colline"].Value.ToString());
                _jobSeq = Convert.ToInt32(dgvRequestedItems.Rows[e.RowIndex].Cells["colseq"].Value.ToString());
                _sentwcn = Convert.ToInt32(dgvRequestedItems.Rows[e.RowIndex].Cells["sentwcn"].Value.ToString());
               
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to cancel?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if(_sentwcn==1)
                {

                    MessageBox.Show("Unable to cancel this already sent WCN", "Unable to cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if ( string.IsNullOrEmpty(_job)  )
                {

                    MessageBox.Show("Only requested items can be cancelled", "Select Job", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string _docNo = string.Empty;
                if (_job != "")
                {
                    int effet = CHNLSVC.CustService.CancelSupplierClaimWarrantyReq(_job, _jobline, _jobSeq, BaseCls.GlbUserID, out _docNo);

                    if (effet > 0)
                    {

                        MessageBox.Show(_docNo, "Cancellation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
        }
    }
}