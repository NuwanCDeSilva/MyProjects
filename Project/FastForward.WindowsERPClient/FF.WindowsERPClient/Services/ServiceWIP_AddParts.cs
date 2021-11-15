using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceWIP_AddParts : Base
    {
        private string GblJobNum = string.Empty;
        private Int32 GbljobLineNum = 0;
        private MasterItem _itemdetail = null;
        private List<InventoryWarrantySubDetail> oMailList = new List<InventoryWarrantySubDetail>();

        private Service_job_Det oItem;
        private List<Service_job_Det> oItms;
        private Service_JOB_HDR oHeader;

        public ServiceWIP_AddParts(string job, Int32 jobLine)
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
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
            }

            return paramsText.ToString();
        }

        private void txtItemCode_DoubleClick(object sender, EventArgs e)
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
            { txtItemCode.Clear(); this.Cursor = Cursors.Default; }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
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

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtItemCode_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (cmbStatus.Enabled == false)
                {
                    if (true)
                    {
                    }
                    txtSerial.Focus();
                }
                else
                {
                    cmbStatus.Focus();
                }
            }
        }

        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            txtItemCode_DoubleClick(null, null);
        }

        private void ServiceWIP_AddParts_Load(object sender, EventArgs e)
        {
            clearAll();
            fillItemStatus();
            checkItemCategoryWarrnty();
            loadSerSubItems();
        }

        private bool LoadItemDetail(string _item)
        {
            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;
            lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
            _itemdetail = new MasterItem();

            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item))
                _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
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
                    btnAddItem.Focus();
                }
                else
                {
                    txtQty.Focus();
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

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //  txtRemark.Focus();
                btnAddItem.Focus();
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
            if (checkAvailability())
            {
                MessageBox.Show("Selected Serial is already added.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSerial.Clear();
                txtSerial.Focus();
                return;
            }

            InventoryWarrantySubDetail oItemnew = new InventoryWarrantySubDetail();
            oItemnew.Irsms_ser_id = 0;
            oItemnew.Irsms_ser_line = oMailList.Count + 1;
            oItemnew.Irsms_warr_no = oItem.Jbd_warr;
            oItemnew.Irsms_itm_cd = txtItemCode.Text;
            oItemnew.Irsms_itm_stus = cmbStatus.SelectedValue.ToString();
            oItemnew.Irsms_itm_stus_text = cmbStatus.Text.ToString();
            oItemnew.Irsms_sub_ser = txtSerial.Text;
            oItemnew.Irsms_mfc = "N/A";
            oItemnew.Irsms_tp = _itemdetail.Mi_itm_tp;
            oItemnew.Irsms_warr_period = 0;
            oItemnew.Irsms_warr_rem = "";
            oItemnew.Irsms_act = true;
            oItemnew.Mi_longdesc = lblDesc.Text;
            oItemnew.Irsms_qty = Convert.ToDecimal(txtQty.Text);
            oMailList.Add(oItemnew);
            dgvItems.DataSource = new List<InventoryWarrantySubDetail>();
            dgvItems.DataSource = oMailList;

            if (MessageBox.Show("Do you want to add another item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                txtItemCode.Focus();
            }
            else
            {
                toolStrip1.Focus();
                btnSave.Select();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to close?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear the screen?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                clearAll();
                loadSerSubItems();
            }
        }

        private void clearAll()
        {
            lblMainItem.Text = "";
            lblDesc.Text = "";
            lblItemBrand.Text = "";
            lblItemDescription.Text = "";
            lblItemModel.Text = "";
            lblItemSerialStatus.Text = "";
            lblMainItem.Text = "";
            lblPartNo.Text = "";
            txtItemCode.Clear();
            txtQty.Clear();
            txtSerial.Clear();
            oMailList = new List<InventoryWarrantySubDetail>();
            dgvItems.DataSource = new List<InventoryWarrantySubDetail>();

            oItem = new Service_job_Det();
            oItms = CHNLSVC.CustService.GetJobDetails(GblJobNum, GbljobLineNum, BaseCls.GlbUserComCode);
            if (oItms.Count > 0)
            {
                oItem = oItms[0];
            }
            oHeader = CHNLSVC.CustService.GetServiceJobHeader(GblJobNum, BaseCls.GlbUserComCode);

            lblMainItem.Text = oItem.Jbd_itm_cd;
        }

        private void checkItemCategoryWarrnty()
        {
            string outMSg;
            MST_ITM_CAT_COMP oCateComp;
            if (!CHNLSVC.CustService.CheckItemCategoriWarrantyStatus(BaseCls.GlbUserComCode, oItem.Jbd_itm_cd, out outMSg, out oCateComp))
            {
                MessageBox.Show(outMSg);
                this.Enabled = false;
            }
        }

        private void loadSerSubItems()
        {
            List<InventoryWarrantySubDetail> oItemsOld = CHNLSVC.Inventory.GetSubItemSerials(oItem.Jbd_itm_cd, oItem.Jbd_ser1, Convert.ToInt32(oItem.Jbd_ser_id));
            if (oItemsOld != null && oItemsOld.Count > 0)
            {
                oMailList.AddRange(oItemsOld);
                dgvItems.DataSource = new List<InventoryWarrantySubDetail>();
                dgvItems.DataSource = oMailList;
            }
        }

        private void txtSerial_Leave(object sender, EventArgs e)
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
                if (dtTemp == null || dtTemp.Rows.Count == 0)
                {
                    MessageBox.Show("Please enter a valid serial number.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerial.Clear();
                    txtSerial.Focus();
                    return;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (oMailList == null || oMailList.Count == 0)
            {
                MessageBox.Show("Please add items", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Do you want to save?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                string msg;

                int result = CHNLSVC.CustService.INSERT_INR_SERMSTSUB(oMailList, out msg);
                if (result > 0)
                {
                    MessageBox.Show("Successfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show("Process Terminated" + "\nError :" + msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void dgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Do you want to delete this record?","Question",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        string itemCode = dgvItems.SelectedRows[0].Cells["Irsms_itm_cdD1"].Value.ToString();
                        string serial = dgvItems.SelectedRows[0].Cells["Irsms_sub_serD1"].Value.ToString();

                        oMailList.RemoveAll(x => x.Irsms_sub_ser == serial && x.Irsms_itm_cd == itemCode);
                        dgvItems.DataSource = new List<InventoryWarrantySubDetail>();
                        dgvItems.DataSource = oMailList;
                    }
                }
            }
        }

        private bool checkAvailability()
        {
            bool result = false;

            if (oMailList.Count > 0)
            {
                if (oMailList.FindAll(x => x.Irsms_itm_cd == txtItemCode.Text.Trim() && x.Irsms_sub_ser == txtSerial.Text.Trim()).Count > 0)
                {
                    result = true;
                    return result;
                }
            }

            return result;

        }

        private void clearLevel()
        {
            txtItemCode.Clear();
            txtSerial.Clear();
            txtQty.Clear();
            lblItemDescription.Text = "";
            lblItemModel.Text = "";
            lblItemBrand.Text = "";
            lblItemSerialStatus.Text = "";
            lblDesc.Text = "";
            lblPartNo.Text = "";
        }
    }
}