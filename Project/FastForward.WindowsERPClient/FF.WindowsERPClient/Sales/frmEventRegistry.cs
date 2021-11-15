using FF.BusinessObjects;
using FF.BusinessObjects.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FF.WindowsERPClient.Sales
{
    public partial class frmEventRegistry : Base
    {
        private CommonSearch.CommonSearch _commonSearch = null;
        private List<EventItems> _EventItems = new List<EventItems>();
        private List<EventRegistry> _EventList = new List<EventRegistry>();
        public List<EventRegistry> PickedEvents { get; set; }
        public List<EventItems> PickedEventItem { get; set; }
        public bool IsAddItem { get; set; }
        public frmEventRegistry()
        {
            InitializeComponent();
        }

        private void ClearScreen()
        {            
            txtCustCode.Text = string.Empty;
            txtCustName.Text = string.Empty;
            txtDeliAddress.Text = string.Empty;
            txtEventDate.Text = string.Empty;
            txtDeliveryDate.Text = string.Empty;
            txtEventType.Text = string.Empty;
            txtEventDesciption.Text = string.Empty;

            _EventItems = new List<EventItems>();
            _EventList = new List<EventRegistry>();

            BindingSource _source = new BindingSource();
            _source.DataSource = _EventItems
                .Select(x =>
                    new
                    {
                        SERE_LINE = x.SERE_LINE,
                        SERE_ITM_CD = x.SERE_ITM_CD,
                        SERE_ITM_STUS = x.SERE_ITM_STUS,
                        SERE_ITM_QTY = x.SERE_ITM_QTY,
                        SERE_ITM_SOLD = x.SERE_ITM_SOLD,
                        SERE_PB = x.SERE_PB,
                        SERE_PB_LVL = x.SERE_PB_LVL,
                        SERE_ITM_PRICE = x.SERE_ITM_PRICE
                    }).OrderBy(o => o.SERE_LINE).ToList();

            dgvItem.DataSource = _source;

            dgvItem.Refresh();

            pnlItem.Visible = false;
            IsAddItem = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEventId.Text = string.Empty;
            ClearScreen();
        }

        private void SearchEnvents()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.IsSearchEnter = true;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RegisterdEvents);
                DataTable _result = CHNLSVC.CommonSearch.SearchEvents(_commonSearch.SearchParams, null, null);
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtEventId;                
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtEventId.Select();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void LoadEventDetails()
        {
            try
            {
                ClearScreen();

                if (string.IsNullOrEmpty(txtEventId.Text.Trim()))
                {
                    MessageBox.Show("Please enter event ID !", "Event Registry - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    _EventList = new List<EventRegistry>();
                    _EventList = CHNLSVC.Sales.GetEventDetById(txtEventId.Text.Trim());
                    if (_EventList != null && _EventList.Count > 0)
                    {
                        foreach (EventRegistry _event in _EventList)
                        {
                            txtEventType.Text = _event.SERE_EVE_TP.ToUpper();
                            txtEventDesciption.Text = _event.SERE_DESC.ToUpper();
                            txtEventDate.Text = _event.SERE_EVE_DT.ToShortDateString().ToUpper();
                            txtDeliveryDate.Text = _event.SERE_DEL_DT.ToShortDateString().ToUpper();
                            txtCustCode.Text = _event.SERE_CUST_CD.ToUpper();
                            txtCustName.Text = (!string.IsNullOrEmpty(_event.SERE_NAME_ONE)) && (!string.IsNullOrEmpty(_event.SERE_NAME_TWO)) ? _event.SERE_NAME_ONE.ToUpper() + " " + _event.SERE_NAME_TWO.ToUpper() : _event.SERE_NAME_ONE.ToUpper();
                            txtDeliAddress.Text = _event.SERE_DEL_ADDRESS.ToUpper();

                            //Bind item details
                            _EventItems = new List<EventItems>();
                            _EventItems = CHNLSVC.Sales.GetEventItemsByEventId(txtEventId.Text.Trim());
                            if (_EventItems != null && _EventItems.Count > 0)
                            {
                                //remove already selected item from list
                                if (PickedEventItem != null && PickedEventItem.Count > 0)
                                {
                                    foreach (EventItems _item in PickedEventItem)
                                    {
                                        if (_item.IsSelected)
                                        {
                                            _EventItems.RemoveAll(x => x.SERE_LINE == _item.SERE_LINE && x.SERE_ITM_CD == _item.SERE_ITM_CD);
                                        }
                                    }
                                }

                                if (_EventItems != null && _EventItems.Count > 0)
                                {
                                    BindingSource _source = new BindingSource();
                                    _source.DataSource = _EventItems
                                        .Select(x =>
                                            new
                                            {
                                                SERE_LINE = x.SERE_LINE,
                                                SERE_ITM_CD = x.SERE_ITM_CD,
                                                SERE_ITM_STUS = x.SERE_ITM_STUS,
                                                SERE_ITM_QTY = x.SERE_ITM_QTY,
                                                SERE_ITM_SOLD = x.SERE_ITM_SOLD,
                                                SERE_PB = x.SERE_PB,
                                                SERE_PB_LVL = x.SERE_PB_LVL,
                                                SERE_ITM_PRICE = x.SERE_ITM_PRICE
                                            }).OrderBy(o => o.SERE_LINE).ToList();

                                    dgvItem.DataSource = _source;
                                    dgvItem.Refresh();
                                    pnlItem.Visible = true;
                                }                                
                            }
                            else 
                            {
                                MessageBox.Show("Event item details not found !", "Event Registry - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Event details not found !", "Event Registry - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEventId.Text = string.Empty;
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while validating event details" + Environment.NewLine + ex.Message, "Event Registry - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnSearchEvent_Click(object sender, EventArgs e)
        {
            SearchEnvents();
        }

        private void txtEventId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadEventDetails();
            }
            else if (e.KeyCode == Keys.F2)
            {
                SearchEnvents();
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {                
                case CommonUIDefiniton.SearchUserControlType.RegisterdEvents:
                    {
                        paramsText.Append(seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void txtEventId_DoubleClick(object sender, EventArgs e)
        {
            SearchEnvents();
        }

        private void AddItem()
        {
            try
            {
                DateTime _evenDate = DateTime.Now;
                DateTime.TryParse(txtEventDate.Text, out _evenDate);

                if (string.IsNullOrEmpty(txtEventId.Text.Trim()))
                {
                    MessageBox.Show("Please enter event ID !", "Event Registry - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    IsAddItem = false;
                    return;
                }
                else if (_EventItems == null || _EventItems.Count < 1)
                {
                    MessageBox.Show("Event item details not found", "Event Registry - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    IsAddItem = false;
                    return;
                }
                else if (_evenDate.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("Invalid event. Event date has expired", "Event Registry - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    IsAddItem = false;
                    return;
                }
                else
                {
                    if (dgvItem.Rows.Count > 0)
                    {
                        var _selectedItems = dgvItem.Rows.Cast<DataGridViewRow>().Where(x =>x.Cells["ItemSelected"].Value != null).ToList();
                        if (_selectedItems == null || _selectedItems.Count < 1)
                        {
                            MessageBox.Show("Please select a item", "Add Event Items", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            IsAddItem = false;
                            return;
                        }

                        DialogResult _result = MessageBox.Show("Do you want to add selected items ?", "Add Event Items", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_result == System.Windows.Forms.DialogResult.No)
                        {
                            IsAddItem = false;
                            return;
                        }
                        else
                        {
                            foreach (DataGridViewRow _dataRow in dgvItem.Rows)
                            {
                                bool tmpSelected = _dataRow.Cells["ItemSelected"].Value == DBNull.Value ? false : Convert.ToBoolean(_dataRow.Cells["ItemSelected"].Value);

                                Int32 _tmpLineNo = 0;
                                Int32.TryParse(_dataRow.Cells["ItemLine"].Value.ToString(), out _tmpLineNo);
                                string _tmpItem = _dataRow.Cells["ItemCode"].Value.ToString();
                                Int32 _pickQty = _dataRow.Cells["PickQty"].Value == null ? 0 : Convert.ToInt32(_dataRow.Cells["PickQty"].Value);

                                if (_EventItems != null && _EventItems.Count > 0)
                                {
                                    List<EventItems> _tmpEventItems = new List<EventItems>();
                                    _tmpEventItems = _EventItems.Where(x => x.SERE_LINE == _tmpLineNo && x.SERE_ITM_CD == _tmpItem).ToList();
                                    if (_tmpEventItems != null && _tmpEventItems.Count > 0)
                                    {
                                        foreach (EventItems _eventItem in _tmpEventItems)
                                        {
                                            //update if available
                                            int _existingCount = 0;
                                            if (PickedEventItem != null && PickedEventItem.Count > 0)
                                            {
                                                var _existingItem = PickedEventItem.Where(x => x.SERE_LINE == _eventItem.SERE_LINE && x.SERE_ITM_CD == _eventItem.SERE_ITM_CD).ToList();
                                                if (_existingItem != null && _existingItem.Count > 0)
                                                {
                                                    _existingCount = _existingItem.Count;
                                                }
                                            }

                                            if (_existingCount > 0)
                                            {
                                                if (tmpSelected)
                                                {
                                                    PickedEventItem.Where(x => x.SERE_LINE == _eventItem.SERE_LINE && x.SERE_ITM_CD == _eventItem.SERE_ITM_CD && x.IsSelected == false).ToList()
                                                    .ForEach(y =>
                                                    {
                                                        y.IsSelected = tmpSelected;
                                                        y.SelectedQty = _pickQty;
                                                        y.SERE_UPDATE = 2;// if invoice from web 1 , windows - 2
                                                        y.SERE_UPDATE_BY = BaseCls.GlbUserID;
                                                        y.SERE_UPDATE_DT = DateTime.Now;
                                                        y.SERE_UPDATE_SESSION = GlbUserSessionID;
                                                    });
                                                }
                                            }
                                            else
                                            {
                                                if (tmpSelected)
                                                {
                                                    _eventItem.IsSelected = tmpSelected;
                                                    _eventItem.SelectedQty = _pickQty;
                                                    _eventItem.SERE_UPDATE = 2;// if invoice from web 1 , windows - 2
                                                    _eventItem.SERE_UPDATE_BY = BaseCls.GlbUserID;
                                                    _eventItem.SERE_UPDATE_DT = DateTime.Now;
                                                    _eventItem.SERE_UPDATE_SESSION = GlbUserSessionID;
                                                }                                                
                                                PickedEventItem.Add(_eventItem);
                                            }
                                        }
                                    }                                                                                                                       
                                }
                            }
                            PickedEvents = _EventList;
                            IsAddItem = true;
                            this.Close();
                        }                      
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System error" + Environment.NewLine + ex.Message, "Event Registry - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void dgvItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvItem.CommitEdit(DataGridViewDataErrorContexts.Commit);

            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (dgvItem.Rows.Count > 0)
                    {
                        bool _isSelected = dgvItem.Rows[e.RowIndex].Cells["ItemSelected"].Value == DBNull.Value ? false : Convert.ToBoolean(dgvItem.Rows[e.RowIndex].Cells["ItemSelected"].Value);
                        if (_isSelected)
                        {
                            Int32 _lineNo = 0;
                            Int32.TryParse(dgvItem.Rows[e.RowIndex].Cells["ItemLine"].Value.ToString() , out _lineNo);

                            Int32 _pickQty = dgvItem.Rows[e.RowIndex].Cells["PickQty"].Value == null ? 0 : Convert.ToInt32(dgvItem.Rows[e.RowIndex].Cells["PickQty"].Value.ToString());
                            string _itemCode = dgvItem.Rows[e.RowIndex].Cells["ItemLine"].Value == null ? string.Empty : dgvItem.Rows[e.RowIndex].Cells["ItemLine"].Value.ToString();
                            decimal _itmQty  = dgvItem.Rows[e.RowIndex].Cells["ItemQty"].Value == null ? 0 : (decimal)dgvItem.Rows[e.RowIndex].Cells["ItemQty"].Value;
                            decimal _soldQty = dgvItem.Rows[e.RowIndex].Cells["SoldQty"].Value == null ? 0 : (decimal)dgvItem.Rows[e.RowIndex].Cells["SoldQty"].Value;

                            if (_pickQty == 0)
                            {
                                dgvItem.Rows[e.RowIndex].Cells["ItemSelected"].Value = false;
                                dgvItem.CommitEdit(DataGridViewDataErrorContexts.Commit);
                                MessageBox.Show("Please enter the pick qty", "Event Registry - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (_pickQty < 0)
                            {
                                dgvItem.Rows[e.RowIndex].Cells["ItemSelected"].Value = false;
                                dgvItem.CommitEdit(DataGridViewDataErrorContexts.Commit);
                                MessageBox.Show("Selected qty is invalid", "Event Registry - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }                            

                            decimal _availableItemQty = _itmQty - _soldQty;

                            if (_pickQty > _availableItemQty)
                            {
                                dgvItem.Rows[e.RowIndex].Cells["ItemSelected"].Value = false;
                                dgvItem.CommitEdit(DataGridViewDataErrorContexts.Commit);
                                MessageBox.Show("Selected qty cannot be greater than available qty", "Event Registry - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (_availableItemQty < 1)
                            {
                                dgvItem.Rows[e.RowIndex].Cells["ItemSelected"].Value = false;
                                dgvItem.CommitEdit(DataGridViewDataErrorContexts.Commit);
                                MessageBox.Show("Item balance is 0", "Event Registry - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }


                            if (PickedEventItem != null && PickedEventItem.Count > 0)
                            {
                                int _pickedCount = PickedEventItem.Where(x => x.SERE_LINE == _lineNo && x.SERE_CUST_CD == _itemCode).ToList().Count;
                                if (_pickedCount > 0)
                                {                                    
                                    MessageBox.Show("This item has already selected !", "Event Registry - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    dgvItem.Rows[e.RowIndex].Cells["ItemSelected"].Value = false;
                                    dgvItem.CommitEdit(DataGridViewDataErrorContexts.Commit);
                                    return;
                                }
                            }
                        }
                    }
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show("System error" + Environment.NewLine + ex.Message, "Event Registry - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }                      
        }

        private void txtEventId_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEventId.Text))
            {
                LoadEventDetails();
            }
        }
    }
}
