using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using FF.BusinessObjects;
using System.Runtime.InteropServices;

namespace FF.WindowsERPClient.Enquiries.Inventory
{
    public partial class SerialTracker : FF.WindowsERPClient.Base
    {
        DataTable _serviceHistory  = new DataTable();
        private void InitializeForm()
        {
            ClearBar7();
            ClearBar6();
            ClearBar5();
            ClearBar4(false);
            ClearBar3();
            ClearBar2();
            ClearBar1(true);
            pnlMultipleItem.Size = new Size(833, 154);
            gvMovement.AutoGenerateColumns = false;
            gvSubSerial.AutoGenerateColumns = false;
            gvSale.AutoGenerateColumns = false;
            gvMultipleItem.AutoGenerateColumns = false;
        }
        public SerialTracker()
        {
            InitializeComponent();
            InitializeForm();
        }

        #region Clear Screen
        private void ClearBar1(bool _isMustFocus)
        {
            cmbSerialType.SelectedIndex = 0;
            txtSerialNo.Clear();
            cmbCaseType.SelectedIndex = 0;
            chkWholeWord.Checked = true;
            lblItem.Text = string.Empty;
            pnlItem.Visible = false;
            if (_isMustFocus) txtSerialNo.Focus();
        }
        private void ClearBar2()
        {
            lblCurLocation.Text = string.Empty;
            lblCurCompany.Text = string.Empty;
            lblCurReceivedDate.Text = string.Empty;
            lblCurBin.Text = string.Empty;
            lblCurItemStatus.Text = string.Empty;
        }
        private void ClearBar3()
        {
            lblItemDescription.Text = "Description : ";
            lblItemModel.Text = "Model : ";
            lblItemBrand.Text = "Brand : ";
        }
        private void ClearBar4(bool _isAdvVisible)
        {
            lblItmColor.Text = string.Empty;
            lblItmUOM.Text = string.Empty;
            lblItmDimension.Text = string.Empty;
            lblItmWeight.Text = string.Empty;

            lblItmMainCat.Text = string.Empty;
            lblItmHSCode.Text = string.Empty;
            lblItmSerSts1.Text = "Serial 1";
            lblItmSerSts2.Text = "Serial 2";
            lblItmSerSts3.Text = "Serial 3";
            lblItmSerSts4.Text = "Serial 4";

            lblItmSerSts1.BackColor = Color.Gainsboro;
            lblItmSerSts1.ForeColor = Color.Black;
            lblItmSerSts2.BackColor = Color.Gainsboro;
            lblItmSerSts2.ForeColor = Color.Black;
            lblItmSerSts3.BackColor = Color.Gainsboro;
            lblItmSerSts3.ForeColor = Color.Black;
            lblItmSerSts4.BackColor = Color.Gainsboro;
            lblItmSerSts4.ForeColor = Color.Black;

            lblItmHPAvailability.Text = string.Empty;
            lblItmInsuAvailability.Text = string.Empty;
            picItem.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.no_image;
            if (_isAdvVisible == false) if (pnlAdvItem.Visible) this.Size = new Size(this.Width, this.Height - pnlAdvItem.Height);
            if (_isAdvVisible) pnlAdvItem.Visible = true; else pnlAdvItem.Visible = false;

        }
        private void ClearBar5()
        {
            lblWaraNo.Text = "Warranty No : ";
            lblWaraPeriod.Text = "Period : ";
        }
        private void ClearBar6()
        {
            BindingSource _source = new BindingSource();
            gvSubSerial.DataSource = _source;
        }
        private void ClearBar7()
        {
            BindingSource _source1 = new BindingSource();
            gvMovement.DataSource = _source1;

            BindingSource _source2 = new BindingSource();
            gvSale.DataSource = _source2;

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you need to clear the screen?", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            ClearBar7();
            ClearBar6();
            ClearBar5();
            ClearBar4(false);
            ClearBar3();
            ClearBar2();
            ClearBar1(true);
            btnViewServiceHistory.Visible = false;
        }
        #endregion

        //need sp return data table which location description/company description and item status description
        private void GetCurrentAvailableDetail(DataTable _availableSerial)
        {
            ClearBar2();
            if (_availableSerial != null)
            {
                if (string.IsNullOrEmpty(_availableSerial.Rows[0].Field<string>("INS_COM")))
                {
                    lblCurLocation.Text = _availableSerial.Rows[0].Field<string>("INS_LOC") + " - " + _availableSerial.Rows[0].Field<string>("ML_LOC_DESC");
                    lblCurCompany.Text = _availableSerial.Rows[0].Field<string>("INS_COM") + " - " + _availableSerial.Rows[0].Field<string>("MC_DESC");
                    lblCurReceivedDate.Text = Convert.ToDateTime(_availableSerial.Rows[0].Field<string>("INS_DOC_DT")).ToString("dd/MMM/yyyy");
                    lblCurBin.Text = _availableSerial.Rows[0].Field<string>("INS_BIN");
                    lblCurItemStatus.Text = _availableSerial.Rows[0].Field<string>("INS_ITM_STUS");
                }
            }
        }
        
        private bool GetItemAdvanceDetail(string _item)
        {
            bool _isValid = false;
            try
            {
                ClearBar3();
                ClearBar4(pnlAdvItem.Visible);

                MasterItem _itm = new MasterItem();

                if (!string.IsNullOrEmpty(_item)) _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                if (_itm != null)
                    if (!string.IsNullOrEmpty(_itm.Mi_cd))
                    {
                        _isValid = true;
                        string _description = _itm.Mi_longdesc;
                        string _model = _itm.Mi_model;
                        string _brand = _itm.Mi_brand;

                        lblItemDescription.Text = "Description : " + _description;
                        lblItemModel.Text = "Model : " + _model;
                        lblItemBrand.Text = "Brand : " + _brand;

                        lblItmColor.Text = _itm.Mi_color_ext == "NULL" ? string.Empty : _itm.Mi_color_ext;
                        lblItmUOM.Text = _itm.Mi_itm_uom == "NULL" ? string.Empty : _itm.Mi_itm_uom;
                        lblItmDimension.Text = _itm.Mi_dim_width.ToString() + " x " + _itm.Mi_dim_height.ToString() + " x " + _itm.Mi_dim_length.ToString() + " " + (_itm.Mi_dim_uom == "NULL" ? string.Empty : _itm.Mi_dim_uom);
                        lblItmWeight.Text = _itm.Mi_net_weight.ToString() + " " + (_itm.Mi_weight_uom == "NULL" ? string.Empty : _itm.Mi_weight_uom);
                        lblItmMainCat.Text = _itm.Mi_cate_1;
                        lblItmHSCode.Text = _itm.Mi_hs_cd;
                        if (_itm.Mi_is_ser1 == 1)
                        { lblItmSerSts1.BackColor = Color.LawnGreen; lblItmSerSts1.ForeColor = Color.Black; }
                        else
                        { lblItmSerSts1.BackColor = Color.Crimson; lblItmSerSts1.ForeColor = Color.White; }

                        if (_itm.Mi_is_ser2 == 1)
                        { lblItmSerSts2.BackColor = Color.LawnGreen; lblItmSerSts2.ForeColor = Color.Black; }
                        else
                        { lblItmSerSts2.BackColor = Color.Crimson; lblItmSerSts2.ForeColor = Color.White; }

                        if (_itm.Mi_is_ser3)
                        { lblItmSerSts3.BackColor = Color.LawnGreen; lblItmSerSts3.ForeColor = Color.Black; }
                        else
                        { lblItmSerSts3.BackColor = Color.Crimson; lblItmSerSts3.ForeColor = Color.White; }

                        lblItmHPAvailability.Text = _itm.Mi_hp_allow ? "Yes" : "No";
                        lblItmInsuAvailability.Text = _itm.Mi_insu_allow ? "Yes" : "No";

                    }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return _isValid;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
            return _isValid;
        }

        private void lblAdvItem_Click(object sender, EventArgs e)
        {
            if (pnlAdvItem.Visible)
            {
                pnlAdvItem.Visible = false;
                this.Size = new Size(this.Width, this.Height - pnlAdvItem.Height);
            }
            else
            {
                pnlAdvItem.Visible = true;
                this.Size = new Size(this.Width, this.Height + pnlAdvItem.Height);
            }
        }

        DataTable _InitialStageSearch = null;
        private void btnGetDetail_Click(object sender, EventArgs e)
        {


            string _serialtype = cmbSerialType.Text.Trim();
            string _characterCase = cmbCaseType.Text.Trim();
            bool _isMatchWholeWord = chkWholeWord.Checked;

            if (string.IsNullOrEmpty(txtSerialNo.Text))
            {
                MessageBox.Show("Please select the serial no", "Serial No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSerialNo.Clear();
                txtSerialNo.Focus();
                return;
            }

            if (txtSerialNo.Text.Substring(0, 1) == "%"  && chkWholeWord.Checked==false)
            {
                MessageBox.Show("You can not add % as starting character");
                return;
            }


            string _serial = string.Empty;
            string _serialType = string.Empty;
            Int16 _isWholeWord = Convert.ToInt16(chkWholeWord.Checked ? 1 : 0);

            if (cmbSerialType.Text.Trim() == "Serial 1")
                _serialType = "SERIAL1";
            else if (cmbSerialType.Text.Trim() == "Serial 2")
                _serialType = "SERIAL2";
            else if (cmbSerialType.Text.Trim() == "Serial 3")
                _serialType = "SERIAL3";
            else if (cmbSerialType.Text.Trim() == "Serial 4")
                _serialType = "SERIAL4";

            if (cmbCaseType.Text == "Normal")
                _serial = txtSerialNo.Text.Trim();
            else if (cmbCaseType.Text == "Upper")
                _serial = txtSerialNo.Text.Trim().ToUpper();
            else if (cmbCaseType.Text == "Lower")
                _serial = txtSerialNo.Text.Trim().ToLower();

            if (_isWholeWord == 0) _serial += "%";

   
            _InitialStageSearch = CHNLSVC.Inventory.GetSerialItem(_serialType, BaseCls.GlbUserComCode, _serial, _isWholeWord);

            if (_InitialStageSearch.Rows.Count == 0)
            {
                MessageBox.Show("There is no such serial available in the system for the given criteria", "No Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblItem.Text = string.Empty;
                GetItemAdvanceDetail(lblItem.Text.Trim());
                if (!string.IsNullOrEmpty(lblItem.Text)) { pnlItem.Visible = true; }

                AssignCurrentLocation(null);
                BindingSource _source = new BindingSource();
                _source.DataSource = new DataTable();
                gvSubSerial.DataSource = _source;
                gvMovement.DataSource = _source;
                gvSale.DataSource = _source;
                txtSerialNo.Focus();
                return;
            }

            if (_InitialStageSearch.Rows.Count > 1)
            {
                //Many Items
                BindingSource _source = new BindingSource();
                _source.DataSource = _InitialStageSearch;
                gvMultipleItem.DataSource = _source;
                pnlMultipleItem.Visible = true;
                if (pnlAdvItem.Visible) label53.BackColor = Color.White; else label53.BackColor = Color.Lavender;
                return;
            }

            if (_InitialStageSearch.Rows.Count > 0)
            {
                string _item = Convert.ToString(_InitialStageSearch.Rows[0].Field<string>("ins_itm_cd"));
                _serial = txtSerialNo.Text.Trim();
                lblItem.Text = _item;
                if (!string.IsNullOrEmpty(lblItem.Text)) { pnlItem.Visible = true; }

                GetItemAdvanceDetail(_item);

                DataTable _currentLocation = CHNLSVC.Inventory.GetSeriaLocation(_serialType, BaseCls.GlbUserComCode, _serial, _item);
                ClearBar2();
                AssignCurrentLocation(_currentLocation);

                ClearBar7();
                DataTable _movement = CHNLSVC.Inventory.GetSeriaMovement(_serialType, BaseCls.GlbUserComCode, _serial, _item);
                AssignMovement(_movement);

                if (_movement.Rows.Count > 0)
                {
                    var _do = _movement.AsEnumerable().Where(x => x.Field<string>("ITH_DOC_TP") == "DO" || x.Field<string>("ITH_DOC_TP") == "DO").ToList().Select(y => y.Field<string>("ITH_OTH_DOCNO"));

                    if (_do != null)
                        if (_do.Count() > 0)
                        {
                            DataTable _mgTbl = new DataTable();
                            foreach (string _doc in _do)
                            {
                                DataTable _sale = CHNLSVC.Sales.GetInvoiceDetail(BaseCls.GlbUserComCode, _doc, _item);
                                _mgTbl.Merge(_sale);
                            }

                            AssignSale(_mgTbl);
                        }
                }
            }

        }

        private void txtSerialNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSerialNo.Text)) return;

                DataTable _multiItemforSerial = CHNLSVC.Inventory.GetMultipleItemforOneSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtSerialNo.Text.Trim(), string.Empty);
                Int32 _isAvailable = _multiItemforSerial.Rows.Count;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void AssignCurrentLocation(DataTable _currentLocation)
        {
            if (_currentLocation == null)
            {
                lblCurBin.Text = string.Empty;
                lblCurCompany.Text = string.Empty;
                lblCurItemStatus.Text = string.Empty;
                lblCurLocation.Text = string.Empty;
                lblCurReceivedDate.Text = string.Empty;
                return;
            }


            if (_currentLocation.Rows.Count > 0)
            {

                lblCurBin.Text = Convert.ToString(_currentLocation.Rows[0].Field<string>("INS_BIN"));
                lblCurCompany.Text = Convert.ToString(_currentLocation.Rows[0].Field<string>("INS_COM")) + " - " + Convert.ToString(_currentLocation.Rows[0].Field<string>("MC_DESC"));
                lblCurItemStatus.Text = Convert.ToString(_currentLocation.Rows[0].Field<string>("MIS_DESC"));
                lblCurLocation.Text = Convert.ToString(_currentLocation.Rows[0].Field<string>("INS_LOC")) + " - " + Convert.ToString(_currentLocation.Rows[0].Field<string>("ML_LOC_DESC"));
                lblCurReceivedDate.Text = Convert.ToDateTime(_currentLocation.Rows[0].Field<DateTime>("INS_DOC_DT")).Date.ToShortDateString();

            }

        }
        private void AssignMovement(DataTable _movement)
        {
            if (_movement == null) return;
            if (_movement.Rows.Count > 0)
            {
                BindingSource _source = new BindingSource();
                _source.DataSource = _movement;
                gvMovement.DataSource = _source;
            }
  
        }
        private void AssignSale(DataTable _sale)
        {
            if (_sale == null) return;
            if (_sale.Rows.Count > 0)
            {
                BindingSource _source = new BindingSource();
                _source.DataSource = _sale;
                gvSale.DataSource = _source;
            }
        }

        private void TakeItemAndLoadSerialDetails(int _row)
        {
            try
            {
                if (_InitialStageSearch.Rows.Count == 0) return;
                if (_InitialStageSearch.Rows.Count > 0)
                {
                    string _item = Convert.ToString(gvMultipleItem.Rows[_row].Cells["MuItm_Item"].Value);
                    string _serial = Convert.ToString(gvMultipleItem.Rows[_row].Cells["MuItm_Serial"].Value);
                    txtSerialNo.Text = _serial;
                    lblItem.Text = _item;
                    if (!string.IsNullOrEmpty(lblItem.Text)) 
                    {
                        pnlItem.Visible = true;
                        List<Service_job_Det> oJobItems = CHNLSVC.CustService.GET_SCV_JOB_DET_BY_SERIAL(txtSerialNo.Text, lblItem.Text, BaseCls.GlbUserComCode);
                        if (oJobItems != null)
                        {
                            btnViewServiceHistory.Visible = true;
                        }
                    }

                    string _serialtype = cmbSerialType.Text.Trim();
                    string _characterCase = cmbCaseType.Text.Trim();
                    bool _isMatchWholeWord = chkWholeWord.Checked;

                    if (string.IsNullOrEmpty(txtSerialNo.Text))
                    {
                        MessageBox.Show("Please select the serial no", "Serial No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSerialNo.Clear();
                        txtSerialNo.Focus();
                        return;
                    }


                    string _serialType = string.Empty;
                    Int16 _isWholeWord = Convert.ToInt16(chkWholeWord.Checked ? 1 : 0);

                    if (cmbSerialType.Text.Trim() == "Serial 1")
                        _serialType = "SERIAL1";
                    else if (cmbSerialType.Text.Trim() == "Serial 2")
                        _serialType = "SERIAL2";
                    else if (cmbSerialType.Text.Trim() == "Serial 3")
                        _serialType = "SERIAL3";
                    else if (cmbSerialType.Text.Trim() == "Serial 4")
                        _serialType = "SERIAL4";

                    if (cmbCaseType.Text == "Normal")
                        _serial = txtSerialNo.Text.Trim();
                    else if (cmbCaseType.Text == "Upper")
                        _serial = txtSerialNo.Text.Trim().ToUpper();
                    else if (cmbCaseType.Text == "Lower")
                        _serial = txtSerialNo.Text.Trim().ToLower();


                    GetItemAdvanceDetail(_item);
                    DataTable _currentLocation = CHNLSVC.Inventory.GetSeriaLocation(_serialType, BaseCls.GlbUserComCode, _serial, _item);
                    ClearBar2();
                    AssignCurrentLocation(_currentLocation);

                    ClearBar7();
                    DataTable _movement = CHNLSVC.Inventory.GetSeriaMovement(_serialType, BaseCls.GlbUserComCode, _serial, _item);
                    AssignMovement(_movement);

                    if (_movement.Rows.Count > 0)
                    {
                        var _do = _movement.AsEnumerable().Where(x => x.Field<string>("ITH_DOC_TP") == "DO" || x.Field<string>("ITH_DOC_TP") == "DO").ToList().Select(y => y.Field<string>("ITH_OTH_DOCNO"));

                        if (_do != null)
                            if (_do.Count() > 0)
                            {
                                DataTable _mgTbl = new DataTable();
                                foreach (string _doc in _do)
                                {
                                    DataTable _sale = CHNLSVC.Sales.GetInvoiceDetail(BaseCls.GlbUserComCode, _doc, _item);
                                    _mgTbl.Merge(_sale);
                                }

                                AssignSale(_mgTbl);
                            }
                    }
                    pnlMultipleItem.Visible = false;
                    return;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void gvMultipleItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvMultipleItem.RowCount <= 0) return;
                int _row = e.RowIndex;
                if (_row != -1)
                {
                    TakeItemAndLoadSerialDetails(_row);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void gvMultipleItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (gvMultipleItem.RowCount <= 0) return;

                if (e.KeyCode == Keys.Enter)
                {
                    int _row = gvMultipleItem.SelectedRows[0].Index;
                    if (_row != -1)
                    {
                        TakeItemAndLoadSerialDetails(_row);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnPnlMuItemClose_Click(object sender, EventArgs e)
        {
            pnlMultipleItem.Visible = false;//gvSubSerial.DataSource
        }

        private void txtSerialNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnGetDetail.Focus();
        }

        
        private void btnViewServiceHistory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSerialNo.Text)) { MessageBox.Show("Please enter the serail number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); return;}
            else if (string.IsNullOrEmpty(lblItem.Text)) { MessageBox.Show("Item details not found. Please enter valid serail no", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            else 
            {
                LoadServiceJobHistorty(txtSerialNo.Text.Trim(), lblItem.Text.Trim());
            }
        }

        private void LoadServiceJobHistorty(string _serial, string item)
        {
            try
            {                
                _serviceHistory = new DataTable();
                _serviceHistory = CHNLSVC.CustService.GetServiceJobHistoryBySerial(_serial, item);                

                if (_serviceHistory.Rows.Count > 0)
                {
                    ServiceHistory _frmServiceHistory = new ServiceHistory();
                    _frmServiceHistory.ServiceJobHistory = _serviceHistory;
                    _frmServiceHistory.ShowDialog();
                }
                else { MessageBox.Show("Couldn't find service details for given serial number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while searching service details" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtSerialNo_TextChanged(object sender, EventArgs e)
        {
            btnViewServiceHistory.Visible = false;
        }
    }
}
